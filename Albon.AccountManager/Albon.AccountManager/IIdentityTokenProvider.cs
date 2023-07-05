using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albon.AccountManager
{
    public interface IIdentityTokenProvider
    {
        public string ProvideToken(string accountId);
    }
}
