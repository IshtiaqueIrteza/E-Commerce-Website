<h2>E-Commerce Website:</h2>

This project is a simple E-Commerce website based on <b>ASP.NET MVC 5</b>

<b>Features:</b>
* User can register, buy products, see his shop history.
* Admin can insert, update, and delete products, view transaction history, top users etc.
* The whole project is based on Entity Framework, Linq, and Lambda expression.
* Also with some basic ADO.Net and <b>Layered Architecture</b>

<b>Language used              : asp.net (MVC)
  
Database                        : Microsoft SQL Server 2012</b>

<h2> How to run the project : </h2>

1. Import the database (ProductDB.bacpac) file to Microsoft SQL Server ar first.
2. In visual studio, add the solution or project.
3. Modify the Connection String as you needed to connect to database. Go to (inside project) :
PMApp/Web.config. At the last portion, you'll find the connection string like below : 

```
<connectionStrings>
    <add name="ProductDBContext" providerName="System.Data.SqlClient" connectionString="data source = .\SQLEXPRESS; initial catalog = ProductDB; user id = SA; password = 1234;"/>
  </connectionStrings>
  ```
  
  * Check on the internet how to write or change Connection Strings, change your <b>data source, user id, password</b> as needed. Or provide <b> Windows Authentication </b>, as your wish.
  
  4. <b>Some word of wisdom : This is a very basic project in ASP.NET. My intention to upload this so that beginners can learn from it, get some idea for their project.</b>
  
      Enjoy !!
