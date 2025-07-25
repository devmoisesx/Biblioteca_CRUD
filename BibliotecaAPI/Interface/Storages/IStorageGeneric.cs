namespace BibliotecaAPI.Interface.Storages
{
    public interface IStorageGeneric<TEntity> where TEntity : class     // Interface generica para os Storages
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(string id);
        Task<List<TEntity>> GetsAsync();
        Task UpdateAsync(string id, TEntity entity);
        Task DeleteAsync(string id);
    }
}