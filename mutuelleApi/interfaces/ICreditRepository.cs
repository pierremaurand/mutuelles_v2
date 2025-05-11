using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface ICreditRepository
    {
        Task<IEnumerable<Credit>?> GetAllAsync();
        void Add(Credit credit);
        void Delete(int id);
    }
}