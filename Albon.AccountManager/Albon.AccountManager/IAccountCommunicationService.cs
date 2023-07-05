using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albon.AccountManager
{
    public interface IAccountCommunicationService
    {
        public void NotifyAccountCreation(string emailAddress);
    }
}
