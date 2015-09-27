using System;
using System.Text;
using System.Collections.Generic;


namespace Dacha.DataModel.Domain {
    
    public class Electricity {
        public Electricity() { }
        public virtual long Id { get; set; }
        public virtual bool Electirc { get; set; }
        public virtual bool Outdoor { get; set; }
        public virtual string Comment { get; set; }
    }
}
