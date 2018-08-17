using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniFirst_BL.Enums;

namespace UniFirst_BL.Entities
{
    public class Location
    {
       public int LocationId { get; set; }
       public LocationType LocationType { get; set; }
    }
}
