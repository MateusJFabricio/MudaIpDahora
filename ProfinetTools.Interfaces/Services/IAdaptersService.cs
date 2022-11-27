using System;
using System.Collections.Generic;
using System.Linq;
using SharpPcap;

namespace ProfinetTools.Interfaces.Services
{
	public interface IAdaptersService
	{
		List<ILiveDevice> GetAdapters();

		IObservable<ILiveDevice> SelectedAdapter { get; }
		void SelectAdapter(ILiveDevice adapter);
	}
}