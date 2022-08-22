using AuditSeverityService.Models;
using AuditSeverityService.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuditSeverityService.Providers
{
    public interface IAuditSeverityProvider
    {
        public Task<Audit> PostAudit(AuditDetails item);
        public AuditDetails SetStatusAndAction(AuditDetails item);


    }
}
