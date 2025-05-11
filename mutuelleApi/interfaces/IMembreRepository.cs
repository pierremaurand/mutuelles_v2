using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IMembreRepository
    {
        Task<IEnumerable<Membre>?> GetAllAsync();
        void Add(Membre membre);
        void Delete(int id);
    }
}