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

        public SqlDataReader PageSearch(string sql, params object[] parameters)
        {
            SqlConnection connection = (SqlConnection)this.Database.Connection;
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddRange(parameters.ToArray());
            SqlDataReader result = command.ExecuteReader();
            return result;
        }

        public DataSet ExecuteSQl(string sql, params object[] parameters)
        {
            SqlConnection connection = (SqlConnection)this.Database.Connection;
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddRange(parameters.ToArray());
            var adaptor = new SqlDataAdapter(command);
            var ds = new DataSet();
            adaptor.Fill(ds);
            return ds;
        }
    }
}
