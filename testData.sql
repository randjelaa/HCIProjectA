-- Use the database
USE VetClinic;

-- Insert Roles
INSERT INTO Roles (Name) VALUES
('admin'), ('vet'), ('customer');

-- Insert Users
INSERT INTO Users (Name, Email, Password, RoleID) VALUES
('Alice Johnson', 'alice.johnson@example.com', 'hashedpassword1', 1),  -- Admin
('Dr. James Smith', 'j.smith@vetclinic.com', 'hashedpassword2', 2),    -- Vet
('Dr. Laura Kim', 'l.kim@vetclinic.com', 'hashedpassword3', 2),        -- Vet
('Emma Brown', 'emma.brown@example.com', 'hashedpassword4', 3),        -- Customer
('Michael Green', 'michael.green@example.com', 'hashedpassword5', 3),  -- Customer
('Sophia Lee', 'sophia.lee@example.com', 'hashedpassword6', 3),        -- Customer
('Liam Walker', 'liam.walker@example.com', 'hashedpassword7', 3),
('Olivia Harris', 'olivia.harris@example.com', 'hashedpassword8', 3),
('Noah Young', 'noah.young@example.com', 'hashedpassword9', 3),
('Ava Scott', 'ava.scott@example.com', 'hashedpassword10', 3),
('Elijah King', 'elijah.king@example.com', 'hashedpassword11', 3),
('Isabella Adams', 'isabella.adams@example.com', 'hashedpassword12', 3);

-- Insert User Preferences
INSERT INTO UserPreferences (UserID, Theme, Language) VALUES
(1, 'Light', 'en'),
(2, 'Dark', 'en'),
(3, 'Dark', 'en'),
(4, 'Light', 'en'),
(5, 'Light', 'en'),
(6, 'Dark', 'en');

-- Insert Services
INSERT INTO Services (Name, Description, Price, DurationMinutes) VALUES
('General Checkup', 'Routine health check for pets.', 50.00, 30),
('Vaccination', 'Rabies and core vaccines.', 40.00, 20),
('Dental Cleaning', 'Professional dental cleaning.', 100.00, 45),
('Surgery Consultation', 'Pre-surgery consultation and assessment.', 80.00, 60);

-- Insert Pets (owned by customers)
INSERT INTO Pets (Name, Species, Breed, OwnerID) VALUES
('Bella', 'Dog', 'Labrador Retriever', 4),
('Milo', 'Cat', 'Siamese', 5),
('Coco', 'Dog', 'Poodle', 6),
('Max', 'Dog', 'Beagle', 7),
('Luna', 'Cat', 'Maine Coon', 8),
('Charlie', 'Rabbit', 'Lop', 9),
('Daisy', 'Dog', 'German Shepherd', 10),
('Oscar', 'Parrot', 'African Grey', 11),
('Lily', 'Cat', 'British Shorthair', 12);

-- Insert Appointments
INSERT INTO Appointments (Date, PetID, VetID, ServiceID) VALUES
('2025-06-01 10:00:00', 1, 2, 1),  -- Bella, Dr. Smith, Checkup
('2025-06-02 11:00:00', 2, 2, 2),  -- Milo, Dr. Smith, Vaccination
('2025-06-03 09:00:00', 3, 3, 3),  -- Coco, Dr. Kim, Dental
('2025-06-04 14:00:00', 1, 3, 4);  -- Bella, Dr. Kim, Surgery Consult

-- Past (with records)
INSERT INTO Appointments (Date, PetID, VetID, ServiceID) VALUES
('2025-05-10 09:00:00', 4, 2, 1),  -- Max
('2025-05-15 10:30:00', 5, 2, 2),  -- Luna
('2025-05-20 13:00:00', 6, 3, 3);  -- Charlie

-- Missed (past without records)
INSERT INTO Appointments (Date, PetID, VetID, ServiceID) VALUES
('2025-05-25 09:30:00', 7, 3, 4),  -- Daisy
('2025-05-28 15:00:00', 8, 2, 1);  -- Oscar

-- Upcoming
INSERT INTO Appointments (Date, PetID, VetID, ServiceID) VALUES
('2025-06-20 11:00:00', 9, 3, 2),  -- Lily
('2025-06-22 14:00:00', 1, 2, 1),  -- Bella again
('2025-06-25 09:00:00', 2, 3, 3),  -- Milo again
('2025-06-26 16:00:00', 3, 2, 4),  -- Coco again
('2025-06-30 10:00:00', 4, 3, 1);  -- Max again


-- Insert Medical Records (for some appointments)
INSERT INTO MedicalRecords (AppointmentID, Diagnosis, Treatment, Medications, Notes) VALUES
(1, 'Healthy', 'No treatment needed', '', 'Very active dog.'),
(2, 'Routine vaccination', 'Rabies shot', 'RabVac', 'Next vaccine due in 1 year'),
(3, 'Mild tartar buildup', 'Dental cleaning', 'None', 'Recommended brushing weekly'),
(5, 'Normal check', 'None', '', 'Healthy.'),
(6, 'Vaccination complete', 'Rabies + FVRCP', 'RabVac, FelinePlus', 'Next due in 12 months'),
(7, 'Tooth trimming', 'Manual trim', '', 'Stress managed well.');

-- Insert Payments (for some appointments)
INSERT INTO Payments (Amount, Date, UserID, AppointmentID) VALUES
(50.00, '2025-06-01 10:30:00', 4, 1), -- Emma paid for Bella's checkup
(40.00, '2025-06-02 11:15:00', 5, 2); -- Michael paid for Milo's vaccination

-- Insert Unpaid Services (for unpaid appointment)
INSERT INTO UnpaidServices (UserID, AppointmentID, Amount, Status) VALUES
(6, 3, 100.00, 'pending'),  -- Sophia hasn't paid for Coco's dental
(4, 4, 80.00, 'overdue');   -- Emma hasn't paid for Bella's surgery consult

-- Payments for appointments with records
INSERT INTO Payments (Amount, Date, UserID, AppointmentID) VALUES
(50.00, '2025-05-10 09:30:00', 7, 5),
(40.00, '2025-05-15 10:45:00', 8, 6),
(100.00, '2025-05-20 13:30:00', 9, 7);

-- Missed appointments (no record, unpaid)
INSERT INTO UnpaidServices (UserID, AppointmentID, Amount, Status) VALUES
(10, 8, 80.00, 'pending'),  -- Daisy
(11, 9, 50.00, 'overdue');  -- Oscar

-- One future unpaid upfront appointment
INSERT INTO UnpaidServices (UserID, AppointmentID, Amount, Status) VALUES
(12, 10, 40.00, 'pending'); -- Lily

