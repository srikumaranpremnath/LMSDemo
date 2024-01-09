using Domain.Interfaces;

namespace Domain
{
    public interface IUnitOfWork
    {
        IBookRepository Book { get; }
        IUserRepository User { get; }
    }
}
