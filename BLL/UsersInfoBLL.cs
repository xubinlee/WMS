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
    public class UsersInfoBLL : IBLLBase
    {
        public void Insert(UsersInfo obj, List<Permission> pList, List<ButtonPermission> btnList)
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                DALFty.Create<UsersInfoDAL>().Insert(db, obj, pList, btnList);
            }
        }
    }
}
