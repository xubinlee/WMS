using EDMX;
using Factory;
using IBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Z.EntityFramework.Plus;

namespace DAL
{
    public class UsersInfoDAL : IDALBase
    {
        public void Insert(WmsContext db, UsersInfo obj, List<Permission> pList, List<ButtonPermission> btnList)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                // 让使用nameof(T)标签的所有缓存过期
                QueryCacheManager.ExpireTag(nameof(UsersInfo));
                QueryCacheManager.ExpireTag(nameof(Permission));
                QueryCacheManager.ExpireTag(nameof(ButtonPermission));
                BaseDAL baseDAL = DALFty.Create<BaseDAL>();
                db.Set<UsersInfo>().Add(obj);
                if (pList.Count > 0)
                    baseDAL.AddByBulkCopy<Permission>(db, pList);
                if (btnList.Count > 0)
                    baseDAL.AddByBulkCopy<ButtonPermission>(db, btnList);
                ts.Complete();
            }
        }

        //public void Delete(WmsContext db, UsersInfo user)
        //{
        //    using (TransactionScope ts = new TransactionScope())
        //    {
        //        db.Entry(user).State = EntityState.Modified;
        //        db.SaveChanges();

        //        db.Set<Permission>().Where(o => o.UserID == user.ID).DeleteFromQuery();
        //        db.Set<ButtonPermission>().Where(o => o.UserID == user.ID).DeleteFromQuery();
        //        ts.Complete();
        //    }
        //}
    }
}
