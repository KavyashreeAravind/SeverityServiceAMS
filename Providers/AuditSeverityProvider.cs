using AuditSeverityService.Repositories;
using AuditSeverityService.Models;
using AuditSeverityService.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AuditSeverityService.Providers
{
    public class AuditSeverityProvider:IAuditSeverityProvider
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityProvider));
        IAuditSeverityRepos _repos;
        int CountOfNosAllowed;
        public AuditSeverityProvider()
        {

        }

        public AuditSeverityProvider(IAuditSeverityRepos repos)
        {
            this._repos = repos;    

        }
        public Task<Audit> PostAudit(AuditDetails item)
        {
            return _repos.PostAudit(item);

        }



        public AuditDetails SetStatusAndAction(AuditDetails item)
        {
            _log4net.Info("Status and action set method of AuditSeverityProvider called");
            var dict = _repos.GetCountOfNosAllowed(item);
            this.CountOfNosAllowed =  dict[item.AuditType];

            try
            {
                if (item.AuditType == "Internal")
                {
                    if (item.CountOfNos > CountOfNosAllowed)
                    {
                        item.ProjectExecutionStatus = "RED";
                        item.RemedialActionDuration = "Action to be taken in 2 weeks";
                    }
                    else if (item.CountOfNos == CountOfNosAllowed)
                    {
                        item.ProjectExecutionStatus = "AMBER";
                        item.RemedialActionDuration = "Action to be taken in 1 week";
                    }
                    else
                    {
                        item.ProjectExecutionStatus = "GREEN";
                        item.RemedialActionDuration = "No action needed";
                    }

                }
                else if (item.AuditType == "SOX")
                {
                    if (item.CountOfNos > CountOfNosAllowed)
                    {
                        item.ProjectExecutionStatus = "RED";
                        item.RemedialActionDuration = "Action to be taken in 1 week";
                    }
                    else if (item.CountOfNos == CountOfNosAllowed)
                    {
                        item.ProjectExecutionStatus = "AMBER";
                        item.RemedialActionDuration = "Action to be taken in 4 days";
                    }
                    else
                    {
                        item.ProjectExecutionStatus = "GREEN";
                        item.RemedialActionDuration = "No action needed";
                    }

                }
                else if (item.AuditType == "PayRoll")
                {
                    if (item.CountOfNos > CountOfNosAllowed)
                    {
                        item.ProjectExecutionStatus = "RED";
                        item.RemedialActionDuration = "Action to be taken in 3 weeks";
                    }
                    if (item.CountOfNos == CountOfNosAllowed)
                    {
                        item.ProjectExecutionStatus = "AMBER";
                        item.RemedialActionDuration = "Action to be taken in 1.5 week";
                    }
                    else
                    {
                        item.ProjectExecutionStatus = "GREEN";
                        item.RemedialActionDuration = "No action needed";
                    }

                }
                else if (item.AuditType == "Financial")
                {
                    if (item.CountOfNos > CountOfNosAllowed)
                    {
                        item.ProjectExecutionStatus = "RED";
                        item.RemedialActionDuration = "Action to be taken in 2 weeks";
                    }
                    if (item.CountOfNos == CountOfNosAllowed)
                    {
                        item.ProjectExecutionStatus = "AMBER";
                        item.RemedialActionDuration = "Action to be taken in 6 days";
                    }
                    else
                    {
                        item.ProjectExecutionStatus = "GREEN";
                        item.RemedialActionDuration = "No action needed";
                    }

                }
                else
                {
                    return null;
                }
                return item;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }


    }
}
