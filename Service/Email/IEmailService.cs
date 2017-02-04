using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IEmailService
    {
        void SentEmail(Email model);

        Email GetEmail(Guid id);

        List<Email> PageSearch(int pageIndex, int pageSize, out int totalCount, IEnumerable<string> wheres = null, string orderBy = "", params SqlParameter[] sqlParameters);
    }
}
