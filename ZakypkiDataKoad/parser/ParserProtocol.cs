using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Xml;
using GosZakypkiDataLoad.connection;

namespace GosZakypkiDataLoad.parser
{
    public class ParserProtocol
    {
        public static void Protocol(string[] file)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                foreach (string fil in file)
                {
                    if (File.ReadAllBytes(fil).Length > 0)
                    {
                        String id = "";
                        String purchaseNumber = "";
                        String protocolNumber = "";
                        String protocolDate = "";
                        String abandonedReason = "";
                        DateTime DateString = DateTime.Now;
                        xDoc.Load(fil);

                        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xDoc.NameTable);
                        namespaceManager.AddNamespace("q", "http://zakupki.gov.ru/oos/types/1");
                        namespaceManager.AddNamespace("s", "http://zakupki.gov.ru/oos/common/1");
                        namespaceManager.AddNamespace("p", "http://zakupki.gov.ru/oos/EPtypes/1");
                        namespaceManager.AddNamespace("n", "http://zakupki.gov.ru/oos/base/1");
                        namespaceManager.AddNamespace("f", "http://zakupki.gov.ru/oos/common/1");

                        if (xDoc.SelectSingleNode("//q:id", namespaceManager) != null)
                        {
                            id = xDoc.SelectSingleNode("//q:id", namespaceManager).InnerText;
                        }

                        if (xDoc.SelectSingleNode("//p:id", namespaceManager) != null)
                        {
                            id = xDoc.SelectSingleNode("//p:id", namespaceManager).InnerText;
                        }

                        if (xDoc.SelectSingleNode("//q:purchaseNumber", namespaceManager) != null)
                        {
                            purchaseNumber = xDoc.SelectSingleNode("//q:purchaseNumber", namespaceManager).InnerText;
                        }

                        if (xDoc.SelectSingleNode("//p:purchaseNumber", namespaceManager) != null)
                        {
                            purchaseNumber = xDoc.SelectSingleNode("//p:purchaseNumber", namespaceManager).InnerText;
                        }

                        if (xDoc.SelectSingleNode("//q:protocolNumber", namespaceManager) != null)
                        {
                            protocolNumber = xDoc.SelectSingleNode("//q:protocolNumber", namespaceManager).InnerText;
                        }

                        if (xDoc.SelectSingleNode("//p:foundationDocNumber", namespaceManager) != null)
                        {
                            protocolNumber = xDoc.SelectSingleNode("//p:foundationDocNumber", namespaceManager).InnerText;
                        }

                        if (xDoc.SelectSingleNode("//q:protocolDate", namespaceManager) != null)
                        {
                            protocolDate = xDoc.SelectSingleNode("//q:protocolDate", namespaceManager).InnerText;
                            protocolDate = protocolDate.Substring(0, 10);
                        }

                        if (xDoc.SelectSingleNode("//p:publishDTInEIS", namespaceManager) != null)
                        {
                            protocolDate = xDoc.SelectSingleNode("//p:publishDTInEIS", namespaceManager).InnerText;
                            protocolDate = protocolDate.Substring(0, 10);
                        }

                        if (xDoc.SelectSingleNode("//q:abandonedReason/q:name", namespaceManager) != null)
                        {
                            abandonedReason = xDoc.SelectSingleNode("//q:abandonedReason/q:name", namespaceManager).InnerText;
                        }

                        if (xDoc.SelectSingleNode("//p:abandonedReason/n:name", namespaceManager) != null)
                        {
                            abandonedReason = xDoc.SelectSingleNode("//p:abandonedReason/n:name", namespaceManager).InnerText;
                        }

                        int k = 1;
                        //For Each org As Xml.XmlNode In doc.SelectNodes("//q:application", namespaceManager)
                        foreach (XmlNode org in xDoc.SelectNodes("//q:application", namespaceManager))
                        {
                            String inn = "";
                            String kpp = "";
                            String appRating = "";
                            String organizationName = "";
                            String resultType = "";
                            String explanation = "";
                            String admitted = "";
                            String lastName = "";
                            String firstName = "";
                            String middleName = "";

                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipants/q:appParticipant/q:inn", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipants/q:appParticipant/q:inn", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipant/q:inn", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipant/q:inn", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:legalEntityRFInfo/s:INN", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:legalEntityRFInfo/s:INN", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:individualPersonRFInfo/s:INN", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:individualPersonRFInfo/s:INN", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipants/q:appParticipant/q:kpp", namespaceManager) != null)
                            {
                                kpp = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipants/q:appParticipant/q:kpp", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipant/q:kpp", namespaceManager) != null)
                            {
                                kpp = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipant/q:kpp", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:legalEntityRFInfo/s:KPP", namespaceManager) != null)
                            {
                                kpp = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:legalEntityRFInfo/s:KPP", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:individualPersonRFInfo/s:KPP", namespaceManager) != null)
                            {
                                kpp = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:individualPersonRFInfo/s:KPP", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:legalEntityRFInfo/s:fullName", namespaceManager) != null)
                            {
                                organizationName = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:legalEntityRFInfo/s:fullName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:individualPersonRFInfo/s:nameInfo/s:lastName", namespaceManager) != null)
                            {
                                lastName = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:individualPersonRFInfo/s:nameInfo/s:lastName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:individualPersonRFInfo/s:nameInfo/s:firstName", namespaceManager) != null)
                            {
                                firstName = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:individualPersonRFInfo/s:nameInfo/s:firstName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:individualPersonRFInfo/s:nameInfo/s:middleName", namespaceManager) != null)
                            {
                                middleName = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipantInfo/s:individualPersonRFInfo/s:nameInfo/s:middleName", namespaceManager).InnerText;
                            }
                            if (lastName != "" && middleName != "" && firstName != "")
                            {
                                organizationName = lastName + " " + firstName + " " + middleName;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipants/q:appParticipant/q:organizationName", namespaceManager) != null)
                            {
                                organizationName = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipants/q:appParticipant/q:organizationName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipant/q:organizationName", namespaceManager) != null)
                            {
                                organizationName = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipant/q:organizationName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipants/q:appParticipant/q:firmName", namespaceManager) != null)
                            {
                                organizationName = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipants/q:appParticipant/q:firmName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipant/q:firmName", namespaceManager) != null)
                            {
                                organizationName = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appParticipant/q:firmName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appRating", namespaceManager) != null)
                            {
                                appRating = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appRating", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:admittedInfo/q:appRating", namespaceManager) != null)
                            {
                                appRating = xDoc.SelectSingleNode("//q:application[" + k + "]/q:admittedInfo/q:appRating", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:admitted", namespaceManager) != null)
                            {
                                admitted = xDoc.SelectSingleNode("//q:application[" + k + "]/q:admitted", namespaceManager).InnerText;
                                if (admitted == "true")
                                    admitted = "1";
                                else if (admitted == "false")
                                    admitted = "0";
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:admissionResults/q:admissionResult/q:admitted", namespaceManager) != null)
                            {
                                admitted = xDoc.SelectSingleNode("//q:application[" + k + "]/q:admissionResults/q:admissionResult/q:admitted", namespaceManager).InnerText;
                                if (admitted == "true")
                                    admitted = "1";
                                else if (admitted == "false")
                                    admitted = "0";
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:admittedInfo/q:resultType", namespaceManager) != null)
                            {
                                resultType = xDoc.SelectSingleNode("//q:application[" + k + "]/q:admittedInfo/q:resultType", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:admittedInfo/q:appRejectedReason/q:explanation", namespaceManager) != null)
                            {
                                explanation = xDoc.SelectSingleNode("//q:application[" + k + "]/q:admittedInfo/q:appRejectedReason/q:explanation", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appRejectedReason/q:explanation", namespaceManager) != null)
                            {
                                explanation = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appRejectedReason/q:explanation", namespaceManager).InnerText;
                            }
                            k = k + 1;

                            organizationName = organizationName.Replace("'", "''");
                            abandonedReason = abandonedReason.Replace("'", "''");
                            explanation = explanation.Replace("'", "''");

                            Connect44FZ.cnn.Open();
                            // внесение данных в БД
                            SqlCommand cmd44 = new SqlCommand(@"INSERT INTO dbo.[44_Protocol]([id_eis],[purchaseNumber],[protocolNumber],[protocolDate],[organizationName],[inn],[kpp],[appRating],[resultType],[explanation],[admitted],[abandonedReason],[dataZagryzki]) VALUES (@id,@purchaseNumber,@protocolNumber,@protocolDate,@organizationName,@inn,@kpp,@appRating,@resultType,@explanation,@admitted,@abandonedReason,@DateString)", Connect44FZ.cnn);

                            cmd44.Parameters.AddWithValue("@id", id);
                            cmd44.Parameters.AddWithValue("@purchaseNumber", purchaseNumber);
                            cmd44.Parameters.AddWithValue("@protocolNumber", protocolNumber);
                            cmd44.Parameters.AddWithValue("@protocolDate", protocolDate);
                            cmd44.Parameters.AddWithValue("@organizationName", organizationName);
                            cmd44.Parameters.AddWithValue("@inn", inn);
                            cmd44.Parameters.AddWithValue("@kpp", kpp);
                            cmd44.Parameters.AddWithValue("@appRating", appRating);
                            cmd44.Parameters.AddWithValue("@resultType", resultType);
                            cmd44.Parameters.AddWithValue("@explanation", explanation);
                            cmd44.Parameters.AddWithValue("@admitted", admitted);
                            cmd44.Parameters.AddWithValue("@abandonedReason", abandonedReason);
                            cmd44.Parameters.AddWithValue("@DateString", DateString);

                            cmd44.ExecuteNonQuery();

                            //добавляем новую запись в таблицу

                            Connect44FZ.cnn.Close();
                        }

                        foreach (XmlNode org in xDoc.SelectNodes("//p:applicationInfo", namespaceManager))
                        {
                            String inn = "";
                            String KPP = "";
                            String appRating = "";
                            String organizationName = "";
                            String resultType = "";
                            String explanation = "";
                            String admitted = "";
                            String lastName = "";
                            String firstName = "";
                            String middleName = "";

                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:legalEntityRFInfo/p:fullName", namespaceManager) != null)
                            {
                                organizationName = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:legalEntityRFInfo/p:fullName", namespaceManager).InnerText;
                            }

                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:legalEntityRFInfo/f:fullName", namespaceManager) != null)
                            {
                                organizationName = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:legalEntityRFInfo/f:fullName", namespaceManager).InnerText;
                            }

                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:individualPersonRFInfo/p:nameInfo/f:lastName", namespaceManager) != null)
                            {
                                lastName = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:individualPersonRFInfo/p:nameInfo/f:lastName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:individualPersonRFInfo/p:nameInfo/f:firstName", namespaceManager) != null)
                            {
                                firstName = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:individualPersonRFInfo/p:nameInfo/f:firstName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:individualPersonRFInfo/p:nameInfo/f:middleName", namespaceManager) != null)
                            {
                                middleName = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:individualPersonRFInfo/p:nameInfo/f:middleName", namespaceManager).InnerText;
                            }
                            if (lastName != "" && middleName != "" && firstName != "")
                            {
                                organizationName = lastName + " " + firstName + " " + middleName;
                            }

                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:individualPersonRFInfo/f:nameInfo/f:lastName", namespaceManager) != null)
                            {
                                lastName = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:individualPersonRFInfo/f:nameInfo/f:lastName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:individualPersonRFInfo/f:nameInfo/f:firstName", namespaceManager) != null)
                            {
                                firstName = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:individualPersonRFInfo/f:nameInfo/f:firstName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:individualPersonRFInfo/f:nameInfo/f:middleName", namespaceManager) != null)
                            {
                                middleName = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:individualPersonRFInfo/f:nameInfo/f:middleName", namespaceManager).InnerText;
                            }
                            if (lastName != "" && middleName != "" && firstName != "")
                            {
                                organizationName = lastName + " " + firstName + " " + middleName;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipants/p:appParticipant/p:legalEntityRFInfo/p:fullName", namespaceManager) != null)
                            {
                                organizationName = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipants/p:appParticipant/p:legalEntityRFInfo/p:fullName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipant/p:legalEntityRFInfo/p:fullName", namespaceManager) != null)
                            {
                                organizationName = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipant/p:legalEntityRFInfo/p:fullName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipants/p:appParticipant/p:individualPersonRFInfo/p:lastName", namespaceManager) != null)
                            {
                                organizationName = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipants/p:appParticipant/p:individualPersonRFInfo/p:lastName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipant/p:individualPersonRFInfo/p:lastName", namespaceManager) != null)
                            {
                                organizationName = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipant/p:individualPersonRFInfo/p:lastName", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:legalEntityRFInfo/p:INN", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:legalEntityRFInfo/p:INN", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:legalEntityRFInfo/f:INN", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:legalEntityRFInfo/f:INN", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:individualPersonRFInfo/p:INN", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:individualPersonRFInfo/p:INN", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:individualPersonRFInfo/f:INN", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:individualPersonRFInfo/f:INN", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipants/p:appParticipant/p:legalEntityRFInfo/p:INN", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipants/p:appParticipant/p:legalEntityRFInfo/p:INN", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipant/p:legalEntityRFInfo/p:INN", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipant/p:legalEntityRFInfo/p:INN", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipants/p:appParticipant/p:individualPersonRFInfo/p:INN", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipants/p:appParticipant/p:individualPersonRFInfo/p:INN", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipant/p:individualPersonRFInfo/p:INN", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipant/p:individualPersonRFInfo/p:INN", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:legalEntityRFInfo/f:KPP", namespaceManager) != null)
                            {
                                KPP = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:legalEntityRFInfo/f:KPP", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:individualPersonRFInfo/p:KPP", namespaceManager) != null)
                            {
                                KPP = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/p:individualPersonRFInfo/p:KPP", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:individualPersonRFInfo/f:KPP", namespaceManager) != null)
                            {
                                KPP = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipantInfo/f:individualPersonRFInfo/f:KPP", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipants/p:appParticipant/p:legalEntityRFInfo/p:KPP", namespaceManager) != null)
                            {
                                KPP = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipants/p:appParticipant/p:legalEntityRFInfo/p:KPP", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipant/p:legalEntityRFInfo/p:KPP", namespaceManager) != null)
                            {
                                KPP = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipant/p:legalEntityRFInfo/p:KPP", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipants/p:appParticipant/p:individualPersonRFInfo/p:KPP", namespaceManager) != null)
                            {
                                KPP = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipants/p:appParticipant/p:individualPersonRFInfo/p:KPP", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipant/p:individualPersonRFInfo/p:KPP", namespaceManager) != null)
                            {
                                KPP = xDoc.SelectSingleNode("//p:applicationInfo[" + k + "]/p:appParticipant/p:individualPersonRFInfo/p:KPP", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appRating", namespaceManager) != null)
                            {
                                appRating = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appRating", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:admittedInfo/q:appRating", namespaceManager) != null)
                            {
                                appRating = xDoc.SelectSingleNode("//q:application[" + k + "]/q:admittedInfo/q:appRating", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:admitted", namespaceManager) != null)
                            {
                                admitted = xDoc.SelectSingleNode("//q:application[" + k + "]/q:admitted", namespaceManager).InnerText;
                                if (admitted == "true")
                                    admitted = "1";
                                else if (admitted == "false")
                                    admitted = "0";
                            }

                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:admissionResults/q:admissionResult/q:admitted", namespaceManager) != null)
                            {
                                admitted = xDoc.SelectSingleNode("//q:application[" + k + "]/q:admissionResults/q:admissionResult/q:admitted", namespaceManager).InnerText;
                                if (admitted == "true")
                                    admitted = "1";
                                else if (admitted == "false")
                                    admitted = "0";
                            }

                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:admittedInfo/q:resultType", namespaceManager) != null)
                            {
                                resultType = xDoc.SelectSingleNode("//q:application[" + k + "]/q:admittedInfo/q:resultType", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:admittedInfo/q:appRejectedReason/q:explanation", namespaceManager) != null)
                            {
                                explanation = xDoc.SelectSingleNode("//q:application[" + k + "]/q:admittedInfo/q:appRejectedReason/q:explanation", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//q:application[" + k + "]/q:appRejectedReason/q:explanation", namespaceManager) != null)
                            {
                                explanation = xDoc.SelectSingleNode("//q:application[" + k + "]/q:appRejectedReason/q:explanation", namespaceManager).InnerText;
                            }
                            k = k + 1;

                            organizationName = organizationName.Replace("'", "''");
                            abandonedReason = abandonedReason.Replace("'", "''");
                            explanation = explanation.Replace("'", "''");
                            Connect44FZ.cnn.Open();
                            // внесение данных в БД
                            SqlCommand cmd44 = new SqlCommand(@"INSERT INTO dbo.[44_Protocol]([id_eis],[purchaseNumber],[protocolNumber],[protocolDate],[organizationName],[inn],[kpp],[appRating],[resultType],[explanation],[admitted],[abandonedReason],[dataZagryzki]) VALUES (@id,@purchaseNumber,@protocolNumber,@protocolDate,@organizationName,@inn,@kpp,@appRating,@resultType,@explanation,@admitted,@abandonedReason,@DateString)", Connect44FZ.cnn);

                            cmd44.Parameters.AddWithValue("@id", id);
                            cmd44.Parameters.AddWithValue("@purchaseNumber", purchaseNumber);
                            cmd44.Parameters.AddWithValue("@protocolNumber", protocolNumber);
                            cmd44.Parameters.AddWithValue("@protocolDate", protocolDate);
                            cmd44.Parameters.AddWithValue("@organizationName", organizationName);
                            cmd44.Parameters.AddWithValue("@inn", inn);
                            cmd44.Parameters.AddWithValue("@kpp", KPP);
                            cmd44.Parameters.AddWithValue("@appRating", appRating);
                            cmd44.Parameters.AddWithValue("@resultType", resultType);
                            cmd44.Parameters.AddWithValue("@explanation", explanation);
                            cmd44.Parameters.AddWithValue("@admitted", admitted);
                            cmd44.Parameters.AddWithValue("@abandonedReason", abandonedReason);
                            cmd44.Parameters.AddWithValue("@DateString", DateString);

                            cmd44.ExecuteNonQuery();
                            SqlCommand tr = new SqlCommand(@"delete from [44_Protocol] where COALESCE(organizationName, '') = '' and COALESCE(inn, '') = ''", Connect44FZ.cnn);
                            tr.ExecuteNonQuery();
                            //добавляем новую запись в таблицу

                            Connect44FZ.cnn.Close();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
           
        }
    }
}
