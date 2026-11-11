USE DotNetCourseDatabase;

GO
SELECT [UserId],
       [FirstName],
       [LastName],
       [Email],
       [Gender],
       [Active]
FROM   Project1AppSchema.Users;

SELECT *
FROM   Project1AppSchema.Users AS Users
WHERE  Users.Email = "test_user1@dev.com";

SELECT [UserId],
       [Salary]
FROM   Project1AppSchema.UserSalary;

SELECT *
FROM   Project1AppSchema.UserSalary;

SELECT [UserId],
       [JobTitle],
       [Department]
FROM   Project1AppSchema.UserJobInfo;

SELECT *
FROM   Project1AppSchema.Users AS Users
       LEFT OUTER JOIN
       Project1AppSchema.UserSalary AS UserSalary
       ON Users.UserId = UserSalary.UserId;