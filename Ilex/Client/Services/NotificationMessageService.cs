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

        public void NotifyFromApiResponse(ApiResponse apiResponse)
        {
            _notificationService.Notify
                (
                    severity:apiResponse.Success?NotificationSeverity.Success:NotificationSeverity.Error,
                    summary:apiResponse.Success?apiResponse.Message:apiResponse.Error,
                    duration:4500
                );
        }
    }
}
