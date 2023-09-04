# PubPlusProject
This system manages the work statuses of employees after they log in.
Every user can change their work status and see the statuses of other employees. 
The system allows filtering multiple statuses from a list. Additionally, the system enables text filtering on each column, including choosing the operator based on the field type.

##Insall .NET
To get started with ASP.NET Core in .NET 5.0 install the .NET 5.0 SDK.
https://dotnet.microsoft.com/en-us/download/dotnet/5.0

##Install VS
If you’re on Windows using Visual Studio, install Visual Studio 2019.
https://visualstudio.microsoft.com/vs/older-downloads/

If you’re on macOS, install Visual Studio 2019 for Mac 8.6.
https://docs.microsoft.com/visualstudio/releasenotes/vs2019-mac-preview-relnotes

After installation, you should open the PubPlusProject.sln file using Visual Studio.

##Install database:

Run the PubPlus.bak file in your local MSSQL.
In the appsettings.json file (located in the PubPlusProject folder), replace the DataSource with your computer's local data by replacing the term YOUR-MSSQL-DATASOURCE-HERE within the Database.
