using System.Text.Json.Serialization;

namespace DoItAllList_API.Models

{

    public class GetListItemDBRequest
    {

        [JsonPropertyName("UserID")]
        public int UserID { get; set; }

        [JsonPropertyName("ListID")]
        public int ListID { get; set; }

        [JsonPropertyName("ListTitle")]
        public string ListTitle { get; set; }

        [JsonPropertyName("ListColour")]
        public string ListColour { get; set; }

        [JsonPropertyName("ListItemID")]
        public int ListItemID { get; set; }

        [JsonPropertyName("Text")]
        public string Text { get; set; }

        [JsonPropertyName("isChecked")]
        public bool isChecked { get; set; }

        public GetListItemDBRequest(int _UserID, int _ListID, string _ListTitle, string _ListColour, int _ListItemID, string _Text)
        {
            this.UserID = _UserID;
            this.ListID = _ListID;
            this.ListTitle = _ListTitle;
            this.ListColour = _ListColour;
            this.ListItemID = _ListItemID;
            this.Text = _Text;
        }
    }

}