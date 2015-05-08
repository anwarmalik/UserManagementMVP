using Embed.TestApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.TestApp
{
    public interface IUsersView
    {
        event EventHandler AddUser;
        event EventHandler SaveOrUpdateUser;
        event EventHandler DeleteUser;
        event EventHandler SelectionChanged;

        int Id { get; set; }
        string UserName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime DateOfBirth { get; set; }

        void ShowMessage(string title, string message);
        void ClearUserForm();
        void LoadUsers(IEnumerable<User> users);
        void LoadUser(User user);
        User GetUser();

    }
}
