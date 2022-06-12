using System.Collections.Generic;
using System.Threading.Tasks;
using testfullstack.Models;
using testfullstack.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace testfullstack.Controllers
{
	[Route("api/OpenNotify")]
	public class OpenNotifyController : ControllerBase
	{


		private readonly ILocationService _locationService;
		
		public OpenNotifyController(ILocationService locationService)
		{
			_locationService = locationService;
			
		}
		[HttpGet]
		[Route("Get")]
		public async Task<IActionResult> Get()
		{
			var res = await _locationService.GetAsync();
			return Ok(res);


		}
		[HttpGet]
		[Route("GetSavedLocations")]
		public async Task<IActionResult> GetSavedLocations()
		{
			var res = _locationService.GetSavedLocations();
			return Ok(res);


		}
		[HttpPost]
		[Route("Save")]
		public IActionResult Save([FromBody] OpenNotify openNotify)
		{
			var res = _locationService.Save(openNotify);
			return Ok(res);
		}
	}
}
