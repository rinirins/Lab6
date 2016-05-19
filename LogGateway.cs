using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using DZ.Properties;

namespace DZ.Properties
{
    public class LogGateway : Gateway
    {
        public string IdR;
        public string Number;
        public string Summa;
        public string Ostatok;
        public string Date;
        public string Status;  // 1 - пришли, 2 - сняли


        public SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\DZ\DZ\App_Data\dz.mdf;Integrated Security=True");

        public override void OpenConnection()
        {
            cnn.Open();
        }

        public override void CloseConnection()
        {
            cnn.Close();
        }



        public override void Insert()
        {
            string sqlInsert = "INSERT INTO log (IdR, Number, Summa,  Ostatok,  Date, Status) VALUES (@IdR, @Number, @Summa, @Ostatok, @Date, @Status)";
            using (SqlCommand cmd = new SqlCommand(sqlInsert, cnn))
            {
                cmd.Parameters.AddWithValue("@IdR", IdR);
                cmd.Parameters.AddWithValue("@Number", Number);
                cmd.Parameters.AddWithValue("@Summa", Summa);
                cmd.Parameters.AddWithValue("@Ostatok", Ostatok);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.ExecuteNonQuery();
            }
        }

        public override void Update()
        {
            int i = 0;
        }

    }


   public class LogGatewayFind: LogGateway
    {
        public List<LogGateway> FindPrihod(string userid)
        {

            List<LogGateway> logs = new List<LogGateway>();
            string sqlFind = "SELECT * FROM Log WHERE IdR=@IdR AND Status=1";

            using (SqlCommand cmd = new SqlCommand(sqlFind,cnn))
            {
                cmd.Parameters.AddWithValue("@IdR", userid);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LogGateway log = new LogGateway();
                    log.IdR = reader.GetValue(1).ToString();
                    log.Number = reader.GetValue(2).ToString();
                    log.Summa = reader.GetValue(3).ToString();
                    log.Ostatok = reader.GetValue(4).ToString();
                    log.Date = reader.GetValue(5).ToString();
                    log.Status = reader.GetValue(6).ToString();
                    logs.Add(log);
                }
                reader.Close();
            }
            return logs;
        }

        public List<LogGateway> FindRashod(string userid)
        {

            List<LogGateway> logs = new List<LogGateway>();
            string sqlFind = "SELECT * FROM Log WHERE IdR=@IdR AND Status=2";

            using (SqlCommand cmd = new SqlCommand(sqlFind, cnn))
            {
                cmd.Parameters.AddWithValue("@IdR", userid);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LogGateway log = new LogGateway();
                    log.IdR = reader.GetValue(1).ToString();
                    log.Number = reader.GetValue(2).ToString();
                    log.Summa = reader.GetValue(3).ToString();
                    log.Ostatok = reader.GetValue(4).ToString();
                    log.Date = reader.GetValue(5).ToString();
                    log.Status = reader.GetValue(6).ToString();
                    logs.Add(log);
                }
                reader.Close();
            }
            return logs;
        }

    }
}

      
