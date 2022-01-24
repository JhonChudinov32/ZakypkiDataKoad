using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Xml;
using GosZakypkiDataLoad.connection;

namespace GosZakypkiDataLoad.parser
{
    public class ParserNotification
    {
        public static void Notification(string[] file)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                foreach (string fil in file)
                {
                    if (File.ReadAllBytes(fil).Length > 0)
                    {
                        String purchaseObjectDescription = "";
                        String purchaseNumber = "";
                        String docPublishDate = "";
                        String purchaseObjectInfo = "";
                        String inn = "";
                        String zakazchik = "";
                        String placingWay = "";
                        String purchaseObject = "";
                        String OKPD2_name = "";
                        String href = "";
                        DateTime DateString = DateTime.Now;

                        xDoc.Load(fil);

                        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xDoc.NameTable);
                        namespaceManager.AddNamespace("q", "http://zakupki.gov.ru/oos/types/1");
                        namespaceManager.AddNamespace("p", "http://zakupki.gov.ru/oos/EPtypes/1");
                        namespaceManager.AddNamespace("r", "http://zakupki.gov.ru/oos/common/1");
                        namespaceManager.AddNamespace("s", "http://zakupki.gov.ru/oos/base/1");

                        if (xDoc.SelectSingleNode("//q:purchaseNumber", namespaceManager) != null)
                        {
                            purchaseNumber = xDoc.SelectSingleNode("//q:purchaseNumber", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//q:docPublishDate", namespaceManager) != null)
                        {
                            docPublishDate = xDoc.SelectSingleNode("//q:docPublishDate", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//q:purchaseObjectInfo", namespaceManager) != null)
                        {
                            purchaseObjectInfo = xDoc.SelectSingleNode("//q:purchaseObjectInfo", namespaceManager).InnerText;
                            purchaseObjectInfo = purchaseObjectInfo.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:INN", namespaceManager) != null)
                        {
                            inn = xDoc.SelectSingleNode("//q:INN", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//q:responsibleOrg/q:fullName", namespaceManager) != null)
                        {
                            zakazchik = xDoc.SelectSingleNode("//q:responsibleOrg/q:fullName", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//q:placingWay/q:name", namespaceManager) != null)
                        {
                            placingWay = xDoc.SelectSingleNode("//q:placingWay/q:name", namespaceManager).InnerText;
                            placingWay = placingWay.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:purchaseObjectDescription", namespaceManager) != null)
                        {
                            purchaseObjectDescription = xDoc.SelectSingleNode("//q:purchaseObjectDescription", namespaceManager).InnerText;
                            purchaseObjectDescription = purchaseObjectDescription.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:purchaseObject/q:name", namespaceManager) != null)
                        {
                            purchaseObject = xDoc.SelectSingleNode("//q:purchaseObject/q:name", namespaceManager).InnerText;
                            purchaseObject = purchaseObject.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:OKPD2/q:name", namespaceManager) != null)
                        {
                            OKPD2_name = xDoc.SelectSingleNode("//q:OKPD2/q:name", namespaceManager).InnerText;
                            OKPD2_name = OKPD2_name.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//q:href", namespaceManager) != null)
                        {
                            href = xDoc.SelectSingleNode("//q:href", namespaceManager).InnerText;
                        }

                        if (xDoc.SelectSingleNode("//p:purchaseNumber", namespaceManager) != null)
                        {
                            purchaseNumber = xDoc.SelectSingleNode("//p:purchaseNumber", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//p:publishDTInEIS", namespaceManager) != null)
                        {
                            docPublishDate = xDoc.SelectSingleNode("//p:publishDTInEIS", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//p:purchaseObjectInfo", namespaceManager) != null)
                        {
                            purchaseObjectInfo = xDoc.SelectSingleNode("//p:purchaseObjectInfo", namespaceManager).InnerText;
                            purchaseObjectInfo = purchaseObjectInfo.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//p:INN", namespaceManager) != null)
                        {
                            inn = xDoc.SelectSingleNode("//p:INN", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//p:responsibleOrgInfo/p:fullName", namespaceManager) != null)
                        {
                            zakazchik = xDoc.SelectSingleNode("//p:responsibleOrgInfo/p:fullName", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//p:placingWay/p:name", namespaceManager) != null)
                        {
                            placingWay = xDoc.SelectSingleNode("//p:placingWay/p:name", namespaceManager).InnerText;
                            placingWay = placingWay.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//p:purchaseObjectDescription", namespaceManager) != null)
                        {
                            purchaseObjectDescription = xDoc.SelectSingleNode("//p:purchaseObjectDescription", namespaceManager).InnerText;
                            purchaseObjectDescription = purchaseObjectDescription.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//p:href", namespaceManager) != null)
                        {
                            href = xDoc.SelectSingleNode("//p:href", namespaceManager).InnerText;
                        }

                        if (xDoc.SelectSingleNode("//r:purchaseObject/r:name", namespaceManager) != null)
                        {
                            purchaseObject = xDoc.SelectSingleNode("//r:purchaseObject/r:name", namespaceManager).InnerText;
                        }

                        if (xDoc.SelectSingleNode("//r:OKPD2/s:OKPDName", namespaceManager) != null)
                        {
                            OKPD2_name = xDoc.SelectSingleNode("//r:OKPD2/s:OKPDName", namespaceManager).InnerText;
                            OKPD2_name = OKPD2_name.Replace("'", "''");
                        }
                        if (xDoc.SelectSingleNode("//s:purchaseObject/s:name", namespaceManager) != null)
                        {
                            purchaseObject = xDoc.SelectSingleNode("//s:purchaseObject/s:name", namespaceManager).InnerText;
                        }
                        if (xDoc.SelectSingleNode("//q:placingWay/s:name", namespaceManager) != null)
                        {
                            placingWay = xDoc.SelectSingleNode("//q:placingWay/s:name", namespaceManager).InnerText;
                        }

                        int m = 0;
                        SqlCommand SqlProv = new SqlCommand(@"SELECT COUNT(purchaseNumber) As CountTabNum FROM dbo.[44_Notification] WHERE [purchaseNumber]= '" + purchaseNumber + "' ", Connect44FZ.cnn);
                        Connect44FZ.cnn.Open();
                        m = (Int32)(SqlProv.ExecuteScalar());
                        Connect44FZ.cnn.Close();
                        if (m == 0)
                        {
                            Connect44FZ.cnn.Open();
                            // внесение данных в БД
                            SqlCommand cmd44 = new SqlCommand(@"INSERT INTO dbo.[44_Notification]([purchaseNumber],[purchaseObjectInfo],[docPublishDate],[purchaseObjectDescription],[inn],[zakazchik],[placingWay],[OKPD2_name],[href],[dataZagryzki],[purchaseObject]) VALUES (@purchaseNumber,@purchaseObjectInfo,@docPublishDate,@purchaseObjectDescription,@inn,@zakazchik,@placingWay,@OKPD2_name,@href,@DateString,@purchaseObject)", Connect44FZ.cnn);

                            cmd44.Parameters.AddWithValue("@purchaseNumber", purchaseNumber);
                            cmd44.Parameters.AddWithValue("@purchaseObjectInfo", purchaseObjectInfo);
                            cmd44.Parameters.AddWithValue("@docPublishDate", docPublishDate);
                            cmd44.Parameters.AddWithValue("@purchaseObjectDescription", purchaseObjectDescription);
                            cmd44.Parameters.AddWithValue("@inn", inn);
                            cmd44.Parameters.AddWithValue("@zakazchik", zakazchik);
                            cmd44.Parameters.AddWithValue("@placingWay", placingWay);
                            cmd44.Parameters.AddWithValue("@OKPD2_name", OKPD2_name);
                            cmd44.Parameters.AddWithValue("@href", href);
                            cmd44.Parameters.AddWithValue("@DateString", DateString);
                            cmd44.Parameters.AddWithValue("@purchaseObject", purchaseObject);

                            cmd44.ExecuteNonQuery();
                            SqlCommand tr = new SqlCommand(@"delete from [44_Notification] where COALESCE(purchaseObjectDescription, '') = '' and COALESCE(inn, '') = ''", Connect44FZ.cnn);
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
