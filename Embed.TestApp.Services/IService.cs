using Embed.TestApp.Model;
using Embed.TestApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.TestApp.Services
{

    public interface IService<T> 
       where T : class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> QuerySet { get; }
        void Add(T entity);
        void Update(T entity);
    }

}
