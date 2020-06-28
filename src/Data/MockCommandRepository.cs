using src.Models;
using System.Collections.Generic;

namespace src.Data
{
    public class MockCommandRepository : ICommanderRepository
    {
        private readonly CommanderContext _context;

        public MockCommandRepository(CommanderContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Command> GetAppCommand()
        {
             var commands = new List<Command>
                {
                    new Command {Id = 0, HowTo = "Cozinhar ovo", Line = "Esquentar a água", Platform = "Panela"},
                    new Command {Id = 2, HowTo = "Contar pão", Line = "Pega uma faca", Platform = "Faca"},
                    new Command {Id = 3, HowTo = "Fazer chá", Line = "Colocar o sachê no copo", Platform = "Chaleira"}
             
                };

             return _context.Set<Command>();
        }

        public Command GetCommandById(int id)
        {
            return new Command {Id = 0, HowTo = "Cozinhar ovo", Line = "Esquentar a água", Platform = "Panela"};
        }

        public void Register(Command command)
        {
            //if (command == null) throw new ArgumentException(nameof(command));
            //_context.Commands.Add(command);
            throw new System.NotImplementedException();
        }

        public void Update(Command command)
        {
            //_context.Commands.Update(command);
            throw new System.NotImplementedException();
        }

        public void Remove(Command command)
        {
            //var commandFound = _context.Find<Command>(id);
            //_context.Remove(commandFound);

        }
    }
}