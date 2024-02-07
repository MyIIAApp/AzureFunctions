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
        /// <param name="userProfile">userProfile</param>
        /// <returns>PaymentDetails</returns>
        public static PaymentDetail CalculateMembershipPayment(UserProfile userProfile)
        {
            int profileStatus = userProfile.ProfileStatus;
            string annualTurnover = userProfile.AnnualTurnOver != null ? userProfile.AnnualTurnOver : string.Empty;
            string gstin = userProfile.GSTIN != null ? userProfile.GSTIN : string.Empty;
            string state = userProfile.State != null ? userProfile.State : string.Empty;
            double membershipFee, igst = 0, cgst, sgst = 0, admissionFee = 0;
            if (profileStatus < 5)
            {
                admissionFee = 600;
            }

            if (annualTurnover == "Rs 1 Crore - 3 Crore")
            {
                membershipFee = 5500;
            }
            else if (annualTurnover == "Above 3 Crore")
            {
                membershipFee = 8500;
            }
            else if (annualTurnover.Contains("Other"))
            {
                admissionFee = 2000;
                membershipFee = 20000;
            }
            else
            {
                membershipFee = 3000;
            }

            cgst = ((membershipFee + admissionFee) / 100) * 9;
            string checkgst = string.Empty;
            /*NEW GST Calculation Logic for Membership*/
            if (gstin != null && gstin.Length == 15)
            {
                checkgst = gstin.Remove(2);
                if ((checkgst == "07" && (userProfile.ChapterName == "Delhi" || userProfile.ChapterName == "New Delhi")) || (checkgst == "05" && (userProfile.ChapterName == "Uttarakhand State" || userProfile.ChapterName == "Roorkee" || userProfile.ChapterName == "Dehradoon ( Uttarakhand )" || userProfile.ChapterName == "Haridwar ( Uttarakhand )" || userProfile.ChapterName == "Nainital" || userProfile.ChapterName == "Udham Singh Nagar ( Uttarakhand )")) || (checkgst == "09" && (state == "Uttar Pradesh" || userProfile.ChapterName == "Head Office")))
                {
                    sgst = cgst;
                }
                else
                {
                    igst = 2 * cgst;
                    cgst = 0;
                }
            }
            else if (gstin == null || gstin.Length < 15)
            {
                if (state == "Uttar Pradesh" || userProfile.ChapterName == "Delhi" || state == "Delhi" || userProfile.ChapterName == "Uttarakhand State" || userProfile.ChapterName == "Roorkee" || userProfile.ChapterName == "Dehradoon ( Uttarakhand )" || userProfile.ChapterName == "Haridwar ( Uttarakhand )" || userProfile.ChapterName == "Nainital" || userProfile.ChapterName == "Udham Singh Nagar ( Uttarakhand )" || state == "New Delhi")
                {
                    sgst = cgst;
                }
                else
                {
                    igst = 2 * cgst;
                    cgst = 0;
                }
            }

            return new PaymentDetail(membershipFee, cgst, sgst, igst, admissionFee);
        }
    }
}