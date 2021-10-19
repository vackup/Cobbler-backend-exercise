using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Contracts;
using Entities;
using Webapi.Helpers;
using Webapi.Models;

namespace Webapi.Controllers
{
    /// <summary>
    /// Controller for Money Allocation operations
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
        public async Task<IEnumerable<MoneyAllocationsGroupByPersonModel>> GetGroupByPerson()
        {
            var moneyAllocations = (await this.budgetBusiness.GetMoneyAllocationsByUserIdAsync(HelperData.User)).ToList();

            return !moneyAllocations.ToList().Any() ? null : HelperMappings.EntityToMoneyAllocationsGroupByPersonModel(moneyAllocations);
        }

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

            await this.budgetBusiness.CreateNewMoneyAllocationAsync(HelperMappings.MoneyAllocationCreationModelToEntity(moneyAllocationCreation, HelperData.User));
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

        //    await this.budgetBusiness.UpdateAsync(MoneyAllocationCreationModelToEntity(moneyAllocationCreationModel));
        //}

        //// DELETE api/<MoneyAllocationController>/5
        //[HttpDelete("{id}")]
        //public async Task Delete(int id)
        //{
        //    await this.budgetBusiness.DeleteAsync(id);
        //}

        
    }
}
