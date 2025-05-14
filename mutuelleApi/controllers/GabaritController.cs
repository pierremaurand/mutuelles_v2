using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using mutuelleApi.dtos;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class GabaritController(IMapper mapper, IUnitOfWork uow, IHubContext<SignalrServer> signalrHub) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<SignalrServer> signalrHub = signalrHub;

        [HttpPost("add")]
        public async Task<IActionResult> Add(GabaritDto gabaritDto)
        {
            var gabarit = mapper.Map<Gabarit>(gabaritDto);
            gabarit.ModifiePar = GetUserId();
            gabarit.ModifieLe = DateTime.Now;
            uow.GabaritRepository.Add(gabarit);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await uow.MembreRepository.GabaritIsUse(id))
            //     return Unauthorized("Cette gabarit ne peut pas être supprimer!");
            // uow.GabaritRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GabaritDto gabaritDto)
        {
            if (id != gabaritDto.Id)
            {
                return Unauthorized("Cette gabarit ne peut pas être modifier");
            }
            var gabarit = mapper.Map<Gabarit>(gabaritDto);
            gabarit.ModifiePar = GetUserId();
            gabarit.ModifieLe = DateTime.Now;
            uow.GabaritRepository.Add(gabarit);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gabarits = await uow.GabaritRepository.GetAllAsync();
            if(gabarits is null) {
                return NotFound("Aucune gabarit n'a été trouvé dans la bdd");
            }
            var gabaritsDto = mapper.Map<List<GabaritDto>>(gabarits);
            return Ok(gabaritsDto);
        }
    }
}