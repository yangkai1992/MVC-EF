using Common;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService:IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<LoginHistory> _loginHistoryRepository;
        private readonly IDbContext _dbContext;

        public UserService(IRepository<User> userRepository,IRepository<LoginHistory> loginHistoryRepository,IDbContext dbContext)
        {
            this._userRepository = userRepository;
            this._loginHistoryRepository = loginHistoryRepository;
            this._dbContext = dbContext;
        }

        public User GetUser(Guid id)
        {
            return _userRepository.GetById(id);
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


        public List<User> PageSearch(int pageIndex,int pageSize,out int totalCount,IEnumerable<string> wheres=null,string orderBy="",params SqlParameter[] sqlParameters)
        {
            string sql = SQLUtls.GeneratePageSearchSQL(pageIndex, pageSize, "[user]", wheres, orderBy);
            return _dbContext.PageSearch<User>(sql, out totalCount, sqlParameters);
        }
    }
}
