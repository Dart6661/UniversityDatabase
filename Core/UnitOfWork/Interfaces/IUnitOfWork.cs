namespace Core.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitChangesAsync();
    }
}
