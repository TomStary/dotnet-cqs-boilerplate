using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using BoilerPlate.Utils;

namespace BoilerPlate.Entities.Cqs
{
    public interface IQueryDispatcher
    {
        TRes Dispatch<TParam, TRes>(TParam query)
            where TParam : IQuery;

        Task<TRes> DispatchAsync<TParam, TRes>(TParam query)
            where TParam : IQuery;
    }

    public class QueryDispatcher : IQueryDispatcher, IScopedService<IQueryDispatcher>
    {
        private readonly IServiceProvider provider;

        public QueryDispatcher(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public TRes Dispatch<TParam, TRes>(TParam query) where TParam : IQuery
        {
            var handler = provider.GetService<IQueryHandler<TParam, TRes>>();
            return handler.GetResult(query);
        }

        public async Task<TRes> DispatchAsync<TParam, TRes>(TParam query) where TParam : IQuery
        {
             var handler = provider.GetService<IQueryHandler<TParam, TRes>>();
            return await handler.GetResultAsync(query);
        }
    }
}