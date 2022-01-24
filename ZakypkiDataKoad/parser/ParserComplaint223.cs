using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Xml;
using GosZakypkiDataLoad.connection;

namespace GosZakypkiDataLoad.parser
{
    public class ParserComplaint223
    {
        public static void Complaint223(string[] file)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                foreach (string fil in file)
                {

                    String firstName = "";
                    String middleName = "";
                    String lastName = "";
                    String complaintNumber = "";
                    String publishDate = "";
                    String legalEntity = "";
                    String individualPerson = "";
                    String purchaseNumber = "";
                    String purchaseName = "";
                    String url = "";
                    DateTime DateString = DateTime.Now;
                   
                    if (File.ReadAllBytes(fil).Length > 0)
                    {
                        xDoc.Load(fil);
                        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xDoc.NameTable);
                        namespaceManager.AddNamespace("q", "http://zakupki.gov.ru/223fz/complaint/1");
                        namespaceManager.AddNamespace("d", "http://zakupki.gov.ru/223fz/types/1");

                        if (xDoc.SelectSingleNode("//q:registrationNumber", namespaceManager) != null)
                        {
                            complaintNumber = xDoc.SelectSingleNode("//q:registrationNumber", namespaceManager).InnerText;
                            complaintNumber = complaintNumber.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:publishDate", namespaceManager) != null)
                        {
                            publishDate = xDoc.SelectSingleNode("//q:publishDate", namespaceManager).InnerText;
                            publishDate = publishDate.Substring(0, 10);
                        }
                        if (xDoc.SelectSingleNode("//q:legalPerson/q:organizationName", namespaceManager) != null)
                        {
                            legalEntity = xDoc.SelectSingleNode("//q:legalPerson/q:organizationName", namespaceManager).InnerText;
                            legalEntity = legalEntity.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:individualPerson/q:firstName", namespaceManager) != null)
                        {
                            firstName = xDoc.SelectSingleNode("//q:individualPerson/q:firstName", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//q:individualPerson/q:middleName", namespaceManager) != null)
                        {
                            middleName = xDoc.SelectSingleNode("//q:individualPerson/q:middleName", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//q:individualPerson/q:lastName", namespaceManager) != null)
                        {
                            lastName = xDoc.SelectSingleNode("//q:individualPerson/q:lastName", namespaceManager).InnerText;
                        }
                        if (lastName != "" && middleName != "" && firstName != "")
                        {
                            individualPerson = lastName + " " + firstName + " " + middleName;
                        }
                        if (xDoc.SelectSingleNode("//q:complaintPurchaseInfo/d:purchaseNoticeNumber", namespaceManager) != null)
                        {
                            purchaseNumber = xDoc.SelectSingleNode("//q:complaintPurchaseInfo/d:purchaseNoticeNumber", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//../d:name", namespaceManager) != null)
                        {
                            purchaseName = xDoc.SelectSingleNode("//../d:name", namespaceManager).InnerText;
                            purchaseName = purchaseName.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:urlEIS", namespaceManager) != null)
                        {
                            url = xDoc.SelectSingleNode("//q:urlEIS", namespaceManager).InnerText;
                            //url = "http://zakupki.gov.ru/epz/complaint/card/complaint-information.html?id=" + url;
                        }

                        if (purchaseNumber != "")
                        {

                            Connect223FZ.cnn.Open();
                            // внесение данных в БД
                            SqlCommand cmd223 = new SqlCommand(@"INSERT INTO dbo.[223_Complaint] ([complaintNumber],[publishDate],[legalEntity],[individualPerson],[purchaseNumber],[purchaseName],[url],[dataZagryzki]) VALUES (@complaintNumber,@publishDate,@legalEntity,@individualPerson,@purchaseNumber,@purchaseName,@url,@DateString)", Connect223FZ.cnn);

                            cmd223.Parameters.AddWithValue("@complaintNumber", complaintNumber);
                            cmd223.Parameters.AddWithValue("@publishDate", publishDate);
                            cmd223.Parameters.AddWithValue("@legalEntity", legalEntity);
                            cmd223.Parameters.AddWithValue("@individualPerson", individualPerson);
                            cmd223.Parameters.AddWithValue("@purchaseNumber", purchaseNumber);
                            cmd223.Parameters.AddWithValue("@purchaseName", purchaseName);
                            cmd223.Parameters.AddWithValue("@url", url);
                            cmd223.Parameters.AddWithValue("@DateString", DateString);
                            cmd223.ExecuteNonQuery();

                            Connect223FZ.cnn.Close();

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
