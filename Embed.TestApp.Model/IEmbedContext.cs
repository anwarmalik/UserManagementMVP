using Embed.TestApp.Model.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
namespace Embed.TestApp.Model
{
    public interface IEmbedContext
    {
        IDbSet<User> Users { get; set; }

        DbSet<T> Set<T>() where T : class;
        T FindById<T>(int id) where T : class;
        Task<T> FindByIdAsync<T>(int id) where T : class;


        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Commit();
        Task CommitAsync();
    }
}
