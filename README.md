# Park Management System #

## Overview ##

This repository hosts the Park Management System, a comprehensive solution developed as part of the Software development Laboratory (LABDSOF) in our Master’s Degree program at Instituto Superior de Engenharia do Porto (ISEP). This system is designed to provide an improved customer experience for registered users. The system focuses on known customers and learns from their habits to offer them better service every day. Customers who are registered can avoid the hassle of collecting, paying for, and presenting tickets at the exit. The customers just approach the park entrance and enter the park. Likewise, they just approach the exit and leave the park. For each parking visit the customer will receive PARKY “coins” which can be redeemed to pay future parking visits.

## Team Members ##

Special thanks to all the team members whose dedication and expertise were crucial to the development of this system:

* Member 1: Marta Barbosa
* Member 2: Alfredo Garcês
* Member 3: Daniel Lourenço
* Member 4: Luís Miguel Silva
* Member 5: Luís Henrique Silva

## Documentation ##

For more information on Park Management System, please refer to our comprehensive documentation included [here](Documentation.md).

## How to Use and Clone the System ##

### Prerequisites ###

Before you begin, ensure you have the following installed:

Git

* .Net (for running monolith and other simulation microservices)
* Node.js and npm (for the Angular frontends)

### Cloning the Repository ###

`git clone https://github.com/1181479/Park_Management_System.git`

### Run the frontend apps ###

* `cd <frontend app name>` (navigate to frontend dir)  
* `npm install` (install dependencies)
* `npm start`  (start the app)

### Run the Backend apps ###

* `cd <backend app name>` (navigate to backend dir)  
* dotnet run --urls `"http://localhost:5003"` (starts the app with the port 5003)

### Run everything all ###

execute `RunAll.ps1` file.

### Using the app ###

* **Registered Users:** Log in with your credentials to access the system.
* **New Users:** Sign up to create an account and start using the parking services.
* **Pre-Registered Test Users (Username : Password):**
  1. **User:** User1 : password1
  2. **User:** User2 : password2
  3. **User:** User3 : password3
  4. **User:** User4 : password4
  5. **User:** User5 : password5

## Additional Information ##

The features and components presented in this system are part of a simulation client developed for LABDSOFT in our Master's program. Please be aware that not all functionalities may be fully implemented or finished. This project serves as a prototype and an academic exercise, showcasing the potential capabilities and design for a park management system. As such, some aspects of the system are conceptual and may require further development for real-world application.

# Thesis

This repository and [Park Management System GraphQL](https://github.com/1181479/Park_Management_System_GraphQL) were part of a thesis development for a master's degree at ISEP.
[Thesis](./DIMEI_2324.pdf)