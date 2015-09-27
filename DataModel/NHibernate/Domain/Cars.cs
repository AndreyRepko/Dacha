using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Dialect.Schema;

namespace Dacha.DataModel.Domain {
    
    public class Cars {
        public virtual long Id { get; set; }
        public virtual DateTime? LeaveTime { get; set; }
        public virtual DateTime? EnterTime { get; set; }
        public virtual long? OwnerId { get; set; }
        public virtual string Number { get; set; }
    }
}
