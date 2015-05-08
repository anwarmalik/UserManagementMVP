using System;
using System.Threading.Tasks;
namespace Embed.TestApp.Services
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}
