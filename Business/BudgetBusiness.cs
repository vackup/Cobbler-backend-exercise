using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Business.Contracts;
using DataAccess.Contracts;
using Entities;

namespace Business
{
    public class BudgetBusiness : IBudgetBusiness
    {
        private readonly IUnitOfWork unitOfWork;

        public BudgetBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<Budget>> GetAllAsync()
        {
            return await this.unitOfWork.BudgetRepository.GetAllAsync();
        }

        public async Task<Budget> GetAsync(Guid id)
        {
            return await this.unitOfWork.BudgetRepository.GetAsync(id);
        }

        public async Task<Budget> GetFirstOrDefaultAsync()
        {
            return await this.unitOfWork.BudgetRepository.GetFirstOrDefaultAsync();
        }

        public async Task CreateNewBudgetAsync(Budget budget)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    var userBudget = await this.unitOfWork.BudgetRepository.GetByUserIdAsync(budget.UserId);

                    if (userBudget != null)
                    {
                        throw new Exception($"Budget was already created for user {budget.UserId}");
                    }

                    budget.Id = Guid.NewGuid();

                    await this.unitOfWork.BudgetRepository.InsertAsync(budget);

                    await this.unitOfWork.CompleteAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task UpdateAsync(Budget entity)
        {
            await this.unitOfWork.BudgetRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await this.unitOfWork.BudgetRepository.DeleteAsync(id);
        }

        public async Task DeleteAsync(Budget entity)
        {
            await this.unitOfWork.BudgetRepository.DeleteAsync(entity);
        }

        public async Task<decimal> GetAvailableMoneyToAllocateByUserIdAsync(int user)
        {
            var budget = await this.unitOfWork.BudgetRepository.GetIncludeMoneyAllocationsByUserIdAsync(user);

            return GetAvailableMoneyToAllocateByBudget(budget);
        }

        private static decimal GetAvailableMoneyToAllocateByBudget(Budget budget)
        {
            if (budget == null)
            {
                return 0m;
            }

            var moneyAllocationsSum = GetMoneyAllocated(budget);

            return budget.InitialMoneyToAllocate - moneyAllocationsSum;
        }

        private static decimal GetMoneyAllocated(Budget budget)
        {
            var moneyAllocationsSum = 0m;

            if (budget.MoneyAllocations != null && budget.MoneyAllocations.Any())
            {
                moneyAllocationsSum = budget.MoneyAllocations.Sum(m => m.MoneyAllocated);
            }

            return moneyAllocationsSum;
        }

        public async Task CreateNewMoneyAllocationAsync(MoneyAllocation moneyAllocation)
        {
            if (moneyAllocation.ProjectId == null && moneyAllocation.PersonId == null)
            {
                throw new ArgumentException("Money allocations needs project and / or person");
            }

            var budget = await this.unitOfWork.BudgetRepository.GetIncludeMoneyAllocationsByUserIdAsync(moneyAllocation.UserId);

            var availableMoneyToAllocate = GetAvailableMoneyToAllocateByBudget(budget);

            if (availableMoneyToAllocate <= 0m)
            {
                throw new Exception("There is no more money available to allocate");
            }

            if (moneyAllocation.MoneyAllocated > availableMoneyToAllocate)
            {
                throw new Exception("There is no more money available to allocate");
            }

            moneyAllocation.Id = Guid.NewGuid();
            moneyAllocation.BudgetId = budget.Id;

            await this.unitOfWork.MoneyAllocationRepository.InsertAsync(moneyAllocation);
        }

        public async Task<IEnumerable<MoneyAllocation>> GetMoneyAllocationsByUserIdAsync(int user)
        {
            var budget = await this.unitOfWork.BudgetRepository.GetByUserIdAsync(user);

            if (budget == null)
            {
                return new List<MoneyAllocation>();
            }

            return await this.unitOfWork.MoneyAllocationRepository.GetIncludePersonAndProjectByBudgetIdAsync(budget.Id);
        }
    }
}