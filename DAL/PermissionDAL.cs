using EDMX;
using IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Z.EntityFramework.Plus;

namespace DAL
{
    public class PermissionDAL : IDALBase
    {
        public void Update(WmsContext db, List<Permission> opList, List<ButtonPermission> btnList)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                // 让使用nameof(T)标签的所有缓存过期
                QueryCacheManager.ExpireTag(nameof(Permission));
                QueryCacheManager.ExpireTag(nameof(ButtonPermission));
                db.Set<Permission>().BulkUpdate(opList);
                db.Set<ButtonPermission>().BulkUpdate(btnList);
                ts.Complete();
            }
        }
        
        //public void Update(DCC dcc,Guid userID, List<Permission> insertList)
        //{
        //    using (TransactionScope ts = new TransactionScope())
        //    {
        //        if (insertList.Count > 0)
        //        {
        //            // 先删除原来的数据，再新增（因为可能MainMenu有变动）
        //            var del = dcc.Permission.Where(o => o.UserID.Equals(userID));
        //            dcc.Permission.DeleteAllOnSubmit(del);
        //            dcc.Permission.InsertAllOnSubmit(insertList);
        //        }
        //        dcc.SubmitChanges();
        //        ts.Complete();
        //    }
        //}
    }
}
