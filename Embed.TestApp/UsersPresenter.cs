using Embed.TestApp.Model.Entities;
using Embed.TestApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Embed.TestApp
{
    public class UsersPresenter
    {
        private IUserService userService;
        private IUnitOfWork unitOfWork;

        private IUsersView _view { get; set; }
        private IUserService _service { get; set; }
        private IUnitOfWork _unitOfWork { get; set; }
        private IList<User> _users { get; set; }


        public UsersPresenter(IUsersView view, IUserService userService, IUnitOfWork unitOfWork)
        {
            _service = userService;
            _view = view;
            _unitOfWork = unitOfWork;

            view.AddUser += AddUser;
            view.DeleteUser += DeleteUser;
            view.SaveOrUpdateUser += SaveOrUpdateUser;
            view.SelectionChanged += SelectionChanged;
            LoadUsers();
        }

        public void SelectionChanged(object sender, EventArgs e)
        {
            var user = _users.SingleOrDefault(u => u.Id == _view.Id);
            if (user != null)
            {
                _view.LoadUser(user);
            }
            else
            {
                _view.LoadUser(new User { Id = 0, DateOfBirth = DateTime.Now.AddYears(-20) });
            }
        }

        public async void SaveOrUpdateUser(object sender, EventArgs e)
        {
            var user = _view.GetUser();


            var results = new List<ValidationResult>();
            var context = new ValidationContext(user, null, null);

            if (!Validator.TryValidateObject(user, context, results, true))
            {
                var validationMessage = String.Join("\n", results.Select(t => String.Format("- {0}", t.ErrorMessage)));

                _view.ShowMessage("Validation Error", validationMessage);
                return;
            }

            var isUpdated = false;
            if (user.Id > 0){
                isUpdated = true;
                if (_service.QuerySet.SingleOrDefault(u => u.Id != user.Id && u.UserName == user.UserName) == null)
                {
                    var userToUpdate = _service.QuerySet.FirstOrDefault(u => u.Id == user.Id);
                    if (userToUpdate != null)
                    {
                        userToUpdate.UserName = user.UserName;
                        userToUpdate.FirstName = user.FirstName;
                        userToUpdate.LastName = user.LastName;
                        userToUpdate.DateOfBirth = user.DateOfBirth;
                        _service.Update(userToUpdate);
                        user = userToUpdate;
                    }
                    else
                    {
                        _view.ShowMessage("Error", "Failed to update the user");
                        return;
                    }

                }
                else
                {
                    _view.ShowMessage("Error", "A user already exist the name" + user.UserName);
                    return;
                }
            }
            else
            {
                if (_service.QuerySet.SingleOrDefault(u => u.UserName == user.UserName) == null)
                {
                    _service.Add(user);
                }
                else
                {
                    _view.ShowMessage("Error", "A user already exist the name" + user.UserName);
                    return;
                }
            }
            await _unitOfWork.CommitAsync();
            if (isUpdated)
            {
                _users.Remove(_users.First(u => user.Id == u.Id));
            }

            _users.Add(user);
            LoadUsers();
        }

        public async void DeleteUser(object sender, EventArgs e)
        {
            var user = _view.GetUser();
            var userToDelete = await _service.GetByIdAsync(user.Id);
            if (userToDelete != null)
                _service.Delete(userToDelete);
            await _unitOfWork.CommitAsync();
            _users.Remove(_users.First(u => user.Id == u.Id));
            LoadUsers();
        }

        public void AddUser(object sender, EventArgs e)
        {
            _view.LoadUser(new User { Id = 0, DateOfBirth = DateTime.Now.AddYears(-20) });
        }

        public void LoadUsers()
        {
            if (_users == null)
            {
                _users = _service.QuerySet.ToList();
            }
            _view.LoadUsers(_users);
        }
    }
    
}
