using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Interfaces;

namespace FcmHipri.Models
{
    public class ExceptionRequest
    {
        public Guid ExceptionRequestId{ get; set; }

        public DateTime RequestDate{ get; set; }

        public string RequestorName { get; set; }
        public string RequestorEmail { get; set; }
        public string BusinessJustification { get; set; }
        public DeploymentAuthorization Deployment { get; set; }

        public ExceptionRequestStatus Status{ get; set; }

        public Guid? ApproverId{ get; set; }

        public string ApproverName{ get; set; }

        public string ApproverEmail{ get; set; }

        public Guid[] FormerApproverIds{ get; set; }

        public StatusChange[] ChangeStatus{ get; set; }

        public ApproverNotes[] ApproversNotes{ get; set; }

        public ExceptionRequest(ExceptionRequestBody body)
        {
            Deployment = new DeploymentAuthorization(body.deployment);
            RequestorName = body.RequestorName;
            RequestorEmail = body.RequestorEmail;
            Status = ExceptionRequestStatus.PendingSubmission;
        }
        public ExceptionRequest(DeploymentAuthorization deployment)
        {
            Deployment = deployment;
            RequestorName = deployment.Requestor;
            RequestorEmail = deployment.RequestorEmail;
            Status = ExceptionRequestStatus.PendingSubmission;
        }

        public void Put(Guid id, ExceptionRequest newValues)
        {

        }
    }
}
