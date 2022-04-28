using System.Text.Json.Serialization;

namespace DoItAllList_API.Models

{
    public class List
    {
        public int UserID { get; set; }
        public int ListID { get; set; }
        public string ListTitle { get; set; }
        public string ListColour { get; set; }
    }

}