using DAL;
using DBML;
using Factory;
using IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DepartmentBLL : IBLLBase
    {
        public List<Department> GetDepartment()
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<DepartmentDAL>().GetDepartment(dcc);
            }
        }

        public Department GetDepartment(Guid id)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                return DALFty.Create<DepartmentDAL>().GetDepartment(dcc, id);
            }
        }

        public void Insert(Department obj)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<DepartmentDAL>().Insert(dcc, obj);
                dcc.Save();
            }
        }

        public void Update(Department obj)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<DepartmentDAL>().Update(dcc, obj);
                dcc.Save();
            }
        }

        public void Delete(Guid id)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<DepartmentDAL>().Delete(dcc, id);
                dcc.Save();
            }
        }

        public void Import(List<Department> insertList, List<Department> updateList)
        {
            using (DCC dcc = DBMLFty.Dcc)
            {
                DALFty.Create<DepartmentDAL>().Import(dcc, insertList, updateList);
                dcc.Save();
            }
        }
    }
}
