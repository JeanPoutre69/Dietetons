using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietetons.ViewModel
{
    class FindClientVM
    {
        public List<JeTestItem> JeTest { get; protected set; }

        public FindClientVM()
        {
            JeTest = new List<JeTestItem>();
            JeTest.Add(new JeTestItem() { Text = "Toto" });
            JeTest.Add(new JeTestItem() { Text = "Tata" });

        }

    }
    class JeTestItem
    {
        public string Text { get; set; }
    }
}
