using Embed.TestApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.TestApp.Model
{
    public class EmbedContext : DbContext, IEmbedContext
    {
        public EmbedContext(string name)
            : base(name)
        {
            
        }

        public T FindById<T>(int id) where T : class
        {
            return Set<T>().Find(id);
        }

        public async Task<T> FindByIdAsync<T>(int id) where T : class
        {
            return await Set<T>().FindAsync(id);
        }

        public void Update<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void Add<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
        }

        private void AuditEntities()
        {
            var addedAuditedEntities = ChangeTracker.Entries<IAuditEntity>()
          .Where(p => p.State == EntityState.Added)
          .Select(p => p.Entity);

                    var modifiedAuditedEntities = ChangeTracker.Entries<IAuditEntity>()
                      .Where(p => p.State == EntityState.Modified)
                      .Select(p => p.Entity);

                    var now = DateTime.UtcNow;

                    foreach (var added in addedAuditedEntities)
                    {
                        added.CreatedAt = now;
                        added.LastModifiedAt = now;
                    }

                    foreach (var modified in modifiedAuditedEntities)
                    {
                        modified.LastModifiedAt = now;
                    }
        }

        public void Commit()
        {
            try
            {
                AuditEntities();
                SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var errorMessages = e.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMesssage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMesssage);

                throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                AuditEntities();
                await base.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                var errorMessages = e.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMesssage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMesssage);

                throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
            }
        }


        public IDbSet<User> Users
        {
            get;
            set;
        }
    }
}
