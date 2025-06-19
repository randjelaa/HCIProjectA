# 🐾 VetClinic – Veterinary Clinic Information System

This is a desktop WPF application designed for managing operations in a veterinary clinic. It includes role-based functionality for administrators and veterinarians, with shared features such as language settings and personal profile management.

---

## 📥 Getting Started

![Main Screenshot](./Screenshots/main.PNG)

This is the startup window. You can change the language using the selector in the upper-right corner. The application requires login credentials. Users are assigned one of the following roles:

* **Administrator**
* **Veterinarian**

Upon login, users are redirected to their appropriate dashboard based on their role.

![Login Screenshot](./Screenshots/main_login.PNG)

---

## 👤 Administrator Functionalities

### 🔐 User Management

![User Management Screenshot](./Screenshots/manage_users.PNG)

Manage clinic staff such as veterinarians. You can:

* Add new users:

  ![Add User Screenshot](./Screenshots/manage_users_add.PNG)

* Search users:

  ![Search User Screenshot](./Screenshots/manage_users_search.PNG)

* Soft-delete users (mark as inactive):

  ![Delete User Screenshot](./Screenshots/manage_users_delete.PNG)

---

### 💼 Service Management

![Service Management Screenshot](./Screenshots/manage_services.PNG)

Maintain the list of services offered at the clinic. You can:

* Add services:

  ![Add Service Screenshot](./Screenshots/manage_services_add.PNG)

* Edit services:

  ![Edit Service Screenshot](./Screenshots/manage_services_edit.PNG)

* Soft-delete services (mark as inactive):

  ![Delete Service Screenshot](./Screenshots/manage_services_delete.PNG)

* Search services using the search bar:

  ![Search Service Screenshot](./Screenshots/manage_services_search.PNG)

---

### 📊 Reports and Financial Overview

![Reports Screenshot 1](./Screenshots/reports_1.PNG)  
![Reports Screenshot 2](./Screenshots/reports_2.PNG)

Gain insight into clinic performance. Features include:

* Table of completed payments
* List of unpaid services with user details
* Line chart of income over time
* Pie chart of most popular services

To view additional details about a user, click the **User Details** button:

![User Details Screenshot](./Screenshots/reports_user_details.PNG)

---

## 🩺 Veterinarian Functionalities

### 📅 Appointment Management

Veterinarians can view and manage appointments that do not yet have medical records:

* Upcoming appointments
* Missed appointments (i.e., appointments in the past without a record)

![Active Appointments Screenshot](./Screenshots/active_appointments.PNG)

Veterinarians can:

* View appointment details and add a medical record (Diagnosis, Treatment, Medications, Notes):

  ![View Appointment Screenshot](./Screenshots/active_appointments_view.PNG)

* Cancel appointments if necessary (soft delete them):

  ![Delete Appointment Screenshot](./Screenshots/active_appointments_delete.PNG)

Appointments are filtered by ascending date.

---

### ✅ Closed Appointments

View all past appointments that already have an associated medical record.

![Closed Appointments Screenshot](./Screenshots/closed_appointments.PNG)

Veterinarians can:

* Filter closed appointments by date:

  ![Closed Appointments Filter Screenshot](./Screenshots/closed_appointments_filter.PNG)

* View details of any closed appointment:

  ![Closed Appointments View Screenshot](./Screenshots/closed_appointments_view.PNG)

---

### 🐕 Pets Overview

![Pets Screenshot](./Screenshots/pets.PNG)

Veterinarians can:

* See all pets along with their owners
* View medical records for a selected pet and filter them by date:

  ![Pet Medical Records Screenshot](./Screenshots/pets_medical_records.PNG)

* Search for pets by pet name or owner's name:

  ![Search Pets Screenshot](./Screenshots/pets_search.PNG)

---

## ⚙️ Shared Settings (All Users)

### 🌍 Language & Profile Settings

![Settings Screenshot](./Screenshots/settings.PNG)

Each user can:

* Change the application language (English, Serbian – Srpski)
* Update personal information (name, email, password)

Changes take effect after clicking **Save Changes**.

---

## 🔐 Logout Confirmation

When logging out, a confirmation dialog appears to prevent accidental logout.

![Logout Confirmation Screenshot](./Screenshots/logout.PNG)

---

## ✅ Action Confirmation

Each successful user action is followed by a confirmation message. For example, after updating profile information:

![Settings Confirmation Screenshot](./Screenshots/settings_message.PNG)

---

## 🛠 Technologies Used

* **WPF (.NET)**
* **MVVM Architecture**
* **Entity Framework Core**
* **MySQL**
* **LiveCharts for Graphs**
* **Material Design in XAML Toolkit**

---

## 📎 Notes

* All deletions in the system are **soft deletes**, meaning data remains in the database but is hidden from the user interface.
* The application is designed to support future expansion (e.g., reception roles, pet grooming modules) and adding new localizations.

---

> 📸 **Make sure all screenshots referenced in this document are placed inside the `Screenshots/` folder of the repository.**
