namespace Albon.AccountManager
{
    public class AccountService
    {
        public IDatabaseService DatabaseService { get; set; }

        public IAccountCommunicationService AccountCommunicationService { get; set; }

        public IIdentityTokenProvider IdentityTokenProvider { get; set; }

        public AccountService(IDatabaseService databaseService, IAccountCommunicationService accountCommunicationService, IIdentityTokenProvider identityTokenProvider) 
        { 
            DatabaseService = databaseService;
            AccountCommunicationService = accountCommunicationService;
            IdentityTokenProvider = identityTokenProvider;
        }

        public string GetSessionToken(string publicKey)
        {
            var account = DatabaseService.Query<Account>().FirstOrDefault(account => account.PublicKey == publicKey);

            if(account == null)
            {
                throw new ArgumentException("Account not found.");
            }

            return IdentityTokenProvider.ProvideToken(account.Id);
        }

        public void Create(string emailAddress, string publicKey)
        {
            Account account = new Account(emailAddress, publicKey);
            DatabaseService.Add(account);

            AccountCommunicationService.NotifyAccountCreation(emailAddress);
        }

        public void Delete(string publicKey)
        {
            DatabaseService.Delete(DatabaseService.Query<Account>().Single(a => a.PublicKey == publicKey));
        }

        public void ChangePublicKey(string email, string newPublicKey)
        {
            var account = DatabaseService.Query<Account>().Single(account => account.EmailAddress == email);
            account.PublicKey = newPublicKey;
            DatabaseService.Update(account);
        }

        public void ChangeEmailAddress(string email, string newEmail)
        {
            var account = DatabaseService.Query<Account>().Single(account => account.EmailAddress == email);
            account.EmailAddress = newEmail;
            DatabaseService.Update(account);
        }

        public IEnumerable<Account> Search()
        {
            return DatabaseService.Query<Account>();
        }

        public Account Search(string publicKey)
        {
            return DatabaseService.Query<Account>().Single(x => x.PublicKey == publicKey);
        }

        public bool CheckAccountExistence(string publicKey)
        {
            return DatabaseService.Query<Account>().FirstOrDefault(x => x.PublicKey == publicKey) != null;
        }
    }
}
