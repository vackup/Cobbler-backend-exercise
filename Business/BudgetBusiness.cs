using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Business.Contracts;
using DataAccess.Contracts;
using Entities;

namespace Business
{
    public class BudgetBusiness : IBudgetBusiness
    {
        private readonly IBudgetRepository repository;

        public BudgetBusiness(IBudgetRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Budget>> GetAllAsync()
        {
            return await this.repository.GetAllAsync();
        }

        public async Task<Budget> GetAsync(int id)
        {
            return await this.repository.GetAsync(id);
        }

        public async Task<Budget> GetFirstOrDefaultAsync()
        {
            return await this.repository.GetFirstOrDefaultAsync();
        }

        public async Task CreateAsync(Budget budget)
        {
            var userBudget = await this.repository.GetByUserIdAsync(budget.User);

            if (userBudget != null)
            {
                throw new Exception($"Budget was already created for user {budget.User}");
            }

            await this.repository.InsertAsync(budget);
        }

        public async Task UpdateAsync(Budget entity)
        {
            await this.repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await this.repository.DeleteAsync(id);
        }

        public async Task DeleteAsync(Budget entity)
        {
            await this.repository.DeleteAsync(entity);
        }
    }
}