using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using MySql.Data;
using System.Threading;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Net;

namespace spider_stoki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] portnames = SerialPort.GetPortNames();
            SerialPort port = new SerialPort("COM1", 2400, Parity.None, 8, StopBits.One);

            byte[] data1 = { 0x21 }; // Запрос данных о приборе
            byte[] data2 = { 0x1F }; // Запрос текущих значений

            byte[] data3 = { 0x55 }; // Запрос текущих значений
            byte[] data4 = { 0x1D }; // Запрос архива

            port.Open();

            /* port.Write(data1, 0, data1.Length);
             Thread.Sleep(1000);

             int byteRecieved = port.BytesToRead;
             byte[] messByte = new byte[byteRecieved];
             port.Read(messByte, 0, byteRecieved);
             Thread.Sleep(1000);

             port.Write(data3, 0, data3.Length);
             Thread.Sleep(1000);

             int byteRecieved2 = port.BytesToRead;
             byte[] messByte2 = new byte[byteRecieved2];
             port.Read(messByte2, 0, byteRecieved2);
             Thread.Sleep(1000);*/

            port.Write(data4, 0, data4.Length);
            Thread.Sleep(5000);

            int byteRecieved4 = port.BytesToRead;
            byte[] messByte4 = new byte[byteRecieved4];
            port.Read(messByte4, 0, byteRecieved4);
            Thread.Sleep(1000);



            port.Close();

            string date_mysql = "";
            string date1 = "";
            string date2 = "";
            string time1 = "";
            string time2 = "";

            string pok1 = "";
            string pok2 = "";
            string pok3 = "";
            string pok4 = "";
            string pok_str = "";
            float pok = 0;

            DateTime date11 = DateTime.Now;
            DateTime date22 = DateTime.Now;
            

            for (int k = 0; k < 56; k++)
            {

                date1 = messByte4[503+ k * 8].ToString("X");
                if (date1.Length < 2)
                {
                    date1 = "0" + date1;
                }

                date2 = messByte4[502 + k * 8].ToString("X");
                if (date2.Length < 2)
                {
                    date2 = "0" + date2;
                }

                time1 = messByte4[501 + k * 8].ToString("X");
                if (time1.Length < 2)
                {
                    time1 = "0" + time1;
                }

                time2 = messByte4[500 + k * 8].ToString("X");
                if (time2.Length < 2)
                {
                    time2 = "0" + time2;
                }

                pok1 = messByte4[499 + k * 8].ToString("X");
                if (pok1.Length < 2)
                {
                    pok1 = "0" + pok1;
                }

                pok2 = messByte4[498 + k * 8].ToString("X");
                if (pok2.Length < 2)
                {
                    pok2 = "0" + pok2;
                }

                pok3 = messByte4[497 + k * 8].ToString("X");
                if (pok3.Length < 2)
                {
                    pok3 = "0" + pok3;
                }

                pok4 = messByte4[496 + k * 8].ToString("X");
                if (pok4.Length < 2)
                {
                    pok4 = "0" + pok4;
                }

                pok_str = pok1 + pok2 + pok3 + pok4;

                pok = Convert.ToInt32(pok_str);
                pok = pok / 100;

                char[] chars = pok.ToString("R").ToCharArray();
                for (int n = 0; n < pok.ToString("R").Length; n++)
                {
                    if (chars[n] == ',')
                    {
                        chars[n] = '.';
                    }
                }
                string pok_mysql = new string(chars);

                
               
                date22 = new DateTime(Convert.ToInt32(date11.Year), Convert.ToInt32(date1), Convert.ToInt32(date2), Convert.ToInt32(time1), Convert.ToInt32(time2), 0);
                if (date22 > date11)
                {
                    date11.AddYears(-1);
                }

                date_mysql = date11.Year + date1 + date2 + time1 + time2+"00";

                string conn_str = "Database=resources;Data Source=10.1.1.50;User Id=user;Password=password";
                MySqlLib.MySqlData.MySqlExecute.MyResult result = new MySqlLib.MySqlData.MySqlExecute.MyResult();

                result = MySqlLib.MySqlData.MySqlExecute.SqlNoneQuery("INSERT INTO stoki (`stoki_date`,`stoki`) VALUES (" + "'" + date_mysql + "'" + "," + "'" + pok_mysql + "'" + ")", conn_str);
                textBox1.Text = pok_mysql;
                textBox2.Text = Convert.ToString(pok);


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

          

            string[] portnames = SerialPort.GetPortNames();
            SerialPort port = new SerialPort("COM1", 2400, Parity.None, 8, StopBits.One);

            byte[] data1 = { 0x21 }; // Запрос данных о приборе
            byte[] data2 = { 0x1F }; // Запрос текущих значений

            byte[] data3 = { 0x55 }; // Запрос текущих значений
            byte[] data4 = { 0x1D }; // Запрос архива

            port.Open();

            port.Write(data4, 0, data4.Length);
            Thread.Sleep(5000);

            int byteRecieved4 = port.BytesToRead;
            byte[] messByte4 = new byte[byteRecieved4];
            port.Read(messByte4, 0, byteRecieved4);
            Thread.Sleep(1000);



            port.Close();

            string date_mysql = "";
            string date1 = "";
            string date2 = "";
            string time1 = "";
            string time2 = "";

            string pok1 = "";
            string pok2 = "";
            string pok3 = "";
            string pok4 = "";
            string pok_str = "";
            float pok = 0;

            DateTime date11 = DateTime.Now;
            DateTime date22 = DateTime.Now;


            for (int k = 0; k < 56; k++)
            {

                date1 = messByte4[503 + k * 8].ToString("X");
                if (date1.Length < 2)
                {
                    date1 = "0" + date1;
                }

                date2 = messByte4[502 + k * 8].ToString("X");
                if (date2.Length < 2)
                {
                    date2 = "0" + date2;
                }

                time1 = messByte4[501 + k * 8].ToString("X");
                if (time1.Length < 2)
                {
                    time1 = "0" + time1;
                }

                time2 = messByte4[500 + k * 8].ToString("X");
                if (time2.Length < 2)
                {
                    time2 = "0" + time2;
                }

                pok1 = messByte4[499 + k * 8].ToString("X");
                if (pok1.Length < 2)
                {
                    pok1 = "0" + pok1;
                }

                pok2 = messByte4[498 + k * 8].ToString("X");
                if (pok2.Length < 2)
                {
                    pok2 = "0" + pok2;
                }

                pok3 = messByte4[497 + k * 8].ToString("X");
                if (pok3.Length < 2)
                {
                    pok3 = "0" + pok3;
                }

                pok4 = messByte4[496 + k * 8].ToString("X");
                if (pok4.Length < 2)
                {
                    pok4 = "0" + pok4;
                }

                pok_str = pok1 + pok2 + pok3 + pok4;

                pok = Convert.ToInt32(pok_str);
                pok = pok / 100;

                char[] chars = pok.ToString("R").ToCharArray();
                for (int n = 0; n < pok.ToString("R").Length; n++)
                {
                    if (chars[n] == ',')
                    {
                        chars[n] = '.';
                    }
                }
                string pok_mysql = new string(chars);



                date22 = new DateTime(Convert.ToInt32(date11.Year), Convert.ToInt32(date1), Convert.ToInt32(date2), Convert.ToInt32(time1), Convert.ToInt32(time2), 0);
                if (date22 > date11)
                {
                    date11.AddYears(-1);
                }

                date_mysql = date11.Year + date1 + date2 + time1 + time2 + "00";

                string conn_str = "Database=resources;Data Source=10.1.1.50;User Id=user;Password=password";
                MySqlLib.MySqlData.MySqlExecute.MyResult result = new MySqlLib.MySqlData.MySqlExecute.MyResult();

                result = MySqlLib.MySqlData.MySqlExecute.SqlNoneQuery("INSERT INTO stoki (`stoki_date`,`stoki`) VALUES (" + "'" + date_mysql + "'" + "," + "'" + pok_mysql + "'" + ")", conn_str);
                textBox1.Text = pok_mysql;
                textBox2.Text = Convert.ToString(pok);

                Application.Exit();

            }

        }
    }
}


