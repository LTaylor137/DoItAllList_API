using System.Text.Json.Serialization;

namespace DoItAllList_API.Models

{
    public class DeletexxxListRequest
    {
        public int UserID { get; set; }
        public int ListID { get; set; }

    }
}