using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrueOmniWebApi.Models
{
    public class Listing
    {
        public int ListingID;
        public string Company;
        public string Image_List;
        public int CategoryID;
    }

    public class ListingDTO
    {
        public int ListingID { get; set; }
        public string Company { get; set; }
        public List<string> Image_List { get; set; }

        public ListingDTO(int listingID, string company, List<string> image_List)
        {
            ListingID = listingID;
            Company = company;
            Image_List = image_List;            
        }
    }
}
