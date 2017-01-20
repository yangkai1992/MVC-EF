using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Repository
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IList<TEntity> ExecuteStoredProcedure<TEntity>(string commandText, params object[] parameters) where TEntity : new();

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<T> PageSearch<T>(string sql, out int totalCount, params object[] parameters) where T : new();
        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<T> ExecuteSQL<T>(string sql, params object[] parameters) where T : new();
    }
}
