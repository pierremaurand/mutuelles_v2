using AutoMapper;
using mutuelleApi.dtos;
using mutuelleApi.models;

namespace mutuelleApi.helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Adhesion, AgenceDto>().ReverseMap();
            CreateMap<Adhesion, AgenceRequest>().ReverseMap(); 

            CreateMap<Agence, AgenceDto>().ReverseMap();
            CreateMap<Agence, AgenceRequest>().ReverseMap();

            CreateMap<Avance, AvanceDto>().ReverseMap();
            CreateMap<Compte, CompteDto>().ReverseMap();

            CreateMap<Cotisation, CotisationDto>().ReverseMap();
            CreateMap<Cotisation, CotisationRequestDto>().ReverseMap();

            CreateMap<Credit, CreditDto>().ReverseMap();
            CreateMap<Echeance, EcheanceDto>().ReverseMap();
            CreateMap<Ecriture, EcritureDto>().ReverseMap();
            CreateMap<Gabarit, GabaritDto>().ReverseMap();

            CreateMap<Membre, MembreDto>().ReverseMap();
            CreateMap<Membre, MembreRequest>().ReverseMap();

            CreateMap<Mouvement, MouvementDto>().ReverseMap();
            CreateMap<MouvementComptable, MouvementComptableDto>().ReverseMap();
            CreateMap<Operation, OperationDto>().ReverseMap();

            CreateMap<Utilisateur, UtilisateurDto>().ReverseMap();
            CreateMap<Utilisateur, UtilisateurRequestDto>().ReverseMap();
        }
    }
}