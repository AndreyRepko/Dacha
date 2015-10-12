using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dacha.PropertyMappings
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class PropertyMapping : System.Attribute
    {
        public string DisplayName;

        public PropertyMapping(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
