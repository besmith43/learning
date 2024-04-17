using System.Threading.Tasks;

namespace console_interface.Commands
{
    public interface ICommand
    {
        Task<bool> ExecuteAsync();
    }
}
