//
// ExposureNotifications C# bindings
//
// Authors:
//	Tomoaki Masuda <masuda@moonmile.net>
//
// Copyright (c) OpenCacao
//

using System;

using ObjCRuntime;
using Foundation;
using CoreFoundation;
using System.Threading.Tasks;

namespace ExposureNotifications {

	[Introduced (PlatformName.iOS, 13, 5)]
	[Native]
	public enum ENErrorCode : long {
		Ok = 0,
		Unknown = 1,
		BadParameter = 2,
		NotEntitled = 3,
		NotAuthorized = 4,
		Unsupported = 5,
		Invalidated = 6,
		BluetoothOff = 7,
		InsufficientStorage = 8,
		NotEnabled = 9,
		ApiMisuse = 10,
		Internal = 11,
		InsufficientMemory = 12,
		RateLimited = 13,
		Restricted = 14,
		BadFormat = 15,
		DataInaccessible = 16,
		TravelStatusNotAvailable = 17,
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	[Native]
	public enum ENAuthorizationStatus : long {
		Unknown = 0,
		Restricted = 1,
		NotAuthorized = 2,
		Authorized = 3,
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	[Native]
	public enum ENStatus : long {
		Unknown = 0,
		Active = 1,
		Disabled = 2,
		BluetoothOff = 3,
		Restricted = 4,
		Paused = 5,
		Unauthorized = 6,
	}

	[Introduced (PlatformName.iOS, 13, 7)]
	public enum ENDiagnosisReportType : uint {
		Unknown = 0,
		ConfirmedTest = 1,
		ConfirmedClinicalDiagnosis = 2,
		SelfReported = 3,
		Recursive = 4,
		Revoked = 5,
	}

	[Introduced (PlatformName.iOS, 13, 7)]
	public enum ENCalibrationConfidence : byte {
		Lowest = 0,
		Low = 1,
		Medium = 2,
		High = 3,
	}

	[Introduced (PlatformName.iOS, 13, 7)]
	public enum ENInfectiousness : uint {
		None = 0,
		Standard = 1,
		High = 2
	}

	public delegate void ENErrorHandler (NSError error);

	[Introduced (PlatformName.iOS, 13, 5)]
	// [BaseType (typeof (NSObject))]
	public class ENTemporaryExposureKey
	{ 

		[Export ("keyData", ArgumentSemantic.Copy)]
		public NSData KeyData { get; set; }

		[Export ("rollingPeriod")]
		public uint RollingPeriod { get; set; }

		[Export ("rollingStartNumber")]
		public uint RollingStartNumber { get; set; }

		[Export ("transmissionRiskLevel")]
		public byte TransmissionRiskLevel { get; set; }
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	// [BaseType (typeof (NSObject))]
	// [DisableDefaultCtor]
	// interface ENExposureDetectionSummary {
	public class ENExposureDetectionSummary { 
		[BindAs (typeof (int []))]
		[Export ("attenuationDurations", ArgumentSemantic.Copy)]
		public NSNumber[] AttenuationDurations { get; }

		[Export ("daysSinceLastExposure")]
		public nint DaysSinceLastExposure { get; }

		[Export ("matchedKeyCount")]
		public ulong MatchedKeyCount { get; }

		[Export ("maximumRiskScore")]
		public byte MaximumRiskScore { get; }

		[Introduced (PlatformName.iOS, 13, 6)]
		[Export ("maximumRiskScoreFullRange")]
		public double MaximumRiskScoreFullRange { get; }

		// [NullAllowed, Export ("metadata", ArgumentSemantic.Copy)]
		public NSDictionary Metadata { get; }

		[Introduced (PlatformName.iOS, 13, 6)]
		[Export ("riskScoreSumFullRange")]
		public double RiskScoreSumFullRange { get; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("daySummaries", ArgumentSemantic.Copy)]
		public ENExposureDaySummary[] DaySummaries { get; }
	}

	[Introduced (PlatformName.iOS, 13, 7)]
	// [BaseType (typeof (NSObject))]
	// [DisableDefaultCtor]
	public class ENExposureSummaryItem {

