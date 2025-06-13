using System.Collections.ObjectModel;
using VetClinic.Models;

public class AddUserViewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Role? SelectedRole { get; set; }
    public ObservableCollection<Role> Roles { get; }

    public AddUserViewModel()
    {
        using var db = new VetClinicContext();
        Roles = new ObservableCollection<Role>(db.Roles.ToList());
    }
}
