using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfinetTools.Interfaces.Models;
using SharpPcap;

namespace ProfinetTools.Interfaces.Services
{
	public interface ILiveDeviceService
	{
		Task<List<Device>> GetDevices(ILiveDevice adapter, TimeSpan timeout);
		void SelectDevice(Device device);
		IObservable<Device> SelectedDevice { get; }
	}
}