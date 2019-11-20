using DAL;
using DBML;
using Factory;
using IBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportBLL : IBLLBase
    {
        public List<T> GetT<T>(String filter) where T : class, new()
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<ReportDAL>().GetT<T>(dcc, filter);
            }
        }

        public IList GetList(Type type, String filter)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<ReportDAL>().GetList(dcc, type, filter);
            }

        }
    }
}
