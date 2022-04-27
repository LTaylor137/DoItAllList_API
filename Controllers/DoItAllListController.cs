using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DoItAllList_API.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;





namespace DoItAllList_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoItAllListController : ControllerBase

    {

        SqlConnection connection = new SqlConnection();

        private readonly IConfiguration configuration;
        string connectionString = "";

        public List<GetListItemDBRequest> ListFromDB = new List<GetListItemDBRequest>();

        public List<LocalListt> ListOfListsFromDB = new List<LocalListt>();

        public DoItAllListController(IConfiguration _configuration)
        {
            // this.configuration = iConfig;

            // use this when working from Local machine database 
            connectionString = _configuration.GetSection("ConnectionStrings").GetSection("LocalConnectionString").Value;

            // use this when working from AWS hosted database 
            // connectionString = this.configuration.GetSection("ConnectionStrings").GetSection("DBConnectionString").Value;
        }


        //this is a version of the list which exists only in the API.
        static public List<LocalListItem> List = new List<LocalListItem>();

        // GET DoItAllList/GenerateList
        [HttpGet("GenerateList")]
        public void GenerateList()
        {
            List.Add(new LocalListItem(1, "Apples"));
            List.Add(new LocalListItem(2, "Milk"));
            List.Add(new LocalListItem(3, "Cat Food"));
        }

        // GET DoItAllList/GenerateAndShowList
        [HttpGet("GenerateAndShowList")]
        public ActionResult<IEnumerable<LocalListItem>> GenerateAndShowList()
        {
            List.Add(new LocalListItem(1, "Apples"));
            List.Add(new LocalListItem(2, "Milk"));
            List.Add(new LocalListItem(3, "Cat Food"));
            return List;
        }

        // GET DoItAllList/GetList
        [HttpGet("GetListFromDB")]
        public List<LocalListItem> GetListFromDB()
        {
            return List;
        }

        // GET DoItAllList/GetAllListItemsFromDB
        [HttpGet("GetAllListItemsFromDB")]
        public List<GetListItemDBRequest> GetAllListItemsFromDB()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            ListFromDB = new List<GetListItemDBRequest>();

               string queryString = "SELECT * " +
                                 "FROM LISTITEM LM " +
                                 "INNER JOIN LIST LT " +
                                 "ON LM.USERID = LT.USERID " +
                                 "AND LM.LISTID = LT.LISTID ";

            SqlCommand command = new SqlCommand(queryString, connection);
            Console.WriteLine(command);
            connection.Open();
            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //add all values as a list item to ListfromDB
                        ListFromDB.Add(
                        new GetListItemDBRequest((int)reader[0], (int)reader[1], reader[7].ToString(), reader[8].ToString(), (int)reader[2], reader[3].ToString())
                        );
                    };
                };
                return ListFromDB;
            }
            finally
            {
                connection.Close();
            }
        }

        // GET DoItAllList/GetAllListItemsFromDBOfUser
        [HttpGet("GetAllListItemsFromDBOfUser")]
        public List<GetListItemDBRequest> GetAllListItemsFromDBOfUser(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string queryString = "SELECT * " +
                                 "FROM LISTITEM LM " +
                                 "INNER JOIN LIST LT " +
                                 "ON LM.USERID = LT.USERID " +
                                 "AND LM.LISTID = LT.LISTID " +
                                 "WHERE LM.USERID = @QUERYID";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@QUERYID", id);
            Console.WriteLine(command);
            connection.Open();
            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //add all values as a list item to ListfromDB
                        ListFromDB.Add(
                        new GetListItemDBRequest((int)reader[0], (int)reader[1], reader[7].ToString(), reader[8].ToString(), (int)reader[2], reader[3].ToString())
                        );
                    };
                };
                return ListFromDB;
            }
            finally
            {
                connection.Close();
            }
        }

        // POST api/values
        [HttpPost("PostListItemToDB")]
        public PostListItemDBRequest PostListItemToDB(NewListItem model)
        {
            string queryString = "";
            PostListItemDBRequest ListItemR = new PostListItemDBRequest(model.UserID, model.ListID, model.ListItemID, model.Text);

            queryString = "INSERT INTO LISTITEM (USERID, LISTID, LISTITEMID, [TEXT], ISCHECKED) " +
                          "VALUES (@USERID, @LISTID, @LISTITEMID, '@TEXT', 0)";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@USERID", ListItemR.UserID);
            command.Parameters.AddWithValue("@LISTID", ListItemR.ListID);
            command.Parameters.AddWithValue("@LISTITEMID", ListItemR.ListItemID);
            command.Parameters.AddWithValue("@TEXT", ListItemR.Text);

            try
            {
                connection.Open();
                var result = command.ExecuteNonQuery();
                return ListItemR;
            }
            finally
            {
                connection.Close();
            }


        }





        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

}
