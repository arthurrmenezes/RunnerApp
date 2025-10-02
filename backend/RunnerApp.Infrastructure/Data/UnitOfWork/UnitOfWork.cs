using RunnerApp.Infrastructure.Data.UnitOfWork.Interfaces;

namespace RunnerApp.Infrastructure.Data.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;

    public UnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> ExecuteInTransactionAsync<T>(
        Func<Task<T>> operation,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _dataContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            T result = await operation();

            await _dataContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
