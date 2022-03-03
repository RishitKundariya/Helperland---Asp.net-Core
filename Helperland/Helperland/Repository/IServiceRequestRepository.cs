using Helperland.Models.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Repository
{
    public interface IServiceRequestRepository
    {
        public List<CurrentServicesViewModel> GetServicesByUserId(int userID);
        public object GetServiceDetails(int serviceRequestId);
        public string GetServiceDate(int serviceRequestId);
        public bool UpdateServiceDate(int serviceRequestId, DateTime serviceDate);
        public bool CancelService(int serviceRequestId,string message);
        public List<ServiceHistoryViewModel> GetServicesHistoryByUserId(int userID);


    }
}
