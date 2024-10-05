create database Hosp_Mngmt

create table patient (
    patientid int identity(1,1) primary key,
    firstname varchar(50),
    lastname varchar(50),
    dateofbirth date,
    gender varchar(10),
    contactnumber varchar(15),
    address varchar(150)
)
create table doctor (
    doctorid int identity(101,1) primary key,
    firstname varchar(50),
    lastname varchar(50),
    specialization varchar(100),
    contactnumber varchar(15)
)

create table appointment (
    appointmentid int identity(1,1) primary key,
    patientid int,
    doctorid int,
    appointmentdate date,
    description text,
    constraint fk_patient foreign key (patientid) references patient(patientid),
    constraint fk_doctor foreign key (doctorid) references doctor(doctorid)
)

insert into patient (firstname, lastname, dateofbirth, gender, contactnumber, address)
values ('Raj', 'Kumar', '1985-03-15', 'Male', '9876543210', '123 MG Road, Delhi'),
('Priya', 'Sharma', '1990-07-22', 'Female', '8765432109', '456 Park Street, Mumbai')

insert into doctor (firstname, lastname, specialization, contactnumber)
values ('Anjali', 'Verma', 'Cardiology', '9988776655'),
 ('Vikas', 'Patel', 'Dermatology', '8877665544')
 
 insert into appointment (patientid, doctorid, appointmentdate, description)
values (1, 101, '2024-10-10', 'Routine cardiovascular health checkup.'),
(2, 102, '2024-10-12', 'Consultation for skin irritation.')

select*from patient
select*from doctor
select*from appointment
