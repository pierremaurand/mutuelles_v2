using mutuelleApi.data.repo;
using mutuelleApi.interfaces;

namespace mutuelleApi.data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;
        public UnitOfWork(DataContext dc)
        {
            this.dc = dc;
        }

        public IAgenceRepository AgenceRepository => new AgenceRepository(dc);

    public IAvanceRepository AvanceRepository => new AvanceRepository(dc);

        public ICompteRepository CompteRepository => new CompteRepository(dc);

        public ICotisationRepository CotisationRepository => new CotisationRepository(dc);

        public ICreditRepository CreditRepository => new CreditRepository(dc);

        public IEcheanceRepository EcheanceRepository => new EcheanceRepository(dc);

        public IEcritureRepository EcritureRepository => new EcritureRepository(dc);

        public IGabaritRepository GabaritRepository => new GabaritRepository(dc);

        public IMembreRepository MembreRepository => new MembreRepository(dc);

        public IMouvementRepository MouvementRepository => new MouvementRepository(dc);

        public IMouvementComptableRepository MouvementComptableRepository => new MouvementComptableRepository(dc);

        public IOperationRepository OperationRepository => new OperationRepository(dc);

        public IUtilisateurRepository UtilisateurRepository => new UtilisateurRepository(dc);

        public async Task<bool> SaveAsync()
        {
           return await dc.SaveChangesAsync() > 0;
        }
    }
}