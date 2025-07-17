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

        [HttpPost]
        public async Task<IActionResult> Add(AgenceRequest request)
        {
            var agence = mapper.Map<Agence>(request);
            agence.ModifiePar = GetUserId();
            agence.ModifieLe = DateTime.Now;
            uow.AgenceRepository.Add(agence);

            await uow.SaveAsync();
            // await signalrHub.Clients.All.SendAsync("AgenceAdded", mapper.Map<AgenceDto>(agence));
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            uow.AgenceRepository.Delete(id);
            await uow.SaveAsync();
            await signalrHub.Clients.All.SendAsync("AgenceDeleted", id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AgenceRequest request)
        {
            var agence = await uow.AgenceRepository.FindByIdAsync(id);
            if (agence is null)
            {
                return NotFound("Agence non trouvée!");
            }
            mapper.Map(request, agence);
            agence.ModifiePar = GetUserId();
            agence.ModifieLe = DateTime.Now;

            await uow.SaveAsync();
            // await signalrHub.Clients.All.SendAsync("AgenceUpdated", mapper.Map<AgenceDto>(agence));
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agences = await uow.AgenceRepository.FindAllAsync();
            if(agences is null) {
                return NotFound("Aucune agence n'a été trouvé dans la bdd");
            }
            var agencesDto = mapper.Map<List<AgenceDto>>(agences);
            return Ok(agencesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var agence = await uow.AgenceRepository.FindByIdAsync(id);
            if(agence is null) {
                return NotFound("Agence non trouvée!");
            }
            var agenceDto = mapper.Map<AgenceDto>(agence);
            return Ok(agenceDto);
        }
    }
}