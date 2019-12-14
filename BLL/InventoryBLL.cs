using DAL;
using EDMX;
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
    public class InventoryBLL : IBLLBase
    {
        #region 盘点
        
        public void SaveProfitAndLoss(SystemStatus systemStatus, StocktakingLogHd hd, List<StocktakingLogDtl> dtlList, List<ProfitAndLoss> insertList, List<ProfitAndLoss> updateList)
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                DALFty.Create<InventoryDAL>().SaveProfitAndLoss(db, systemStatus, hd, dtlList, insertList, updateList);
            }
        }

        public void FinishCheck(SystemStatus systemStatus, StocktakingLogHd hd, List<ProfitAndLossLog> dtlList, List<UnlistedGoods> uglList)
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                DALFty.Create<InventoryDAL>().FinishCheck(db, systemStatus, hd, dtlList, uglList);
            }
        }

        public void FinishUnlistedGoods(List<UnlistedGoodsLog> dtlList)
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                DALFty.Create<InventoryDAL>().FinishUnlistedGoods(db, dtlList);
            }
        }

        #endregion
    }
}
