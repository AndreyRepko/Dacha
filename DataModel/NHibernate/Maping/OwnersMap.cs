using Dacha.DataModel.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dacha.DataModel.NHibernate.Maping {
    
    
    public class OwnersMap : ClassMapping<Owner> {
        
        public OwnersMap() {
			Schema("public");
            Table("owners");
			Lazy(true);
			Id(x => x.Id, map => map.Generator(Generators.Assigned));
			Property(x => x.Comments);
			Property(x => x.Name);
        }
    }
}
