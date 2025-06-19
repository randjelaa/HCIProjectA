# 🐾 VetClinic – Veterinary Clinic Information System

This is a desktop WPF application designed for managing operations in a veterinary clinic. It includes role-based functionality for administrators and veterinarians, with shared features such as language settings and personal profile management.

---

## 📥 Getting Started

![Main Screenshot](./Screenshots/main.PNG)

This is the startup window. You can change the language using the selector in the upper-right corner. The application requires login credentials. Users are assigned one of the following roles:

* **Administrator**
* **Veterinarian**

Upon login, users are redirected to their appropriate dashboard based on their role.

![Main Login Screenshot](./Screenshots/main_login.PNG)

---

## 👤 Administrator Functionalities

### 🔐 User Management

![User Management Screenshot](./Screenshots/manage_users.PNG)

Manage clinic staff such as veterinarians.
You can:

* Add new users

![Main Screenshot](./Screenshots/manage_users_add.PNG)
  
* Search users

![Main Screenshot](./Screenshots/manage_users_search.PNG)
  
* Soft-delete users (mark as inactive)

![User Management Screenshot](./Screenshots/manage_users_delete.PNG)

---

### 💼 Service Management

![Main Screenshot](./Screenshots/manage_services.PNG)

Maintain the list of services offered at the clinic.
You can:

* Add services

![Main Screenshot](./Screenshots/manage_services_add.PNG)

* Edit services

![Main Screenshot](./Screenshots/manage_services_edit.PNG)

* Soft delete services (mark as inactive)

![Main Screenshot](./Screenshots/manage_services_delete.PNG)

* Search services using the search bar

![Service Management Screenshot](./Screenshots/manage_services_search.PNG)

---

### 📊 Reports and Financial Overview

![Main Screenshot](./Screenshots/reports_1.PNG)

![Main Screenshot](./Screenshots/reports_2.PNG)

Get insights into clinic performance.
Features include:

* Table of completed payments
* List of unpaid services with user details
* Line chart of income over time
* Pie chart of most popular services

You can see further user details by pressing the button:

![Main Screenshot](./Screenshots/reports_user_details.PNG)

---

## 🩺 Veterinarian Functionalities

### 📅 Appointment Management

View and manage active appointments — appointments that have not yet been completed (i.e., without a medical record):
* Upcoming appointments
* Missed appointments (no medical record added)

![Main Screenshot](./Screenshots/active_appointments.PNG)

Veterianrian can:
* View appointment where he can also add a record (Diagnosis, Treatment, Medications, Notes)

![Main Screenshot](./Screenshots/active_appointments_view.PNG)

* Cancel appointments if necessary - soft delete them (mark as inactive)

![Main Screenshot](./Screenshots/active_appointments_delete.PNG)

Appointments are filtered by date in ascending order.




View all closed appointments - appointments with a record.

![Main Screenshot](./Screenshots/closed_appointments.PNG)

Veterianarian can:
* Filter closed appointments by date:

![Main Screenshot](./Screenshots/closed_appointments_filter.PNG)

* View details of the appointment:

![Main Screenshot](./Screenshots/closed_appointments_view.PNG)

---

### Pets

![Main Screenshot](./Screenshots/pets.PNG)

Veterinarian can see all the pets and their owners, and also by clicking on a button he can see pet's medical records and filter them by dates from and to:

![Main Screenshot](./Screenshots/pets_medical_records.PNG)

He can also search the pets by their name or the owner's name:

![Main Screenshot](./Screenshots/pets_search.PNG)

## ⚙️ Shared Settings (All Users)

### 🌍 Language & Profile Settings

![Settings Screenshot](./Screenshots/settings.PNG)

Each user can:

* Change the application language (English, Serbian - Srpski)
* Update personal information (name, email, password)

Changes take effect after clicking **Save Changes**.

---

## 🔐 Logout Confirmation

When logging out, a confirmation dialog appears to prevent accidental logout.

![Logout Confirmation Screenshot](./Screenshots/logout.PNG)

## Success Confirmation

Each action of the user shows a message of succession, for example editing the profile:

![Settings Screenshot](./Screenshots/settings_message.PNG)

---

## 📝 Technologies Used

* **WPF (.NET)**
* **MVVM Architecture**
* **Entity Framework Core**
* **MySQL**
* **LiveCharts for graphs**
* **Material Design in XAML Toolkit**

---

## 📎 Notes

* All deletions are *soft deletes* to preserve data integrity. That means that the data still exists in the database, it is just not shown in the application.
* Application supports future expansion (e.g., reception roles, pet grooming modules) and also adding new localizations.
* All images referenced here should be located in the `Screenshots/` folder at the root of the repository.

