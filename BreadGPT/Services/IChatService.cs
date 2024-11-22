namespace BreadGPT.Services
{
    interface IChatService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<T> CreateAsync(T chat);
        Task<T> UpdateAsync(Guid id, T chat);
        Task<bool> DeleteAsync(Guid id);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
    }
}
