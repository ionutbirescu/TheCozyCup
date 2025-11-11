using System;
using System.Collections.Generic;
using System.Text;

namespace TheCozyCup
{
    internal interface ITransaction
    {
        decimal GetSubTotal();
        decimal CalculateFinalTotal();
        decimal ApplyDiscount(decimal discountPercentage);
        bool IsFinalized { get; set; }
    }
}