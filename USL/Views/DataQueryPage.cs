using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using System.Collections;
using EDMX;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using IBase;
using Utility;
using BLL;
using Factory;
using CommonLibrary;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraGrid.Columns;
using System.Data.OleDb;
using System.IO;
using System.Data.Linq;
using DevExpress.XtraGrid.Views.Grid;
using System.Reflection;
using static Utility.EnumHelper;
using DevExpress.Utils;
using System.Threading;
using Utility.Interceptor;

namespace USL
{
    public partial class DataQueryPage : DevExpress.XtraEditors.XtraUserControl, IItemDetail, IExtensions
    {
        private static ClientFactory clientFactory = LoggerInterceptor.CreateProxy<ClientFactory>();
        public MainMenu mainMenu;
        IList dataSource;
        Object currentObj = null;
        PageGroup pageGroupCore;
        Dictionary<String, int> itemDetailButtonList; //子菜单按钮项
        //Dictionary<String, ItemDetailPage> itemDetailPageList;
        WindowsUIView view = null;
        //List<VUsersInfo> displayUsers;
        List<TypesList> types;   //类型列表
        List<Warehouse> warehouseList;

        //[ServiceDependency]
        //public WorkItem RootWorkItem { get; set; }
        public DataQueryPage(MainMenu menu, IList list, PageGroup child, Dictionary<String, int> childButtonList)
        {
            InitializeComponent();
            pageGroupCore = child;
            itemDetailButtonList = childButtonList;
            gridView.ShowFindPanel();
            gridView.IndicatorWidth = 40;
            mainMenu = menu;
            dataSource = list;
            //itemDetailPageList = new Dictionary<string, ItemDetailPage>();
            types = BLLFty.Create<BaseBLL>().GetListByNoTracking<TypesList>(null);
            warehouseList = BLLFty.Create<BaseBLL>().GetListByNoTracking<Warehouse>(null); 
            // 盘点差异表/未上架商品确认单可编辑
            if (menu.Name.Equals(MainMenuConstants.ProfitAndLoss) ||
                menu.Name.Equals(MainMenuConstants.UnlistedGoods)) {
                gridView.OptionsBehavior.Editable = true;
                gridView.OptionsBehavior.ReadOnly = false;
            }
        }

