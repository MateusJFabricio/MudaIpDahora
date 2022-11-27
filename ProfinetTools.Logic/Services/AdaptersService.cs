using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ProfinetTools.Interfaces.Services;
using SharpPcap;

namespace ProfinetTools.Logic.Services
{
	public class AdaptersService : IAdaptersService
	{
		private readonly BehaviorSubject<ILiveDevice> selectedAdapterSubject = new BehaviorSubject<ILiveDevice>(null);
		public List<ILiveDevice> GetAdapters()
		{
			var devices = new List<ILiveDevice>();

			try
			{
				foreach (ILiveDevice dev in CaptureDeviceList.Instance)
					devices.Add(dev);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return devices;
		}

		public void SelectAdapter(ILiveDevice adapter)
		{
			selectedAdapterSubject.OnNext(adapter);
		}

		public IObservable<ILiveDevice> SelectedAdapter => selectedAdapterSubject.AsObservable();

		IObservable<ILiveDevice> IAdaptersService.SelectedAdapter => throw new NotImplementedException();
	}
}