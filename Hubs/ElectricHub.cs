using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Secim_Api.Hubs
{
    public class ElectricHub : Hub
    {
        private readonly ElectricService _electricService;

        public ElectricHub(ElectricService electricService)
        {
            _electricService = electricService;
        }

        public async Task GetElectricConsumeList()
        {
            await Clients.All.SendAsync("ReceiveElectricList", _electricService.GetElectricChartsList());
        }
    }
}
