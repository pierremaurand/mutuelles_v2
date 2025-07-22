using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using mutuelleApi.dtos;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class CreditControler(IMapper mapper, IUnitOfWork uow, IHubContext<SignalrServer> signalrHub) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<SignalrServer> signalrHub = signalrHub;

        [HttpPost]
        public async Task<IActionResult> Add(CreditRequestDto request)
        {
            var credit = mapper.Map<Credit>(request);
            credit.ModifiePar = GetUserId();
            credit.ModifieLe = DateTime.Now;
            uow.CreditRepository.Add(credit);

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