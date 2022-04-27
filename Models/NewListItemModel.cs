using System.Text.Json.Serialization;

namespace DoItAllList_API.Models

{
    public class NewListItem
    {
        public int UserID { get; set; }
        public int ListID { get; set; }
        public int ListItemID { get; set; }
        public string Text { get; set; }
    }

}