﻿using FluentAssertions;
using Optivem.Template.Core.Application.Orders.Commands;
using Optivem.Template.Core.Application.Orders.Queries;
using Optivem.Template.Core.Common.Orders;
using Optivem.Template.Web.RestApi.IntegrationTest.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Optivem.Template.Web.RestApi.IntegrationTest.Orders.Commands
{
    public class CancelOrderCommandTest : Test
    {
        public CancelOrderCommandTest(Fixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task CancelOrder_Valid()
        {
            // Arrange

            var createResponses = await CreateSampleOrdersAsync();
            var someCreateResponse = createResponses[1];

            // Act

            var id = someCreateResponse.Data.Id;
            var cancelRequest = new CancelOrderCommand { Id = id };
            var cancelHttpResponse = await Fixture.Api.Orders.CancelOrderAsync(cancelRequest);

            // Assert

            cancelHttpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var archiveResponse = cancelHttpResponse.Data;

            var expectedArchiveResponse = new CancelOrderCommandResponse
            {
                Id = id,
                Status = OrderStatus.Cancelled,
            };

            archiveResponse.Should().BeEquivalentTo(expectedArchiveResponse);

            var findRequest = new FindOrderQuery { Id = id };
            var findHttpResponse = await Fixture.Api.Orders.FindOrderAsync(findRequest);

            findHttpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var findResponse = findHttpResponse.Data;

            findResponse.Should().BeEquivalentTo(archiveResponse);
        }
    }
}
