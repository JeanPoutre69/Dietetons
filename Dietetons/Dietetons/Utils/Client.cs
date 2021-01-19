using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietetons.Utils
{
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public double? IMC 
        {
            get
            {
                if (null == Weight || null == Height)
                    return null;

                return Weight.Value / Math.Pow(Height.Value / 100.0, 2);
            }
        }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public DateTime BirthDate { get; set; }
        public Dictionary<DateTime, Ration> Rations { get; set; }
    }
}
