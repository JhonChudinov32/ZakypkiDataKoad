using System;
using System.Collections.Generic;
using System.Net;

namespace GosZakypkiDataLoad.server
{
    public class FTP
    {
        private System.Net.NetworkCredential _credentials;
        public FTP(string _FTPUser, string _FTPPass)
        {
            SetCredentials(_FTPUser, _FTPPass);
        }
        public List<string> GetDirectory(string ftpPath)
        {
            List<string> ret = new List<string>();
            try
            {
                FtpWebRequest _request = (FtpWebRequest)WebRequest.Create(ftpPath);
                _request.KeepAlive = false;
                _request.Method = System.Net.WebRequestMethods.Ftp.ListDirectory;
                _request.Credentials = _credentials;
                //  _request.UsePassive = true;

                FtpWebResponse _response = (FtpWebResponse)_request.GetResponse();
                System.IO.Stream responseStream = _response.GetResponseStream();
                System.IO.StreamReader _reader = new System.IO.StreamReader(responseStream);
                string FileData = _reader.ReadToEnd();
                string[] Lines = FileData.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string l in Lines)
                    ret.Add(l);
                _reader.Close();
                _response.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                FtpWebRequest _request = (FtpWebRequest)WebRequest.Create(ftpPath);
                _request.KeepAlive = false;
                _request.Method = System.Net.WebRequestMethods.Ftp.ListDirectory;
                _request.Credentials = _credentials;
                //  _request.UsePassive = true;

                FtpWebResponse _response = (FtpWebResponse)_request.GetResponse();
                System.IO.Stream responseStream = _response.GetResponseStream();
                System.IO.StreamReader _reader = new System.IO.StreamReader(responseStream);
                string FileData = _reader.ReadToEnd();
                string[] Lines = FileData.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string l in Lines)
                    ret.Add(l);
                _reader.Close();
                _response.Close();
            }
            return ret;
        }
        private void SetCredentials(string _FTPUser, string _FTPPass)
        {
            _credentials = new System.Net.NetworkCredential(_FTPUser, _FTPPass);
        }
    }
}
