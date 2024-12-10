# Task

## API

 * Run "API-Stack-Repository" solution Using VS 
 * Change SQL string in startup with your sql credentials
 * Select "stack.DAL" then Run "Update-database" (to run database migrations) in package manager console and check Database Tables in MSSQL
 * Run the project and it'll run automatically run on `https://localhost:44307/swagger`.
 
   > Initializer()'ll excute automatically to initialize the customers and vehicles data, 
   > you can check the initizalized data in appsettings.json in stack.API .

 **For Deployment purpose**
 * change the sql string server , Origins by provide the deployed UI Link (cross origin policy (cors) for allowed client connections)
 * Build > Publish Api stack.API and continue the setps depend on the deployment environment

## UI 

* Run  **npm i**  in cmd to install node modules
* Run  **ng s**  to run the project on `http://localhost:4200/`

**For Deployment purpose**
 * Change environments apiUrl and hubUrl with deployed API link 
   
   > `Vehicle-Task\vehicles-panel\src\environments`
* Run  **ng build --prod** then you'll find the build files in dist folder
   
   >`Vehicle-Task\dist` 

## Guide 

* Use below credentials to login through the app
    
  For customer view
   > *user name : customer1 , password :P@ssw0rd

   > *user name : customer2 , password :P@ssw0rd
   
   > *user name : customer3 , password :P@ssw0rd
   
  For admin view
    > *user name : admin , password :P@ssw0rd

* Save JWT token in session/local storage depend on checking  **remember me** 
* After login 
  
  *Customer
    
    > user name , address and his vehicles.
    > vehicles'll be updated by random status every 1 min and send "ping" hub to admin to trigger the changes immediately.
   
   *Admin
   
    > All vehicles view with their updated status and last ping time every ping from the vehicle.
    > signalR Connection will start after load vehicles page listening to any "ping" hub to update the list.
   
   And logout for both
     
    
## Database diagram 
   
![Capture](https://user-images.githubusercontent.com/71011105/142763301-a012edfc-d165-4731-ba66-04b73d0b9431.PNG)


 
