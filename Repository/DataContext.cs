using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.Entity.ModelConfiguration;
using Model;
using System.Data.Common;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Repository
{
    public sealed class DataContext:DbContext,IDbContext
    {

        public DataContext(string connectionString)
            : base(connectionString)
        { 
        
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //注入模型
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public IDbSet<User> UserList { get; set; }


        public IList<TEntity> ExecuteStoredProcedure<TEntity>(string commandText, params object[] parameters) where TEntity : new()
        {
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    DbParameter p = parameters[i] as DbParameter;
                    if (p == null)
                    {
                        throw new Exception("不支持的参数类型");
                    }

                    commandText += i == 0 ? " " : ",";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        commandText += " output";
                    }
                }
            }

            var result = this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();

            return result;
        }

        public List<T> PageSearch<T>(string sql,out int totalCount ,params object[] parameters) where T:new()
        {
            SqlConnection connection = (SqlConnection)this.Database.Connection;
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddRange(parameters.ToArray());
            connection.Open();
            SqlDataReader result = command.ExecuteReader();

            List<T> resultList = new List<T>();
            totalCount = 0;

            if (result != null)
            {
                while (result.Read())
                {
                    T current = new T();
                    foreach (var pi in typeof(T).GetProperties())
                    {
                        var fieldName = pi.Name;
                        if (!pi.CanWrite)
                        {
                            continue;
                        }

                        object value = result[fieldName];
                        if (value != DBNull.Value)
                        {
                            pi.SetValue(current, value);
                        }
                    }
                    resultList.Add(current);
                }

                result.NextResult();
                result.Read();
                totalCount = result.GetInt32(0);
            }
            connection.Close();

            return resultList;
        }

        public List<T> ExecuteSQL<T>(string sql, params object[] parameters) where T : new()
        {
            SqlConnection connection = (SqlConnection)this.Database.Connection;
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddRange(parameters.ToArray());
            var adaptor = new SqlDataAdapter(command);
            var ds = new DataSet();
            adaptor.Fill(ds);

            if (ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable table = ds.Tables[0];
            List<T> resultList = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T current = new T();
                foreach (var pi in typeof(T).GetProperties())
                {
                    string fieldName = pi.Name;
                    if (!row.Table.Columns.Contains(fieldName))
                    {
                        continue;
                    }

                    if (!pi.CanWrite)
                    {
                        continue;
                    }

                    object value = row[fieldName];
                    if (value != DBNull.Value)
                    {
                        pi.SetValue(current, value);
                    }
                }
                resultList.Add(current);
            }

            return resultList;
        }
    }
}
