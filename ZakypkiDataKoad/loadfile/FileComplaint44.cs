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
    public class FileComplaint44
    {
        public static void Complaint44()
        {
            FTP ftp = new FTP("free", "free");
            try
            {

                if (Directory.Exists(@"D:\Users\ODP-ChudinovEM\complaint"))
                {
                    Directory.Delete(@"D:\Users\ODP-ChudinovEM\complaint", true);
                    Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\complaint");
                    Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\complaint\извлечено\");
                }
                else
                {
                    Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\complaint");
                    Directory.CreateDirectory(@"D:\Users\ODP-ChudinovEM\complaint\извлечено\");
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
                for (n = -int.Parse(period); n <= 0; n++)
                {

                    var nD = DateTime.Today.AddDays(n);
                    S = nD.ToString("yyyyMMdd");
                    /// MessageBox.Show(S);
                    List<string> complaint = ftp.GetDirectory("ftp://ftp.zakupki.gov.ru/fcs_fas/complaint/currMonth/");

                    foreach (string filename in complaint)
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
                                    BaseAddress = @"ftp://ftp.zakupki.gov.ru/fcs_fas/complaint/currMonth/",
                                    Credentials = new NetworkCredential("free", "free")
                                };
                                //wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                                //wc.DownloadFileCompleted += new AsyncCompletedEventHandler(FileCompleted);
                                wc.DownloadFile(new Uri(@"ftp://ftp.zakupki.gov.ru/fcs_fas/complaint/currMonth/" + filename), @"D:\Users\ODP-ChudinovEM\complaint\" + filename);

                                // string[] files = Directory.GetFiles(@"D:\Чудинов\" + papki[pap] + "извлечено/", "contract_*.xml");
                                //  Parser(files);
                                if (File.ReadAllBytes(@"D:\Users\ODP-ChudinovEM\complaint\" + filename).Length > 22L)
                                {

                                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                                    p.StartInfo.FileName = @"C:\Program Files\WinRAR\WinRAR.exe";
                                    p.StartInfo.Arguments = string.Format("x -o+ \"{0}\" \"{1}\"", @"D:\Users\ODP-ChudinovEM\complaint\" + filename, @"D:\Users\ODP-ChudinovEM\complaint\извлечено\");
                                    p.EnableRaisingEvents = true;
                                    p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                    p.Start();

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
            string[] files = Directory.GetFiles(@"D:\Users\ODP-ChudinovEM\complaint\извлечено\", "complaint_*.xml");
            ParserComplaint.Complaint(files);
        }
    }
}
