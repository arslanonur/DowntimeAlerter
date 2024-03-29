### Down Time Alerter
![alt text](https://user-images.githubusercontent.com/6920376/137645985-2861fff9-5718-4566-99b5-7220fe2f1589.png)

**Scenario**

You are developing a web application to monitor target applications’ health. It takes a URL as
input and periodically checks whether it’s up or not. It sends a notification message when a
request to the URL returns a response code other than 2XX.

- ● The user should be able to create/update/delete target apps.
- ● The user should be able to configure a name and URL for the target app.
- ● The user should be able to configure a monitoring interval for the target app.
- ● A notification should be sent if the target app is down. Currently an email notification will be enough, but it should be easy to add another notification types.

**Built With**

- ● .Net Core MVC 5.0
- ● Jquery
- ● Hangfire
- ● MsSql
- ● AblePro Template
- ● XUnit

**Getting Started**

*Default user information for login:*
- Username: onurarslan
- Password:  0606Invicti!

*Change target mail*
You can change target mail address from application.json file like this.

```
> "MailSettings": {
    "Mail": "onur.arslan.invicti2@gmail.com",
    "DisplayName": "Downtime Alerter",
    "Password": "0606Invicti!",
    "Host": "smtp.gmail.com",
    "Port": 587
  }
```
  
**Screenshots**
![alt text](https://user-images.githubusercontent.com/6920376/137645908-fca0dfe2-dbd0-4fca-8496-28f357cea097.PNG)
![alt text](https://user-images.githubusercontent.com/6920376/137645915-89f78e6d-f796-4da9-bc39-c5df39db1dec.PNG)
![alt text](https://user-images.githubusercontent.com/6920376/137645920-90390c49-f1c3-4b78-ac8e-f887ff5ba8b3.PNG)
![alt text](https://user-images.githubusercontent.com/6920376/137645922-aa725047-b4e5-411c-b27d-676f24066ece.PNG)
![alt text](https://user-images.githubusercontent.com/6920376/137645927-8b2608e0-357f-4002-9d65-81e0127356de.PNG)


