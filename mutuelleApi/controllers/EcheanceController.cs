using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using mutuelleApi.dtos;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class EcheanceController(IMapper mapper, IUnitOfWork uow, IHubContext<SignalrServer> signalrHub) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<SignalrServer> signalrHub = signalrHub;

        [HttpGet]
		[AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var echeances = await uow.EcheanceRepository.GetAllAsync();
            if(echeances is null) {
                return NotFound("Echéances non trouvées");
            }
            var echeancesDto = mapper.Map<List<EcheanceDto>>(echeances);
            return Ok(echeancesDto);
        }
		
		[HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var echeance = await uow.EcheanceRepository.GetByIdAsync(id);
            if(echeance is null) {
                return NotFound("Echéance non trouvée");
            }
            var echeanceDto = mapper.Map<EcheanceDto>(echeance);
            return Ok(echeanceDto);
        }
    }
}