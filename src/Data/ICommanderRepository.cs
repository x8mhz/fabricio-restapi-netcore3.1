using src.Models;
using System.Collections.Generic;

namespace src.Data
{
    public interface ICommanderRepository
    {
        bool SaveChanges();

        IEnumerable<Command> GetAppCommand();
        Command GetCommandById(int id);
        void Register(Command command);
        void Update(Command command);
        void Remove(Command command);
    }
}