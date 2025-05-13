using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mutuelleApi.dtos;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class UtilisateurController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UtilisateurController(IMapper mapper, IUnitOfWork uow, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.uow = uow;
        }

         [HttpPost("add")]
        public async Task<IActionResult> Add(UtilisateurDto utilisateurDto)
        {
            var utilisateur = mapper.Map<Utilisateur>(utilisateurDto);
            utilisateur.ModifiePar = GetUserId();
            utilisateur.ModifieLe = DateTime.Now;
            uow.UtilisateurRepository.Add(utilisateur);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await uow.MembreRepository.UtilisateurIsUse(id))
                return Unauthorized("Cette utilisateur ne peut pas être supprimer!");
            uow.UtilisateurRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UtilisateurDto utilisateurDto)
        {
            if (id != utilisateurDto.Id)
            {
                return Unauthorized("Cette utilisateur ne peut pas être modifier");
            }
            var utilisateur = mapper.Map<Utilisateur>(utilisateurDto);
            utilisateur.ModifiePar = GetUserId();
            utilisateur.ModifieLe = DateTime.Now;
            uow.UtilisateurRepository.Add(utilisateur);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var utilisateurs = await uow.UtilisateurRepository.GetAllAsync();
            if(utilisateurs is null) {
                return NotFound("Aucune utilisateur n'a été trouvé dans la bdd");
            }
            var utilisateursDto = mapper.Map<List<UtilisateurDto>>(utilisateurs);
            return Ok(utilisateursDto);
        }
    }
}