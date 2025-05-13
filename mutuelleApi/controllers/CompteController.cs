using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mutuelleApi.dtos;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class CompteController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public CompteController(IMapper mapper, IUnitOfWork uow, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.uow = uow;
        }

         [HttpPost("add")]
        public async Task<IActionResult> Add(CompteDto compteDto)
        {
            var compte = mapper.Map<Compte>(compteDto);
            compte.ModifiePar = GetUserId();
            compte.ModifieLe = DateTime.Now;
            uow.CompteRepository.Add(compte);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await uow.MembreRepository.CompteIsUse(id))
            //     return Unauthorized("Cette compte ne peut pas être supprimer!");
            // uow.CompteRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CompteDto compteDto)
        {
            if (id != compteDto.Id)
            {
                return Unauthorized("Cette compte ne peut pas être modifier");
            }
            var compte = mapper.Map<Compte>(compteDto);
            compte.ModifiePar = GetUserId();
            compte.ModifieLe = DateTime.Now;
            uow.CompteRepository.Add(compte);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comptes = await uow.CompteRepository.GetAllAsync();
            if(comptes is null) {
                return NotFound("Aucune compte n'a été trouvé dans la bdd");
            }
            var comptesDto = mapper.Map<List<CompteDto>>(comptes);
            return Ok(comptesDto);
        }
    }
}