using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class CreditRepository : ICreditRepository
    {
        public readonly DataContext dc;
        public CreditRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void Add(Credit credit)
        {
            if(dc.Credits is not null && credit is not null) {
                dc.Credits.Add(credit);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Credit>?> GetAllAsync()
        {
            if(dc.Credits is not null) {
                var credits = await dc.Credits
                .ToListAsync();
                if(credits is not null) {
                    return credits;
                }
            }

            return null;
        }
    }
}