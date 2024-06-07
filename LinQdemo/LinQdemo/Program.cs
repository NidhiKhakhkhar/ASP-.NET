
using LinQdemo;



var context = new ApplicationDBContext();
//For example, Display FirstName of all employees.
var q1 = context.Employee.Select(x => x.FirstName);
foreach (var employee in q1)
{
    Console.WriteLine("\n {0}", employee);
}


//1.Display data of all employees.

//For example, Display FirstName of all employees.
var q2 = context.Employee.Select(x => x.FirstName + ' ' + x.LastName + ' ' + x.PhoneNo + ' ' + x.Salary);
foreach (var employee in q2)
{
    Console.WriteLine("\n {0}", employee);
}
//2. Select ActNo, FirstName and Salary of all employees to a new class and display it.
var q3 = context.Employee.Select(x => new Employee() { AccountNo = x.AccountNo,FirstName = x.FirstName,Salary = x.Salary }).ToList();
foreach (var employee in q3)
{
    Console.WriteLine("\n AccountNo : {0} , FirstName : {1} , Salary : {2}",employee.AccountNo ,employee.FirstName,employee.Salary);
}


//3.Display data in following format => {Anil} works in {Admin} Department.

var q4 = context.Employee.Select(x => x.FirstName+ " Works in  "+ x.Department + "Department");
foreach (var employee in q4)
{
    Console.WriteLine("\n {0}", employee);
}

//4.Select Employee Full Name, Email and Department as anonymous and display it.

var q5 = context.Employee.Select(x => new {  FullName = x.FirstName + " "+ x.LastName, Email = x.Email , Department = x.Department});
foreach (var employee in q5)
{
    Console.WriteLine("\n {0}", employee);
}

//5. Display employees with their joining date.

var q6 = context.Employee.Select(x => x.FirstName + " " + x.LastName +" : "+ x.JoiningDate);
foreach (var employee in q6)
{
    Console.WriteLine("\n {0}", employee);
}


//6.Display employees between age 20 to 30.

var q7 = context.Employee.Where(x => x.Age >= 20 && x.Age <= 30);
foreach (var employee in q7)
{
    Console.WriteLine("\n FirstName :{0},Age : {1}",employee.FirstName ,employee.Age);
}
//7. Display female employees.
var q8 = context.Employee.Where(x => x.Gender == "Female");
foreach (var employee in q8)
{
    Console.WriteLine("\n FirstName :{0}", employee.FirstName);
}


//8. Display employees with salary more than 35000.
var q9 = context.Employee.Where(x => x.Salary > 35000);
foreach (var employee in q9)
{
    Console.WriteLine("\n FirstName :{0},Salary : {1}", employee.FirstName,employee.Salary);
}
//9. Display employees whose account no is less than 110.
var q10 = context.Employee.Where(x => x.AccountNo <= 110);
foreach (var employee in q10)
{
    Console.WriteLine("\n AccountNo : {1},FirstName :{0}", employee.FirstName,employee.AccountNo);
}

//10. Display employees who belongs to either Rajkot or Morbi city.

var q11 = context.Employee.Where(x => x.City == "Rajkot" || x.City == "Morbi");
foreach (var employee in q11)
{
    Console.WriteLine("\n FirstName :{0},City : {1}", employee.FirstName , employee.City);
}
//11. Display employees whose salary is less than 20000.
var q12 = context.Employee.Where(x => x.Salary < 20000);
foreach (var employee in q12)
{
    Console.WriteLine("\n FirstName :{0},Salary : {1}", employee.FirstName, employee.Salary);
}
//12. Display employees whose salary is more than equal to 30000 and less than equal to 60000.

var q13 = context.Employee.Where(x => x.Salary >= 30000 && x.Salary <= 60000);
foreach (var employee in q13)
{
    Console.WriteLine("\n FirstName :{0},Salary : {1}", employee.FirstName, employee.Salary);
}
//13. Display ActNo, FirstName and Amount of employees who belong to Morbi or Ahmedabad or
//Surat city and Account No greater than 120.
var q14 = context.Employee.Where(x => x.City == "Ahmedabad" || x.City == "Morbi" || x.City == "Surat" && x.AccountNo > 120);
foreach (var employee in q14)
{
    Console.WriteLine("\n AccountNo : {2},FirstName :{0},City : {1},Amount : {3}", employee.FirstName, employee.City,employee.AccountNo,employee.Salary);
}

//14. Display male employees of age between 30 to 35 and belongs to Rajkot city.
var q15 = context.Employee.Where(x => x.Age >= 30 && x.Age <= 35 && x.Gender =="Male" && x.City == "Rajkot");
foreach (var employee in q15)
{
    Console.WriteLine("\n FirstName :{0},Age : {1},City : {2}", employee.FirstName, employee.Age,employee.City);
}

//15. Display Unique Cities. (use Distinct())
var q16 = context.Employee.Select(x => x.City).Distinct();
foreach (var employee in q16)
{
    Console.WriteLine("\n City :{0}", employee);
}

