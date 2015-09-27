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
    }
}
