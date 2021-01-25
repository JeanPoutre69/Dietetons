using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietetons.Utils
{
    public class Ingredient
    {
        public string Name { get; set; }
        public Composition Composition { get; set; }

        public Ingredient(string name, Composition composition)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);
            if (null == composition)
                throw new ArgumentNullException(name);

            Name = name;
            Composition = composition;
        }
    }
}
