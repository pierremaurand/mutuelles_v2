using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IAdhesionRepository
    {
        Task<IEnumerable<Adhesion>?> FindAllAsync();
        Task<Adhesion?> FindByIdAsync(int id);
        void Add(Adhesion adhesion);
        void Delete(int id);
    }
}