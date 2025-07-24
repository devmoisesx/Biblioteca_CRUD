namespace BibliotecaAPI.Interface
{
    public interface IServiceGeneric<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(string id);
        Task<List<TEntity>> GetsAsync();
        Task UpdateAsync(string id, TEntity entity);
        Task DeleteAsync(string id);
    }
}