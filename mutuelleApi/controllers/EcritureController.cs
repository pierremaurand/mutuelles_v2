using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mutuelleApi.dtos;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class EcritureController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public EcritureController(IMapper mapper, IUnitOfWork uow, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.uow = uow;
        }

         [HttpPost("add")]
        public async Task<IActionResult> Add(EcritureDto ecritureDto)
        {
            var ecriture = mapper.Map<Ecriture>(ecritureDto);
            ecriture.ModifiePar = GetUserId();
            ecriture.ModifieLe = DateTime.Now;
            uow.EcritureRepository.Add(ecriture);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await uow.MembreRepository.EcritureIsUse(id))
            //     return Unauthorized("Cette ecriture ne peut pas être supprimer!");
            // uow.EcritureRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EcritureDto ecritureDto)
        {
            if (id != ecritureDto.Id)
            {
                return Unauthorized("Cette ecriture ne peut pas être modifier");
            }
            var ecriture = mapper.Map<Ecriture>(ecritureDto);
            ecriture.ModifiePar = GetUserId();
            ecriture.ModifieLe = DateTime.Now;
            uow.EcritureRepository.Add(ecriture);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ecritures = await uow.EcritureRepository.GetAllAsync();
            if(ecritures is null) {
                return NotFound("Aucune ecriture n'a été trouvé dans la bdd");
            }
            var ecrituresDto = mapper.Map<List<EcritureDto>>(ecritures);
            return Ok(ecrituresDto);
        }
    }
}