using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using mutuelleApi.dtos;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class CreditController(IMapper mapper, IUnitOfWork uow, IHubContext<SignalrServer> signalrHub) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<SignalrServer> signalrHub = signalrHub;

		[HttpPut("anticipation/{id}")]
        public async Task<IActionResult> Anticipation(int id,List<EcheanceCreditRequestDto> request)
        {
			var credit = await uow.CreditRepository.GetByIdAsync(id);
			
			if(credit is null) {
				return NotFound("Credit non trouvé");
			}

            var membre = await uow.MembreRepository.GetByIdAsync(credit.MembreId);
            if (membre is null)
            {
                return NotFound("Membre non trouvé");
            }
			
			credit.Membre = membre;

            foreach (var echeanceDto in request)
            {
                var echeance = await uow.EcheanceRepository.GetByIdAsync(echeanceDto.Id);
                if (echeance is null)
                {
                    return NotFound("Echeance non trouvée");
                }

                if (echeance.CreditId != credit.Id)
                {
                    return BadRequest("Echeance non valide");
                }

                mapper.Map(echeanceDto, echeance);
				echeance.Credit = credit;
                echeance.ModifiePar = GetUserId();
                echeance.ModifieLe = DateTime.Now;
                echeance.Rembourser(GetUserId());
            }

            await uow.SaveAsync();
            return StatusCode(201);
        }
		
        [HttpPost]
        public async Task<IActionResult> Add(InfosCreditDto request)
        {
            var membre = await uow.MembreRepository.GetByIdAsync(request.Credit.MembreId);
            if (membre is null)
            {
                return NotFound("Membre non trouvé");
            }
            var credit = mapper.Map<Credit>(request.Credit);
			credit.Membre = membre;
            credit.ModifiePar = GetUserId();
            credit.ModifieLe = DateTime.Now;
            credit.Decaisser(GetUserId());
            uow.CreditRepository.Add(credit);
			
			foreach (var echeanceDto in request.Echeancier)
            {
                var echeance = mapper.Map<Echeance>(echeanceDto);
                echeance.ModifiePar = GetUserId();
                echeance.ModifieLe = DateTime.Now;
                credit.Echeancier?.Add(echeance);
            }

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            uow.CreditRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreditRequestDto request)
        {
            var credit = await uow.CreditRepository.GetByIdAsync(id);
			if(credit is null) {
				return NotFound("Credit non trouvé");
			}
			
			mapper.Map(request, credit);
            credit.ModifiePar = GetUserId();
            credit.ModifieLe = DateTime.Now;

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var credits = await uow.CreditRepository.GetAllAsync();
            if(credits is null) {
                return NotFound("Aucune credit n'a été trouvé dans la bdd");
            }
            var creditsDto = mapper.Map<List<CreditDto>>(credits);
            return Ok(creditsDto);
        }
		
		[HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var credit = await uow.CreditRepository.GetByIdAsync(id);
            if(credit is null) {
                return NotFound("Credit non trouvé");
            }
            var creditDto = mapper.Map<CreditDto>(credit);
            return Ok(creditDto);
        }
		
		[HttpPost("echeance")]
        public async Task<IActionResult> AddEcheances(List<EcheanceCreditRequestDto> request)
        {
            var echeances = mapper.Map<List<Echeance>>(request);
            foreach (var echeance in echeances)
            {
                echeance.ModifiePar = GetUserId();
                echeance.ModifieLe = DateTime.Now;
                uow.EcheanceRepository.Add(echeance);
            }

            await uow.SaveAsync();
            return StatusCode(201);
        }
		
		[HttpPut("echeance/{id}")]
        public async Task<IActionResult> UpdateEcheance(int id, EcheanceCreditRequestDto request)
        {
            var echeance = await uow.EcheanceRepository.GetByIdAsync(id);
			if(echeance is null) {
				return NotFound("Echeance credit non trouvée");
			}
			
			mapper.Map(request, echeance);
            echeance.ModifiePar = GetUserId();
            echeance.ModifieLe = DateTime.Now;

            await uow.SaveAsync();
            return StatusCode(201);
        }
    }
}