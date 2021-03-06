# FinalCapstone: LawnPros

## Introduction:
This project is my individual capstone for my devCodeCamp software development course.  I've deployed it via AWS Elastic Beanstalk, here: 
http://lawnpros-dev.us-west-2.elasticbeanstalk.com/
(Still working through responsiveness on phones and tablets).

## What it is: 
LawnPros is a web application for a fictious landscape company that allows customers to enter some basic information and start a landscape project.  Customers can then make and cancel appointments via a customer portal.  On the employee side, salespeople can view customers, appointments, and projects.  Some cool features I wanted to implement were: a calendar UI for the salesperson, data visualization with Chart.JS, and SMS notifications with Twilio's text API.  I utilized Identity for user registration and login.  

## Technologies used:
* ASP.NET Core MVC
* HTML5 / CSS3
* JavaScript
* LINQ / Entity Framework
* Chart.JS for visualization
* Twilio API for SMS notification
* Google Maps and Geocoding API

## Project status:
I'm currently in the process of refactoring the project to better implement best practices.  Some to-do's include: (1) implementing inheritance for some of the functionality that my salesperson and customer model's share, (2) finalizing my calendar UI and integrating it fully into the rest of my workflow, (3) making the application responsive, (4) implementing unit testing, (5), deploying the application on a live server.

## Contact:
Please reach of to me on LinkedIn https://www.linkedin.com/in/lewis-vine/ or e-mail me @ lew.vine@gmail.com if you have any questions!

## Images: 

Salesperson Calendar UI | Salesperson Home
---------------- | -----------------------
![appointments_dashboard](https://user-images.githubusercontent.com/39601384/118505385-5ccdc300-b6fa-11eb-9df2-7e9000b0c145.PNG) | ![salesperson_portal](https://user-images.githubusercontent.com/39601384/118504944-f5177800-b6f9-11eb-8769-ae9a535a68e1.PNG)
Customer Landing | Project Details
---------------- | ---------------
![homepage](https://user-images.githubusercontent.com/39601384/118506707-8e935980-b6fb-11eb-8010-20d13c4160c8.PNG) |![project_details](https://user-images.githubusercontent.com/39601384/118506749-96eb9480-b6fb-11eb-84db-003635d825fe.PNG)
