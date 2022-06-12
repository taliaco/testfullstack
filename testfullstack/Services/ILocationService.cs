using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using testfullstack.Models;

namespace testfullstack.Services.Interfaces
{
	public interface ILocationService
	{
		Task<OpenNotify> GetAsync();
		List<OpenNotify> GetSavedLocations();
		List<OpenNotify> Save(OpenNotify location);
	}
}
