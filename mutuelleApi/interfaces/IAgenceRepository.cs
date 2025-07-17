using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IAgenceRepository
    {
        Task<IEnumerable<Agence>?> FindAllAsync();
        Task<Agence?> FindByIdAsync(int id);
        void Add(Agence agence);
        void Delete(int id);
    }
}