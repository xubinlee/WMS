using BLL;
using CommonLibrary;
using CommonLibrary.Client;
using DBML;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Factory;
using IBase;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Linq;
using System.Data.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Utility;
using System.IO;
using System.Data;
using System.Reflection;
using static Utility.EnumHelper;
using System.Transactions;
using System.Threading;
using DevExpress.Utils;

namespace USL
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    //[SmartPart]
    public partial class ItemDetailPage : XtraUserControl
    {
        //Dictionary<Guid, PageGroup> groupsItemDetailPage;
        ////public Dictionary<String, IItemDetail> itemDetailList;
        Dictionary<String, int> itemDetailButtonList; //子菜单按钮项
        PageGroup pageGroupCore;
        //IList list;
        RadialMenu radialMenu;
        List<MainMenu> menuList;
        public MainMenu menu;
        public IItemDetail itemDetail;
        public IExtensions iExtensions;
        //BaseContentContainer documentContainer;

        //按钮
        public NavButton btnSave;
        public NavButton btnAudit;
        public NavButton btnDel;
        public NavButton btnConnect;
        public NavButton btnImportCheck;// 盘点导入按钮
        public bool isBOMPrint = false;
        //public NavButton btnInvalid;
        //public NavButton btnCancelAudit;
        //NavButton btnPrint;

        //public WorkItem RootWorkItem
        //{
        //    get { return ServiceClient.GetClientService<WorkItem>(); }
        //}
        public ItemDetailPage(MainMenu item, PageGroup child, Dictionary<Guid, PageGroup> groupsItemDetailList, List<MainMenu> AllMenuList, Dictionary<String, int> childButtonList)
        {
            InitializeComponent();
            menu = item;
            menuList = AllMenuList;
            pageGroupCore = child;
            //documentContainer = pageGroupCore.Parent as BaseContentContainer;
            //groupsItemDetailPage = groupsItemDetailList;
            ////itemDetailList = iid;
            itemDetailButtonList = childButtonList;
            radialMenu = new RadialMenu();
            radialMenu.Manager = barManager;
            CreateRadiaMenu();
            //labelTitle.Text = item.Title;
            labelTitle.Text = item.Caption;
            //labelSubtitle.Text = item.Subtitle;
            imageControl.Image = DevExpress.Utils.ResourceImageHelper.CreateImageFromResources(item.ImagePath, typeof(ItemDetailPage).Assembly);
            //labelContent.Text = item.Content; 
            LoadBusinessData(menu);
            layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;//图片
            //联系
            //NavButton btnContact = new NavButton();
            //btnContact.Caption = "购买咨询：15220288727  李先生";
            //btnContact.Glyph = global::USL.Properties.Resources.phone_32;
            //btnContact.Name = "btnContact";
            //btnContact.Alignment = NavButtonAlignment.Right;
            ////btnContact.ElementClick += btnRefresh_ElementClick;
            //tileNavPane.Buttons.Add(btnContact);
        }
        public ItemDetailPage(MainMenu item, PageGroup child, List<MainMenu> AllMenuList, Dictionary<String, int> childButtonList)
        {
            InitializeComponent();
            menu = item;
            pageGroupCore = child;
            menuList = AllMenuList;
            itemDetailButtonList = childButtonList;
            radialMenu = new RadialMenu();
            radialMenu.Manager = barManager;
            CreateRadiaMenu();
            //labelTitle.Text = item.Title;
            labelTitle.Text = item.Caption;
            //labelSubtitle.Text = item.Subtitle;
            imageControl.Image = DevExpress.Utils.ResourceImageHelper.CreateImageFromResources(item.ImagePath, typeof(ItemDetailPage).Assembly);
            //labelContent.Text = item.Content; 
            layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;//图片
            BindData();
            //联系
            //NavButton btnContact = new NavButton();
            //btnContact.Caption = "购买咨询：15989744575  李先生";
            //btnContact.Glyph = global::USL.Properties.Resources.phone_32;
            //btnContact.Name = "btnContact";
            //btnContact.Alignment = NavButtonAlignment.Right;
            ////btnContact.ElementClick += btnRefresh_ElementClick;
            //tileNavPane.Buttons.Add(btnContact);
        }

        public void BindData()
        {
            // 绑定门店
            deptBindingSource.DataSource = MainForm.ConvertList<Department>((IList)MainForm.dataSourceList[typeof(Department)]).FindAll(o => o.IsDel == false);
            // 绑定仓位
            warehouseBindingSource.DataSource = MainForm.ConvertList<Warehouse>((IList)MainForm.dataSourceList[typeof(Warehouse)]);
        }

        //public void InitPage(MainMenu item)
        //{
        //    menu = item;
        //    ////menuList = AllMenuList;
        //    pageGroupCore = child;
        //    //documentContainer = pageGroupCore.Parent as BaseContentContainer;
        //    //groupsItemDetailPage = groupsItemDetailList;
        //    ////itemDetailList = iid;
        //    ////itemDetailButtonList = childButtonList;
        //    ////CreateRadiaMenu();
        //    //labelTitle.Text = item.Title;
        //    labelTitle.Text = item.Caption;
        //    //labelSubtitle.Text = item.Subtitle;
        //    imageControl.Image = DevExpress.Utils.ResourceImageHelper.CreateImageFromResources(item.ImagePath, typeof(ItemDetailPage).Assembly);
        //    //labelContent.Text = item.Content; 
        //    LoadBusinessData(menu);
        //    layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;//图片
        //    //联系
        //    //NavButton btnContact = new NavButton();
        //    //btnContact.Caption = "购买咨询：15220288727  李先生";
        //    //btnContact.Glyph = global::USL.Properties.Resources.phone_32;
        //    //btnContact.Name = "btnContact";
        //    //btnContact.Alignment = NavButtonAlignment.Right;
        //    ////btnContact.ElementClick += btnRefresh_ElementClick;
        //    //tileNavPane.Buttons.Add(btnContact);
        //}

        /// <summary>
        /// 获得盘点当前状态
        /// </summary>
        /// <returns></returns>
        int GetStocktakingStatus()
        {
            SystemStatus systemStatus = MainForm.ConvertList<SystemStatus>((IList)MainForm.dataSourceList[typeof(SystemStatus)]).FirstOrDefault(o => o.MainMenuName.Equals(MainMenuConstants.Stocktaking));
            int status = 0;
            if (systemStatus != null)
                status = systemStatus.Status;
            return status;
        }

        void CreateToolsButton(MainMenu menu)
        {
            //刷新
            NavButton btnRefresh = new NavButton();
            btnRefresh.Caption = "刷新";
            btnRefresh.Glyph = global::USL.Properties.Resources.Refresh_32x32;
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ElementClick += btnRefresh_ElementClick;
            tileNavPane.Buttons.Add(btnRefresh);
            //基础资料
            if (menu.ParentID == new Guid("7ea0e093-592a-420c-9a7f-8316f88c35e2"))
            {
                if (MainForm.buttonPermissions.Exists(o => o.Name == "btnAdd" && o.CheckBoxState == true))
                {
                    //添加
                    NavButton btnAdd = new NavButton();
                    btnAdd.Caption = "添加";
                    btnAdd.Glyph = global::USL.Properties.Resources.add;
                    btnAdd.Name = "btnAdd";
                    btnAdd.ElementClick += btnAdd_ElementClick;
                    tileNavPane.Buttons.Add(btnAdd);
                }
                if (MainForm.buttonPermissions.Exists(o => o.Name == "btnEdit" && o.CheckBoxState == true))
                {
                    //修改
                    NavButton btnEdit = new NavButton();
                    btnEdit.Caption = "修改";
                    btnEdit.Glyph = global::USL.Properties.Resources.edit;
                    btnEdit.Name = "btnEdit";
                    btnEdit.ElementClick += btnEdit_ElementClick;
                    tileNavPane.Buttons.Add(btnEdit);
                }
                //部门导入
                if (menu.Name == MainMenuConstants.Department)
                {
                    //导入
                    NavButton btnDeptImport = new NavButton();
                    btnDeptImport.Caption = "导入";
                    btnDeptImport.Glyph = global::USL.Properties.Resources.ExportToXLS_32x32;
                    btnDeptImport.Name = "btnDeptImport";
                    btnDeptImport.ElementClick += BtnDeptImport_ElementClick;
                    tileNavPane.Buttons.Add(btnDeptImport);
                }
                //成品导入
                if (menu.Name == MainMenuConstants.Goods)
                {
                    //导入
                    NavButton btnGoodsImport = new NavButton();
                    btnGoodsImport.Caption = "导入";
                    btnGoodsImport.Glyph = global::USL.Properties.Resources.ExportToXLS_32x32;
                    btnGoodsImport.Name = "btnGoodsImport";
                    btnGoodsImport.ElementClick += btnGoodsImport_ElementClick;
                    tileNavPane.Buttons.Add(btnGoodsImport);
                    //导入图片
                    //NavButton btnPicImport = new NavButton();
                    //btnPicImport.Caption = "导入图片";
                    //btnPicImport.Glyph = global::USL.Properties.Resources.Picture_Save_32px;
                    //btnPicImport.Name = "btnPicImport";
                    //btnPicImport.ElementClick += BtnPicImport_ElementClick;
                    //tileNavPane.Buttons.Add(btnPicImport);
                }
                //物料导入
                if (menu.Name == MainMenuConstants.Material)
                {
                    //导入
                    NavButton btnMaterialImport = new NavButton();
                    btnMaterialImport.Caption = "导入";
                    btnMaterialImport.Glyph = global::USL.Properties.Resources.ExportToXLS_32x32;
                    btnMaterialImport.Name = "btnMaterialImport";
                    btnMaterialImport.ElementClick += btnMaterialImport_ElementClick;
                    tileNavPane.Buttons.Add(btnMaterialImport);
                    //导入图片
                    NavButton btnPicImport = new NavButton();
                    btnPicImport.Caption = "导入图片";
                    btnPicImport.Glyph = global::USL.Properties.Resources.Picture_Save_32px;
                    btnPicImport.Name = "btnPicImport";
                    btnPicImport.ElementClick += BtnPicImport_ElementClick;
                    tileNavPane.Buttons.Add(btnPicImport);
                }
                if (MainForm.buttonPermissions.Exists(o => o.Name == "btnDel" && o.CheckBoxState == true))
                {
                    //删除
                    btnDel = new NavButton();
                    btnDel.Caption = "删除";
                    btnDel.Glyph = global::USL.Properties.Resources.del;
                    btnDel.Name = "btnDel";
                    btnDel.ElementClick += btnDel_ElementClick;
                    tileNavPane.Buttons.Add(btnDel);
                }
            }
            // 库存管理
            else if (menu.ParentID == new Guid("e11eb222-ba7c-460c-bfbe-8de849e89b14"))
            {
                if (menu.Name == MainMenuConstants.Stocktaking)
                {
                    //盘点导入
                    btnImportCheck = new NavButton();
                    //int status = GetStocktakingStatus();
                    btnImportCheck.Caption = "盘点导入";// string.Format("盘点导入【{0}】", EnumHelper.GetDescription<StocktakingStatusEnum>((StocktakingStatusEnum)status, false));
                    btnImportCheck.Glyph = global::USL.Properties.Resources.ExportToXLS_32x32;
                    btnImportCheck.Name = "btnImportCheck";
                    //btnCheckImport.ElementClick += btnImport_ElementClick;
                    btnImportCheck.IsMain = true;
                    tileNavPane.Buttons.Add(btnImportCheck);

                    panelStocktaking.Location = new Point(tileNavPane.Location.X, tileNavPane.Location.Y - tileNavPane.Height);
                    //panelStocktaking.Hide();
                    tileNavPane.OptionsPrimaryDropDown.Height = 1;
                    tileNavPane.OptionsPrimaryDropDown.CloseOnOuterClick = DevExpress.Utils.DefaultBoolean.False;
                    tileNavPane.DropDownShown += TileNavPane_DropDownShown;
                    tileNavPane.DropDownHidden += TileNavPane_DropDownHidden;
                    //tileNavPane.DefaultCategory.Items.Add(item);

                    // 生成差异表
                    NavButton btnDiff = new NavButton();
                    btnDiff.Caption = "生成差异表";
                    btnDiff.Glyph = global::USL.Properties.Resources.SaveAndNew_32x32;
                    btnDiff.Name = "btnDiff";
                    btnDiff.ElementClick += BtnDiff_ElementClick;
                    tileNavPane.Buttons.Add(btnDiff);
                }
                if (menu.Name == MainMenuConstants.ProfitAndLoss)
                {
                    // 更新盘点库存
                    //NavButton btnSTUpdate = new NavButton();
                    //btnSTUpdate.Caption = "更新盘点库存";
                    //btnSTUpdate.Glyph = global::USL.Properties.Resources.SaveAndNew_32x32;
                    //btnSTUpdate.Name = "btnSTUpdate";
                    //btnSTUpdate.ElementClick += btnSTUpdate_ElementClick;
                    //tileNavPane.Buttons.Add(btnSTUpdate);
                    // 导入在途商品
                    NavButton btnTransit = new NavButton();
                    btnTransit.Caption = "导入在途商品";
                    btnTransit.Glyph = global::USL.Properties.Resources.ExportToXLS_32x32;
                    btnTransit.Name = "btnTransit";
                    btnTransit.ElementClick += BtnTransit_ElementClick;
                    tileNavPane.Buttons.Add(btnTransit);
                    // 完成盘点
                    NavButton btnCheckFinish = new NavButton();
                    btnCheckFinish.Caption = "完成盘点";
                    btnCheckFinish.Glyph = global::USL.Properties.Resources.SaveAndNew_32x32;
                    btnCheckFinish.Name = "btnCheckFinish";
                    btnCheckFinish.ElementClick += BtnCheckFinish_ElementClick;
                    tileNavPane.Buttons.Add(btnCheckFinish);
                }
                if (menu.Name == MainMenuConstants.UnlistedGoods)
                {
                    // 完成未上架商品确认单
                    NavButton btnUnlistedFinish = new NavButton();
                    btnUnlistedFinish.Caption = "完成";
                    btnUnlistedFinish.Glyph = global::USL.Properties.Resources.SaveAndNew_32x32;
                    btnUnlistedFinish.Name = "btnUnlistedFinish";
                    btnUnlistedFinish.ElementClick += BtnUnlistedFinish_ElementClick;
                    tileNavPane.Buttons.Add(btnUnlistedFinish);
                }
            }
            // 报表管理
            else if (menu.ParentID == new Guid("51cb786a-d46d-46fe-8798-40170d09fb91")) {

                if (MainForm.buttonPermissions.Exists(o => o.Name == "btnHistoryQuery" && o.CheckBoxState == true))
                {
                    //历史记录查询
                    NavButton btnHistoryQuery = new NavButton();
                    btnHistoryQuery.Caption = "历史数据查询";
                    btnHistoryQuery.Glyph = global::USL.Properties.Resources.HistoryItem_32x32;
                    btnHistoryQuery.Name = "btnHistoryQuery";
                    btnHistoryQuery.ElementClick += btnHistoryQuery_ElementClick;
                    tileNavPane.Buttons.Add(btnHistoryQuery);
                }
            }
            if (menu.Name == MainMenuConstants.AttGeneralLog)
            {
                ////连接设备
                //btnConnect = new NavButton();
                //btnConnect.Caption = "连接设备";
                //btnConnect.Glyph = global::USL.Properties.Resources.switch_on_54px;
                //btnConnect.Name = "btnConnect";
                //btnConnect.ElementClick += btnConnect_ElementClick;
                //tileNavPane.Buttons.Add(btnConnect);
                ////下载考勤数据
                //NavButton btnDownloadAttLogs = new NavButton();
                //btnDownloadAttLogs.Caption = "下载数据";
                //btnDownloadAttLogs.Glyph = global::USL.Properties.Resources.Download_32x32;
                //btnDownloadAttLogs.Name = "btnDownloadAttLogs";
                //btnDownloadAttLogs.ElementClick += btnDownloadAttLogs_ElementClick;
                //tileNavPane.Buttons.Add(btnDownloadAttLogs);
                ////清除考勤数据
                //NavButton btnClearAttLogs = new NavButton();
                //btnClearAttLogs.Caption = "清除设备数据";
                //btnClearAttLogs.Glyph = global::USL.Properties.Resources.clear;
                //btnClearAttLogs.Name = "btnClearAttLogs";
                //btnClearAttLogs.ElementClick += btnClearAttLogs_ElementClick;
                //tileNavPane.Buttons.Add(btnClearAttLogs);
                if (MainForm.buttonPermissions.Exists(o => o.Name == "btnPrint" && o.CheckBoxState == true))
                {
                    //打印
                    NavButton btnPrint = new NavButton();
                    btnPrint.Caption = "打印";
                    btnPrint.Glyph = global::USL.Properties.Resources.print;
                    btnPrint.Name = "btnPrint";
                    btnPrint.ElementClick += btnPrint_ElementClick;
                    tileNavPane.Buttons.Add(btnPrint);
                }
                return;
            }
            if (menu.Name == MainMenuConstants.PermissionSetting)
            {
                if (MainForm.buttonPermissions.Exists(o => o.Name == "btnSave" && o.CheckBoxState == true))
                {
                    //保存
                    btnSave = new NavButton();
                    btnSave.Caption = "保存";
                    btnSave.Glyph = global::USL.Properties.Resources.Save_32x32;
                    btnSave.Name = "btnSave";
                    btnSave.ElementClick += btnSave_ElementClick;
                    tileNavPane.Buttons.Add(btnSave);
                }
                return;
            }

            if (MainForm.buttonPermissions.Exists(o => o.Name == "btnPrint" && o.CheckBoxState == true))
            {
                //打印
                NavButton btnPrint = new NavButton();
                btnPrint.Caption = "打印";
                btnPrint.Glyph = global::USL.Properties.Resources.print;
                btnPrint.Name = "btnPrint";
                btnPrint.ElementClick += btnPrint_ElementClick;
                tileNavPane.Buttons.Add(btnPrint);
            }
            if (MainForm.buttonPermissions.Exists(o => o.Name == "btnPrint" && o.CheckBoxState == true) && (menu.Name == MainMenuConstants.WageBill || menu.Name==MainMenuConstants.AttWageBill))
            {
                //打印日程工资明细
                NavButton btnAPTPrint = new NavButton();
                btnAPTPrint.Caption = "打印日程工资明细";
                btnAPTPrint.Glyph = global::USL.Properties.Resources.print;
                btnAPTPrint.Name = "btnAPTPrint";
                btnAPTPrint.ElementClick += btnAPTPrint_ElementClick;
                tileNavPane.Buttons.Add(btnAPTPrint);
            }
            //if (menu.Name == MainMenuConstants.Goods || menu.Name == MainMenuConstants.Material && (MainForm.buttonPermissions.Exists(o => o.Name == "btnDel" && o.CheckBoxState == true)))
            //{
            //    //停产
            //    NavButton btnStop = new NavButton();
            //    btnStop.Caption = "停产";
            //    btnStop.Glyph = global::USL.Properties.Resources.switch_off_54px;
            //    btnStop.Name = "btnStop";
            //    btnStop.ElementClick += BtnStop_ElementClick;
            //    tileNavPane.Buttons.Add(btnStop);
            //}
            if (menu.Name == MainMenuConstants.UsersInfo)
            {
                ////连接设备
                //btnConnect = new NavButton();
                //btnConnect.Caption = "连接设备";
                //btnConnect.Glyph = global::USL.Properties.Resources.switch_on_54px;
                //btnConnect.Name = "btnConnect";
                //btnConnect.ElementClick += btnConnect_ElementClick;
                //tileNavPane.Buttons.Add(btnConnect);
                ////上传人员信息
                //NavButton btnUploadStaff = new NavButton();
                //btnUploadStaff.Caption = "上传人员";
                //btnUploadStaff.Glyph = global::USL.Properties.Resources.UploadStaff_32px;
                //btnUploadStaff.Name = "btnUploadStaff";
                //btnUploadStaff.ElementClick += btnUploadStaff_ElementClick;
                //tileNavPane.Buttons.Add(btnUploadStaff);
            }
            //if (MainForm.buttonPermissions.Exists(o => o.Name == "btnInvalid" && o.CheckBoxState == true))
            //{
            //    //作废
            //    btnInvalid = new NavButton();
            //    btnInvalid.Caption = "作废";
            //    btnInvalid.Glyph = global::USL.Properties.Resources.del;
            //    btnInvalid.Name = "btnInvalid";
            //    btnInvalid.ElementClick += btnInvalid_ElementClick;
            //    tileNavPane.Buttons.Add(btnInvalid);
            //}
            //if (MainForm.buttonPermissions.Exists(o => o.Name == "btnCancelAudit" && o.CheckBoxState == true))
            //{
            //    //取消审核
            //    btnCancelAudit = new NavButton();
            //    btnCancelAudit.Caption = "取消审核";
            //    btnCancelAudit.Glyph = global::USL.Properties.Resources.del;
            //    btnCancelAudit.Name = "btnCancelAudit";
            //    btnCancelAudit.ElementClick += btnCancelAudit_ElementClick;
            //    tileNavPane.Buttons.Add(btnCancelAudit);
            //}
        }

        /// <summary>
        /// 完成未上架商品确认单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUnlistedFinish_ElementClick(object sender, NavElementEventArgs e)
        {
            WaitDialogForm waitDialogForm = null;
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                using (TransactionScope ts = new TransactionScope())
                {
                    List<UnlistedGoods> ugList = MainForm.ConvertList<UnlistedGoods>((IList)MainForm.dataSourceList[typeof(UnlistedGoods)]);
                    if (ugList == null || ugList.Count == 0)
                    {
                        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "没有未上架数据。");
                        return;
                    }
                    System.Windows.Forms.DialogResult result = XtraMessageBox.Show("确定要完成未上架商品确认单操作吗?\r\n一旦完成，数据将不能再更改。", "操作提示",
                    System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        new Thread((ThreadStart)delegate {
                            waitDialogForm = new WaitDialogForm("请稍候...", "正在保存数据", new Size(300, 60), this.FindForm());
                            System.Windows.Forms.Application.Run(waitDialogForm);
                        }).Start();
                        // 保存未上架商品确认单日志表
                        List<UnlistedGoodsLog> dtlList = new List<UnlistedGoodsLog>();
                        ugList.ForEach(o =>
                        {
                            UnlistedGoodsLog dtl = new UnlistedGoodsLog();
                            foreach (PropertyInfo p in dtl.GetType().GetProperties())
                            {
                                // 同名属性赋值
                                if (o.GetType().GetProperty(p.Name) != null)
                                    p.SetValue(dtl, o.GetType().GetProperty(p.Name).GetValue(o, null), null);
                            }
                            dtlList.Add(dtl);
                        });
                        BLLFty.Create<InventoryBLL>().FinishUnlistedGoods(dtlList);
                        MainForm.BillSaveRefresh<UnlistedGoods>();
                        MainForm.BillSaveRefresh<VUnlistedGoodsLog>();
                        CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "已完成盘点");
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            finally
            {
                if (waitDialogForm != null)
                    waitDialogForm.Invoke((EventHandler)delegate { waitDialogForm.Close(); });
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        /// <summary>
        /// 导入在途商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTransit_ElementClick(object sender, NavElementEventArgs e)
        {
            string code = string.Empty;
            int iError = 1;
            WaitDialogForm waitDialogForm = null;
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                List<ProfitAndLoss> plList = MainForm.ConvertList<ProfitAndLoss>((IList)MainForm.dataSourceList[typeof(ProfitAndLoss)]);
                if (plList == null || plList.Count == 0)
                {
                    CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "没有盘点差异数据。");
                    return;
                }
                var openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "Excel 97-2003 工作簿|*.xls*|Excel 工作簿|*.xlsx|所有文件|*.*";
                openFileDialog.FilterIndex = 1;
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    new Thread((ThreadStart)delegate {
                        waitDialogForm = new WaitDialogForm("请稍候...", "正在加载数据", new Size(300, 60), this.FindForm());
                        System.Windows.Forms.Application.Run(waitDialogForm);
                    }).Start();
                    DataSet ds = ExcelHelper.ImportExcel(openFileDialog.FileName);
                    List<ProfitAndLoss> updateList = new List<ProfitAndLoss>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ++iError;
                        code = row[EnumHelper.GetDescription<TransitEnum>(TransitEnum.Code, false)].ToString().Trim();
                        //检查货号是否存在
                        if (iError > ds.Tables[0].Rows.Count)
                            continue;
                        if (string.IsNullOrWhiteSpace(code))
                        {
                            CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("第{0}行的编码不能为空。", iError));
                            return;
                        }
                        ProfitAndLoss entity = plList.FirstOrDefault(o => o.GoodsCode.Equals(code));
                        if (entity != null)
                        {
                            entity.TransitQty = Convert.ToInt32(row[EnumHelper.GetDescription<TransitEnum>(TransitEnum.Qty, false)]);
                            entity.TransitAMT = Math.Round((decimal)entity.TransitQty * entity.Price, 2);
                            updateList.Add(entity);
                        }
                    }
                    if (updateList.Count > 0)
                    {
                        BLLFty.Create<InventoryBLL>().UpdateProfitAndLoss(updateList);                        
                        MainForm.DataPageRefresh<ProfitAndLoss>();
                        // 计算实际差异
                        MainForm.calcFinalDiff();
                        CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "导入成功");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("第{0}行的编码[{1}]的记录格式错误。错误信息：\r\n", iError, code) + "\r\n" + ex.Message);
                return;
            }
            finally
            {
                if (waitDialogForm != null)
                    waitDialogForm.Invoke((EventHandler)delegate { waitDialogForm.Close(); });
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        /// <summary>
        /// 完成盘点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCheckFinish_ElementClick(object sender, NavElementEventArgs e)
        {
            WaitDialogForm waitDialogForm = null;
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                using (TransactionScope ts = new TransactionScope())
                {
                    List<ProfitAndLoss> plList = MainForm.ConvertList<ProfitAndLoss>((IList)MainForm.dataSourceList[typeof(ProfitAndLoss)]);
                    if (plList == null || plList.Count == 0)
                    {
                        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "没有盘点差异数据。");
                        return;
                    }
                    System.Windows.Forms.DialogResult result = XtraMessageBox.Show("确定要完成盘点操作吗?\r\n一旦完成，差异表数据将不能再更改。", "操作提示",
                    System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        new Thread((ThreadStart)delegate {
                            waitDialogForm = new WaitDialogForm("请稍候...", "正在保存数据", new Size(300, 60), this.FindForm());
                            System.Windows.Forms.Application.Run(waitDialogForm);
                        }).Start();
                        SystemStatus systemStatus = MainForm.GetMaxBillNo(MainMenuConstants.Stocktaking, false);
                        // 保存盘点日志表头
                        StocktakingLogHd hd = new StocktakingLogHd();
                        hd.ID = Guid.NewGuid();
                        hd.BillNo = systemStatus.MaxBillNo;
                        hd.Checker = MainForm.usersInfo.Name;
                        hd.BillDate = DateTime.Now;
                        hd.CheckingTime = string.Empty;
                        hd.Status = (int)StocktakingStatusEnum.Finish;
                        // 保存系统状态表(完成盘点后将状态改为初盘，准备下一轮盘点)
                        systemStatus.Status = (int)StocktakingStatusEnum.First;
                        //if (btnImportCheck != null)
                            //btnImportCheck.Caption = string.Format("盘点导入【{0}】", EnumHelper.GetDescription<StocktakingStatusEnum>((StocktakingStatusEnum)systemStatus.Status, false));
                        // 保存差异日志表
                        List<ProfitAndLossLog> dtlList = new List<ProfitAndLossLog>();
                        plList.ForEach(o =>
                        {
                            ProfitAndLossLog dtl = new ProfitAndLossLog();
                            dtl.HdID = hd.ID;
                            foreach (PropertyInfo p in dtl.GetType().GetProperties())
                            {
                                // 同名属性赋值
                                if (o.GetType().GetProperty(p.Name) != null)
                                    p.SetValue(dtl, o.GetType().GetProperty(p.Name).GetValue(o, null), null);
                            }
                            dtlList.Add(dtl);
                        });
                        // 保存未上架商品确认单
                        List<VStocktakingLog> vList = BLLFty.Create<InventoryBLL>().GetVStocktakingLog(systemStatus.MaxBillNo);
                        List<UnlistedGoods> insertUGList = new List<UnlistedGoods>();
                        //List<UnlistedGoods> updateUGList = new List<UnlistedGoods>();
                        //List<UnlistedGoods> unlisteds = MainForm.ConvertList<UnlistedGoods>((IList)MainForm.dataSourceList[typeof(UnlistedGoods)]);
                        // 仓库内的盘点商品
                        List<VStocktakingLog> warehouseCheck = vList.FindAll(o => o.WarehouseID == new Guid(EnumHelper.GetDescription<WarehouseEnum>(WarehouseEnum.Warehouse, true))).ToList();
                        // 卖场里的盘点商品
                        List<VStocktakingLog> shopCheck = vList.FindAll(o => o.WarehouseID == new Guid(EnumHelper.GetDescription<WarehouseEnum>(WarehouseEnum.Shop, true))).ToList();
                        warehouseCheck.ForEach(item => {
                            VStocktakingLog stocktaking = shopCheck.FirstOrDefault(o => o.DeptID.Equals(item.DeptID) && o.GoodsCode.Equals(item.GoodsCode));
                            if (stocktaking == null) // 未上架商品
                            {
                                //UnlistedGoods obj = unlisteds.FirstOrDefault(o => o.DeptID.Equals(item.DeptID) && o.GoodsCode.Equals(item.GoodsCode));
                                //if (obj == null)
                                //{
                                    UnlistedGoods entity = new UnlistedGoods();
                                    foreach (PropertyInfo p in entity.GetType().GetProperties())
                                    {
                                        // 同名属性赋值
                                        if (item.GetType().GetProperty(p.Name) != null)
                                            p.SetValue(entity, item.GetType().GetProperty(p.Name).GetValue(item, null), null);
                                    }
                                entity.ID = Guid.NewGuid();
                                entity.HdID = hd.ID;
                                entity.StockAMT = Math.Round((decimal)item.StockQty * entity.Price, 2);
                                entity.UnlistedQty = item.CheckQty;
                                entity.UnlistedAMT = Math.Round((decimal)item.CheckQty * entity.Price, 2);
                                insertUGList.Add(entity);
                                //}
                                //else
                                //{
                                //    obj.StockQty = item.StockQty;
                                //}
                            }
                        });
                        BLLFty.Create<InventoryBLL>().FinishCheck(systemStatus, hd, dtlList, insertUGList);
                        MainForm.BillSaveRefresh<StocktakingLogHd>();
                        MainForm.BillSaveRefresh<ProfitAndLoss>();
                        MainForm.BillSaveRefresh<VProfitAndLossLog>();
                        MainForm.BillSaveRefresh<UnlistedGoods>();
                        CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "已完成盘点");
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            finally
            {
                if (waitDialogForm != null)
                    waitDialogForm.Invoke((EventHandler)delegate { waitDialogForm.Close(); });
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            
        }

        /// <summary>
        /// 生成差异表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDiff_ElementClick(object sender, NavElementEventArgs e)
        {
            WaitDialogForm waitDialogForm = null;
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                using (TransactionScope ts = new TransactionScope())
                {
                    List<Stocktaking> list =MainForm.ConvertList<Stocktaking>((IList)MainForm.dataSourceList[typeof(Stocktaking)]);
                    if (list == null || list.Count == 0)
                    {
                        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "没有盘点数据，请先导入。");
                        return;
                    }
                    System.Windows.Forms.DialogResult result = XtraMessageBox.Show("确定要生成差异表吗?", "操作提示",
                System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        new Thread((ThreadStart)delegate {
                            waitDialogForm = new WaitDialogForm("请稍候...", "正在加载数据", new Size(300, 60), this.FindForm());
                            System.Windows.Forms.Application.Run(waitDialogForm);
                        }).Start();
                        
                        StocktakingStatusEnum status = StocktakingStatusEnum.First;
                        SystemStatus systemStatus = MainForm.ConvertList<SystemStatus>((IList)MainForm.dataSourceList[typeof(SystemStatus)]).FirstOrDefault(o => o.MainMenuName.Equals(MainMenuConstants.Stocktaking));
                        if (systemStatus != null)
                            status = (StocktakingStatusEnum)systemStatus.Status;
                        // 初盘创建新单，复盘单号和初盘一致
                        systemStatus = MainForm.GetMaxBillNo(MainMenuConstants.Stocktaking,
                            status == StocktakingStatusEnum.First ? true : false);
                        // 保存盘点日志表头
                        StocktakingLogHd hd = new StocktakingLogHd();
                        hd.ID = Guid.NewGuid();
                        hd.BillNo = systemStatus.MaxBillNo;
                        hd.Checker = MainForm.usersInfo.Name;
                        hd.BillDate = DateTime.Now;
                        TimeSpan timeSpan = list.Max(o => o.CheckingDate).Value - list.Min(o => o.CheckingDate).Value;
                        hd.CheckingTime = string.Format("{0}小时{1}分钟{2}秒", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                        hd.Status = systemStatus.Status;// 先保存当前状态，再更新systemStatus
                        // 保存系统状态表(导入盘点阶段只有初盘和复盘两种状态，保存差异表后更改盘点状态：只能保存一次初盘，可以多次复盘)
                        if ((StocktakingStatusEnum)systemStatus.Status != StocktakingStatusEnum.Check)
                            systemStatus.Status = (int)StocktakingStatusEnum.Check;
                        if (btnImportCheck != null)
                            btnImportCheck.Caption = string.Format("盘点导入【{0}】", EnumHelper.GetDescription<StocktakingStatusEnum>((StocktakingStatusEnum)systemStatus.Status, false));
                        // 保存盘点日志表体
                        List<StocktakingLogDtl> dtlList = new List<StocktakingLogDtl>();
                        list.ForEach(o =>
                        {
                            StocktakingLogDtl dtl = new StocktakingLogDtl();
                            dtl.HdID = hd.ID;
                            foreach (PropertyInfo p in dtl.GetType().GetProperties())
                            {
                                // 同名属性赋值
                                if (o.GetType().GetProperty(p.Name) != null)
                                    p.SetValue(dtl, o.GetType().GetProperty(p.Name).GetValue(o, null), null);
                            }
                            dtlList.Add(dtl);
                        });
                        // 保存差异表
                        List<Goods> goodsList = MainForm.ConvertList<Goods>(((IList)MainForm.dataSourceList[typeof(Goods)])).FindAll(o => o.Type == 0);
                        #region 注释
                        //var diffList = list.GroupBy(p => new { p.DeptID, p.GoodsCode }).Select(s => (new
                        //{
                        //    DeptID = s.Key.DeptID,
                        //    DeptCode = s.FirstOrDefault().DeptCode,
                        //    DeptName = s.FirstOrDefault().DeptName,
                        //    GoodsCode = s.Key.GoodsCode,
                        //    GoodsName = s.FirstOrDefault().GoodsName,
                        //    Barcode = s.FirstOrDefault().Barcode,
                        //    SPEC = s.FirstOrDefault().SPEC,
                        //    Unit = s.FirstOrDefault().Unit,
                        //    StockQty = s.FirstOrDefault().StockQty,// s.Sum(item => item.StockQty),
                        //    CheckQty = s.Sum(item => item.CheckQty),
                        //    Price = s.FirstOrDefault().Price,
                        //    ScanBill = s.FirstOrDefault().ScanBill,
                        //    Checker = s.FirstOrDefault().Checker,
                        //    CheckingDate = s.FirstOrDefault().CheckingDate,
                        //}));
                        //var ll= diffList.GroupJoin(goodsList, st => st.GoodsCode, g => g.Code, (st, g) => new { st, g }).SelectMany(z => z.g.DefaultIfEmpty(), (x, y) => new { Stocktaking = x.st, goods = y }).Select(res => new ProfitAndLoss
                        // {
                        //     ID = Guid.NewGuid(),
                        //     DeptID = res.Stocktaking.DeptID,
                        //     DeptCode = res.Stocktaking.DeptCode,
                        //     DeptName = res.Stocktaking.DeptName,
                        //     GoodsCode = res.Stocktaking.GoodsCode,
                        //     GoodsName = res.Stocktaking.GoodsName,
                        //     Price = res.Stocktaking.Price,
                        //     StockQty = res.goods==null?0:res.goods.Qty.Value,
                        //     StockAMT = Math.Round((decimal)(res.goods == null ? 0 : res.goods.Qty) * res.Stocktaking.Price, 2),
                        //     CheckQty = res.Stocktaking.CheckQty,
                        //     CheckAMT = Math.Round((decimal)res.Stocktaking.CheckQty * res.Stocktaking.Price, 2),
                        //     DiffQty = res.Stocktaking.CheckQty - (res.goods == null ? 0 : res.goods.Qty.Value),
                        //     DiffAMT = Math.Round((decimal)(res.Stocktaking.CheckQty - (res.goods == null ? 0 : res.goods.Qty.Value)) * res.Stocktaking.Price, 2)
                        // }).ToList();
                        #endregion
                        var diffList = list.GroupBy(p => new { p.DeptID, p.GoodsCode }).GroupJoin(goodsList, st => st.Key.GoodsCode, g => g.Code, (st, g) => new { st, g }).SelectMany(
                            z => z.g.DefaultIfEmpty(), (x, y) => new { Stocktaking = x.st, goods = y }).Select(res =>
                            new ProfitAndLoss
                            {
                                ID = Guid.NewGuid(),
                                DeptID = res.Stocktaking.Key.DeptID,
                                DeptCode = res.Stocktaking.FirstOrDefault().DeptCode,
                                DeptName = res.Stocktaking.FirstOrDefault().DeptName,
                                Category = res.goods == null ? string.Empty : res.goods.Category,
                                GoodsCode = res.Stocktaking.FirstOrDefault().GoodsCode,
                                GoodsName = res.Stocktaking.FirstOrDefault().GoodsName,
                                Price = res.goods == null ? 0 : res.goods.Price,
                                StockQty = res.goods == null ? 0 : res.goods.Qty.Value,
                                StockAMT = Math.Round((decimal)(res.goods == null ? 0 : res.goods.Qty) * (res.goods == null ? 0 : res.goods.Price), 2),
                                CheckQty = res.Stocktaking.Sum(item => item.CheckQty),
                                CheckAMT = Math.Round((decimal)res.Stocktaking.Sum(item => item.CheckQty) * (res.goods == null ? 0 : res.goods.Price), 2),
                                DiffQty = res.Stocktaking.Sum(item => item.CheckQty) - (res.goods == null ? 0 : res.goods.Qty.Value),
                                DiffAMT = Math.Round((decimal)(res.Stocktaking.Sum(item => item.CheckQty) - (res.goods == null ? 0 : res.goods.Qty.Value * res.goods.Price)), 2)
                            }).ToList();
                        List<ProfitAndLoss> insertList = new List<ProfitAndLoss>();
                        List<ProfitAndLoss> updateList = new List<ProfitAndLoss>();
                        List<ProfitAndLoss> plList = MainForm.ConvertList<ProfitAndLoss>((IList)MainForm.dataSourceList[typeof(ProfitAndLoss)]);
                        diffList.ForEach(o =>
                        {
                            ProfitAndLoss item = plList.FirstOrDefault(p => p.DeptID.Equals(o.DeptID) && p.GoodsCode.Equals(o.GoodsCode));
                            if (item == null)
                            {
                                insertList.Add(o);
                            }
                            else
                            {
                                item.StockQty = o.StockQty;
                                item.StockAMT = o.StockAMT;
                                item.CheckQty = o.CheckQty;
                                item.CheckAMT = o.CheckAMT;
                                item.DiffQty = o.DiffQty;
                                item.DiffAMT = o.DiffAMT;
                                updateList.Add(item);
                            }
                        });
                        BLLFty.Create<InventoryBLL>().SaveProfitAndLoss(systemStatus, hd, dtlList, insertList, updateList);
                        IList refresh = MainForm.DataPageRefresh<ProfitAndLoss>();
                        MainForm.DataPageRefresh<Stocktaking>();
                        //定位
                        MainForm.SetSelected(pageGroupCore, MainForm.mainMenuList[MainMenuConstants.ProfitAndLoss]);
                        DataQueryPage page = MainForm.itemDetailPageList[MainMenuConstants.ProfitAndLoss].itemDetail as DataQueryPage;
                        page.InitGrid(refresh);
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                //XtraMessageBox.Show(ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (waitDialogForm != null)
                    waitDialogForm.Invoke((EventHandler)delegate { waitDialogForm.Close(); });
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void TileNavPane_DropDownShown(object sender, DropDownEventArgs e)
        {
            panelStocktaking.Width =  tileNavPane.Width-20;
            lueDept.EditValue = null;
            lueWarehouse.EditValue = null;
            panelStocktaking.Show();
        }

        private void TileNavPane_DropDownHidden(object sender, DropDownEventArgs e)
        {
            panelStocktaking.Hide();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            List<UnlistedGoods> ugList = MainForm.ConvertList<UnlistedGoods>((IList)MainForm.dataSourceList[typeof(UnlistedGoods)]);
            if (ugList.Count > 0)
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "有未上架商品确认单尚未处理，请先处理完再执行盘点导入。");
                return;
            }
            if (validated())
            {
                //实现导入功能
                Dictionary<string, object> dict = new Dictionary<string, object>();
                List<Department> deptList =MainForm.ConvertList<Department>((IList)MainForm.dataSourceList[typeof(Department)]);
                List<Warehouse> warehouseList = MainForm.ConvertList<Warehouse>((IList)MainForm.dataSourceList[typeof(Warehouse)]);
                dict.Add(MainMenuConstants.Department, deptList.FirstOrDefault(o => o.ID.Equals(new Guid(lueDept.EditValue.ToString()))));
                dict.Add(MainMenuConstants.Warehouse, warehouseList.FirstOrDefault(o => o.ID.Equals(new Guid(lueWarehouse.EditValue.ToString()))));
                iExtensions.SendData(dict);
                tileNavPane.HideDropDownWindow();
            }
        }

        bool validated()
        {
            CommonServices.ErrorTrace.Clear();
            bool flag = true;
            if (string.IsNullOrEmpty(lueDept.Text.Trim()))
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "请填写" + lciDept.Text);
                flag = false;
            }
            if (string.IsNullOrEmpty(lueWarehouse.Text.Trim()))
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "请填写" + lciWarehouse.Text);
                flag = false;
            }
            return flag;
        }

            private void BtnGetMoldOrder_ElementClick(object sender, NavElementEventArgs e)
        {
            iExtensions.SendData(BOMType.MoldList);
        }

        private void BtnOEMGetBack_ElementClick(object sender, NavElementEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                object currentObj = iExtensions.ReceiveData();


                if (menu.Name == MainMenuConstants.ProductionOrderQuery && currentObj != null)
                {
                    VProductionOrder order = currentObj as VProductionOrder;
                    OrderHd hd = BLLFty.Create<OrderBLL>().GetOrderHd(order.HdID);
                    List<VProductionOrder> dtl = MainForm.ConvertList<VProductionOrder>((IList)MainForm.dataSourceList[typeof(VProductionOrder)]).FindAll(o => o.HdID == order.HdID);
                    //外加工回收单表头数据
                    StockInBillHd inHd = new StockInBillHd();
                    inHd.ID = Guid.NewGuid();
                    inHd.BillNo = MainForm.GetBillMaxBillNo(MainMenuConstants.StockInBillType, "RK");
                    inHd.WarehouseID = hd.WarehouseID;
                    inHd.WarehouseType = hd.WarehouseType;
                    inHd.OrderID = hd.ID;
                    inHd.OrderDate = hd.OrderDate;
                    inHd.CompanyID = hd.CompanyID;
                    inHd.Contacts = hd.Contacts;
                    inHd.SupplierID = hd.SupplierID;
                    inHd.BillDate = DateTime.Now;
                    inHd.Maker = hd.Maker;
                    inHd.MakeDate = hd.MakeDate;
                    inHd.Remark = hd.Remark;
                    inHd.Type = 3;  //外加工回收
                    inHd.Status = 0;
                    inHd.BillAMT = hd.BillAMT;
                    inHd.UnPaidAMT = hd.UnReceiptedAMT;

                    List<StockInBillDtl> inDtlList = new List<StockInBillDtl>();
                    foreach (VProductionOrder dtlItem in dtl)
                    {
                        //外加工回收单明细数据
                        StockInBillDtl inDtl = new StockInBillDtl();
                        inDtl.ID = Guid.NewGuid();
                        inDtl.HdID = inHd.ID;
                        inDtl.SerialNo = dtlItem.SerialNo;
                        inDtl.GoodsID = dtlItem.GoodsID;
                        inDtl.Qty = dtlItem.未收箱数.Value;
                        inDtl.MEAS = dtlItem.外箱规格;
                        inDtl.PCS = dtlItem.装箱数;
                        inDtl.InnerBox = dtlItem.内盒;
                        inDtl.NWeight = dtlItem.净重;
                        inDtl.Price = dtlItem.单价;
                        inDtl.Discount = 1;
                        inDtl.OtherFee = 0;
                        inDtlList.Add(inDtl);
                    }

                    BLLFty.Create<StockInBillBLL>().Insert(inHd, inDtlList);
                    //////MainForm.BillSaveRefresh<>(MainMenuConstants.FGStockInBillQuery);

                    //定位
                    MainForm.SetSelected(pageGroupCore, MainForm.mainMenuList[MainMenuConstants.FGStockInBill]);
                    StockInBillPage page = MainForm.itemDetailPageList[MainMenuConstants.FGStockInBill].itemDetail as StockInBillPage;
                    page.BindData(inHd.ID);
                }
                else if (menu.Name == MainMenuConstants.SalesReturnBillQuery && currentObj != null)
                {
                    VStockInBill bill = currentObj as VStockInBill;
                    StockInBillHd hd = BLLFty.Create<StockInBillBLL>().GetStockInBillHd(bill.HdID);
                    List<StockInBillDtl> dtlByBOM = BLLFty.Create<StockInBillBLL>().GetVStockInBillDtlByBOM(bill.HdID, (int)BOMType.BOM);
                    //退料单表头数据
                    StockInBillHd inHd = new StockInBillHd();
                    inHd.ID = Guid.NewGuid();
                    inHd.BillNo = MainForm.GetBillMaxBillNo(MainMenuConstants.StockInBillType, "RK");
                    inHd.WarehouseID = MainForm.WarehouseList.FirstOrDefault(o => o.Code == WarehouseConstants.SFG).ID;  //半成品
                    inHd.WarehouseType = hd.WarehouseType;
                    inHd.OrderID = hd.ID;
                    inHd.OrderDate = hd.OrderDate;
                    inHd.CompanyID = hd.CompanyID;
                    inHd.Contacts = hd.Contacts;
                    inHd.SupplierID = hd.SupplierID;
                    inHd.BillDate = DateTime.Now;
                    inHd.Maker = hd.Maker;
                    inHd.MakeDate = hd.MakeDate;
                    inHd.Remark = hd.Remark;
                    inHd.Type = 10;  //客户退料
                    inHd.Status = 0;
                    inHd.BillAMT = hd.BillAMT;
                    inHd.UnPaidAMT = hd.UnPaidAMT;

                    List<StockInBillDtl> inDtlList = new List<StockInBillDtl>();
                    foreach (StockInBillDtl dtlItem in dtlByBOM)
                    {
                        //退料单明细数据
                        StockInBillDtl inDtl = new StockInBillDtl();
                        inDtl.ID = Guid.NewGuid();
                        inDtl.HdID = inHd.ID;
                        inDtl.SerialNo = dtlItem.SerialNo;
                        inDtl.GoodsID = dtlItem.GoodsID;
                        inDtl.Qty = bill.箱数;
                        inDtl.MEAS = dtlItem.MEAS;
                        inDtl.PCS = dtlItem.PCS;
                        inDtl.InnerBox = dtlItem.InnerBox;
                        inDtl.NWeight = dtlItem.NWeight;
                        inDtl.Price = dtlItem.Price;
                        inDtl.Discount = 1;
                        inDtl.OtherFee = 0;
                        inDtlList.Add(inDtl);
                    }

                    BLLFty.Create<StockInBillBLL>().Insert(inHd, inDtlList);
                    //////MainForm.BillSaveRefresh(MainMenuConstants.ReturnedMaterialBillQuery);
                    //////MainForm.BillSaveRefresh(MainMenuConstants.SalesReturnBillQuery);

                    //定位
                    MainForm.SetSelected(pageGroupCore, MainForm.mainMenuList[MainMenuConstants.ReturnedMaterialBill]);
                    StockInBillPage page = MainForm.itemDetailPageList[MainMenuConstants.ReturnedMaterialBill].itemDetail as StockInBillPage;
                    page.BindData(inHd.ID);
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

        private void BtnPicImport_ElementClick(object sender, NavElementEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string _path = dialog.SelectedPath;
                    if (_path.Length > 0)
                    {
                        if (!System.IO.Directory.Exists(MainForm.DownloadFilePath))
                            System.IO.Directory.CreateDirectory(MainForm.DownloadFilePath);
                        string[] files = Directory.GetFiles(_path);//, "*.png", SearchOption.AllDirectories);
                        List<Goods> goodsList = MainForm.ConvertList<Goods>((IList)MainForm.dataSourceList[typeof(Goods)]);
                        //bool result = true;
                        foreach (Goods item in goodsList)
                        {
                            string downloadFileName = MainForm.DownloadFilePath + String.Format("{0}.jpg", item.Code);
                            string fileName = files.FirstOrDefault(o => Path.GetFileNameWithoutExtension(o.ToString()) == item.Code);
                            if (item.Pic == null && !string.IsNullOrEmpty(fileName))
                            {
                                //上传文件
                                FtpUpDown ftpUpDown = new FtpUpDown(MainForm.ServerUrl, MainForm.ServerUserName, MainForm.ServerPassword);
                                string error = string.Empty;
                                bool flag = ftpUpDown.Upload(fileName, out error);
                                if (flag == false)
                                {
                                    CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("图片[{0}]上传失败:", item.Code) + error);
                                    //result = false;
                                    return;
                                }
                                //图片保存到本地下载目录
                                Image.FromFile(fileName).Save(downloadFileName);
                                item.Pic = ImageHelper.MakeBuff(ImageHelper.GetReducedImage(Image.FromFile(fileName), 24, 24));
                            }
                        }
                        //if (result == false)
                        //    return;
                        BLLFty.Create<GoodsBLL>().Update(goodsList);
                        MainForm.DataPageRefresh<Goods>();
                        MainForm.DataPageRefresh<VMaterial>();
                        CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "导入成功");
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

        private void BtnGetMaterial_ElementClick(object sender, NavElementEventArgs e)
        {
            iExtensions.ReceiveData();
        }

        private void BtnDeptImport_ElementClick(object sender, NavElementEventArgs e)
        {
            string code = string.Empty;
            int iError = 1;
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
                    List<Department> insertList = new List<Department>();
                    List<Department> updateList = new List<Department>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ++iError;
                        code = row[EnumHelper.GetDescription<DeptEnum>(DeptEnum.Code,false)].ToString().Trim();
                        //检查货号是否存在
                        if (iError > ds.Tables[0].Rows.Count)
                            continue;
                        if (string.IsNullOrWhiteSpace(code))
                        {
                            CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("第{0}行的编码不能为空。", iError));
                            return;
                        }
                        Department entity = MainForm.ConvertList<Department>((IList)MainForm.dataSourceList[typeof(Department)]).Find(o => o.Code == code);
                        bool isNew = false;
                        if (entity == null)
                        {
                            entity = new Department();
                            entity.ID = Guid.NewGuid();
                            entity.AddTime = DateTime.Now;
                            isNew = true;
                        }
                        //entity.Code = code;
                        foreach (PropertyInfo p in entity.GetType().GetProperties())
                        {
                            ListItem<DeptEnum> item = EnumHelper.GetEnumValues<DeptEnum>(false).FirstOrDefault(o => o.Value.ToString().Equals(p.Name));
                            if (item != null)
                            {
                                if (ds.Tables[0].Columns.Contains(item.Name))
                                {
                                    object cell = row[item.Name];
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
                            insertList.Add(entity);
                        else
                            updateList.Add(entity);
                    }
                    if (insertList.Count > 0 || updateList.Count > 0)
                    {
                        BLLFty.Create<DepartmentBLL>().Import(insertList, updateList);
                        MainForm.DataPageRefresh<Department>();
                        CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "导入成功");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("第{0}行的编码[{1}]的记录格式错误。错误信息：\r\n", iError, code) + "\r\n" + ex.Message);
                return;
            }
            finally
            {
                if (waitDialogForm != null)
                    waitDialogForm.Invoke((EventHandler)delegate { waitDialogForm.Close(); });
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        void btnGoodsImport_ElementClick(object sender, NavElementEventArgs e)
        {
            string goodsCode = string.Empty;
            int iError = 1;
            WaitDialogForm waitDialogForm = null;
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    var openFileDialog = new System.Windows.Forms.OpenFileDialog();
                    openFileDialog.Filter = "Excel 97-2003 工作簿|*.xls*|Excel 工作簿|*.xlsx|所有文件|*.*";
                    openFileDialog.FilterIndex = 1;
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    new Thread((ThreadStart)delegate
                    {
                        waitDialogForm = new WaitDialogForm("请稍候...", "正在导入数据", new Size(300, 60), this.FindForm());
                        System.Windows.Forms.Application.Run(waitDialogForm);
                    }).Start();

                    DataSet ds = ExcelHelper.ImportExcel(openFileDialog.FileName);
                    List<Goods> preList = MainForm.ConvertList<Goods>((IList)MainForm.dataSourceList[typeof(Goods)]);
                    // 先将原有商品的库存全部改为0
                    if (preList.Count > 0)
                    {
                        preList.ForEach(item =>
                        {
                            item.Qty = 0;
                        });
                        BLLFty.Create<GoodsBLL>().Update(preList);
                    }
                    List<Goods> InsertGoodsList = new List<Goods>();
                    List<Goods> UpdateGoodsList = new List<Goods>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ++iError;
                        goodsCode = row[EnumHelper.GetDescription<GoodsEnum>(GoodsEnum.Code, false)].ToString().Trim();
                        //检查货号是否存在
                        if (iError > ds.Tables[0].Rows.Count)
                            continue;
                        if (string.IsNullOrWhiteSpace(goodsCode))
                        {
                            CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("第{0}行的编码不能为空。", iError));
                            return;
                        }
                        Goods hasGoods = preList.Find(o => o.Code == goodsCode);
                        bool isNewGoods = false;
                        if (hasGoods == null)
                        {
                            hasGoods = new Goods();
                            hasGoods.ID = Guid.NewGuid();
                            hasGoods.AddTime = DateTime.Now;
                            isNewGoods = true;
                        }
                        hasGoods.Code = goodsCode;
                        foreach (PropertyInfo p in hasGoods.GetType().GetProperties())
                        {
                            ListItem<GoodsEnum> goodsEnum = EnumHelper.GetEnumValues<GoodsEnum>(false).FirstOrDefault(o => o.Value.ToString().Equals(p.Name));
                            if (goodsEnum != null)
                            {
                                if (ds.Tables[0].Columns.Contains(goodsEnum.Name))
                                {
                                    if (p.PropertyType.IsGenericType)
                                    {
                                        // 泛型Nullable<>
                                        Type genericTypeDefinition = p.PropertyType.GetGenericTypeDefinition();
                                        if (genericTypeDefinition == typeof(Nullable<>))
                                            p.SetValue(hasGoods, string.IsNullOrWhiteSpace(row[goodsEnum.Name].ToString()) ? null : Convert.ChangeType(row[goodsEnum.Name], Nullable.GetUnderlyingType(p.PropertyType)), null);
                                    }
                                    else
                                    {
                                        // 非泛型
                                        p.SetValue(hasGoods, string.IsNullOrWhiteSpace(row[goodsEnum.Name].ToString()) ? null : Convert.ChangeType(row[goodsEnum.Name], p.PropertyType), null);
                                    }
                                }
                            }
                        }
                        if (isNewGoods)
                            InsertGoodsList.Add(hasGoods);
                        else
                            UpdateGoodsList.Add(hasGoods);
                    }
                    if (InsertGoodsList.Count > 0 || UpdateGoodsList.Count > 0)
                    {
                        BLLFty.Create<GoodsBLL>().Import(InsertGoodsList, UpdateGoodsList);
                        MainForm.DataPageRefresh<Goods>();
                        CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "导入成功");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("第{0}行的编码[{1}]的记录格式错误。错误信息：\r\n", iError, goodsCode) + "\r\n" + ex.Message);
                return;
            }
            finally
            {
                if (waitDialogForm != null)
                    waitDialogForm.Invoke((EventHandler)delegate { waitDialogForm.Close(); });
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        void btnMaterialImport_ElementClick(object sender, NavElementEventArgs e)
        {
            string goodsCode = string.Empty;
            int iError = 1;
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                var openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "Excel 97-2003 工作簿|*.xls*|Excel 工作簿|*.xlsx|所有文件|*.*";
                openFileDialog.FilterIndex = 1;
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DataSet ds = ExcelHelper.ImportExcel(openFileDialog.FileName);
                    List<Goods> hasGoodsList = MainForm.ConvertList<Goods>((IList)MainForm.dataSourceList[typeof(Goods)]);
                    List<Goods> InsertGoodsList = new List<Goods>();
                    List<Goods> UpdateGoodsList = new List<Goods>();
                    Hashtable htGoods = new Hashtable();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ++iError;
                        if (string.IsNullOrEmpty(row["货品类型"].ToString().Trim()))
                        {
                            CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "货品类型不能为空。");
                            return;
                        }
                        //检查货品类型是否存在
                        GoodsType goodsType = BLLFty.Create<GoodsTypeBLL>().GetGoodsType().Find(o => o.Name.Contains(row["货品类型"].ToString().Trim()));
                        if (goodsType == null)
                        {
                            GoodsType gt = new GoodsType();
                            gt.ID = Guid.NewGuid();
                            gt.Code = Rexlib.GetSpellCode(row["货品类型"].ToString().Trim());
                            gt.Name = row["货品类型"].ToString().Trim();
                            goodsType = gt;
                            BLLFty.Create<GoodsTypeBLL>().Insert(gt);
                            //CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("找不到货品类型[{0}]，请先添加该货品类型。", row["货品类型"].ToString().Trim()));
                            //return;
                        }
                        //检查货号是否存在
                        Goods hasGoods = hasGoodsList.FirstOrDefault(o => o.Code == row["货号"].ToString().Trim());
                        bool isNewGoods = false;
                        if (hasGoods == null)
                        {
                            hasGoods = new Goods();
                            hasGoods.ID = Guid.NewGuid();
                            hasGoods.AddTime = DateTime.Now;
                            isNewGoods = true;
                        }
                        if (htGoods[row["货号"].ToString().Trim()] == null)
                        {
                            htGoods.Add(row["货号"].ToString().Trim(), hasGoods);
                        }
                        else
                        {
                            //((Goods)htGoods[row["货号"].ToString().Trim()]).NWeight += Convert.ToDecimal(row["模重"]) == 0 ? 1 : Convert.ToDecimal(row["模重"]);
                            //((Goods)htGoods[row["货号"].ToString().Trim()]).CavityNumber+= Convert.ToDecimal(row["出数"]) == 0 ? 1 : Convert.ToDecimal(row["出数"]);
                            continue;
                        }
                        //if (hasGoods == null)
                        //{
                        //Goods goods = new Goods();
                        //if (htGoods[row["货号"].ToString().Trim()] == null)
                        //{
                        //    htGoods.Add(row["货号"].ToString().Trim(), goods);
                        //}
                        //else
                        //{
                        //    //((Goods)htGoods[row["货号"].ToString().Trim()]).NWeight += Convert.ToDecimal(row["模重"]) == 0 ? 1 : Convert.ToDecimal(row["模重"]);
                        //    //((Goods)htGoods[row["货号"].ToString().Trim()]).CavityNumber+= Convert.ToDecimal(row["出数"]) == 0 ? 1 : Convert.ToDecimal(row["出数"]);
                        //    continue;
                        //}
                        goodsCode = row["货号"].ToString().Trim();

                        //goods.ID = Guid.NewGuid();
                        if (row["货品类型"].ToString().Trim().Contains("包装"))
                            hasGoods.Type = (int)GoodsBigType.SFGoods;
                        else if (row["货品类型"].ToString().Trim().Contains("胶件")
                            || row["货品类型"].ToString().Trim().Equals("半成品")
                            || row["货品类型"].ToString().Trim().Contains("配件")
                            || row["货品类型"].ToString().Trim().Contains("喷漆")
                            || row["货品类型"].ToString().Trim().Contains("电镀")
                            || row["货品类型"].ToString().Trim().Contains("移印")
                            || row["货品类型"].ToString().Trim().Contains("贴标"))
                            hasGoods.Type = (int)GoodsBigType.Stuff;
                        else if (row["货品类型"].ToString().Trim().Contains("原料"))
                            hasGoods.Type = (int)GoodsBigType.Material;
                        else if (row["货品类型"].ToString().Trim().Contains("筐袋"))
                            hasGoods.Type = (int)GoodsBigType.Basket;
                        else if (row["货品类型"].ToString().Trim().Equals("成品"))
                            continue;
                        else
                        {

                            CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("货品类型：[{0}]不存在。", row["货品类型"].ToString().Trim()));
                            return;
                        }
                        hasGoods.Code = row["货号"].ToString().Trim();
                        hasGoods.Name = row["品名"].ToString().Trim();
                        hasGoods.GoodsTypeID = goodsType.ID;
                            //goods.PackagingID = packaging.ID;
                            if (string.IsNullOrEmpty(row["单价"].ToString().Trim()))
                            hasGoods.Price = 0;
                            else
                            hasGoods.Price = Convert.ToDecimal(row["单价"]);
                        //goods.BarCode = Convert.ToString(++iBarCode);
                        hasGoods.PCS = 1;
                        hasGoods.CavityNumber = 1;
                            //goods.InnerBox = Convert.ToInt32(row["内盒"]);
                            if (!string.IsNullOrEmpty(row["单位"].ToString().Trim()))
                            hasGoods.Unit = row["单位"].ToString().Trim();
                            if (!string.IsNullOrEmpty(row["规格"].ToString().Trim()))
                            hasGoods.SPEC = row["规格"].ToString().Trim();
                            //goods.MEAS = row["外箱规格"].ToString().Trim();
                            ////条形码由货号+规格中的字母和数字组成
                            //goods.BarCode = Regex.Replace(goods.Code + goods.SPEC, "[*-.]", "", RegexOptions.IgnoreCase);
                            //if (!string.IsNullOrEmpty(row["毛重"].ToString().Trim()))
                            //    goods.GWeight = Convert.ToDecimal(row["毛重"]);
                            if (string.IsNullOrEmpty(row["模重"].ToString().Trim()))
                            hasGoods.NWeight = 1;
                            else
                            hasGoods.NWeight = Convert.ToDecimal(row["模重"]) == 0 ? 1 : Convert.ToDecimal(row["模重"]);
                            if (string.IsNullOrEmpty(row["出数"].ToString().Trim()))
                            hasGoods.CavityNumber = 1;
                            else
                            hasGoods.CavityNumber = Convert.ToDecimal(row["出数"]) == 0 ? 1 : Convert.ToDecimal(row["出数"]);
                            if (row["货品类型"].ToString().Trim().Contains("原料"))
                            {
                            hasGoods.NWeight = 1;
                            hasGoods.CavityNumber = 1;
                            }
                        //if (!string.IsNullOrEmpty(row["计算周期"].ToString().Trim()))
                        //    goods.Cycle = Convert.ToDecimal(row["计算周期"]);
                        //if (!string.IsNullOrEmpty(row["射胶"].ToString().Trim()))
                        //    goods.Injection = Convert.ToDecimal(row["射胶"]);
                        //if (!string.IsNullOrEmpty(row["冷却"].ToString().Trim()))
                        //    goods.Cooling = Convert.ToDecimal(row["冷却"]);
                        //goods.Weight = row["毛重"].ToString().Trim() + "/" + row["模重"].ToString().Trim() + "KGS";
                        ////if (!string.IsNullOrEmpty(goods.SPEC))
                        ////{
                        ////    decimal iVolume = MainForm.GetVolume(goods.SPEC);
                        ////    //goods.Cuft = Math.Round(iVolume * (decimal)0.000035294, 2);//Convert.ToDecimal(row["材积"]);
                        ////    goods.Volume = Math.Round(iVolume / 1000000, 2);
                        ////}
                        hasGoods.Remark = row["备注"].ToString().Trim();
                        //goods.CommissionRate = 1;
                        //goods.AddTime = DateTime.Now;
                        hasGoods.UpdateTime = DateTime.Now;
                        if (isNewGoods)
                            InsertGoodsList.Add(hasGoods);
                        else
                            UpdateGoodsList.Add(hasGoods);
                        //InsertGoodsList.Add(goods);
                        //}
                        //else
                        //{
                        //    ////foreach (Goods obj in UpdateGoodsList)
                        //    ////{

                        //        ////if (obj == hasGoods)
                        //        ////{
                        //            goodsCode = hasGoods.Code;
                        //            //oldGoods.ID = hasGoods.ID;
                        //            //oldGoods.MfrsID = hasGoods.MfrsID;
                        //            //oldGoods.Code = hasGoods.Code;
                        //            if (row["货品类型"].ToString().Trim().Contains("包装"))
                        //        hasGoods.Type = (int)GoodsBigType.SFGoods;
                        //            else if (row["货品类型"].ToString().Trim().Contains("胶件")
                        //        || row["货品类型"].ToString().Trim().Equals("半成品")
                        //        || row["货品类型"].ToString().Trim().Contains("配件")
                        //        || row["货品类型"].ToString().Trim().Contains("喷漆")
                        //        || row["货品类型"].ToString().Trim().Contains("电镀")
                        //        || row["货品类型"].ToString().Trim().Contains("移印")
                        //        || row["货品类型"].ToString().Trim().Contains("贴标"))
                        //        hasGoods.Type = (int)GoodsBigType.Stuff;
                        //            else if (row["货品类型"].ToString().Trim().Contains("原料"))
                        //        hasGoods.Type = (int)GoodsBigType.Material;
                        //            else if (row["货品类型"].ToString().Trim().Contains("筐袋"))
                        //        hasGoods.Type = (int)GoodsBigType.Basket;
                        //            else if (row["货品类型"].ToString().Trim().Equals("成品"))
                        //                break; ;
                        //    hasGoods.Name = row["品名"].ToString().Trim();
                        //    hasGoods.GoodsTypeID = goodsType.ID;
                        //            //obj.PackagingID = packaging.ID;
                        //            if (string.IsNullOrEmpty(row["单价"].ToString().Trim()))
                        //        hasGoods.Price = 0;
                        //            else
                        //        hasGoods.Price = Convert.ToDecimal(row["单价"]);
                        //    //oldGoods.BarCode = hasGoods.BarCode;//Convert.ToString(++iBarCode);
                        //    hasGoods.PCS = 1;
                        //    hasGoods.CavityNumber = 1;
                        //            //obj.InnerBox = Convert.ToInt32(row["内盒"]);
                        //            if (!string.IsNullOrEmpty(row["单位"].ToString().Trim()))
                        //        hasGoods.Unit = row["单位"].ToString().Trim();
                        //            if (!string.IsNullOrEmpty(row["规格"].ToString().Trim()))
                        //        hasGoods.SPEC = row["规格"].ToString().Trim();
                        //            //obj.MEAS = row["外箱规格"].ToString().Trim();
                        //            //if (!string.IsNullOrEmpty(row["毛重"].ToString().Trim()))
                        //            //    obj.GWeight = Convert.ToDecimal(row["毛重"]);
                        //            if (string.IsNullOrEmpty(row["模重"].ToString().Trim()))
                        //        hasGoods.NWeight = 1;
                        //            else
                        //        hasGoods.NWeight = Convert.ToDecimal(row["模重"]) == 0 ? 1 : Convert.ToDecimal(row["模重"]);
                        //            if (string.IsNullOrEmpty(row["出数"].ToString().Trim()))
                        //        hasGoods.CavityNumber = 1;
                        //            else
                        //        hasGoods.CavityNumber = Convert.ToDecimal(row["出数"]) == 0 ? 1 : Convert.ToDecimal(row["出数"]);
                        //            if (row["货品类型"].ToString().Trim().Contains("原料"))
                        //            {
                        //        hasGoods.NWeight = 1;
                        //        hasGoods.CavityNumber = 1;
                        //            }
                        //    //if (!string.IsNullOrEmpty(row["计算周期"].ToString().Trim()))
                        //    //    obj.Cycle = Convert.ToDecimal(row["计算周期"]);
                        //    //if (!string.IsNullOrEmpty(row["射胶"].ToString().Trim()))
                        //    //    obj.Injection = Convert.ToDecimal(row["射胶"]);
                        //    //if (!string.IsNullOrEmpty(row["冷却"].ToString().Trim()))
                        //    //    obj.Cooling = Convert.ToDecimal(row["冷却"]);
                        //    //obj.Weight = row["毛重"].ToString().Trim() + "/" + row["模重"].ToString().Trim() + "KGS";
                        //    ////if (!string.IsNullOrEmpty(obj.SPEC))
                        //    ////{
                        //    ////    decimal iVolume = MainForm.GetVolume(obj.SPEC);
                        //    ////    //obj.Cuft = Math.Round(iVolume * (decimal)0.000035294, 2);//Convert.ToDecimal(row["材积"]);
                        //    ////    obj.Volume = Math.Round(iVolume / 1000000, 2);
                        //    ////}
                        //    //oldGoods.Pic = hasGoods.Pic;
                        //    //oldGoods.AddTime = hasGoods.AddTime;
                        //    hasGoods.Remark = row["备注"].ToString().Trim();
                        //    //hasGoods.CommissionRate = 1;
                        //    hasGoods.UpdateTime = DateTime.Now;
                        //    ////}
                        //    ////}
                        //    UpdateGoodsList.Add(hasGoods);
                        //}

                    }
                    if (InsertGoodsList.Count > 0 || UpdateGoodsList.Count > 0)
                    {
                        Hashtable has = new Hashtable();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            //给胶件以外的物料赋值模重和出数
                            //添加
                            Goods goods = null;
                            Goods jj = null;
                            goods = MainForm.ConvertList<Goods>((IList)MainForm.dataSourceList[typeof(Goods)]).FirstOrDefault(o => o.Code == row["用料"].ToString().Trim());
                            if (goods == null)
                                jj = InsertGoodsList.Find(o => o.Code == row["用料"].ToString().Trim());
                            else
                                jj = goods;
                            if (jj != null)
                            {
                                Goods g = InsertGoodsList.Find(o => o.Code == row["货号"].ToString().Trim());
                                if (g != null && g.Type != (int)GoodsBigType.Goods)
                                {
                                    if (jj.Type != (int)GoodsBigType.Material)
                                    {
                                        if (has[g.Code] == null)
                                        {
                                            has.Add(g.Code, g);
                                            g.NWeight = jj.NWeight * (string.IsNullOrEmpty(row["损耗"].ToString().Trim()) ? 1 : Convert.ToInt32(row["损耗"]));
                                            g.CavityNumber = jj.CavityNumber;
                                        }
                                        else
                                        {
                                            g.NWeight += jj.NWeight * (string.IsNullOrEmpty(row["损耗"].ToString().Trim()) ? 1 : Convert.ToInt32(row["损耗"]));
                                            g.CavityNumber = 1;
                                        }
                                    }
                                }
                            }
                            //修改
                            Goods jju = null;
                            if (goods == null)
                                jju = UpdateGoodsList.Find(o => o.Code == row["用料"].ToString().Trim());
                            else
                                jju = goods;
                            if (jju != null)
                            {
                                Goods gu = UpdateGoodsList.Find(o => o.Code == row["货号"].ToString().Trim());
                                if (gu != null && gu.Type != (int)GoodsBigType.Goods)
                                {
                                    if (jju.Type != (int)GoodsBigType.Material)
                                    {
                                        if (has[gu.Code] == null)
                                        {
                                            has.Add(gu.Code, gu);
                                            gu.NWeight = jju.NWeight * (string.IsNullOrEmpty(row["损耗"].ToString().Trim()) ? 1 : Convert.ToInt32(row["损耗"]));
                                            gu.CavityNumber = jju.CavityNumber;
                                        }
                                        else
                                        {
                                            gu.NWeight += jju.NWeight * (string.IsNullOrEmpty(row["损耗"].ToString().Trim()) ? 1 : Convert.ToInt32(row["损耗"]));
                                            gu.CavityNumber = 1;
                                        }
                                    }
                                }
                            }
                        }
                        BLLFty.Create<GoodsBLL>().Import(InsertGoodsList, UpdateGoodsList);
                        //导入物料后再导入物料清单
                        bool result = BOMImport(ds);
                        if (result == false)
                            return;
                        CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "导入成功");
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("第{0}行的货号[{1}]的记录格式错误。错误信息：\r\n", iError, goodsCode) + "\r\n" + ex.Message);
                return;
            }
            finally
            {
                MainForm.DataPageRefresh<VMaterial>();
                ((TabbedGoodsPage)itemDetail).DataRefresh();
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        bool BOMImport(DataSet ds)
        {
            List<BOM> InsertBOMList = new List<BOM>();
            List<BOM> updateBOMList = new List<BOM>();
            List<BOM> DeleteBOMList = new List<BOM>();
            Hashtable hasBOM = new Hashtable();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Goods hasGoods = null;
                Goods hasMaterial = null;
                VParentGoodsByBOM hasParentGoods = null;
                if (row["货品类型"].ToString().Trim().Contains("胶件")
                                || row["货品类型"].ToString().Trim().Contains("成品")
                                || row["货品类型"].ToString().Trim().Contains("喷漆")
                                || row["货品类型"].ToString().Trim().Contains("电镀")
                                || row["货品类型"].ToString().Trim().Contains("移印")
                                || row["货品类型"].ToString().Trim().Contains("贴标"))
                {
                    string strGoodsType = string.Empty;
                    int iGoodsType = 0;
                    if (row["货品类型"].ToString().Trim().Contains("胶件"))
                    {
                        strGoodsType = "原料";
                        iGoodsType = (int)GoodsBigType.Material;
                    }
                    else if (row["货品类型"].ToString().Trim().Equals("成品"))
                    {
                        //strGoodsType = row["货品类型"].ToString().Trim();
                        iGoodsType = (int)GoodsBigType.Goods;
                    }
                    else
                    {
                        strGoodsType = row["货品类型"].ToString().Trim();
                        iGoodsType = (int)GoodsBigType.Stuff;
                    }
                    List<Goods> goodsList = BLLFty.Create<GoodsBLL>().GetGoods();
                    //检查用料
                    hasMaterial = goodsList.FirstOrDefault(o => o.Name.Contains(row["用料"].ToString().Trim()));
                    if (row["货品类型"].ToString().Trim().Contains("胶件"))
                    {
                        if (hasMaterial == null)
                        {
                            Goods material = new Goods();
                            material.ID = Guid.NewGuid();
                            material.Code = Rexlib.GetSpellCode(row["用料"].ToString().Trim());
                            material.Name = row["用料"].ToString().Trim();
                            bool b = BLLFty.Create<GoodsTypeBLL>().GetGoodsType().Any(o => o.Name.Contains(strGoodsType));
                            if (b == false)
                            {
                                GoodsType gt = new GoodsType();
                                gt.ID = Guid.NewGuid();
                                gt.Code = Rexlib.GetSpellCode(strGoodsType);
                                gt.Name = strGoodsType;
                                BLLFty.Create<GoodsTypeBLL>().Insert(gt);
                                //CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("找不到货品类型[{0}]，请先添加该货品类型。", row["货品类型"].ToString().Trim()));
                                //return;
                            }
                            material.GoodsTypeID = BLLFty.Create<GoodsTypeBLL>().GetGoodsType().FirstOrDefault(o => o.Name.Contains(strGoodsType)).ID;  //原料
                            material.Type = iGoodsType;
                            material.Price = 0;
                            material.PCS = 1;
                            material.NWeight = 1;
                            material.CavityNumber = 1;
                            material.AddTime = DateTime.Now;
                            BLLFty.Create<GoodsBLL>().Insert(material);
                            hasMaterial = material;
                        }
                    }
                    hasGoods = goodsList.FirstOrDefault(o => o.Code == row["货号"].ToString().Trim());
                    hasParentGoods = BLLFty.Create<GoodsBLL>().GetVParentGoodsByBOM().FirstOrDefault(o => o.货号 == row["货号"].ToString().Trim());
                }
                else
                    continue;
                if (hasMaterial == null)
                    continue;
                if (hasGoods == null)
                {
                    CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), string.Format("货品：[{0}]不存在。", row["货号"].ToString().Trim()));
                    return false;
                }
                if (hasBOM[hasGoods.ID.ToString() + hasMaterial.ID] == null)
                {
                    hasBOM.Add(hasGoods.ID.ToString() + hasMaterial.ID, hasGoods);
                    if (hasParentGoods == null)
                    {
                        BOM bom = new BOM();
                        bom.GoodsID = hasMaterial.ID;
                        bom.ParentGoodsID = hasGoods.ID;
                        if (!string.IsNullOrEmpty(row["损耗"].ToString().Trim()))
                            bom.Qty = Convert.ToDecimal(row["损耗"]);
                        else
                            bom.Qty = 1;
                        if (hasGoods.Type == 0)
                        {
                            bom.PCS = hasGoods.PCS;
                            bom.Type = (int)BOMType.BOM;
                        }
                        else
                        {
                            bom.PCS = 1;
                            bom.Type = (int)BOMType.Assemble;
                        }
                        InsertBOMList.Add(bom);
                    }
                    else
                    {
                        BOM bomCheck = BLLFty.Create<BOMBLL>().GetBOM(hasParentGoods.ID.GetValueOrDefault())[0];
                        if (bomCheck != null)
                        {
                            BOM bom = new BOM();
                            bom.GoodsID = hasMaterial.ID;
                            bom.ParentGoodsID = hasGoods.ID;
                            if (!string.IsNullOrEmpty(row["损耗"].ToString().Trim()))
                                bom.Qty = Convert.ToDecimal(row["损耗"]);
                            else
                                bom.Qty = 1;
                            if (hasGoods.Type == 0)
                            {
                                bom.PCS = hasGoods.PCS;
                                bom.Type = (int)BOMType.BOM;
                            }
                            else
                            {
                                bom.PCS = 1;
                                bom.Type = (int)BOMType.Assemble;
                            }
                            //updateBOMList.Add(bom);
                            DeleteBOMList.Add(bomCheck);
                            InsertBOMList.Add(bom);
                        }
                    }
                }
            }
            if (InsertBOMList.Count > 0 || updateBOMList.Count > 0)
            {
                BLLFty.Create<BOMBLL>().Import(InsertBOMList, DeleteBOMList);
            }
            return true;
        }

        void btnSOAPrint_ElementClick(object sender, NavElementEventArgs e)
        {
            iExtensions.ReceiveData();
        }

        #region 中控考勤

        //void btnUploadStaff_ElementClick(object sender, NavElementEventArgs e)
        //{
        //    try
        //    {
        //        this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
        //        if (MainForm.IsConnected == false)
        //        {
        //            XtraMessageBox.Show("请先连接设备。", "操作提示");
        //            return;
        //        }
        //        if (MainForm.AttParam == null)
        //        {
        //            XtraMessageBox.Show("连接失败，请检查设备的通讯方式。", "错误");
        //            return;
        //        }
        //        List<UsersInfo> attUsers = ((List<UsersInfo>)MainForm.dataSourceList[typeof(UsersInfo)]).FindAll(o => o.AttEnabled == true);
        //        int idwErrorCode = 0;
        //        bool result;
        //        foreach (UsersInfo user in attUsers)
        //        {
        //            //if (string.IsNullOrEmpty(user.AttCardnumber))
        //            //{
        //            //    //XtraMessageBox.Show(string.Format("请先为用户:[{0}]设置卡号。", user.Name), "错误");
        //            //    continue;
        //            //}
        //            //string sName = txtName.Text.Trim();
        //            //string sPassword = txtPassword.Text.Trim();
        //            //int iPrivilege = Convert.ToInt32(cbPrivilege.Text.Trim());
        //            //string sCardnumber = txtCardnumber.Text.Trim();

        //            MainForm.AxCZKEM1.EnableDevice(MainForm.AttParam.MachineNumber, false);
        //            //string sdwEnrollNumber = txtUserID.Text.Trim();
        //            MainForm.AxCZKEM1.SetStrCardNumber(user.AttCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device
        //            result = MainForm.AxCZKEM1.SSR_SetUserInfo(MainForm.AttParam.MachineNumber, user.Code, user.Name, user.Password, user.AttPrivilege, user.AttEnabled);
        //            if (result == false)
        //                MainForm.AxCZKEM1.GetLastError(ref idwErrorCode);
        //            //if (axCZKEM1.SSR_SetUserInfo(MainForm.AttParam.MachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload the user's information(card number included)
        //            //{
        //            //    XtraMessageBox.Show("(SSR_)SetUserInfo,UserID:" + sdwEnrollNumber + " Privilege:" + iPrivilege.ToString() + " Enabled:" + bEnabled.ToString(), "Success");
        //            //}
        //            //else
        //            //{
        //            //    axCZKEM1.GetLastError(ref idwErrorCode);
        //            //    XtraMessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
        //            //}
        //        }
        //        if (idwErrorCode == 0)
        //            CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "数据上传成功！");
        //        else
        //            XtraMessageBox.Show("操作失败,错误代码=" + idwErrorCode.ToString(), "错误");
        //        MainForm.AxCZKEM1.RefreshData(MainForm.AttParam.MachineNumber);//the data in the device should be refreshed
        //        MainForm.AxCZKEM1.EnableDevice(MainForm.AttParam.MachineNumber, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
        //    }
        //    finally
        //    {
        //        this.Cursor = System.Windows.Forms.Cursors.Default;
        //    }
        //}

        //void btnClearAttLogs_ElementClick(object sender, NavElementEventArgs e)
        //{
        //    try
        //    {
        //        this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
        //        if (MainForm.IsConnected == false)
        //        {
        //            XtraMessageBox.Show("请先连接设备。", "操作提示");
        //            return;
        //        }
        //        if (MainForm.AttParam == null)
        //        {
        //            XtraMessageBox.Show("连接失败，请检查设备的通讯方式。", "错误");
        //            return;
        //        }
        //        System.Windows.Forms.DialogResult result = XtraMessageBox.Show("确定要清除考勤机的考勤数据吗?", "操作提示",
        //        System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
        //        if (result == System.Windows.Forms.DialogResult.OK)
        //        {
        //            int idwErrorCode = 0;

        //            //lvLogs.Items.Clear();
        //            //((DataQueryPage)itemDetail).
        //            MainForm.AxCZKEM1.EnableDevice(MainForm.AttParam.MachineNumber, false);//disable the device
        //            if (MainForm.AxCZKEM1.ClearGLog(MainForm.AttParam.MachineNumber))
        //            {
        //                //BLLFty.Create<AttGeneralLogBLL>().Delete();
        //                MainForm.AxCZKEM1.RefreshData(MainForm.AttParam.MachineNumber);//the data in the device should be refreshed
        //                XtraMessageBox.Show("已经清除终端所有考勤数据", "操作提示");
        //            }
        //            else
        //            {
        //                MainForm.AxCZKEM1.GetLastError(ref idwErrorCode);
        //                XtraMessageBox.Show("操作失败,错误代码=" + idwErrorCode.ToString(), "错误");
        //            }
        //            MainForm.AxCZKEM1.EnableDevice(MainForm.AttParam.MachineNumber, true);//enable the device 
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
        //    }
        //    finally
        //    {
        //        this.Cursor = System.Windows.Forms.Cursors.Default;
        //    }
        //}

        //void btnConnect_ElementClick(object sender, NavElementEventArgs e)
        //{
        //    try
        //    {
        //        this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
        //        if (MainForm.AttParam == null)
        //        {
        //            XtraMessageBox.Show("连接失败，请检查设备的通讯方式。", "错误");
        //            return;
        //        }
        //        int idwErrorCode = 0;

        //        if (btnConnect.Caption == "断开设备")
        //        {
        //            MainForm.AxCZKEM1.Disconnect();
        //            MainForm.IsConnected = false;
        //            btnConnect.Caption = "连接设备";
        //            btnConnect.Glyph = global::USL.Properties.Resources.switch_on_54px;
        //            //lblState.Text = "Current State:DisConnected";
        //            CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "已断开");
        //            return;
        //        }

        //        MainForm.IsConnected = MainForm.AxCZKEM1.Connect_Net(MainForm.AttParam.AttIP, Convert.ToInt32(MainForm.AttParam.AttPort));
        //        if (MainForm.IsConnected)
        //        {
        //            btnConnect.Caption = "断开设备";
        //            btnConnect.Glyph = global::USL.Properties.Resources.switch_off_54px;
        //            //lblState.Text = "Current State:Connected";
        //            CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "已连接");
        //            MainForm.AxCZKEM1.RegEvent(MainForm.AttParam.MachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
        //        }
        //        else
        //        {
        //            MainForm.AxCZKEM1.GetLastError(ref idwErrorCode);
        //            XtraMessageBox.Show("无法连接设备,错误代码=" + idwErrorCode.ToString(), "错误");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
        //    }
        //    finally
        //    {
        //        this.Cursor = System.Windows.Forms.Cursors.Default;
        //    }
        //}

        //void btnDownloadAttLogs_ElementClick(object sender, NavElementEventArgs e)
        //{
        //    try
        //    {
        //        this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
        //        if (MainForm.IsConnected == false)
        //        {
        //            XtraMessageBox.Show("请先连接设备。", "操作提示");
        //            return;
        //        }
        //        if (MainForm.AttParam == null)
        //        {
        //            XtraMessageBox.Show("连接失败，请检查设备的通讯方式。", "错误");
        //            return;
        //        }
        //        int idwErrorCode = 0;

        //        string sdwEnrollNumber = "";
        //        int idwVerifyMode = 0;
        //        int idwInOutMode = 0;
        //        int idwYear = 0;
        //        int idwMonth = 0;
        //        int idwDay = 0;
        //        int idwHour = 0;
        //        int idwMinute = 0;
        //        int idwSecond = 0;
        //        int idwWorkcode = 0;

        //        //int iGLCount = 0;
        //        //int iIndex = 0;

        //        List<AttGeneralLog> attLogList = new List<AttGeneralLog>();
        //        AttGeneralLog attLog = null;
        //        //lvLogs.Items.Clear();
        //        MainForm.AxCZKEM1.EnableDevice(MainForm.AttParam.MachineNumber, false);//disable the device
        //        List<AttGeneralLog> attLogs = BLLFty.Create<AttGeneralLogBLL>().GetAttGeneralLog();
        //        if (MainForm.AxCZKEM1.ReadGeneralLogData(MainForm.AttParam.MachineNumber))//read all the attendance records to the memory
        //        {
        //            while (MainForm.AxCZKEM1.SSR_GetGeneralLogData(MainForm.AttParam.MachineNumber, out sdwEnrollNumber, out idwVerifyMode,
        //                        out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
        //            {
        //                //iGLCount++;
        //                //lvLogs.Items.Add(iGLCount.ToString());
        //                //lvLogs.Items[iIndex].SubItems.Add(sdwEnrollNumber);//modify by Darcy on Nov.26 2009
        //                //lvLogs.Items[iIndex].SubItems.Add(idwVerifyMode.ToString());
        //                //lvLogs.Items[iIndex].SubItems.Add(idwInOutMode.ToString());
        //                //lvLogs.Items[iIndex].SubItems.Add(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString());
        //                //lvLogs.Items[iIndex].SubItems.Add(idwWorkcode.ToString());
        //                //iIndex++;
        //                DateTime attTime = Convert.ToDateTime(idwYear.ToString() + "-" + idwMonth.ToString().PadLeft(2, '0') + "-" + idwDay.ToString().PadLeft(2, '0') + " " + idwHour.ToString().PadLeft(2, '0') + ":" + idwMinute.ToString().PadLeft(2, '0') + ":" + idwSecond.ToString().PadLeft(2, '0'));
        //                attLog = attLogs.FirstOrDefault(o =>
        //                    o.EnrollNumber == sdwEnrollNumber && o.AttTime == attTime);
        //                if (attLog == null || attLog.ID == Guid.Empty)
        //                {
        //                    attLog = new AttGeneralLog();
        //                    attLog.ID = Guid.NewGuid();
        //                    attLog.EnrollNumber = sdwEnrollNumber;
        //                    attLog.VerifyMode = idwVerifyMode;
        //                    attLog.InOutMode = idwInOutMode;
        //                    attLog.AttTime = attTime;
        //                    attLog.Workcode = idwWorkcode;
        //                    attLogList.Add(attLog);
        //                }
        //            }
        //            BLLFty.Create<AttGeneralLogBLL>().Insert(attLogList);
        //            if (attLogList.Count>0)
        //            {
        //                MainForm.dataSourceList[typeof(AttGeneralLog)] = BLLFty.Create<AttGeneralLogBLL>().GetAttGeneralLog();
        //                MainForm.dataSourceList[typeof(VAttGeneralLog)] = BLLFty.Create<AttGeneralLogBLL>().GetVAttGeneralLog();
        //                ((DataQueryPage)itemDetail).InitGrid((IList)MainForm.dataSourceList[typeof(AttGeneralLog)]);
        //                //获取并添加apt
        //                MainForm.GetAttAppointments();
        //            }
        //        }
        //        else
        //        {
        //            MainForm.AxCZKEM1.GetLastError(ref idwErrorCode);

        //            if (idwErrorCode != 0)
        //            {
        //                XtraMessageBox.Show("从终端读取数据失败,错误代码: " + idwErrorCode.ToString(), "错误");
        //            }
        //            else
        //            {
        //                XtraMessageBox.Show("终端没有数据返回!", "错误");
        //            }
        //        }
        //        MainForm.AxCZKEM1.EnableDevice(MainForm.AttParam.MachineNumber, true);//enable the device
        //        MainForm.DataPageRefresh<VAttGeneralLog>();
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
        //    }
        //    finally
        //    {
        //        this.Cursor = System.Windows.Forms.Cursors.Default;
        //    }
        //}

        #endregion

        void btnHistoryQuery_ElementClick(object sender, NavElementEventArgs e)
        {
            HistoryQueryForm form = new HistoryQueryForm(menu, ((DataQueryPage)itemDetail).gridView.DataSource);
            form.ShowDialog();
            //////MainForm.GetDBData(menu.Name, form.Filter);
             MainForm.DataPageRefresh(menu.Name, form.Filter);
            //////((DataQueryPage)itemDetail).InitGrid(MainForm.GetData(menu.Name));
            if (string.IsNullOrEmpty(form.Filter))
            {
                ((DataQueryPage)itemDetail).lblHistoryFilter.Visible = false;
                ((DataQueryPage)itemDetail).lblHistoryFilter.Text = string.Empty;
            }
            else
            {
                ((DataQueryPage)itemDetail).lblHistoryFilter.Visible = true;
                ((DataQueryPage)itemDetail).lblHistoryFilter.Text = "历史数据查询条件：" + form.lblFilterCriteria.Text.Trim();
            }
        }

        void btnAPTPrint_ElementClick(object sender, NavElementEventArgs e)
        {
            iExtensions.SendData(null);
        }

        void btnSOA2CPrint_ElementClick(object sender, NavElementEventArgs e)
        {
            iExtensions.SendData(null);
        }

        void btnSOA2SPrint_ElementClick(object sender, NavElementEventArgs e)
        {
            iExtensions.SendData(null);
        }

        private Point GetMenuLocation()
        {
            Point pt = new Point();
            pt.X = Location.X + Size.Width / 2;
            pt.Y = Location.Y + Size.Height / 2;
            return pt;
        }

        void btnMenu_ElementClick(object sender, NavElementEventArgs e)
        {
            Point pt = GetMenuLocation();
            radialMenu.ShowPopup(pt);
            radialMenu.Expand();
        }

        void btnBOMPrint_ElementClick(object sender, NavElementEventArgs e)
        {
            isBOMPrint = true;
            iExtensions.SendData(BOMType.BOM);
        }

        void btnMoldListPrint_ElementClick(object sender, NavElementEventArgs e)
        {
            isBOMPrint = true;
            iExtensions.SendData(null);
        }

        void btnMoldMaterialPrint_ElementClick(object sender, NavElementEventArgs e)
        {
            isBOMPrint = true;
            iExtensions.SendData(BOMType.MoldMaterial);
        }

        void btnAssemblePrint_ElementClick(object sender, NavElementEventArgs e)
        {
            isBOMPrint = true;
            iExtensions.SendData(BOMType.Assemble);
        }

        void btnCancelAudit_ElementClick(object sender, NavElementEventArgs e)
        {
            throw new NotImplementedException();
        }

        void btnInvalid_ElementClick(object sender, NavElementEventArgs e)
        {
            //itemDetail.Invalid();
        }

        void btnImport_ElementClick(object sender, NavElementEventArgs e)
        {
            //借用保存接口实现导入功能
            itemDetail.Save();
        }

        void btnSTUpdate_ElementClick(object sender, NavElementEventArgs e)
        {
            //借用审核接口实现更新盘点数据功能
            itemDetail.Audit();
        }

        public void PageFty(string menuName)
        {
            switch (menuName)
            {
                case MainMenuConstants.ProductionStockInBill:
                //case MainMenuConstants.MakeupStockInBill:
                case MainMenuConstants.SalesReturnBill:
                case MainMenuConstants.FGStockInBill:
                case MainMenuConstants.EMSReturnBill:
                case MainMenuConstants.SFGStockInBill:
                case MainMenuConstants.FSMStockInBill:
                case MainMenuConstants.FSMReturnBill:
                case MainMenuConstants.AssembleStockInBill:
                case MainMenuConstants.ReturnedMaterialBill:
                    itemDetail = new StockInBillPage(Guid.Empty, pageGroupCore, menuName);
                    iExtensions = (StockInBillPage)itemDetail;
                    break;
                case MainMenuConstants.Order:
                case MainMenuConstants.FSMOrder:
                case MainMenuConstants.ProductionOrder:
                    itemDetail = new OrderEditPage(Guid.Empty, menuName);
                    iExtensions = (OrderEditPage)itemDetail;
                    break;
                case MainMenuConstants.FGStockOutBill:
                case MainMenuConstants.EMSStockOutBill:
                case MainMenuConstants.SFGStockOutBill:
                case MainMenuConstants.FSMStockOutBill:
                case MainMenuConstants.GetMaterialBill:
                case MainMenuConstants.FSMDPReturnBill:
                case MainMenuConstants.EMSDPReturnBill:
                    itemDetail = new StockOutBillPage(Guid.Empty, menuName);
                    iExtensions = (StockOutBillPage)itemDetail;
                    break;
                case MainMenuConstants.BOM:
                    itemDetail = new BOMEditPage(BOMType.BOM);
                    break;
                case MainMenuConstants.MoldList:
                    itemDetail = new BOMEditPage(BOMType.MoldList);
                    break;
                case MainMenuConstants.MoldMaterial:
                    itemDetail = new BOMEditPage(BOMType.MoldMaterial);
                    break;
                case MainMenuConstants.Assemble:
                    itemDetail = new BOMEditPage(BOMType.Assemble);
                    break;
                case MainMenuConstants.MoldAllot:
                    itemDetail = new MoldAllotPage();
                    break;
                case MainMenuConstants.CustomerSLSalePrice:
                    itemDetail = new SLSalePricePage(BusinessContactType.Customer);
                    break;
                case MainMenuConstants.SupplierSLSalePrice:
                    itemDetail = new SLSalePricePage(BusinessContactType.Supplier);
                    break;
                case MainMenuConstants.ReceiptBill:
                    itemDetail = new ReceiptBillPage(Guid.Empty);
                    iExtensions = (ReceiptBillPage)itemDetail;
                    break;
                case MainMenuConstants.PaymentBill:
                    itemDetail = new PaymentBillPage(Guid.Empty);
                    iExtensions = (PaymentBillPage)itemDetail;
                    break;
                case MainMenuConstants.PermissionSetting:
                    itemDetail = new PermissionSettingPage();
                    break;
                case MainMenuConstants.Material:
                    itemDetail = new TabbedGoodsPage(menu, pageGroupCore, itemDetailButtonList);
                    iExtensions = (TabbedGoodsPage)itemDetail;
                    break;
                case MainMenuConstants.ProductionScheduling:
                    itemDetail = new ProductionSchedulingPage();
                    break;
                case MainMenuConstants.WageBill:
                    itemDetail = new WageBillPage(Guid.Empty);
                    iExtensions = (WageBillPage)itemDetail;
                    break;
                case MainMenuConstants.SchClass:
                    itemDetail = new SchClassPage();
                    break;
                case MainMenuConstants.StaffSchClass:
                    itemDetail = new StaffSchClassPage();
                    break;
                case MainMenuConstants.StaffAttendance:
                    itemDetail = new AttendanceSchedulingPage();
                    break;
                case MainMenuConstants.AttWageBill:
                    itemDetail = new AttWageBillPage(Guid.Empty);
                    iExtensions = (AttWageBillPage)itemDetail;
                    break;
                case MainMenuConstants.SalesSummaryMonthlyReport:
                    itemDetail = new MonthlyChartPage();
                    break;
                case MainMenuConstants.AnnualSalesSummaryByCustomerReport:
                    itemDetail = new CustomerChartPage();
                    break;
                case MainMenuConstants.AnnualSalesSummaryByGoodsReport:
                    itemDetail = new GoodsChartPage();
                    break;
            }
        }

        public void LoadBusinessData(MainMenu menu)
        {
            //////IList list = MainForm.GetData(menu.Name);
            IList list = MainForm.DataPageRefresh(menu.Name, string.Empty);
            if (list != null)
            {
                itemDetail = new DataQueryPage(menu, list, pageGroupCore, itemDetailButtonList);
                iExtensions = (DataQueryPage)itemDetail;
            }
            PageFty(menu.Name);
            if (itemDetail != null)
            {
                ((XtraUserControl)itemDetail).Dock = System.Windows.Forms.DockStyle.Fill;
                xtraScrollableControl1.Controls.Add((XtraUserControl)itemDetail);
                if (MainForm.itemDetailList.ContainsKey(menu.Name) == false)
                    MainForm.itemDetailList.Add(menu.Name, itemDetail);
                //RootWorkItem.State["ItemDetailList"] = itemDetailList;
                CreateToolsButton(menu);
                //菜单
                NavButton btnMenu = new NavButton();
                btnMenu.Caption = "菜单";
                btnMenu.Glyph = global::USL.Properties.Resources.Menu_blue_32px;
                btnMenu.Name = "btnMenu";
                btnMenu.Alignment = NavButtonAlignment.Right;
                btnMenu.ElementClick += btnMenu_ElementClick;
                tileNavPane.Buttons.Add(btnMenu);
                //皮肤主题
                TileNavCategory tileNavCategory = new TileNavCategory();
                tileNavCategory.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
                tileNavCategory.Caption = "皮肤主题";
                tileNavCategory.Name = "tileSkin";
                tileNavCategory.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
                tileNavCategory.OwnerCollection = null;
                tileNavCategory.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
                tileNavCategory.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
                //tileNavPane.Buttons.Add(tileNavCategory);

                foreach (SkinContainer item in SkinManager.Default.Skins)
                {
                    //rgSkins.Properties.Items.Add(new RadioGroupItem(item.SkinName, item.SkinName));
                    TileNavItem tileNavItem = new TileNavItem();
                    tileNavCategory.Items.AddRange(new DevExpress.XtraBars.Navigation.TileNavItem[] { tileNavItem });
                    tileNavItem.Caption = item.SkinName;
                    tileNavItem.Name = item.SkinName;
                    tileNavItem.TileText = item.SkinName;
                    tileNavItem.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
                    tileNavItem.OwnerCollection = tileNavCategory.Items;
                    tileNavItem.ElementClick += tileNavItem_ElementClick;
                }

            }
        }

        void tileNavItem_ElementClick(object sender, NavElementEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                UserLookAndFeel.Default.SkinName = e.Element.Name;
                //rgStyles.EditValue = LookAndFeelStyle.Skin;
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

        void btnRefresh_ElementClick(object sender, NavElementEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                //MainForm.GetDataSource();
                MainForm.DataPageRefresh(menu.Name, string.Empty);
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

        void btnAdd_ElementClick(object sender, NavElementEventArgs e)
        {
            itemDetail.Add();
            setNavButtonStatus(menu, ButtonType.btnAdd);
        }

        void btnSave_ElementClick(object sender, NavElementEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                if (itemDetail.Save())
                {
                    if (!menu.Name.ToUpper().Contains("BILL") && !menu.Name.Contains("Order") && !menu.Name.Contains("SLSalePrice"))
                        MainForm.DataPageRefresh(menu.Name, string.Empty);
                    setNavButtonStatus(menu, ButtonType.btnSave);
                    CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "保存成功");
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

        void btnAudit_ElementClick(object sender, NavElementEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                if (itemDetail.Audit())
                {
                    //if (!menu.Name.ToUpper().Contains("STOCKIN") && !menu.Name.ToUpper().Contains("STOCKOUT") && !menu.Name.ToUpper().Contains("ORDER") && !menu.Name.ToUpper().Contains("WAGEBILL")
                    //     && !menu.Name.ToUpper().Contains("RECEIPT") && !menu.Name.ToUpper().Contains("PAYMENT"))
                    if (!menu.Name.ToUpper().Contains("BILL") && !menu.Name.Contains("Order") && !menu.Name.Contains("SLSalePrice"))
                        MainForm.DataPageRefresh(menu.Name, string.Empty);
                    if (!menu.Name.Contains("Query"))
                    {
                        if (btnAudit.Caption == "审核")
                        {
                            setNavButtonStatus(menu, ButtonType.btnAudit);
                            //if (!menu.Name.Contains("Order") && !menu.Name.Contains("Receipt") && !menu.Name.Contains("Payment"))
                            if (!menu.Name.Contains("Receipt") && !menu.Name.Contains("Payment"))
                            {
                                btnAudit.Caption = "取消审核";
                                btnAudit.Glyph = global::USL.Properties.Resources.Undo_32x32;
                            }
                            CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "审核成功");
                        }
                        else
                        {
                            setNavButtonStatus(menu, ButtonType.btnSave);
                            btnAudit.Caption = "审核";
                            btnAudit.Glyph = global::USL.Properties.Resources.audit;
                            CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "取消审核成功");
                        }
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

        void btnEdit_ElementClick(object sender, NavElementEventArgs e)
        {
            itemDetail.Edit();
            //DataQueryPageRefresh();
        }

        private void BtnStop_ElementClick(object sender, NavElementEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                iExtensions.Export();
                //CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "货品设置停产成功");
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

        void btnDel_ElementClick(object sender, NavElementEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                itemDetail.Del();
                CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "删除成功");
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

        void btnPrint_ElementClick(object sender, NavElementEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                isBOMPrint = false;
                itemDetail.Print();
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

        public void setNavButtonStatus(MainMenu menu, ButtonType status)
        {
            if (menu.ParentID != new Guid("7ea0e093-592a-420c-9a7f-8316f88c35e2"))//基础资料
            {
                switch (status)
                {
                    case ButtonType.btnAdd:
                        //if (menu.Name.Contains("Receipt") || menu.Name.Contains("Payment")) //结款单直接审核
                        //{
                        //    if (btnSave != null)
                        //        btnSave.Visible = false;
                        //    if (btnAudit != null)
                        //    {
                        //        btnAudit.Visible = true;
                        //        btnAudit.Caption = "审核";
                        //        btnAudit.Glyph = global::USL.Properties.Resources.audit;
                        //    }
                        //}
                        //else
                        //{
                            if (btnSave != null)
                                btnSave.Visible = true;
                            if (btnAudit != null)
                                btnAudit.Visible = false;
                        //}
                        if (btnDel != null)
                            btnDel.Visible = false;
                        //btnPrint.Visible = false;
                        //if (btnInvalid != null)
                        //    btnInvalid.Visible = false;
                        break;
                    case ButtonType.btnEdit:
                        break;
                    case ButtonType.btnSave:
                        //if (menu.Name.Contains("Receipt") || menu.Name.Contains("Payment")) //结款单直接审核
                        //{
                        //    if (btnSave != null)
                        //        btnSave.Visible = false;
                        //}
                        //else
                        //{
                            if (btnSave != null)
                                btnSave.Visible = true;
                        //}
                        if (btnAudit != null)
                        {
                            btnAudit.Visible = true;
                            btnAudit.Caption = "审核";
                            btnAudit.Glyph = global::USL.Properties.Resources.audit;
                        }
                        if (btnDel != null)
                            btnDel.Visible = true;
                        //btnPrint.Visible = true;
                        //if (btnInvalid != null)
                        //    btnInvalid.Visible = false;
                        break;
                    case ButtonType.btnAudit:
                        if (btnSave != null)
                            btnSave.Visible = false;
                        if (btnAudit != null)
                        {
                            if (btnAudit.Caption == "审核")
                            {
                                //if (menu.Name.Contains("Order"))
                                //    btnAudit.Visible = false;
                                //else
                                //{
                                    btnAudit.Visible = true;
                                    btnAudit.Caption = "取消审核";
                                    btnAudit.Glyph = global::USL.Properties.Resources.Undo_32x32;
                                //}
                            }
                            //else
                            //{
                            //    btnAudit.Caption = "审核";
                            //    btnAudit.Glyph = global::USL.Properties.Resources.audit;
                            //}
                        }
                        if (btnDel != null)
                            btnDel.Visible = false;
                        //if (btnInvalid != null)
                        //    btnInvalid.Visible = true;
                        break;
                    case ButtonType.btnDel:
                        break;
                    default:
                        break;
                }
            }
        }

        #region RadiaMenu

        private void CreateRadiaMenu()
        {
            int i = 0;
            BarManager barCopy = new BarManager();
            foreach (MainMenu item in menuList.FindAll(o => o.ParentID == null))
            {
                //根据用户权限控制是否显示Tile
                ////if (MainForm.userPermissions.Count > 0 && MainForm.userPermissions.Find(o => o.Caption.Trim() == item.Caption.Trim()).CheckBoxState)
                ////{
                    BarSubItem barSubItem = new BarSubItem();
                    // 
                    // barSubItem
                    // 
                    barSubItem.Caption = item.Caption;
                    barSubItem.Id = ++i;
                    barSubItem.Name = item.Name;
                    barSubItem.Tag = item.ID;

                    barCopy.Items.AddRange(new DevExpress.XtraBars.BarItem[] { barSubItem });
                ////}
            }

            foreach (BarItem barItem in barCopy.Items)
            {
                if (barItem is BarSubItem)
                {
                    BarSubItem barSubItem = new BarSubItem();
                    barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] { barSubItem });
                    radialMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                    new DevExpress.XtraBars.LinkPersistInfo(barSubItem)});
                    // 
                    // barSubItem
                    // 
                    barSubItem.Caption = barItem.Caption;
                    barSubItem.Id = ++i;
                    barSubItem.Name = barItem.Name;
                    barSubItem.Tag = barItem.Tag;
                    //barSubItem.Tag = menuList.Find(o => o.ID == new Guid(barItem.Tag.ToString()));

                    barSubItem.ItemClick += barSubItem_ItemClick;

                    //BarSubItem subItem = barItem as BarSubItem;
                    int index = 0;
                    foreach (MainMenu obj in menuList.FindAll(o => o.ParentID == new Guid(barItem.Tag.ToString())))
                    {
                        //根据用户权限控制是否显示Tile
                        ////if (MainForm.userPermissions.Count > 0 && MainForm.userPermissions.Find(o => o.Caption.Trim() == obj.Caption.Trim()).CheckBoxState)
                        ////{
                            BarButtonItem barButtonItem = new BarButtonItem();
                            barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] { barButtonItem });

                            barSubItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                        new DevExpress.XtraBars.LinkPersistInfo(barButtonItem)});
                            // 
                            // barButtonItem
                            // 
                            barButtonItem.Caption = obj.Caption;
                            barButtonItem.Id = index++;
                            barButtonItem.Name = obj.Name;
                            barButtonItem.Tag = obj.ParentID;
                            if (itemDetailButtonList.ContainsKey(obj.Name) == false)
                                itemDetailButtonList.Add(obj.Name, index - 1);

                            barButtonItem.ItemClick += barButtonItem_ItemClick;
                        ////}
                    }
                }
            }
        }

        void barSubItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            BaseContentContainer documentContainer = pageGroupCore.Parent as BaseContentContainer;
            if (documentContainer != null) ActivateContainer(documentContainer.Manager, e.Item.Tag);
        }

        void ActivateContainer(DocumentManager documentManager, Object obj)
        {
            WindowsUIView view = documentManager.View as WindowsUIView;
            if (view != null)
            {
                Guid parentID = new Guid(obj.ToString());
                if (parentID != null)
                {
                    pageGroupCore = MainForm.groupsItemDetailList[parentID];
                    view.ActivateContainer(pageGroupCore);
                }
                //pageGroupCore = groupsItemDetailPage[parentID];
                //view.ActivateContainer(groupsItemDetailPage[parentID]);
            }
        }

        void barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            BaseContentContainer documentContainer = pageGroupCore.Parent as BaseContentContainer;
            if (documentContainer != null) ActivateContainer(documentContainer.Manager, e.Item);
        }
        //void ActivateContainer(DocumentManager manager,int indexCore)
        void ActivateContainer(DocumentManager manager, BarItem item)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                WindowsUIView view = manager.View as WindowsUIView;
                if (view != null)
                {
                    foreach (DBML.MainMenu mm in menuList.FindAll(o => o.ParentID == MainForm.mainMenuList[item.Name].ParentID))
                    {
                        if (MainForm.hasItemDetailPage[mm.Name] == null)
                        {
                            MainForm.itemDetailPageList[mm.Name].LoadBusinessData(MainForm.mainMenuList[mm.Name]);
                            MainForm.hasItemDetailPage.Add(mm.Name, true);
                        }
                    }
                    //if (MainForm.hasItemDetailPage[item.Name] == null)
                    //{
                    //    MainForm.itemDetailPageList[item.Name].LoadBusinessData(MainForm.mainMenuList[item.Name]);
                    //    MainForm.hasItemDetailPage.Add(item.Name, true);
                    //}
                    //pageGroupCore.Parent = this.Tag as IContentContainer;
                    pageGroupCore.SetSelected(pageGroupCore.Items[item.Id]);
                    view.ActivateContainer(pageGroupCore);
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

        #endregion
    }
}
