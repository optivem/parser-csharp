﻿using Optivem.Framework.Core.Common;

namespace Optivem.Framework.Core.Domain
{
    public class ExistsAggregateRootRequest<TAggregateRoot, TIdentity>
        : IAggregateRootRequest<TAggregateRoot, TIdentity>
        where TAggregateRoot : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        public ExistsAggregateRootRequest(TIdentity identity)
        {
            Identity = identity;
        }

        public TIdentity Identity { get; }
    }
}