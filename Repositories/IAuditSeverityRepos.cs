using AuditSeverityService.Models;
using AuditSeverityService.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityService.Repositories
{
    public interface IAuditSeverityRepos
    {
        Task<Audit> PostAudit(AuditDetails item);
        public Dictionary<string, int> GetCountOfNosAllowed(AuditDetails item);

    }
}
