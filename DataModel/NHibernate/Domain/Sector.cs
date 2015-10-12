using System.Collections;
using System.Collections.Generic;
using Dacha.DataModel.Domain;
using Dacha.PropertyMappings;

namespace Dacha.DataModel.NHibernate.Domain {
    
    public class Sector {
        public Sector()
        {
            Places = new List<Places>();
        }

        public virtual long Id { get; protected set; }

        [PropertyMapping("Name")]
        public virtual string Name { get; set; }
        public virtual IList<Places> Places { get; set; }
    }
}
