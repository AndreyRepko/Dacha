using System;
using System.Text;
using System.Collections.Generic;


namespace Dacha.DataModel.Domain {
    
    public class Phones {
        public virtual long Id { get; set; }
        public virtual string Phone { get; set; }
        public virtual long? OwnerId { get; set; }
    }
}
