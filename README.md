
# Passion Project: Bakery Order Management App

## Overview
This project is basically an application which helps the bakeries to manage customer orders efficiently and smoothly. It also helps the customers to avoid standing in a long queue waiting for their bills. 
In this way this application saves a lot of time for the customer and for the managers.

## Inspiration 

I am working in a bustling bakery, constantly filled with customers lining up and staff looking overwhelmed. 
Observing the scene, I noticed that the shop's manager, is highly stressed as he juggles managing handwritten orders. 
He often struggles to keep track of orders, leading to frequent mistakes and unsatisfied customers. 

Witnessing these challenges firsthand inspired me to develop the Bakery Order Management App. 
This application aims to streamline the ordering process, reduce errors, and enhance customer satisfaction, ultimately easing the stress on managers.

## Main  Features
- **Customer Data Management:** Allows to read,write ,update and delete customer information.Also the customer can update their information .
- **Menu Data Management:** Allows to read,write ,update and delete menu information.Also the customers can view different menu.
- **Menu Data Management:** Allows to read,write ,update and delete order information.Also the customers can make orders online.

### Features to Explore in the Future
- **Image Upload Feature:** I would like to add a feature to upload the images of each menu so that customers can easily identify the product.
- **Notification System:** Sends notifications to managers regarding an order confirmations or cancellations.

## Technologies Used
- **Backend:** ASP.NET Web API, Entity Framework
- **Frontend:** HTML, CSS, Bootstrap
- **Database:** Microsoft SQL Server
- **Tools and Libraries:** Visual Studio

## Challenges
One of the significant challenges encountered in the development of the Bakery Order Management App was managing and retrieving data using foreign keys. 
This issue primarily arose due to the interrelated nature of our database tables: Orders, Customers, and Menu.
I solved this issue by ORM Tools: Utilizing Object-Relational Mapping (ORM) tools like Entity Framework simplified the process 

## Database Schema
### Orders
- **OrderID:** Unique identifier for each order
- **CustomerID:** Foreign key referencing the Customer table
- **MenuID:** Foreign key referencing the Menu table
- **OrderDate:** Date and time the order was placed
- **Quantity:** Number of items ordered
- **TotalPrice:** Total price of the order

### Customers
- **CustomerID:** Unique identifier for each customer
- **Name:** Customer's name
- **Email:** Customer's email address
- **Phone:** Customer's phone number
- **Address:** Customer's address
- **RegistrationDate:** Date and time of registration

### Menu
- **MenuID:** Unique identifier for each menu item
- **ItemName:** Name of the menu item
- **Price:** Price of the menu item
## Contact
For any questions or suggestions, feel free to contact at [lyeapaul@gmail.com](mailto:lyeapaul@gmail.com).


