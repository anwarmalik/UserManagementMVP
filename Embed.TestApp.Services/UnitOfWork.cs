using Embed.TestApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.TestApp.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEmbedContext _context;

        public UnitOfWork (IEmbedContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.Commit();
        }

        public async Task CommitAsync()
        {
            await _context.CommitAsync();
        }

    }

}
