using Smartwyre.DeveloperTest.IData;
using Smartwyre.DeveloperTest.Services;
using System;
using Xunit;
using Moq;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    public readonly Mock<IRebateDataStore> _rebateDataStore;
    public readonly Mock<IProductDataStore> _productDataStore;
    public readonly RebateService _rebateService;

    public  PaymentServiceTests()
    {
        _rebateDataStore = new Mock<IRebateDataStore>();
        _productDataStore = new Mock<IProductDataStore>();
        _rebateService =new RebateService(_rebateDataStore.Object, _productDataStore.Object);

    }


    [Fact]
    public void TestRebateService()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate",
            ProductIdentifier = "product",
            Volume = 100
        };
        var rebate = new Rebate
        {
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 100
        };
        var product = new Product
        {
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        _rebateDataStore.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(rebate);
        _productDataStore.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(product);
        var result = _rebateService.Calculate(request);
        Assert.True(result.Success);
        
    }
    [Fact]
    public void TestRebateService_Failure()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate",
            ProductIdentifier = "product",
            Volume = 1
        };
        var rebate = new Rebate
        {
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 100
        };
        var product = (Product)null;

        _rebateDataStore.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(rebate);
        _productDataStore.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(product);
        var result = _rebateService.Calculate(request);
        Assert.False(result.Success);

    }
}
