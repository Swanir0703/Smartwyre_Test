using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class FixedCashAmount_RebateCalculator : IRebateCalculator
    {
        public bool CalculateRebate(CalculateRebateRequest request, Rebate rebate, Product product, out decimal rebateAmount)
        {
            bool result = false;
             rebateAmount = 0;
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
            {
                result = false;
            }
            else if (rebate.Amount == 0)
            {
                result = false;
            }
            else
            {
                rebateAmount = rebate.Amount;
                result = true;
            }
            return result;
        }
    }
}
