﻿using Castle.DynamicProxy;
using DevExpress.XtraEditors;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility.Interceptor
{
    // Castle只会对virtual标记的虚方法进行代理
    public class LoggerInterceptor: StandardInterceptor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 创建代理类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateProxy<T>() where T : class
        {
            ProxyGenerator generator = new ProxyGenerator();
            var proxy = generator.CreateClassProxy<T>(new LoggerInterceptor());
            return proxy;
        }

        protected override void PerformProceed(IInvocation invocation)
        {
            try
            {
                base.PerformProceed(invocation);
            }
            catch (Exception ex)
            {
                // 方法执行中抛异常时记录日志
                string split = "\r\n------------" + DateTime.Now.ToString() + "------------\r\n";
                string exception = string.Format("\r\nInnerException：{0}\r\nException：{1}\r\nStackTrace：{2}{3}", ex.InnerException, ex.Message, ex.StackTrace, split);
                Logger.Error(exception);
                XtraMessageBox.Show(ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
