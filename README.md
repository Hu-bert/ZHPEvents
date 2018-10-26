# ZHP Events
***
### Table of Contents
[General Information](#general-Information)
[Technologies](#technologies)
[Installation](#installation)
[Features](#features)
[Project status](#project-status)
[Things to do](#things-to-do)
[Sources](#sources)
[Other informations](#other-informations)
***
###### General Information
ZHPEvents is a web application made in C # and ASP.NET Core 2.0. The application is used for publishing, viewing events and posting internal reports in the ZHP organization.
ZHPEvents is created for the Chorągwi Dolnośląskiej ZHP.
ZHPEvents is engineering work.
What is ZHP?
Związek Harcerstwa Polskiego (English: The Polish Scouting and Guiding Association) in brief ZHP is the coeducational Polish Scouting organization.
Wikipedia: [ZHP](https://en.wikipedia.org/wiki/Polish_Scouting_and_Guiding_Association)
***
###### Technologies
Project is created with:
[ASP.NET Core](https://www.microsoft.com/net) v2.0
[Boostrap](https://getbootstrap.com/) v4.1
[SEmentic Ui](https://semantic-ui.com/) v2.4.2
[Font Awesome](https://fontawesome.com/) v4.7
[Pace](https://github.hubspot.com/pace/) v1.0.2
[jQuery](https://jquery.com/) v3.2.1
[Smooth Scroll](https://github.com/cferdinandi/smooth-scroll) v14.2.1
***
###### Installation
1. Installing ASP.NET Core 2.0. 
We can download ASP.NET Core 2.0 from [microsoft.com]](https://www.microsoft.com/net/download) (click button "Download .NET Core SDK". Why "Download .NET Core SDK", because the application is in developer state. 
After downloading, we install the program. 

2. Downloading repository.
We need download ZHPEvents repository.

3. Finding repository.
In Command Prompt (cmd) we enter the path where we can find the unpacked repository.
```
cd Downloads\ZHPEvents\ZHPEvents
```
4. Creating migration.
```
dotnet ef migrations add Initial
dotnet ef database update 
```
> Migrations are created in the User folder under the name asp net-ZHP Events -...

5. Runing aplication.
```
donten run
```
> The application will compile if everything is okey, it will display the domain of the site, e.g. https://localhost:5001.

6. Getting to know the roles.
In the Application, we have already created users with roles
| Role          | Login                  | Password        |
| --------------|:----------------------:| ---------------:|
| User          | User@gmai.com          | User!1          |
| RaportAuthor  | RaportAuthor@gmai.com  | RaportAuthor!1  |
| RaportEditor  | RaportEditor@gmai.com  | RaportEditor!1  |
| EventAuthor   | EventAuthor@gmai.com   | EventAuthor!1   |
| EventEditor   | EventEditor@gmai.com   | EventEditor!1   |
| Author        | Author@gmai.com        | Author!1        |
| Editor        | Editor@gmai.com        | Editor!1        |
| Administrator | Administrator@gmai.com | Administrator!1 |

7. Starting the service of e-mails
    soon...
***
###### Features
Site for regular users
- displaying events
- filtering events
- information about the application and other minor things

Dashboard to manage the application
- event management, reports for authentication users by role

The user roles are available in the application:
User:
- może wejść w dashboard
RaportAuthor
- can create and edit your reports
RaportEditor
- can create reports and edit reports of yourself and each user
 EventAuthor
- can create and edit your events
EventEditor
- can create reports and edit your and each user's events
 - approves the event, approved events are displayed on the site
 Author
- has features such as RaportAuthor and EventAuthor
 Editor
- has features such as RaportAuthor and EventAuthor
- has capabilities such as RaportEditor and EventEditor
Administrator
- has capabilities such as other users 
- manage users
***
######  Project status
The application is in a creation state. Executed key application elements:
- user roles
- CRUD report and event
- user management by the administrator
- sending e-mails to users
***
###### Things to do
More important things to do:
- full models of reports
- full models of events
- full page for search, search, filing events
- better registration process
- management of user account
- adding IsDeleted to events and reports for archiving
- statistics from raprots and events
***
###### Sources
***
###### Other informations


