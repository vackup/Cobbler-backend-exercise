using System;
using System.Collections.Generic;
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

        public async Task UpdateMoneyAllocationAsync(MoneyAllocation moneyAllocation)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    ValidatePersonAndProject(moneyAllocation);

                    var budget = await this.unitOfWork.BudgetRepository.GetIncludeMoneyAllocationsByUserIdAsync(
                        moneyAllocation.UserId);

                    var currentMoneyAvailableToAllocate = GetCurrentMoneyAvailableToAllocate(moneyAllocation, budget);

                    if (CanAllocateMoney(moneyAllocation.MoneyAllocated, currentMoneyAvailableToAllocate))
                    {
                        throw new Exception("Money to allocate is greater than available");
                    }

                    await this.unitOfWork.MoneyAllocationRepository.UpdateAsync(GetMoneyAllocationsToUpdate(moneyAllocation, budget));
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

        public async Task<decimal> GetAvailableMoneyToAllocateByUserIdAsync(int user)
        {
            var budget = await this.unitOfWork.BudgetRepository.GetIncludeMoneyAllocationsByUserIdAsync(user);

            return GetAvailableMoneyToAllocateByBudget(budget);
        }
        
        public async Task<Guid> CreateNewMoneyAllocationAsync(MoneyAllocation moneyAllocation)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    ValidatePersonAndProject(moneyAllocation);

                    var budget = await this.unitOfWork.BudgetRepository.GetIncludeMoneyAllocationsByUserIdAsync(
                        moneyAllocation.UserId);

                    var availableMoneyToAllocate = GetAvailableMoneyToAllocateByBudget(budget);

                    if (CanAllocateMoney(moneyAllocation.MoneyAllocated, availableMoneyToAllocate))
                    {
                        throw new Exception("There is no more money available to allocate");
                    }

                    moneyAllocation.BudgetId = budget.Id;
                    moneyAllocation.Id = Guid.NewGuid();

                    await this.unitOfWork.MoneyAllocationRepository.InsertAsync(moneyAllocation);
                    await this.unitOfWork.CompleteAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

                return moneyAllocation.Id;
            }
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

        private static bool CanAllocateMoney(decimal moneyToAllocate, decimal moneyAvailableToAllocate)
        {
            return moneyToAllocate > moneyAvailableToAllocate;
        }

        private static decimal GetCurrentMoneyAvailableToAllocate(MoneyAllocation moneyAllocation, Budget budget)
        {
            var availableMoneyToAllocate = GetAvailableMoneyToAllocateByBudget(budget);

            var previousMoneyAllocation = GetPreviousMoneyAllocation(moneyAllocation, budget);

            return availableMoneyToAllocate + previousMoneyAllocation;
        }

        private static decimal GetPreviousMoneyAllocation(MoneyAllocation moneyAllocation, Budget budget)
        {
            var moneyAllocationsToUpdate = budget.MoneyAllocations.FirstOrDefault(ma => ma.Id == moneyAllocation.Id);

            if (moneyAllocationsToUpdate == null)
            {
                throw new Exception("The money allocation you are trying to update doesn't exist");
            }

            var currentMoneyAllocation = moneyAllocationsToUpdate.MoneyAllocated;
            return currentMoneyAllocation;
        }

        private static MoneyAllocation GetMoneyAllocationsToUpdate(MoneyAllocation moneyAllocation, Budget budget)
        {
            var moneyAllocationsToUpdate = budget.MoneyAllocations.First(ma => ma.Id == moneyAllocation.Id);
            moneyAllocationsToUpdate.MoneyAllocated = moneyAllocation.MoneyAllocated;
            moneyAllocationsToUpdate.PersonId = moneyAllocation.PersonId;
            moneyAllocationsToUpdate.ProjectId = moneyAllocation.ProjectId;
            moneyAllocationsToUpdate.AllocationDate = moneyAllocation.AllocationDate;
            return moneyAllocationsToUpdate;
        }

        private static void ValidatePersonAndProject(MoneyAllocation moneyAllocation)
        {
            if (moneyAllocation.ProjectId == null && moneyAllocation.PersonId == null)
            {
                throw new ArgumentException("Money allocations needs project and / or person");
            }
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
    }
}