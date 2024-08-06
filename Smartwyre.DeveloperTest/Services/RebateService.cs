using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.IData;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    readonly IRebateDataStore rebateDataStore;
    readonly IProductDataStore productDataStore;

    public RebateService(IRebateDataStore _rebateDataStore, IProductDataStore _productDataStore)
    {
        rebateDataStore = _rebateDataStore;
        productDataStore = _productDataStore;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        #region OldCode
        //var rebateDataStore = new RebateDataStore();
        //var productDataStore = new ProductDataStore();
        #endregion
        Rebate rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = productDataStore.GetProduct(request.ProductIdentifier);

        var result = new CalculateRebateResult();
        if (rebate == null || product == null)
        {
            result.Success = false;
            return result;
        }

        var rebateAmount = 0m;
        //create object of required insentive
       
        IRebateCalculator rebateCalculator = CreateIncentiveType.Create(rebate.Incentive);
        result.Success= rebateCalculator.CalculateRebate(request,rebate,product,out rebateAmount);

        #region old code
        //switch (rebate.Incentive)
        //{
        //    case IncentiveType.FixedCashAmount:
        //        if (rebate == null)
        //        {
        //            result.Success = false;
        //        }
        //        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
        //        {
        //            result.Success = false;
        //        }
        //        else if (rebate.Amount == 0)
        //        {
        //            result.Success = false;
        //        }
        //        else
        //        {
        //            rebateAmount = rebate.Amount;
        //            result.Success = true;
        //        }
        //        break;

        //    case IncentiveType.FixedRateRebate:
        //        if (rebate == null)
        //        {
        //            result.Success = false;
        //        }
        //        else if (product == null)
        //        {
        //            result.Success = false;
        //        }
        //        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
        //        {
        //            result.Success = false;
        //        }
        //        else if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
        //        {
        //            result.Success = false;
        //        }
        //        else
        //        {
        //            rebateAmount += product.Price * rebate.Percentage * request.Volume;
        //            result.Success = true;
        //        }
        //        break;

        //    case IncentiveType.AmountPerUom:
        //        if (rebate == null)
        //        {
        //            result.Success = false;
        //        }
        //        else if (product == null)
        //        {
        //            result.Success = false;
        //        }
        //        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
        //        {
        //            result.Success = false;
        //        }
        //        else if (rebate.Amount == 0 || request.Volume == 0)
        //        {
        //            result.Success = false;
        //        }
        //        else
        //        {
        //            rebateAmount += rebate.Amount * request.Volume;
        //            result.Success = true;
        //        }
        //        break;
        //}
        #endregion
        if (result.Success)
        {
            #region oldcode
            //var storeRebateDataStore = new RebateDataStore();
            //storeRebateDataStore.StoreCalculationResult(rebate, rebateAmount);
            #endregion

            rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
        }

        return result;
    }



}

public static class CreateIncentiveType
{
    public static IRebateCalculator Create(IncentiveType incentiveType)
    {
        switch (incentiveType)
        {
            case IncentiveType.AmountPerUom: return new AmountPerUom_RebateCalculator();
            case IncentiveType.FixedRateRebate: return new FixedRateRebate_RebateCalculator();
            case IncentiveType.FixedCashAmount: return new FixedCashAmount_RebateCalculator();
            //Add new case if new incentive is added in future.
            default: return null;
        }
    }
}
