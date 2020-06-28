using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace src.Data
{
    public class SqlCommanderRepository : ICommanderRepository
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepository(CommanderContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Command> GetAppCommand()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(x => x.Id == id);
        }

        public void Register(Command command)
        {
            if (command == null) throw new ArgumentException(nameof(command));
            _context.Commands.Add(command);
        }

        public void Update(Command command)
        {
           // _context.Commands.Update(command);
        }

        public void Remove(Command command)
        {
            if (command == null) throw new ArgumentException(nameof(command));
            _context.Remove(command);
        }
    }
}