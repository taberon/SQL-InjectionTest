# SQL-InjectionTest #
Windows Forms desktop app purposely containing a SQL Injection security flaw using an in-memory SQLite database. Intended to test static security code scanning tools.

(XKCD Web Comic - Source of inspiration and humor)

 ![XKCD Direct Comic Image Link](https://imgs.xkcd.com/comics/exploits_of_a_mom.png)
 
 [https://xkcd.com/327/](https://xkcd.com/327/)  

(Basic app, showing all database info being dumped via SQL Injection.)

![Image of all db names dumbed](Screenshots/Screenshot_DumpAll.png)

(Example of embedded SQL Injections accessible via context menu in input field.)

![Image example SQL Injections](Screenshots/Screenshot_BobbyTablesTestApp.png)

The app also containes multiple modes of input handling, enabling interactive testing of more and less secure text input.
 * Unsafe - Direct user-input is concatenated to form a SQL query.
 * Escaped - User-input is first escaped to prevent most injection vulnerabilities, and then concatenated to for SQL query.
 * Parameterized - Most secure and preffered manner of handling SQL command creations, defining all possible user-input parameters separately from the actual query.

Have fun -- and be safe!

Created by Taber with a computer.
