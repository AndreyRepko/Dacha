using Dacha.DataModel.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dacha.DataModel.NHibernate.Maping {
    
    
    public class CarsMap : ClassMapping<Cars> {
        
        public CarsMap() {
			Schema("public");
			Lazy(true);
			Id(x => x.Id, map => map.Generator(Generators.Assigned));
			Property(x => x.LeaveTime, map => map.Column("leave_time"));
			Property(x => x.EnterTime, map => map.Column("enter_time"));
			Property(x => x.OwnerId, map => map.Column("owner_id"));
			Property(x => x.Number, map =>map.Column("number"));
        }
    }
}
