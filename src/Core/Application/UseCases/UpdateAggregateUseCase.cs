﻿using Optivem.Framework.Core.Common.Mapping;
using Optivem.Framework.Core.Domain;
using System.Threading.Tasks;

namespace Optivem.Framework.Core.Application
{
    public abstract class UpdateAggregateUseCase<TRepository, TRequest, TResponse, TAggregateRoot, TIdentity, TId>
        : UnitOfWorkUseCase<TRepository, TRequest, TResponse>
        where TRepository : IFindAggregateRepository<TAggregateRoot, TIdentity>, IExistsAggregateRepository<TAggregateRoot, TIdentity>, IUpdateAggregateRepository<TAggregateRoot, TIdentity>
        where TRequest : IRequest<TId>
        where TResponse : class, IResponse<TId>
        where TAggregateRoot : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity<TId>
    {
        public UpdateAggregateUseCase(IMapper mapper, IUnitOfWork unitOfWork) 
            : base(mapper, unitOfWork)
        {
        }

        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            var id = request.Id;
            var identity = Mapper.Map<TId, TIdentity>(id);

            var repository = GetRepository();

            var aggregateRoot = await repository.GetAsync(identity);

            if (aggregateRoot == null)
            {
                throw new NotFoundRequestException();
            }

            await UpdateAsync(request, aggregateRoot);

            try
            {
                aggregateRoot = await repository.UpdateAsync(aggregateRoot);
                await UnitOfWork.SaveChangesAsync();
                var response = Mapper.Map<TAggregateRoot, TResponse>(aggregateRoot);
                return response;
            }
            catch (ConcurrentUpdateException ex)
            {
                var exists = await repository.ExistsAsync(identity);

                if (!exists)
                {
                    throw new NotFoundRequestException(ex.Message, ex);
                }

                throw;
            }
        }

        protected abstract Task UpdateAsync(TRequest request, TAggregateRoot aggregateRoot);
    }
}