using Core.UnitOfWork.Interfaces;
using DatabaseContext.Context;

namespace DatabaseContext.UnitOfWork
{
    public class DbUnitOfWork(UnivContext appContext) : IUnitOfWork
    {
        private readonly UnivContext _db = appContext;
        private bool _disposed = false;

        public Task CommitChangesAsync() => _db.SaveChangesAsync();

        public void Dispose()
        {
            if (_disposed) return;
            _db.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
