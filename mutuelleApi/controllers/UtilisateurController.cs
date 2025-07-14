using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using mutuelleApi.dtos;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class UtilisateurController(IMapper mapper, IUnitOfWork uow, IConfiguration configuration, IHubContext<SignalrServer> signalrHub) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IConfiguration configuration = configuration;
        private readonly IHubContext<SignalrServer> signalrHub = signalrHub;

        [HttpPost]
        public async Task<IActionResult> Add(UtilisateurRequestDto request)
        {
            if (await uow.UtilisateurRepository.FindByLoginAsync(request.Login))
            {
                return BadRequest("Ce login est pris!");
            }
            var utilisateur = mapper.Map<Utilisateur>(request);
            utilisateur.ModifiePar = GetUserId();
            utilisateur.ModifieLe = DateTime.Now;
            uow.UtilisateurRepository.Add(utilisateur);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("activate/{id}")]
        public async Task<IActionResult> Activate(int id, UpdateUtilisateurActifRequestDto request)
        {
            var utilisateur = await uow.UtilisateurRepository.FindByIdAsync(id);
            if (utilisateur is null)
            {
                return NotFound("Cet utilisateur n'existe pas!");
            }
            mapper.Map(request, utilisateur);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UtilisateurRequestDto request)
        {
            var utilisateur = await uow.UtilisateurRepository.FindByIdAsync(id);
            if (utilisateur is null)
            {
                return NotFound("Cet utilisateur n'existe pas!");
            }

            if (utilisateur.Login is not null && !utilisateur.Login.Equals(request.Login) && await uow.UtilisateurRepository.FindByLoginAsync(request.Login))
            {
                return BadRequest("Ce login est pris!");
            }

            mapper.Map(request, utilisateur);
            utilisateur.ModifiePar = GetUserId();
            utilisateur.ModifieLe = DateTime.Now;

            await uow.SaveAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var utilisateurs = await uow.UtilisateurRepository.GetAllAsync();
            if (utilisateurs is null)
            {
                return NotFound("Aucune utilisateur n'a été trouvé dans la bdd");
            }
            var utilisateursDto = mapper.Map<List<UtilisateurDto>>(utilisateurs);
            return Ok(utilisateursDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var utilisateur = await uow.UtilisateurRepository.FindByIdAsync(id);
            if (utilisateur is null) {
                return NotFound("Utilisateur non trouvé!");
            }
            var utilisateurDto = mapper.Map<UtilisateurDto>(utilisateur);
            return Ok(utilisateurDto);
        }
    }
}