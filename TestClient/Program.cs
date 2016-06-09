using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data.Extensions;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // See "Client.Execute()" for usage.
        }
    }

    public class Client
    {
        public Client()
        {
        }

        public void Execute()
        {
            // Stored Procedure with parameter
            // -------------------------------

            var manager = new SqlDataManager("DefaultDatabase"); // DefaultDatabase = connection string key

            // Map to collection of Model
            IEnumerable<Person> persons = manager
                .CommandText("spGetPersonsList")
                .AddParameter("@DepartmentID", 1)
                .IsStoredProcedure()
                .Fetch<Person>();

            // Map to Model
            Person person = manager
                .CommandText("spGetPersonsList")
                .AddParameter("@DepartmentID", 1)
                .IsStoredProcedure()
                .Fetch<Person>()
                .Single();

            // To Data Table
            DataTable dataTable = manager
                .CommandText("spGetPersonsList")
                .AddParameter("@DepartmentID", 1)
                .IsStoredProcedure()
                .FetchAsDataTable();

            // DataTable to Model
            var collection = dataTable.As<Person>();


            // SQL query as command text
            // -------------------------

            IEnumerable<Person> personsTwo = manager
                .CommandText("SELECT * FROM Persons")
                .Fetch<Person>();


            // Execute Non-Query
            //------------------

            manager
                .CommandText("spInsertPerson")
                .IsStoredProcedure()
                .AddParameter("@FirstName", "John")
                .AddParameter("@LastName", "Doe")
                .AddParameter("@Address", "Washington")
                .AddOutputParameter("@Status", SqlDbType.Int)
                .Execute();
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
