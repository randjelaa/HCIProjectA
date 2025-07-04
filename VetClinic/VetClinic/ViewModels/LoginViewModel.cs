﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VetClinic.Models; // Namespace where VetClinicContext is

public class LoginViewModel : INotifyPropertyChanged
{
    private string _email;
    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(nameof(Email)); }
    }

    public string Password { get; set; }

    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new RelayCommand(ExecuteLogin);
    }

    public User LoggedInUser { get; private set; }

    private void ExecuteLogin(object parameter)
    {
        using var db = new VetClinicContext();
        var user = db.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Email == Email && u.Password == Password);

        if (user != null)
        {
            LoggedInUser = user;

            if (parameter is Window loginWindow)
            {
                loginWindow.DialogResult = true; // triggers ShowDialog() to return true
            }
        }
        else
        {
            MessageBox.Show("Invalid email or password.");
        }
    }



    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
