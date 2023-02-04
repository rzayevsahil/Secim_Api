using Microsoft.AspNetCore.SignalR;
using Secim_Api.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Secim_Api.Hubs
{
    public class ElectricService
    {
        private readonly Context _context;
        private readonly IHubContext<ElectricHub> _hubContext;

        public ElectricService(Context context, IHubContext<ElectricHub> hubContext)
        {
            _hubContext = hubContext;
            _context = context;
        }

        public IQueryable<Electric> GetList()
        {
            return _context.Electrics.AsQueryable();
        }

        public async Task SaveElectric(Electric electric)
        {
            await _context.Electrics.AddAsync(electric);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveElectricList", "Data");
        }
    }
}
