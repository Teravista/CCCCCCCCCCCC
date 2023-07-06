// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Deparamtes;

Console.WriteLine("Hello, World!");
var department = new DepartmentModel { DepartmentName="ass" };
var departments = new List<DepartmentModel>();
using (var context = new AppDbContext())
{
   // context.Add(department); 
    context.Entry(department).State=EntityState.Added;
     context.SaveChanges();

    departments = await context.Departamentos.ToListAsync();
}

foreach (var i in departments)
{
    Console.WriteLine(i.DepartmentName);
}