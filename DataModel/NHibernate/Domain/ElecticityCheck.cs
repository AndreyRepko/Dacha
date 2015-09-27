using System;
using System.Text;
using System.Collections.Generic;


namespace Dacha.DataModel.Domain {
    
    public class ElecticityCheck {
        public virtual long Id { get; set; }
        public virtual DateTime Check { get; set; }
        public virtual int Value { get; set; }
        public virtual long ElectricityId { get; set; }
    }
}
