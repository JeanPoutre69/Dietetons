using Dietetons.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietetons.BusinessClasses
{
    public class Ration
    {
        public Dictionary<double, Ingredient> GramsPerIngredient { get; set; } = new Dictionary<double, Ingredient>();
    }
}
