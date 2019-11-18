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
    public class InventoryBLL : IBLLBase
    {
        //public Dictionary<Guid, Decimal> GetGoodsTotalQty(Guid warehouseID)
        //{
        //    using (DCC dcc = DBMLFty.Dcc)
        //    {
        //        return DALFty.Create<InventoryDAL>().GetGoodsTotalQty(dcc, warehouseID);
        //    }
        //}

        public List<Inventory> GetInventory(Guid warehouseID, Guid goodsID)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<InventoryDAL>().GetInventory(dcc, warehouseID, goodsID);
            }
        }

        public List<Inventory> GetInventory(Guid warehouseID, Guid goodsID, int pcs)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<InventoryDAL>().GetInventory(dcc, warehouseID, goodsID, pcs);
            }
        }

        public List<VInventory> GetInventory()
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<InventoryDAL>().GetInventory(dcc);
            }
        }

        public List<VInventoryGroupByGoods> GetInventoryGroupByGoods()
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<InventoryDAL>().GetInventoryGroupByGoods(dcc);
            }
        }

        public List<VMaterialInventory> GetMaterialInventory()
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<InventoryDAL>().GetMaterialInventory(dcc);
            }
        }

        public List<VMaterialInventoryGroupByGoods> GetMaterialInventoryGroupByGoods()
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<InventoryDAL>().GetMaterialInventoryGroupByGoods(dcc);
            }
        }

        public List<VEMSInventoryGroupByGoods> GetEMSInventoryGroupByGoods()
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<InventoryDAL>().GetEMSInventoryGroupByGoods(dcc);
            }
        }

        public List<VFSMInventoryGroupByGoods> GetFSMInventoryGroupByGoods()
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<InventoryDAL>().GetFSMInventoryGroupByGoods(dcc);
            }
        }

        public List<VAccountBook> GetAccountBook()
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<InventoryDAL>().GetAccountBook(dcc);
            }
        }

        #region 盘点

        public List<Stocktaking> GetStocktaking()
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<InventoryDAL>().GetStocktaking(dcc);
            }
        }

        public List<ProfitAndLoss> GetProfitAndLoss()
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<InventoryDAL>().GetProfitAndLoss(dcc);
            }
        }

        public List<VStocktakingLog> GetVStocktakingLog(String billNo)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<InventoryDAL>().GetVStocktakingLog(dcc, billNo);
            }
        }

        public void Insert(List<Stocktaking> stList)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<InventoryDAL>().Insert(dcc, stList);
                dcc.Save();
            }
        }

        public void UpdateProfitAndLoss(List<ProfitAndLoss> list)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<InventoryDAL>().UpdateProfitAndLoss(dcc, list);
                dcc.Save();
            }
        }

        public void UpdateUnlistedGoods(List<UnlistedGoods> list)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<InventoryDAL>().UpdateUnlistedGoods(dcc, list);
                dcc.Save();
            }
        }

        public void SaveProfitAndLoss(SystemStatus systemStatus, StocktakingLogHd hd, List<StocktakingLogDtl> dtlList, List<ProfitAndLoss> insertList, List<ProfitAndLoss> updateList)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<InventoryDAL>().SaveProfitAndLoss(dcc, systemStatus, hd, dtlList, insertList, updateList);
                dcc.Save();
            }
        }

        public void FinishCheck(SystemStatus systemStatus, StocktakingLogHd hd, List<ProfitAndLossLog> dtlList, List<UnlistedGoods> uglList)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<InventoryDAL>().FinishCheck(dcc, systemStatus, hd, dtlList, uglList);
                dcc.Save();
            }
        }

        public void FinishUnlistedGoods(List<UnlistedGoodsLog> dtlList)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<InventoryDAL>().FinishUnlistedGoods(dcc, dtlList);
                dcc.Save();
            }
        }

        public void StocktakingUpdate(Guid warehouseID, int goodsBigType, Guid? supplierID, List<Inventory> list, List<AccountBook> abList)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<InventoryDAL>().StocktakingUpdate(dcc, warehouseID, goodsBigType, supplierID, list, abList);
                dcc.Save();
            }
        }

        #endregion

        public void Insert(Object hd, IList dtl, List<Inventory> ityList, List<AccountBook> abList, List<Alert> delList, List<Alert> alertList)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<InventoryDAL>().Insert(dcc, hd, dtl, ityList, abList, delList, alertList);
                dcc.Save();
            }
        }

        public void CancelAudit(Object hd, List<Inventory> ityList, List<AccountBook> abList)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<InventoryDAL>().CancelAudit(dcc, hd, ityList, abList);
                dcc.Save();
            }
        }

        public void Import(List<Stocktaking> insertList, List<Stocktaking> updateList)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<InventoryDAL>().Import(dcc, insertList, updateList);
                dcc.Save();
            }
        }
    }
}
