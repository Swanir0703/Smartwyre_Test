using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{

    /// <summary>
    /// This Interface will implement calculateRebate and must be implemented by every incetive, so that it is designed to function according to the incentive
    /// Any newly added incentive must implement this interface and implement the function to behave in it's required way.
    /// </summary>
    public interface IRebateCalculator 
    {
        bool CalculateRebate(CalculateRebateRequest request,Rebate rebate,Product product,out decimal rebateAmount);
    }
}
