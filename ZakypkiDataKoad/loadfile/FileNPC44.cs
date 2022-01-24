using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Windows;
using GosZakypkiDataLoad.connection;
using GosZakypkiDataLoad.server;
using GosZakypkiDataLoad.parser;
using static GosZakypkiDataLoad.server.Directories;

namespace GosZakypkiDataLoad.loadfile
{
    public class FileNPC44
    {
        public static void NPC44()
        {
            FTP ftp = new FTP("free", "free");
            int pap;
            int index;

            // Создаем папки если их нет для загрузки архивов
            for (pap = 0; pap <= papki.GetUpperBound(0); pap++)
            {
                try
                {

                    if (Directory.Exists(@"D:\Users\ODP-ChudinovEM\" + papki[pap]))
                    {
                        Directory.Delete(@"D:\Users\ODP-ChudinovEM\" + papki[pap], true);
                        Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\" + papki[pap]);
                        Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\" + papki[pap] + "извлечено/");
                    }
                    else
                    {
                        Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\" + papki[pap]);
                        Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\" + papki[pap] + "извлечено/");
                    }

                }

                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            // Проверка актуальности и загрузка архивов
            for (index = 0; index <= spisok_reg.GetUpperBound(0); index++)
            {
                try
                {
                    SqlCommand SqlProv;
                    for (pap = 0; pap <= papki.GetUpperBound(0); pap++)
                    {
                        string S = "";
                        int n;
                        // проверяются загружены ли новые архивы за последние n дней
                        for (n = -int.Parse(period); n <= 0; n++)
                        {

                            var nD = DateTime.Today.AddDays(n);
                            S = nD.ToString("yyyyMMdd");
                            /// MessageBox.Show(S);
                            List<string> regions = ftp.GetDirectory("ftp://ftp.zakupki.gov.ru/fcs_regions/" + spisok_reg[index] + papki[pap]);

                            foreach (string filename in regions)
                            {
                                if (filename.Contains(S))
                                {

                                    int k = 0;
                                    SqlCommand SqlCom;
                                    SqlProv = new SqlCommand("SELECT COUNT(Name) As CountTabNum FROM dbo.Files WHERE [Name]='" + filename + "'", Connect44FZ.cnn);
                                    Connect44FZ.cnn.Open();
                                    k = (Int32)SqlProv.ExecuteScalar();
                                    Connect44FZ.cnn.Close();
                                    if (k == 0)
                                    {
                                        SqlCom = new SqlCommand("INSERT INTO [Files] ([Name], [Date]) VALUES ('" + filename + "','" + DateTime.Now.ToString("dd.MM.yyyy") + " " + DateTime.Now.ToString("HH:mm:ss") + "')", Connect44FZ.cnn);
                                        Connect44FZ.cnn.Open();
                                        SqlCom.ExecuteNonQuery();
                                        Connect44FZ.cnn.Close();

                                        WebClient wc = new WebClient
                                        {
                                            BaseAddress = @"ftp://ftp.zakupki.gov.ru/fcs_regions/",
                                            Credentials = new NetworkCredential("free", "free")
                                        };
                                        //wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                                        //wc.DownloadFileCompleted += new AsyncCompletedEventHandler(FileCompleted);
                                        wc.DownloadFile(new Uri(@"ftp://ftp.zakupki.gov.ru/fcs_regions/" + spisok_reg[index] + papki[pap] + filename), @"D:\Users\ODP-ChudinovEM\" + papki[pap] + filename);

                                        // string[] files = Directory.GetFiles(@"D:\Чудинов\" + papki[pap] + "извлечено/", "contract_*.xml");
                                        //  Parser(files);
                                        if (File.ReadAllBytes(@"D:\Users\ODP-ChudinovEM\" + papki[pap] + filename).Length > 22L)
                                        {

                                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                                            p.StartInfo.FileName = @"C:\Program Files\WinRAR\WinRAR.exe";
                                            p.StartInfo.Arguments = string.Format("x -o+ \"{0}\" \"{1}\"", @"D:\Users\ODP-ChudinovEM\" + papki[pap] + filename, @"D:\Users\ODP-ChudinovEM\" + papki[pap] + "извлечено/");
                                            p.EnableRaisingEvents = true;
                                            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                            p.Start();

                                        }

                                    }
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
            // Отбор по критерию и загрузка контрактов
            for (pap = 0; pap <= 1; pap++)
            {
                string[] files = Directory.GetFiles(@"D:\Users\ODP-ChudinovEM\" + papki[pap] + "извлечено/", "contract_*.xml");
                ParserContarct.Contarct(files);
            }
            //Загрузка извещений в БД
            for (pap = 2; pap <= 3; pap++)
            {
                string[] files = Directory.GetFiles(@"D:\Users\ODP-ChudinovEM\" + papki[pap] + "извлечено/", "*.xml");
               ParserNotification.Notification(files);
            }
            //Загрузка протоколов в БД
            for (pap = 4; pap <= 5; pap++)
            {
                string[] files = Directory.GetFiles(@"D:\Users\ODP-ChudinovEM\" + papki[pap] + "извлечено / ", "*.xml");
                ParserProtocol.Protocol(files);
            }
           
        }
    }
}
