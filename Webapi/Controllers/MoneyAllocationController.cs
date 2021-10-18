using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Contracts;
using Entities;
using Webapi.Models;

namespace Webapi.Controllers
{
    /// <summary>
    /// Controller for Money Allocation actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyAllocationController : ControllerBase
    {
        private readonly IBudgetBusiness budgetBusiness;

        public MoneyAllocationController(IBudgetBusiness business)
        {
            this.budgetBusiness = business ?? throw new ArgumentNullException(nameof(business));
        }

        /// <summary>
        /// View the allocations grouped by person
        /// </summary>
        /// <returns></returns>
        [Route("~/GroupByPerson")]
        [HttpGet]
        public async Task<IEnumerable<BudgetGroupByPersonModel>> GetGroupByPerson()
        {
            var budget = await this.budgetBusiness.GetIncludeMoneyAllocationsByUserIdAsync(HelperData.User);

            if (budget?.MoneyAllocations == null || !budget.MoneyAllocations.Any())
            {
                return null;
            }

            var allocations = budget.MoneyAllocations;

            var persons = from a in allocations
                group a by a.Person.Name
                into newPersonGroup
                orderby newPersonGroup.Key
                select newPersonGroup;

            foreach (var nameGroup in persons)
            {
                Console.WriteLine($"\t{nameGroup.Key}");

                foreach (var moneyAllocation in nameGroup)
                {
                    Console.WriteLine($"\t{moneyAllocation.Project}");
                }
            }

            return ToBudgetToBudgetGroupByPersonModel(budget);
        }

        private IEnumerable<BudgetGroupByPersonModel> ToBudgetToBudgetGroupByPersonModel(Budget budget)
        {
            throw new NotImplementedException();
        }

        //private BudgetGroupByPersonModel ToBookResponseModel(MoneyAllocation moneyAllocationCreationModel)
        //{
        //    return new BudgetGroupByPersonModel
        //    {
        //        Id = moneyAllocationCreationModel.Id,
        //        SalesCount = moneyAllocationCreationModel.SalesCount,
        //        Title = moneyAllocationCreationModel.Title,
        //        Author = new AuthorResponseModel
        //        {
        //            Id = moneyAllocationCreationModel.Author.Id,
        //            Name = moneyAllocationCreationModel.Author.Name,
        //        }
        //    };
        //}

        //// GET api/<MoneyAllocationController>/5
        //[HttpGet("{id}")]
        //public async Task<MoneyAllocation> Get(int id)
        //{
        //    return await this.budgetBusiness.GetAsync(id);
        //}

        /// <summary>
        /// Allocate money for a user to a project and / or person
        /// </summary>
        /// <param name="moneyAllocationCreation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Post([FromBody] MoneyAllocationCreationModel moneyAllocationCreation)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            await this.budgetBusiness.CreateNewMoneyAllocationAsync(GetEntity(moneyAllocationCreation, HelperData.User));
        }

        //// PUT api/<MoneyAllocationController>/5
        //[HttpPut("{id}")]
        //public async Task Put(int id, [FromBody] MoneyAllocationCreationModel moneyAllocationCreationModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new Exception();
        //    }

        //    moneyAllocationCreationModel.Id = id;

        //    await this.budgetBusiness.UpdateAsync(GetEntity(moneyAllocationCreationModel));
        //}

        //// DELETE api/<MoneyAllocationController>/5
        //[HttpDelete("{id}")]
        //public async Task Delete(int id)
        //{
        //    await this.budgetBusiness.DeleteAsync(id);
        //}

        private MoneyAllocation GetEntity(MoneyAllocationCreationModel moneyAllocationCreationModel, int user)
        {
            var moneyAllocation = new MoneyAllocation
            {
                //Budget = new Budget { User = user },
                UserId = user,
                MoneyAllocated = moneyAllocationCreationModel.MoneyToAllocate,
                AllocationDate = DateTime.Now
            };

            if (moneyAllocationCreationModel.PersonId.HasValue)
            {
                //moneyAllocation.Person = new Person {Id = moneyAllocationCreationModel.PersonId.Value};
                moneyAllocation.PersonId = moneyAllocationCreationModel.PersonId.Value;
            }

            if (moneyAllocationCreationModel.ProjectId.HasValue)
            {
                //moneyAllocation.Project = new Project { Id = moneyAllocationCreationModel.ProjectId.Value };
                moneyAllocation.ProjectId = moneyAllocationCreationModel.ProjectId.Value;
            }

            return moneyAllocation;
        }
    }
}
