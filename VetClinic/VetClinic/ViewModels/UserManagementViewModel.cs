using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VetClinic.Models;
using Microsoft.EntityFrameworkCore;


public class UserManagementViewModel : BaseViewModel
{
    public ObservableCollection<User> Users { get; set; }

    public ICommand AddUserCommand { get; }
    public ICommand DeleteUserCommand { get; }

    public UserManagementViewModel()
    {
        using var db = new VetClinicContext();
        Users = new ObservableCollection<User>(db.Users.ToList());

        AddUserCommand = new RelayCommand(_ => AddUser());
        DeleteUserCommand = new RelayCommand(userObj => DeleteUser(userObj as User));
        LoadUsers();
    }

    private void AddUser()
    {
       /* var newUser = new User
        {
            Username = "NewUser",
            Email = "email@example.com",
            RoleId = 1 // Default role, or make this configurable
        };

        using var db = new VetClinicDbContext();
        db.Users.Add(newUser);
        db.SaveChanges();

        Users.Add(newUser); */
    }

    private void DeleteUser(User user)
    {
        if (user == null) return;

        using var db = new VetClinicContext();
        var toDelete = db.Users.Find(user.Id);
        if (toDelete == null) return;

        db.Users.Remove(toDelete);
        db.SaveChanges();

        Users.Remove(user);
    }

    private void LoadUsers()
    {
        using var context = new VetClinicContext();
        var usersWithRoles = context.Users
            .Include(u => u.Role)
            .ToList();

        Users = new ObservableCollection<User>(usersWithRoles);
    }
}
