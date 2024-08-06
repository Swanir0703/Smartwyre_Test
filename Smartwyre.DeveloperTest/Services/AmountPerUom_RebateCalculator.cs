using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class AmountPerUom_RebateCalculator : IRebateCalculator
    {
        public bool CalculateRebate(CalculateRebateRequest request, Rebate rebate, Product product,out decimal rebateAmount)
        {
            bool result = false;
            rebateAmount = 0m;
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
            {
                result = false;
            }
            else if (rebate.Amount == 0 || request.Volume == 0)
            {
                result = false;
            }
            else
            {
                rebateAmount += rebate.Amount * request.Volume;
                result = true;
            }
            return result;
        }
    }
}
