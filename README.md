Great! Below is an updated version of your `README.md` with placeholders for screenshots. You can now insert your screenshots at the appropriate locations.

---

# 🐾 VetClinic – Veterinary Clinic Information System

This is a desktop WPF application designed for managing operations in a veterinary clinic. It includes role-based functionality for administrators and veterinarians, with shared features such as language settings and personal profile management.

---

## 📥 Getting Started

![Main Screenshot](./Screenshots/main.png)

This is the sratup window, you can change the language of this window in the upper right corner. The application requires login credentials. Users are assigned one of the following roles:

* **Administrator**
* **Veterinarian**

Upon login, users are redirected to their appropriate dashboard based on their role.

---

## 👤 Administrator Functionalities

### 🔐 User Management

Manage clinic staff such as veterinarians.
You can:

* Add new users
* Edit user roles
* Soft-delete users (mark as inactive)

![User Management Screenshot](path_to_screenshot_image_here)

---

### 💼 Service Management

Maintain the list of services offered at the clinic.
You can:

* Create, update, or delete services
* Set price and duration
* Filter services using the search bar

![Service Management Screenshot](path_to_screenshot_image_here)

---

### 📊 Reports and Financial Overview

Get insights into clinic performance.
Features include:

* Table of completed payments
* List of unpaid services with user details
* Line chart of income over time
* Pie chart of most popular services

![Reports Screenshot](path_to_screenshot_image_here)

---

## 🩺 Veterinarian Functionalities

### 📅 Appointment Management

View and manage appointments:

* Upcoming appointments
* Missed appointments (no medical record added)
* Cancel appointments if necessary

Appointments are filtered by date.

![Appointments Screenshot](path_to_screenshot_image_here)

---

### 📋 Medical Records

Add detailed records for appointments:

* Diagnosis
* Treatment
* Medications
* Notes

You can also view medical history of pets, filtered by date range.

![Medical Records Screenshot](path_to_screenshot_image_here)

---

## ⚙️ Shared Settings (All Users)

### 🌍 Language & Profile Settings

Each user can:

* Change the application language (e.g., English, Bosnian)
* Update personal information (name, email, password)

Changes take effect after clicking **Save Changes**.

![Settings Screenshot](path_to_screenshot_image_here)

---

## 🔐 Logout Confirmation

When logging out, a confirmation dialog appears to prevent accidental logout.

![Logout Confirmation Screenshot](path_to_screenshot_image_here)

---

## 📝 Technologies Used

* **WPF (.NET)**
* **MVVM Architecture**
* **Entity Framework Core**
* **SQLite (or your DB)**
* **LiveCharts for graphs**
* **Material Design in XAML Toolkit**

---

## 📎 Notes

* All deletions are *soft deletes* to preserve data integrity.
* Application supports future expansion (e.g., reception roles, pet grooming modules).

---

Now, you just need to replace `path_to_screenshot_image_here` with the actual paths to your screenshot images. You can upload these images to the project directory or host them online.

Once you're done adding the screenshots, you can export the document to PDF if needed.

Let me know if you need help with any part of this process!
