using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mutuelleApi.dtos;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class MembreController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public MembreController(IMapper mapper, IUnitOfWork uow, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.uow = uow;
        }

         [HttpPost("add")]
        public async Task<IActionResult> Add(MembreDto membreDto)
        {
            var membre = mapper.Map<Membre>(membreDto);
            membre.ModifiePar = GetUserId();
            membre.ModifieLe = DateTime.Now;
            uow.MembreRepository.Add(membre);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await uow.MembreRepository.MembreIsUse(id))
            //     return Unauthorized("Cette membre ne peut pas être supprimer!");
            // uow.MembreRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MembreDto membreDto)
        {
            if (id != membreDto.Id)
            {
                return Unauthorized("Cette membre ne peut pas être modifier");
            }
            var membre = mapper.Map<Membre>(membreDto);
            membre.ModifiePar = GetUserId();
            membre.ModifieLe = DateTime.Now;
            uow.MembreRepository.Add(membre);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var membres = await uow.MembreRepository.GetAllAsync();
            if(membres is null) {
                return NotFound("Aucune membre n'a été trouvé dans la bdd");
            }
            var membresDto = mapper.Map<List<MembreDto>>(membres);
            return Ok(membresDto);
        }
    }
}