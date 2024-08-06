﻿using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.IData
{
    public interface IProductDataStore
    {
        public Product GetProduct(string productIdentifier);
    }
}
