using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Android.Gms.Common.Api.Internal;
using Android.Gms.Common.Apis;
using Android.Gms.Nearby.ExposureNotification;
using Java.Interop;
using Java.IO;

namespace Xamarin.ExposureNotification
{
    class ExposureNotificationClientMock  
    {
        public Task<IList<ExposureInformation>> GetExposureInformationAsync(string token)
        {
            Xamarin.ExposureNotifications.LoggerService.StartMethod();
            var items = new List<ExposureInformation>();
            return new Task<IList<ExposureInformation>>(
                () => { return items; });
        }

        public Task<ExposureSummary> GetExposureSummaryAsync(string token)
        {
            Xamarin.ExposureNotifications.LoggerService.StartMethod();
            var item = new ExposureSummary.ExposureSummaryBuilder().Build();
            return new Task<ExposureSummary>(
                () => item);
        }
        public Task<IList<TemporaryExposureKey>> GetTemporaryExposureKeyHistoryAsync()
        {
            Xamarin.ExposureNotifications.LoggerService.StartMethod();
            var items = new List<TemporaryExposureKey>();
            return new Task<IList<TemporaryExposureKey>>(
                () => items);
        }

        public Task StartAsync()
        {
            Xamarin.ExposureNotifications.LoggerService.StartMethod();
            return new Task(() => { });
        }
        public Task StopAsync()
        {
            Xamarin.ExposureNotifications.LoggerService.StartMethod();
            return new Task(() => { });
        }
        public Task<bool> IsEnabledAsync()
        {
            Xamarin.ExposureNotifications.LoggerService.StartMethod();
            return new Task<bool>(() => true);
        }

        public Task ProvideDiagnosisKeysAsync(List<Java.IO.File> files, ExposureConfiguration config, string guid )
        {
            Xamarin.ExposureNotifications.LoggerService.StartMethod();
            return new Task<bool>(() => true);
        }
    }
}
