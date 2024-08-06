# Library-Management-System-
An overview of the application
This application is aimed at registering librarians initially such that an account is created for a particular librarian which later supports as the login functionality
A librarian can add, modify and delete a book. Again He/She can register students to the library and capture flow of books in the library with ease

Instructions for setting up and running the application locally
Initially when a librarian is new to system or employed, He/She is required to sign Up a form  by pressing a Sign UP button on the login page, If the credentials are valid then access is granted for further forms Like Book_Management form.
On the Book_Management form there is a Navigation Pane which helps the librarian to navigate to different forms like the Membership form,Borrow_and_Return and Log Out. When a user clicks Log Out the session is terminated and Login Page appears for the next ssession if desired
        TERMINOLOGIES USED
SQLite database called Lib Manage which stores all the data entered from the forms
Data Grid Views for data displaying and retrieving from the different tables from the database(Lib Manage)
Imports such System.Data.SQLite which is the core dependency during database connection
         CHALLENGES FACED
Connecting the Vb.net forms to SQLite database. We over came this challenge by consulting online platforms like YouTube and chat gpt plus Gemini(AI)
Creating and adding data in SQLite database. Since it was our first time to use SQLite database connection, we at first faced a challenge of interacting with the DB Browser interface for some good minutes
Creating  notification triggers for due date book borrowers. This is left unhandled due to failure of doing enough research about it and repeated power outages at campus