//16.Display employees whose joining is between 15/07/2018 to 16/09/2019.
//var q17 = context.Employee.Where(x => x.JoiningDate > Convert.ToDateTime("2018/07/15") && x.JoiningDate < Convert.ToDateTime("2019/09/16") );
//foreach (var employee in q17)
//{
//    Console.WriteLine("\n FirstName :{0},JoiningDate : {1}", employee.FirstName, employee.JoiningDate);
//}

//17. Display female employees who works in Sales department.
var q18 = context.Employee.Where(x => x.Gender == "Female" && x.Department == "Sales");
foreach (var employee in q18)
{
    Console.WriteLine("\n FirstName :{0},Department : {1}", employee.FirstName,employee.Department);
}

//18. Display employees with their Age who belong to Rajkot city and more than 35 years old.

var q19 = context.Employee.Where(x => x.Age < 35  && x.City == "Rajkot");
foreach (var employee in q19)
{
    Console.WriteLine("\n FirstName :{0},Age : {1},City : {2}", employee.FirstName, employee.Age, employee.City);
}

//LINQ Queries on Aggregate

//19. Find total salary and display it.
var q20 = context.Employee.Sum(x => x.Salary);
Console.WriteLine("\n Total Salary : {0}",q20);

//20. Find total number of employees of Admin department who belongs to Rajkot city.
var q21 = context.Employee.Where(x => x.Department == "Admin" && x.City == "Rajkot").Count();
Console.WriteLine("\n Total Employee : {0}", q21);


//21. Find total salary of Distribution department.
var q22 = context.Employee.Where(x => x.Department == "Distribution").Sum(x => x.Salary);
Console.WriteLine("\n Total Salary : {0}", q22);


//22. Find average salary of IT department.
var q23 = context.Employee.Where(x => x.Department == "IT").Average(x => x.Salary);
Console.WriteLine("\n Total Salary : {0}", q23);

//23. Find minimum salary of Customer Relationship department.
var q24 = context.Employee.Where(x => x.Department == "Customer Relationship").Min(x => x.Salary);
Console.WriteLine("\n Min Salary : {0}", q24);

//24. Find maximum salary of Distribution department who belongs to Baroda city.
var q25 = context.Employee.Where(x => x.Department == "Distribution" && x.City == "Baroda").Max(x => x.Salary);
Console.WriteLine("\n Min Salary : {0}", q25);

//25. Find number of employees whose Age is more than 40.
var q26 = context.Employee.Count(x => x.Age > 40);
Console.WriteLine("\n Total Employee : {0}", q26);

//26. Find total female employees working in Ahmedabad city.
var q27 = context.Employee.Count(x => x.Gender == "Female" && x.City == "Ahmedabad");
Console.WriteLine("\n Total Employee : {0}", q27);

//27. Find total salary of male employees who belongs to Gandhinagar city and works in IT
//department.
var q28 = context.Employee.Where(x => x.Gender == "Male" && x.Department == "IT" && x.City == "Gandhinagar").Sum(x => x.Salary);
Console.WriteLine("\n Total Salary : {0}", q28);

//28. Find average salary of male employees whose age is between 21 to 35.
var q29 = context.Employee.Where(x => x.Gender == "Male" && x.Age > 21 && x.Age < 35).Average(x => x.Salary);
Console.WriteLine("\n Total Salary : {0}", q29);

//LINQ Queries on Sorting operators

//29. Display employees by their first name in ascending order.
var q30 = context.Employee.OrderBy(x => x.FirstName);
foreach (var employee in q30)
{
    Console.WriteLine("\n FirstName :{0}", employee.FirstName);
}

//30. Display employees by department name in descending order.
var q31 = context.Employee.OrderByDescending(x => x.Department);
foreach (var employee in q31)
{
    Console.WriteLine("\n FirstName :{0},Department : {1}", employee.FirstName,employee.Department);
}

//31. Display employees by department name descending and first name in ascending order.
var q32 = context.Employee.OrderByDescending(x => x.Department).ThenBy(x => x.FirstName);
foreach (var employee in q32)
{
    Console.WriteLine("\n FirstName :{0},Department : {1}", employee.FirstName, employee.Department);
}

//32. Display employees by their first name in ascending order and last name in descending order.
var q33 = context.Employee.OrderBy(x => x.FirstName).ThenByDescending(x => x.LastName);
foreach (var employee in q33)
{
    Console.WriteLine("\n FirstName :{0},LastName : {1}", employee.FirstName, employee.LastName);
}

//33. Display employees by their Joining Date using Reverse() operator
var q34 = context.Employee.OrderBy(x => x.JoiningDate).Reverse();
foreach (var employee in q34)
{
    Console.WriteLine("\n FirstName :{0},JoiningDate : {1}", employee.FirstName, employee.JoiningDate);
}
