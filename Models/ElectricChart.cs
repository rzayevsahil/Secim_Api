using System.Collections.Generic;

namespace Secim_Api.Models
{
    public class ElectricChart
    {
        public ElectricChart()
        {
            Counts = new List<int>();
        }
        public string ElectricDate { get; set; }
        public List<int> Counts { get; set; }
    }
}
