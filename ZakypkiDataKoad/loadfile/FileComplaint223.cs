using System;
using System.Collections.Generic;
using GosZakypkiDataLoad.connection;
using GosZakypkiDataLoad.server;
using GosZakypkiDataLoad.parser;
using static GosZakypkiDataLoad.server.Directories;
using System.IO;
using System.Data.SqlClient;
using System.Net;

namespace GosZakypkiDataLoad.loadfile
{
    public class FileComplaint223
    {
        public static void FZC223()
        {
            FTP ftp = new FTP("fz223free", "fz223free");
            try
            {

                if (Directory.Exists(@"D:\Users\ODP-ChudinovEM\complaint_223"))
                {
                    Directory.Delete(@"D:\Users\ODP-ChudinovEM\complaint_223", true);
                    Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\complaint_223");
                    Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\complaint_223\извлечено\");
                }
                else
                {
                    Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\complaint_223");
                    Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\complaint_223\извлечено\");
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
                        if (!reg.Contains("fz223in_published") && !reg.Contains("list_catalog") && !reg.Contains("list_regions") && !reg.Contains("mkdir_list") && !reg.Contains("archive") && !reg.Contains("ast"))
                        {
                            List<string> compalint = ftp.GetDirectory("ftp://ftp.zakupki.gov.ru/out/published/" + reg + "/complaint/daily/");

                            foreach (string filename in compalint)
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
                                        wc.DownloadFile(new Uri(@"ftp://ftp.zakupki.gov.ru/out/published/" + reg + "/complaint/daily/" + filename), @"D:\Users\ODP-ChudinovEM\complaint_223\" + filename);

                                        if (File.ReadAllBytes(@"D:\Users\ODP-ChudinovEM\complaint_223\" + filename).Length > 22L)
                                        {

                                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                                            p.StartInfo.FileName = @"C:\Program Files\WinRAR\WinRAR.exe";
                                            p.StartInfo.Arguments = string.Format("x -o+ \"{0}\" \"{1}\"", @"D:\Users\ODP-ChudinovEM\complaint_223\" + filename, @"D:\Users\ODP-ChudinovEM\complaint_223\извлечено\");
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
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            string[] files = Directory.GetFiles(@"D:\Users\ODP-ChudinovEM\complaint_223\извлечено\", "*.xml");
            ParserComplaint223.Complaint223(files);
        }
    }
}
