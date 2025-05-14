using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using mutuelleApi.dtos;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class AgenceController(IMapper mapper, IUnitOfWork uow, IHubContext<SignalrServer> signalrHub) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<SignalrServer> signalrHub = signalrHub;

        [HttpPost("add")]
        public async Task<IActionResult> Add(AgenceDto agenceDto)
        {
            var agence = mapper.Map<Agence>(agenceDto);
            agence.ModifiePar = GetUserId();
            agence.ModifieLe = DateTime.Now;
            uow.AgenceRepository.Add(agence);

            await uow.SaveAsync();
            await signalrHub.Clients.All.SendAsync("AgenceAdded", mapper.Map<AgenceDto>(agence));
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await uow.MembreRepository.AgenceIsUse(id))
                return Unauthorized("Cette agence ne peut pas être supprimer!");
            uow.AgenceRepository.Delete(id);
            await uow.SaveAsync();
            await signalrHub.Clients.All.SendAsync("AgenceDeleted", id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AgenceDto agenceDto)
        {
            if (id != agenceDto.Id)
            {
                return Unauthorized("Cette agence ne peut pas être modifier");
            }
            var agence = mapper.Map<Agence>(agenceDto);
            agence.ModifiePar = GetUserId();
            agence.ModifieLe = DateTime.Now;
            uow.AgenceRepository.Add(agence);

            await uow.SaveAsync();
            await signalrHub.Clients.All.SendAsync("AgenceUpdated", mapper.Map<AgenceDto>(agence));
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agences = await uow.AgenceRepository.GetAllAsync();
            if(agences is null) {
                return NotFound("Aucune agence n'a été trouvé dans la bdd");
            }
            var agencesDto = mapper.Map<List<AgenceDto>>(agences);
            return Ok(agencesDto);
        }
    }
}