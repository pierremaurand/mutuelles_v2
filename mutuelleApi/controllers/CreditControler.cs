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

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreditDto creditDto)
        {
            var credit = mapper.Map<Credit>(creditDto);
            credit.ModifiePar = GetUserId();
            credit.ModifieLe = DateTime.Now;
            uow.CreditRepository.Add(credit);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await uow.MembreRepository.CreditIsUse(id))
            //     return Unauthorized("Cette credit ne peut pas être supprimer!");
            // uow.CreditRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreditDto creditDto)
        {
            if (id != creditDto.Id)
            {
                return Unauthorized("Cette credit ne peut pas être modifier");
            }
            var credit = mapper.Map<Credit>(creditDto);
            credit.ModifiePar = GetUserId();
            credit.ModifieLe = DateTime.Now;
            uow.CreditRepository.Add(credit);

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
    }
}