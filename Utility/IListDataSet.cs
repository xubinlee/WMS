﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using static Utility.EnumHelper;

namespace Utility
{
    public class IListDataSet
    {
        /// <summary> 
        /// 集合装换DataSet 
        /// </summary> 
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataSet ToDataSet(IList p_List)
        {
            DataSet result = new DataSet();
            DataTable _DataTable = new DataTable();
            if (p_List.Count > 0)
            {
                PropertyInfo[] propertys = p_List[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < p_List.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(p_List[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    _DataTable.LoadDataRow(array, true);
                }
            }
            result.Tables.Add(_DataTable);
            return result;
        }

        /// <summary>
        /// 泛型集合转换DataSet
        /// </summary>   
        /// <typeparam name="T"></typeparam>  
        /// <param name="list">泛型集合</param>  
        /// <returns></returns>   
        public static DataSet ToDataSet<T>(IList<T> list)
        {
            return ToDataSet<T>(list, null);
        }

        /// <summary> 
        /// 泛型集合转换DataSet 
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="p_List">泛型集合</param>
        /// <param name="p_PropertyName">待转换属性名数组</param>     
        /// <returns></returns>     
        public static DataSet ToDataSet<T>(IList<T> p_List, params string[] p_PropertyName)
        {
            List<string> propertyNameList = new List<string>();
            if (p_PropertyName != null)
                propertyNameList.AddRange(p_PropertyName);
            DataSet result = new DataSet();
            DataTable _DataTable = new DataTable();
            if (p_List.Count > 0)
            {
                PropertyInfo[] propertys = p_List[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        // 没有指定属性的情况下全部属性都要转换  
                        if (pi.PropertyType.Name.Equals("Nullable`1"))
                            _DataTable.Columns.Add(pi.Name);
                        else
                            _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                for (int i = 0; i < p_List.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(p_List[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(p_List[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    _DataTable.LoadDataRow(array, true);
                }
            }
            result.Tables.Add(_DataTable);
            return result;
        }

        /// <summary>
        /// DataSet装换为泛型集合     
        /// </summary>     
        /// <typeparam name="T"></typeparam>    
        /// <param name="p_DataSet">DataSet</param>    
        /// <param name="p_TableIndex">待转换数据表索引</param>    
        /// <returns></returns>       
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, int p_TableIndex)
        {
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (p_TableIndex > p_DataSet.Tables.Count - 1)
                return null;
            if (p_TableIndex < 0)
                p_TableIndex = 0;
            DataTable p_Data = p_DataSet.Tables[p_TableIndex];
            // 返回值初始化       
            IList<T> result = new List<T>();
            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < p_Data.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值       
                        if (pi.Name.Equals(p_Data.Columns[i].ColumnName))
                        {
                            // 数据库NULL值单独处理       
                            if (p_Data.Rows[j][i] != DBNull.Value)
                                pi.SetValue(_t, p_Data.Rows[j][i], null);
                            else
                                pi.SetValue(_t, null, null);
                            break;
                        }
                    }
                }
                result.Add(_t);
            }
            return result;
        }

        /// <summary> 
        /// DataSet装换为泛型集合 
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="p_DataSet">DataSet</param>     
        /// <param name="p_TableName">待转换数据表名称</param>     
        /// <returns></returns>      
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, string p_TableName)
        {
            int _TableIndex = 0;
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (string.IsNullOrEmpty(p_TableName))
                return null;
            for (int i = 0; i < p_DataSet.Tables.Count; i++)
            {
                // 获取Table名称在Tables集合中的索引值       
                if (p_DataSet.Tables[i].TableName.Equals(p_TableName))
                {
                    _TableIndex = i;
                    break;
                }
            }
            return DataSetToIList<T>(p_DataSet, _TableIndex);
        }

        /// <summary>
        /// DataRow装换为泛型实体     
        /// </summary>     
        /// <typeparam name="T">实体类型</typeparam>    
        /// <param name="dt">DataTable</param>   
        /// <param name="row">数据行</param>    
        /// <returns>T</returns>       
        public static T DataRowToModel<T>(DataTable dt, DataRow row, T model)
        {
            if (dt == null)
                return model;
            foreach (PropertyInfo p in model.GetType().GetProperties())
            {
                string enumTypeName = model.GetType().Name + "Enum";
                ListItem item = EnumHelper.GetEnumValues(enumTypeName, false).FirstOrDefault(o => o.Value.ToString().Equals(p.Name));
                if (item != null)
                {
                    if (dt.Columns.Contains(item.Name))
                    {
                        if (p.PropertyType.IsGenericType)
                        {
                            // 泛型Nullable<>
                            Type genericTypeDefinition = p.PropertyType.GetGenericTypeDefinition();
                            if (genericTypeDefinition == typeof(Nullable<>))
                                p.SetValue(model, string.IsNullOrWhiteSpace(row[item.Name].ToString()) ? null : Convert.ChangeType(row[item.Name], Nullable.GetUnderlyingType(p.PropertyType)), null);
                        }
                        else
                        {
                            // 非泛型
                            p.SetValue(model, string.IsNullOrWhiteSpace(row[item.Name].ToString()) ? null : Convert.ChangeType(row[item.Name], p.PropertyType), null);
                        }
                    }
                }
            }
            return model;
        }
    }
}
