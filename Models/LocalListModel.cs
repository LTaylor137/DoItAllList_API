
// copied from angular app for reference



// import { ListItem } from './ListItem';
// // import { ListService } from '../Services/list.service';

// export class List {

//   ListID: number;
//   ListTitle: string;
//   List: ListItem[] = [];
//   ListColour: string;

//   isAddListItemsActive: boolean;
//   isShowListActive: boolean;
//   isShowListOptionsActive: boolean;
//   isListRenameActive: boolean;
//   isShowListColourOptionsActive: boolean;
//   isDeleteListOptionsActive: boolean;

//   constructor(_ListID: number, _ListTitle: string, _ListColour: string) {
//     this.ListID = _ListID;
//     this.ListTitle = _ListTitle;
//     this.isAddListItemsActive = false;
//     this.isShowListActive = true;

//     this.isShowListOptionsActive = false;
//     this.ListColour = _ListColour;
//     this.isListRenameActive = false;
//     this.isShowListColourOptionsActive = false;
//     this.isDeleteListOptionsActive = false;
//   }
  
// }


using System.Text.Json.Serialization;

namespace DoItAllList_API.Models

{

    public class LocalList
    {

        [JsonPropertyName("ListID")]
        public int ListID { get; set; }

        [JsonPropertyName("ListTitle")]
        public string ListTitle { get; set; }

        [JsonPropertyName("ListItem")]
        public LocalListItem[] ListItem { get; set; }

        [JsonPropertyName("ListColour")]
        public string ListColour { get; set; }


        public LocalList() { }

        public LocalList(int _ListID, string _ListTitle, LocalListItem[] _ListItem, string _ListColour)
        {
            this.ListID = _ListID;
            this.ListTitle = _ListTitle;
             this.ListItem = _ListItem;
            this.ListColour = _ListColour;
        }
    }

}


