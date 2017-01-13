using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Common.Session
{
    internal sealed class SessionState
    {
        public byte[] Content{get;set;}

        public bool Locked { get; set;}

        public DateTime SetTime { get; set;}

        public int LockId { get; set;}

        public int ActionFlag { get; set;}
    }
}
