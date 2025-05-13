using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mutuelleApi.dtos;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class AgenceController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AgenceController(IMapper mapper, IUnitOfWork uow, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.uow = uow;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AgenceDto agenceDto)
        {
            var agence = mapper.Map<Agence>(agenceDto);
            agence.ModifiePar = GetUserId();
            agence.ModifieLe = DateTime.Now;
            uow.AgenceRepository.Add(agence);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await uow.MembreRepository.AgenceIsUse(id))
                return Unauthorized("Cette agence ne peut pas être supprimer!");
            uow.AgenceRepository.Delete(id);
            await uow.SaveAsync();
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