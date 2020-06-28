using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Dtos;
using src.Models;
using System.Collections.Generic;

namespace src.Controllers
{
    //api/commands
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepository _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //Traz os itens PERMITIDOS para leitura atraves do mapeamento dos modelos command/commandreaddto
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandsItems = _repository.GetAppCommand();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandsItems));
        }
        
        //GET api/commands/{id}
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null) return NotFound();

            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

        //Registra no banco de dados os itens do modelo command sem que seja necessário o acesso direto a class, atraves do mapeamento com uma classe especifica para tratar essa diretriz
        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);

            _repository.Register(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);
        }

        //Atualiza atraves da procura da chave, porem sem usar um função direta de atualização como update no repository, mas sim atraves do mapeamento das identicas classes commandupdatedto e command
        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepository = _repository.GetCommandById(id);
            if (commandModelFromRepository == null) return NotFound();

            _mapper.Map(commandUpdateDto, commandModelFromRepository);

            _repository.Update(commandModelFromRepository);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> pathDocument)
        {
            var commandModelFromRepository = _repository.GetCommandById(id);
            if (commandModelFromRepository == null) return NotFound();

            var commandToPath = _mapper.Map<CommandUpdateDto>(commandModelFromRepository);

            pathDocument.ApplyTo(commandToPath, ModelState);
            if (!TryValidateModel(commandToPath)) return ValidationProblem(ModelState);

            _mapper.Map(commandToPath, commandModelFromRepository);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepository = _repository.GetCommandById(id);
            if (commandModelFromRepository == null) return NotFound();

            _repository.Remove(commandModelFromRepository);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
