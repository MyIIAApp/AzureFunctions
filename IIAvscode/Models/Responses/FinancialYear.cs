using System;
using System.Collections.Generic;
using System.Text;

namespace IIABackend
{
    /// <summary>
    /// Base response class
    /// </summary>
    public class FinancialYear
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialYear"/> class.
        /// </summary>
        /// <param name="startYear">startYear</param>
        /// <param name="expiryYear">expiryYear</param>
        public FinancialYear(int startYear, int expiryYear)
        {
            this.StartYear = startYear;
            this.ExpiryYear = expiryYear;
        }

        /// <summary>
        /// Gets or sets StartYear
        /// </summary>
        public int StartYear { get; set; }

        /// <summary>
        /// Gets or sets ExpiryYear
        /// </summary>
        public int ExpiryYear { get; set; }
    }
}
