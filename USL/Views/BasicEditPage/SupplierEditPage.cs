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

namespace USL
{
    public partial class SupplierEditPage : DevExpress.XtraEditors.XtraUserControl, IDataEdit
    {
        Supplier supplier = null;
        List<TypesList> types;   //类型列表
        public SupplierEditPage(Object obj)
        {
            InitializeComponent();
            types = MainForm.TypesList;
            //单据类型
            if (MainForm.ISnowSoftVersion == ISnowSoftVersion.Procurement || MainForm.ISnowSoftVersion == ISnowSoftVersion.PurchaseSellStock)
                typesListBindingSource.DataSource = types.FindAll(o => o.Type == TypesListConstants.SupplierType && o.No == 0);
            else
                typesListBindingSource.DataSource = types.FindAll(o => o.Type == TypesListConstants.SupplierType);
            if (obj == null)
            {
                supplierBindingSource.DataSource = new Supplier();
            }
            else
            {
                supplierBindingSource.DataSource = obj;
                supplier = (Supplier)obj;
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
                Supplier obj = supplierBindingSource.DataSource as Supplier;
                obj.Code = Rexlib.GetSpellCode(obj.Name);
                //添加
                if (supplier == null)
                {
                    supplier = obj;
                    supplier.ID = Guid.NewGuid();
                    supplier.AddTime = DateTime.Now;
                    bool exists = BLLFty.Create<BaseBLL>().GetListByNoTracking<Supplier>(o => o.ID != obj.ID && o.Name.Equals(supplier.Name)).Any();
                    if (exists)
                    {
                        XtraMessageBox.Show("该供应商已经存在，不能重复添加。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    BLLFty.Create<BaseBLL>().Add<Supplier>(supplier);
                }
                else//修改
                    BLLFty.Create<BaseBLL>().Modify<Supplier>(obj);
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
            supplier = null;
            supplierBindingSource.DataSource = new Supplier();
            txtCode.Focus();
        }
    }
}
