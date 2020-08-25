# Ecommerce-App: Mojo Music
Authors: Michael Refvem, Yasir Mohamud
Contributors: Amanda Iverson, Andrew Casper, Kyungrae Kim, Andrew Smith, Paul Rest, Nicco Ryan, Bryant Davis, Na'ama Bar-Ilan, Peyton Cysewski

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
- While logged-in as the Admin, you will see a link reading "Admin Panel" on the Nav Bar which leads to a dashboard which gives the Admin access to special operations on the site including the ability to see, create, update and delete products as well as upload product pictures to an Azure Blob Storage account.

# A description of the Products Database Schema
The products database contains the following tables: dbo.CartItems, dbo.Carts, dbo.Order, dbo.OrderAddress and dbo.Products. The dbo.Products table lists all of the products in the database and includes their properties such as Id, Name, SKU, Price, Description and Image URL.
The dbo.Carts and dbo.Order tables are join tables that link a specific user to their Cart Items and Order Address, respectively. The dbo.Carts table contains the Id of the cart, the UserEmail associated with the user and a boolean describing the current status of the cart ("IsActive" when set to true means it is active, and when set to false means it is one of the user's past purchases). You can use the dbo.Carts table to link to the dbo.CartItems table which simply links a CartId with a ProductId and then gives the quantity of the item present in the cart correlating with the given product Id.
Much in the way the the dbo.Carts table links to the dbo.CartItems table, so does the dbo.Order table with the dbo.OrderAddress table. The dbo.OrderTable links an Order Address to a specific Cart (which is important as long as you want a specified shipment to reach the correct address!). The dbo.OrderAddress table contains a primary key to identify a unique order address in the database, as well as all of the practical information necessary to ship an item to a customer such as the customer's FirstName, LastName, Address, City, State and ZIP.

# Change-Log

24 Aug 2020 - SPRINT 2 Complete
2.0 Sprint 1
- *README: README contains the following: A link to the deployed website, a description of the products the site is selling, a description of the claims that are captured upon account registration, a description of the policies that are being reinforced, a description of the structure of the products database schema, a description of the DB Schema (particularly the basket/order tables)*
- *Mini Cart: Mini Cart exists on the product landing page. Mini basket holds a current view of all the existing items the user has in their cart. A view component is used to implement the mini cart across the site.*
- *Cart: A dedicated cart page that consists of all the user's current products. Items in the cart can be modified in quantity or removed. Home page consists of a link to the cart for easability (present in the Nav Bar on all pages). Cart page contains a "Checkout" button that redirects to the Checkout page. The user must be logged in to access the cart page.*
- *Receipt Page: Receipt page consists of an itemized list of all products the user has "purchased". Includes the item name, quantity and total price of each item as well as total price of all items in the basket.*
- *Email Sender: A welcome email is sent to the user upon a new registration. A receipt email is also sent to the user after the "checkout" process from the basket page. Each email is fitting to the problem domain, and formatted professionally. The email consists of a custom greeting and personalization.*
- *Admin Dashboard: Admin Dashboard exists with a landing page directing users to Products Controller. An admin can execute full CRUD operations on the products. Only users with a role of "Admin" can access this page.*
- *User Experience: HTML/CSS is present across the site. Site is clean and professional looking. Pages are properly linked and the overall experience and flow of the site is usable. Site does not contain any unhandled exceptions.*
- *README & Documentation: README contains an introduction to the web application. Site contains all required questions as well as a link to the deployed site. All contributors are referenced and cite within the README.*
1.6 Sprint 1 resubmission criteria addressed (Reviewer: Please regrade/reevaluate this project from Sprint 1 as the following feedback was addressed)
- *Added documentation for how the Admin portal can be accessed to the README*
- *Admin panel now exists where an admin can perform full CRUD operations on products.*
- *Access permitted for admin access on admin panel*
- *User experience: clicking on "Mojo Music" nav element now returns the user to the home page*
- *User experience: Nav bar now hides the login/register tab even after registered and logged in*
- *Summary comments added and present for all logical methods*

23 Aug 2020
1.5 Sprint - Milestone #3 Complete
User Story 2
- *As a user, I would like to see a summary of my purchase after completing my checkout process*
- *Added a receipt page for the user to see a summary of their order after having made a purchase*
User Story 3
- *As a user, I would like a summary of my purchase to be emailed to me so that I can store the receipt for my records*
- *Added SendGrind email service to the method that authorizes a user's purchase, complete with the user's purchase information.*

22 Aug 2020
1.3 Sprint 2 - Milestone #3
User Story 1
- *As a user, I would like to checkout with my purchases using electronic payment options on my site during the checkout process*
- *Implemented a Payment Service to accept credit card transactions into the site*

20 Aug 2020
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
