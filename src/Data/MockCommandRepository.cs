using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using src.Models;

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
                    new Command {Id = 0, HowTo = "Boil an egg", Line = "Boil Water", Platform = "Kettle & Pan"},
                    new Command {Id = 2, HowTo = "Cut bread", Line = "Get a knife", Platform = "knife & Chopping board"},
                    new Command {Id = 3, HowTo = "Make cup of tea", Line = "Place teabag in cup", Platform = "kettle & cup"}
             
                };

             return _context.Set<Command>();
        }

        public Command GetCommandId(int id)
        {
            return new Command {Id = 0, HowTo = "Boil an egg", Line = "Boil Water", Platform = "Kettle & Pan"};
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

        public void Remove(int id)
        {
            //var commandFound = _context.Find<Command>(id);
            //_context.Remove(commandFound);
        }
    }
}