namespace MySqlLib
{
    /// <summary>
    /// Набор компонент для простой работы с MySQL базой данных.
    /// </summary>
    public class MySqlData
    {

        /// <summary>
        /// Методы реализующие выполнение запросов с возвращением одного параметра либо без параметров вовсе.
        /// </summary>
        public class MySqlExecute
        {

            /// <summary>
            /// Возвращаемый набор данных.
            /// </summary>
            public class MyResult
            {
                /// <summary>
                /// Возвращает результат запроса.
                /// </summary>
                public string ResultText;
                /// <summary>
                /// Возвращает True - если произошла ошибка.
                /// </summary>
                public string ErrorText;
                /// <summary>
                /// Возвращает текст ошибки.
                /// </summary>
                public bool HasError;
            }

            /// <summary>
            /// Для выполнения запросов к MySQL с возвращением 1 параметра.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает значение при успешном выполнении запроса, текст ошибки - при ошибке.</returns>
            public static MyResult SqlScalar(string sql, string connection)
            {
                MyResult result = new MyResult();
                try
                {
                    MySql.Data.MySqlClient.MySqlConnection connRC = new MySql.Data.MySqlClient.MySqlConnection(connection);
                    MySql.Data.MySqlClient.MySqlCommand commRC = new MySql.Data.MySqlClient.MySqlCommand(sql, connRC);
                    connRC.Open();
                    try
                    {
                        result.ResultText = commRC.ExecuteScalar().ToString();
                        result.HasError = false;
                    }
                    catch (Exception ex)
                    {
                        result.ErrorText = ex.Message;
                        result.HasError = true;

                    }
                    connRC.Close();
                }
                catch (Exception ex)//Этот эксепшн на случай отсутствия соединения с сервером.
                {
                    result.ErrorText = ex.Message;
                    result.HasError = true;
                }
                return result;
            }


