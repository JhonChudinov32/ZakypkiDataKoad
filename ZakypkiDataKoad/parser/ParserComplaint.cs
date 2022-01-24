using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Xml;
using GosZakypkiDataLoad.connection;

namespace GosZakypkiDataLoad.parser
{
    public class ParserComplaint
    {
        public static void Complaint(string[] file)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                foreach (string fil in file)
                {
                    String complaintNumber = "";
                    String versionNumber = "";
                    String planDecisionDate = "";
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
                        namespaceManager.AddNamespace("q", "http://zakupki.gov.ru/oos/types/1");

                        if (xDoc.SelectSingleNode("//q:complaintNumber", namespaceManager) != null)
                        {
                            complaintNumber = xDoc.SelectSingleNode("//q:complaintNumber", namespaceManager).InnerText;
                            complaintNumber = complaintNumber.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:versionNumber", namespaceManager) != null)
                        {
                            versionNumber = xDoc.SelectSingleNode("//q:versionNumber", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//q:planDecisionDate", namespaceManager) != null)
                        {
                            planDecisionDate = xDoc.SelectSingleNode("//q:planDecisionDate", namespaceManager).InnerText;
                            planDecisionDate = planDecisionDate.Substring(0, 10);
                        }
                        if (xDoc.SelectSingleNode("//q:publishDate", namespaceManager) != null)
                        {
                            publishDate = xDoc.SelectSingleNode("//q:publishDate", namespaceManager).InnerText;
                            publishDate = publishDate.Substring(0, 10);
                        }
                        if (xDoc.SelectSingleNode("//q:legalEntity/q:fullName", namespaceManager) != null)
                        {
                            legalEntity = xDoc.SelectSingleNode("//q:legalEntity/q:fullName", namespaceManager).InnerText;
                            legalEntity = legalEntity.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:individualPerson/q:name", namespaceManager) != null)
                        {
                            individualPerson = xDoc.SelectSingleNode("//q:individualPerson/q:name", namespaceManager).InnerText;
                            individualPerson = individualPerson.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:individualBusinessman/q:name", namespaceManager) != null)
                        {
                            individualPerson = xDoc.SelectSingleNode("//q:individualBusinessman/q:name", namespaceManager).InnerText;
                            individualPerson = individualPerson.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:purchaseNumber", namespaceManager) != null)
                        {
                            purchaseNumber = xDoc.SelectSingleNode("//q:purchaseNumber", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//q:purchaseName", namespaceManager) != null)
                        {
                            purchaseName = xDoc.SelectSingleNode("//q:purchaseName", namespaceManager).InnerText;
                            purchaseName = purchaseName.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:printForm/q:url", namespaceManager) != null)
                        {
                            url = xDoc.SelectSingleNode("//q:printForm/q:url", namespaceManager).InnerText;
                            //url = "http://zakupki.gov.ru/epz/complaint/card/complaint-information.html?id=" + url;
                        }

                        if (purchaseNumber != "")
                        {
                            if (versionNumber == "" || versionNumber == "1")
                            {
                                Connect44FZ.cnn.Open();
                                // внесение данных в БД
                                SqlCommand cmd44 = new SqlCommand(@"INSERT INTO dbo.[44_Complaint] ([complaintNumber],[planDecisionDate],[publishDate],[legalEntity],[individualPerson],[purchaseNumber],[purchaseName],[url],[dataZagryzki]) VALUES (@complaintNumber,@planDecisionDate,@publishDate,@legalEntity,@individualPerson,@purchaseNumber,@purchaseName,@url,@DateString)", Connect44FZ.cnn);

                                cmd44.Parameters.AddWithValue("@complaintNumber", complaintNumber);
                                cmd44.Parameters.AddWithValue("@planDecisionDate", planDecisionDate);
                                cmd44.Parameters.AddWithValue("@publishDate", publishDate);
                                cmd44.Parameters.AddWithValue("@legalEntity", legalEntity);
                                cmd44.Parameters.AddWithValue("@individualPerson", individualPerson);
                                cmd44.Parameters.AddWithValue("@purchaseNumber", purchaseNumber);
                                cmd44.Parameters.AddWithValue("@purchaseName", purchaseName);
                                cmd44.Parameters.AddWithValue("@url", url);
                                cmd44.Parameters.AddWithValue("@DateString", DateString);
                                cmd44.ExecuteNonQuery();

                                Connect44FZ.cnn.Close();
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
