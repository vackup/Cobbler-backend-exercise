using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Entities;
using Webapi.Models;

namespace Webapi.Helpers
{
    /// <summary>
    /// Helper methods to map entities to models and models to entities
    /// </summary>
    public static class HelperMappings
    {
        internal static MoneyAllocation MoneyAllocationCreationModelToEntity(MoneyAllocationCreationModel moneyAllocationCreationModel, int user)
        {
            var moneyAllocation = new MoneyAllocation
            {
                UserId = user,
                MoneyAllocated = moneyAllocationCreationModel.MoneyToAllocate,
                AllocationDate = DateTime.Now
            };

            if (moneyAllocationCreationModel.PersonId.HasValue)
            {
                moneyAllocation.PersonId = moneyAllocationCreationModel.PersonId.Value;
            }

            if (moneyAllocationCreationModel.ProjectId.HasValue)
            {
                moneyAllocation.ProjectId = moneyAllocationCreationModel.ProjectId.Value;
            }

            return moneyAllocation;
        }

        internal static IEnumerable<MoneyAllocationsGroupByPersonModel> EntityToMoneyAllocationsGroupByPersonModel(List<MoneyAllocation> moneyAllocations)
        {
            var moneyAllocationsGroupByPersonModelList = new List<MoneyAllocationsGroupByPersonModel>();

            var allocationsGroupByPerson = GetMoneyAllocationsGroupByPerson(moneyAllocations);

            foreach (var allocationGrouped in allocationsGroupByPerson)
            {
                var moneyAllocationsGroupByPersonModel = new MoneyAllocationsGroupByPersonModel
                {
                    PersonName = allocationGrouped.Key,
                    ProjectNames = new List<string>()
                };

                foreach (var moneyAllocation in allocationGrouped)
                {
                    moneyAllocationsGroupByPersonModel.ProjectNames.Add(GetProjectName(moneyAllocation));
                }

                moneyAllocationsGroupByPersonModelList.Add(moneyAllocationsGroupByPersonModel);
            }

            return moneyAllocationsGroupByPersonModelList;
        }

        private static string GetProjectName(MoneyAllocation moneyAllocation)
        {
            return moneyAllocation.Project != null ? moneyAllocation.Project.Name : "Any project";
        }

        private static IOrderedEnumerable<IGrouping<string, MoneyAllocation>> GetMoneyAllocationsGroupByPerson(List<MoneyAllocation> moneyAllocations)
        {
            return from ma in moneyAllocations
                group ma by GetPersonName(ma)
                into newGroup
                orderby newGroup.Key
                select newGroup;
        }

        private static string GetPersonName(MoneyAllocation moneyAllocation)
        {
            return moneyAllocation.Person != null ? moneyAllocation.Person.Name : "Any person";
        }
    }
}