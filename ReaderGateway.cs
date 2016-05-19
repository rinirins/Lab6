using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using DZ.Properties;


namespace DZ.Properties
{
    public class ReaderGateway : Gateway
    {
        public string name;
        public string email;
        public string login;
        public string password;
        public string id;

        public SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\DZ\DZ\App_Data\dz.mdf;Integrated Security=True");
        
        public override void OpenConnection()
        {
            cnn.Open();
        }

        public override void CloseConnection()
        {
            cnn.Close();
        }

        public void LAB ()
	{
		TextBox1.Text = "This is test message for lab 6!";
	}

    public void LAB1 ()
	{
		TextBox1.Text = "This is NEW BRANCH!";
	}


        public override void Insert()
        {
            string sqlInsert = "INSERT INTO Reader (Name, Email, login, Password) VALUES (@name,@email,@login,@password)";
            using (SqlCommand cmd = new SqlCommand(sqlInsert, cnn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.ExecuteNonQuery();
            }
        }

        public override void Update()
        {
            int i = 0;
        }


      
    }


    public class ReaderGatewayFind: ReaderGateway
    {

        public string FindReader(string log)
        {
            string sqlFind = "SELECT IdR FROM Reader WHERE login = @log";
            using (SqlCommand cmd = new SqlCommand(sqlFind, cnn))
            {
                cmd.Parameters.AddWithValue("@log", log);
                id = Convert.ToString(cmd.ExecuteScalar());
            }
            return id;
        }



        public ReaderGateway FindNameReader(string userid)
        {

            ReaderGateway readers = new ReaderGateway();
            string sqlFind = "SELECT * FROM Reader WHERE IdR=@IdR";
            using (SqlCommand cmd = new SqlCommand(sqlFind, cnn))
            {

                cmd.Parameters.AddWithValue("@IdR", userid);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    readers.id = reader.GetValue(0).ToString();
                    readers.name = reader.GetValue(1).ToString();
                    readers.email = reader.GetValue(2).ToString();
                    readers.login = reader.GetValue(3).ToString();
                    readers.password = reader.GetValue(4).ToString();
                }
                reader.Close();

            }
            return readers;

        }

    }
        

}

