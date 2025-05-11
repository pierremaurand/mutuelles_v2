using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface ICotisationRepository
    {
        Task<IEnumerable<Cotisation>?> GetAllAsync();
        void Add(Cotisation cotisation);
        void Delete(int id);
    }
}