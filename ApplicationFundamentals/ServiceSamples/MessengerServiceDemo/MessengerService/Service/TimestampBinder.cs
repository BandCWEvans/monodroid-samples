using Android.OS;
using MessengerCore;

namespace MessengerService
{
	public class TimestampBinder : Binder, IGetTimestamp
	{
		public TimestampBinder(TimestampService service)
		{
			this.Service = service;
		}

		public TimestampService Service { get; private set; }

		public string GetFormattedTimestamp()
		{
			return Service?.GetFormattedTimestamp();
		}
	}	
}
