namespace Hyperar.HPA.DataContracts
{
    using System.Linq;
    using Hyperar.HPA.Domain.Interfaces;

    public interface IQueryStrategy<T> where T : class, IEntity
    {
        IQueryable<T> ApplyIncludes(IQueryable<T> query);
    }
}