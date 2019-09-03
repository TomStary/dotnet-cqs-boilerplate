using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BoilerPlate.Utils;
using Microsoft.Extensions.Logging;

namespace BoilerPlate.Entities.Cqs
{
    public interface IQueryHandler<in TParam, TRes> where TParam : IQuery
    {
        TRes GetResult(TParam query);

        Task<TRes> GetResultAsync(TParam query);
    }

    public abstract class QueryHandler<TParam, TRes> : IQueryHandler<TParam, TRes>, IScopedService<IQueryHandler<TParam, TRes>>
        where TParam : IQuery, new()
    {
        protected readonly ILogger<QueryHandler<TParam, TRes>> Logger;
        protected AppDbContext DbContext;

        public QueryHandler(ILogger<QueryHandler<TParam, TRes>> logger, AppDbContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }

        /// <summary>
        /// Gets result for given query in specified format
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public TRes GetResult(TParam query)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();

            TRes _queryResult;

            try
            {
                //handle the query request
                _queryResult = Handle(query);

            }
            catch (Exception _exception)
            {
                Logger.LogCritical("Error in {0} queryHandler. Message: {1} \n Stacktrace: {2}", typeof(TParam).Name, _exception.Message, _exception.StackTrace);
                //Do more error more logic here
                throw;
            }
            finally
            {
                _stopWatch.Stop();
                Logger.LogDebug("Response for query {0} served (elapsed time: {1} msec)", typeof(TParam).Name, _stopWatch.ElapsedMilliseconds);
            }


            return _queryResult;
        }

        /// <summary>
        /// Gets async result for given query in specified format
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TRes> GetResultAsync(TParam query)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();

            Task<TRes> _queryResult;

            try
            {
                //handle the query request
                _queryResult = HandleAsync(query);

            }
            catch (Exception _exception)
            {
                Logger.LogCritical("Error in {0} queryHandler. Message: {1} \n Stacktrace: {2}", typeof(TParam).Name, _exception.Message, _exception.StackTrace);
                //Do more error more logic here
                throw;
            }
            finally
            {
                _stopWatch.Stop();
                Logger.LogDebug("Response for query {0} served (elapsed time: {1} msec)", typeof(TParam).Name, _stopWatch.ElapsedMilliseconds);
            }


            return await _queryResult;
        }

        /// <summary>
        /// The actual Handle method that will be implemented in the sub class
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract TRes Handle(TParam request);

        /// <summary>
        /// The actual async Handle method that will be implemented in the sub class
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract Task<TRes> HandleAsync(TParam request);
    }
}