using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietetons.Utils
{
    public class Ration
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<double, Ingredient> Summary { get; set; }
    }
}
