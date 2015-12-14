using Dacha.DataModel.Domain;
using Dacha.DataModel.NHibernate.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dacha.DataModel.NHibernate.Maping {
    
    
    public class PhonesMap : ClassMapping<Phones> {
        
        public PhonesMap() {
			Schema("public");
			Lazy(true);
			Id(x => x.Id, map => map.Generator(Generators.Assigned));
			Property(x => x.Phone);
			Property(x => x.OwnerId, map => map.Column("owner_id"));
        }
    }
}
