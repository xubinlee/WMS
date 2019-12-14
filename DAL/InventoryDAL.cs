using EDMX;
using Factory;
using IBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Z.EntityFramework.Plus;

namespace DAL
{
    public class InventoryDAL : IDALBase
    {

        #region 盘点

        public void SaveProfitAndLoss(DbContext db, SystemStatus systemStatus, StocktakingLogHd hd, List<StocktakingLogDtl> dtlList, List<ProfitAndLoss> insertList, List<ProfitAndLoss> updateList)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                // 让使用nameof(T)标签的所有缓存过期
                QueryCacheManager.ExpireTag(nameof(StocktakingLogHd));
                QueryCacheManager.ExpireTag(nameof(StocktakingLogDtl));
                QueryCacheManager.ExpireTag(nameof(ProfitAndLoss));
                QueryCacheManager.ExpireTag(nameof(Stocktaking));
                BaseDAL baseDAL = DALFty.Create<BaseDAL>();
                // 更新状态表
                db.Entry(systemStatus).State = EntityState.Modified;
                db.SaveChanges();
                // 添加盘点日志
                db.Set<StocktakingLogHd>().Add(hd);
                if (dtlList.Count > 0)
                    baseDAL.AddByBulkCopy<StocktakingLogDtl>(db, dtlList);
                // 保存盘点差异表
                if (insertList.Count > 0)
                    baseDAL.AddByBulkCopy<ProfitAndLoss>(db, insertList);
                if (updateList.Count > 0)
                {
                    db.Set<ProfitAndLoss>().BulkUpdate(updateList);
                }
                // 删除盘点导入表
                db.Set<Stocktaking>().DeleteFromQuery();
                ts.Complete();
            }
        }

        public void FinishCheck(DbContext db, SystemStatus systemStatus, StocktakingLogHd hd, List<ProfitAndLossLog> dtlList, List<UnlistedGoods> uglList)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                // 让使用nameof(T)标签的所有缓存过期
                QueryCacheManager.ExpireTag(nameof(SystemStatus));
                QueryCacheManager.ExpireTag(nameof(StocktakingLogHd));
                QueryCacheManager.ExpireTag(nameof(ProfitAndLossLog));
                QueryCacheManager.ExpireTag(nameof(UnlistedGoods));
                BaseDAL baseDAL = DALFty.Create<BaseDAL>();
                // 更新状态表
                db.Entry(systemStatus).State = EntityState.Modified;
                db.SaveChanges();
                // 添加盘点日志
                db.Set<StocktakingLogHd>().Add(hd);
                // 保存盘点差异日志表
                if (dtlList.Count > 0)
                    baseDAL.AddByBulkCopy<ProfitAndLossLog>(db, dtlList);
                // 保存未上架商品确认单
                if (uglList.Count > 0)
                    baseDAL.AddByBulkCopy<UnlistedGoods>(db, uglList);
                // 删除盘点差异表
                db.Set<ProfitAndLoss>().DeleteFromQuery();
                ts.Complete();
            }
        }

        public void FinishUnlistedGoods(DbContext db, List<UnlistedGoodsLog> dtlList)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                // 让使用nameof(T)标签的所有缓存过期
                QueryCacheManager.ExpireTag(nameof(UnlistedGoodsLog));
                // 保存未上架商品确认单日志表
                if (dtlList.Count > 0)
                    DALFty.Create<BaseDAL>().AddByBulkCopy<UnlistedGoodsLog>(db, dtlList);
                // 删除未上架商品确认单
                db.Set<UnlistedGoods>().DeleteFromQuery();
                ts.Complete();
            }
        }

        #endregion
    }
}
