using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using MessengerCore;

namespace MessengerService
{
	[Service(Name = "com.xamarin.TimestampService", 
	         Exported=true, 
	         Permission="com.xamarin.xample.messengerservice.TimestampService",
	         Process="com.xamarin.xample.messengerservice.timestampservice_process")]
	public class TimestampService : Service, IGetTimestamp
	{
		static readonly string TAG = typeof(TimestampService).FullName;
		IGetTimestamp timestamper;
		Messenger msg ;

		public IBinder Binder { get; private set; }

		public override void OnCreate()
		{
			base.OnCreate();
			Log.Debug(TAG, "OnCreate");
			timestamper = new UtcTimestamper();
			msg = new Messenger(new TimestampRequestHandler(this));

			Log.Info(TAG, $"TimestampService is running in process id {Android.OS.Process.MyPid()}.");
		}

		public override IBinder OnBind(Intent intent)
		{
			Log.Debug(TAG, "OnBind");
			return msg.Binder;
		}

		public override void OnDestroy()
		{
			Log.Debug(TAG, "OnDestroy");
			msg.Dispose();
			Binder = null;
			timestamper = null;
			base.OnDestroy();
		}

		/// <summary>
		/// This method will return a formatted timestamp to the client.
		/// </summary>
		/// <returns>A string that details what time the service started and how long it has been running.</returns>
		public string GetFormattedTimestamp()
		{
			return timestamper?.GetFormattedTimestamp();
		}
	}
}
