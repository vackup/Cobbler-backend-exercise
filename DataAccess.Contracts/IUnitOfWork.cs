using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IUnitOfWork
    {
        IDatabaseTransaction BeginTransaction();
        IBudgetRepository BudgetRepository { get; }
        IMoneyAllocationRepository MoneyAllocationRepository { get; }
        IPersonRepository PersonRepository { get; }
        IProjectRepository ProjectRepository { get; }
        Task CompleteAsync();
        void Dispose();
    }
}