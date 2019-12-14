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
using EDMX;
using Factory;
using BLL;
using System.Configuration;
using Utility.Interceptor;

namespace USL
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        private static ClientFactory clientFactory = LoggerInterceptor.CreateProxy<ClientFactory>();
        public LoginForm()
        {
            InitializeComponent();
            //List<Warehouse> list = BLLFty.Create<BaseBLL>().GetListByNoTracking<Warehouse>(null);
            //List<Warehouse> list2 = BLLFty.Create<BaseBLL>().GetListByNoTracking<Warehouse>(null);
            //Warehouse warehouse = new Warehouse();
            //warehouse.ID = Guid.NewGuid();
            //warehouse.Code = "aaa";
            //warehouse.Name = "bbb";
            //BLLFty.Create<BaseBLL>().Add<Warehouse>(warehouse);
            //List<Warehouse> list3 = BLLFty.Create<BaseBLL>().GetListByNoTracking<Warehouse>(null);
            //List<Warehouse> list4 = BLLFty.Create<BaseBLL>().GetListByNoTracking<Warehouse>(null);
            BindData();
        }

        public void BindData()
        {
            usersInfoBindingSource.DataSource = BLLFty.Create<BaseBLL>().GetListBy<UsersInfo>(o => o.IsDel == false && !string.IsNullOrEmpty(o.Password));
            deptBindingSource.DataSource = BLLFty.Create<BaseBLL>().GetListBy<Department>(o => o.IsDel == false);
            txtCode.EditValue = Utility.ConfigAppSettings.GetValue("User");
        }
        bool Invalid()
        {
            bool flag = false;
            dxErrorProvider1.ClearErrors();
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                dxErrorProvider1.SetError(txtCode, "用户不存在。");
                //XtraMessageBox.Show("用户不存在。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCode.Focus();
                txtCode.SelectAll();
                flag = true;
            }
            if (string.IsNullOrEmpty(lueDept.Text.Trim()))
            {
                dxErrorProvider1.SetError(lueDept, "门店不存在。");
                lueDept.Focus();
                lueDept.SelectAll();
                flag = true;
            }
            return flag;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Invalid())
                return;
            UsersInfo user = BLLFty.Create<BaseBLL>().GetListBy<UsersInfo>(o => o.Code.Equals(txtCode.Text.Trim())).FirstOrDefault();
            Guid deptID = Guid.Parse(lueDept.EditValue.ToString());
            Department dept = BLLFty.Create<BaseBLL>().GetListBy<Department>(o => o.ID.Equals(deptID)).FirstOrDefault();
            //string msg = string.Empty;
            if (user.Password != txtPassword.Text.Trim())
            {
                dxErrorProvider1.SetError(txtPassword, "密码不正确。");
                //XtraMessageBox.Show("密码不正确。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                txtPassword.SelectAll();
            }
            else
            {
                Utility.ConfigAppSettings.SetValue("User", user.Code);
                MainForm.usersInfo = user;
                MainForm.department = dept;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}