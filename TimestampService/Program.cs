using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimestampService
{
    class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;
            int currsec = dateTime.Second;
            string currTableName = String.Format("data_{0}_{1}_{2}_{3}_{4}_{5}",dateTime.Year, dateTime.Month.ToString().PadLeft(2, '0'), dateTime.Day.ToString().PadLeft(2, '0'), dateTime.Hour.ToString().PadLeft(2, '0'), dateTime.Minute.ToString().PadLeft(2, '0'), dateTime.Second.ToString().PadLeft(2,'0'));
            SqlCommand sqlCommand = new SqlCommand(String.Format("CREATE TABLE[dbo].[{0}]([timestamp][datetime2](7) NULL,[text] [char](10) NULL) ON[PRIMARY]",currTableName), new SqlConnection("Data Source=DAVIDOMEN;Initial Catalog=homework;Integrated Security=True"));
            sqlCommand.Connection.Open();
            for(int i = 0;i<10;i++)
            {
                sqlCommand.ExecuteNonQuery();
                while (currsec == DateTime.Now.Second)
                {
                    //генерируем инсёрты
                    //Console.WriteLine(currsec);
                    sqlCommand.CommandText = String.Format("INSERT INTO [dbo].[{2}]  ([timestamp],[text]) VALUES('{0}','{1}')", dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), RandomString(7),currTableName);
                    sqlCommand.ExecuteNonQuery();
                }
                
                dateTime = DateTime.Now;
                currsec = dateTime.Second;
                currTableName = String.Format("data_{0}_{1}_{2}_{3}_{4}_{5}",dateTime.Year, dateTime.Month.ToString().PadLeft(2, '0'), dateTime.Day.ToString().PadLeft(2, '0'), dateTime.Hour.ToString().PadLeft(2, '0'), dateTime.Minute.ToString().PadLeft(2, '0'), dateTime.Second.ToString().PadLeft(2, '0'));
                sqlCommand.CommandText = String.Format("CREATE TABLE[dbo].[{0}]([timestamp][datetime2](7) NULL,[text] [char](10) NULL) ON[PRIMARY]", currTableName);
            }
            




        sqlCommand.Connection.Close();

        }
        public static string RandomString(int length)
        {
             const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
             return new string(Enumerable.Repeat(chars, length)
               .Select(s => s[random.Next(s.Length)]).ToArray());
            //return "AAAAA";
        }
    }
}