		[Export ("maximumScore")]
		public double MaximumScore { get; }

		[Export ("scoreSum")]
		public double ScoreSum { get; }

		[Export ("weightedDurationSum")]
		public double WeightedDurationSum { get; }
	}

	[Introduced (PlatformName.iOS, 13, 7)]
	// [BaseType (typeof (NSObject))]
	// [DisableDefaultCtor]
	public class ENExposureWindow {

		[Export ("calibrationConfidence", ArgumentSemantic.Assign)]
		public ENCalibrationConfidence CalibrationConfidence { get; }

		[Export ("date", ArgumentSemantic.Copy)]
		public NSDate Date { get; }

		[Export ("diagnosisReportType", ArgumentSemantic.Assign)]
		public ENDiagnosisReportType DiagnosisReportType { get; }

		[Export ("infectiousness", ArgumentSemantic.Assign)]
		public ENInfectiousness Infectiousness { get; }

		[Export ("scanInstances", ArgumentSemantic.Copy)]
		public ENScanInstance[] ScanInstances { get; }
	}

	[Introduced (PlatformName.iOS, 13, 7)]
	// [BaseType (typeof (NSObject))]
	// [DisableDefaultCtor]
	public class ENScanInstance {

		[Export ("minimumAttenuation")]
		public byte MinimumAttenuation { get; }

		[Export ("typicalAttenuation")]
		public byte TypicalAttenuation { get; }

		[Export ("secondsSinceLastScan")]
		public nint SecondsSinceLastScan { get; }
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	// [BaseType (typeof (NSObject))]
	public class ENExposureConfiguration {

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("immediateDurationWeight")]
		public double ImmediateDurationWeight { get; set; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("nearDurationWeight")]
		public double NearDurationWeight { get; set; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("mediumDurationWeight")]
		public double MediumDurationWeight { get; set; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("otherDurationWeight")]
		public double OtherDurationWeight { get; set; }

		[Introduced (PlatformName.iOS, 13, 7)]
		// [NullAllowed, Export ("infectiousnessForDaysSinceOnsetOfSymptoms", ArgumentSemantic.Copy)]
		public NSDictionary<NSNumber, NSNumber> InfectiousnessForDaysSinceOnsetOfSymptoms { get; set; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("infectiousnessStandardWeight")]
		public double InfectiousnessStandardWeight { get; set; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("infectiousnessHighWeight")]
		public double InfectiousnessHighWeight { get; set; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("reportTypeConfirmedTestWeight")]
		public double ReportTypeConfirmedTestWeight { get; set; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("reportTypeConfirmedClinicalDiagnosisWeight")]
		public double ReportTypeConfirmedClinicalDiagnosisWeight { get; set; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("reportTypeSelfReportedWeight")]
		public double ReportTypeSelfReportedWeight { get; set; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("reportTypeRecursiveWeight")]
		public double ReportTypeRecursiveWeight { get; set; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("reportTypeNoneMap", ArgumentSemantic.Assign)]
		public ENDiagnosisReportType ReportTypeNoneMap { get; set; }

		[Introduced (PlatformName.iOS, 13, 6)]
		[BindAs (typeof (int []))]
		[Export ("attenuationDurationThresholds", ArgumentSemantic.Copy)]
		public NSNumber[] AttenuationDurationThresholds { get; set; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("daysSinceLastExposureThreshold")]
		public nint DaysSinceLastExposureThreshold { get; set; }

		[Export ("minimumRiskScoreFullRange")]
		public double MinimumRiskScoreFullRange { get; set; }

		[BindAs (typeof (int []))]
		[Export ("attenuationLevelValues", ArgumentSemantic.Copy)]
		public int[] AttenuationLevelValues { get; set; }

		[Export ("attenuationWeight")]
		public double AttenuationWeight { get; set; }

		[BindAs (typeof (int []))]
		[Export ("daysSinceLastExposureLevelValues", ArgumentSemantic.Copy)]
		public int[] DaysSinceLastExposureLevelValues { get; set; }

