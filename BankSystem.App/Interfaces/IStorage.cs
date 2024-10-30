
namespace BankSystem.Data.Storages
{
    public interface IStorage<T>
    {
        public Task AddAsync(T item);
        public Task UpdateAsync(T item);
        public Task DeleteAsync(T item);
        public Task<List<T>> GetAsync(Func<T, bool> filter);
    }
}
