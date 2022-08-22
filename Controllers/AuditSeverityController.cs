using AuditSeverityService.Models;
using AuditSeverityService.Models.ViewModels;
using AuditSeverityService.Providers;
using AuditSeverityService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuditSeverityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditSeverityController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityController));
        private readonly IAuditSeverityProvider _context;

        public AuditSeverityController(IAuditSeverityProvider context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAudit(AuditDetails item)
        {
            _log4net.Info("AUDIT SEVERITY SERVICE STARTS");
            
            Dictionary<string, int> auditBenchmark = new Dictionary<string, int>();
            if (!ModelState.IsValid)
            {
                _log4net.Warn("Invalid ModelState");
                _log4net.Info("AUDIT SEVERITY PROCESS ENDS");
                _log4net.Info("----------------------------------------------------------------");
                return BadRequest(ModelState);
            }
            try
            {
                _log4net.Info("Status and action attribute set process initiated");
                item = _context.SetStatusAndAction(item);
                if (item == null)
                    return BadRequest(item);
                _log4net.Info("Saving contents to database");
                var addAudit = await _context.PostAudit(item);
                _log4net.Info("Process completed");
                _log4net.Info("AUDIT SEVERITY PROCESS ENDS");
                _log4net.Info("----------------------------------------------------------------");
               return Ok(addAudit);

            }
            catch (Exception)
            {
                _log4net.Error("Bad Request - 400");
                _log4net.Info("AUDIT SEVERITY PROCESS ENDS");
                _log4net.Info("----------------------------------------------------------------");
                return BadRequest();
            }
        }
    }
}

