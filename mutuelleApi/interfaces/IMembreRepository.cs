using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IMembreRepository
    {
        Task<IEnumerable<Membre>?> FindAllAsync();
        Task<Membre?> FindByIdAsync(int id);
        void Add(Membre membre);
        void Delete(int id);
    }
}