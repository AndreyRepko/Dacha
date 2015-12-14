using System;

namespace Dacha.DataModel.NHibernate.Domain {
    
    public class Cars {
        public virtual long Id { get; set; }
        public virtual long? OwnerId { get; set; }
        public virtual string Number { get; set; }
    }
}
