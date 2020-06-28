using System.Collections.Generic;
using src.Models;

namespace src.Data
{
    public interface ICommanderRepository
    {
        bool SaveChanges();

        IEnumerable<Command> GetAppCommand();
        Command GetCommandId(int id);
        void Register(Command command);
        void Update(Command command);
        void Remove(int id);
    }
}