using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IGabaritRepository
    {
        Task<IEnumerable<Gabarit>?> GetAllAsync();
        void Add(Gabarit gabarit);
        void Delete(int id);
    }
}