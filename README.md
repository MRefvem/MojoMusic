# Ecommerce-App: Mojo Music
Authors: Michael Refvem, Yasir Mohamud
Contributors: Amanda Iverson, Andrew Casper, Kyungrae Kim, Andrew Smith, Paul Rest, Nicco Ryan, Bryant Davis

# Link to the Deployed Site
https://mojomusic.azurewebsites.net/

# Introduction 
This Ecommerce web app displays information about products as well as a home page. The theme of the site is "Mojo Music" and displays various musical instruments and studio products for sale. It has the ability to allow a user to view products, view information about a single product. Users can register, login and logout of their accounts.
The first time the seeded Administrator is registered, they are assigned the role of Admin and given claims that allows them special access to different areas of the site, including an Admin portal that is used to allow that person the ability to add photos to Azure Blob.
User Claims that are gathered for every user include First Name, Last Name, Username, Email and Password. These claims do not provide special access to any areas of the site unless that user is also an Admin. These user claims are captured within the RoleInitializer.cs class.

# Getting Started
To get started using this app:
1.	Install the latest version of Microsoft Visual Studio
2.	Clone the Git repository into your local machine: https://mkrefvem@dev.azure.com/mkrefvem/Ecommerce/_git/Ecommerce-App
3.	Open Ecommerce-App.sln
4.	Run the latest build

# Build and Test
The app should simply be able to run through Microsoft Visual Studio. 

# How to login as an Admin and access the Admin Panel
- Login with these credentials: UserName = "admin@gmail.com", Password = "@Test123!"
- While logged-in as the Admin, you will see a link reading "Admin Panel" on the Nav Bar which leads to a dashboard which gives the Admin access to special operations on the site including the ability to upload product pictures to the Azure Blob.

# Change-Log

19 Aug 2020
1.2 Sprint 2 - Milestone #2 Complete
User Story 1
- *As a user, I would like a way to store the items I wish to purchase in a cart within the application.*
- *Created new tables, Cart and CartItems with all appropriate Models, Services, Interfaces, and created the ability to perform all CRUD operations on these entities.*
User Story 2
- *As a user, I would like the ability to view my desired purchases while browsing the other products on the site.*
User Story 3
- *As a user, I would like a dedicated page where I can view all the products I wish to purchase all in one location.*

18 Aug 2020
1.1 Sprint 2 - Milestone #1 Complete
User Story 1
- *As a user, I would like to receive a notification after I register for an account on the site.*
- *Added SendGrid functionality to register route so that after registration an email gets sent to the user.*
User Story 2
- *As a user, I would like to view specific product details.*
- *On the browse page that shows all of the products, the user can click on a specific product and be redirected to an individual page with the product details.*

17 Aug 2020 - SPRINT 1 Complete
1.0 Sprint 1
- *README & Documentation: README contains an introduction to the web application. README contains all required questions as well as link to the deployed site. All contributors are referenced and cited within the README.*
- *MVC Skeleton & Scaffold: Site contains basic MVC Scaffold including a DBContext with basic products seeded into database. Site implements the repository design pattern with appropriate interfaces and services. Dependency injection is properly registered in Startup.cs file.*
- *Home Page: Introduction to the site with HTML/CSS. Anonymous accessibility. Home page contains login/register links as well as custom greeting for logged-in users.*
- *Login Page: Login page consists of username and password form with password masked for visibility. Login page redirect to home page after successful login. Login page access is anonymous allowed. Login page utilizes ASP.NET Core Identity.*
- *Registration Page: Registration page consists of a form that successfully creates a new account while utilizing ASP.NET Core Identity. Custom claims are properly captured within registration. User is automatically logged in and redirects user to home page after registration. Registration is accessible by anonymous users.*
- *Products Page: Shopping page is accessible by anonymous users as well as fully displays all products existing in the database. Each product is evenly displayed on the page and shopping page has a clean professional appearance.*
- *Azure Blob: All images are stored in Azure. Admin panel exists where the admin can create new products.*
- *User Experience: HTML/CSS is present across the site. Site is clean and professional looking. Pages are properly linked and the overall experience and flow of the site is usable. Site does not contain and unhandled exceptions.*
- *Azure DevOps Process: Azure DevOps tool is properly maintained. User stories assigned to each member are properly filled out, including time estimation, tasks, branches, description, and acceptance tests.*
- *Industry Standard: Summary Comments are present and completed. Methods and variable names are appropriate. Fundamentals are properly used. No misuse of code or industry standards present.*

16 Aug 2020
0.4 Sprint 1 - Milestone #2 Complete
User Story 1
- *As a User, I would like the images of my products to be stored securely and external to the project*
- *Added the ability to store product images securely in the cloud, utilizing Microsoft Azure Blob Storage*
User Story 2
- *As a user, I would like the home page to showcase and reflect the products that we are selling.*
- *Added HTML and CSS to the Site*
- *Home page features some featured products from the Database*
User Story 3
- *As a user, I would like to be able to easily login and register for the site from the home page so that the user can quickly get started*
- *Added the ability to securely login and register to the site*
User Story 4
- *As a user, I would like to have an administrative role available within the application*
- *Administrator given special role and claims to allow them access secret areas of the site*

12 Aug 2020
0.3 Sprint 1 - Milestone #1 Complete
User Story 1 
- *Ability for the user to register for an account on the site, so that they can have a personalized experience*
- *Added identity for the MVC project*
- *Created a Register page*
User Story 2
- *Ability for the user to securely login to their account so that they can have a personalized experienced on the site*
- *Created a page for users to log in. Added a new Razor page*
User Story 3
- *Ability for the user to see products available for sale so that customers can browse through the inventory for purchase*
- *Set up a database, StoreDbContext. Registered in the Startup.cs file. Implemented repository design pattern.*

11 Aug 2020
0.2 *Refactored code base so that Products.cs can be read as a generic class. Created new class, Cereal.cs, that Products.cs then inherits from. Refactored services and controllers according to these changes. Bug fixes: navigating between routes after performing CRUD operations no longer breaks the site. Sort method free of bugs.*

10 Aug 2020
0.1
- *Views, models and controllers implemented. Site functionality: ability to list products and all info, see information on a single product and sort alphabetically and reverse-alphabetically. Basic front end implemented using Bootstrap.*
- *Commit 62d9bfd0: Initial commit. File tree established, added .gitignore, README.md. Data set, cereal.csv. Directories, Controllers, Models, Views*