        void GetItemDetailPage()
        {
            BaseContentContainer documentContainer = pageGroupCore.Parent as BaseContentContainer;
            if (documentContainer != null)
            {
                view = documentContainer.Manager.View as WindowsUIView;
                //if (view != null)
                //{

                //    foreach (BaseDocument doc in view.Documents)
                //    {
                //        if (doc.Control is ItemDetailPage)
                //        {
                //            ItemDetailPage itemDetailPage = (ItemDetailPage)doc.Control;
                //            //if (itemDetailPage.menu==mainMenu)
                //            //    break;
                //            itemDetailPageList.Add(itemDetailPage.menu.Name, itemDetailPage);
                //        }

                //    }
                //}
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //displayUsers = BLLFty.Create<UsersInfoBLL>().GetVUsersInfo();
            GetItemDetailPage();
            BindData(dataSource);
            //SetGridSummaryItem();
            MainForm.SetQueryPageGridColumn(gridView, mainMenu);
        }

        public void InitGrid<T>(IList list)
        {
            if (mainMenu.Name.Equals(typeof(T).Name))
            {
                gridControl.DataSource = list;
                //MainForm.SetQueryPageGridColumn(gridView, mainMenu);
            }
        }

        public void BindData(object list)
        {
            //类型相同才绑定
            //if (list.Count > 0 && mainMenu.Name.Equals(list[0].GetType().Name))
            if (list!=null && mainMenu.Name.Equals(list.GetType().GenericTypeArguments[0].Name))
            {
                gridControl.DataSource = list;
                //MainForm.SetQueryPageGridColumn(gridView, mainMenu);
            }
        }

        private void gridView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                GridHitInfo hInfo = gridView.CalcHitInfo(new Point(e.X, e.Y));
                //双击左键
                if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 2)
                {
                    //判断光标是否在行范围内 
                    if (hInfo.InRow)
                    {
                        //currentObj = gridView.GetRow(hInfo.RowHandle);
                        //NavMenu(currentObj);
                        if ((gridView.FocusedColumn.FieldName == "Pic" || gridView.FocusedColumn.FieldName == "Photo") && gridView.GetFocusedValue() != null)  //双击图片单元格打开图片
                        {
                            if (!System.IO.Directory.Exists(MainForm.DownloadFilePath))
                                System.IO.Directory.CreateDirectory(MainForm.DownloadFilePath);
                            string downloadFileName = MainForm.DownloadFilePath + String.Format("{0}.jpg", gridView.GetFocusedRowCellValue("Code").ToString());
                            string strErrorinfo = string.Empty;
                            //FtpUpDown ftpUpDown = new FtpUpDown(MainForm.ServerUrl, MainForm.ServerUserName, MainForm.ServerPassword);
                            //ftpUpDown.Download(MainForm.DownloadFilePath, String.Format("{0}.jpg", gridView.GetFocusedRowCellValue("Code").ToString()), out strErrorinfo);
                            ImageHelper.WindowsPhotoViewer(Image.FromFile(downloadFileName));
                        }
                        else
                            Edit();
                    }
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 1 && !mainMenu.Name.Contains("Report") && (mainMenu.Name.Contains("Bill") || mainMenu.Name.Contains("Order")))  //单击左键
                {
                    //object obj = gridView.GetRow(gridView.FocusedRowHandle);
                    if (hInfo.InRowCell)
                    {
                        if (Convert.ToInt32(gridView.GetRowCellValue(hInfo.RowHandle, "状态")) == 0)
                        {
                            MainForm.itemDetailPageList[mainMenu.Name].btnAudit.Visible = true;
                            MainForm.itemDetailPageList[mainMenu.Name].btnAudit.Caption = "审核";
                            MainForm.itemDetailPageList[mainMenu.Name].btnAudit.Glyph = global::USL.Properties.Resources.audit;
                        }
                        else if (Convert.ToInt32(gridView.GetRowCellValue(hInfo.RowHandle, "状态")) == 1)// && !mainMenu.Name.Contains("Order"))
                        {
                            MainForm.itemDetailPageList[mainMenu.Name].btnAudit.Visible = true;
                            MainForm.itemDetailPageList[mainMenu.Name].btnAudit.Caption = "取消审核";
                            MainForm.itemDetailPageList[mainMenu.Name].btnAudit.Glyph = global::USL.Properties.Resources.Undo_32x32;
                        }
                        else
                            MainForm.itemDetailPageList[mainMenu.Name].btnAudit.Visible = false;
                    }
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 1 &&
                    mainMenu.ParentID == new Guid("7ea0e093-592a-420c-9a7f-8316f88c35e2"))  //基础资料-单击左键
                {
                    if (hInfo.InRowCell)
                    {
                        if (Convert.ToBoolean(gridView.GetRowCellValue(hInfo.RowHandle, "IsDel")))
                        {
                            MainForm.itemDetailPageList[mainMenu.Name].btnDel.Caption = "恢复";
                        }
                        else
                            MainForm.itemDetailPageList[mainMenu.Name].btnDel.Caption = "删除";
                    }
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            finally
            {

                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        //查询面板切换到编辑面板
        MainMenu NavMenu(object currentObj)
        {
            if (view != null)
            {
                if (MainForm.itemDetailPageList.Count > 0)
                {

                    //加载窗体页面
                    if (MainForm.hasItemDetailPage[mainMenu.Name.Replace("Query", "")] == null)
                    {
                        MainForm.itemDetailPageList[mainMenu.Name.Replace("Query", "")].LoadBusinessData(MainForm.mainMenuList[mainMenu.Name.Replace("Query", "")]);
                        MainForm.hasItemDetailPage.Add(mainMenu.Name.Replace("Query", ""), true);
                    }
                    //切换到对应的单据界面并传递数据
                    //if (mainMenu.Name.ToUpper().Contains("BILLQUERY"))
                    //{
                    //    if (mainMenu.Name.ToUpper().Contains("STOCKOUT") || mainMenu.Name == MainMenuConstants.GetMaterialBillQuery
                    //        || mainMenu.Name == MainMenuConstants.FSMDPReturnBillQuery || mainMenu.Name == MainMenuConstants.EMSDPReturnBillQuery)
                    //    {
                    //        StockOutBillPage page = MainForm.itemDetailPageList[mainMenu.Name.Replace("Query", "")].itemDetail as StockOutBillPage;
                    //        if (mainMenu.Name == MainMenuConstants.FGStockOutBillQuery)
                    //            page.BindData(((VStockOutBill)currentObj).HdID);
                    //        else
                    //            page.BindData(((VMaterialStockOutBill)currentObj).HdID);
                    //    }
                    //    else if (mainMenu.Name.ToUpper().Contains("STOCKIN") || mainMenu.Name.ToUpper().Contains("ReturnBill".ToUpper()) || mainMenu.Name == MainMenuConstants.ReturnedMaterialBillQuery)
                    //    {
                    //        StockInBillPage page = MainForm.itemDetailPageList[mainMenu.Name.Replace("Query", "")].itemDetail as StockInBillPage;
                    //        if (mainMenu.Name == MainMenuConstants.ProductionStockInBillQuery || mainMenu.Name == MainMenuConstants.SalesReturnBillQuery)
                    //            page.BindData(((VStockInBill)currentObj).HdID);
                    //        else
                    //            page.BindData(((VMaterialStockInBill)currentObj).HdID);
                    //    }
                    //    else if (mainMenu.Name == MainMenuConstants.ReceiptBillQuery)
                    //    {
                    //        ReceiptBillPage page = MainForm.itemDetailPageList[mainMenu.Name.Replace("Query", "")].itemDetail as ReceiptBillPage;
                    //        page.BindData(((VReceiptBill)currentObj).HdID);
                    //    }
                    //    else if (mainMenu.Name == MainMenuConstants.PaymentBillQuery)
                    //    {
                    //        PaymentBillPage page = MainForm.itemDetailPageList[mainMenu.Name.Replace("Query", "")].itemDetail as PaymentBillPage;
                    //        page.BindData(((VPaymentBill)currentObj).HdID);
                    //    }
                    //    else if (mainMenu.Name == MainMenuConstants.WageBillQuery)
                    //    {
                    //        WageBillPage page = MainForm.itemDetailPageList[mainMenu.Name.Replace("Query", "")].itemDetail as WageBillPage;
                    //        page.BindData(((VWageBill)currentObj).HdID);
                    //    }
                    //    else if (mainMenu.Name == MainMenuConstants.AttWageBillQuery)
                    //    {
                    //        AttWageBillPage page = MainForm.itemDetailPageList[mainMenu.Name.Replace("Query", "")].itemDetail as AttWageBillPage;
                    //        page.BindData(((VAttWageBill)currentObj).HdID);
                    //    }
                    //}
                    //else if (mainMenu.Name == MainMenuConstants.OrderQuery)
                    //{
                    //    OrderEditPage page = MainForm.itemDetailPageList[MainMenuConstants.Order].itemDetail as OrderEditPage;
                    //    page.BindData(((VOrder)currentObj).HdID);
                    //}
                    //else if (mainMenu.Name == MainMenuConstants.FSMOrderQuery)
                    //{
                    //    OrderEditPage page = MainForm.itemDetailPageList[MainMenuConstants.FSMOrder].itemDetail as OrderEditPage;
                    //    page.BindData(((VFSMOrder)currentObj).HdID);
                    //}
                    //else if (mainMenu.Name == MainMenuConstants.ProductionOrderQuery)
                    //{
                    //    OrderEditPage page = MainForm.itemDetailPageList[MainMenuConstants.ProductionOrder].itemDetail as OrderEditPage;
                    //    page.BindData(((VProductionOrder)currentObj).HdID);
                    //}
                    else
                        return MainForm.mainMenuList[mainMenu.Name.Replace("Query", "")];

                    pageGroupCore.SetSelected(pageGroupCore.Items[itemDetailButtonList[mainMenu.Name] - 1]);
                    view.ActivateContainer(pageGroupCore);
                }
                return MainForm.mainMenuList[mainMenu.Name.Replace("Query", "")];
                //if (itemDetailPageList.Count>0)
                //{
                //    //切换到对应的单据界面并传递数据
                //    if (mainMenu.Name.ToUpper().Contains("BILLQUERY"))
                //    {
                //        if (mainMenu.Name.ToUpper().Contains("STOCKOUT"))
                //        {
                //            StockOutBillPage page = itemDetailPageList[mainMenu.Name.Replace("Query", "")].itemDetail as StockOutBillPage;
                //            if (mainMenu.Name==MainMenuConstants.FGStockOutBillQuery)
                //                page.BindData(((VStockOutBill)currentObj).HdID);
                //            else
                //                page.BindData(((VMaterialStockOutBill)currentObj).HdID);
                //        }
                //        else if (mainMenu.Name.ToUpper().Contains("STOCKIN") || mainMenu.Name.ToUpper().Contains("ReturnBill".ToUpper()))
                //        {
                //            StockInBillPage page = itemDetailPageList[mainMenu.Name.Replace("Query", "")].itemDetail as StockInBillPage;
                //            if (mainMenu.Name == MainMenuConstants.ProductionStockInBillQuery || mainMenu.Name == MainMenuConstants.SalesReturnBillQuery)
                //                page.BindData(((VStockInBill)currentObj).HdID);
                //            else
                //                page.BindData(((VMaterialStockInBill)currentObj).HdID);
                //        }
                //        else if (mainMenu.Name==MainMenuConstants.ReceiptBillQuery)
                //        {
                //            ReceiptBillPage page = itemDetailPageList[mainMenu.Name.Replace("Query", "")].itemDetail as ReceiptBillPage;
                //            page.BindData(((VReceiptBill)currentObj).HdID);
                //        }
                //        else if (mainMenu.Name == MainMenuConstants.PaymentBillQuery)
                //        {
                //            PaymentBillPage page = itemDetailPageList[mainMenu.Name.Replace("Query", "")].itemDetail as PaymentBillPage;
                //            page.BindData(((VPaymentBill)currentObj).HdID);
                //        }
                //    }
                //    else if (mainMenu.Name == MainMenuConstants.OrderQuery)
                //    {
                //        OrderEditPage page = itemDetailPageList[MainMenuConstants.Order].itemDetail as OrderEditPage;
                //        page.BindData(((VOrder)currentObj).HdID);
                //    }
                //    else if (mainMenu.Name == MainMenuConstants.FSMOrderQuery)
                //    {
                //        OrderEditPage page = itemDetailPageList[MainMenuConstants.FSMOrder].itemDetail as OrderEditPage;
                //        page.BindData(((VFSMOrder)currentObj).HdID);
                //    }
                //    else if (mainMenu.Name == MainMenuConstants.ProductionOrderQuery)
                //    {
                //        OrderEditPage page = itemDetailPageList[MainMenuConstants.ProductionOrder].itemDetail as OrderEditPage;
                //        page.BindData(((VProductionOrder)currentObj).HdID);
                //    }
                //    pageGroupCore.SetSelected(pageGroupCore.Items[itemDetailButtonList[mainMenu.Name] - 1]);
                //    view.ActivateContainer(pageGroupCore);
                //}
            }
            else
                return mainMenu;
        }

        //显示行的序号 
        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        public void Add()
        {
            currentObj = null;
            DataEditForm form = new DataEditForm(mainMenu, currentObj, pageGroupCore);
            form.ShowDialog();
        }

        public void Edit()
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                //if (mainMenu.ParentID == new Guid("ce85d24f-b4e9-43f0-b605-3b5b9d321935"))//系统信息
                //    return;
                if (gridView.SelectedRowsCount > 0)
                {
                    currentObj = gridView.GetRow(gridView.FocusedRowHandle);
                    if (mainMenu.ParentID == new Guid("7ea0e093-592a-420c-9a7f-8316f88c35e2"))//基础资料
                    {
                        //IList goodsList = null;
                        if (mainMenu.Name == MainMenuConstants.Material)
                        {
                            MainForm.GoodsBigType = types.Find(o =>
                                o.Type == TypesListConstants.GoodsType && o.Name == MainForm.GoodsBigTypeName).No;
                        }
                        int focusRow = gridView.FocusedRowHandle;
                        DataEditForm form = new DataEditForm(mainMenu, currentObj, pageGroupCore);
                        form.ShowDialog();
                        //if (mainMenu.Name == MainMenuConstants.Material)
                        //{
                        //    goodsList = clientFactory.GetData<VMaterial>().FindAll(o => o.Type == MainForm.GoodsBigType);
                        //    BindData(goodsList);
                        //}
                        //刷新数据
                        //itemDetailPage.DataQueryPageRefresh();
                        gridView.FocusedRowHandle = focusRow;
                    }
                    else if (mainMenu.Name.ToUpper().Contains("BILL") || mainMenu.Name.ToUpper().Contains("ORDER"))
                    {
                        MainMenu menu = NavMenu(currentObj);
                        if (Convert.ToInt32(gridView.GetFocusedRowCellValue("Status")) == 0)
                            MainForm.itemDetailPageList[mainMenu.Name.Replace("Query", "")].setNavButtonStatus(menu, ButtonType.btnSave);
                        else
                            MainForm.itemDetailPageList[mainMenu.Name.Replace("Query", "")].setNavButtonStatus(menu, ButtonType.btnAudit);
                    }
                    //else if (mainMenu.Name == MainMenuConstants.AlertQuery)
                    //{
                    //    VAlert obj = currentObj as VAlert;
                    //    MainMenu menu = null;
                    //    if (obj.BillID != null)
                    //    {
                    //        if (obj.内容.Contains("DH"))
                    //        {
                    //            menu = MainForm.mainMenuList[MainMenuConstants.Order];
                    //            MainForm.SetSelected(pageGroupCore, menu);
                    //            OrderEditPage page = MainForm.itemDetailPageList[MainMenuConstants.Order].itemDetail as OrderEditPage;
                    //            page.BindData(obj.BillID.Value);
                    //        }
                    //        else if (obj.内容.Contains("CK"))
                    //        {
                    //            menu = MainForm.mainMenuList[MainMenuConstants.FGStockOutBill];
                    //            MainForm.SetSelected(pageGroupCore, menu);
                    //            StockOutBillPage page = MainForm.itemDetailPageList[MainMenuConstants.FGStockOutBill].itemDetail as StockOutBillPage;
                    //            page.BindData(obj.BillID.Value);
                    //        }
                    //    }
                    //}
                    //else if (mainMenu.Name == MainMenuConstants.SampleStockOutReport)
                    //{
                    //    VSampleStockOut obj = currentObj as VSampleStockOut;
                    //    MainMenu menu = MainForm.mainMenuList[MainMenuConstants.FGStockOutBill];
                    //    MainForm.SetSelected(pageGroupCore, menu);
                    //    StockOutBillPage page = MainForm.itemDetailPageList[MainMenuConstants.FGStockOutBill].itemDetail as StockOutBillPage;
                    //    page.BindData(obj.HdID);
                    //}
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        /// <summary>
        /// 刷新单据编辑界面基础数据
        /// </summary>
        //void BillEditPageRefresh()
        //{
        //    if (itemDetailPageList.Count >0)
        //    {
        //        //StockInBillPage stockInBillPage = itemDetailPage.itemDetailList[MainMenuConstants.stockinb] as StockInBillPage;
        //        //stockInBillPage.BindData(Guid.Empty);
        //        //OrderEditPage orderEditPage = itemDetailPage.itemDetailList[MainMenuConstants.Order] as OrderEditPage;
        //        //orderEditPage.BindData(Guid.Empty);
        //        //StockOutBillPage stockOutBillPage = itemDetailPage.itemDetailList[MainMenuConstants.OutStoreBill] as StockOutBillPage;
        //        //stockOutBillPage.BindData(Guid.Empty);


        //    }
        //}

        public void Del()
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                string btnName = MainForm.itemDetailPageList[mainMenu.Name].btnDel.Caption;
                System.Windows.Forms.DialogResult result = XtraMessageBox.Show(string.Format("确定要{0}选择的记录吗?", btnName), "操作提示",
                System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    currentObj = gridView.GetRow(gridView.FocusedRowHandle);
                    switch (mainMenu.Name)
                    {
                        case MainMenuConstants.Department:
                            Department dept = currentObj as Department;
                            dept.IsDel = !dept.IsDel;
                            BLLFty.Create<BaseBLL>().Modify<Department>(dept);
                            break;
                        //case MainMenuConstants.Company:
                        //    BLLFty.Create<CompanyBLL>().Delete(((VCompany)currentObj).ID);
                        //    break;
                        //case MainMenuConstants.Supplier:
                        //    BLLFty.Create<SupplierBLL>().Delete(((VSupplier)currentObj).ID);
                        //    break;
                        case MainMenuConstants.UsersInfo:
                            UsersInfo obj = currentObj as UsersInfo;
                            obj.IsDel = !obj.IsDel;
                            BLLFty.Create<BaseBLL>().Modify<UsersInfo>(obj);
                            break;
                        case MainMenuConstants.Goods:
                            Goods goods = currentObj as Goods;
                            goods.IsDel = !goods.IsDel;
                            BLLFty.Create<BaseBLL>().Modify<Goods>(goods);
                            break;
                        //case MainMenuConstants.Material:
                        //    BLLFty.Create<GoodsBLL>().Delete(((VMaterial)currentObj).ID);
                        //    break;
                        //case MainMenuConstants.GoodsType:
                        //    BLLFty.Create<GoodsTypeBLL>().Delete(((VGoodsType)currentObj).ID);
                        //    break;
                        //case MainMenuConstants.Packaging:
                        //    BLLFty.Create<PackagingBLL>().Delete(((VPackaging)currentObj).ID);
                        //    break;
                        case MainMenuConstants.Inventory:
                            BLLFty.Create<BaseBLL>().DeleteByBulk<Inventory>(null);
                            break;
                    }
                    //gridView.DeleteSelectedRows();
                    clientFactory.DataPageRefresh(mainMenu.Name, string.Empty);
                    CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "删除成功");
                    //BillEditPageRefresh();
                    //itemDetailPageList[mainMenu.Name].DataPageRefresh();
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Audit()
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                if (mainMenu.Name.Contains("Order") || mainMenu.Name.Contains("Bill"))
                {
                    if (gridView.SelectedRowsCount > 0)
                    {
                        currentObj = gridView.GetRow(gridView.FocusedRowHandle);

                        #region PrintData

                        //if (ClientFactory.itemDetailList.ContainsKey(mainMenu.Name.Replace("Query", "").Trim()))
                        //{
                        //    if (currentObj is VStockInBill)
                        //    {
                        //        VStockInBill bill = currentObj as VStockInBill;
                        //        StockInBillPage page = ClientFactory.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as StockInBillPage;
                        //        page.BindData(bill.HdID);
                        //        System.Windows.Forms.DialogResult result = XtraMessageBox.Show(string.Format("确定要{0}审核单据:{1}吗?", bill.状态 == 1 ? "取消" : "", bill.入库单号), "操作提示",
                        //System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        //        if (result == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            page.Hd = BLLFty.Create<StockInBillBLL>().GetStockInBillHd(bill.HdID);
                        //            return page.Audit();
                        //        }
                        //    }
                        //    else if (currentObj is VMaterialStockInBill)
                        //    {
                        //        VMaterialStockInBill bill = currentObj as VMaterialStockInBill;
                        //        StockInBillPage page = ClientFactory.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as StockInBillPage;
                        //        page.BindData(bill.HdID);
                        //        System.Windows.Forms.DialogResult result = XtraMessageBox.Show(string.Format("确定要{0}审核单据:{1}吗?", bill.状态 == 1 ? "取消" : "", bill.入库单号), "操作提示",
                        //System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        //        if (result == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            page.Hd = BLLFty.Create<StockInBillBLL>().GetStockInBillHd(bill.HdID);
                        //            return page.Audit();
                        //        }
                        //    }
                        //    else if (currentObj is VStockOutBill)
                        //    {
                        //        VStockOutBill bill = currentObj as VStockOutBill;
                        //        StockOutBillPage page = ClientFactory.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as StockOutBillPage;
                        //        page.BindData(bill.HdID);
                        //        System.Windows.Forms.DialogResult result = XtraMessageBox.Show(string.Format("确定要{0}审核单据:{1}吗?", bill.状态 == 1 ? "取消" : "", bill.出库单号), "操作提示",
                        //System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        //        if (result == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            page.Hd = BLLFty.Create<StockOutBillBLL>().GetStockOutBillHd(bill.HdID);
                        //            return page.Audit();
                        //        }
                        //    }
                        //    else if (currentObj is VMaterialStockOutBill)
                        //    {
                        //        VMaterialStockOutBill bill = currentObj as VMaterialStockOutBill;
                        //        StockOutBillPage page = ClientFactory.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as StockOutBillPage;
                        //        page.BindData(bill.HdID);
                        //        System.Windows.Forms.DialogResult result = XtraMessageBox.Show(string.Format("确定要{0}审核单据:{1}吗?", bill.状态 == 1 ? "取消" : "", bill.出库单号), "操作提示",
                        //System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        //        if (result == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            page.Hd = BLLFty.Create<StockOutBillBLL>().GetStockOutBillHd(bill.HdID);
                        //            return page.Audit();
                        //        }
                        //    }
                        //    else if (currentObj is VOrder)
                        //    {
                        //        VOrder bill = currentObj as VOrder;
                        //        OrderEditPage page = ClientFactory.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as OrderEditPage;
                        //        page.BindData(bill.HdID);
                        //        System.Windows.Forms.DialogResult result = XtraMessageBox.Show(string.Format("确定要{0}审核单据:{1}吗?", bill.状态 == 1 ? "取消" : "", bill.订货单号), "操作提示",
                        //System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        //        if (result == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            page.Hd = BLLFty.Create<OrderBLL>().GetOrderHd(bill.HdID);
                        //            return page.Audit();
                        //        }
                        //    }
                        //    else if (currentObj is VFSMOrder)
                        //    {
                        //        VFSMOrder bill = currentObj as VFSMOrder;
                        //        OrderEditPage page = ClientFactory.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as OrderEditPage;
                        //        page.BindData(bill.HdID);
                        //        System.Windows.Forms.DialogResult result = XtraMessageBox.Show(string.Format("确定要{0}审核单据:{1}吗?", bill.状态 == 1 ? "取消" : "", bill.订货单号), "操作提示",
                        //System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        //        if (result == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            page.Hd = BLLFty.Create<OrderBLL>().GetOrderHd(bill.HdID);
                        //            return page.Audit();
                        //        }
                        //    }
                        //    else if (currentObj is VProductionOrder)
                        //    {
                        //        VProductionOrder bill = currentObj as VProductionOrder;
                        //        OrderEditPage page = ClientFactory.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as OrderEditPage;
                        //        page.BindData(bill.HdID);
                        //        System.Windows.Forms.DialogResult result = XtraMessageBox.Show(string.Format("确定要{0}审核单据:{1}吗?", bill.状态 == 1 ? "取消" : "", bill.订货单号), "操作提示",
                        //System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        //        if (result == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            page.Hd = BLLFty.Create<OrderBLL>().GetOrderHd(bill.HdID);
                        //            return page.Audit();
                        //        }
                        //    }
                        //    else if (currentObj is VReceiptBill)
                        //    {
                        //        VReceiptBill bill = currentObj as VReceiptBill;
                        //        ReceiptBillPage page = ClientFactory.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as ReceiptBillPage;
                        //        page.BindData(bill.HdID);
                        //        System.Windows.Forms.DialogResult result = XtraMessageBox.Show(string.Format("确定要{0}审核单据:{1}吗?", bill.状态 == 1 ? "取消" : "", bill.收款单号), "操作提示",
                        //System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        //        if (result == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            page.Hd = BLLFty.Create<ReceiptBillBLL>().GetReceiptBillHd(bill.HdID);
                        //            return page.Audit();
                        //        }
                        //    }
                        //    else if (currentObj is VPaymentBill)
                        //    {
                        //        VPaymentBill bill = currentObj as VPaymentBill;
                        //        PaymentBillPage page = ClientFactory.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as PaymentBillPage;
                        //        page.BindData(bill.HdID);
                        //        System.Windows.Forms.DialogResult result = XtraMessageBox.Show(string.Format("确定要{0}审核单据:{1}吗?", bill.状态 == 1 ? "取消" : "", bill.付款单号), "操作提示",
                        //System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        //        if (result == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            page.Hd = BLLFty.Create<PaymentBillBLL>().GetPaymentBillHd(bill.HdID);
                        //            return page.Audit();
                        //        }
                        //    }
                        //    else if (currentObj is VWageBill)
                        //    {
                        //        VWageBill bill = currentObj as VWageBill;
                        //        WageBillPage page = ClientFactory.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as WageBillPage;
                        //        page.BindData(bill.HdID);
                        //        System.Windows.Forms.DialogResult result = XtraMessageBox.Show(string.Format("确定要{0}审核单据:{1}吗?", bill.状态 == 1 ? "取消" : "", bill.工资单号), "操作提示",
                        //System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        //        if (result == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            page.Hd = BLLFty.Create<WageBillBLL>().GetWageBillHd(bill.HdID);
                        //            return page.Audit();
                        //        }
                        //    }
                        //    else if (currentObj is VAttWageBill)
                        //    {
                        //        VAttWageBill bill = currentObj as VAttWageBill;
                        //        AttWageBillPage page = ClientFactory.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as AttWageBillPage;
                        //        page.BindData(bill.HdID);
                        //        System.Windows.Forms.DialogResult result = XtraMessageBox.Show(string.Format("确定要{0}审核单据:{1}吗?", bill.状态 == 1 ? "取消" : "", bill.工资单号), "操作提示",
                        //System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        //        if (result == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            page.Hd = BLLFty.Create<AttWageBillBLL>().GetAttWageBillHd(bill.HdID);
                        //            return page.Audit();
                        //        }
                        //    }
                        //}
                        #endregion
                    }
                    return false;
                }
                else
                {
                    //更新库存盘点功能借用
                    //if (gridView.DataRowCount > 0)
                    //{
                    //    System.Windows.Forms.DialogResult result = XtraMessageBox.Show("确定要更新库存盘点吗?更新后将不能撤销。", "操作提示",
                    //System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                    //    if (result == System.Windows.Forms.DialogResult.OK)
                    //    {
                    //        List<Inventory> inventoryList = new List<Inventory>();
                    //        List<AccountBook> accountBooklist = new List<AccountBook>();
                    //        List<VProfitAndLoss> plList = gridControl.DataSource as List<VProfitAndLoss>;
                    //        foreach (VProfitAndLoss item in plList)
                    //        {
                    //            //库存数据
                    //            Inventory obj = new Inventory();
                    //            obj.ID = Guid.NewGuid();
                    //            obj.WarehouseID = item.WarehouseID;
                    //            obj.WarehouseType = item.仓库类型;
                    //            //obj.WarehouseType=item.wa
                    //            //obj.CompanyID=item  //盘点时货品没区分客户全盘在一起，所以不填
                    //            obj.SupplierID = item.SupplierID;
                    //            //obj.DeptID=item.//盘点时货品没区分部门全盘在一起，所以不填
                    //            obj.GoodsID = item.GoodsID;
                    //            obj.Qty = item.盘点数量.Value;
                    //            obj.MEAS = item.外箱规格;
                    //            obj.PCS = item.装箱数;
                    //            obj.InnerBox = item.内盒;
                    //            obj.Price = item.单价;
                    //            obj.Discount = 1;
                    //            obj.OtherFee = 0;
                    //            obj.EntryDate = DateTime.Now;
                    //            obj.BillNo = "库存盘点";
                    //            obj.BillDate = DateTime.Now;
                    //            inventoryList.Add(obj);
                    //            //账页数据
                    //            AccountBook ab = new AccountBook();
                    //            ab.ID = Guid.NewGuid();
                    //            ab.WarehouseID = item.WarehouseID;
                    //            ab.WarehouseType = item.仓库类型;
                    //            ab.GoodsID = item.GoodsID;
                    //            ab.AccntDate = DateTime.Now;
                    //            ab.MEAS = item.外箱规格;
                    //            ab.PCS = item.装箱数;
                    //            ab.Price = item.单价;
                    //            ab.Discount = 1;
                    //            ab.OtherFee = 0;
                    //            ab.InQty = item.盘点数量.Value;
                    //            ab.BalanceQty = item.盘点数量.Value;
                    //            ab.BalanceCost = ab.BalanceQty * ab.Price * ab.Discount + ab.OtherFee;
                    //            ab.BillNo = "库存盘点";
                    //            ab.BillDate = DateTime.Now;
                    //            accountBooklist.Add(ab);
                    //        }
                    //        //BLLFty.Create<InventoryBLL>().StocktakingUpdate(inventoryList[0].WarehouseID, inventoryList);
                    //        List<VStocktaking> stocktakingList = BLLFty.Create<InventoryBLL>().GetStocktaking();
                    //        Guid warehouseID = Guid.Empty;
                    //        int goodsBigType = -1;
                    //        if (stocktakingList.Count > 0)
                    //        {
                    //            switch (stocktakingList[0].盘点仓库)
                    //            {
                    //                case "成品仓":
                    //                    warehouseID = warehouseList.FirstOrDefault(o => o.Code == WarehouseConstants.FG).ID;
                    //                    break;
                    //                case "半成品仓":
                    //                    warehouseID = warehouseList.FirstOrDefault(o => o.Code == WarehouseConstants.SFG).ID;
                    //                    break;
                    //                case "外加工":
                    //                    warehouseID = warehouseList.FirstOrDefault(o => o.Code == WarehouseConstants.EMS).ID;
                    //                    break;
                    //                case "自动机":
                    //                    warehouseID = warehouseList.FirstOrDefault(o => o.Code == WarehouseConstants.FSM).ID;
                    //                    break;
                    //                default:
                    //                    break;
                    //            }
                    //            goodsBigType = stocktakingList[0].货品大类;
                    //        }
                    //        if (warehouseID == Guid.Empty)
                    //        {
                    //            CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "没有盘点数据，请先导入盘点数据。");
                    //            return false;
                    //        }
                    //        BLLFty.Create<InventoryBLL>().StocktakingUpdate(warehouseID, goodsBigType, stocktakingList[0].SupplierID, inventoryList, accountBooklist);
                    //        MainForm.DataQueryPageRefresh();
                    //        CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "盘点库存更新成功");
                    //        return true;
                    //    }
                    //}
                    return false;

                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                return false;
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        public void Print()
        {
            PrintSettingController psc = null;
            //if (mainMenu.Name.Contains("Order") || mainMenu.Name.Contains("Bill"))
            if (mainMenu.Name.Contains("StatementOfAccount"))
            {
                if (gridView.SelectedRowsCount > 0)
                {
                    currentObj = gridView.GetRow(gridView.FocusedRowHandle);

                    #region PrintData

                    //if (currentObj is VStockInBill)
                    //{
                    //    VStockInBill bill = currentObj as VStockInBill;
                    //    StockInBillPage page = MainForm.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as StockInBillPage;
                    //    page.BindData(bill.HdID);
                    //    page.Print();
                    //}
                    //else if (currentObj is VMaterialStockInBill)
                    //{
                    //    VMaterialStockInBill bill = currentObj as VMaterialStockInBill;
                    //    StockInBillPage page = MainForm.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as StockInBillPage;
                    //    page.BindData(bill.HdID);
                    //    page.Print();
                    //}
                    //else if (currentObj is VStockOutBill)
                    //{
                    //    VStockOutBill bill = currentObj as VStockOutBill;
                    //    StockOutBillPage page = MainForm.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as StockOutBillPage;
                    //    page.BindData(bill.HdID);
                    //    page.Print();
                    //}
                    //else if (currentObj is VMaterialStockOutBill)
                    //{
                    //    VMaterialStockOutBill bill = currentObj as VMaterialStockOutBill;
                    //    StockOutBillPage page = MainForm.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as StockOutBillPage;
                    //    page.BindData(bill.HdID);
                    //    page.Print();
                    //}
                    //else if (currentObj is VOrder)
                    //{
                    //    VOrder bill = currentObj as VOrder;
                    //    OrderEditPage page = MainForm.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as OrderEditPage;
                    //    page.BindData(bill.HdID);
                    //    page.Print();
                    //}
                    //else if (currentObj is VFSMOrder)
                    //{
                    //    VFSMOrder bill = currentObj as VFSMOrder;
                    //    OrderEditPage page = MainForm.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as OrderEditPage;
                    //    page.BindData(bill.HdID);
                    //    page.Print();
                    //}
                    //else if (currentObj is VProductionOrder)
                    //{
                    //    VProductionOrder bill = currentObj as VProductionOrder;
                    //    OrderEditPage page = MainForm.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as OrderEditPage;
                    //    page.BindData(bill.HdID);
                    //    page.Print();
                    //}
                    //else if (currentObj is VReceiptBill)
                    //{
                    //    VReceiptBill bill = currentObj as VReceiptBill;
                    //    ReceiptBillPage page = MainForm.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as ReceiptBillPage;
                    //    page.BindData(bill.HdID);
                    //    page.Print();
                    //}
                    //else if (currentObj is VPaymentBill)
                    //{
                    //    VPaymentBill bill = currentObj as VPaymentBill;
                    //    PaymentBillPage page = MainForm.itemDetailList[mainMenu.Name.Replace("Query", "").Trim()] as PaymentBillPage;
                    //    page.BindData(bill.HdID);
                    //    page.Print();
                    //}
                    //if (currentObj is StatementOfAccountToCustomerReport && ClientFactory.itemDetailList.ContainsKey(MainMenuConstants.ReceiptBill))
                    //{
                    //    StatementOfAccountToCustomerReport bill = currentObj as StatementOfAccountToCustomerReport;
                    //    ReceiptBillPage page = ClientFactory.itemDetailList[MainMenuConstants.ReceiptBill] as ReceiptBillPage;
                    //    page.BindData(((List<ReceiptBillHd>)ClientFactory.dataSourceList[typeof(ReceiptBillHd)]).FirstOrDefault(o => o.BillNo == bill.收款单号).ID);
                    //    page.SendData(null);
                    //}
                    //else if (currentObj is StatementOfAccountToSupplierReport && ClientFactory.itemDetailList.ContainsKey(MainMenuConstants.PaymentBill))
                    //{
                    //    StatementOfAccountToSupplierReport bill = currentObj as StatementOfAccountToSupplierReport;
                    //    PaymentBillPage page = ClientFactory.itemDetailList[MainMenuConstants.PaymentBill] as PaymentBillPage;
                    //    page.BindData(((List<PaymentBillHd>)ClientFactory.dataSourceList[typeof(PaymentBillHd)]).FirstOrDefault(o => o.BillNo == bill.付款单号).ID);
                    //    page.SendData(null);
                    //}

                    #endregion
                }
                psc.IsBill = true;
            }
            else
            {
                //foreach (DevExpress.XtraGrid.Columns.GridColumn col in ((GridView)gridControl.MainView).Columns)
                //{
                //    col.BestFit();
                //}
                psc = new PrintSettingController(this.gridControl);
                //页眉 
                psc.PrintCompany = MainForm.Company;
                psc.PrintLeftHeader = this.lblHistoryFilter.Text.Substring(lblHistoryFilter.Text.IndexOf('：') + 1);
                if (mainMenu.Caption == "物料资料")
                    psc.PrintHeader = MainForm.GoodsBigTypeName;
                else
                    psc.PrintHeader = mainMenu.Caption;
                psc.PrintSubTitle = MainForm.Contacts.Replace("\\r\\n", "\r\n");
                //页脚 
                //psc.PrintRightFooter = "打印日期：" + DateTime.Now.ToString();

                psc.IsBill = false;

                //横纵向 
                //psc.LandScape = this.rbtnHorizon.Checked;
                if (psc.IsBill || (MainForm.Company.Contains("镇阳") && mainMenu.Name == MainMenuConstants.FGStockOutBillQuery))
                    psc.LandScape = false;
                else
                    psc.LandScape = true;
                //纸型 
                psc.PaperKind = System.Drawing.Printing.PaperKind.A4;
                //加载页面设置信息 
                psc.LoadPageSetting();

                psc.Preview();
            }

        }

        private void gridView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Value != null && MainForm.dataSourceList.Count > 0)
            if (e.Value!=null)
            {
                if (e.Column.FieldName == "Status")
                {
                    if (mainMenu.Name.Equals(MainMenuConstants.StocktakingLogHd))
                        e.DisplayText = EnumHelper.GetDescription<StocktakingStatusEnum>((StocktakingStatusEnum)e.Value, false);
                }
                if (e.Column.FieldName == "状态")
                {
                    e.DisplayText = EnumHelper.GetDescription<BillStatus>((BillStatus)e.Value, false);
                }
                if (e.Column.FieldName == "工资状态")
                {
                    e.DisplayText = EnumHelper.GetDescription<WageStatus>((WageStatus)e.Value, false);
                }
                if (e.Column.FieldName == "考勤状态")
                {
                    e.DisplayText = EnumHelper.GetDescription<AttStatusType>((AttStatusType)e.Value, false);
                }
                if (e.Column.FieldName == "订单类型")
                {
                    e.DisplayText = EnumHelper.GetDescription<OrderBillType>((OrderBillType)e.Value, false);
                }
                else if (e.Column.FieldName == "货品大类")
                {
                    e.DisplayText = EnumHelper.GetDescription<GoodsBigType>((GoodsBigType)e.Value, false);
                }
                else if (e.Column.FieldName == "班次" && mainMenu.Name != MainMenuConstants.AttWageBillQuery)
                {
                    e.DisplayText = EnumHelper.GetDescription<WorkShiftsType>((WorkShiftsType)e.Value, false);
                }
                else if (e.Column.FieldName == "机号")
                {
                    e.DisplayText = EnumHelper.GetDescription<MachineType>((MachineType)e.Value, false);
                }
                else if (e.Value is int && (e.Column.FieldName == "客户类型" || e.Column.FieldName == "仓库类型"))
                {
                    e.DisplayText = types.Find(o => o.Type == TypesListConstants.CustomerType && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                }
                else if (e.Value is int && e.Column.FieldName == "供应商类型")
                {
                    e.DisplayText = types.Find(o => o.Type == TypesListConstants.SupplierType && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                }
                else if (e.Value is int && e.Column.FieldName.Contains("特权"))
                {
                    e.DisplayText = types.Find(o => o.Type == TypesListConstants.PrivilegeType && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                }
                else if (e.Value is int && e.Column.FieldName == "验证方式")
                {
                    e.DisplayText = types.Find(o => o.Type == TypesListConstants.VerifyMethodType && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                }
                else if (e.Value is int && e.Column.FieldName == "结算方式")
                {
                    e.DisplayText = types.Find(o => o.Type == TypesListConstants.POClearType && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                }
                else if (e.Value is int && e.Column.FieldName == "收款类型")
                {
                    e.DisplayText = types.Find(o => o.Type == TypesListConstants.ReceiptBillType && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                }
                else if (e.Value is int && e.Column.FieldName == "付款类型")
                {
                    e.DisplayText = types.Find(o => o.Type == TypesListConstants.PaymentBillType && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                }
                else if (e.Value is int && e.Column.FieldName == "门店类型")
                {
                    e.DisplayText = types.Find(o => o.Type == TypesListConstants.DeptType && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                }
                else if (e.Value is int && e.Column.FieldName == "类型")
                {
                    if (mainMenu.Name.Contains(MainMenuConstants.GetMaterialBill))
                        e.DisplayText = types.FirstOrDefault(o => o.Type == MainMenuConstants.StockOutBillType && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                    else if (mainMenu.Name.Contains(MainMenuConstants.ReturnedMaterialBill))
                        e.DisplayText = types.FirstOrDefault(o => o.Type == MainMenuConstants.StockInBillType && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                    else
                        e.DisplayText = types.Find(o => (mainMenu.Name.Contains(o.Type.Substring(0, 7)) || mainMenu.Name.Contains(o.SubType)) && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                }

            }
        }

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 实际差异数量大于0显示红色
            if (e.Column.FieldName.Equals("FinalDiffQty"))
            {
                GridCellInfo gridCellInfo = e.Cell as GridCellInfo;
                if (gridCellInfo.IsDataCell && gridCellInfo.CellValue != null &&
                    int.Parse(gridCellInfo.CellValue.ToString()) > 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
            if (e.Column.FieldName == "状态")
            {
                GridCellInfo gridCellInfo = e.Cell as GridCellInfo;
                if (gridCellInfo.IsDataCell && gridCellInfo.CellValue != null &&
                    (int.Parse(gridCellInfo.CellValue.ToString()) == (int)BillStatus.UnAudited ||
                    int.Parse(gridCellInfo.CellValue.ToString()) == (int)BillStatus.UnCleared ||
                    int.Parse(gridCellInfo.CellValue.ToString()) == (int)WageStatus.UnClosed))
                    e.Appearance.ForeColor = Color.Red;
            }
            if (e.Column.FieldName == "工资状态")
            {
                GridCellInfo gridCellInfo = e.Cell as GridCellInfo;
                if (gridCellInfo.IsDataCell && gridCellInfo.CellValue != null &&
                    int.Parse(gridCellInfo.CellValue.ToString()) == (int)WageStatus.UnClosed)
                    e.Appearance.ForeColor = Color.Red;
            }
            if (mainMenu.Name.ToUpper().Contains("ORDER") && e.Column.FieldName == "类型")
            {
                GridCellInfo gridCellInfo = e.Cell as GridCellInfo;
                if (gridCellInfo.IsDataCell && gridCellInfo.CellValue != null && int.Parse(gridCellInfo.CellValue.ToString()) == (int)OrderType.Emergency)
                    e.Appearance.ForeColor = Color.Red;
            }

            if (MainForm.itemDetailPageList[mainMenu.Name].btnConnect != null)
            {
                if (MainForm.IsConnected)
                {
                    MainForm.itemDetailPageList[mainMenu.Name].btnConnect.Caption = "断开设备";
                    MainForm.itemDetailPageList[mainMenu.Name].btnConnect.Glyph = global::USL.Properties.Resources.switch_off_54px;
                }
                else
                {
                    MainForm.itemDetailPageList[mainMenu.Name].btnConnect.Caption = "连接设备";
                    MainForm.itemDetailPageList[mainMenu.Name].btnConnect.Glyph = global::USL.Properties.Resources.switch_on_54px;
                }
            }

        }

        private void gvLedgerDtlForPrint_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Column.FieldName == "Type")
                {
                    if (mainMenu.Name == MainMenuConstants.ReceiptBillQuery)
                        e.DisplayText = types.Find(o => o.Type == TypesListConstants.ReceiptBillType && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                    else if (mainMenu.Name == MainMenuConstants.PaymentBillQuery)
                        e.DisplayText = types.Find(o => o.Type == TypesListConstants.PaymentBillType && o.No == Convert.ToInt32(e.Value)).Name.Trim();
                }
            }
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            object o = gridView.GetRowCellValue(e.RowHandle, "IsDel");
            if (o != null && bool.Parse(o.ToString()))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
                e.Appearance.FontStyleDelta = FontStyle.Strikeout;
            }
            object s = gridView.GetRowCellValue(e.RowHandle, "停产");
            if (s != null && bool.Parse(s.ToString()))
            {
                e.Appearance.ForeColor = Color.Red;
            }
            // 账面差异数量小于0显示红色
            object diffQty = gridView.GetRowCellValue(e.RowHandle, "DiffQty");
            if (diffQty != null && int.Parse(diffQty.ToString()) < 0)
            {
                e.Appearance.ForeColor = Color.Red;
            }
            // 实际差异数量大于0显示黄色
            //object finalDiffQty = gridView.GetRowCellValue(e.RowHandle, "FinalDiffQty");
            //if (finalDiffQty!=null && int.Parse(finalDiffQty.ToString())>0)
            //{
            //    e.Appearance.ForeColor = Color.YellowGreen;
            //}
        }

        public void Import()
        {
            throw new NotImplementedException();
        }

        public void Export()
        {
            throw new NotImplementedException();
            //借用于设置货品停产
            //try
            //{
            //    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            //    System.Windows.Forms.DialogResult result = XtraMessageBox.Show("确定要停产选择的货品吗?", "操作提示",
            //    System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
            //    if (result == System.Windows.Forms.DialogResult.OK)
            //    {
            //        currentObj = gridView.GetRow(gridView.FocusedRowHandle);
            //        switch (mainMenu.Name)
            //        {
            //            case MainMenuConstants.Goods:
            //                Goods goods = BLLFty.Create<GoodsBLL>().GetGoods(((Goods)currentObj).ID);
            //                goods.IsDel = true;
            //                BLLFty.Create<GoodsBLL>().Update(goods);
            //                break;
            //            case MainMenuConstants.Material:
            //                Goods sf = BLLFty.Create<GoodsBLL>().GetGoods(((VMaterial)currentObj).ID);
            //                sf.IsDel = true;
            //                BLLFty.Create<GoodsBLL>().Update(sf);
            //                break;
            //        }
            //        clientFactory.DataPageRefresh(mainMenu.Name,string.Empty);
            //        CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "货品设置停产成功");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            //}
            //finally
            //{
            //    this.Cursor = System.Windows.Forms.Cursors.Default;
            //}
        }

        public void SendData(object data)
        {
            //盘点导入
            if (mainMenu.Name.Equals(MainMenuConstants.Stocktaking))
            {
                string code = string.Empty;
                Dictionary<string, object> dict = (Dictionary<string, object>)data;
                //Department dept = dict[MainMenuConstants.Department] as Department;
                Warehouse warehouse = dict[MainMenuConstants.Warehouse] as Warehouse;
                int iCount = 1;
                WaitDialogForm waitDialogForm = null;
                try
                {
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    var openFileDialog = new System.Windows.Forms.OpenFileDialog();
                    openFileDialog.Filter = "Excel 97-2003 工作簿|*.xls*|Excel 工作簿|*.xlsx|所有文件|*.*";
                    openFileDialog.FilterIndex = 1;
                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        new Thread((ThreadStart)delegate {
                            waitDialogForm = new WaitDialogForm("请稍候...", "正在导入数据", new Size(300, 60), this.FindForm());
                            System.Windows.Forms.Application.Run(waitDialogForm);
                        }).Start();

                        DataSet ds = ExcelHelper.ImportExcel(openFileDialog.FileName);

                        List<Stocktaking> insertList = new List<Stocktaking>();
                        List<Stocktaking> updateList = new List<Stocktaking>();
                        List<string> repeatCodeList = new List<string>();
                        List<Stocktaking> stList = BLLFty.Create<BaseBLL>().GetListBy<Stocktaking>(null);
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            iCount++;
                            code = row[EnumHelper.GetDescription<StocktakingEnum>(StocktakingEnum.GoodsCode, false)].ToString().Trim();
                            //检查编号是否存在
                            if (iCount > ds.Tables[0].Rows.Count)
                                continue;
                            if (string.IsNullOrWhiteSpace(code))
                            {
                                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("第{0}行的编码不能为空。", iCount));
                                return;
                            }
                            Stocktaking entity = stList.FirstOrDefault(o => o.DeptID.Equals(MainForm.department.ID) && o.WarehouseID.Equals(warehouse.ID) && o.GoodsCode.Equals(code));
                            bool isNew = false;
                            int oldCheckQty = 0;
                            if (entity == null)
                            {
                                entity = new Stocktaking();
                                entity.ID = Guid.NewGuid();
                                entity.DeptID = MainForm.department.ID;
                                entity.DeptCode = MainForm.department.Code;
                                entity.DeptName = MainForm.department.Name;
                                entity.WarehouseID = warehouse.ID;
                                entity.WarehouseCode = warehouse.Code;
                                entity.WarehouseName = warehouse.Name;
                                isNew = true;
                            }
                            else
                            {
                                // 记录之前的盘点数，和新导入的合并
                                oldCheckQty = entity.CheckQty;
                            }
                            foreach (PropertyInfo p in entity.GetType().GetProperties())
                            {
                                ListItem<StocktakingEnum> item = EnumHelper.GetEnumValues<StocktakingEnum>(false).FirstOrDefault(o => o.Value.ToString().Equals(p.Name));
                                if (item != null)
                                {
                                    if (ds.Tables[0].Columns.Contains(item.Name))
                                    {
                                        if (p.PropertyType.IsGenericType)
                                        {
                                            // 泛型Nullable<>
                                            Type genericTypeDefinition = p.PropertyType.GetGenericTypeDefinition();
                                            if (genericTypeDefinition == typeof(Nullable<>))
                                                p.SetValue(entity, string.IsNullOrWhiteSpace(row[item.Name].ToString()) ? null : Convert.ChangeType(row[item.Name], Nullable.GetUnderlyingType(p.PropertyType)), null);
                                        }
                                        else
                                        {
                                            // 非泛型
                                            p.SetValue(entity, string.IsNullOrWhiteSpace(row[item.Name].ToString()) ? null : Convert.ChangeType(row[item.Name], p.PropertyType), null);
                                        }
                                    }
                                }
                            }
                            if (isNew)
                            {
                                // excel表里的编码如果有重复，则合并
                                if (repeatCodeList.Contains(entity.GoodsCode))
                                {
                                    Stocktaking repeatItem = insertList.FirstOrDefault(o => o.GoodsCode.Equals(entity.GoodsCode));
                                    repeatItem.CheckQty += entity.CheckQty;
                                }
                                else
                                {
                                    insertList.Add(entity);
                                    repeatCodeList.Add(entity.GoodsCode);
                                }

                                //insertList.ForEach(o=>{
                                //    // 编号重复则盘点数累加
                                //    if (o.WarehouseID.Equals(entity.WarehouseID) && o.Code.Equals(entity.Code))
                                //    {
                                //        o.CheckQty += entity.CheckQty;
                                //    }
                                //});
                            }
                            else
                            {
                                entity.CheckQty += oldCheckQty;// 合并盘点数
                                updateList.Add(entity);
                            }
                        }
                        if (insertList.Count > 0 || updateList.Count > 0)
                        {
                            BLLFty.Create<BaseBLL>().AddAndUpdate(insertList, updateList);
                            clientFactory.DataPageRefresh<Stocktaking>();
                            //InitGrid(list);
                            //刷新盘点盈亏表
                            //if (MainForm.itemDetailList.ContainsKey(MainMenuConstants.ProfitAndLoss))
                            //{
                            //    DataQueryPage page = MainForm.itemDetailList[MainMenuConstants.ProfitAndLoss] as DataQueryPage;
                            //    if (stList[0].GoodsBigType == -1)
                            //        page.InitGrid(((List<VProfitAndLoss>)MainForm.dataSourceList[typeof(VProfitAndLoss)]).FindAll(o =>
                            //            o.仓库 == stList[0].Warehouse && o.SupplierID == stList[0].SupplierID));
                            //    else
                            //        page.InitGrid(((List<VProfitAndLoss>)MainForm.dataSourceList[typeof(VProfitAndLoss)]).FindAll(o =>
                            //        o.仓库 == stList[0].Warehouse && o.SupplierID == stList[0].SupplierID && o.货品大类 == stList[0].GoodsBigType));
                            //    CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "导入成功");
                            //    return true;
                            //}
                            CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "导入成功");
                        }
                    }

                }
                catch (Exception ex)
                {
                    CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("第{0}行记录格式异常。", iCount) + "\r\n" + ex.Message);
                }
                finally
                {
                    if (waitDialogForm != null)
                        waitDialogForm.Invoke((EventHandler)delegate { waitDialogForm.Close(); });
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                }

            }
        }

        public object ReceiveData()
        {
            throw new NotImplementedException();
            //currentObj = gridView.GetRow(gridView.FocusedRowHandle);
            //if (mainMenu.Name == MainMenuConstants.ProductionOrderQuery)
            //{
            //    if (currentObj == null)
            //    {
            //        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "请选择要产品回收的布产单。");
            //        return null;
            //    }
            //    VProductionOrder order = currentObj as VProductionOrder;
            //    if (order == null || order.状态 == (int)BillStatus.UnAudited)
            //    {
            //        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "未审核布产单不允许回收产品。");
            //        return null;
            //    }
            //    if (order.SupplierID == null)
            //    {
            //        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "请填写加工厂家。");
            //        return null;
            //    }
            //    if (order.未收箱数 <= 0)
            //    {
            //        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "布产数量已经全部发完，不能再回收产品。");
            //        return null;
            //    }

            //    System.Windows.Forms.DialogResult result = XtraMessageBox.Show("确定要回收产品吗?", "操作提示",
            //                   System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
            //    if (result == System.Windows.Forms.DialogResult.OK)
            //    {
            //        return currentObj;
            //    }
            //    else
            //        return null;
            //}
            //else if (mainMenu.Name == MainMenuConstants.SalesReturnBillQuery)
            //{
            //    if (currentObj == null)
            //    {
            //        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "请选择要退料的退货单。");
            //        return null;
            //    }
            //    VStockInBill bill = currentObj as VStockInBill;
            //    //if (bill == null || bill.状态 == (int)BillStatus.UnAudited)
            //    //{
            //    //    CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "未审核退货单不允许退料入库。");
            //    //    return null;
            //    //}
            //    if (bill.已退料入库 == true)
            //    {
            //        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "该退货单物料已退货入库。");
            //        return null;
            //    }

            //    System.Windows.Forms.DialogResult result = XtraMessageBox.Show("确定要将退货物料入库吗?", "操作提示",
            //                   System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
            //    if (result == System.Windows.Forms.DialogResult.OK)
            //    {
            //        return currentObj;
            //    }
            //    else
            //        return null;
            //}
            //else
            //    return currentObj;
        }

        private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            IList list = (IList)view.DataSource;
            //switch (mainMenu.Name)
            //{
            //    case MainMenuConstants.ProfitAndLoss:
            //        list = (List<ProfitAndLoss>)view.DataSource;
            //        break;
            //    case MainMenuConstants.UnlistedGoods:
            //        list = (List<UnlistedGoods>)view.DataSource;
            //        break;
            //    default:
            //        break;
            //}
            if (list != null && list.Count > 0)
            {
                if (e.Column == view.FocusedColumn && e.Column.FieldName.ToLower().Contains("qty"))
                {
                    view.SetRowCellValue(e.RowHandle, e.Column.FieldName.Replace("Qty", "AMT"), Convert.ToDecimal(e.Value) * (decimal)view.GetRowCellValue(e.RowHandle, "Price"));
                    MainForm.calcFinalDiff();
                }
                WaitDialogForm waitDialogForm = null;
                new Thread((ThreadStart)delegate {
                    waitDialogForm = new WaitDialogForm("请稍候...", "正在保存数据", new Size(300, 60), this.FindForm());
                    System.Windows.Forms.Application.Run(waitDialogForm);
                }).Start();
                // 保存
                switch (mainMenu.Name)
                {
                    case MainMenuConstants.ProfitAndLoss:
                        BLLFty.Create<BaseBLL>().UpdateByBulk((List<ProfitAndLoss>)list);
                        break;
                    case MainMenuConstants.UnlistedGoods:
                        BLLFty.Create<BaseBLL>().UpdateByBulk((List<UnlistedGoods>)list);
                        break;
                    default:
                        break;
                }
                MainForm.SetSummaryItemColumns(gridView);
                waitDialogForm.Invoke((EventHandler)delegate { waitDialogForm.Close(); });
            }
        }
    }
}
