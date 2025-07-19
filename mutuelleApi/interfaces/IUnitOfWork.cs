

namespace mutuelleApi.interfaces
{
    public interface IUnitOfWork
    {
        IAdhesionRepository AdhesionRepository { get; } 
        IAgenceRepository AgenceRepository { get; } 
        IAvanceRepository AvanceRepository { get; }
        ICompteRepository CompteRepository { get; }
        ICotisationRepository CotisationRepository { get; }
        ICreditRepository CreditRepository { get; }
        IEcheanceRepository EcheanceRepository { get; }
        IEcritureRepository EcritureRepository { get; }
        IGabaritRepository GabaritRepository { get; }
        IMembreRepository MembreRepository { get; }
        IMouvementRepository MouvementRepository { get; }
        IMouvementComptableRepository MouvementComptableRepository { get; }
        IOperationRepository OperationRepository { get; }
        IUtilisateurRepository UtilisateurRepository { get; }

        Task<bool> SaveAsync();
    }
}