using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using Secim_Api.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            await _hubContext.Clients.All.SendAsync("ReceiveElectricList", GetElectricChartsList());
        }

        public List<ElectricChart> GetElectricChartsList()
        {
            List<ElectricChart> electricCharts = new List<ElectricChart>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText =
                    "Select Tarih,[1],[2],[3],[4],[5] From (select[City],[Count],Cast([ElectricDate] as date) as Tarih From Electrics) as electricT Pivot (Sum(Count) For City in ([1],[2],[3],[4],[5])) as ptable order by Tarih Asc";
                command.CommandType = System.Data.CommandType.Text;
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ElectricChart electricChart = new ElectricChart();
                        electricChart.ElectricDate = reader.GetDateTime(0).ToShortDateString();
                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            if (System.DBNull.Value.Equals(reader[x]))
                            {
                                electricChart.Counts.Add(0);
                            }
                            else
                            {
                                electricChart.Counts.Add(reader.GetInt32(x));
                            }
                        });
                        electricCharts.Add(electricChart);
                    }
                }
                _context.Database.CloseConnection();
                return electricCharts;
            }
        }
    }
}
