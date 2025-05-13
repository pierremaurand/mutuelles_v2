using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mutuelleApi.dtos;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class GabaritController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public GabaritController(IMapper mapper, IUnitOfWork uow, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.uow = uow;
        }

         [HttpPost("add")]
        public async Task<IActionResult> Add(GabaritDto gabaritDto)
        {
            var gabarit = mapper.Map<Gabarit>(gabaritDto);
            gabarit.ModifiePar = GetUserId();
            gabarit.ModifieLe = DateTime.Now;
            uow.GabaritRepository.Add(gabarit);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await uow.MembreRepository.GabaritIsUse(id))
            //     return Unauthorized("Cette gabarit ne peut pas être supprimer!");
            // uow.GabaritRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GabaritDto gabaritDto)
        {
            if (id != gabaritDto.Id)
            {
                return Unauthorized("Cette gabarit ne peut pas être modifier");
            }
            var gabarit = mapper.Map<Gabarit>(gabaritDto);
            gabarit.ModifiePar = GetUserId();
            gabarit.ModifieLe = DateTime.Now;
            uow.GabaritRepository.Add(gabarit);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gabarits = await uow.GabaritRepository.GetAllAsync();
            if(gabarits is null) {
                return NotFound("Aucune gabarit n'a été trouvé dans la bdd");
            }
            var gabaritsDto = mapper.Map<List<GabaritDto>>(gabarits);
            return Ok(gabaritsDto);
        }
    }
}