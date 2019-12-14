using DAL;
using EDMX;
using Factory;
using IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PermissionBLL : IBLLBase
    {
        public void Update(List<Permission> opList, List<ButtonPermission> btnList)
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                DALFty.Create<PermissionDAL>().Update(db, opList, btnList);
            }
        }
    }
}
