using EDMX;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Z.EntityFramework.Plus;

namespace Factory
{
    public class EDMXFty
    {
        public static WmsContext Dc
        {
            get
            {
                // 解决【对象名 'dbo.EdmMetadata和dbo .__MigrationHistory' 无效。】的问题
                // 由于数据库已经存在，不会有dbo.EdmMetadata和dbo .__MigrationHistory表
                Database.SetInitializer<WmsContext>(null);
                // 注册侦听器
                //1.记录执行非异步命令时的警告信息
                //2.记录执行任何命令引发的异常信息
                DbInterception.Add(new NLogCommandInterceptor());
                ////创建全局过滤
                QueryFilterManager.Filter<Goods>(x => x.Where(c => c.Type == 0));
                WmsContext db = new WmsContext();// new DbContext(ConfigInfo.SqlConStr);
                //// 让过滤生效
                QueryFilterManager.InitilizeGlobalFilter(db);
                return db;
            }
        }
    }
}
