using Common;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService:IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<LoginHistory> _loginHistoryRepository;

        public UserService(IRepository<User> userRepository,IRepository<LoginHistory> loginHistoryRepository)
        {
            this._userRepository = userRepository;
            this._loginHistoryRepository = loginHistoryRepository;
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.Password = EncryptHelper.GetMD5(user.Password);

            _userRepository.Insert(user);
        }

        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            _userRepository.Delete(user);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            _userRepository.Update(user);
        }

        /// <summary>
        /// 按账号查询用户
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        public User Find(string account)
        {
            var query = _userRepository.Table;
            return query.FirstOrDefault<User>(m => m.Account == account);
        }

        public void CreateLoginHistory(LoginHistory loginHistory)
        {
            if (loginHistory == null)
            {
                throw new ArgumentNullException("loginHistory");
            }

            _loginHistoryRepository.Insert(loginHistory);
        }


        public void UpdateLoginHistory(LoginHistory loginHistory)
        {
            if (loginHistory == null)
            {
                throw new ArgumentNullException("loginHistory");
            }

            _loginHistoryRepository.Update(loginHistory);
        }
    }
}
