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
using CommonLibrary;
using Utility;
using System.Collections;
using System.Reflection;
using static Utility.EnumHelper;
using Utility.Interceptor;

namespace USL
{
    public partial class DeptEditPage : DevExpress.XtraEditors.XtraUserControl, IDataEdit
    {
        Department dept = null;
        public DeptEditPage(Object obj)
        {
            InitializeComponent();
            if (obj == null)
            {
                departmentBindingSource.DataSource = new Department();
            }
            else
            {
                departmentBindingSource.DataSource = obj;
                dept = (Department)obj;
            }
            GetTypeList();
        }

        private void GetTypeList()
        {
            List<Department> depts = BLLFty.Create<BaseBLL>().GetListBy<Department>(null);
            //门店类型
            List<string> list = depts.Select(o => o.Type).Distinct().ToList();
            if (list != null)
            {
                cboType.Properties.Items.Clear();
                list.ForEach(p =>
                {
                    if (p != null)
                        cboType.Properties.Items.Add(p);
                });
            }
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
                Department obj = departmentBindingSource.DataSource as Department;
                //obj.Code = Rexlib.GetSpellCode(obj.Name);
                //添加
                if (dept == null)
                {
                    dept = obj;
                    dept.ID = Guid.NewGuid();
                    dept.AddTime = DateTime.Now;
                    List<Department> deptList = BLLFty.Create<BaseBLL>().GetListBy<Department>(null);
                    if (deptList.Exists(o => o.Name == dept.Code))
                    {
                        XtraMessageBox.Show("该门店已经存在，不能重复添加。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    BLLFty.Create<BaseBLL>().Add<Department>(dept);
                }
                else//修改
                    BLLFty.Create<BaseBLL>().Modify<Department>(obj);
                //CommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                //CommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
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
            dept = null;
            departmentBindingSource.DataSource = new Department();
            txtCode.Focus();
        }
    }
}
