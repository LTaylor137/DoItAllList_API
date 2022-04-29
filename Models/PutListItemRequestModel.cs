using System.Text.Json.Serialization;

namespace DoItAllList_API.Models

{
    public class PutListItemRequest
    {
        [JsonPropertyName("UserID")]
        public int UserID { get; set; }

        [JsonPropertyName("ListID")]
        public int ListID { get; set; }

        [JsonPropertyName("ListItemID")]
        public int ListItemID { get; set; }

        [JsonPropertyName("IsChecked")]
        public bool IsChecked { get; set; }

        public PutListItemRequest(int _UserID, int _ListID, int _ListItemID, bool _IsChecked)
        {
            this.UserID = _UserID;
            this.ListID = _ListID;
            this.ListItemID = _ListItemID;
            this.IsChecked = _IsChecked;
        }
    }

}