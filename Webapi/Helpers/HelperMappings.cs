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
        internal static MoneyAllocation MoneyAllocationCreationModelToEntity(MoneyAllocationModel moneyAllocationModel, int user)
        {
            var moneyAllocation = new MoneyAllocation
            {
                UserId = user,
                MoneyAllocated = moneyAllocationModel.MoneyToAllocate,
                AllocationDate = DateTime.Now
            };

            if (moneyAllocationModel.PersonId.HasValue)
            {
                moneyAllocation.PersonId = moneyAllocationModel.PersonId.Value;
            }

            if (moneyAllocationModel.ProjectId.HasValue)
            {
                moneyAllocation.ProjectId = moneyAllocationModel.ProjectId.Value;
            }

            return moneyAllocation;
        }

        internal static IEnumerable<MoneyAllocationsGroupByPersonModel> EntityToMoneyAllocationsGroupByPersonModel(List<MoneyAllocation> moneyAllocations)
        {
            var moneyAllocationsGroupByPersonModelList = new List<MoneyAllocationsGroupByPersonModel>();

            var allocationsGroupByPerson = GetMoneyAllocationsGroupByPerson(moneyAllocations);

            GetMoneyAllocationsGroupByPersonModel(allocationsGroupByPerson, moneyAllocationsGroupByPersonModelList);

            return moneyAllocationsGroupByPersonModelList;
        }

        private static void GetMoneyAllocationsGroupByPersonModel(
            IEnumerable<IGrouping<string, MoneyAllocation>> allocationsGroupByPerson,
            ICollection<MoneyAllocationsGroupByPersonModel> moneyAllocationsGroupByPersonModelList)
        {
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
        }

        private static string GetProjectName(MoneyAllocation moneyAllocation)
        {
            return moneyAllocation.Project != null ? moneyAllocation.Project.Name : "Any project";
        }

        private static IOrderedEnumerable<IGrouping<string, MoneyAllocation>> GetMoneyAllocationsGroupByPerson(IEnumerable<MoneyAllocation> moneyAllocations)
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

        internal static IEnumerable<MoneyAllocationsGroupByProjectModel> EntityToMoneyAllocationsGroupByProjectModel(IEnumerable<MoneyAllocation> moneyAllocations)
        {
            var moneyAllocationsGroupByProjectModelList = new List<MoneyAllocationsGroupByProjectModel>();

            var allocationsGroupByProject = GetMoneyAllocationsGroupByProject(moneyAllocations);

            GetMoneyAllocationsGroupByProjectModel(allocationsGroupByProject, moneyAllocationsGroupByProjectModelList);

            return moneyAllocationsGroupByProjectModelList;
        }

        private static IOrderedEnumerable<IGrouping<string, MoneyAllocation>> GetMoneyAllocationsGroupByProject(IEnumerable<MoneyAllocation> moneyAllocations)
        {
            return from ma in moneyAllocations
                group ma by GetProjectName(ma)
                into newGroup
                orderby newGroup.Key
                select newGroup;
        }

        private static void GetMoneyAllocationsGroupByProjectModel(
            IEnumerable<IGrouping<string, MoneyAllocation>> allocationsGroupByPerson,
            ICollection<MoneyAllocationsGroupByProjectModel> moneyAllocationsGroupByPersonModelList)
        {
            foreach (var allocationGrouped in allocationsGroupByPerson)
            {
                var moneyAllocationsGroupByPersonModel = new MoneyAllocationsGroupByProjectModel()
                {
                    ProjectName = allocationGrouped.Key,
                    PersonsNames = new List<string>()
                };

                foreach (var moneyAllocation in allocationGrouped)
                {
                    moneyAllocationsGroupByPersonModel.PersonsNames.Add(GetPersonName(moneyAllocation));
                }

                moneyAllocationsGroupByPersonModelList.Add(moneyAllocationsGroupByPersonModel);
            }
        }

        internal static MoneyAllocation MoneyAllocationCreationModelToEntity(MoneyAllocationModel moneyAllocationModel, Guid moneyAllocationId, int user)
        {
            var moneyAllocation = new MoneyAllocation
            {
                Id = moneyAllocationId,
                UserId = user,
                MoneyAllocated = moneyAllocationModel.MoneyToAllocate,
                AllocationDate = DateTime.Now
            };

            if (moneyAllocationModel.PersonId.HasValue)
            {
                moneyAllocation.PersonId = moneyAllocationModel.PersonId.Value;
            }

            if (moneyAllocationModel.ProjectId.HasValue)
            {
                moneyAllocation.ProjectId = moneyAllocationModel.ProjectId.Value;
            }

            return moneyAllocation;
        }
    }
}