using BLL;
using Factory;
using IBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace USL
{
    public class ClientFactory
    {
        // 数据集
        //public static Dictionary<Type, object> dataSourceList = new Dictionary<Type, object>();
        // 界面列表（ItemDetailPage页面所有加载的Control）
        public static Dictionary<String, IItemDetail> itemDetailList = new Dictionary<string, IItemDetail>();

        #region 添加
        /// <summary>
        /// 添加单个实体
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        //public virtual int Add<T>(T model) where T : class, new()
        //{
        //    return BLLFty.Create<BaseBLL>().Add<T>(model);
        //}

        /// <summary>
        /// 海量数据插入方法
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">数据列表</param>
        //public virtual void AddByBulkCopy<T>(List<T> list) where T : class, new()
        //{
        //    BLLFty.Create<BaseBLL>().AddByBulkCopy<T>(list);
        //}
        #endregion

        #region 删除
        /// <summary>
        /// 删除(适用于先查询后删除的单个实体)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        //public virtual int Delete<T>(T model) where T : class, new()
        //{
        //    return BLLFty.Create<BaseBLL>().Delete<T>(model);
        //}

        /// <summary>
        /// 根据条件删除(支持批量删除)
        /// </summary>
        /// <param name="delWhere">传入Lambda表达式(生成表达式目录树)</param>
        /// <returns></returns>
        //public virtual int DeleteByBulk<T>(Expression<Func<T, bool>> delWhere) where T : class, new()
        //{
        //    return BLLFty.Create<BaseBLL>().DeleteByBulk<T>(delWhere);
        //}
        #endregion

        #region 修改
        /// <summary>
        /// 修改单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        //public virtual int Update<T>(T model) where T : class, new()
        //{
        //    return BLLFty.Create<BaseBLL>().Modify<T>(model);
        //}

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">数据列表</param>
        /// <returns></returns>
        //public virtual void UpdateByBulk<T>(List<T> list) where T : class, new()
        //{
        //    BLLFty.Create<BaseBLL>().UpdateByBulk<T>(list);
        //}
        #endregion

        #region 查询

        /// <summary>
        /// 按实体类型查询实体列表数据（返回List不需要修改或删除）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        //public virtual List<T> GetListByNoTracking<T>(Expression<Func<T, bool>> whereLambda) where T : class, new()
        //{
        //    return BLLFty.Create<BaseBLL>().GetListByNoTracking<T>(whereLambda);
        //}

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="whereLambda">查询条件(lambda表达式的形式生成表达式目录树)</param>
        /// <returns></returns>
        //public virtual List<T> GetListBy<T>(Expression<Func<T, bool>> whereLambda) where T : class, new()
        //{
        //    return BLLFty.Create<BaseBLL>().GetListBy<T>(whereLambda);
        //}

        /// <summary>
        /// 贪婪加载
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="path">贪婪加载对象</param>
        /// <returns></returns>
        //public virtual List<T> GetListByInclude<T>(string path) where T : class, new()
        //{
        //    return BLLFty.Create<BaseBLL>().GetListByInclude<T>(path);
        //}

        /// <summary>
        /// 通过反射取得类型
        /// </summary>
        /// <param name="entityName">实体名称</param>
        /// <returns></returns>
        public virtual Type Reflect(string entityName)
        {
            string assemblyString =  nameof(EDMX);
            Assembly assembly = Assembly.Load(assemblyString);
            return assembly.GetType(string.Format("{0}.{1}", assemblyString, entityName));
        }

        /// <summary>
        /// 从缓存获取数据，缓存没有再从数据库取
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        //public virtual List<T> GetData<T>() where T : class, new()
        //{
        //    return BLLFty.Create<BaseBLL>().GetListBy<T>(null);
        //    //List<T> list = new List<T>();
        //    //if (dataSourceList.ContainsKey(typeof(T)))
        //    //{
        //    //    list = (List<T>)dataSourceList[typeof(T)];
        //    //}
        //    //else
        //    //{
        //    //    list = BLLFty.Create<BaseBLL>().GetListBy<T>(null);
        //    //    dataSourceList.Add(typeof(T), list);
        //    //}
        //    //return list;
        //}

        /// <summary>
        /// 从缓存获取数据，缓存没有再从数据库取（返回List不需要修改或删除）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        //public virtual List<T> GetDataByNoTracking<T>() where T : class, new()
        //{
        //    return BLLFty.Create<BaseBLL>().GetListByNoTracking<T>(null);
        //    //List<T> list = new List<T>();
        //    //if (dataSourceList.ContainsKey(typeof(T)))
        //    //{
        //    //    list = (List<T>)dataSourceList[typeof(T)];
        //    //}
        //    //else
        //    //{
        //    //    list = BLLFty.Create<BaseBLL>().GetListByNoTracking<T>(null);
        //    //    dataSourceList.Add(typeof(T), list);
        //    //}
        //    //return list;
        //}

        /// <summary>
        /// 刷新所有界面（直接从数据库取数）
        /// </summary>
        /// <param name="entityName">实体类型名称</param>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        public virtual IList DataPageRefresh(String entityName, String filter)
        {
            IList list = null;
            Type type = Reflect(entityName);
            if (type != null)
            {
                list = BLLFty.Create<BaseBLL>().ExecuteQuery(type, filter);
                //if (dataSourceList.ContainsKey(type))
                //{
                //    dataSourceList[type] = list;
                //}
                //else
                //{
                //    dataSourceList.Add(type, list);
                //}
                foreach (KeyValuePair<String, IItemDetail> kvp in itemDetailList)
                {
                    kvp.Value.BindData(list);
                }
            }
            return list;
        }


        /// <summary>
        /// 刷新所有界面（直接从数据库取数）
        /// </summary>
        /// <param name="entityName">实体类型名称</param>
        /// <returns></returns>
        public virtual IList DataPageRefresh(String entityName)
        {
            return DataPageRefresh(entityName, string.Empty);
        }

        /// <summary>
        /// 刷新所有界面
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public virtual List<T> DataPageRefresh<T>() where T : class, new()
        {
            List<T> list = BLLFty.Create<BaseBLL>().GetListBy<T>(null);
            foreach (KeyValuePair<String, IItemDetail> kvp in itemDetailList)
            {
                kvp.Value.BindData(list);
            }
            return list;
        }
        #endregion
    }
}
