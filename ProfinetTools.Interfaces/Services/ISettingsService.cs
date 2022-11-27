using System;
using System.Linq;
using System.Threading.Tasks;
using ProfinetTools.Interfaces.Models;
using SharpPcap;

namespace ProfinetTools.Interfaces.Services
{
	public interface ISettingsService
	{
		Task<SaveResult> FactoryReset(ILiveDevice adapter, string deviceName);
		Task<SaveResult> SendSettings(ILiveDevice adapter, string macAddress, Device newSettings);
		bool TryParseNetworkConfiguration(Device device);
	}
}