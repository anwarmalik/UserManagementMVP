using Embed.TestApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.TestApp.Services
{
    public class BaseService<T> : IService<T> where T : class
    {

        protected IEmbedContext EmbedContext;

        protected BaseService(IEmbedContext context)
        {
            EmbedContext = context;
        }

        public T GetById(int id)
        {
            return EmbedContext.FindById<T>(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await EmbedContext.FindByIdAsync<T>(id);
        }

        public virtual IQueryable<T> QuerySet
        {
            get
            {
                return EmbedContext.Set<T>();
            }
        }

        public void Add(T entity)
        {
            EmbedContext.Add(entity);
        }

        public void Update(T entity)
        {
            EmbedContext.Update(entity);
        }
    }

}
