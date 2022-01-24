using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Xml;
using GosZakypkiDataLoad.connection;

namespace GosZakypkiDataLoad.parser
{
    public class ParserProtocol223
    {
        public static void Protocol223(string[] file)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                foreach (string fil in file)
                {
                    if (File.ReadAllBytes(fil).Length > 0)
                    {
                        String purchaseNoticeNumber = "";
                        String createDateTime = "";
                        String name = "";
                        String purchaseCodeName = "";
                        String registrationNumber = "";
                        String typeName = "";
                        String subject = "";
                        String inn = "";
                        String accepted = "";
                        String organizationName = "";
                        String rejectionReasonCode = "";
                        String contractSigned = "";
                        String winnerIndication = "";
                        String rejectionReason = "";

                        DateTime DateString = DateTime.Now;

                        xDoc.Load(fil);
                        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xDoc.NameTable);
                        namespaceManager.AddNamespace("q", "http://zakupki.gov.ru/223fz/types/1");
                        namespaceManager.AddNamespace("p", "http://zakupki.gov.ru/223fz/purchase/1");

                        if (xDoc.SelectSingleNode("//q:purchaseNoticeNumber", namespaceManager) != null)
                        {
                            purchaseNoticeNumber = xDoc.SelectSingleNode("//q:purchaseNoticeNumber", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//q:createDateTime", namespaceManager) != null)
                        {
                            createDateTime = xDoc.SelectSingleNode("//q:createDateTime", namespaceManager).InnerText;
                            //createDateTime = createDateTime.Substring(1, 10);
                        }
                        if (xDoc.SelectSingleNode("//q:name", namespaceManager) != null)
                        {
                            name = xDoc.SelectSingleNode("//q:name", namespaceManager).InnerText;
                            name = name.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:purchaseCodeName", namespaceManager) != null)
                        {
                            purchaseCodeName = xDoc.SelectSingleNode("//q:purchaseCodeName", namespaceManager).InnerText;
                            purchaseCodeName = purchaseCodeName.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:protocolCancellation/q:body/q:item/q:cancellationReason", namespaceManager) != null)
                        {
                            rejectionReason = xDoc.SelectSingleNode("//q:protocolCancellation/q:body/q:item/q:cancellationReason", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//p:registrationNumber", namespaceManager) != null)
                        {
                            registrationNumber = xDoc.SelectSingleNode("//p:registrationNumber", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//p:typeName", namespaceManager) != null)
                        {
                            typeName = xDoc.SelectSingleNode("//p:typeName", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//p:subject", namespaceManager) != null)
                        {
                            subject = xDoc.SelectSingleNode("//p:subject", namespaceManager).InnerText;
                            subject = subject.Replace("'", "''");
                        }

                        int k = 1;
                        //For Each org As Xml.XmlNode In doc.SelectNodes("//q:application", namespaceManager)
                        foreach (XmlNode org in xDoc.SelectNodes("//p:application", namespaceManager))
                        {
                            if (xDoc.SelectSingleNode("//p:application[" + k + "]/p:supplierInfo/q:name", namespaceManager) != null)
                            {
                                organizationName = xDoc.SelectSingleNode("//p:application[" + k + "]/p:supplierInfo/q:name", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:application[" + k + "]/p:supplierInfo/q:inn", namespaceManager) != null)
                            {
                                inn = xDoc.SelectSingleNode("//p:application[" + k + "]/p:supplierInfo/q:inn", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:application[" + k + "]/p:accepted", namespaceManager) != null)
                            {
                                accepted = xDoc.SelectSingleNode("//p:application[" + k + "]/p:accepted", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:application[" + k + "]/p:winnerIndication", namespaceManager) != null)
                            {
                                winnerIndication = xDoc.SelectSingleNode("//p:application[" + k + "]/p:winnerIndication", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:application[" + k + "]/p:rejectionReasonCode", namespaceManager) != null)
                            {
                                rejectionReasonCode = xDoc.SelectSingleNode("//p:application[" + k + "]/p:rejectionReasonCode", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:application[" + k + "]/p:contractSigned", namespaceManager) != null)
                            {
                                contractSigned = xDoc.SelectSingleNode("//p:application[" + k + "]/p:contractSigned", namespaceManager).InnerText;
                            }
                            if (xDoc.SelectSingleNode("//p:application[" + k + "]/p:rejectionReason", namespaceManager) != null)
                            {
                                rejectionReason = xDoc.SelectSingleNode("//p:application[" + k + "]/p:rejectionReason", namespaceManager).InnerText;
                            }

                            k = k + 1;
                            if (organizationName != "")
                            {
                                organizationName = organizationName.Replace("'", "''");
                                purchaseCodeName = purchaseCodeName.Replace("'", "''");
                                name = name.Replace("'", "''");
                                subject = subject.Replace("'", "''");
                                typeName = typeName.Replace("'", "''");

                                Connect223FZ.cnn.Open();
                                // внесение данных в БД
                                SqlCommand cmd223 = new SqlCommand(@"INSERT INTO dbo.[223_Protocol] ([purchaseNoticeNumber],[registrationNumber],[createDateTime],[purchaseCodeName],[name],[subject],[typeName],[organizationName],[inn],[accepted],[winnerIndication],[rejectionReasonCode],[contractSigned],[rejectionReason],[dataZagryzki]) VALUES (@purchaseNoticeNumber,@registrationNumber,@createDateTime,@purchaseCodeName,@name,@subject,@typeName,@organizationName,@inn,@accepted,@winnerIndication,@rejectionReasonCode,@contractSigned,@rejectionReason,@DateString)", Connect223FZ.cnn);

                                cmd223.Parameters.AddWithValue("@purchaseNoticeNumber", purchaseNoticeNumber);
                                cmd223.Parameters.AddWithValue("@createDateTime", createDateTime);
                                cmd223.Parameters.AddWithValue("@name", name);
                                cmd223.Parameters.AddWithValue("@purchaseCodeName", purchaseCodeName);
                                cmd223.Parameters.AddWithValue("@registrationNumber", registrationNumber);
                                cmd223.Parameters.AddWithValue("@typeName", typeName);
                                cmd223.Parameters.AddWithValue("@subject", subject);
                                cmd223.Parameters.AddWithValue("@inn", inn);
                                cmd223.Parameters.AddWithValue("@accepted", accepted);
                                cmd223.Parameters.AddWithValue("@organizationName", organizationName);
                                cmd223.Parameters.AddWithValue("@rejectionReasonCode", rejectionReasonCode);
                                cmd223.Parameters.AddWithValue("@contractSigned", contractSigned);
                                cmd223.Parameters.AddWithValue("@winnerIndication", winnerIndication);
                                cmd223.Parameters.AddWithValue("@rejectionReason", rejectionReason);
                                cmd223.Parameters.AddWithValue("@DateString", DateString);

                                cmd223.ExecuteNonQuery();


                                Connect223FZ.cnn.Close();
                            }
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
