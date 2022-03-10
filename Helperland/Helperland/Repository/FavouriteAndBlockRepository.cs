using Helperland.Models.Data;
using Helperland.Models.ViewModel.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
    public class FavouriteAndBlockRepository:IFavouriteAndBlockRepository
    {
        private readonly HelperlandContext helperlandContext;
        public FavouriteAndBlockRepository(HelperlandContext helperlandContext)
        {
            this.helperlandContext = helperlandContext;
        }

        #region Get List of All Customer
        public List<BlockCustomerrViewModel> GetListOfCustomer(int userId)
        {
            List<BlockCustomerrViewModel> lists = new List<BlockCustomerrViewModel>();
            List<ServiceRequest> li = helperlandContext.ServiceRequests.Where(x=>x.ServiceProviderId== userId).ToList();
            foreach(var item in li)
            {
                try
                {
                    User user = helperlandContext.Users.Where(x => x.UserId == item.UserId).FirstOrDefault();
                    BlockCustomerrViewModel temp = new BlockCustomerrViewModel();
                    FavoriteAndBlocked favoriteAndBlocked = helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId==item.ServiceProviderId && x.TargetUserId == item.UserId).FirstOrDefault();
                    if (favoriteAndBlocked == null)
                    {
                        temp.UserId = user.UserId;
                        temp.Username = user.FirstName + " " + user.LastName;
                        temp.IsBlock = false;
                    }
                    else
                    {
                        temp.UserId = user.UserId;
                        temp.Username = user.FirstName + " " + user.LastName;
                        temp.IsBlock = favoriteAndBlocked.IsBlocked;
                    }
                    bool alreadyExists = lists.Any(x => x.UserId == temp.UserId);
                    if (!alreadyExists)
                    {
                        lists.Add(temp);
                    }
                }
                catch(Exception ex)
                {
                    return lists;
                }
                
                
            }


            return lists;
        }
        #endregion

        #region Block Customer
        public bool BlockCustomer(int userId,int targetUserId)
        {
            FavoriteAndBlocked favoriteAndBlocked = helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == userId && x.TargetUserId == targetUserId).FirstOrDefault();
            if(favoriteAndBlocked == null)
            {
                favoriteAndBlocked = new FavoriteAndBlocked();
                favoriteAndBlocked.UserId = userId;
                favoriteAndBlocked.TargetUserId = targetUserId;
                favoriteAndBlocked.IsFavorite = false;
                favoriteAndBlocked.IsBlocked = true;
                helperlandContext.FavoriteAndBlockeds.Add(favoriteAndBlocked);
                helperlandContext.SaveChanges();
            }
            else
            {
                favoriteAndBlocked.IsFavorite = false;
                favoriteAndBlocked.IsBlocked = true;
                helperlandContext.FavoriteAndBlockeds.Update(favoriteAndBlocked);
                helperlandContext.SaveChanges();
            }
            return true;
        }
        #endregion

        #region Unblock  Customer
        public bool UnblockCustomer(int userId, int targetUserId)
        {
            FavoriteAndBlocked favoriteAndBlocked = helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == userId && x.TargetUserId == targetUserId).FirstOrDefault();
            if (favoriteAndBlocked == null)
            {
                favoriteAndBlocked = new FavoriteAndBlocked();
                favoriteAndBlocked.UserId = userId;
                favoriteAndBlocked.TargetUserId = targetUserId;
                favoriteAndBlocked.IsFavorite = false;
                favoriteAndBlocked.IsBlocked = false;
                helperlandContext.FavoriteAndBlockeds.Add(favoriteAndBlocked);
                helperlandContext.SaveChanges();
            }
            else
            {
                favoriteAndBlocked.IsFavorite = false;
                favoriteAndBlocked.IsBlocked = false;
                helperlandContext.FavoriteAndBlockeds.Update(favoriteAndBlocked);
                helperlandContext.SaveChanges();
            }
            return true;
        }
        #endregion
    }
}
