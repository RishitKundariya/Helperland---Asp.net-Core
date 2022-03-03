using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
   public interface IRatingRepository
    {
        public bool AddRating(int serviceRequestId, decimal onTime, decimal friendly, decimal qualityOfService, string feedBack);
        public decimal? GetRating(int? serviceProviderId);
    }
}
