using DBML;
using IBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ReportDAL : IDALBase
    {
        public List<T> GetT<T>(DCC dcc, String filter) where T : class, new()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select * from {0}", typeof(T).Name);
            if (!string.IsNullOrEmpty(filter))
                sb.AppendFormat(" where {0}", filter);
            return dcc.ExecuteQuery<T>(sb.ToString()).ToList();
        }

        public IList GetList(DCC dcc,Type type, String filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select * from {0}", type.Name);
            if (!string.IsNullOrEmpty(filter))
                sb.AppendFormat(" where {0}", filter);
            var val = dcc.ExecuteQuery(type, sb.ToString());

            var listType = typeof(List<>);
            Type[] typeArgs = { type };
            var makeme = listType.MakeGenericType(typeArgs);
            IList list = (IList)Activator.CreateInstance(makeme, true);
            foreach (var item in val)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
