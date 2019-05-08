﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Optivem.Core.Application
{
    public interface IValidationFilter<TRequest> : IFilter<TRequest>
        where TRequest : IRequest
    {
    }
}