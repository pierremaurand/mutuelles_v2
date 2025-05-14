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

        [HttpPost("add")]
        public async Task<IActionResult> Add(AvanceDto avanceDto)
        {
            var avance = mapper.Map<Avance>(avanceDto);
            avance.ModifiePar = GetUserId();
            avance.ModifieLe = DateTime.Now;
            uow.AvanceRepository.Add(avance);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await uow.MembreRepository.AvanceIsUse(id))
            //     return Unauthorized("Cette avance ne peut pas être supprimer!");
            // uow.AvanceRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AvanceDto avanceDto)
        {
            if (id != avanceDto.Id)
            {
                return Unauthorized("Cette avance ne peut pas être modifier");
            }
            var avance = mapper.Map<Avance>(avanceDto);
            avance.ModifiePar = GetUserId();
            avance.ModifieLe = DateTime.Now;
            uow.AvanceRepository.Add(avance);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var avances = await uow.AvanceRepository.GetAllAsync();
            if(avances is null) {
                return NotFound("Aucune avance n'a été trouvé dans la bdd");
            }
            var avancesDto = mapper.Map<List<AvanceDto>>(avances);
            return Ok(avancesDto);
        }
    }
}