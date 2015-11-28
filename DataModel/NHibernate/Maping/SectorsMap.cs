using Dacha.DataModel.Domain;
using Dacha.DataModel.NHibernate.Domain;
using NHibernate.Id;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dacha.DataModel.NHibernate.Maping {
    
    
    public class SectorsMap : ClassMapping<Sector> {
        
        public SectorsMap() {
			Schema("public");
            Table("sectors");
			Lazy(true);
			Id(x => x.Id, map => map.Generator(Generators.SequenceHiLo, g => g.Params(new { sequence = "sectors_id_seq" })));
			Property(x => x.Name);
            List(x => x.Places,
                x =>
                {
                    x.Key(s =>
                    {
                        s.Column("sector_id");
                        s.NotNullable(true);
                    });
                    x.Index(idx => idx.Column("id"));
                    x.Cascade(Cascade.All);
                    x.Lazy(CollectionLazy.Lazy);
                    x.Fetch(CollectionFetchMode.Join);
                    x.Table("places");
                },
                x =>
                {
                    x.OneToMany();
                    x.Element(y=>y.Column("id"));
                });
        }
    }
}
