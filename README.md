# MoneyFlowApplication

An application that fetches data about one's imaginary monthly bank statements from an external API. Parse data to calculate monthly balance with income and spending that is sent to an external API.

External api(get) for list of transactions\
(External api requires authentication for it to work)\
https://api.sandbox.treasuryprime.com/transaction

The application laverage on the Microsoft dotnet core stack\
Aspnet core 6\
React\
C#

Requirements for installation\
Visual Studio Code\
Dotnet core 6

How to install the application\
1. Clone the application on your machine
2. Change directory to the root of the clone project
3. Run dotnet restore
4. Run dotnet build
5. Run dotnet run -p MoneyFlowApplication

Publishing\
1. Run dotnet publish --configuration Release -p MoneyFlowApplication

Deploy to azure (Deploying to Azure with GitHub Actions tutorials for clarity)\
https://www.youtube.com/watch?v=FeSMRFkaRIU
