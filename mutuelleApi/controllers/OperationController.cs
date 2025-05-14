using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using mutuelleApi.dtos;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class OperationController(IMapper mapper, IUnitOfWork uow, IHubContext<SignalrServer> signalrHub) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<SignalrServer> signalrHub = signalrHub;

        [HttpPost("add")]
        public async Task<IActionResult> Add(OperationDto operationDto)
        {
            var operation = mapper.Map<Operation>(operationDto);
            operation.ModifiePar = GetUserId();
            operation.ModifieLe = DateTime.Now;
            uow.OperationRepository.Add(operation);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await uow.MembreRepository.OperationIsUse(id))
            //     return Unauthorized("Cette operation ne peut pas être supprimer!");
            // uow.OperationRepository.Delete(id);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OperationDto operationDto)
        {
            if (id != operationDto.Id)
            {
                return Unauthorized("Cette operation ne peut pas être modifier");
            }
            var operation = mapper.Map<Operation>(operationDto);
            operation.ModifiePar = GetUserId();
            operation.ModifieLe = DateTime.Now;
            uow.OperationRepository.Add(operation);

            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var operations = await uow.OperationRepository.GetAllAsync();
            if(operations is null) {
                return NotFound("Aucune operation n'a été trouvé dans la bdd");
            }
            var operationsDto = mapper.Map<List<OperationDto>>(operations);
            return Ok(operationsDto);
        }
    }
}