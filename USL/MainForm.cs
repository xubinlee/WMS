using DevExpress.XtraEditors;
using System.Collections.Generic;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Docking2010.Views;
using System.Drawing;
using EDMX;
using Factory;
using BLL;
using DevExpress.XtraBars;
using System;
using CommonLibrary;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using IBase;
using System.Collections;
using Utility;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraGrid.Columns;
using System.Linq;
using System.Configuration;
using DevExpress.XtraBars.Docking2010;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;
using static Utility.EnumHelper;
using System.Reflection;
using System.Data.Linq;
using System.Transactions;
using DevExpress.XtraEditors.Filtering;
using Utility.Interceptor;
using MainMenu = EDMX.MainMenu;
using System.Linq.Expressions;

namespace USL
{
    public partial class MainForm : XtraForm,IToolbar,IStatusbar
    {
        //public static Dictionary<Type, object> dataSourceList = new Dictionary<Type, object>();
        //Thread threadGetVDataSource;
        Thread threadGetUserInfo;
        //Thread threadInsertAlert;
        public static Dictionary<String, int> alertCount;
        public static UsersInfo usersInfo;
        public static Department department;
        ////public static List<Permission> userPermissions; //功能项权限
        public static List<ButtonPermission> buttonPermissions;//按钮权限
        //PageGroup pageGroupCore;
        //SampleDataSource dataSource;
        //Dictionary<SampleDataGroup, PageGroup> groupsItemDetailPage;
        static List<MainMenu> userMenuList; // 用户菜单，有权限设置的菜单列表
        public static List<MainMenu> UserMenuList { get => userMenuList; }
        private static List<MainMenu> allMainMenuList; // 菜单总表
        public static List<MainMenu> AllMainMenuList { get => allMainMenuList; }
        //static List<SystemStatus> statusList;
        public ErrorInfoControl ErrorListControl { get => this.errorInfoControl1; }        
        Dictionary<MainMenu, PageGroup> groupsItemDetailPage;
        public static Dictionary<Guid, PageGroup> groupsItemDetailList;
        Dictionary<String, int> itemDetailButtonList; //子菜单按钮项
        public static AlertControl alertControl;
        //public static int exportSalesReceiptDate = int.Parse(ConfigurationManager.AppSettings["ExportSalesReceiptDate"]);  //外销账期
        static int goodsBigType = 0;  //Goods大类
        static string goodsBigTypeName = string.Empty;
        //考勤组件
        //private static zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        //public static zkemkeeper.CZKEMClass AxCZKEM1
        //{
        //    get { return axCZKEM1; }
        //    //set { axCZKEM1 = value; }
        //}
        private static bool isConnected = false;//the boolean value identifies whether the device is connected

