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


        public List<GetUserDBRequest> UsersFromDB = new List<GetUserDBRequest>();


        public List<GetListItemRequest> ListItemsFromDB = new List<GetListItemRequest>();

        public List<List> EmptyListsFromDB = new List<List>();

        public List<LocalList> ListOfListsFromDB = new List<LocalList>();

        public DoItAllListController(IConfiguration _configuration)
        {
            // use this when working from Local machine database 
            connectionString = _configuration.GetSection("ConnectionStrings").GetSection("LocalConnectionString").Value;

            // use this when working from AWS hosted database 
            // connectionString = this.configuration.GetSection("ConnectionStrings").GetSection("DBConnectionString").Value;
        }


        // ======================================================================================================================================
        // ================================================   LOCAL API CALLS FOR REFERENCE    ==================================================
        // ======================================================================================================================================



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

        // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }

        // ======================================================================================================================================
        // ================================================         DATABASE CALLS          =====================================================
        // ======================================================================================================================================


        // GET DoItAllList/GetAllUsersFromDB
        [HttpGet("GetAllUsersFromDB")]
        public List<GetUserDBRequest> GetAllUsersFromDB()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            UsersFromDB = new List<GetUserDBRequest>();

            string queryString = "SELECT * " +
                                 "FROM [USER]";

            SqlCommand command = new SqlCommand(queryString, connection);
            Console.WriteLine(command);
            connection.Open();
            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //add all values as a list item to ListItemsFromDB
                        UsersFromDB.Add(
                        new GetUserDBRequest((int)reader[0], (string)reader[1])
                        );
                    };
                };
                return UsersFromDB;
            }
            finally
            {
                connection.Close();
            }
        }


        // GET DoItAllList/GetAllListItemsFromDB
        [HttpGet("GetAllListItemsFromDB")]
        public List<GetListItemRequest> GetAllListItemsFromDB()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            ListItemsFromDB = new List<GetListItemRequest>();

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
                        //add all values as a list item to ListItemsFromDB
                        ListItemsFromDB.Add(
                        new GetListItemRequest((int)reader[0], (int)reader[1], reader[7].ToString(), reader[8].ToString(), (int)reader[2], reader[3].ToString(), (bool)reader[5])
                        );
                    };
                };
                return ListItemsFromDB;
            }
            finally
            {
                connection.Close();
            }
        }

        // GET DoItAllList/GetAllListItemsFromDBOfUser
        // [HttpGet("GetAllListItemsFromDBOfUser")]
        // public List<GetListItemRequest> GetAllListItemsFromDBOfUser(int id)
        // {
        //     SqlConnection connection = new SqlConnection(connectionString);

        //     string queryString = "SELECT * " +
        //                          "FROM LISTITEM LM " +
        //                          "INNER JOIN LIST LT " +
        //                          "ON LM.USERID = LT.USERID " +
        //                          "AND LM.LISTID = LT.LISTID " +
        //                          "WHERE LM.USERID = @QUERYID";

        //     SqlCommand command = new SqlCommand(queryString, connection);
        //     command.Parameters.AddWithValue("@QUERYID", id);
        //     Console.WriteLine(command);
        //     connection.Open();
        //     try
        //     {
        //         using (SqlDataReader reader = command.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 //add all values as a list item to ListItemsFromDB
        //                 ListItemsFromDB.Add(
        //                 new GetListItemRequest((int)reader[0], (int)reader[1], reader[7].ToString(), reader[8].ToString(), (int)reader[2], reader[3].ToString(), (bool)reader[5])
        //                 );
        //             };
        //         };
        //         return ListItemsFromDB;
        //     }
        //     finally
        //     {
        //         connection.Close();
        //     }
        // }

        // GET DoItAllList/GetAllListItemsAndEmptyListsFromDBOfUser
        [HttpGet("GetAllListItemsAndEmptyListsFromDBOfUser")]
        public List<GetListItemRequest> GetAllListItemsAndEmptyListsFromDBOfUser(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string queryString1 =
                                "SELECT LM.USERID, LM.LISTID, LM.LISTITEMID, " +
                                "LM.TEXT, LM.ISCHECKED, LT.LISTTITLE, LT.LISTCOLOUR " +
                                "FROM LISTITEM LM " +
                                "INNER JOIN LIST LT " +
                                "ON LM.USERID = LT.USERID " +
                                "AND LM.LISTID = LT.LISTID " +
                                "WHERE LM.USERID = @QUERYID;";
            string queryString2 =
                                "SELECT L.USERID, L.LISTID, " +
                                "0 AS LISTITEMID, " +
                                "NULL AS TEXT, " +
                                "0 AS ISCHECKED, " +
                                "L.LISTTITLE, " +
                                "L.LISTCOLOUR " +
                                "FROM LIST L " +
                                "WHERE L.USERID = @QUERYID " +
                                "AND L.LISTID NOT IN " +
                                "(SELECT I.LISTID FROM LISTITEM I WHERE I.USERID = @QUERYID)";


            SqlCommand command1 = new SqlCommand(queryString1, connection);
            command1.Parameters.AddWithValue("@QUERYID", id);

            SqlCommand command2 = new SqlCommand(queryString2, connection);
            command2.Parameters.AddWithValue("@QUERYID", id);

            connection.Open();
            try
            {
                using (SqlDataReader reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //add all values as a list item to ListItemsFromDB
                        ListItemsFromDB.Add(
                        new GetListItemRequest((int)reader[0], (int)reader[1], reader[5].ToString(), reader[6].ToString(), (int)reader[2], reader[3].ToString(), (bool)reader[4])
                        );
                    };
                };
                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //add all values as a list item to ListItemsFromDB
                        ListItemsFromDB.Add(
                        new GetListItemRequest((int)reader[0], (int)reader[1], reader[5].ToString(), reader[6].ToString(), (int)reader[2], reader[3].ToString(), (bool)reader[4])
                        );
                    };
                };
                return ListItemsFromDB;
            }
            finally
            {
                connection.Close();
            }
        }



        // POST api/DoItAllList/CreateNewUser
        [HttpPost("CreateNewUser")]
        public GetUserDBRequest CreateNewUser(User model)
        {
            string queryString = "";
            GetUserDBRequest ListItemR = new GetUserDBRequest(model.UserID, model.Username);

            queryString = "INSERT INTO [USER] (USERID, USERNAME) " +
                          "VALUES (@USERID, @USERNAME)";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@USERID", ListItemR.UserID);
            command.Parameters.AddWithValue("@USERNAME", ListItemR.Username);

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



        // POST api/DoItAllList/PostListItemToDB
        [HttpPost("PostListItemToDB")]
        public PostListItemRequest PostListItemToDB(ListItem model)
        {
            string queryString = "";
            PostListItemRequest ListItemR = new PostListItemRequest(model.UserID, model.ListID, model.ListItemID, model.Text);

            queryString = "INSERT INTO LISTITEM (USERID, LISTID, LISTITEMID, [TEXT], ISCHECKED) " +
                          "VALUES (@USERID, @LISTID, @LISTITEMID, @TEXT, 0)";

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


        // POST api/DoItAllList/PostListOfListItemsToDB
        //TODO


        // POST api/DoItAllList/CreateNewList
        [HttpPost("CreateNewList")]
        public PostListRequest CreateNewList(List model)
        {
            string queryString = "";
            PostListRequest ListItemR = new PostListRequest(model.UserID, model.ListID, model.ListTitle, model.ListColour);

            queryString = "INSERT INTO LIST (USERID, LISTID, LISTTITLE, LISTCOLOUR) " +
                          "VALUES (@USERID, @LISTID, @LISTTITLE, @LISTCOLOUR)";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@USERID", ListItemR.UserID);
            command.Parameters.AddWithValue("@LISTID", ListItemR.ListID);
            command.Parameters.AddWithValue("@LISTTITLE", ListItemR.ListTitle);
            command.Parameters.AddWithValue("@LISTCOLOUR", ListItemR.ListColour);

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


        // POST api/DoItAllList/UpdateListTitle
        [HttpPut("UpdateListTitle")]
        public PostListRequest UpdateListTitle(List model)
        {
            string queryString = "";
            PostListRequest ListR = new PostListRequest(model.UserID, model.ListID, model.ListTitle, model.ListColour);

            queryString = "UPDATE LIST " +
                            "SET LISTTITLE = @LISTTITLE " +
                            "WHERE USERID = @USERID " +
                            "AND LISTID = @LISTID ";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@USERID", ListR.UserID);
            command.Parameters.AddWithValue("@LISTID", ListR.ListID);
            command.Parameters.AddWithValue("@LISTTITLE", ListR.ListTitle);

            try
            {
                connection.Open();
                var result = command.ExecuteNonQuery();
                return ListR;
            }
            finally
            {
                connection.Close();
            }
        }


        // POST api/DoItAllList/UpdateListColour
        [HttpPut("UpdateListColour")]
        public PostListRequest UpdateListColour(List model)
        {
            string queryString = "";
            PostListRequest ListR = new PostListRequest(model.UserID, model.ListID, model.ListTitle, model.ListColour);

            queryString = "UPDATE LIST " +
                            "SET LISTCOLOUR = @LISTCOLOUR " +
                            "WHERE USERID = @USERID " +
                            "AND LISTID = @LISTID ";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@USERID", ListR.UserID);
            command.Parameters.AddWithValue("@LISTID", ListR.ListID);
            command.Parameters.AddWithValue("@LISTCOLOUR", ListR.ListColour);

            try
            {
                connection.Open();
                var result = command.ExecuteNonQuery();
                return ListR;
            }
            finally
            {
                connection.Close();
            }
        }




        // POST api/DoItAllList/UpdateListItemChecked
        [HttpPut("UpdateListItemChecked")]
        public PutListItemRequest UpdateListItemChecked(ListItem model)
        {
            string queryString = "";
            PutListItemRequest ListR = new PutListItemRequest(model.UserID, model.ListID, model.ListItemID, model.IsChecked);

            queryString = "UPDATE LISTITEM " +
                            "SET ISCHECKED = @ISCHECKED " +
                            "WHERE USERID = @USERID " +
                            "AND LISTID = @LISTID " + 
                            "AND LISTITEMID = @LISTITEMID ";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@USERID", ListR.UserID);
            command.Parameters.AddWithValue("@LISTID", ListR.ListID);
            command.Parameters.AddWithValue("@LISTITEMID", ListR.ListItemID);
             command.Parameters.AddWithValue("@ISCHECKED", ListR.IsChecked);

            try
            {
                connection.Open();
                var result = command.ExecuteNonQuery();
                return ListR;
            }
            finally
            {
                connection.Close();
            }
        }




        // POST api/DoItAllList/DeleteListItemFromDB
        [HttpPut("DeleteListItemFromDB")]
        public PostListItemRequest DeleteListItemFromDB(ListItem model)
        {
            string queryString = "";
            PostListItemRequest ListItemR = new PostListItemRequest(model.UserID, model.ListID, model.ListItemID, model.Text);

            queryString = "DELETE FROM LISTITEM " +
                            "WHERE USERID = @USERID " +
                            "AND LISTID = @LISTID " +
                            "AND LISTITEMID = @LISTITEMID";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@USERID", ListItemR.UserID);
            command.Parameters.AddWithValue("@LISTID", ListItemR.ListID);
            command.Parameters.AddWithValue("@LISTITEMID", ListItemR.ListItemID);

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

        // POST api/DoItAllList/DeleteListFromDB
        [HttpPut("DeleteListFromDB")]
        public PostListRequest DeleteListFromDB(List model)
        {
            string queryString = "";
            PostListRequest ListItemR = new PostListRequest(model.UserID, model.ListID, "x", "x");

            queryString = "DELETE FROM LISTITEM " +
                            "WHERE USERID = @USERID " +
                            "AND LISTID = @LISTID " +
                            "DELETE FROM LIST " +
                            "WHERE USERID = @USERID " +
                            "AND LISTID = @LISTID ";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@USERID", ListItemR.UserID);
            command.Parameters.AddWithValue("@LISTID", ListItemR.ListID);

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

    }

}
