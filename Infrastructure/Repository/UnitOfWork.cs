using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repository
{
    public  class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IBookRepository bookRepository , IUserRepository user   )
        {
            Book = bookRepository;
            User = user; 
        }
        public IBookRepository Book { get; }
        public IUserRepository User { get; }

    }
}
