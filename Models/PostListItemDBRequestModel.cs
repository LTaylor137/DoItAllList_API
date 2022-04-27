using System.Text.Json.Serialization;

namespace DoItAllList_API.Models

{
    public class PostListItemDBRequest
    {
        [JsonPropertyName("UserID")]
        public int UserID { get; set; }

        [JsonPropertyName("ListID")]
        public int ListID { get; set; }

        [JsonPropertyName("ListItemID")]
        public int ListItemID { get; set; }

        [JsonPropertyName("Text")]
        public string Text { get; set; }

        public PostListItemDBRequest(int _UserID, int _ListID, int _ListItemID, string _Text)
        {
            this.UserID = _UserID;
            this.ListID = _ListID;
            this.ListItemID = _ListItemID;
            this.Text = _Text;
        }
    }

}