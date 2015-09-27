using System;
using System.Text;
using System.Collections.Generic;


namespace Dacha.DataModel.Domain {
    
    public class Owner {
        public Owner() { }
        public virtual long Id { get; set; }
        public virtual string Comments { get; set; }
        public virtual string Name { get; set; }
    }
}
