using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
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
    }
}
