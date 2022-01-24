using System;
using System.Collections.Generic;
using GosZakypkiDataLoad.connection;
using GosZakypkiDataLoad.server;
using GosZakypkiDataLoad.parser;
using static GosZakypkiDataLoad.server.Directories;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Windows;

namespace GosZakypkiDataLoad.loadfile
{
    public class FileProtocol223
    {
        public static void FZ223()
        {
            FTP ftp = new FTP("fz223free", "fz223free");
            try
            {

                if (Directory.Exists(@"D:\Users\ODP-ChudinovEM\protocol"))
                {
                    Directory.Delete(@"D:\Users\ODP-ChudinovEM\protocol", true);
                    Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\protocol");
                    Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\protocol\извлечено\");
                }
                else
                {
                    Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\protocol");
                    Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\protocol\извлечено\");
                }

            }

            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            try
            {
                SqlCommand SqlProv;
                string S = "";
                int n;
                // проверяются загружены ли новые архивы за последние n дней
                for (n = -int.Parse(period); n <= 0; n++)
                {

                    var nD = DateTime.Today.AddDays(n);
                    S = nD.ToString("yyyyMMdd");
                    /// MessageBox.Show(S);
                    List<string> regions = ftp.GetDirectory("ftp://ftp.zakupki.gov.ru/out/published/");

                    foreach (string reg in regions)
                    {
                        if (!reg.Contains("fz223in_published") && !reg.Contains("list_catalog") && !reg.Contains("list_regions") && !reg.Contains("mkdir_list"))
                        {
                            List<string> region = ftp.GetDirectory("ftp://ftp.zakupki.gov.ru/out/published/" + reg + "/");
                            foreach (string protocol in region)
                            {
                                if (protocol.Contains("purchaseProtocol"))
                                {
                                    List<string> directory = ftp.GetDirectory("ftp://ftp.zakupki.gov.ru/out/published/" + reg + "/" + protocol + "/daily/");
                                    foreach (string filename in directory)
                                    {
                                        if (filename.Contains(S))
                                        {
                                            int k = 0;
                                            SqlCommand SqlCom;
                                            SqlProv = new SqlCommand("SELECT COUNT(Name) As CountTabNum FROM dbo.Files WHERE [Name]='" + filename + "'", Connect223FZ.cnn);
                                            Connect223FZ.cnn.Open();
                                            k = (Int32)SqlProv.ExecuteScalar();
                                            Connect223FZ.cnn.Close();

                                            if (k == 0)
                                            {
                                                SqlCom = new SqlCommand("INSERT INTO [Files] ([Name], [Date]) VALUES ('" + filename + "','" + DateTime.Now.ToString("dd.MM.yyyy") + " " + DateTime.Now.ToString("HH:mm:ss") + "')", Connect223FZ.cnn);
                                                Connect223FZ.cnn.Open();
                                                SqlCom.ExecuteNonQuery();
                                                Connect223FZ.cnn.Close();

                                                WebClient wc = new WebClient
                                                {
                                                    BaseAddress = @"ftp://ftp.zakupki.gov.ru/out/published/",
                                                    Credentials = new NetworkCredential("fz223free", "fz223free")
                                                };
                                                wc.DownloadFile(new Uri(@"ftp://ftp.zakupki.gov.ru/out/published/" + reg + "/" + protocol + "/daily/" + filename), @"D:\Users\ODP-ChudinovEM\protocol\" + filename);
                                                //  wc.DownloadFile(new Uri(@"ftp://ftp.zakupki.gov.ru/out/published/" + spisok_reg + "/purchaseProtocol/daily/" + filename), @"D:\Users\ODP-ChudinovEM\protocol\" + filename);
                                                if (File.ReadAllBytes(@"D:\Users\ODP-ChudinovEM\protocol\" + filename).Length > 22L)
                                                {

                                                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                                                    p.StartInfo.FileName = @"C:\Program Files\WinRAR\WinRAR.exe";
                                                    p.StartInfo.Arguments = string.Format("x -o+ \"{0}\" \"{1}\"", @"D:\Users\ODP-ChudinovEM\protocol\" + filename, @"D:\Users\ODP-ChudinovEM\protocol\извлечено\");
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
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            string[] files = Directory.GetFiles(@"D:\Users\ODP-ChudinovEM\protocol\извлечено\", "*.xml");
            ParserProtocol223.Protocol223(files);
        }
    }
}