            /// <summary>
            /// Для выполнения запросов к MySQL без возвращения параметров.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает True - ошибка или False - выполнено успешно.</returns>
            public static MyResult SqlNoneQuery(string sql, string connection)
            {
                MyResult result = new MyResult();
                try
                {
                    MySql.Data.MySqlClient.MySqlConnection connRC = new MySql.Data.MySqlClient.MySqlConnection(connection);
                    MySql.Data.MySqlClient.MySqlCommand commRC = new MySql.Data.MySqlClient.MySqlCommand(sql, connRC);
                    connRC.Open();
                    try
                    {
                        commRC.ExecuteNonQuery();
                        result.HasError = false;
                    }
                    catch (Exception ex)
                    {
                        result.ErrorText = ex.Message;
                        result.HasError = true;
                    }
                    connRC.Close();
                }
                catch (Exception ex)//Этот эксепшн на случай отсутствия соединения с сервером.
                {
                    result.ErrorText = ex.Message;
                    result.HasError = true;
                }
                return result;
            }

        }
        /// <summary>
        /// Методы реализующие выполнение запросов с возвращением набора данных.
        /// </summary>
        public class MySqlExecuteData
        {
            /// <summary>
            /// Возвращаемый набор данных.
            /// </summary>
            public class MyResultData
            {
                /// <summary>
                /// Возвращает результат запроса.
                /// </summary>
                public DataTable ResultData;
                /// <summary>
                /// Возвращает True - если произошла ошибка.
                /// </summary>
                public string ErrorText;
                /// <summary>
                /// Возвращает текст ошибки.
                /// </summary>
                public bool HasError;
            }
            /// <summary>
            /// Выполняет запрос выборки набора строк.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает набор строк в DataSet.</returns>
            public static MyResultData SqlReturnDataset(string sql, string connection)
            {
                MyResultData result = new MyResultData();
                try
                {
                    MySql.Data.MySqlClient.MySqlConnection connRC = new MySql.Data.MySqlClient.MySqlConnection(connection);
                    MySql.Data.MySqlClient.MySqlCommand commRC = new MySql.Data.MySqlClient.MySqlCommand(sql, connRC);
                    connRC.Open();
                    try
                    {
                        MySql.Data.MySqlClient.MySqlDataAdapter AdapterP = new MySql.Data.MySqlClient.MySqlDataAdapter();
                        AdapterP.SelectCommand = commRC;
                        DataSet ds1 = new DataSet();
                        AdapterP.Fill(ds1);
                        result.ResultData = ds1.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        result.HasError = true;
                        result.ErrorText = ex.Message;
                    }
                    connRC.Close();
                }
                catch (Exception ex)//Этот эксепшн на случай отсутствия соединения с сервером.
                {
                    result.ErrorText = ex.Message;
                    result.HasError = true;
                }
                return result;
            }
        }
    }
}