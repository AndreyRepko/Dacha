using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                /*return @"SELECT " +
                       " n.nspname || '.' || c.relname as \"Name\"" +
                       " FROM pg_catalog.pg_class c" +
                       " JOIN pg_catalog.pg_roles r ON r.oid = c.relowner" +
                       " LEFT JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace" +
                       " WHERE c.relkind IN ('S', '')" +
                       " AND n.nspname = 'public'" +
                       " AND pg_catalog.pg_table_is_visible(c.oid)";*/
                return "SELECT  c.relname FROM pg_catalog.pg_class c WHERE c.relkind IN ('S','') AND pg_catalog.pg_table_is_visible(c.oid)";
            }
        }

    }
}
