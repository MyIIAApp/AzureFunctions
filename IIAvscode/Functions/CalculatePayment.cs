using System;
using System.Collections.Generic;
using System.IO;
using ExcelDataReader;

namespace IIABackend
{
    /// <summary>
    /// Calculate Payment
    /// </summary>
    public static class CalculatePayment
    {
        /// <summary>
        /// Calculate Payment
        /// </summary>
        /// <param name="profileStatus">ProfileStatus</param>
        /// <param name="annualTurnover">Annual TurnOver</param>
        /// <param name="gstin">GSTIN</param>
        /// <returns>PaymentDetails</returns>
        public static PaymentDetail CalculateMembershipPayment(int profileStatus, string annualTurnover, string gstin)
        {
            int membershipFee, igst = 0, cgst, sgst = 0, admissionFee = 0;
            if (profileStatus < 5)
            {
                admissionFee = 600;
            }

            if (annualTurnover == "Rs 1 Crore - 3 Crore")
            {
                membershipFee = 7000;
            }
            else if (annualTurnover == "Above 3 Crore")
            {
                membershipFee = 10000;
            }
            else
            {
                membershipFee = 3000;
            }

            cgst = ((membershipFee + admissionFee) / 100) * 9;

            if (gstin.Length > 2 && gstin.StartsWith("09"))
            {
                sgst = cgst;
            }
            else
            {
                igst = cgst;
            }

            return new PaymentDetail(membershipFee, cgst, sgst, igst, admissionFee);
        }
    }
}