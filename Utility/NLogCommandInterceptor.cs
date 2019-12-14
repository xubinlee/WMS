﻿using NLog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class NLogCommandInterceptor : IDbCommandInterceptor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public void NonQueryExecuting(
        DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogIfNonAsync(command, interceptionContext);
        }
        public void NonQueryExecuted(
        DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }
        public void ReaderExecuting(
        DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogIfNonAsync(command, interceptionContext);
        }
        public void ReaderExecuted(
        DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }
        public void ScalarExecuting(
        DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogIfNonAsync(command, interceptionContext);
        }
        public void ScalarExecuted(
        DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }
        private void LogIfNonAsync<TResult>(
        DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            if (!interceptionContext.IsAsync)
            {
                Logger.Warn("\r\nNon-async command used: {0}", command.CommandText);
            }
        }
        private void LogIfError<TResult>(
        DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            if (interceptionContext.Exception != null)
            {
                string split = "\r\n------------" + DateTime.Now.ToString() + "------------\r\n";
                Logger.Error("\r\nCommand： {0} \r\nfailed with exception： {1}{2}",
                command.CommandText, interceptionContext.Exception, split);
            }
        }
    }
}
