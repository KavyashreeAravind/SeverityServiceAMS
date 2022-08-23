using AuditSeverityService.Models;
using AuditSeverityService.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AuditSeverityService.Providers;
using AuditSeverityService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditSeverityService.Repositories
{
    public class AuditSeverityRepos : IAuditSeverityRepos
    {
        private readonly AuditManagementSystemContext _context;
        readonly string _baseUrlForAudityBenchmarkApi = "http://20.225.25.207/";
        public Dictionary<string, int> auditBenchmark = new Dictionary<string, int>();
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityRepos));

        public AuditSeverityRepos()
        {

        }

        public AuditSeverityRepos(AuditManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<Audit> PostAudit(AuditDetails item)
        {
            Audit audit = null;
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                audit = new Audit()
                {
                    Auditid = item.Auditid,
                    ProjectName = item.ProjectName,
                    ProjectManagerName = item.ProjectManagerName,
                    ApplicationOwnerName = item.ApplicationOwnerName,
                    AuditType = item.AuditType,
                    AuditDate = DateTime.Now,
                    ProjectExecutionStatus = item.ProjectExecutionStatus,
                    RemedialActionDuration = item.RemedialActionDuration,
                    Userid = item.Userid
                };
                await _context.Audit.AddAsync(audit);
                await _context.SaveChangesAsync();
            }
            return audit;
        }
        
        public Dictionary<string, int> GetCountOfNosAllowed(AuditDetails item)
        {
            _log4net.Info("GetCountOfNosAllowed method of AuditSeverityRepo called");
            _log4net.Info("InterServiceCommunicationBegins");
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_baseUrlForAudityBenchmarkApi);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = httpClient.GetAsync("api/AuditBenchMark/" + item.AuditType).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        _log4net.Info("Audit Benchmark For Audit Type " + item.AuditType + " Was Called !!");
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        auditBenchmark = JsonConvert.DeserializeObject<Dictionary<string, int>>(Response);
                    }
                }
                _log4net.Info("InterServiceCommunicationEnds");
                return auditBenchmark;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
           
}

