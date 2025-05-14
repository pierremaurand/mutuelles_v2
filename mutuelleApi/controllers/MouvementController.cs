using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using mutuelleApi.dtos;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class MouvementController(IMapper mapper, IUnitOfWork uow, IHubContext<SignalrServer> signalrHub) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<SignalrServer> signalrHub = signalrHub;

        [HttpPost("add")]
        public async Task<IActionResult> Add(MouvementComptableDto mouvementcomptableDto)
        {
            var mouvementcomptable = mapper.Map<MouvementComptable>(mouvementcomptableDto);
            mouvementcomptable.ModifiePar = GetUserId();
            mouvementcomptable.ModifieLe = DateTime.Now;
            uow.MouvementComptableRepository.Add(mouvementcomptable);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await uow.MembreRepository.MouvementComptableIsUse(id))
            //     return Unauthorized("Cette mouvementcomptable ne peut pas être supprimer!");
            // uow.MouvementComptableRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MouvementComptableDto mouvementcomptableDto)
        {
            if (id != mouvementcomptableDto.Id)
            {
                return Unauthorized("Cette mouvementcomptable ne peut pas être modifier");
            }
            var mouvementcomptable = mapper.Map<MouvementComptable>(mouvementcomptableDto);
            mouvementcomptable.ModifiePar = GetUserId();
            mouvementcomptable.ModifieLe = DateTime.Now;
            uow.MouvementComptableRepository.Add(mouvementcomptable);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mouvementcomptables = await uow.MouvementComptableRepository.GetAllAsync();
            if(mouvementcomptables is null) {
                return NotFound("Aucune mouvementcomptable n'a été trouvé dans la bdd");
            }
            var mouvementcomptablesDto = mapper.Map<List<MouvementComptableDto>>(mouvementcomptables);
            return Ok(mouvementcomptablesDto);
        }
    }
}