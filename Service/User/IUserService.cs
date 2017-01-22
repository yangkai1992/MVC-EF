using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        User GetUser(Guid id);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        void CreateUser(User user);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        void DeleteUser(User user);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        void UpdateUser(User user);

        User Find(string account);

        void CreateLoginHistory(LoginHistory loginHistory);

        void UpdateLoginHistory(LoginHistory loginHistory);

        List<User> PageSearch(int pageIndex, int pageSize, out int totalCount, IEnumerable<string> wheres = null, string orderBy = "", params SqlParameter[] sqlParameters);
    }
}
