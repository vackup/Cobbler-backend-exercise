using System;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public class DataGenerator
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BudgetAllocatorDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BudgetAllocatorDbContext>>()))
            {
                // Insert Persons
                var personRepository = new PersonRepository(context);

                await InsertPersonAsync(personRepository, 1, "John");
                await InsertPersonAsync(personRepository, 2, "AJ");
                await InsertPersonAsync(personRepository, 3, "BJ");

                // Insert Project
                var projectRepository = new ProjectRepository(context);
                await InsertProjectAsync(projectRepository, 1, "Project A");
                await InsertProjectAsync(projectRepository, 2, "Project B");
                await InsertProjectAsync(projectRepository, 3, "Project C");
            }
        }

        private static async Task InsertProjectAsync(ProjectRepository projectRepository, int id, string name)
        {
            var project = new Project {Id = id, Name = name};
            await projectRepository.InsertAsync(project);
        }

        private static async Task InsertPersonAsync(PersonRepository personRepository, int id, string name)
        {
            var person = new Person {Id = id, Name = name };
            await personRepository.InsertAsync(person);
        }
    }
}