		[Export ("daysSinceLastExposureWeight")]
		public double DaysSinceLastExposureWeight { get; set; }

		[BindAs (typeof (int []))]
		[Export ("durationLevelValues", ArgumentSemantic.Copy)]
		public int[] DurationLevelValues { get; set; }

		[Export ("durationWeight")]
		public double DurationWeight { get; set; }

		// [NullAllowed, Export ("metadata", ArgumentSemantic.Copy)]
		public NSDictionary Metadata { get; set; }

		[Export ("minimumRiskScore")]
		public byte MinimumRiskScore { get; set; }

		[BindAs (typeof (int []))]
		[Export ("transmissionRiskLevelValues", ArgumentSemantic.Copy)]
		public int[] TransmissionRiskLevelValues { get; set; }

		[Export ("transmissionRiskWeight")]
		public double TransmissionRiskWeight { get; set; }
	}

	[Introduced (PlatformName.iOS, 13, 7)]
	// [BaseType (typeof (NSObject))]
	// [DisableDefaultCtor]
	public class ENExposureDaySummary {

		[Export ("date", ArgumentSemantic.Copy)]
		public NSDate Date { get; }

		// [NullAllowed, Export ("confirmedTestSummary", ArgumentSemantic.Strong)]
		public ENExposureSummaryItem ConfirmedTestSummary { get; }

		// [NullAllowed, Export ("confirmedClinicalDiagnosisSummary", ArgumentSemantic.Strong)]
		public ENExposureSummaryItem ConfirmedClinicalDiagnosisSummary { get; }

		// [NullAllowed, Export ("recursiveSummary", ArgumentSemantic.Strong)]
		public ENExposureSummaryItem RecursiveSummary { get; }

		// [NullAllowed, Export ("selfReportedSummary", ArgumentSemantic.Strong)]
		public ENExposureSummaryItem SelfReportedSummary { get; }

		[Export ("daySummary", ArgumentSemantic.Strong)]
		public ENExposureSummaryItem DaySummary { get; }
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	// [BaseType (typeof (NSObject))]
	// [DisableDefaultCtor]
	public class ENExposureInfo {

		[BindAs (typeof (int []))]
		[Export ("attenuationDurations", ArgumentSemantic.Copy)]
		public int[] AttenuationDurations { get; }

		[Export ("attenuationValue")]
		public byte AttenuationValue { get; }

		[Export ("date", ArgumentSemantic.Copy)]
		public NSDate Date { get; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("daysSinceOnsetOfSymptoms")]
		public nint DaysSinceOnsetOfSymptoms { get; }

		[Introduced (PlatformName.iOS, 13, 7)]
		[Export ("diagnosisReportType", ArgumentSemantic.Assign)]
		public ENDiagnosisReportType DiagnosisReportType { get; }

		[Export ("duration")]
		public double Duration { get; }

		// [NullAllowed, Export ("metadata", ArgumentSemantic.Copy)]
		public NSDictionary Metadata { get; }

		[Export ("totalRiskScore")]
		public byte TotalRiskScore { get; }

		[Introduced (PlatformName.iOS, 13, 6)]
		[Export ("totalRiskScoreFullRange")]
		public double TotalRiskScoreFullRange { get; }

		[Export ("transmissionRiskLevel")]
		public byte TransmissionRiskLevel { get; }
	}

	public delegate void ENGetDiagnosisKeysHandler (ENTemporaryExposureKey [] keys = null, NSError error = null);
	public delegate void ENDetectExposuresHandler (ENExposureDetectionSummary summary = null, NSError error = null);
	public delegate void ENGetExposureInfoHandler ( ENExposureInfo [] exposures = null, NSError error = null);
	public delegate void ENGetExposureWindowsHandler ( ENExposureWindow [] exposureWindows = null, NSError error = null);
	public delegate void ENGetUserTraveledHandler (bool traveled, NSError error);

	[Introduced (PlatformName.iOS, 13, 5)]
	// [BaseType (typeof (NSObject))]
	public partial class ENManager {

