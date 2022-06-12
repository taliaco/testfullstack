
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using testfullstack.Models;
using testfullstack.Services.Interfaces;

namespace testfullstack.Services
{
	public class LocationService : ILocationService
	{
		private IConfiguration _configuration;
		private readonly ISession _session;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public LocationService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
		{
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
			_session = _httpContextAccessor.HttpContext.Session;
		}
		public async Task<OpenNotify> GetAsync()
		{
			try
			{
				var baseUrl = _configuration["OpenNotifyURL"];
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri($"{baseUrl}");


					HttpResponseMessage response = await client.GetAsync("");
					if (response.IsSuccessStatusCode)
					{
						var contentResult = await response.Content.ReadAsStringAsync();

						var dataResult = Newtonsoft.Json.JsonConvert.DeserializeObject<OpenNotify>(contentResult);
						if (dataResult != null)
						{
							return dataResult;
						}
					}
				}
			}
			catch (Exception e)
			{
			}
			return null;
		}
		public List<OpenNotify> GetSavedLocations()
		{
			List<OpenNotify> savedLocations;
			string value = _session.GetString("savedLocations");
			if (value == null)
			{
				savedLocations = new List<OpenNotify>();
			}
			else
			{
				savedLocations = JsonConvert.DeserializeObject<List<OpenNotify>>(value);
			}
			return savedLocations;
		}
		public List<OpenNotify> Save(OpenNotify location)
		{
			List<OpenNotify> savedLocations;
			savedLocations = GetSavedLocations();
			savedLocations.Add(location);
			_session.SetString("savedLocations", JsonConvert.SerializeObject(savedLocations)); //this is the original user
			return savedLocations;
		}


	}
}
