using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mutuelleApi.dtos;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class MouvementComptableController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public MouvementComptableController(IMapper mapper, IUnitOfWork uow, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.uow = uow;
        }

         [HttpPost("add")]
        public async Task<IActionResult> Add(MouvementDto mouvementDto)
        {
            var mouvement = mapper.Map<Mouvement>(mouvementDto);
            mouvement.ModifiePar = GetUserId();
            mouvement.ModifieLe = DateTime.Now;
            uow.MouvementRepository.Add(mouvement);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await uow.MembreRepository.MouvementIsUse(id))
            //     return Unauthorized("Cette mouvement ne peut pas être supprimer!");
            // uow.MouvementRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MouvementDto mouvementDto)
        {
            if (id != mouvementDto.Id)
            {
                return Unauthorized("Cette mouvement ne peut pas être modifier");
            }
            var mouvement = mapper.Map<Mouvement>(mouvementDto);
            mouvement.ModifiePar = GetUserId();
            mouvement.ModifieLe = DateTime.Now;
            uow.MouvementRepository.Add(mouvement);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mouvements = await uow.MouvementRepository.GetAllAsync();
            if(mouvements is null) {
                return NotFound("Aucune mouvement n'a été trouvé dans la bdd");
            }
            var mouvementsDto = mapper.Map<List<MouvementDto>>(mouvements);
            return Ok(mouvementsDto);
        }
    }
}