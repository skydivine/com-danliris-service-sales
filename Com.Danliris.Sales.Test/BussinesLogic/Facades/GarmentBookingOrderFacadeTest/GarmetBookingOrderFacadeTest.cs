﻿using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentBookingOrderDataUtils;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Danliris.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.GarmentBookingOrderFacadeTest
{
    public class GarmetBookingOrderFacadeTest : BaseFacadeTest<SalesDbContext, GarmentBookingOrderFacade, GarmentBookingOrderLogic, GarmentBookingOrder, GarmentBookingOrderDataUtil>
    {
        private const string ENTITY = "GarmentBookingOrders";

        public GarmetBookingOrderFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var garmentBookingOrderItemLogic = new GarmentBookingOrderItemLogic(identityService, serviceProviderMock.Object, dbContext);
            var garmentBookingOrderLogic = new GarmentBookingOrderLogic(garmentBookingOrderItemLogic, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentBookingOrderLogic)))
                .Returns(garmentBookingOrderLogic);

            return serviceProviderMock;
        }

        [Fact]
        public async void BOCancel_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentBookingOrderFacade facade = new GarmentBookingOrderFacade(serviceProvider, dbContext);
            
            var data = await DataUtil(facade).GetTestData();

            var Response = await facade.BOCancel((int)data.Id, data);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public async void BODelete_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentBookingOrderFacade facade = new GarmentBookingOrderFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            var Response = await facade.BODelete((int)data.Id, data);
            Assert.NotEqual(Response, 0);
        }
    }
}
