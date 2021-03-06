﻿using Atomiv.Core.Application;

namespace Generator.Core.Application.Products.Requests
{
    public class CreateProductRequest : IRequest
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
