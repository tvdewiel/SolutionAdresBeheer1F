using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetNamesBL.Model
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private Dictionary<int, Municipality> Municipalities = new();
    }
}
