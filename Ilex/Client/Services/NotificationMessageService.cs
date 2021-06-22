using Ilex.Shared.Helpers;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ilex.Client.Services
{
    public class NotificationMessageService
    {
        private readonly NotificationService _notificationService;

        public NotificationMessageService(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void NotifyFromApiResponse(bool success, string text)
        {
            //_notificationService.Notify
            //    (
            //        severity:success?NotificationSeverity.Success:NotificationSeverity.Error,
            //        summary:text,
            //        duration:4500
            //    );

            _notificationService.Notify
               (
                   new NotificationMessage
                   {
                       Severity=success?NotificationSeverity.Success:NotificationSeverity.Error,
                    Summary=text,
                    Duration=4500,
                    Style= "position: absolute; right: 20px; bottom: 20px;"
                   }
               );
        }

      
    }
}
