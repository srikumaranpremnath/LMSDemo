using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainCommon
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task Create(Entity entity);
        Task Update(Entity entity);
        Task Delete(Entity entity);
    }
}
