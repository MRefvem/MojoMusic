# Ecommerce-App: Mojo Music
Authors: Michael Refvem, Yasir Mohamud

# Introduction 
This Ecommerce web app displays information about products as well as a home page. The theme of the site is "Mojo Music" and displays various musical instruments and studio products for sale. It has the ability to allow a user to view products, view information about a single product. Users can register, login and logout of their accounts. The first time the seeded Administrator is registered, they are assigned the role of Admin and given claims that allows them special access to different areas of the site, include and Admin portal that is used to allow that person the ability to add photos to Azure Blob.

# Getting Started
To get started using this app:
1.	Install the latest version of Microsoft Visual Studio
2.	Clone the Git repository into your local machine: https://mkrefvem@dev.azure.com/mkrefvem/Ecommerce/_git/Ecommerce-App
3.	Open Ecommerce-App.sln
4.	Run the latest build

# Build and Test
The app should simply be able to run through Microsoft Visual Studio. 

# Change-Log

16 Aug 2020
3.0 Sprint 1 - Milestone #2 Complete
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
2.0 Sprint 1 - Milestone #1 Complete
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
1.2 *Refactored code base so that Products.cs can be read as a generic class. Created new class, Cereal.cs, that Products.cs then inherits from. Refactored services and controllers according to these changes. Bug fixes: navigating between routes after performing CRUD operations no longer breaks the site. Sort method free of bugs.*

10 Aug 2020
1.1 *Views, models and controllers implemented. Site functionality: ability to list products and all info, see information on a single product and sort alphabetically and reverse-alphabetically. Basic front end implemented using Bootstrap.*
1.0 *Commit 62d9bfd0: Initial commit. File tree established, added .gitignore, README.md. Data set, cereal.csv. Directories, Controllers, Models, Views*