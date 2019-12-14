using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Navigation;
using Utility;
using IBase;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Docking2010.Views;
using System.Collections;
using Factory;
using BLL;
using EDMX;
using DevExpress.XtraGrid.Columns;
using CommonLibrary;
using Utility.Interceptor;
using MainMenu = EDMX.MainMenu;

namespace USL
{
    public partial class DataEditForm : DevExpress.XtraEditors.XtraForm
    {
        private static ClientFactory clientFactory = LoggerInterceptor.CreateProxy<ClientFactory>();
        Dictionary<MainMenu, Object> editPage;
        MainMenu mainMenu;
        PageGroup pageGroupCore;
        IDataEdit dataEditPage;

        public DataEditForm(MainMenu menu, Object obj, PageGroup child)
        {
            InitializeComponent();
            editPage = new Dictionary<EDMX.MainMenu, object>();
            mainMenu = menu;
            pageGroupCore = child;
            this.Text = mainMenu.Caption;
            if (obj == null)
                this.Text += "—添加";
            CreateToolsButton();
            dataEditPage = CreateEditPage(obj);
            //if (obj != null && obj is VSupplier)
            //{
            //    dpMoldAllot.Enabled = true;
            //    vGoodsByMoldAllotBindingSource.DataSource = clientFactory.GetData<VGoodsByMoldAllot>().FindAll(o => o.SupplierID == ((VSupplier)obj).ID);
            //    gridView.BestFitColumns();
            //    foreach (GridColumn col in gridView.Columns)
            //    {
            //        if (col.Name.ToUpper().Contains("ID".ToUpper()))
            //            col.Visible = false;
            //        if (col.ColumnType.Equals(typeof(System.Data.Linq.Binary)))
            //        {
            //            col.Width = 50;  //调整图片的列宽度
            //        }
            //        if (col.FieldName == "单价" || col.FieldName == "售价")
            //        {
            //            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //            col.DisplayFormat.FormatString = "c5";
            //        }
            //        if (col.FieldName.Contains("金额") || col.FieldName == "额外费用")
            //        {
            //            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //            col.DisplayFormat.FormatString = "c";
            //        }
            //        if (col.FieldName == "去税单价")
            //        {
            //            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //            col.DisplayFormat.FormatString = "c6";
            //        }
            //    }
            //}
            //else
                dpMoldAllot.Enabled = false;
        }

        IDataEdit CreateEditPage(Object obj)
        {
            IDataEdit de = null;
            switch (mainMenu.Name)
            {

                case MainMenuConstants.Department:
                    de = new DeptEditPage(obj);
                    ((DeptEditPage)de).Dock = DockStyle.Fill;
                    panelControl.Controls.Add((DeptEditPage)de);
                    break;
                //case MainMenuConstants.Company:
                //    if (obj != null)
                //        obj = clientFactory.GetData<Company>().FirstOrDefault(o => o.ID == ((VCompany)obj).ID);
                //    de = new CompanyEditPage(obj);
                //    ((CompanyEditPage)de).Dock = DockStyle.Fill;
                //    panelControl.Controls.Add((CompanyEditPage)de);
                //    break;
                //case MainMenuConstants.Supplier:
                //    if (obj != null)
                //        obj = clientFactory.GetData<Supplier>().FirstOrDefault(o => o.ID == ((VSupplier)obj).ID);
                //    de = new SupplierEditPage(obj);
                //    ((SupplierEditPage)de).Dock = DockStyle.Fill;
                //    panelControl.Controls.Add((SupplierEditPage)de);
                //    break;
                case MainMenuConstants.UsersInfo:
                    de = new UsersEditPage(obj);
                    ((UsersEditPage)de).Dock = DockStyle.Fill;
                    panelControl.Controls.Add((UsersEditPage)de);
                    break;
                case MainMenuConstants.Goods:
                case MainMenuConstants.Material:
                    de = new GoodsEditPage(mainMenu, obj);
                    ((GoodsEditPage)de).Dock = DockStyle.Fill;
                    panelControl.Controls.Add((GoodsEditPage)de);
                    break;
                //case MainMenuConstants.GoodsType:
                //    if (obj != null)
                //        obj = clientFactory.GetData<GoodsType>().FirstOrDefault(o => o.ID == ((VGoodsType)obj).ID);
                //    de = new GoodsTypeEditPage(obj);
                //    ((GoodsTypeEditPage)de).Dock = DockStyle.Fill;
                //    panelControl.Controls.Add((GoodsTypeEditPage)de);
                //    break;
                //case MainMenuConstants.Packaging:
                //    if (obj != null)
                //        obj = clientFactory.GetData<Packaging>().FirstOrDefault(o => o.ID == ((VPackaging)obj).ID);
                //    de = new PackagingEditPage(obj);
                //    ((PackagingEditPage)de).Dock = DockStyle.Fill;
                //    panelControl.Controls.Add((PackagingEditPage)de);
                //    break;
            }
            return de;
        }

        void CreateToolsButton()
        {
            //添加
            NavButton btnAdd = new NavButton();
            btnAdd.Caption = "添加";
            btnAdd.Glyph = global::USL.Properties.Resources.add;
            btnAdd.Name = "btnAdd";
            btnAdd.ElementClick += btnAdd_ElementClick;
            tileNavPane.Buttons.Add(btnAdd);
            //保存
            NavButton btnSave = new NavButton();
            btnSave.Caption = "保存";
            btnSave.Glyph = global::USL.Properties.Resources.save;
            btnSave.Name = "btnSave";
            btnSave.ElementClick += btnSave_ElementClick;
            tileNavPane.Buttons.Add(btnSave);
            //清空
            NavButton btnClear = new NavButton();
            btnClear.Caption = "清空";
            btnClear.Glyph = global::USL.Properties.Resources.clear;
            btnClear.Name = "btnClear";
            btnClear.ElementClick += btnClear_ElementClick;
            tileNavPane.Buttons.Add(btnClear);
        }

        void btnAdd_ElementClick(object sender, NavElementEventArgs e)
        {
            if (dataEditPage != null)
            {
                dataEditPage.Add();
                this.Text += "—添加";
            }
        }

        void btnSave_ElementClick(object sender, NavElementEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                if (dataEditPage != null)
                {
                    this.Text = mainMenu.Caption;
                    if (dataEditPage.Save())
                    {
                        //switch (mainMenu.Name)
                        //{
                        //    case MainMenuConstants.Department:
                        //        MainForm.DataPageRefresh<Department>();
                        //        break;
                        //    case MainMenuConstants.UsersInfo:
                        //        MainForm.DataPageRefresh<UsersInfo>();
                        //        break;
                        //    case MainMenuConstants.Goods:
                        //        MainForm.DataPageRefresh<Goods>();
                        //        break;
                        //    default:
                        //        break;
                        //}
                        clientFactory.DataPageRefresh(mainMenu.Name, string.Empty);
                        CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "保存成功");
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

        void btnClear_ElementClick(object sender, NavElementEventArgs e)
        {
            if (dataEditPage != null)
            {
                dataEditPage.Clear();
                this.Text += "—添加";
            }
        }
    }
}