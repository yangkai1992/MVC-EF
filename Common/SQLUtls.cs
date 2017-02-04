using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SQLUtls
    {
        /// <summary>
        /// 分页搜索SQL
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="table"></param>
        /// <param name="wheres"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static string GeneratePageSearchSQL(int pageIndex, int pageSize, string table, IEnumerable<string> wheres, string orderBy)
        {
            string sql = @"select top {0} * from 
                        (select row_number()over(order by id)rownumber,* from {1}
                            {2}
                        )a
                        where rownumber>{4}
                        {3}
                        select count(*) from {1} {2}";
            string where = " where 1 = 1";
            if (wheres != null && wheres.Count() > 0)
            {
                where += " and " + string.Join(" and ", wheres);
            }

            sql = string.Format(sql, pageSize, table, where, orderBy, (pageIndex - 1) * pageSize);
            return sql;
        }
    }
}
