using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using mutuelleApi.dtos;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class CotisationController(IMapper mapper, IUnitOfWork uow, IHubContext<SignalrServer> signalrHub) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<SignalrServer> signalrHub = signalrHub;

        [HttpPost("add")]
        public async Task<IActionResult> Add(CotisationDto cotisationDto)
        {
            var cotisation = mapper.Map<Cotisation>(cotisationDto);
            cotisation.ModifiePar = GetUserId();
            cotisation.ModifieLe = DateTime.Now;
            uow.CotisationRepository.Add(cotisation);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await uow.MembreRepository.CotisationIsUse(id))
            //     return Unauthorized("Cette cotisation ne peut pas être supprimer!");
            // uow.CotisationRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CotisationDto cotisationDto)
        {
            if (id != cotisationDto.Id)
            {
                return Unauthorized("Cette cotisation ne peut pas être modifier");
            }
            var cotisation = mapper.Map<Cotisation>(cotisationDto);
            cotisation.ModifiePar = GetUserId();
            cotisation.ModifieLe = DateTime.Now;
            uow.CotisationRepository.Add(cotisation);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cotisations = await uow.CotisationRepository.GetAllAsync();
            if(cotisations is null) {
                return NotFound("Aucune cotisation n'a été trouvé dans la bdd");
            }
            var cotisationsDto = mapper.Map<List<CotisationDto>>(cotisations);
            return Ok(cotisationsDto);
        }
    }
}