        public static bool IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; }
        }

        public static string GoodsBigTypeName
        {
            get { return MainForm.goodsBigTypeName; }
            set { MainForm.goodsBigTypeName = value; }
        }

        public static int GoodsBigType
        {
            get { return MainForm.goodsBigType; }
            set { MainForm.goodsBigType = value; }
        }

        //[ServiceDependency]
        //public WorkItem RootWorkItem { get; set; }
        static string serverUrl = string.Empty;

        public static string ServerUrl
        {
            get { return MainForm.serverUrl; }
            set { MainForm.serverUrl = value; }
        }

        static string serverUserName = string.Empty;

        public static string ServerUserName
        {
            get { return MainForm.serverUserName; }
            set { MainForm.serverUserName = value; }
        }
        static string serverPassword = string.Empty;

        public static string ServerPassword
        {
            get { return MainForm.serverPassword; }
            set { MainForm.serverPassword = value; }
        }
        static string serverDomain = string.Empty;

        public static string ServerDomain
        {
            get { return MainForm.serverDomain; }
            set { MainForm.serverDomain = value; }
        }

        static string downloadPath = Application.StartupPath + "\\PicFile\\";

        public static string DownloadFilePath
        {
            get { return MainForm.downloadPath; }
            set { MainForm.downloadPath = value; }
        }

        static string company = string.Empty;

        public static string Company
        {
            get { return MainForm.company; }
            set { MainForm.company = value; }
        }

        static AttParameters attParam = null;

        public static AttParameters AttParam
        {
            get { return MainForm.attParam; }
            set { MainForm.attParam = value; }
        }

        //static SystemInfo sysInfo = null;

        //public static SystemInfo SysInfo
        //{
        //    get { return MainForm.sysInfo; }
        //    set { MainForm.sysInfo = value; }
        //}

        static List<TypesList> typesList = null;

        public static List<TypesList> TypesList { get => typesList; }

        public static Dictionary<string, MainMenu> mainMenuList = new Dictionary<string, MainMenu>();
        public static Dictionary<string, ItemDetailPage> itemDetailPageList = new Dictionary<string, ItemDetailPage>();
        public static Hashtable hasItemDetailPage = new Hashtable();

        static bool isLandScape = true;

        public static bool IsLandScape
        {
            get { return MainForm.isLandScape; }
            set { MainForm.isLandScape = value; }
        }

        static System.Drawing.Printing.PaperKind printPaperKind = System.Drawing.Printing.PaperKind.A5Rotated;

        public static System.Drawing.Printing.PaperKind PrintPaperKind
        {
            get { return MainForm.printPaperKind; }
            set { MainForm.printPaperKind = value; }
        }

        static Size paperSize = new Size((int)(215 / 25.4 * 100), (int)(139 / 25.4 * 100));

        public static Size PaperSize
        {
            get { return MainForm.paperSize; }
            set { MainForm.paperSize = value; }
        }

        static string contacts = string.Empty;

        public static string Contacts
        {
            get { return MainForm.contacts; }
            set { MainForm.contacts = value; }
        }

        static string accounts = string.Empty;

        public static string Accounts
        {
            get { return MainForm.accounts; }
            set { MainForm.accounts = value; }
        }

        static ISnowSoftVersion iSnowSoftVersion = ISnowSoftVersion.ALL;

        public static ISnowSoftVersion ISnowSoftVersion
        {
            get { return MainForm.iSnowSoftVersion; }
            //set { MainForm.iSnowSoftVersion = value; }
        }

        bool authorised = true;  //判断系统是否已经授权使用

        public MainForm()
        {
            InitializeComponent();
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                using (TransactionScope ts = new TransactionScope())
                {
                    //iSnowSoftVersion = EnumHelper.GetEnumValues<ISnowSoftVersion>(true).FirstOrDefault(o => o.Name == ConfigurationManager.AppSettings["ISnowSoftVersion"].ToString()).Value;
                    this.windowsUIView.Caption = ConfigurationManager.AppSettings["SystemName"];
                    company = ConfigurationManager.AppSettings["Company"];
                    contacts = ConfigurationManager.AppSettings["Contacts"];
                    accounts = ConfigurationManager.AppSettings["Accounts"];
                    this.tileContainer.Properties.IndentBetweenGroups = int.Parse(ConfigurationManager.AppSettings["IndentBetweenGroups"]);
                    this.tileContainer.Properties.ItemSize = int.Parse(ConfigurationManager.AppSettings["ItemSize"]);
                    this.tileContainer.Properties.LargeItemWidth = int.Parse(ConfigurationManager.AppSettings["LargeItemWidth"]);
                    this.tileContainer.Properties.RowCount = int.Parse(ConfigurationManager.AppSettings["RowCount"]);
                    serverUrl = ConfigurationManager.AppSettings["ServerUrl"];
                    serverUserName = ConfigurationManager.AppSettings["ServerUserName"];
                    serverPassword = Security.Decrypt(ConfigurationManager.AppSettings["ServerPassword"].ToString());
                    serverDomain = ConfigurationManager.AppSettings["ServerDomain"];
                    //if (MainForm.Company.Contains("镇阳"))
                    //{
                    //    isLandScape = false;
                    //    printPaperKind = System.Drawing.Printing.PaperKind.A5Rotated;
                    //}
                    if (MainForm.Company.Contains("创萌"))
                    {
                        isLandScape = false;
                        printPaperKind = System.Drawing.Printing.PaperKind.Custom;
                        PaperSize = new Size((int)(218 / 25.4 * 100), (int)(140 / 25.4 * 100));
                    }
                    else if (MainForm.Company.Contains("谷铭达") || MainForm.Company.Contains("镇阳") || MainForm.Company.Contains("盛兴"))
                    {
                        isLandScape = false;
                        printPaperKind = System.Drawing.Printing.PaperKind.Custom;
                        PaperSize = new Size((int)(215 / 25.4 * 100), (int)(139 / 25.4 * 100));
                    }
                    else
                    {
                        isLandScape = false;
                        printPaperKind = System.Drawing.Printing.PaperKind.A5Rotated;
                    }

                    this.tools.Visible = false;
                    windowsUIView.AddTileWhenCreatingDocument = DevExpress.Utils.DefaultBoolean.False;
                    userMenuList = BLLFty.Create<BaseBLL>().GetListBy<MainMenu>(null);
                    allMainMenuList = userMenuList;
                    //statusList = cli BLLFty.Create<SystemInfoBLL>().GetSystemStatus();
                    List<Permission> pList = BLLFty.Create<BaseBLL>().GetListBy<Permission>(o => o.UserID == usersInfo.ID);

                    //如果MainMenu有变更，更新用户权限列表
                    pList = updatePermission(pList);
                    //设置权限
                    for (int i = userMenuList.Count - 1; i >= 0; i--)
                    {
                        if (userMenuList[i].ParentID == null)
                            continue;
                        Permission p = pList.FirstOrDefault(o => o.Caption.Trim() == userMenuList[i].Caption.Trim());
                        if (p!=null && p.CheckBoxState == false)
                        {
                            userMenuList.RemoveAt(i);
                            continue;
                        }
                    }
                    alertCount = new Dictionary<string, int>();
                    //groupsItemDetailPage = new Dictionary<SampleDataGroup, PageGroup>();
                    groupsItemDetailPage = new Dictionary<MainMenu, PageGroup>();
                    groupsItemDetailList = new Dictionary<Guid, PageGroup>();
                    itemDetailButtonList = new Dictionary<string, int>();
                    alertControl = new AlertControl(this.components);
                    alertControl.FormShowingEffect = AlertFormShowingEffect.SlideHorizontal;

                    SetStateBarInfo();
                    //GetDataSource();
                    //GetVDataSource();
                    buttonPermissions =BLLFty.Create<BaseBLL>().GetListBy<ButtonPermission>(o => o.UserID == usersInfo.ID);
                    attParam =BLLFty.Create<BaseBLL>().GetListByNoTracking<AttParameters>(o => o.CommMode == "TCP/IP").FirstOrDefault();
                    typesList = BLLFty.Create<BaseBLL>().GetListByNoTracking<TypesList>(null);
                    CreateLayout();
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                //CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                XtraMessageBox.Show(ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private List<Permission> updatePermission(List<Permission> pList)
        {
            List<Permission> insertList = new List<Permission>();
            allMainMenuList.ForEach(menu =>
            {
                Permission p = pList.FirstOrDefault(o => o.UserID == usersInfo.ID && o.Caption == menu.Caption);
                Permission obj = new Permission();
                if (p == null)
                    obj.CheckBoxState = false;
                else
                    obj.CheckBoxState = p.CheckBoxState;
                obj.ID = menu.SerialNo;
                string no = menu.SerialNo.ToString().Trim();
                if (menu.ParentID == null)
                    obj.ParentID = 0;
                else if (no.Length > 2)
                    obj.ParentID = int.Parse(no.Substring(0, no.Length - 2));
                obj.SerialNo = menu.SerialNo;
                obj.UserID = usersInfo.ID;
                obj.Caption = menu.Caption;
                insertList.Add(obj);
            });
            if (insertList.Count > 0)
            {
                Expression<Func<Permission, bool>> delWhere = o => o.UserID.Equals(usersInfo.ID);
                BLLFty.Create<BaseBLL>().DeleteAndAdd(delWhere, insertList);
                return BLLFty.Create<BaseBLL>().GetListBy<Permission>(o => o.UserID == usersInfo.ID);
            }
            else
                return pList;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetErrorPanelInfo();
            //SetStateBarInfo();
            InnerHideDockPanel();
            //警告提醒
            ////List<Alert> alertList = BLLFty.Create<AlertBLL>().GetAlert();//((List<Alert>)MainForm.dataSourceList[typeof(Alert)]);
            ////foreach (Alert item in alertList)
            ////{
            ////    alertControl.Show(this, item.Caption, item.Text, global::USL.Properties.Resources.Alarm_Clock);
            ////}
            //SetAlertCount();
            barUserInfo.ItemClick += barUserInfo_ItemClick;
            //AlertControl ac = new AlertControl();
            //ac.AppearanceCaption.Font = new Font("宋体", 15);
            //ac.AppearanceText.Font = new Font("宋体", 15);
            ////ac.AutoHeight = true;
            //ac.Show(this, "购买咨询：", "\r\n15220288727 李先生", global::USL.Properties.Resources.phone_32);

            //判断电脑系统时间是否和互联网标准北京时间一致（或者时间等于2015-01-01，即不能上网，获取不了系统时间）
            //if (Program.SysDateTime.ToString("yyyy-MM-dd") != Security.DataStandardTime().ToString("yyyy-MM-dd") || Security.Encrypt(Security.DataStandardTime().ToString("yyyy-MM-dd")).Equals("7877984B2FAE09F6A4B7C75AC9DD29BC"))
            //{
            //    authorised = false;
            //    XtraMessageBox.Show("系统未授权使用，请联系系统开发人员。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}
        }

        /// <summary>
        /// 提醒记录数量
        /// </summary>
        //public static void SetAlertCount()
        //{
        //    List<Alert> alertList = BLLFty.Create<AlertBLL>().GetAlert();//((List<Alert>)MainForm.dataSourceList[typeof(Alert)]);
        //    alertCount.Clear();
        //    foreach (Alert item in alertList)
        //    {
        //        if (alertCount.ContainsKey(item.Caption) == false)
        //        {
        //            alertCount.Add(item.Caption, 1);
        //        }
        //        else
        //            alertCount[item.Caption] = ++alertCount[item.Caption];
        //    }
        //    //lblAlert.Caption = "提醒信息：";
        //    //foreach (KeyValuePair<String, int> kvp in alertCount)
        //    //{
        //    //    lblAlert.Caption += string.Format("{0}条{1}；", kvp.Value, kvp.Key);
        //    //}
        //}

        void barUserInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog(); 
        }

        void InsertAlert()
        {
            //// 删除所有单据提醒记录
            //BLLFty.Create<AlertBLL>().DeleteBill();
            ////单据交货日期提醒
            //List<Alert> dellist = new List<Alert>();
            //List<Alert> insertlist = new List<Alert>();
            ////订货单
            //List<OrderHd> orderList = BLLFty.Create<OrderBLL>().GetOrderHd();
            //foreach (OrderHd order in orderList)
            //{
            //    //Alert alert = BLLFty.Create<AlertBLL>().GetAlert().Find(o => o.BillID == order.ID);
            //    //if (alert != null)
            //    //    dellist.Add(alert);
            //    Company customer = BLLFty.Create<CompanyBLL>().GetCompany().FirstOrDefault(o => o.ID == order.CompanyID);
            //    if (order.Status == 0 && order.DeliveryDate <= DateTime.Now.AddDays(3))
            //    {
            //        Alert obj = new Alert();
            //        obj.ID = Guid.NewGuid();
            //        obj.BillID = order.ID;
            //        obj.Caption = "交货提醒";
            //        obj.Text = string.Format("客户:[{0}],唛头:[{1}]的订货单[{2}]交货日期是:[{3}].请尽快发货。\r\n备注:{4}", customer.Name ?? string.Empty, order.MainMark, order.BillNo, order.DeliveryDate.ToString("yyyy-MM-dd"), order.Remark);
            //        obj.AddTime = DateTime.Now;
            //        insertlist.Add(obj);
            //    }
            //}

            ////出库单
            //List<StockOutBillHd> billList = BLLFty.Create<StockOutBillBLL>().GetStockOutBillHd();
            //foreach (StockOutBillHd bill in billList)
            //{
            //    //Alert alert = BLLFty.Create<AlertBLL>().GetAlert().Find(o => o.BillID == bill.ID);
            //    //if (alert != null)
            //    //    dellist.Add(alert);
            //    Company customer = BLLFty.Create<CompanyBLL>().GetCompany().FirstOrDefault(o => o.ID == bill.CompanyID);
            //    List<TypesList> types = BLLFty.Create<TypesListBLL>().GetTypesList();
            //    string billName = types.Find(o => o.Type == TypesListConstants.StockOutBillType && o.No == bill.Type).Name;
            //    if (bill.Status == 0 && bill.DeliveryDate <= DateTime.Now.AddDays(3))
            //    {
            //        Alert obj = new Alert();
            //        obj.ID = Guid.NewGuid();
            //        obj.BillID = bill.ID;
            //        obj.Caption = "交货提醒";
            //        obj.Text = string.Format("客户:[{0}],唛头:[{1}]的{2}单[{3}]交货日期是:[{4}].请尽快发货。\r\n备注:{5}", customer == null ? string.Empty : customer.Name, bill.MainMark, billName, bill.BillNo, bill.DeliveryDate.ToString("yyyy-MM-dd"), bill.Remark);
            //        obj.AddTime = DateTime.Now;
            //        insertlist.Add(obj);
            //    }
            //    if (bill.CompanyID != null)  
            //    {
            //        Company company = BLLFty.Create<CompanyBLL>().GetCompany().Find(o => o.ID == bill.CompanyID && o.Type == 1);//外销客户
            //        if (bill.Status == 1 && company != null && company.AccountPeriod.HasValue && company.AccountPeriod.Value > 0 && bill.BillDate.AddDays(company.AccountPeriod.Value) <= DateTime.Now)  //外销账期，如交货后45天收款
            //        {
            //            Alert obj = new Alert();
            //            obj.ID = Guid.NewGuid();
            //            obj.BillID = bill.ID;
            //            obj.Caption = "收款提醒";
            //            obj.Text = string.Format("客户:[{0}],唛头:[{1}]的{2}单[{3}]的账期{4}天已到.可以收款了。\r\n备注:{5}", company.Name, bill.MainMark, billName, bill.BillNo, company.AccountPeriod.Value, bill.Remark);
            //            obj.AddTime = DateTime.Now;
            //            insertlist.Add(obj);
            //        }
            //    }
            //}
            //if (dellist.Count > 0 || insertlist.Count > 0)
            //    BLLFty.Create<AlertBLL>().Insert(dellist, insertlist);
        }

        public static void SetSelected(PageGroup pageGroupCore, MainMenu mainMenu)
        {
            BaseContentContainer documentContainer = pageGroupCore.Parent as BaseContentContainer;
            if (documentContainer != null)
            {
                //进入一级菜单
                WindowsUIView view = documentContainer.Manager.View as WindowsUIView;
                if (view != null)
                {
                    pageGroupCore = MainForm.groupsItemDetailList[mainMenu.ParentID.Value];
                    view.ActivateContainer(MainForm.groupsItemDetailList[mainMenu.ParentID.Value]);
                }
                //进入二级菜单
                int i = 0, index = 0;
                foreach (MainMenu mm in userMenuList.FindAll(o => o.ParentID == mainMenu.ParentID))
                {
                    if (MainForm.hasItemDetailPage[mm.Name] == null)
                    {
                        MainForm.itemDetailPageList[mm.Name].LoadBusinessData(MainForm.mainMenuList[mm.Name]);
                        MainForm.hasItemDetailPage.Add(mm.Name, true);
                    }
                    if (mm.Name == mainMenu.Name)
                        index = i;
                    ++i;
                }
                pageGroupCore.SetSelected(pageGroupCore.Items[index]);
                view.ActivateContainer(pageGroupCore);
            }
        }

        /// <summary>
        /// 获取最大单号
        /// </summary>
        /// <param name="billType">单据类型</param>
        /// <param name="IsCreated">是否创建新单</param>
        /// <returns></returns>
        public static SystemStatus GetMaxBillNo(MainMenuEnum menuEnum, bool IsCreated)
        {
            string prefix = string.Empty;
            MainMenu menu = allMainMenuList.FirstOrDefault(o => o.Name.Equals(menuEnum.ToString()));
            if (menu != null)
                prefix = menu.Prefix;
            string no = prefix + DateTime.Now.ToString("yyyyMMdd") + "000";
            SystemStatus entity = null;
            List<SystemStatus> list = BLLFty.Create<BaseBLL>().GetListBy<SystemStatus>(null);
            if (list != null)
            {
                entity = list.FirstOrDefault(o => o.MainMenuName.Equals(menuEnum.ToString()));
                if (entity != null)
                    no = entity.MaxBillNo.Trim();
            }
            if (entity == null)
            {
                entity = new SystemStatus()
                {
                    MainMenuName = menuEnum.ToString(),
                    MaxBillNo = no,
                    Status = 0
                };
            }
            if (IsCreated)
            {
                // 单号流水+1
                if (no.Length == 13)
                {
                    // 今天的单号
                    if (no.Substring(2, 8).Equals(DateTime.Now.ToString("yyyyMMdd")))
                        no = prefix + DateTime.Now.ToString("yyyyMMdd") + Convert.ToString(int.Parse(no.Substring(10, 3)) + 1).PadLeft(3, '0');
                    else
                        no = prefix + DateTime.Now.ToString("yyyyMMdd") + "001";
                    entity.MaxBillNo = no;
                }
                else
                {

                    XtraMessageBox.Show("单号格式错误。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            BLLFty.Create<BaseBLL>().AddOrUpdate<SystemStatus>(entity);
            return entity;
        }
        
        /// <summary>
        /// 获得外箱规格对应的体积
        /// </summary>
        /// <param name="meas"></param>
        /// <returns></returns>
        public static decimal GetVolume(string meas)
        {
            decimal iVolume = 1;
            string sVolume = Regex.Replace(meas, "[X-×-&]", "*");
            foreach (string item in sVolume.Split('*'))
            {
                if (Rexlib.IsValidDecimal2(item))
                    iVolume *= decimal.Parse(item);
            }
            return iVolume;
        }

        //static VStaffSchClass GetStaffSchClass(AttGeneralLog log, Guid? deptID)
        //{
        //    //AttFlag:True表示签到，False表示签退
        //    VStaffSchClass result = null;
        //    List<VStaffSchClass> schList =clientFactory.GetData<VStaffSchClass>().FindAll(o => o.DeptID == deptID);
        //    VStaffSchClass schIn = schList.FirstOrDefault(o =>
        //        o.CheckInStartTime.Value.TimeOfDay <= log.AttTime.TimeOfDay && o.CheckInEndTime.Value.TimeOfDay >= log.AttTime.TimeOfDay);
        //    if (schIn == null)
        //    {
        //        VStaffSchClass schOut = schList.FirstOrDefault(o =>
        //            o.CheckOutStartTime.Value.TimeOfDay <= log.AttTime.TimeOfDay && o.CheckOutEndTime.Value.TimeOfDay >= log.AttTime.TimeOfDay);
        //        if (schOut != null)
        //        {
        //            log.AttFlag = false;
        //            result = schOut;
        //        }
        //    }
        //    else
        //    {
        //        log.AttFlag = true;
        //        result = schIn;
        //    }
        //    return result;
        //}

        //public static void GetAttAppointments()
        //{
        //    //添加考勤报表
        //    List<AttGeneralLog> attGLogs =clientFactory.GetData<AttGeneralLog>();
        //    Hashtable hasAtt = new Hashtable();
        //    List<AttAppointments> aptList = BLLFty.Create<AttAppointmentsBLL>().GetAttAppointments();
        //    List<AttAppointments> aptInsertList = new List<AttAppointments>();
        //    List<AttAppointments> aptUpdateList = new List<AttAppointments>();
        //    //List<VAttAppointments> vaptList = new List<VAttAppointments>();
        //    AttAppointments apt = null;
        //    VStaffSchClass vssc = null;
        //    foreach (AttGeneralLog log in attGLogs)
        //    {
        //        UsersInfo user = clientFactory.GetData<UsersInfo>().FirstOrDefault(o => o.Code == log.EnrollNumber && o.IsDel == false);
        //        if (user == null)
        //            continue;
        //        if (log.SchClassID == null || log.SchClassID == Guid.Empty)
        //        {
        //            vssc = GetStaffSchClass(log, user.DeptID);
        //            if (vssc == null)
        //                continue;  //排除那些无意义的打卡记录
        //        }
        //        else
        //            vssc =clientFactory.GetData<VStaffSchClass>().FirstOrDefault(o =>
        //                o.SchClassID == log.SchClassID && o.DeptID == user.DeptID);
        //        if (user != null && vssc != null)
        //        {
        //            if (hasAtt[log.EnrollNumber + log.AttTime.ToString("yyyyMMdd") + vssc.Name] == null)
        //            //AttAppointments apt = aptList.FirstOrDefault(o => o.UserID == user.ID
        //            //    && (o.CheckInTime.Value.ToString("yyyyMM") == log.AttTime.ToString("yyyyMM") || o.CheckOutTime.Value.ToString("yyyyMM") == log.AttTime.ToString("yyyyMM"))
        //            //    && o.SchClassName == vssc.Name);
        //            //if (apt == null)
        //            {
        //                apt = new AttAppointments();
        //                apt.UserID = user.ID;
        //                apt.SchClassID = vssc.SchClassID;
        //                apt.SchClassName = vssc.Name;
        //                apt.SchSerialNo = vssc.SchSerialNo;
        //                apt.SchStartTime = vssc.StartTime;
        //                apt.SchEndTime = vssc.EndTime;
        //                if (log.AttFlag)
        //                {
        //                    apt.CheckInTime = log.AttTime;
        //                    apt.GLogStartID = log.ID;
        //                    int late = (int)(log.AttTime.TimeOfDay - vssc.StartTime.Value.TimeOfDay).TotalMinutes;
        //                    if (late > vssc.LateMinutes)
        //                        apt.LateMinutes = late;
        //                }
        //                else if (log.AttFlag == false)
        //                {
        //                    apt.CheckOutTime = log.AttTime;
        //                    apt.GLogEndID = log.ID;
        //                    int early = (int)(vssc.EndTime.Value.TimeOfDay - log.AttTime.TimeOfDay).TotalMinutes;
        //                    if (early > vssc.EarlyMinutes)
        //                        apt.EarlyMinutes = early;
        //                }
        //                apt.AttStatus = log.AttStatus;
        //                apt.Description = log.Description;
        //                //apt = SetAttStatus(apt);
        //                //aptInsertList.Add(apt);
        //                hasAtt.Add(log.EnrollNumber + log.AttTime.ToString("yyyyMMdd") + vssc.Name, apt);
        //            }
        //            else
        //            {
        //                apt = hasAtt[log.EnrollNumber + log.AttTime.ToString("yyyyMMdd") + vssc.Name] as AttAppointments;
        //                apt.UserID = user.ID;
        //                apt.SchClassID = vssc.SchClassID;
        //                apt.SchClassName = vssc.Name;
        //                apt.SchSerialNo = vssc.SchSerialNo;
        //                apt.SchStartTime = vssc.StartTime;
        //                apt.SchEndTime = vssc.EndTime;
        //                if (log.AttFlag)
        //                {
        //                    apt.CheckInTime = log.AttTime;
        //                    apt.GLogStartID = log.ID;
        //                    int late = (int)(log.AttTime.TimeOfDay - vssc.StartTime.Value.TimeOfDay).TotalMinutes;
        //                    if (late > vssc.LateMinutes)
        //                        apt.LateMinutes = late;
        //                }
        //                else if (log.AttFlag == false)
        //                {
        //                    apt.CheckOutTime = log.AttTime;
        //                    apt.GLogEndID = log.ID;
        //                    int early = (int)(vssc.EndTime.Value.TimeOfDay - log.AttTime.TimeOfDay).TotalMinutes;
        //                    if (early > vssc.EarlyMinutes)
        //                        apt.EarlyMinutes = early;
        //                }
        //                apt.AttStatus = log.AttStatus;
        //                apt.Description = log.Description;
        //                //apt = SetAttStatus(apt);
        //                //aptUpdateList.Add(apt);
        //                hasAtt[log.EnrollNumber + log.AttTime.ToString("yyyyMMdd") + vssc.Name] = apt;
        //            }
        //        }
        //    }

        //    foreach (DictionaryEntry de in hasAtt)
        //    {
        //        AttAppointments obj = de.Value as AttAppointments;               
        //        if (obj.AttStatus < (int)AttStatusType.Absent)  //判断考勤状态
        //        {
        //            if (obj.LateMinutes != null && obj.LateMinutes > 0)
        //                obj.AttStatus = (int)AttStatusType.Late;
        //            if (obj.EarlyMinutes != null && obj.EarlyMinutes > 0)
        //                obj.AttStatus = (int)AttStatusType.Early;
        //            if (obj.LateMinutes != null && obj.LateMinutes > 0 && obj.EarlyMinutes != null && obj.EarlyMinutes > 0)
        //                obj.AttStatus = (int)AttStatusType.LateEarly;
        //            if (obj.CheckInTime == null)
        //            {
        //                obj.AttStatus = (int)AttStatusType.NoCheckIn;
        //                obj.CheckInTime = obj.CheckOutTime.Value.Date;
        //            }
        //            if (obj.CheckOutTime == null)
        //            {
        //                obj.AttStatus = (int)AttStatusType.NoCheckOut;
        //                obj.CheckOutTime = obj.CheckInTime.Value.Date;
        //            }
        //            if (obj.CheckInTime == null && obj.CheckOutTime == null)
        //                obj.AttStatus = (int)AttStatusType.Absent;
        //        }
        //        else
        //        {
        //            if (obj.CheckInTime == null)
        //                obj.CheckInTime = obj.CheckOutTime.Value.Date;
        //            if (obj.CheckOutTime == null)
        //                obj.CheckOutTime = obj.CheckInTime.Value.Date;
        //            if (obj.LateMinutes != null && obj.LateMinutes >= 0)
        //                obj.LateMinutes = null;
        //            if (obj.EarlyMinutes != null && obj.EarlyMinutes >= 0)
        //                obj.EarlyMinutes = null;
        //        }
        //        if (obj.LateMinutes.GetValueOrDefault() > 0 || obj.EarlyMinutes.GetValueOrDefault() > 0)
        //            obj.Location = string.Format("{0} {1}", obj.LateMinutes, obj.EarlyMinutes);
        //        obj.Subject = EnumHelper.GetDescription<AttStatusType>((AttStatusType)obj.AttStatus, false);

        //        AttAppointments attApt = aptList.FirstOrDefault(o => o.UserID == obj.UserID
        //            && o.CheckInTime.Value.ToString("yyyyMMdd") == obj.CheckInTime.Value.ToString("yyyyMMdd")
        //            && o.SchClassName == obj.SchClassName);
        //        if (attApt == null)
        //            aptInsertList.Add(obj);
        //        else
        //        {
        //            attApt.SchClassID = obj.SchClassID;
        //            attApt.SchClassName = obj.SchClassName;
        //            attApt.SchSerialNo = obj.SchSerialNo;
        //            attApt.SchStartTime = obj.SchStartTime;
        //            attApt.SchEndTime = obj.SchEndTime;
        //            attApt.CheckInTime = obj.CheckInTime;
        //            attApt.CheckOutTime = obj.CheckOutTime;
        //            attApt.LateMinutes = obj.LateMinutes;
        //            attApt.EarlyMinutes = obj.EarlyMinutes;
        //            attApt.AttStatus = obj.AttStatus;
        //            attApt.Subject = obj.Subject;
        //            attApt.Location = obj.Location;
        //            attApt.Description = obj.Description;
        //            aptUpdateList.Add(attApt);
        //        }
        //    }
        //    BLLFty.Create<AttAppointmentsBLL>().Save(aptInsertList, aptUpdateList);
        //}

        public static void SetQueryPageGridColumn(DevExpress.XtraGrid.Views.Grid.GridView gv, MainMenuEnum menu)
        {
            ////gv.BestFitColumns();
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gv.Columns)
            {
                //if (col.Name.ToUpper() == "colID".ToUpper() || col.Name.ToUpper() == "colHdID".ToUpper())
                if (col.Name.ToUpper().Contains("ID".ToUpper()) || col.Name.ToUpper() == "COLTYPE" || col.Name.ToUpper() == "COLPICPATH" || col.Name.ToUpper().Contains("SERIALNO") || col.Name.ToUpper().Contains("PASSWORD"))
                    col.Visible = false;
                if (col.FieldName == "Price")
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "c2";
                }
                if (col.ColumnType.Equals(typeof(System.Data.Linq.Binary)))
                {
                    col.Width = 50;  //调整图片的列宽度
                }
                if (col.FieldName.Contains("BillNo") || col.FieldName.Contains("单号") || col.FieldName.Contains("编号"))
                    col.Width = 120;
                if (col.FieldName == "已退料入库")
                    col.Width = 80;
                if (col.FieldName == "单价" || col.FieldName == "售价" || col.FieldName == "进价" || col.FieldName == "成本价")
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "c5";
                }
                if (col.FieldName.Contains("AMT") || col.FieldName.Contains("金额") || col.FieldName == "额外费用" ||
                    col.FieldName == "成本" || col.FieldName == "毛利")
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "c";
                }
                if (col.FieldName.Contains("率") || col.FieldName.ToLower().Contains("rate"))
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "p";
                }
                if (col.FieldName.Contains("数量"))
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "n";
                }
                if (col.FieldName == "去税单价")
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "c6";
                }
                if (col.FieldName == "上班时间" || col.FieldName == "下班时间" || col.FieldName == "签到时间" || col.FieldName == "签退时间")
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    col.DisplayFormat.FormatString = "T";
                }
                if (col.FieldName == "出勤时间")
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    col.DisplayFormat.FormatString = "G";
                    col.Width = 130;
                }
                if (col.FieldName == "类型" || col.FieldName == "订单类型" || col.FieldName == "状态")
                    col.AppearanceHeader.ForeColor = Color.Blue;
                //else if (col.FieldName == "品名")
                //{
                //    //总计
                //    col.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                //    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count,"货号", "总计:{0}")});
                //    //组计
                //    gv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "货号", col, "小组合计:{0}");
                //}

                if ((menu == MainMenuEnum.InventoryQuery || menu == MainMenuEnum.MaterialInventoryQuery))
                {
                    if (col.FieldName == "货号")
                    {
                        col.GroupIndex = 0;
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                    else if (col.FieldName == "品名")
                    {
                        col.BestFit();
                    }
                }
                else if (menu == MainMenuEnum.InventoryGroupByGoodsQuery)
                {
                    if (col.FieldName == "仓库类型")
                    {
                        col.GroupIndex = 0;
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                    else if (col.FieldName == "货号")
                    {
                        col.BestFit();
                    }
                }
                else if (menu == MainMenuEnum.EMSInventoryQuery)
                {
                    if (col.FieldName == "供应商名称")
                    {
                        col.GroupIndex = 0;
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                    else if (col.FieldName == "供应商代码")
                    {
                        col.BestFit();
                    }
                }
                else if (menu == MainMenuEnum.FSMInventoryQuery)
                {
                    if (col.FieldName == "供应商名称")
                    {
                        col.GroupIndex = 0;
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                    else if (col.FieldName == "供应商代码")
                    {
                        col.BestFit();
                    }
                }
                else if (menu == MainMenuEnum.AccountBookQuery)
                {
                    if (col.FieldName == "仓库")
                    {
                        col.GroupIndex = 0;
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                    }
                    else if (col.FieldName == "货号")
                    {
                        col.GroupIndex = 1;
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                    else if (col.FieldName == "品名")
                    {
                        col.BestFit();
                    }
                }
                else if ((menu == MainMenuEnum.EMSGoodsTrackingDailyReport || menu == MainMenuEnum.FSMGoodsTrackingDailyReport))
                {
                    if (col.FieldName == "委托厂商")
                        col.GroupIndex = 0;
                    else if (col.FieldName == "日期")
                    {
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                        col.BestFit();
                    }
                }
                else if (menu == MainMenuEnum.SampleStockOutReport)
                {
                    if (col.FieldName == "客户名称")
                        col.GroupIndex = 0;
                    else if (col.FieldName == "出库单号" || col.FieldName == "出库日期" || col.FieldName == "交货日期" || col.FieldName == "仓库" || col.FieldName == "类型" ||
                        col.FieldName == "制单人" || col.FieldName == "制单日期" || col.FieldName == "审核人" || col.FieldName == "审核日期" || col.FieldName == "状态")
                        col.Visible = false;
                }
                else if (menu == MainMenuEnum.StatementOfAccountToCustomer)
                {
                    if (col.FieldName == "收款单号")
                    {
                        col.GroupIndex = 0;
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                    }
                    //else if (col.FieldName == "客户代码")
                    //    col.Visible = false;
                    //else if (col.FieldName == "客户名称")
                    //    col.Visible = false;
                    else if (col.FieldName == "客户类型")
                        col.Visible = false;
                    else if (col.FieldName == "货品类型代码")
                        col.Visible = false;
                    else if (col.FieldName == "货品类型名称")
                        col.Visible = false;
                    else if (col.FieldName == "单位")
                        col.Visible = false;
                    else if (col.FieldName == "规格")
                        col.Visible = false;
                    else if (col.FieldName == "体积")
                        col.Visible = false;
                    else if (col.FieldName == "毛重")
                        col.Visible = false;
                    else if (col.FieldName == "净重")
                        col.Visible = false;
                    else if (col.FieldName == "结算类型")
                    {
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                    else if (col.FieldName == "出库日期")
                    {
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                    else if (col.FieldName == "收款日期")
                    {
                        col.BestFit();
                    }
                }
                else if (menu == MainMenuEnum.StatementOfAccountToSupplier)
                {
                    if (col.FieldName == "付款单号")
                    {
                        col.GroupIndex = 0;
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                    }
                    //else if (col.FieldName == "供应商代码")
                    //    col.Visible = false;
                    //else if (col.FieldName == "供应商名称")
                        //col.Visible = false;
                    else if (col.FieldName == "货品类型代码")
                        col.Visible = false;
                    else if (col.FieldName == "货品类型名称")
                        col.Visible = false;
                    else if (col.FieldName == "结算类型")
                    {
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                    else if (col.FieldName == "结算日期")
                    {
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                    else if (col.FieldName == "付款日期")
                    {
                        col.BestFit();
                    }
                }
                else if (menu == MainMenuEnum.AlertQuery)
                {
                    if (col.FieldName == "标题")
                    {
                        col.GroupIndex = 0;
                    }
                }
                else if (menu == MainMenuEnum.AttGeneralLog)
                {
                    if (col.FieldName == "姓名")
                        col.GroupIndex = 0;
                    //else if (col.FieldName == "工号")
                    //    col.Width = 75;
                    else if (col.FieldName == "出勤日期")
                    {
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                }
                else if (menu == MainMenuEnum.AttendanceQuery)
                {
                    if (col.FieldName == "姓名")
                        col.GroupIndex = 0;
                    else if (col.FieldName == "工号")
                        col.Width = 75;
                    else if (col.FieldName == "年月")
                        col.GroupIndex = 1;
                    else if (col.FieldName == "日期")
                    {
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                }
                else if (menu == MainMenuEnum.SchedulingQuery)
                {
                    if (col.FieldName == "姓名")
                        col.GroupIndex = 0;
                    else if (col.FieldName == "工号")
                        col.Width = 75;
                    else if (col.FieldName == "年月")
                        col.GroupIndex = 1;
                    else if (col.FieldName == "日期")
                    {
                        col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                }
                if ((MainForm.Company.Contains("大正") || MainForm.Company.Contains("纸")) && (col.FieldName == "仓库类型" || col.FieldName == "客户类型"))
                    col.Visible = false;

                SetColumnCaption(menu, col);
                if (menu.Equals(MainMenuEnum.VStocktakingLog) ||
                    menu.Equals(MainMenuEnum.VProfitAndLossLog) ||
                    menu.Equals(MainMenuEnum.VUnlistedGoodsLog) ||
                    menu.Equals(MainMenuEnum.VInventoryLossLog) ||
                    menu.Equals(MainMenuEnum.VInventoryLog))
                {
                    if (col.FieldName.Equals("BillNo"))
                    {
                        col.GroupIndex = 0;
                    }
                    if (col.FieldName.Equals("DeptName"))
                        col.GroupIndex = 1;
                }
                if (menu.Equals(MainMenuEnum.ProfitAndLoss))
                {
                    // 列允许编辑
                    if (col.FieldName.Equals("TransitQty") ||
                        col.FieldName.Equals("NonArrivalQty") ||
                        col.FieldName.Equals("NonInStoreQty") ||
                        col.FieldName.Equals("SoldQty") ||
                        col.FieldName.Equals("NonChargeOffQty") ||
                        col.FieldName.Equals("ReturnedQty") ||
                        col.FieldName.Equals("GroupBuyingQty") ||
                        col.FieldName.Equals("OtherQty") ||
                        col.FieldName.Equals("ScrapQty") ||
                        col.FieldName.Equals("Remark"))
                    {
                        col.AppearanceHeader.ForeColor = Color.Green;
                        col.OptionsColumn.AllowEdit = true;
                    }
                    else
                        col.OptionsColumn.AllowEdit = false;
                    // 列冻结--品名之前冻结
                    EnumHelper.GetEnumValues<VProfitAndLossLogEnum>(true).ForEach(o => {
                        if (o.Name.Equals(col.FieldName) && o.Value <= VProfitAndLossLogEnum.GoodsName)
                            col.Fixed = FixedStyle.Left;
                    });

                }
                if (menu.Equals(MainMenuEnum.UnlistedGoods))
                {
                    // 列允许编辑
                    if (col.FieldName.Equals("ScrapQty") ||
                        col.FieldName.Equals("Remark"))
                    {
                        col.AppearanceHeader.ForeColor = Color.Green;
                        col.OptionsColumn.AllowEdit = true;
                    }
                    else
                        col.OptionsColumn.AllowEdit = false;
                    // 列冻结--品名之前冻结
                    //EnumHelper.GetEnumValues<UnlistedGoodsEnum>(true).ForEach(o => {
                    //    if (o.Name.Equals(col.FieldName) && o.Value <= UnlistedGoodsEnum.GoodsName)
                    //        col.Fixed = FixedStyle.Left;
                    //});

                }
                col.BestFit();
            }

            //设置合计列
            SetSummaryItemColumns(gv);
            
        }

        static void SetColumnCaption(MainMenuEnum menuEnum, GridColumn col)
        {
            string enumTypeName = menuEnum.ToString() + "Enum";
            ListItem st = EnumHelper.GetEnumValues(nameof(USL), enumTypeName, false).FirstOrDefault(o => o.Value.ToString().Equals(col.FieldName));
            if (st != null)
                col.Caption = st.Name;
            else
                col.Visible = false;
            col.BestFit();
        }

        public static void SetColumnCaption(string typeName, FilterColumn col)
        {
            string enumTypeName = typeName + "Enum";
            ListItem st = EnumHelper.GetEnumValues(nameof(USL), enumTypeName, false).FirstOrDefault(o => o.Value.ToString().Equals(col.FieldName));
            if (st != null)
                col.SetColumnCaption(st.Name);
        }

        public static void SetSummaryItemColumns(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            gv.GroupSummary.Clear();
            foreach (GridColumn col in gv.Columns)
            {

                if (col.FieldName == "GoodsName")
                {
                    col.Summary.Clear();
                    //合计说明
                    col.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GoodsName", "总计:")});

                    gv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "GoodsName", col, "小组合计:");
                }
                else
                {
                    string sumFormat = string.Empty;
                    if (col.FieldName.ToUpper().Contains("AMT"))
                        sumFormat = "{0:c}";
                    else if (col.FieldName.ToLower().Contains("qty"))
                        sumFormat = "{0}";
                    if (sumFormat != string.Empty)
                    {
                        col.Summary.Clear();
                        //总计
                        col.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, col.FieldName, sumFormat)});
                        //组计
                        gv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, col.FieldName, col, sumFormat);
                    }
                }
                col.BestFit();
            }
            //foreach (string item in Enum.GetNames(typeof(SummaryItemColumns)))
            //{
            //    GridColumn col = gv.Columns.FirstOrDefault(c => c.FieldName == item);
            //    if (col != null)
            //    {
            //        if (col.FieldName == "GoodsName")
            //        {
            //            //合计说明
            //            col.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            //            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GoodsName", "总计:")});

            //            gv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "品名", col, "小组合计:");
            //        }
            //        else
            //        {
            //            string sumFormat = string.Empty;
            //            if (item.Contains("AMT") || item.Contains("金额") || item.Contains("工资") || item.Contains("福利") || item.Contains("扣款") || item.Contains("代扣"))
            //                sumFormat = "{0:c}";
            //            else
            //                sumFormat = "{0}";
            //            //总计
            //            col.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            //            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, item, sumFormat)});
            //            //组计
            //            gv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, item, col, sumFormat);
            //        }
            //    }
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        //private void GetVDataSource()
        //{
        //    threadGetVDataSource = new Thread(GetViewDataSource);
        //    threadGetVDataSource.Start();

        //    ////threadInsertAlert = new Thread(InsertAlert);
        //    ////threadInsertAlert.Start();
        //}

        /// <summary>
        /// 状态栏信息设置
        /// </summary>
        private void SetStateBarInfo()
        {
            threadGetUserInfo = new Thread(GetUserInfo);
            threadGetUserInfo.Start();

            ////threadInsertAlert = new Thread(InsertAlert);
            ////threadInsertAlert.Start();
        }

        private void GetUserInfo()
        {
            if (usersInfo != null)
                lblUser.Caption = usersInfo.Name;
        }

        private delegate void SetErrorPanelInfoDelegate();
        /// <summary>
        /// 错误面板设置
        /// </summary>
        private void SetErrorPanelInfo()
        {
            if (this.InvokeRequired)
            {
                SetErrorPanelInfoDelegate setDelegate = new SetErrorPanelInfoDelegate(InnerSetErrorPanelInfo);
                this.Invoke(setDelegate);
            }
            else
            {
                InnerSetErrorPanelInfo();
            }

        }

        private void InnerSetErrorPanelInfo()
        {
            //错误面板设置
            // dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            this.errorInfoControl1.OnShowPanel += delegate(object sender, EventArgs e1)
            {
                if (this.InvokeRequired)
                {
                    SetErrorPanelInfoDelegate setDelegate = new SetErrorPanelInfoDelegate(InnerShowDockPanel1);
                    this.Invoke(setDelegate);
                }
                else
                {
                    InnerShowDockPanel1();
                }

            };
            this.errorInfoControl1.OnHidePanel += delegate(object sender, EventArgs e2)
            {
                if (this.InvokeRequired)
                {
                    SetErrorPanelInfoDelegate setDelegate = new SetErrorPanelInfoDelegate(InnerHideDockPanel);
                    this.Invoke(setDelegate);
                }
                else
                {
                    InnerHideDockPanel();
                }

            };
        }

        private void InnerHideDockPanel()
        {
            dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
        }

        private void InnerShowDockPanel1()
        {
            dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            dockPanel1.Show();
        }

        void CreateLayout()
        {
            //foreach (SampleDataGroup group in dataSource.Data.Groups)
            foreach (MainMenu group in userMenuList.FindAll(o => o.ParentID == null && o.CheckBoxState))// 父级
            {
                //根据用户权限控制是否显示Tile
                ////if (MainForm.userPermissions.Count > 0 && MainForm.userPermissions.Find(o => o.Caption.Trim() == group.Caption.Trim()).CheckBoxState)
                tileContainer.Buttons.Add(new DevExpress.XtraBars.Docking2010.WindowsUIButton(group.Caption, null, -1, DevExpress.XtraBars.Docking2010.ImageLocation.AboveText, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, null, true, -1, true, null, false, false, true, null, group, -1, false, false));
                PageGroup pageGroup = new PageGroup();
                pageGroup.Parent = tileContainer;
                //pageGroup.Caption = group.Title;
                pageGroup.Caption = group.Caption;
                windowsUIView.ContentContainers.Add(pageGroup);
                List<MainMenu> dataItemList = userMenuList.FindAll(o => o.ID == group.ID || o.ParentID == group.ID);
                if (dataItemList != null)
                {
                    groupsItemDetailPage.Add(group, CreateGroupItemDetailPage(dataItemList, pageGroup));
                    groupsItemDetailList.Add(group.ID, pageGroup);
                }
                foreach (MainMenu item in userMenuList.FindAll(o => o.ParentID == group.ID)) // 子级
                {
                    //ItemDetailPage itemDetailPage = new ItemDetailPage(item, pageGroup, groupsItemDetailList, menuList, itemDetailButtonList);
                    //itemDetailPage.Dock = System.Windows.Forms.DockStyle.Fill;
                    //BaseDocument document = windowsUIView.AddDocument(itemDetailPage);
                    //BaseDocument document = windowsUIView.AddDocument(item.Caption, item.Name);
                    ItemDetailPage itemDetailPage = new ItemDetailPage(item, pageGroup, itemDetailButtonList);
                    itemDetailPage.Dock = System.Windows.Forms.DockStyle.Fill;
                    itemDetailPageList.Add(item.Name, itemDetailPage);
                    mainMenuList.Add(item.Name, item);
                    BaseDocument document = windowsUIView.AddDocument(itemDetailPage);
                    document.Caption = item.Caption;
                    pageGroup.Items.Add(document as Document);
                    //////pageGroup.SetLength(document as Document, 5000);
                    CreateTile(document as Document, item).ActivationTarget = pageGroup;
                }
            }
            windowsUIView.ActivateContainer(tileContainer);
            //tileContainer.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(buttonClick);
        }
        Tile CreateTile(Document document, MainMenu item)
        {
            Tile tile = new Tile();
            tile.Document = document;
            //tile.Group = item.GroupName;
            tile.Group = userMenuList.Find(o => o.ID == item.ParentID).Caption;
            tile.Tag = item;
            //if (item.Name == "GoodsType")
            //根据用户权限控制是否显示Tile
            ////if (MainForm.userPermissions.Count > 0 && item.Name != MainMenuEnum.AboutBox)
            ////tile.Visible = userPermissions.Find(o => o.Caption.Trim() == item.Caption.Trim()).CheckBoxState;
            //tile.Elements.Add(CreateTileItemElement(item.Subtitle, TileItemContentAlignment.BottomLeft, Point.Empty, 9));
            //tile.Elements.Add(CreateTileItemElement(item.Subtitle, TileItemContentAlignment.Manual, new Point(0, 100), 12));
            tile.Elements.Add(CreateTileItemElement(item.Caption, TileItemContentAlignment.BottomLeft, Point.Empty, 11));
            tile.Elements.Add(CreateTileItemElement(item.Caption, TileItemContentAlignment.Manual, new Point(0, 100), 12));
            tile.Appearances.Selected.BackColor = tile.Appearances.Hovered.BackColor = tile.Appearances.Normal.BackColor = Color.FromArgb(45, 116, 169);//Color.FromArgb(140, 140, 140);//GetRandomColor(); 
            tile.Appearances.Selected.BorderColor = tile.Appearances.Hovered.BorderColor = tile.Appearances.Normal.BorderColor = Color.FromArgb(45, 116, 169);
            tile.Click += new TileClickEventHandler(tile_Click);
            windowsUIView.Tiles.Add(tile);
            tileContainer.Items.Add(tile);
            return tile;
        }

        #region 获得随机颜色
        
        public static System.Drawing.Color GetRandomColor()
        {
            Random randomNum_1 = new Random(Guid.NewGuid().GetHashCode());
            System.Threading.Thread.Sleep(randomNum_1.Next(1));
            int int_Red = randomNum_1.Next(255);

            Random randomNum_2 = new Random((int)DateTime.Now.Ticks);
            int int_Green = randomNum_2.Next(255);

            Random randomNum_3 = new Random(Guid.NewGuid().GetHashCode());

            int int_Blue = randomNum_3.Next(255);
            int_Blue = (int_Red + int_Green > 380) ? int_Red + int_Green - 380 : int_Blue;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;


            return GetDarkerColor(System.Drawing.Color.FromArgb(int_Red, int_Green, int_Blue));
        }

        //获取加深颜色
        public static Color GetDarkerColor(Color color)
        {
            const int max = 255;
            int increase = new Random(Guid.NewGuid().GetHashCode()).Next(30, 255); //还可以根据需要调整此处的值

            int r = Math.Abs(Math.Min(color.R - increase, max));
            int g = Math.Abs(Math.Min(color.G - increase, max));
            int b = Math.Abs(Math.Min(color.B - increase, max));

            return Color.FromArgb(r, g, b);
        }

        #endregion

        TileItemElement CreateTileItemElement(string text, TileItemContentAlignment alignment, Point location, float fontSize)
        {
            TileItemElement element = new TileItemElement();
            element.TextAlignment = alignment;
            if (!location.IsEmpty) element.TextLocation = location;
            element.Appearance.Normal.Options.UseFont = true;
            element.Appearance.Normal.Font = new System.Drawing.Font(new FontFamily("Segoe UI Symbol"), fontSize);
            element.Text = text;
            return element;
        }
        void tile_Click(object sender, TileClickEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                PageGroup page = ((e.Tile as Tile).ActivationTarget as PageGroup);
                if (page != null)
                {
                    MainMenu menu = e.Tile.Tag as MainMenu;
                    foreach (MainMenu item in userMenuList.FindAll(o => o.ParentID == menu.ParentID))
                    {
                        if (hasItemDetailPage[item.Name] == null)
                        {
                            itemDetailPageList[item.Name].LoadBusinessData(item);
                            hasItemDetailPage.Add(item.Name, true);
                        }
                    }
                    //if (hasItemDetailPage[menu.Name] == null)
                    //{
                    //    itemDetailPageList[menu.Name].LoadBusinessData(menu);
                    //    hasItemDetailPage.Add(menu.Name, true);
                    //}
                    if (menu.Name == MainMenuEnum.AboutBox.ToString())
                    {
                        AboutBox ab = new AboutBox();
                        ab.ShowDialog();
                        e.Handled = true;
                    }
                    page.Parent = tileContainer;
                    page.SetSelected((e.Tile as Tile).Document);
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

        //PageGroup CreateGroupItemDetailPage(SampleDataGroup group, PageGroup child)
        PageGroup CreateGroupItemDetailPage(List<MainMenu> group, PageGroup child)
        {
            GroupDetailPage page = new GroupDetailPage(group, child);
            PageGroup pageGroup = page.PageGroup;
            BaseDocument document = windowsUIView.AddDocument(page);
            pageGroup.Parent = tileContainer;
            pageGroup.Items.Add(document as Document);
            windowsUIView.ContentContainers.Add(pageGroup);
            windowsUIView.ActivateContainer(pageGroup);
            return pageGroup;
        }
        void buttonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            //SampleDataGroup tileGroup = (e.Button.Properties.Tag as SampleDataGroup);
            MainMenu tileGroup = (e.Button.Properties.Tag as MainMenu);
            if (tileGroup != null)
            {
                windowsUIView.ActivateContainer(groupsItemDetailPage[tileGroup]);
            }
        }
        void clearPanel(DevExpress.XtraBars.BarStaticItem label)
        {
            ThreadStart start = new ThreadStart(
                delegate
                {
                    Thread.Sleep(3000);
                    label.Caption = string.Empty;
                    label.Glyph = null;
                }
                );

            Thread thread = new Thread(start);
            thread.Start();
        }

        public void AddButton(string text, Image image, EventHandler onClick)
        {
            
        }

        public void AddToolStripItem(System.Windows.Forms.ToolStripItem item)
        {
            
        }

        void IStatusbar.SetStatusBarPanel(string text)
        {
            this.Invoke(new EventHandler(delegate
            {
                lblTip.Caption = text;
                clearPanel(lblTip);
            }));
        }

        void IStatusbar.SetStatusBarPanel1(string text)
        {
            ((IStatusbar)this).SetStatusBarPanel(text);
        }

        class ImageText : IDisposable
        {
            public void Dispose()
            {
                if (Image != null)
                {
                    Image.Dispose();
                    Image = null;
                }
            }
            public string Text { get; set; }
            public Image Image { get; set; }
            public Exception AttachedException { get; set; }
        }

        Dictionary<object, ImageText> lifecycleStatusText = new Dictionary<object, ImageText>();

        void IStatusbar.SetStatusBarPanel(string text, Image image, Control lifecycleWith)
        {
            this.Invoke(new EventHandler(delegate
            {
                if (lifecycleStatusText.ContainsKey(lifecycleWith))
                {
                    lifecycleStatusText[lifecycleWith] = new ImageText { Image = image, Text = text };
                }
                else
                {
                    lifecycleStatusText.Add(lifecycleWith, new ImageText { Image = image, Text = text });
                }

                lifecycleWith.Disposed += delegate(object sender, EventArgs e)
                {
                    lifecycleStatusText.Remove(lifecycleWith);
                };

                this.lblTip.Caption = text;
                lblTip.Glyph = image;
            }));
        }

        void IStatusbar.SetStatusBarPanel(string text, StatusIconType iconType, Control lifecycleWith)
        {
            Image image;
            switch (iconType)
            {
                case StatusIconType.Warning:
                    image =global::USL.Properties.Resources.Warning;// global ::ICP.Framework.Client.Properties.Resources.warning;
                    break;
                case StatusIconType.Error:
                    image = global::USL.Properties.Resources.Warning;// global ::ICP.Framework.Client.Properties.Resources.warning;
                    break;
                case StatusIconType.Info:
                    image = global::USL.Properties.Resources.Info;// global ::ICP.Framework.Client.Properties.Resources.info;
                    break;
                default:
                    image = null;
                    break;
            }
            ((IStatusbar)this).SetStatusBarPanel(text, image, lifecycleWith);
        }

        void IStatusbar.SetStatusBarPanel(Exception ex, Control lifecycleWith)
        {
            Image image = global::USL.Properties.Resources.mark;// global::ICP.Framework.Client.Properties.Resources.warning;
            string text = ex.Message;
            this.Invoke(new EventHandler(delegate
            {
                if (lifecycleStatusText.ContainsKey(lifecycleWith))
                {
                    lifecycleStatusText[lifecycleWith] = new ImageText { Text = text, Image = image, AttachedException = ex };
                }
                else
                {
                    lifecycleStatusText.Add(lifecycleWith, new ImageText { Text = text, Image = image, AttachedException = ex });
                }
                lifecycleWith.Disposed += delegate(object sender, EventArgs e)
                {
                    lifecycleStatusText.Remove(lifecycleWith);
                };

                lblTip.Caption = text;
                lblTip.Glyph = image;
            }));
        }

        delegate void _addNotifyIconDelegate(string text, Image image, EventHandler click);
        public void AddNotifyIcon(string text, Image image, EventHandler click)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new _addNotifyIconDelegate(AddNotifyIcon), new object[] { text, image, click });
                return;
            }

            DevExpress.XtraBars.BarButtonItem item = new BarButtonItem();
            item.Caption = text;
            item.Glyph = image;
            item.ItemClick += delegate(object sender, ItemClickEventArgs e) { click(sender, EventArgs.Empty); };
            this.status.AddItem(item);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (authorised)
            {
                System.Windows.Forms.DialogResult result = XtraMessageBox.Show("确定要退出系统吗?", "操作提示",
                    System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// 切换界面事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowsUIView_DocumentActivated(object sender, DocumentEventArgs e)
        {
            if (e.Document.Caption.Equals("存货盘点"))
            {
                ItemDetailPage page = itemDetailPageList[MainMenuEnum.Stocktaking.ToString()];
                if (page != null)
                {
                    if (page.btnImportCheck != null)
                    {
                        int status = 0;
                        SystemStatus systemStatus = BLLFty.Create<BaseBLL>().GetListBy<SystemStatus>(null).FirstOrDefault(o => o.MainMenuName.Equals(MainMenuEnum.Stocktaking.ToString()));
                        if (systemStatus != null)
                            status = systemStatus.Status;
                        page.btnImportCheck.Caption = string.Format("盘点导入【{0}】", EnumHelper.GetDescription<StocktakingStatusEnum>((StocktakingStatusEnum)status, false));
                    }
                    // 刷新绑定数据
                    page.BindData();
                }
            }
            //if (e.Document.Caption.Equals("盘点差异表"))
            //{
            //    DataQueryPage page = itemDetailPageList[MainMenuEnum.ProfitAndLoss].itemDetail as DataQueryPage;
            //    if (page != null)
            //    {
            //        List<ProfitAndLoss> plList = calcFinalDiff();
            //        page.InitGrid(plList);
            //    }
            //}
        }

        /// <summary>
        /// 计算差异表-实际差异
        /// </summary>
        public static ProfitAndLoss calcFinalDiff( ProfitAndLoss entity)
        {
            //List<ProfitAndLoss> plList = BLLFty.Create<BaseBLL>().GetListBy<ProfitAndLoss>(null);
            //if (plList != null && plList.Count > 0)
            //{
                //plList.ForEach(entity => {
                    PropertyInfo[] properties = entity.GetType().GetProperties();
                    foreach (PropertyInfo p in properties)
                    {
                        if (p.Name.ToUpper().Contains("AMT"))
                        {
                            PropertyInfo pQty = entity.GetType().GetProperty(p.Name.Replace("AMT", "Qty"));
                            decimal amt = pQty.GetValue(entity) == null ? 0 : Math.Round((int)pQty.GetValue(entity) * entity.Price, 2);
                            if (p.PropertyType.IsGenericType)
                            {
                                Type genericTypeDefinition = p.PropertyType.GetGenericTypeDefinition();
                                if (genericTypeDefinition == typeof(Nullable<>))
                                {
                                    p.SetValue(entity, Convert.ChangeType(amt, Nullable.GetUnderlyingType(p.PropertyType)), null);
                                }
                            }
                            else
                            {
                                p.SetValue(entity, Convert.ChangeType(amt, p.PropertyType), null);
                            }
                        }
                    }
                    entity.FinalDiffQty = entity.DiffQty - (entity.TransitQty + entity.ReturnedQty + entity.NonInStoreQty + entity.NonArrivalQty + entity.ExtraPresellQty + entity.ExtraSoldQty + entity.IntraPresellQty + entity.IntraSoldQty + entity.NonChargeOffQty + entity.NonRecordedQty + entity.GroupBuyingQty + entity.OtherQty);
                    entity.FinalDiffAMT = Math.Round((decimal)entity.FinalDiffQty * entity.Price, 2);
                //});
            //}
            return entity;
        }
    }
}
