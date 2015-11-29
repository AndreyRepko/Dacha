using System.Data;
using NHibernate.Dialect;

namespace Dacha.DataModel.NHibernate
{
    public class PostgreSQL94Dialect : PostgreSQL82Dialect
    {
        public PostgreSQL94Dialect()
        {
            RegisterColumnType(DbType.String, "text");
        }

        public override string QuerySequencesString
        {
            get
            {
                return "SELECT  c.relname FROM pg_catalog.pg_class c WHERE c.relkind IN ('S','') AND pg_catalog.pg_table_is_visible(c.oid)";
            }
        }

    }
}
