using AutoMapper;
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

            var commandItem = _repository.GetCommandId(id);

            if (commandItem == null) return NotFound();

            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

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

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepository = _repository.GetCommandId(id);
            if (commandModelFromRepository == null) return NotFound();

            _mapper.Map(commandUpdateDto, commandModelFromRepository);

            _repository.Update(commandModelFromRepository);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
