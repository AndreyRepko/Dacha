using System;
using System.Text;
using System.Collections.Generic;
using Dacha.DataModel.NHibernate.Domain;

namespace Dacha.DataModel.Domain {
    
    public class Places {
        public virtual long Id { get; protected set; }
        public virtual double? Square { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual string PlaceNumber { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual long? ElectricityId { get; set; }
    }
}
