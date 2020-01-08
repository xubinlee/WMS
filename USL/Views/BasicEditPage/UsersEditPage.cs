using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using IBase;
using EDMX;
using Factory;
using BLL;
using Utility;
using System.Data.Linq;
using CommonLibrary;
using System.Collections;
using Utility.Interceptor;
using MainMenu = EDMX.MainMenu;
using System.Linq.Expressions;

namespace USL
{
    public partial class UsersEditPage : DevExpress.XtraEditors.XtraUserControl, IDataEdit
    {
        UsersInfo user = null;
        List<TypesList> types;   //类型列表
        public UsersEditPage(Object obj)
        {
            InitializeComponent();
            BindData();
            if (MainForm.usersInfo.AttPrivilege == 1)
            {
                lciPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciWageType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciWages.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciSchClassWage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciTimeWage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lciPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciWageType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciWages.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciSchClassWage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciTimeWage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            types = BLLFty.Create<BaseBLL>().GetListByNoTracking<TypesList>(null);
            //typesListBindingSource.DataSource = types.FindAll(o => o.Type == TypesListConstants.PrivilegeType);
            //wageTypeBindingSource.DataSource = types.FindAll(o => o.Type == TypesListConstants.WageType);
            if (obj == null)
            {
                user = new UsersInfo();
                user.AttEnabled = true;
                usersInfoBindingSource.DataSource = user;
            }
            else
            {
                usersInfoBindingSource.DataSource = user = (UsersInfo)obj;
                //user = (UsersInfo)obj;
            }
        }

        public void BindData()
        {
            departmentBindingSource.DataSource = BLLFty.Create<BaseBLL>().GetListBy<Department>(null);
        }

        public void Add()
        {
            Clear();
        }

        public bool Save()
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                UsersInfo obj = usersInfoBindingSource.DataSource as UsersInfo;
                if (pePhoto.EditValue == System.DBNull.Value)
                    obj.Photo = null;
                else
                {
                    if (pePhoto.EditValue is byte[])
                        obj.Photo = (byte[])pePhoto.EditValue;
                    else
                        obj.Photo = ImageHelper.MakeBuff((Image)pePhoto.EditValue);
                }
                Expression<Func<UsersInfo, bool>> whereLambda = o => o.ID != obj.ID && o.AttCardnumber.Equals(obj.AttCardnumber) && o.IsDel == false;
                List<UsersInfo> users = BLLFty.Create<BaseBLL>().GetListByNoTracking<UsersInfo>(whereLambda);
                if (users.Count > 0 && user.ID != obj.ID && !string.IsNullOrEmpty(obj.AttCardnumber) && user.AttCardnumber == obj.AttCardnumber)
                {
                    XtraMessageBox.Show(string.Format("考勤卡号：{0}已经存在，不允许添加重复考勤卡号。", obj.AttCardnumber), "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //添加
                //if (user == null)
                if (user.ID == null || user.ID == Guid.Empty)
                {
                    //user = obj;
                    user.ID = Guid.NewGuid();

                    //添加功能权限信息
                    List<Permission> pList = new List<Permission>();
                    List<ButtonPermission> btnList = new List<ButtonPermission>();
                    foreach (MainMenu menu in MainForm.AllMainMenuList)
                    {
                        Permission p = new Permission();
                        p.ID = menu.SerialNo;
                        string no = menu.SerialNo.ToString().Trim();
                        if (menu.ParentID == null)
                            p.ParentID = 0;
                        else if (no.Length > 2)
                            p.ParentID = int.Parse(no.Substring(0, no.Length - 2));
                        p.SerialNo = menu.SerialNo;
                        p.UserID = user.ID;
                        p.Caption = menu.Caption;
                        //p.CheckBoxState = false;
                        // 由于不开放权限设置功能，暂时新增用户时分配所有权限
                        p.CheckBoxState = true;
                        pList.Add(p);
                    }
                    //添加按钮权限信息
                    foreach (ButtonType btn in Enum.GetValues(typeof(ButtonType)))
                    {
                        ButtonPermission btnP = new ButtonPermission();
                        btnP.UserID = user.ID;
                        btnP.Name = btn.ToString();
                        btnP.Caption = EnumHelper.GetDescription<ButtonType>(btn, false);
                        //btnP.CheckBoxState = false;
                        // 由于不开放权限设置功能，暂时新增用户时分配所有权限
                        btnP.CheckBoxState = true;
                        btnList.Add(btnP);
                    }
                    BLLFty.Create<UsersInfoBLL>().Insert(user, pList, btnList);
                }
                else
                    BLLFty.Create<BaseBLL>().Modify<UsersInfo>(obj);
                //CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                //if (ex.HResult == -2146232060)  //违反了PRIMARY KEY约束
                //    XtraMessageBox.Show(string.Format("账号:{0}已经存在,请重新设置账号。", user.Code), "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //else
                    XtraMessageBox.Show(ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        public void Clear()
        {
            //user = null;
            user = new UsersInfo();
            user.AttEnabled = true;
            usersInfoBindingSource.DataSource = user;
            txtName.Focus();
        }

        private void pePhoto_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                if (pePhoto.EditValue != null && pePhoto.Image != null)
                {
                    ImageHelper.WindowsPhotoViewer(pePhoto.Image);
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
    }
}