		[Export ("dispatchQueue", ArgumentSemantic.Strong)]
		public DispatchQueue DispatchQueue { get; set; }

		[Export ("exposureNotificationStatus", ArgumentSemantic.Assign)]
		public ENStatus ExposureNotificationStatus { get; }

		// [NullAllowed, Export("invalidationHandler", ArgumentSemantic.Copy)]
		public Action InvalidationHandler { get; set; }

		//[Async]
		[Export ("activateWithCompletionHandler:")]
		public Task ActivateAsync (ENErrorHandler completionHandler = null) { return new Task(() => { }); }

		[Export ("invalidate")]
		public void Invalidate () { }

		[Introduced (PlatformName.iOS, 13, 7)]
		//[Async]
		[Export ("getUserTraveledWithCompletionHandler:")]
		public Task GetUserTraveledAsync (ENGetUserTraveledHandler completionHandler = null) { return new Task(() => { }); }

		//[Static]
		[Export ("authorizationStatus", ArgumentSemantic.Assign)]
		public static ENAuthorizationStatus AuthorizationStatus { get; }

		[Export ("exposureNotificationEnabled")]
		public bool ExposureNotificationEnabled { get; }

		//[Async]
		[Export ("setExposureNotificationEnabled:completionHandler:")]
		public Task SetExposureNotificationEnabledAsync (bool enabled) { return new Task(() => { }); }

		[Introduced (PlatformName.iOS, 13, 7)]
		//[Async]
		[Export ("detectExposuresWithConfiguration:completionHandler:")]
		public Task<NSProgress> DetectExposuresAsync (ENExposureConfiguration configuration, out ENDetectExposuresHandler completionHandler)
        {
			completionHandler = new ENDetectExposuresHandler((_, __) => { });
			return new Task<NSProgress>(() => new NSProgress());
        }

		//[Async]
		[Export ("detectExposuresWithConfiguration:diagnosisKeyURLs:completionHandler:")]
		public Task<ENExposureDetectionSummary> DetectExposuresAsync(ENExposureConfiguration configuration, NSUrl[] diagnosisKeyUrls, out NSProgress result)
		// public Task<NSProgress> DetectExposuresAsync (ENExposureConfiguration configuration, NSUrl [] diagnosisKeyUrls, out ENDetectExposuresHandler completionHandler)
        {
			result = new NSProgress();
			return new Task<ENExposureDetectionSummary>(() => new ENExposureDetectionSummary());
		}

		[Deprecated (PlatformName.iOS, 13, 6, message: "Use 'GetExposureWindows' instead.")]
		//[Async]
		[Export ("getExposureInfoFromSummary:userExplanation:completionHandler:")]
		public Task<ENExposureInfo[]> GetExposureInfoAsync(ENExposureDetectionSummary summary, string userExplanation, out NSProgress result)
		// public Task<NSProgress> GetExposureInfoAsync (ENExposureDetectionSummary summary, string userExplanation, out ENGetExposureInfoHandler completionHandler)
        {
			result = new NSProgress();
			return new Task<ENExposureInfo[]>(() => new ENExposureInfo[] { });
		}

		[Introduced(PlatformName.iOS, 13, 7)]
		//[Async]
		[Export("getExposureWindowsFromSummary:completionHandler:")]
		public Task<NSProgress> GetExposureWindowsAsync(ENExposureDetectionSummary summary, ENGetExposureWindowsHandler completionHandler)
		{
			return new Task<NSProgress>(() => new NSProgress());
		}

		//[Async]
		[Export ("getDiagnosisKeysWithCompletionHandler:")]
		public Task<ENTemporaryExposureKey[]> GetDiagnosisKeysAsync ()	{ 
			return new Task<ENTemporaryExposureKey[]>(() => new ENTemporaryExposureKey[] { }); }

		//[Async]
		[Export ("getTestDiagnosisKeysWithCompletionHandler:")]
		public Task GetTestDiagnosisKeysAsync (ENGetDiagnosisKeysHandler completionHandler) { return new Task(() => { }); }
	}
}
