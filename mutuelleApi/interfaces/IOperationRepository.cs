using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IOperationRepository
    {
        Task<IEnumerable<Operation>?> GetAllAsync();
        void Add(Operation operation);
        void Delete(int id);
    }
}