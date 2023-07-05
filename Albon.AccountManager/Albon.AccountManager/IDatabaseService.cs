namespace Albon.AccountManager
{
    public interface IDatabaseService
    {
        public void Add<T>(T entity);

        public void Update<T>(T entity);

        public void Delete<T>(T entity);

        public IQueryable<T> Query<T>();
    }
}
