using Helperland.Models.ViewModel.Customer;
using Helperland.Models.ViewModel.ServiceProvider;
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

        public List<NewServiceRequestViewModel> GetServiceRequestsNotAccepted(int userId, bool hasPate);
        public object GetServiceDetailsforServiceProvider(int serviceRequestId);
        public string AcceptServiceRequest(int userId, int serviceRequestId);

        public List<NewServiceRequestViewModel> GetServiceRequestsIsAccepted(int userId);
        public bool CompletedService(int serviceRequestId);
        public bool CancelServiceRequest(int serviceRequestId, string message);
        public List<ServiceProviderServiceHistoryViewModel> GetServiceHistoryForServiceProvider(int useId);


    }
}
