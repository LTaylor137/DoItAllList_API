using System.Text.Json.Serialization;

namespace DoItAllList_API.Models

{

    public class GetUserDBRequest
    {

        [JsonPropertyName("UserID")]
        public int UserID { get; set; }

        [JsonPropertyName("Username")]
        public string Username { get; set; }


        public GetUserDBRequest(int _UserID, string _Username)
        {
            this.UserID = _UserID;
            this.Username = _Username;
        }
    }

}