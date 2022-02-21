using System;
using System.Collections.Generic;

#nullable disable

namespace Helperland.Models.Data
{
    public partial class UserAddress
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string ZipCode { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual User User { get; set; }
    }
}
