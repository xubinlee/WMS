using DBML;
using IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SystemInfoDAL: IDALBase
    {
        public List<SystemInfo> GetSystemInfo(DCC dcc)
        {
            return dcc.SystemInfo.ToList();
        }

        public List<SystemStatus> GetSystemStatus(DCC dcc)
        {
            return dcc.SystemStatus.ToList();
        }

        public void Insert(DCC dcc, SystemStatus obj)
        {
            dcc.SystemStatus.InsertOnSubmit(obj);
        }

        public void Update(DCC dcc, SystemStatus obj)
        {
            dcc.SystemStatus.Attach(obj);
            dcc.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, obj);
        }
    }
}
