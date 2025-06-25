using System;
using System.Collections.Generic;
using System.Text;

namespace FcmHipri.Interfaces
{
    public interface IExceptionRequestManagement
    {
        public Guid StartExceptionRequestProcess(IDeployment request);
        public IExceptionRequest GetExceptionRequest(Guid exceptionRequestId);
        public void UpdateExceptionRequestWithEvents(Guid exceptionRequestId, IDeploymentAuthorization data);
        public ExceptionRequestStatus GetExceptionRequestSatus(Guid exceptionRequestId);
        public IExceptionRequest[] GetExceptionRequests(DateTime rangeStart, DateTime rangeEnd, ExceptionRequestStatus? withStatus);
        public IExceptionRequest[] GetExceptionRequestsForSubscriptions(IEnumerable<Guid> subscriptionIds);
        public IExceptionRequest[] GetExceptionRequestsForSubscriptions(IEnumerable<Guid> subscriptionIds, DateTime rangeStart, DateTime rangeEnd);
        public IExceptionRequest[] GetExceptionRequestsForRegions(IEnumerable<string> regions, DateTime rangeStart, DateTime rangeEnd);
        public IExceptionRequest[] GetExceptionRequestsForApprover(string approverId, DateTime rangeStart, DateTime rangeEnd);
    }
}
