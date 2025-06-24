using System;
using System.Collections.Generic;
using System.Text;

namespace FcmHipri.Interfaces
{
    public enum ExceptionRequestStatus
    {
        Declined,
        PartiallyDeclined,
        Approved,
        PendingSubmission,
        PendingApproval,
        Delegated,
    }
}
