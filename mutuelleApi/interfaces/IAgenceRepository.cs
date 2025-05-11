using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IAgenceRepository
    {
        Task<IEnumerable<Agence>?> GetAllAsync();
        void Add(Agence agence);
        void Delete(int id);
    }
}