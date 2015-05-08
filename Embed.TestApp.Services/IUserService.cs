using Embed.TestApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.TestApp.Services
{
    public interface IUserService : IService<User>
    {
        void Delete(User user);
    }
}
