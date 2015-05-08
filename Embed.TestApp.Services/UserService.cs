using Embed.TestApp.Model;
using Embed.TestApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.TestApp.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private IEmbedContext _context { get; set; }

        public UserService(IEmbedContext context)
            : base(context)
        {
            
        }

        public void Delete(User user)
        {
            EmbedContext.Delete(user);
        }
    }
}
