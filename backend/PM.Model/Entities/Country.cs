using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Model.Entities
{
    public class Country : Location
    {
        public virtual ICollection<Province> Provinces { get; set; }
        public virtual ClientInfo ClientInfo { get; set; }
    }
}
