using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AndroidTask = Android.Gms.Tasks.Task;

namespace Android.Gms.Nearby.ExposureNotification
{
	public partial interface IExposureNotificationClient
	{
		public Task StartAsync()
			=> new Task(() => { });

		public Task StopAsync()
			=> new Task(() => { });

		[Obsolete("Use GetExposureWindowsAsync() instead.")]
		public Task<IList<ExposureInformation>> GetExposureInformationAsync(string token)
			=> new Task<IList<ExposureInformation>>(() => new List<ExposureInformation>());

		[Obsolete("Use GetDailySummariesAsync(DailySummariesConfig) instead.")]
		public Task<ExposureSummary> GetExposureSummaryAsync(string token)
			=> new Task<ExposureSummary>(() => new ExposureSummary.ExposureSummaryBuilder().Build());

		public Task<bool> IsEnabledAsync()
			=> new Task<bool>(() => true ); // 常に有効

		[Obsolete("Use GetDailySummariesAsync(IList<Java.IO.File>) instead.")]
		public Task ProvideDiagnosisKeysAsync(IList<Java.IO.File> files, ExposureConfiguration config, string token)
			=> new Task(() => { });

		[Obsolete("Use ProvideDiagnosisKeysAsync(DiagnosisKeyFileProvider) instead.")]
		public Task ProvideDiagnosisKeysAsync(IList<Java.IO.File> files)
			=> new Task(() => { });

		public Task ProvideDiagnosisKeysAsync(DiagnosisKeyFileProvider provider)
			=> new Task(() => { });

		public Task<IList<TemporaryExposureKey>> GetTemporaryExposureKeyHistoryAsync()
			=> new Task<IList<TemporaryExposureKey>>(() => new List<TemporaryExposureKey>());

		public Task<long> GetVersionAsync()
			=> new Task<long>(() => 2);     // EN v2  

		public Task<int> GetCalibrationConfidenceAsync()
			=> new Task<int>(() => 0);      // 0 を返しておく

		public Task<IList<DailySummary>> GetDailySummariesAsync(DailySummariesConfig config)
			=> new Task<IList<DailySummary>>(() => new List<DailySummary>());

		public Task<DiagnosisKeysDataMapping> GetDiagnosisKeysDataMappingAsync()
			=> new Task<DiagnosisKeysDataMapping>(() => new DiagnosisKeysDataMapping.DiagnosisKeysDataMappingBuilder().Build());

		public Task SetDiagnosisKeysDataMappingAsync(DiagnosisKeysDataMapping mapping)
			=> new Task(() => { });

		public Task<IList<ExposureWindow>> GetExposureWindowsAsync()
			=> new Task<IList<ExposureWindow>>(() => new List<ExposureWindow>());

		[Obsolete("Use GetExposureWindowsAsync() instead.")]
		public Task<IList<ExposureWindow>> GetExposureWindowsAsync(string token)
			=> new Task<IList<ExposureWindow>>(() => new List<ExposureWindow>());

		public  Task<PackageConfiguration> GetPackageConfigurationAsync()
			=> new Task<PackageConfiguration>(() => new PackageConfiguration.PackageConfigurationBuilder().Build());

		public Task<ExposureNotificationStatus> GetStatusAsync()
			=> new Task<ExposureNotificationStatus>(() => ExposureNotificationStatus.Activated);    // 仮に Activated を返す
	}

	internal static class GoogleTaskExtensions
	{
		public static Task CastTask(this AndroidTask androidTask)
		{
			var tcs = new TaskCompletionSource<bool>();

			androidTask.AddOnCompleteListener(new MyCompleteListener(
				t =>
				{
					if (t.Exception == null)
						tcs.TrySetResult(false);
					else
						tcs.TrySetException(t.Exception);
				}
			));

			return tcs.Task;
		}

		public static Task<TResult> CastTask<TResult>(this AndroidTask androidTask)
			where TResult : Java.Lang.Object
		{
			var tcs = new TaskCompletionSource<TResult>();

			androidTask.AddOnCompleteListener(new MyCompleteListener(
				t =>
				{
					if (t.Exception == null)
						tcs.TrySetResult(t.Result.JavaCast<TResult>());
					else
						tcs.TrySetException(t.Exception);
				}));

			return tcs.Task;
		}

		class MyCompleteListener : Java.Lang.Object, Android.Gms.Tasks.IOnCompleteListener
		{
			public MyCompleteListener(Action<AndroidTask> onComplete)
				=> OnCompleteHandler = onComplete;

			public Action<AndroidTask> OnCompleteHandler { get; }

			public void OnComplete(AndroidTask task)
				=> OnCompleteHandler?.Invoke(task);
		}
	}
}
