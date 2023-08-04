using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservisas_P3PL1_API
{
    class ClientCar
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public ushort Year { get; set; }
        public string Licence { get; set; }
        public string Fuel { get; set; }
        public decimal Engine { get; set; }
        public ushort Power { get; set; }
    }

    class Cars
    {
        public List<ClientCar> AllCars { get; set; }
    }
}
