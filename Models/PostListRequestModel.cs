using System.Text.Json.Serialization;

namespace DoItAllList_API.Models

{
    public class PostListRequest
    {
        [JsonPropertyName("UserID")]
        public int UserID { get; set; }

        [JsonPropertyName("ListID")]
        public int ListID { get; set; }

        [JsonPropertyName("ListTitle")]
        public string ListTitle { get; set; }

        [JsonPropertyName("ListColour")]
        public string ListColour { get; set; }



        public PostListRequest(int _UserID, int _ListID, string _ListTitle, string _ListColour)
        {
            this.UserID = _UserID;
            this.ListID = _ListID;
            this.ListTitle = _ListTitle;
            this.ListColour = _ListColour;
        }
    }

}