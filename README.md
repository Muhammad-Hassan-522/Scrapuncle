# Scrapuncle

Scrapuncle is a web-based scrap pickup management platform designed to connect customers, scrap dealers and administrators. It provides a simple and functional solution for scheduling, managing, and completing scrap pickups.

## 🚀 Features

### 👤 User Features
- Register and login with role selection (User or Dealer)
- Schedule scrap pickups (select date, time, address, pincode, estimated weight)
- View scheduled and completed pickups
- View scrap rates

### 🔧 Dealer Features
- Login and access personalized dealer dashboard
- View recent and all assigned pickups with details
- Update profile including phone and serviceable pincodes
- View earnings based on completed pickups

### 🛠️ Admin Features
- Admin dashboard overview
- Manage all pickups across the system
- Add, edit, or delete scrap item rates

## 🏗️ Technologies Used

- **Frontend:** ASP.NET MVC with Razor Views, HTML/CSS, Bootstrap
- **Backend:** C#, .NET Framework
- **Database:** SQL Server
- **Authentication:** ASP.NET Core Identity
- **Design Pattern:** MVC Architecture

## 🧑‍💻 Roles

- **User:** Can schedule and track pickups.
- **Dealer:** Handles assigned pickups and can manage their profile.
- **Admin:** Manages pickups and scrap items.

## 🛠️ Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/Muhammad-Hassan-522/Scrapuncle.git
   cd Scrapuncle

2. **Configure the Database**
- Update appsettings.json or your connection string in Startup.cs or Web.config.
- Apply EF migrations or run SQL scripts to generate tables.

3. **Run the Project**
- Open in Visual Studio
- Build and Run the solution

## 🔐 Authentication & Identity

- Integrated ASP.NET Core Identity
- Register/Login functionality with role support

 ## 🙌 Contributors
 - Muhammad Hassan
