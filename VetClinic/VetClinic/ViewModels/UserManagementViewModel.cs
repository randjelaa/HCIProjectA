using Microsoft.EntityFrameworkCore;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using VetClinic.Models;
using VetClinic.Views.Windows;

public class UserManagementViewModel : BaseViewModel
{
    public ObservableCollection<User> Users { get; set; } = new();
    public ObservableCollection<User> FilteredUsers { get; set; } = new();

    public string SearchText { get; set; }
    public ICommand SearchCommand { get; }
    public ICommand AddUserCommand { get; }
    public ICommand DeleteUserCommand { get; }

    public UserManagementViewModel()
    {
        SearchCommand = new RelayCommand(_ => ApplySearch());
        AddUserCommand = new RelayCommand(_ => AddUser());
        DeleteUserCommand = new RelayCommand(userObj => DeleteUser(userObj as User));
        LoadUsers();
    }

    private void ApplySearch()
    {
        var lower = SearchText?.ToLower() ?? "";
        var filtered = Users.Where(u =>
            (!string.IsNullOrEmpty(u.Name) && u.Name.ToLower().Contains(lower)) ||
            (!string.IsNullOrEmpty(u.Email) && u.Email.ToLower().Contains(lower)) ||
            (!string.IsNullOrEmpty(u.Role?.Name) && u.Role.Name.ToLower().Contains(lower))
        ).ToList();

        FilteredUsers.Clear();
        foreach (var user in filtered)
            FilteredUsers.Add(user);
    }

    private void AddUser()
    {
        var window = new AddUserWindow { Owner = Application.Current.MainWindow };

        if (window.ShowDialog() == true)
        {
            var vm = (AddUserViewModel)window.DataContext;

            using var db = new VetClinicContext();
            var user = new User
            {
                Name = vm.Name,
                Email = vm.Email,
                Password = window.Password,
                RoleId = vm.SelectedRole?.Id
            };

            db.Users.Add(user);
            db.SaveChanges();

            LoadUsers();
        }
    }

    private void DeleteUser(User user)
    {
        if (user == null) return;

        using var db = new VetClinicContext();
        var toDelete = db.Users.Find(user.Id);
        if (toDelete == null) return;

        toDelete.Deleted = DateTime.Now;
        db.SaveChanges();

        Users.Remove(user);
        FilteredUsers.Remove(user);
    }

    private void LoadUsers()
    {
        using var context = new VetClinicContext();
        var usersWithRoles = context.Users
            .Where(u => u.Deleted == null)
            .Include(u => u.Role)
            .ToList();

        Application.Current.Dispatcher.Invoke(() =>
        {
            Users.Clear();
            FilteredUsers.Clear();
            foreach (var user in usersWithRoles)
            {
                Users.Add(user);
                FilteredUsers.Add(user);
            }
        });
    }
}

