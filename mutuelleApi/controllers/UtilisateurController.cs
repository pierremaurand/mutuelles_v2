using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using mutuelleApi.dtos;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.Migrations;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class UtilisateurController(IMapper mapper, IUnitOfWork uow, IConfiguration configuration, IHubContext<SignalrServer> signalrHub) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IConfiguration configuration = configuration;
        private readonly IHubContext<SignalrServer> signalrHub = signalrHub;

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

        [HttpPost("addImage/{id}")]
        public async Task<IActionResult> AddImage(int id, IFormFile files)
        {
            if (id == 0)
                return BadRequest("Utilisateur non valid");

            var utilisateur = await uow.UtilisateurRepository.FindByIdAsync(id);

            if (utilisateur is null)
                return NotFound("Utilisateur introuvable");

            if (files.Length > 0)
            {



            }

            await uow.SaveAsync();
            await signalrHub.Clients.All.SendAsync("UtilisateurUpdated", mapper.Map<UtilisateurDto>(utilisateur));
            return Ok();
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