using System.Text.Json.Serialization;

namespace DoItAllList_API.Models

{

    public class LocalListItem
    {

        [JsonPropertyName("ListItemID")]
        public int ListItemID { get; set; }

        [JsonPropertyName("Text")]
        public string Text { get; set; }

        [JsonPropertyName("isChecked")]
        public bool isChecked { get; set; }


        public LocalListItem() { }

        public LocalListItem(int _ListItemID, string _Text)
        {
            this.ListItemID = _ListItemID;
            this.Text = _Text;
            // this.isChecked = false;
        }
    }

}