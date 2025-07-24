using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using mutuelleApi.dtos;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class AvanceController(IMapper mapper, IUnitOfWork uow, IHubContext<SignalrServer> signalrHub) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<SignalrServer> signalrHub = signalrHub;

		[HttpPut("anticipation/{id}")]
        public async Task<IActionResult> Anticipation(int id,List<EcheanceDto> request)
        {
			var avance = await uow.AvanceRepository.GetByIdAsync(id);
			
			if(avance is null) {
				return NotFound("Avance non trouvée");
			}
			
			foreach (var echeanceDto in request)
            {
				 var echeance = await uow.EcheanceRepository.GetByIdAsync(echeanceDto.Id);
				 if(echeance is null) {
					 return NotFound("Echeance non trouvée");
				 }
				 
				 if(echeance.AvanceId != avance.Id) {
					 return BadRequest("Echeance non valide");
				 }
				 
				 mapper.Map(echeanceDto,echeance);
                echeance.ModifiePar = GetUserId();
                echeance.ModifieLe = DateTime.Now;
            }

            await uow.SaveAsync();
            return StatusCode(201);
        }
		
        [HttpPost]
        public async Task<IActionResult> Add(InfosAvanceDto request)
        {
            var avance = mapper.Map<Avance>(request.Avance);
            avance.ModifiePar = GetUserId();
            avance.ModifieLe = DateTime.Now;
            uow.AvanceRepository.Add(avance);
			await uow.SaveAsync();
			
			foreach (var echeanceDto in request.Echeancier)
            {
				 var echeance = mapper.Map<Echeance>(echeanceDto);
				 echeance.AvanceId = avance.Id;
                echeance.ModifiePar = GetUserId();
                echeance.ModifieLe = DateTime.Now;
                uow.EcheanceRepository.Add(echeance);
            }

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            uow.AvanceRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AvanceRequestDto request)
        {
            var avance = await uow.AvanceRepository.GetByIdAsync(id);
			if(avance is null) {
				return NotFound("Avance non trouvée");
			}
			
			mapper.Map(request, avance);
            avance.ModifiePar = GetUserId();
            avance.ModifieLe = DateTime.Now;

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var avances = await uow.AvanceRepository.GetAllAsync();
            if(avances is null) {
                return NotFound("Avances non trouvées");
            }
            var avancesDto = mapper.Map<List<AvanceDto>>(avances);
            return Ok(avancesDto);
        }
		
		[HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var avance = await uow.AvanceRepository.GetByIdAsync(id);
            if(avance is null) {
                return NotFound("Avance non trouvée");
            }
            var avanceDto = mapper.Map<AvanceDto>(avance);
            return Ok(avanceDto);
        }
		
		[HttpPost("echeance")]
        public async Task<IActionResult> AddEcheances(List<EcheanceAvanceRequestDto> request)
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
        public async Task<IActionResult> UpdateEcheance(int id, EcheanceAvanceRequestDto request)
        {
            var echeance = await uow.EcheanceRepository.GetByIdAsync(id);
			if(echeance is null) {
				return NotFound("Echeance avance non trouvée");
			}
			
			mapper.Map(request, echeance);
            echeance.ModifiePar = GetUserId();
            echeance.ModifieLe = DateTime.Now;

            await uow.SaveAsync();
            return StatusCode(201);
        }
    }
}