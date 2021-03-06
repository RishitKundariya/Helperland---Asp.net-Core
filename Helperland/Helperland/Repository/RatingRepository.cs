using Helperland.Models.Data;
using Helperland.Models.ViewModel.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
    public class RatingRepository :IRatingRepository
    {
        private readonly HelperlandContext _helperlandContext;
        public RatingRepository(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        public bool AddRating(int serviceRequestId, decimal onTime, decimal friendly, decimal qualityOfService, string feedBack)
        {
            ServiceRequest sr = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            Rating rating = new Rating();
            rating.ServiceRequestId = sr.ServiceRequestId;
            rating.RatingFrom = sr.UserId;
            rating.RatingTo =(int) sr.ServiceProviderId;
            rating.Comments = feedBack;
            rating.OnTimeArrival = onTime;
            rating.Friendly = friendly;
            rating.QualityOfService = qualityOfService;
            decimal i = (friendly + onTime + qualityOfService)/3 ;
            rating.Ratings = i;
            rating.RatingDate = DateTime.Now;
            try
            {
                _helperlandContext.Ratings.Add(rating);
                _helperlandContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }

        public decimal? GetRating(int? serviceProviderId)
        {
            if(serviceProviderId == null)
            {
                return null;
            }
            else
            {
                List<Rating> ratings = _helperlandContext.Ratings.Where(x => x.RatingTo == serviceProviderId).ToList();
                int i = 0;
                decimal sum = 0;
                foreach(var item in ratings)
                {
                    sum += item.Ratings;
                    i++;
                }
                if (i == 0)
                {
                    return 0;
                }
                else
                {
                    return Decimal.Round(sum / i);
                }
                
            }
        }

        public List<RatingsViewModel> GetRatingsForServiceProvider(int serviceproviderId)
        {
            List<RatingsViewModel> ratingsViewModels=new List<RatingsViewModel>();
            List<Rating> ratings = _helperlandContext.Ratings.Where(x => x.RatingTo == serviceproviderId).ToList();
            if (ratings != null)
            {
                foreach(var item in ratings)
                {
                    RatingsViewModel r = new RatingsViewModel();
                    User u = _helperlandContext.Users.Where(x => x.UserId == item.RatingFrom).FirstOrDefault();
                    ServiceRequest s = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == item.ServiceRequestId).FirstOrDefault();
                    r.RatingID = item.RatingId;
                    r.CustomerName = u.FirstName + " " + u.LastName;
                    r.Comments = item.Comments;
                    r.Rating = Decimal.Round(item.Ratings);
                    r.ServiceRequestID = item.ServiceRequestId;
                    r.ServiceDate = s.ServiceStartDate.ToString("dd/MM/yyyy");
                    ratingsViewModels.Add(r);
                }
                return ratingsViewModels;
            }
            else
                return null;
                
        }
    }
}
