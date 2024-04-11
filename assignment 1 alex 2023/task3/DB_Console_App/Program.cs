using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Odbc;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Console_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new App();
            app.PrintConsole();
        
          
         
        }
    }

    public abstract class Position
    {
        public int ID { get; set; }
        public string fullName { get; set; }
        public Position(int ID, string Fullname)
        {
            this.ID = ID;
            this.fullName = Fullname;
        }

        public abstract string GetDetails();

    }

    public class Manager : Position
    { // set data to manager object
        //ID, Fullname, Age, TeamSize
        public double Age { get; set; }
        public double TeamSize { get; set; }

        public Manager(int ID, string Fullname, double Age, double TeamSize) : base(ID, Fullname)
        {
            this.Age = Age;
            this.TeamSize = TeamSize; 
           
        }
        public override string GetDetails()
        {
            return $"Manager \t{ID}\t{fullName}\t{Age}\t{TeamSize}";
        }


    }
    public class Salesperson : Position
    { //set data to salesperson object
        //ID, Fullname, Age, SalesVolume
        public double Age { get; set; }
        public double SalesVolume { get; set; }

        public Salesperson(int ID, string Fullname, double Age, double salesVolume) : base(ID, Fullname)
        {
            this.Age = Age;
            this.SalesVolume = salesVolume;
        }

        public override string GetDetails()
        {
            return $"Salesperson\t{ID}\t{fullName}\t{Age}\t{SalesVolume}";
        }

    }


  




    public class App
    {
        public List<Position> Positions { get; set; }
        OdbcConnection Conn;

        public App(){
            this.Conn = Util.GetConn();
            Positions = new List<Position>();
            ReadDB();
        }

       

        public void PrintConsole()
        {
            Console.WriteLine("Position\tID\tFullname\tAge\tDetails");
            Console.WriteLine("----------------------------------------------");
            foreach (var position in Positions)
            {
                Console.WriteLine(position.GetDetails());
            }
        }

        public void ReadDB()
        {
            using (var cmd = Conn.CreateCommand())
            {
                cmd.Connection = Conn;
                string sqlManager = "SELECT ID, Fullname, Age, TeamSize FROM Manager";
                string sqlSalesperson = "SELECT ID, Fullname, Age, SalesVolume FROM Salesperson";
                OdbcDataReader reader;

                //software table
                cmd.CommandText = sqlManager;

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Util.GetInt(reader["ID"]);
                    string name = Util.GetString(reader["Fullname"]);
                    double age = Util.GetDouble(reader["Age"]);

                    int ts = Util.GetInt(reader["TeamSize"]);

                    //checking bad data rows

                    if (id == Util.BAD_INT ||
                     name == Util.BAD_STRING ||
                     age == Util.BAD_DOUBLE ||
                     ts == Util.BAD_INT)
                    {
                        Console.WriteLine("Bad row detected");
                        continue;
                    }
                    var manager = new Manager(id, name, age, ts);
                    Positions.Add(manager);

                }
                reader.Close();


                //read salespersons
                cmd.CommandText = sqlSalesperson;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Util.GetInt(reader["ID"]);
                    string name = Util.GetString(reader["Fullname"]);
                    double age = Util.GetDouble(reader["Age"]);
                    double salesVolume = Util.GetDouble(reader["salesvolume"]);


                    //checking bad data rows

                    if (id == Util.BAD_INT ||
                     name == Util.BAD_STRING ||
                     age == Util.BAD_DOUBLE ||
                     salesVolume == Util.BAD_DOUBLE)
                    {
                        Console.WriteLine("Bad row detected");
                        continue;
                    }
                    var salesperson = new Salesperson(id, name, age, salesVolume);
                    Positions.Add(salesperson);

                }
            }
        }
    }

   

    public static class Util
    {
        internal static readonly string BAD_STRING = string.Empty;
        internal static readonly int BAD_INT = Int32.MinValue;
        internal static readonly double BAD_DOUBLE = Double.MinValue;
        public static int GetInt(object o)
        {
            if (o == null) return BAD_INT;
            int n;
            if (int.TryParse(o.ToString(), out n) == false)
                return BAD_INT;
            return n;
        }
        public static double GetDouble(object o)
        {
            if (o == null) return BAD_DOUBLE;
            double n;
            if (double.TryParse(o.ToString(), out n) == false) return BAD_DOUBLE;
            return n;
        }
        public static string GetString(object o)
        {
            //?? null-coalescing
            return o?.ToString() ?? BAD_STRING;
        }
        internal static OdbcConnection GetConn()
        {
            var dbstr = ConfigurationManager.AppSettings.Get("odbcString");
            string fpath = @"C:\Users\trist\OneDrive\Desktop\EmployeeData.accdb";
            string connstr = dbstr + fpath;
            var conn = new OdbcConnection(connstr);
            conn.Open();
            return conn;
        }
    }

    
}


//add the using statement to import the settings: using System.Configuration

//create model classes to map the tables in the database. consider creating a base class containing the shared fields of salesperson and manager class

//read the rows in the tables and save the data as objects

//display the data to console in tabular format