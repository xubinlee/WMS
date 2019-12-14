using DAL;
using EDMX;
using Factory;
using IBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BaseBLL : IBLLBase
    {
        #region 添加

        /// <summary>
        /// 添加单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public virtual int Add<T>(T model) where T : class, new()
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                return DALFty.Create<BaseDAL>().Add<T>(db, model);
            }
        }

        /// <summary>
        /// 海量数据插入方法
        /// </summary>
        /// <typeparam name="db"></typeparam>
        /// <param name="list"></param>
        public virtual void AddByBulkCopy<T>(List<T> list) where T : class, new()
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                DALFty.Create<BaseDAL>().AddByBulkCopy(db, list);
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除(适用于先查询后删除的单个实体)
        /// </summary>
        /// <param name="model">需要删除的实体</param>
        /// <returns></returns>
        public virtual int Delete<T>(T model) where T : class, new()
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                return DALFty.Create<BaseDAL>().Del<T>(db, model);
            }
        }
        /// <summary>
        /// 根据条件删除(支持批量删除)
        /// </summary>
        /// <param name="delWhere">传入Lambda表达式(生成表达式目录树)</param>
        /// <returns></returns>
        public virtual int DeleteByBulk<T>(Expression<Func<T, bool>> delWhere) where T : class, new()
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                return DALFty.Create<EfPlusDAL>().DeleteByBulk<T>(db, delWhere);
            }
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">修改后的实体</param>
        /// <returns></returns>
        public virtual int Modify<T>(T model) where T : class, new()
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                return DALFty.Create<BaseDAL>().Modify<T>(db, model);
            }
        }

        /// <summary>
        /// 海量数据修改方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public virtual void UpdateByBulk<T>(List<T> list) where T : class, new()
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                DALFty.Create<EfPlusDAL>().UpdateByBulk(db, list);
            }
        }
        #endregion

        #region 查询

        /// <summary>
        /// 执行增加,删除,修改操作(或调用存储过程)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public virtual int ExecuteSql(string sql, params SqlParameter[] pars)
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                return DALFty.Create<BaseDAL>().ExecuteSql(db, sql, pars);
            }
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <typeparam name="db"></typeparam>
        /// <param name="type">实体类型名称</param>
        /// <param name="filter">where条件</param>
        /// <returns></returns>
        public virtual IList ExecuteQuery(Type type, string filter)
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                return DALFty.Create<BaseDAL>().ExecuteQuery(db, type, filter);
            }
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public virtual List<T> ExecuteQuery<T>(string sql, params SqlParameter[] pars)
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                return DALFty.Create<BaseDAL>().ExecuteQuery<T>(db, sql, pars);
            }
        }

        /// <summary>
        /// 根据条件查询（返回List不需要修改或删除）
        /// </summary>
        /// <param name="whereLambda">查询条件(lambda表达式的形式生成表达式目录树)</param>
        /// <returns></returns>
        public virtual List<T> GetListByNoTracking<T>(Expression<Func<T, bool>> whereLambda) where T : class, new()
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                return DALFty.Create<BaseDAL>().GetListByNoTracking<T>(db, whereLambda);
            }
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="whereLambda">查询条件(lambda表达式的形式生成表达式目录树)</param>
        /// <returns></returns>
        public virtual List<T> GetListBy<T>(Expression<Func<T, bool>> whereLambda) where T : class, new()
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                return DALFty.Create<BaseDAL>().GetListBy<T>(db, whereLambda);
            }
        }

        /// <summary>
        /// 贪婪加载查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="path">贪婪加载实体</param>
        /// <returns></returns>
        public virtual List<T> GetListByInclude<T>(string path) where T : class, new()
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                return DALFty.Create<BaseDAL>().GetListByInclude<T>(db, path);
            }
        }
        #endregion

        #region 复合操作
        /// <summary>
        /// 先删除再添加(批量操作)
        /// </summary>
        /// <param name="delWhere">传入Lambda表达式(生成表达式目录树)</param>
        /// <param name="insertList">插入列表</param>
        /// <returns></returns>
        public virtual void DeleteAndAdd<T>(Expression<Func<T, bool>> delWhere, List<T> insertList) where T : class, new()
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                DALFty.Create<BaseDAL>().DeleteAndAdd<T>(db, delWhere, insertList);
            }
        }

        /// <summary>
        /// 海量数据插入和更新方法
        /// </summary>
        /// <param name="insertList">插入的实体列表</param>
        /// <param name="updateList">更新实体列表</param>
        /// <returns></returns>
        public virtual void AddAndUpdate<T>(List<T> insertList, List<T> updateList) where T : class, new()
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                DALFty.Create<BaseDAL>().AddAndUpdate<T>(db, insertList, updateList);
            }
        }

        /// <summary>
        /// 添加或更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int AddOrUpdate(SystemStatus entity)
        {
            using (WmsContext db = EDMXFty.Dc)
            {
                return DALFty.Create<BaseDAL>().AddOrUpdate(db, entity);
            }
        }
        #endregion
    }
}
