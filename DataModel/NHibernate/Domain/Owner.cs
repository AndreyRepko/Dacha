using Dacha.PropertyMappings;

namespace Dacha.DataModel.NHibernate.Domain {
    
    public class Owner {
        public Owner() { }
        public virtual long Id { get; set; }

        [PropertyMapping("Name")]
        public virtual string Name { get; set; }

        [PropertyMapping("Comment")]
        public virtual string Comments { get; set; }
    }
}
