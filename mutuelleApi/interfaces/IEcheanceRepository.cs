using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IEcheanceRepository
    {
        Task<IEnumerable<Echeance>?> GetAllAsync();
        void Add(Echeance echeance);
        void Delete(int id);
    }
}