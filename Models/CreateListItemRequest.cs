using System.Text.Json.Serialization;

namespace DoItAllList_API.Models

{
    public class CreatexxxListItemRequest
    {
        public int UserID { get; set; }
        public int ListID { get; set; }
        public int ListItemID { get; set; }
        public string Text { get; set; }
        public bool isChecked { get; set; }
    }

}