using DBML;
using IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAL
{
    public class DepartmentDAL : IDALBase
    {
        public List<Department> GetDepartment(DCC dcc)
        {
            return dcc.Department.OrderBy(o => o.Code).ToList();
        }

        public Department GetDepartment(DCC dcc, Guid id)
        {
            return dcc.Department.FirstOrDefault(o => o.ID == id);
        }

        public void Insert(DCC dcc, Department obj)
        {
            dcc.Department.InsertOnSubmit(obj);
        }

        public void Update(DCC dcc, Department obj)
        {
            obj.UpdateTime = DateTime.Now;
            dcc.Department.Attach(obj);
            dcc.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, obj);
        }

        public void Delete(DCC dcc, Guid id)
        {
            var lst = dcc.Department.Where(o => o.ID == id);
            dcc.Department.DeleteAllOnSubmit(lst);
        }

        public void Import(DCC dcc, List<Department> insertList, List<Department> updateList)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (insertList.Count > 0)
                    dcc.Department.InsertAllOnSubmit(insertList);
                if (updateList.Count > 0)
                {
                    dcc.Department.AttachAll(updateList);
                    dcc.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, updateList);
                }
                dcc.SubmitChanges();
                ts.Complete();
            }
        }
    }
}
