using System;

namespace Secim_Api.Models
{
    public enum CityList
    {
        Ankara = 1,
        Bursa = 2,
        Konya = 3,
        Bakü = 4,
        İzmir = 5
    }

    public class Electric
    {
        public int ElectricID { get; set; }
        public CityList City { get; set; }
        public int Count { get; set; }
        public DateTime ElectricDate { get; set; }
    }
}
