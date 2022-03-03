using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel.BookService
{
    public class AddressViewModel
    {
        [JsonPropertyName("addressID")]
        public int? AddressID { get; set; }
        [JsonPropertyName("userId")]
        public int? UserId { get; set; }
        [JsonPropertyName("addressLine1")]
        public string AddressLine1 { get; set; }
        [JsonPropertyName("addressLine2")]
        public string AddressLine2 { get; set; }
        [JsonPropertyName("zipCode")]
        public string ZipCode { get; set; }
        [JsonPropertyName("mobileNo")]
        public string MobileNo { get; set; }
        [JsonPropertyName("cityId")]
        public int? CityId { get; set; }
    }
}
