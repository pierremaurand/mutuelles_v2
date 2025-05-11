using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class OperationRepository : IOperationRepository
    {
        public readonly DataContext dc;
        public OperationRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void Add(Operation operation)
        {
            if (dc.Operations is not null && operation is not null)
            {
                dc.Operations.Add(operation);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Operation>?> GetAllAsync()
        {
            if(dc.Operations is not null) {
                var operations = await dc.Operations
                .ToListAsync();
                if(operations is not null) {
                    return operations;
                }
            }

            return null;
        }
    }
}