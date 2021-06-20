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
            _notificationService.Notify
                (
                    severity:success?NotificationSeverity.Success:NotificationSeverity.Error,
                    summary:text,
                    duration:4500
                );
        }

      
    }
}
