using System;
using System.Data.SqlClient;

namespace GosZakypkiDataLoad.connection
{
    public class Connect44FZ : IDisposable
    {
        public static SqlConnection cnn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\User\GosZakypki.mdf;Integrated Security = True; Connect Timeout = 30");

        public void Dispose()
        {
            if (cnn != null)
            {
                cnn.Dispose();
            }
        }
    }
}
