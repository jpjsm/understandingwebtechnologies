using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using DbCrudCore.Models;

namespace DbCrudCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting query!");

            Guid[] exceptionRequestIds = new Guid[] { };// { new Guid("C444D143-AF1C-4DBA-BD29-2882F24CEAF5"), new Guid("B093D8E5-2104-49B4-AF62-60D772E8CDD2") };
            Guid[] serviceIds = new Guid[] { };
            string approverEmail = "cvp02@example.test";// string.Empty;
            string requestorEmail = string.Empty;
            Guid[] subscriptionIds = new Guid[] { };
            string[] statuses = new string[] { "Declined" };
            DateTime? requestedStartFrom = null;
            DateTime? requestedUpTo = null;

            IEnumerable<ExceptionRequests> rows = GetExceptionRequestsAsync(
                ExceptionRequestIds: exceptionRequestIds,
                ServiceIds: serviceIds,
                ApproverEmail: approverEmail,
                RequestorEmail: requestorEmail,
                SubscriptionIds: subscriptionIds,
                statuses: statuses,
                RequestedStartFrom: requestedStartFrom,
                RequestedUpTo: requestedUpTo
                );

            foreach (ExceptionRequests row in rows)
            {
                Console.WriteLine($"{row.ApproverName} {row.ApproverEmail} {row.ExceptionRequestId}");
            }


            Console.WriteLine("Success!");
            Console.WriteLine("Press any key to end.");
            Console.ReadKey(true);
        }

        private static IEnumerable<ExceptionRequests> GetExceptionRequestsAsync(
             Guid[] ExceptionRequestIds,
             Guid[] ServiceIds,
             string ApproverEmail,
             string RequestorEmail,
             Guid[] SubscriptionIds,
             string[] statuses,
             DateTime? RequestedStartFrom,
             DateTime? RequestedUpTo
            )
        {
            using var _context = new fcmchangemanagersqlContext();
            if (ExceptionRequestIds != null && ExceptionRequestIds.Length > 0)
            {
                return _context.ExceptionRequests.AsNoTracking().AsEnumerable().Where(r => ExceptionRequestIds.Contains(r.ExceptionRequestId));
            }
            IEnumerable<ExceptionRequests> results = null; //_context.ExceptionRequests.ToList();
            Boolean noArguments = true;

            if (!string.IsNullOrWhiteSpace(ApproverEmail))
            {
                try
                {
                    results = _context.ExceptionRequests
                        .AsNoTracking()
                        .AsEnumerable()
                        .Where(r => String.Compare(r.ApproverEmail, ApproverEmail, StringComparison.InvariantCultureIgnoreCase) == 0)
                        .ToList();
                }
                catch (Exception ex)
                {

                    throw;
                }

                noArguments = false;
            }

            if (!string.IsNullOrWhiteSpace(RequestorEmail))
            {
                if (noArguments)
                {
                    results = _context.ExceptionRequests
                                .AsNoTracking()
                                .AsEnumerable()
                                .Where(r => String.Compare(r.RequestorEmail, RequestorEmail, StringComparison.InvariantCultureIgnoreCase) == 0);
                }
                else
                {
                    results = results.Where(r => String.Compare(r.RequestorEmail, RequestorEmail, StringComparison.InvariantCultureIgnoreCase) == 0);
                }

                noArguments = false;
            }

            // Defining the meaning of: RequestedStartFrom.HasValue && !RequestedUpTo.HasValue
            // Find all single exception requests that have some coverage from the date RequestStartFrom or earlier
            if (RequestedStartFrom.HasValue && !RequestedUpTo.HasValue)
            {
                results = noArguments 
                            ? _context.ExceptionRequests
                                .AsNoTracking()
                                .AsEnumerable()
                                .Where(r => r.ExceptionEndsOn > RequestedStartFrom.Value && r.ExceptionBeginsOn <= RequestedStartFrom.Value)
                            : results.Where(r => r.ExceptionEndsOn > RequestedStartFrom.Value && r.ExceptionBeginsOn <= RequestedStartFrom.Value);

                noArguments = false;
            }

            // Defining the meaning of: !RequestedStartFrom.HasValue && RequestedUpTo.HasValue
            // Finds all single exception request that have some cover before the date RequestedUpTo
            if (!RequestedStartFrom.HasValue && RequestedUpTo.HasValue)
            {
                results = noArguments
                            ? _context.ExceptionRequests
                                .AsNoTracking()
                                .AsEnumerable()
                                .Where(r => r.ExceptionBeginsOn < RequestedUpTo.Value && r.ExceptionEndsOn >= RequestedUpTo.Value)
                            : results.Where(r => r.ExceptionBeginsOn < RequestedUpTo.Value && r.ExceptionEndsOn >= RequestedUpTo.Value);

                noArguments = false;
            }

            // Defining the meaning of: RequestedStartFrom.HasValue && RequestedUpTo.HasValue
            // Find all single exception requests that cover the timespan between RequestedStartFrom and RequestedUpTo
            if (RequestedStartFrom.HasValue && RequestedUpTo.HasValue)
            {
                results = noArguments
                            ? _context.ExceptionRequests
                                .AsNoTracking()
                                .AsEnumerable()
                                .Where(r => r.ExceptionBeginsOn <= RequestedStartFrom.Value && r.ExceptionEndsOn >= RequestedUpTo.Value)
                            : results.Where(r => r.ExceptionBeginsOn <= RequestedStartFrom.Value && r.ExceptionEndsOn >= RequestedUpTo.Value);

                noArguments = false;
            }

            if (statuses != null && statuses.Length > 0)
            {
                results = noArguments
                            ? _context.ExceptionRequests
                                .AsNoTracking()
                                .AsEnumerable()
                                .Where(r => statuses.Contains(r.ExceptionRequestStatus))
                            : results.Where(r => statuses.Contains(r.ExceptionRequestStatus));

                noArguments = false;
            }

            if (ServiceIds != null && ServiceIds.Length > 0)
            {
                results = noArguments
                            ? _context.ExceptionRequests
                                .AsNoTracking()
                                .AsEnumerable()
                                .Where(r => r.ExceptionRequestServiceSubscriptionRegions
                                                .Select(s => s.ServiceId)
                                                .Distinct()
                                                .Any(k => ServiceIds.Contains(k)))

                            : results.Where(r => r.ExceptionRequestServiceSubscriptionRegions
                                                .Select(s => s.ServiceId)
                                                .Distinct()
                                                .Any(k => ServiceIds.Contains(k)));

                noArguments = false;
            }

            if (SubscriptionIds != null && SubscriptionIds.Length > 0)
            {
                results = noArguments
                            ? _context.ExceptionRequests
                                .AsNoTracking()
                                .AsEnumerable()
                                .Where(r => r.ExceptionRequestServiceSubscriptionRegions
                                                .Select(u => u.SubscriptionId)
                                                .Distinct()
                                                .Any(k => SubscriptionIds.Contains(k)))
                            : results.Where(r => r.ExceptionRequestServiceSubscriptionRegions
                                                .Select(u => u.SubscriptionId)
                                                .Distinct()
                                                .Any(k => SubscriptionIds.Contains(k)));

                noArguments = false;
            }

            // Let's limit the reply when it's unbounded
            if (noArguments)
            {
                results = _context.ExceptionRequests
                                .AsNoTracking()
                                .AsEnumerable()
                                .Where(r => r.RequestDate > DateTime.UtcNow.AddDays(-7.1)).OrderByDescending(r => r.RequestDate).Take(50);
            }

            return results;
        }
    
    }
}
