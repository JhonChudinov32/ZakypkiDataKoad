using System;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using GosZakypkiDataLoad.connection;

namespace GosZakypkiDataLoad.parser
{
    public class ParserContarct
    {
        public static void Contarct(string[] file)
        {
            //Обработка XML в разделе Контрактов ресурса ГОСЗАКУПКИ
            try
            {
                XmlDocument xDoc = new XmlDocument();
                foreach (string fil in file)
                {
                    String attr = "";
                    String attr0 = "";
                    String attr1 = "";
                    String attr2 = "";
                    String attr3 = "";
                    String attr4 = "";
                    String attr41 = "";
                    String attr42 = "";
                    String attr5 = "";
                    String attr6 = "";
                    String attr7 = "";
                    String attr8 = "";
                    String attr9 = "";
                    String attr10 = "";
                    String attr11 = "";
                    String attr12 = "";
                    String attr13 = "";
                    String attr14 = "";
                    String attr15 = "";
                    String attr16 = "";
                    String attr161 = "";

                    if (File.ReadAllBytes(fil).Length > 0)
                    {

                        xDoc.Load(fil);

                        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xDoc.NameTable);
                        namespaceManager.AddNamespace("q", "http://zakupki.gov.ru/oos/types/1");
                        namespaceManager.AddNamespace("s", "http://zakupki.gov.ru/oos/common/1");
                        namespaceManager.AddNamespace("p", "http://zakupki.gov.ru/oos/EPtypes/1");
                        namespaceManager.AddNamespace("n", "http://zakupki.gov.ru/oos/base/1");

                        if (xDoc.SelectSingleNode("//q:id", namespaceManager) != null)
                        {
                            attr0 = xDoc.SelectSingleNode("//q:id", namespaceManager).InnerText;

                        }
                        if (xDoc.SelectSingleNode("//q:foundation/q:fcsOrder/q:order/q:notificationNumber", namespaceManager) != null)
                        {
                            attr = xDoc.SelectSingleNode("//q:foundation/q:fcsOrder/q:order/q:notificationNumber", namespaceManager).InnerText;

                        }
                        else if (xDoc.SelectSingleNode("//q:foundation/q:oosOrder/q:order/q:notificationNumber", namespaceManager) != null)
                        {
                            attr = xDoc.SelectSingleNode("//q:foundation/q:oosOrder/q:order/q:notificationNumber", namespaceManager).InnerText;

                        }
                        else if (xDoc.SelectSingleNode("//q:notificationNumber", namespaceManager) != null)
                        {
                            attr = xDoc.SelectSingleNode("//q:notificationNumber", namespaceManager).InnerText;

                        }
                        else attr = "-";

                        if (xDoc.SelectSingleNode("//q:regNum", namespaceManager) != null)
                        {
                            attr1 = xDoc.SelectSingleNode("//q:regNum", namespaceManager).InnerText;

                        }
                        else attr1 = "-";

                        if (xDoc.SelectSingleNode("//q:customer/q:fullName", namespaceManager) != null)
                        {
                            attr2 = xDoc.SelectSingleNode("//q:customer/q:fullName", namespaceManager).InnerText;

                        }
                        else attr2 = "-";

                        if (xDoc.SelectSingleNode("//q:customer/q:inn", namespaceManager) != null)
                        {
                            attr3 = xDoc.SelectSingleNode("//q:customer/q:inn", namespaceManager).InnerText;

                        }
                        else attr3 = "-";

                        if (xDoc.SelectSingleNode("//q:suppliers/q:supplier/q:legalEntityRF/q:fullName", namespaceManager) != null)
                        {
                            attr4 = xDoc.SelectSingleNode("//q:suppliers/q:supplier/q:legalEntityRF/q:fullName", namespaceManager).InnerText;

                        }
                        else if (xDoc.SelectSingleNode("//q:suppliers/q:supplier/q:individualPersonRF/q:lastName", namespaceManager) != null && xDoc.SelectSingleNode("//q:suppliers/q:supplier/q:individualPersonRF/q:firstName", namespaceManager) != null && xDoc.SelectSingleNode("//q:suppliers/q:supplier/q:individualPersonRF/q:middleName", namespaceManager) != null)
                        {
                            attr4 = xDoc.SelectSingleNode("//q:suppliers/q:supplier/q:individualPersonRF/q:lastName", namespaceManager).InnerText;
                            attr41 = xDoc.SelectSingleNode("//q:suppliers/q:supplier/q:individualPersonRF/q:firstName", namespaceManager).InnerText;
                            attr42 = xDoc.SelectSingleNode("//q:suppliers/q:supplier/q:individualPersonRF/q:middleName", namespaceManager).InnerText;
                            attr4 = "ИП" + " " + attr4 + " " + attr41 + " " + attr42;
                        }
                        else attr4 = "-";

                        if (xDoc.SelectSingleNode("//q:suppliers/q:supplier/q:legalEntityRF/q:INN", namespaceManager) != null)
                        {
                            attr5 = xDoc.SelectSingleNode("//q:suppliers/q:supplier/q:legalEntityRF/q:INN", namespaceManager).InnerText;

                        }
                        else if (xDoc.SelectSingleNode("//q:suppliers/q:supplier/q:individualPersonRF/q:INN", namespaceManager) != null)
                        {
                            attr5 = xDoc.SelectSingleNode("//q:suppliers/q:supplier/q:individualPersonRF/q:INN", namespaceManager).InnerText;

                        }
                        else attr5 = "-";

                        if (xDoc.SelectSingleNode("//q:protocolDate", namespaceManager) != null)
                        {
                            attr6 = xDoc.SelectSingleNode("//q:protocolDate", namespaceManager).InnerText;
                        }
                        else attr6 = "-";

                        if (xDoc.SelectSingleNode("//q:signDate", namespaceManager) != null)
                        {
                            attr7 = xDoc.SelectSingleNode("//q:signDate", namespaceManager).InnerText;

                        }
                        else attr7 = "-";

                        if (xDoc.SelectSingleNode("//q:number", namespaceManager) != null)
                        {
                            attr8 = xDoc.SelectSingleNode("//q:number", namespaceManager).InnerText;

                        }
                        else attr8 = "-";

                        if (xDoc.SelectSingleNode("//q:startDate", namespaceManager) != null)
                        {
                            attr9 = xDoc.SelectSingleNode("//q:startDate", namespaceManager).InnerText;

                        }
                        else attr9 = "-";

                        if (xDoc.SelectSingleNode("//q:endDate", namespaceManager) != null)
                        {
                            attr10 = xDoc.SelectSingleNode("//q:endDate", namespaceManager).InnerText;

                        }
                        else attr10 = "-";

                        if (xDoc.SelectSingleNode("//q:products/q:product/q:OKPD2/q:code", namespaceManager) != null)
                        {
                            attr11 = xDoc.SelectSingleNode("//q:products/q:product/q:OKPD2/q:code", namespaceManager).InnerText;

                        }
                        else if (xDoc.SelectSingleNode("//q:KTRU/q:code", namespaceManager) != null)
                        {
                            attr11 = xDoc.SelectSingleNode("//q:KTRU/q:code", namespaceManager).InnerText;

                        }
                        else attr11 = "-";

                        if (xDoc.SelectSingleNode("//q:price", namespaceManager) != null)
                        {
                            attr12 = xDoc.SelectSingleNode("//q:price", namespaceManager).InnerText;

                        }
                        else attr12 = "-";

                        if (xDoc.SelectSingleNode("//q:modification/q:errorCorrection/q:description", namespaceManager) != null)
                        {
                            attr13 = xDoc.SelectSingleNode("//q:modification/q:errorCorrection/q:description", namespaceManager).InnerText;

                        }
                        else if (xDoc.SelectSingleNode("//q:modification/q:contractChange/q:reason/q:name", namespaceManager) != null)
                        {
                            attr13 = xDoc.SelectSingleNode("//q:modification/q:contractChange/q:reason/q:name", namespaceManager).InnerText;

                        }
                        else attr13 = "-";

                        if (xDoc.SelectSingleNode("//q:foundation/q:fcsOrder/q:order/q:placing", namespaceManager) != null)
                        {
                            attr14 = xDoc.SelectSingleNode("//q:foundation/q:fcsOrder/q:order/q:placing", namespaceManager).InnerText;

                        }
                        else if (xDoc.SelectSingleNode("//q:foundation/q:oosOrder/q:order/q:placing", namespaceManager) != null)
                        {
                            attr14 = xDoc.SelectSingleNode("//q:foundation/q:oosOrder/q:order/q:placing", namespaceManager).InnerText;

                        }
                        else attr14 = "-";

                        if (xDoc.SelectSingleNode("//q:contractSubject", namespaceManager) != null)
                        {
                            attr15 = xDoc.SelectSingleNode("//q:contractSubject", namespaceManager).InnerText;

                        }
                        else attr15 = "-";

                        if (xDoc.SelectSingleNode("//q:currentContractStage", namespaceManager) != null)
                        {
                            attr16 = xDoc.SelectSingleNode("//q:currentContractStage", namespaceManager).InnerText;

                        }
                        else attr16 = "-";

                        if (xDoc.SelectSingleNode("//q:foundation/q:fcsOrder/q:order/q:singleCustomer/q:reason/q:name", namespaceManager) != null)
                        {
                            attr161 = xDoc.SelectSingleNode("//q:foundation/q:fcsOrder/q:order/q:singleCustomer/q:reason/q:name", namespaceManager).InnerText;

                        }
                        else if (xDoc.SelectSingleNode("//q:foundation/q:oosOrder/q:order/q:singleCustomer/q:reason/q:name", namespaceManager) != null)
                        {
                            attr161 = xDoc.SelectSingleNode("//q:foundation/q:oosOrder/q:order/q:singleCustomer/q:reason/q:name", namespaceManager).InnerText;

                        }
                        else attr161 = "-";

                        int m = 0;
                        SqlCommand SqlProv = new SqlCommand(@"SELECT COUNT(purchaseNumber) As CountTabNum FROM dbo.Contract WHERE [purchaseNumber]= '" + attr + "' ", Connect.cnn);
                        Connect.cnn.Open();
                        m = (Int32)(SqlProv.ExecuteScalar());
                        Connect.cnn.Close();

                        if (m == 0)
                        {

                            // подключение к БД
                            Connect.cnn.Open();
                            Connect44FZ.cnn.Open();
                            // внесение данных в БД
                            SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.Contract(contractID,purchaseNumber,contractRegNum,customerFullName,customerINN,supplierFullName,supplierINN,protocolDate,signDate,number,startDate,endDate,KTRUcode,price,modificationReasonName,currentContractStage,placing,contractSubject,contractExecution) VALUES (@attr0,@attr,@attr1,@attr2,@attr3,@attr4,@attr5,@attr6,@attr7,@attr8,@attr9,@attr10,@attr11,@attr12,@attr13,@attr16,@attr14,@attr15,@attr161) 
                                                                             ", Connect.cnn);
                            SqlCommand cmd44 = new SqlCommand(@"INSERT INTO dbo.[44_Contract](purchaseNumber,signDate) VALUES (@attr,@attr7)", Connect44FZ.cnn);

                            cmd.Parameters.AddWithValue("@attr0", attr0);
                            cmd.Parameters.AddWithValue("@attr", attr);
                            cmd.Parameters.AddWithValue("@attr1", attr1);
                            cmd.Parameters.AddWithValue("@attr2", attr2);
                            cmd.Parameters.AddWithValue("@attr3", attr3);
                            cmd.Parameters.AddWithValue("@attr4", attr4);
                            cmd.Parameters.AddWithValue("@attr5", attr5);
                            cmd.Parameters.AddWithValue("@attr6", attr6);
                            cmd.Parameters.AddWithValue("@attr7", attr7);
                            cmd.Parameters.AddWithValue("@attr8", attr8);
                            cmd.Parameters.AddWithValue("@attr9", attr9);
                            cmd.Parameters.AddWithValue("@attr10", attr10);
                            cmd.Parameters.AddWithValue("@attr11", attr11);
                            cmd.Parameters.AddWithValue("@attr12", attr12);
                            cmd.Parameters.AddWithValue("@attr13", attr13);
                            cmd.Parameters.AddWithValue("@attr14", attr14);
                            cmd.Parameters.AddWithValue("@attr15", attr15);
                            cmd.Parameters.AddWithValue("@attr16", attr16);
                            cmd.Parameters.AddWithValue("@attr161", attr161);
                            cmd.ExecuteNonQuery();

                            cmd44.Parameters.AddWithValue("@attr", attr);
                            cmd44.Parameters.AddWithValue("@attr7", attr7);
                            cmd44.ExecuteNonQuery();

                            //добавляем новую запись в таблицу

                            Connect.cnn.Close();
                            Connect44FZ.cnn.Close();

                            //Загрузка в ГосЗакупки

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
