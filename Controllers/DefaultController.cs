using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Secim_Api.Hubs;
using Secim_Api.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Secim_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly ElectricService _electricService;

        public DefaultController(ElectricService electricService)
        {
            _electricService = electricService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveElectric(Electric electric)
        {
            await _electricService.SaveElectric(electric);
            //IQueryable<Electric> electricList = _electricService.GetList();
            return Ok(_electricService.GetElectricChartsList());
        }

        [HttpGet]
        public IActionResult SendData()
        {
            Random rnd = new Random();
            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {
                foreach (CityList item in Enum.GetValues(typeof(CityList)))
                {
                    var newElectric = new Electric
                    {
                        City = item,
                        Count = rnd.Next(100, 1000),
                        ElectricDate = DateTime.Now.AddDays(x)
                    };
                    _electricService.SaveElectric(newElectric).Wait();
                    System.Threading.Thread.Sleep(1000);
                }
            });
            return Ok("Elektrik verileri başarıyla oluştu");
        }
    }
}
