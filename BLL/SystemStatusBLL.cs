using DAL;
using DBML;
using Factory;
using IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SystemStatusBLL : IBLLBase
    {
        public List<SystemStatus> GetSystemStatus()
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<SystemStatusDAL>().GetSystemStatus(dcc);
            }
        }

        public void Insert(SystemStatus obj)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<SystemStatusDAL>().Insert(dcc, obj);
                dcc.Save();
            }
        }

        public void Update(SystemStatus obj)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<SystemStatusDAL>().Update(dcc, obj);
                dcc.Save();
            }
        }
    }
}
