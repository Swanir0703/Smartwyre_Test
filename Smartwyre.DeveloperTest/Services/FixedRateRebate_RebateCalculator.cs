using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class FixedRateRebate_RebateCalculator : IRebateCalculator
    {
        public bool CalculateRebate(CalculateRebateRequest request, Rebate rebate, Product product, out decimal rebateAmount)
        {
            bool result=false;
            rebateAmount = 0m;
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
            {
                result = false;
            }
            else if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
            {
                result = false;
            }
            else
            {
                rebateAmount += product.Price * rebate.Percentage * request.Volume;
                result = true;
            }
            return result;
        }
    }
}
