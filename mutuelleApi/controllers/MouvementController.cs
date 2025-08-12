using mutuelleApi.dtos;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class MouvementController(IMapper mapper, IUnitOfWork uow, IHubContext<SignalrServer> signalrHub) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<SignalrServer> signalrHub = signalrHub;
		
        [HttpPost]
        public async Task<IActionResult> Add(MouvementRequestDto request)
        {
            var mouvement = mapper.Map<Mouvement>(request); 
			
            mouvement.ModifiePar = GetUserId();
            mouvement.ModifieLe = DateTime.Now;
            uow.MouvementRepository.Add(mouvement);
			
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mouvements = await uow.MouvementRepository.GetAllAsync();
            if(mouvements is null) {
                return NotFound("Mouvements non trouvés");
            }
            var mouvementsDto = mapper.Map<List<MouvementDto>>(mouvements);
            return Ok(mouvementsDto);
        }
		
		[HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var mouvement = await uow.MouvementRepository.GetByIdAsync(id);
            if(mouvement is null) {
                return NotFound("Mouvement non trouvé");
            }
            var mouvementDto = mapper.Map<MouvementDto>(mouvement);
            return Ok(mouvementDto);
        }
		
		[HttpPut]
        public async Task<IActionResult> Remboursement(List<EcheanceDto> request)
        {
			
			foreach(var echeanceRequest in request) {
				var echeance = await uow.EcheanceRepository.GetByIdAsync(echeanceRequest.Id); 
				if(echeance is null) {
					return NotFound("Echance non trouvée");
				}
				mapper.Map(echeanceRequest,echeance);
				echeance.ModifiePar = GetUserId();
                echeance.ModifieLe = DateTime.Now;
                echeance.Rembourser(GetUserId());
			}

            await uow.SaveAsync();
            return StatusCode(201);
        }
		
    }
}