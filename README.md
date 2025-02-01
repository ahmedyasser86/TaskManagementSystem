# Task Management System (CRUD) #

- ### DO THAT BEFORE RUN THE APPLICATION: ###
- 1- In Appsettings.Json: Configure you connectionString
	OR In Program.cs Use the InMemory Storage Insteda of SqlServer
- -------------
- This application is using Onion Arcitcture (Core - Infurstraction - Application - UI(MVC - API))
- There is 2 UI Layers, One fore API and one for MVC(Shouldn't be there)
- -------------

- ### Steps: ###
- Create Class Libraries (Application, Core, Infrastructure)
- Install Needed Packages (For EF core)
- Create Model ()
- Create Db Context => i choose SQL Server, But we choose use any other Storage
- Create Data Repository (TasksRepository) => I Used a simple way, in bigger apps we can create a generic Repository
- Create entity Service (TasksServices) => I didn't use a DTO, in bigger apps we can create a DTO foreach entity
- Create CRUD Controller
- Assign Services in program.cs and Test CRUD Operations
- Add Identity => I used the IdentityUser, we can use a custom ApplicationUser
- Seed Roles and first user (Admin User)
- Create Auth Service
- Create JWT Token Generator Service
- Activate JWT Authentication
- All layers returns same response
- Enable CROS For Angular
- ------ API Done. --------
- ### I HAVE TRIED TO CREATE THE FRONT WITH ANGULAR BUT THE DEADLINE IS SO CLOSE, SO I Couldn't STUDY ANGULAR, BUT I TRIED.. ###
- CREATE MVC APP BESIDE THE API APP => WE CAN MERGE THEM IN ONE APPLICATION IN THE FUTURE
- Create Account Controller and Views (Log in, Sign up)
- Create Tasks Controller And Implement task services (Add, Edit, Delete, Read)
- Add Search To The View


- ToDo:
- Make User who in admin role can put other user to this role, or delete them
- More Validation in registeration to return better error message
