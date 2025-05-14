using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

        [HttpPost("add")]
        public async Task<IActionResult> Add(EcheanceDto echeanceDto)
        {
            var echeance = mapper.Map<Echeance>(echeanceDto);
            echeance.ModifiePar = GetUserId();
            echeance.ModifieLe = DateTime.Now;
            uow.EcheanceRepository.Add(echeance);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await uow.MembreRepository.EcheanceIsUse(id))
            //     return Unauthorized("Cette echeance ne peut pas être supprimer!");
            // uow.EcheanceRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EcheanceDto echeanceDto)
        {
            if (id != echeanceDto.Id)
            {
                return Unauthorized("Cette echeance ne peut pas être modifier");
            }
            var echeance = mapper.Map<Echeance>(echeanceDto);
            echeance.ModifiePar = GetUserId();
            echeance.ModifieLe = DateTime.Now;
            uow.EcheanceRepository.Add(echeance);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var echeances = await uow.EcheanceRepository.GetAllAsync();
            if(echeances is null) {
                return NotFound("Aucune echeance n'a été trouvé dans la bdd");
            }
            var echeancesDto = mapper.Map<List<EcheanceDto>>(echeances);
            return Ok(echeancesDto);
        }
    }
}