using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IIABackend
{
    /// <summary>
    /// Class to manange all Cosmos DB functions.
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="phoneNumber">Phone Number of the User.</param>
        /// <returns>id of the new user</returns>
        public static int CreateUserIfNotExists(string phoneNumber)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_CreateUserIfNotExists", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = phoneNumber;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            return rdr.GetInt32(0);
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Get user.
        /// </summary>
        /// <param name="userId">userId of the User.</param>
        /// <param name="phoneNumber">phoneNumber of the User.</param>
        /// <param name="memberId">memberId of the member.</param>
        /// <returns>id of the new user</returns>
        public static List<Membership> GetMembershipDetails(dynamic userId, string phoneNumber, string memberId)
        {
            var membershipList = new List<Membership>();
            UserProfile userProfile = GetUserProfile(userId, phoneNumber, memberId);
            if (userProfile.Id != -1 && userProfile.ProfileStatus > 4)
            {
                string[] membershipYears = userProfile.MembershipExpiryYears.Split(",");
                foreach (string x in membershipYears)
                {
                    int expiryYear = int.Parse(x);
                    Membership membership = new Membership(userProfile.Id, userProfile.FirstName, userProfile.LastName, userProfile.Email, userProfile.MembershipId, userProfile.ChapterId, userProfile.ChapterName, userProfile.MembershipAdmissionfee, userProfile.MembershipFees, expiryYear, userProfile.MembershipRenewDate, userProfile.MembershipJoinDate, userProfile.CreatedBy);
                    if (expiryYear != userProfile.MembershipCurrentExpiryYear)
                    {
                        membership.MembershipRenewDate = DateTime.Parse("01/04/" + (expiryYear - 1).ToString());
                    }

                    membershipList.Add(membership);
                }
            }

            return membershipList;
        }

        /// <summary>
        /// Is Membership Active
        /// </summary>
        /// <param name="userId">userId of the User.</param>
        /// <param name="phoneNumber">phoneNumber of the User.</param>
        /// <param name="memberId">memberId of the member.</param>
        /// <returns>id of the new user</returns>
        public static ActiveMembershipResponse GetActiveMembership(dynamic userId, string phoneNumber, string memberId)
        {
            UserProfile userProfile = GetUserProfile(userId, phoneNumber, memberId);
            if (userProfile.Id != -1 && userProfile.ProfileStatus > 4)
            {
                ActiveMembershipResponse response = new ActiveMembershipResponse(userProfile.Id, userProfile.FirstName, userProfile.LastName, userProfile.Email, userProfile.MembershipId, userProfile.ChapterId, userProfile.ChapterName, userProfile.MembershipFees, DateTime.Parse(userProfile.MembershipCurrentExpiryYear.ToString() + "-03-31"), userProfile.MembershipJoinDate, 0);
                if ((DateTime.Parse(userProfile.MembershipCurrentExpiryYear.ToString() + "-03-31") - DateTime.Now).TotalDays > 0)
                {
                    response.MembershipStatus = (int)UserProfileStatusEnum.UserProfileStatus.ActiveMembership;
                }
                else if ((DateTime.Parse((userProfile.MembershipCurrentExpiryYear + 1).ToString() + "-03-31") - DateTime.Now).TotalDays > 0)
                {
                    response.MembershipStatus = (int)UserProfileStatusEnum.UserProfileStatus.ActiveGraceMembership;
                }
                else
                {
                    response.MembershipStatus = (int)UserProfileStatusEnum.UserProfileStatus.ExpiredMembership;
                }

                return response;
            }
            else
            {
                return new ActiveMembershipResponse(-1);
            }
        }

        /// <summary>
        /// Creates a new Membership
        /// </summary>
        /// <param name="membership">membership Object</param>
        /// <param name="membershipExpiryYears">membershipExpiryYears</param>
        /// <param name="adminId"> AdminId</param>
        public static void InsertMembership(Membership membership, string membershipExpiryYears, int adminId)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("UserProfile_Membership_UpdateDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = membership.UserId;
                        cmd.Parameters.Add("@ProfileStatus", SqlDbType.Int).Value = (int)UserProfileStatusEnum.UserProfileStatus.ActiveMembership;
                        cmd.Parameters.Add("@MembershipId", SqlDbType.VarChar).Value = membership.MembershipId != null ? membership.MembershipId : (100000 + membership.UserId).ToString();
                        cmd.Parameters.Add("@MembershipAdmissionfee", SqlDbType.Int).Value = membership.MembershipAdmissionfee;
                        cmd.Parameters.Add("@MembershipFees", SqlDbType.Int).Value = membership.MembershipFees;
                        cmd.Parameters.Add("@MembershipCurrentExpiryYear", SqlDbType.Int).Value = membership.MembershipCurrentExpiryYear;
                        cmd.Parameters.Add("@MembershipJoinDate", SqlDbType.DateTime).Value = membership.MembershipJoinDate;
                        cmd.Parameters.Add("@MembershipRenewDate", SqlDbType.DateTime).Value = membership.MembershipRenewDate;
                        cmd.Parameters.Add("@MembershipExpiryYears", SqlDbType.Text).Value = membershipExpiryYears;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = adminId;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Get user.
        /// </summary>
        /// <param name="userId">Id of the User.</param>
        /// <param name="profileStatus">ProfileStatus</param>
        /// <param name="chapter">Chapter</param>
        /// <returns>company profile of user</returns>
        public static List<MembershipProfile> GetMembershipProfile(int userId, dynamic profileStatus, dynamic chapter)
        {
            var membershipProfileList = new List<MembershipProfile>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_MembershipProfile_GetDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@ProfileStatus", SqlDbType.Int).Value = profileStatus != null ? profileStatus : 0;
                    cmd.Parameters.Add("@chapter", SqlDbType.Int).Value = chapter != null ? chapter : 0;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            MembershipProfile membershipProfile = new MembershipProfile(
                               rdr.GetInt32(0),
                               rdr.IsDBNull(1) ? 0 : rdr.GetInt32(1),
                               rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2),
                               rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3),
                               rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4),
                               rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5),
                               rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6),
                               rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7),
                               rdr.IsDBNull(8) ? string.Empty : rdr.GetValue(8).ToString(),
                               rdr.IsDBNull(9) ? string.Empty : rdr.GetString(9),
                               rdr.IsDBNull(10) ? string.Empty : rdr.GetString(10),
                               rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11),
                               rdr.IsDBNull(12) ? string.Empty : rdr.GetString(12),
                               rdr.IsDBNull(13) ? string.Empty : rdr.GetString(13),
                               rdr.IsDBNull(14) ? string.Empty : rdr.GetString(14),
                               rdr.IsDBNull(15) ? string.Empty : rdr.GetString(15),
                               rdr.IsDBNull(16) ? string.Empty : rdr.GetString(16),
                               rdr.IsDBNull(17) ? string.Empty : rdr.GetString(17),
                               rdr.IsDBNull(18) ? string.Empty : rdr.GetString(18),
                               rdr.IsDBNull(19) ? string.Empty : rdr.GetString(19),
                               rdr.IsDBNull(20) ? string.Empty : rdr.GetString(20),
                               rdr.IsDBNull(21) ? string.Empty : rdr.GetString(21),
                               rdr.IsDBNull(22) ? string.Empty : rdr.GetString(22),
                               rdr.IsDBNull(23) ? string.Empty : rdr.GetString(23),
                               rdr.IsDBNull(24) ? string.Empty : rdr.GetString(24),
                               rdr.IsDBNull(25) ? string.Empty : rdr.GetString(25),
                               rdr.IsDBNull(26) ? string.Empty : rdr.GetString(26),
                               rdr.IsDBNull(27) ? string.Empty : rdr.GetString(27),
                               rdr.IsDBNull(28) ? 0 : rdr.GetInt32(28),
                               rdr.IsDBNull(29) ? DateTime.Now : rdr.GetDateTime(29),
                               rdr.IsDBNull(30) ? DateTime.Now : rdr.GetDateTime(30));
                            membershipProfileList.Add(membershipProfile);
                        }
                    }
                }
            }

            return membershipProfileList;
        }

        /// <summary>
        /// Get user.
        /// </summary>
        /// <param name="phoneNumber">phoneNumber of the User.</param>
        /// <returns>company profile of user</returns>
        public static MembershipProfile GetMembershipProfileByNumber(string phoneNumber)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_MembershipProfile_GetDetails_ByPhoneNumber", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = phoneNumber;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            MembershipProfile membershipProfile = new MembershipProfile(
                                rdr.GetInt32(0),
                                rdr.IsDBNull(1) ? 0 : rdr.GetInt32(1),
                                rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2),
                                rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3),
                                rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4),
                                rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5),
                                rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6),
                                rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7),
                                rdr.IsDBNull(8) ? string.Empty : rdr.GetValue(8).ToString(),
                                rdr.IsDBNull(9) ? string.Empty : rdr.GetString(9),
                                rdr.IsDBNull(10) ? string.Empty : rdr.GetString(10),
                                rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11),
                                rdr.IsDBNull(12) ? string.Empty : rdr.GetString(12),
                                rdr.IsDBNull(13) ? string.Empty : rdr.GetString(13),
                                rdr.IsDBNull(14) ? string.Empty : rdr.GetString(14),
                                rdr.IsDBNull(15) ? string.Empty : rdr.GetString(15),
                                rdr.IsDBNull(16) ? string.Empty : rdr.GetString(16),
                                rdr.IsDBNull(17) ? string.Empty : rdr.GetString(17),
                                rdr.IsDBNull(18) ? string.Empty : rdr.GetString(18),
                                rdr.IsDBNull(19) ? string.Empty : rdr.GetString(19),
                                rdr.IsDBNull(20) ? string.Empty : rdr.GetString(20),
                                rdr.IsDBNull(21) ? string.Empty : rdr.GetString(21),
                                rdr.IsDBNull(22) ? string.Empty : rdr.GetString(22),
                                rdr.IsDBNull(23) ? string.Empty : rdr.GetString(23),
                                rdr.IsDBNull(24) ? string.Empty : rdr.GetString(24),
                                rdr.IsDBNull(25) ? string.Empty : rdr.GetString(25),
                                rdr.IsDBNull(26) ? string.Empty : rdr.GetString(26),
                                rdr.IsDBNull(27) ? string.Empty : rdr.GetString(27),
                                rdr.IsDBNull(28) ? 0 : rdr.GetInt32(28),
                                rdr.IsDBNull(29) ? DateTime.Now : rdr.GetDateTime(29),
                                rdr.IsDBNull(30) ? DateTime.Now : rdr.GetDateTime(30));
                            return membershipProfile;
                        }
                    }
                }
            }

            return new MembershipProfile(-1);
        }

        /// <summary>
        /// Get user.
        /// </summary>
        /// <param name="userId">Id of the User.</param>
        /// <param name="phoneNumber">PhoneNumber the User.</param>
        /// <param name="membershipId">MembershipId.</param>
        /// <returns>UserProfile of user</returns>
        public static UserProfile GetUserProfile(dynamic userId, string phoneNumber, string membershipId)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_GetDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = userId != null ? userId : 0;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = phoneNumber != null ? phoneNumber : string.Empty;
                    cmd.Parameters.Add("@MembershipId", SqlDbType.VarChar).Value = membershipId != null ? membershipId : string.Empty;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            UserProfile userProfile = new UserProfile(
                                rdr.GetInt32(0),
                                rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                                rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2),
                                rdr.IsDBNull(3) ? 0 : rdr.GetInt32(3),
                                rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4),
                                rdr.IsDBNull(5) ? 0 : rdr.GetInt32(5),
                                rdr.IsDBNull(6) ? DateTime.MinValue : rdr.GetDateTime(6),
                                rdr.IsDBNull(7) ? DateTime.MinValue : rdr.GetDateTime(7),
                                rdr.IsDBNull(8) ? string.Empty : rdr.GetString(8),
                                rdr.IsDBNull(9) ? 0 : rdr.GetInt32(9),
                                rdr.IsDBNull(10) ? 0 : rdr.GetInt32(10),
                                rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11),
                                rdr.IsDBNull(12) ? string.Empty : rdr.GetString(12),
                                rdr.IsDBNull(13) ? string.Empty : rdr.GetString(13),
                                rdr.IsDBNull(14) ? string.Empty : rdr.GetString(14),
                                rdr.IsDBNull(15) ? string.Empty : rdr.GetString(15),
                                rdr.IsDBNull(16) ? string.Empty : rdr.GetString(16),
                                rdr.IsDBNull(17) ? string.Empty : rdr.GetString(17),
                                rdr.IsDBNull(18) ? string.Empty : rdr.GetString(18),
                                rdr.IsDBNull(19) ? string.Empty : rdr.GetString(19),
                                rdr.IsDBNull(20) ? string.Empty : rdr.GetString(20),
                                rdr.IsDBNull(21) ? string.Empty : rdr.GetString(21),
                                rdr.IsDBNull(22) ? string.Empty : rdr.GetString(22),
                                rdr.IsDBNull(23) ? string.Empty : rdr.GetString(23),
                                rdr.IsDBNull(24) ? string.Empty : rdr.GetString(24),
                                rdr.IsDBNull(25) ? string.Empty : rdr.GetString(25),
                                rdr.IsDBNull(26) ? string.Empty : rdr.GetString(26),
                                rdr.IsDBNull(27) ? string.Empty : rdr.GetString(27),
                                rdr.IsDBNull(28) ? string.Empty : rdr.GetString(28),
                                rdr.IsDBNull(29) ? string.Empty : rdr.GetString(29),
                                rdr.IsDBNull(30) ? string.Empty : rdr.GetString(30),
                                rdr.IsDBNull(31) ? string.Empty : rdr.GetString(31),
                                rdr.IsDBNull(32) ? string.Empty : rdr.GetString(32),
                                rdr.IsDBNull(33) ? string.Empty : rdr.GetString(33),
                                rdr.IsDBNull(34) ? string.Empty : rdr.GetString(34),
                                rdr.IsDBNull(35) ? string.Empty : rdr.GetString(35),
                                rdr.IsDBNull(36) ? 0 : rdr.GetInt32(36),
                                rdr.IsDBNull(37) ? 0 : rdr.GetInt32(37),
                                rdr.IsDBNull(38) ? DateTime.MinValue : rdr.GetDateTime(38),
                                rdr.IsDBNull(39) ? DateTime.MinValue : rdr.GetDateTime(39));
                            return userProfile;
                        }
                    }
                }
            }

            return new UserProfile(-1);
        }
        
/// <summary>
        /// Store Payment Details
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="adminId">adminId</param>
        /// <param name="subTotal">subTotal</param>
        /// <param name="cgst">CGST Rate</param>
        /// <param name="sgst">SGST Rate</param>
        /// <param name="igst">IGST Rate</param>
        /// <param name="igstValue">IGST Amount</param>
        /// <param name="sgstValue">SGST Amount</param>
        /// <param name="cgstValue">CGST Amount</param>
        /// <param name="paymentReason">PaymentReason</param>
        /// <param name="paymentMode">Payment Mode</param>
        /// <param name="chequeNumber">Cheque Number</param>
        /// <param name="orderId">Order Id</param>
        /// <param name="total">Total Amount</param>
        /// <param name="currentTime">Current time</param>
        public static void StorePayment(int userId, int adminId, int subTotal, int cgst, int sgst, int igst, int igstValue, int sgstValue, int cgstValue, string paymentReason, string paymentMode, string chequeNumber, string orderId, int total, DateTime currentTime)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_StorePayment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@AdminId", SqlDbType.Int).Value = adminId;
                    cmd.Parameters.Add("@SubTotal", SqlDbType.Int).Value = subTotal;
                    cmd.Parameters.Add("@CGST", SqlDbType.Int).Value = cgst;
                    cmd.Parameters.Add("@SGST", SqlDbType.Int).Value = sgst;
                    cmd.Parameters.Add("@IGST", SqlDbType.Int).Value = igst;
                    cmd.Parameters.Add("@CGSTValue", SqlDbType.Int).Value = cgstValue;
                    cmd.Parameters.Add("@SGSTValue", SqlDbType.Int).Value = sgstValue;
                    cmd.Parameters.Add("@IGSTValue", SqlDbType.Int).Value = igstValue;
                    cmd.Parameters.Add("@PaymentReason", SqlDbType.NVarChar).Value = paymentReason;
                    cmd.Parameters.Add("@PaymentMode", SqlDbType.NVarChar).Value = paymentMode;
                    cmd.Parameters.Add("@ChequeNumber", SqlDbType.NVarChar).Value = chequeNumber;
                    cmd.Parameters.Add("@OrderId", SqlDbType.NVarChar).Value = orderId;
                    cmd.Parameters.Add("@Total", SqlDbType.Int).Value = total;
                    cmd.Parameters.Add("@CurrentTime", SqlDbType.DateTime).Value = currentTime;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Update Invoice Path
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="adminId">adminId</param>
        /// <param name="url">URL of invoice</param>
        /// <param name="orderId">OrderId</param>
        public static void UpdateInvoicePath(int userId, int adminId, string url, string orderId)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_UpdateInvoicePath", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@AdminId", SqlDbType.Int).Value = adminId;
                    cmd.Parameters.Add("@URL", SqlDbType.NVarChar).Value = url;
                    cmd.Parameters.Add("@OrderId", SqlDbType.NVarChar).Value = orderId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Inserts/Update a user profile
        /// </summary>
        /// <param name="userProfile">UserCompanyProfile</param>
        /// <param name="adminId">adminId</param>
        public static void InsertUpdateUserProfile(UserProfile userProfile, int adminId)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("UserProfile_Insert_Update_Details", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = userProfile.Id;
                        cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = userProfile.PhoneNumber != null ? userProfile.PhoneNumber : string.Empty;
                        cmd.Parameters.Add("@MembershipId", SqlDbType.VarChar).Value = userProfile.MembershipId != null ? userProfile.MembershipId : string.Empty;
                        cmd.Parameters.Add("@MembershipAdmissionfee", SqlDbType.Int).Value = userProfile.MembershipFees;
                        cmd.Parameters.Add("@MembershipFees", SqlDbType.Int).Value = userProfile.MembershipFees;
                        cmd.Parameters.Add("@MembershipCurrentExpiryYear", SqlDbType.Int).Value = userProfile.MembershipCurrentExpiryYear;
                        cmd.Parameters.Add("@MembershipJoinDate", SqlDbType.DateTime).Value = userProfile.MembershipJoinDate;
                        cmd.Parameters.Add("@MembershipRenewDate", SqlDbType.DateTime).Value = userProfile.MembershipRenewDate;
                        cmd.Parameters.Add("@MembershipExpiryYears", SqlDbType.Text).Value = userProfile.MembershipExpiryYears != null ? userProfile.MembershipExpiryYears : string.Empty;
                        cmd.Parameters.Add("@ProfileStatus", SqlDbType.Int).Value = userProfile.ProfileStatus;
                        cmd.Parameters.Add("@Chapter", SqlDbType.Int).Value = userProfile.ChapterId;
                        cmd.Parameters.Add("@UnitName", SqlDbType.VarChar).Value = userProfile.UnitName != null ? userProfile.UnitName : string.Empty;
                        cmd.Parameters.Add("@GSTIN", SqlDbType.VarChar).Value = userProfile.GSTIN != null ? userProfile.GSTIN : string.Empty;
                        cmd.Parameters.Add("@GSTcertpath", SqlDbType.VarChar).Value = userProfile.GSTcertpath != null ? userProfile.GSTcertpath : string.Empty;
                        cmd.Parameters.Add("@IndustryStatus", SqlDbType.VarChar).Value = userProfile.IndustryStatus != null ? userProfile.IndustryStatus : string.Empty;
                        cmd.Parameters.Add("@Address", SqlDbType.Text).Value = userProfile.Address != null ? userProfile.Address : string.Empty;
                        cmd.Parameters.Add("@District", SqlDbType.VarChar).Value = userProfile.District != null ? userProfile.District : string.Empty;
                        cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = userProfile.City != null ? userProfile.City : string.Empty;
                        cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = userProfile.State != null ? userProfile.State : string.Empty;
                        cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = userProfile.Country != null ? userProfile.Country : string.Empty;
                        cmd.Parameters.Add("@Pincode", SqlDbType.VarChar).Value = userProfile.Pincode != null ? userProfile.Pincode : string.Empty;
                        cmd.Parameters.Add("@WebsiteUrl", SqlDbType.VarChar).Value = userProfile.WebsiteUrl != null ? userProfile.WebsiteUrl : string.Empty;
                        cmd.Parameters.Add("@ProductCategory", SqlDbType.VarChar).Value = userProfile.ProductCategory != null ? userProfile.ProductCategory : string.Empty;
                        cmd.Parameters.Add("@ProductSubCategory", SqlDbType.VarChar).Value = userProfile.ProductSubCategory != null ? userProfile.ProductSubCategory : string.Empty;
                        cmd.Parameters.Add("@MajorProducts", SqlDbType.Text).Value = userProfile.MajorProducts != null ? userProfile.MajorProducts : string.Empty;
                        cmd.Parameters.Add("@AnnualTurnOver", SqlDbType.VarChar).Value = userProfile.AnnualTurnOver != null ? userProfile.AnnualTurnOver : string.Empty;
                        cmd.Parameters.Add("@EnterpriseType", SqlDbType.VarChar).Value = userProfile.EnterpriseType != null ? userProfile.EnterpriseType : string.Empty;
                        cmd.Parameters.Add("@Exporter", SqlDbType.VarChar).Value = userProfile.Exporter != null ? userProfile.Exporter : string.Empty;
                        cmd.Parameters.Add("@Classification", SqlDbType.VarChar).Value = userProfile.Classification != null ? userProfile.Classification : string.Empty;
                        cmd.Parameters.Add("@ProfileImagePath", SqlDbType.VarChar).Value = userProfile.ProfileImagePath != null ? userProfile.ProfileImagePath : string.Empty;
                        cmd.Parameters.Add("@FinancialProofPath", SqlDbType.VarChar).Value = userProfile.FinancialProofPath != null ? userProfile.FinancialProofPath : string.Empty;
                        cmd.Parameters.Add("@SignaturePath", SqlDbType.VarChar).Value = userProfile.SignaturePath != null ? userProfile.SignaturePath : string.Empty;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = adminId;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = adminId;
                        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = userProfile.FirstName != null ? userProfile.FirstName : string.Empty;
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = userProfile.LastName != null ? userProfile.LastName : string.Empty;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = userProfile.Email != null ? userProfile.Email : string.Empty;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        /// <summary>
        /// Remove duplicate from table
        /// </summary>
        
        public static void RemoveDuplicate()
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("DuplicateRemoval", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Updates a MembershipProfile Details
        /// </summary>
        /// <param name="membershipProfile">MembershipProfile</param>
        /// <param name="adminId">adminId</param>
        public static void UpdateMembershipProfile(MembershipProfile membershipProfile, int adminId)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_Company_UpdateDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = membershipProfile.Id;
                    cmd.Parameters.Add("@ProfileStatus", SqlDbType.Int).Value = membershipProfile.ProfileStatus;
                    cmd.Parameters.Add("@Chapter", SqlDbType.Int).Value = membershipProfile.ChapterId;
                    cmd.Parameters.Add("@UnitName", SqlDbType.VarChar).Value = membershipProfile.UnitName != null ? membershipProfile.UnitName : string.Empty;
                    cmd.Parameters.Add("@GSTIN", SqlDbType.VarChar).Value = membershipProfile.GSTIN != null ? membershipProfile.GSTIN : string.Empty;
                    cmd.Parameters.Add("@GSTcertpath", SqlDbType.VarChar).Value = membershipProfile.GSTcertpath != null ? membershipProfile.GSTcertpath : string.Empty;
                    cmd.Parameters.Add("@IndustryStatus", SqlDbType.VarChar).Value = membershipProfile.IndustryStatus != null ? membershipProfile.IndustryStatus : string.Empty;
                    cmd.Parameters.Add("@Address", SqlDbType.Text).Value = membershipProfile.Address != null ? membershipProfile.Address : string.Empty;
                    cmd.Parameters.Add("@District", SqlDbType.VarChar).Value = membershipProfile.District != null ? membershipProfile.District : string.Empty;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = membershipProfile.City != null ? membershipProfile.City : string.Empty;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = membershipProfile.State != null ? membershipProfile.State : string.Empty;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = membershipProfile.Country != null ? membershipProfile.Country : string.Empty;
                    cmd.Parameters.Add("@Pincode", SqlDbType.VarChar).Value = membershipProfile.Pincode != null ? membershipProfile.Pincode : string.Empty;
                    cmd.Parameters.Add("@WebsiteUrl", SqlDbType.VarChar).Value = membershipProfile.WebsiteUrl != null ? membershipProfile.WebsiteUrl : string.Empty;
                    cmd.Parameters.Add("@ProductCategory", SqlDbType.VarChar).Value = membershipProfile.ProductCategory != null ? membershipProfile.ProductCategory : string.Empty;
                    cmd.Parameters.Add("@ProductSubCategory", SqlDbType.VarChar).Value = membershipProfile.ProductSubCategory != null ? membershipProfile.ProductSubCategory : string.Empty;
                    cmd.Parameters.Add("@MajorProducts", SqlDbType.Text).Value = membershipProfile.MajorProducts != null ? membershipProfile.MajorProducts : string.Empty;
                    cmd.Parameters.Add("@AnnualTurnOver", SqlDbType.VarChar).Value = membershipProfile.AnnualTurnOver != null ? membershipProfile.AnnualTurnOver : string.Empty;
                    cmd.Parameters.Add("@EnterpriseType", SqlDbType.VarChar).Value = membershipProfile.EnterpriseType != null ? membershipProfile.EnterpriseType : string.Empty;
                    cmd.Parameters.Add("@Exporter", SqlDbType.VarChar).Value = membershipProfile.Exporter != null ? membershipProfile.Exporter : string.Empty;
                    cmd.Parameters.Add("@Classification", SqlDbType.VarChar).Value = membershipProfile.Classification != null ? membershipProfile.Classification : string.Empty;
                    cmd.Parameters.Add("@ProfileImagePath", SqlDbType.VarChar).Value = membershipProfile.ProfileImagePath != null ? membershipProfile.ProfileImagePath : string.Empty;
                    cmd.Parameters.Add("@FinancialProofPath", SqlDbType.VarChar).Value = membershipProfile.FinancialProofPath != null ? membershipProfile.FinancialProofPath : string.Empty;
                    cmd.Parameters.Add("@SignaturePath", SqlDbType.VarChar).Value = membershipProfile.SignaturePath != null ? membershipProfile.SignaturePath : string.Empty;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = adminId;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = membershipProfile.FirstName != null ? membershipProfile.FirstName : string.Empty;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = membershipProfile.LastName != null ? membershipProfile.LastName : string.Empty;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = membershipProfile.Email != null ? membershipProfile.Email : string.Empty;
                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Inserts a Payment
        /// </summary>
        /// <param name="payment">Payment</param>
        /// <returns>id</returns>
        public static int InsertPayment(Payment payment)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("Payment_InsertDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = payment.UserId;
                        cmd.Parameters.Add("@AdminId", SqlDbType.Int).Value = payment.AdminId;
                        cmd.Parameters.Add("@SubTotal", SqlDbType.Int).Value = payment.SubTotal;
                        cmd.Parameters.Add("@IGSTPercent", SqlDbType.Int).Value = payment.IGSTPercent;
                        cmd.Parameters.Add("@CGSTPercent", SqlDbType.Int).Value = payment.CGSTPercent;
                        cmd.Parameters.Add("@SGSTPercent", SqlDbType.Int).Value = payment.SGSTPercent;
                        cmd.Parameters.Add("@IGSTValue", SqlDbType.Int).Value = payment.IGSTValue;
                        cmd.Parameters.Add("@CGSTValue", SqlDbType.Int).Value = payment.CGSTValue;
                        cmd.Parameters.Add("@SGSTValue", SqlDbType.Int).Value = payment.SGSTValue;
                        cmd.Parameters.Add("@PaymentReason", SqlDbType.VarChar).Value = payment.PaymentReason;
                        cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = payment.PaymentMode;
                        cmd.Parameters.Add("@ChequeNumber", SqlDbType.VarChar).Value = payment.ChequeNumber;
                        cmd.Parameters.Add("@OnlineTransactionId", SqlDbType.VarChar).Value = payment.OnlineTransactionId;
                        cmd.Parameters.Add("@OrderId", SqlDbType.VarChar).Value = payment.OrderId;
                        cmd.Parameters.Add("@DiscountPercent", SqlDbType.Int).Value = payment.DiscountPercent;
                        cmd.Parameters.Add("@DiscountValue", SqlDbType.Int).Value = payment.DiscountValue;
                        cmd.Parameters.Add("@Total", SqlDbType.Int).Value = payment.Total;
                        cmd.Parameters.Add("@OnlineFees", SqlDbType.Int).Value = payment.OnlineFees;
                        cmd.Parameters.Add("@HO_Share", SqlDbType.Int).Value = payment.HO_Share;
                        cmd.Parameters.Add("@ChapterShare", SqlDbType.Int).Value = payment.ChapterShare;
                        cmd.Parameters.Add("@InvoicePath", SqlDbType.VarChar).Value = payment.InvoicePath;
                        cmd.Parameters.Add("@CreateDateTimeStamp", SqlDbType.DateTime).Value = DateTime.Now;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                var sproc = "Payment_GetInvoiceId";
                var id = 0;

                using (var conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(sproc, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (!string.IsNullOrEmpty(payment.OrderId))
                        {
                            cmd.Parameters.Add("@orderId", SqlDbType.VarChar).Value = payment.OrderId;
                        }

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                id = rdr.GetInt32(0);
                            }
                        }
                    }
                }

                return id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Inserts a Payment
        /// </summary>
        /// <param name="invoicePath">invoicePath</param>
        /// <param name="orderId">orderId</param>
        public static void AddInvoicePath(string invoicePath, string orderId)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("Payment_InsertInvoicePath", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@invoicePath", SqlDbType.VarChar).Value = invoicePath;
                        cmd.Parameters.Add("@orderId", SqlDbType.VarChar).Value = orderId;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets PaymentUserId
        /// </summary>
        /// <param name="phoneNumber">phoneNumber</param>
        /// <returns>PaymentUserId</returns>
        public static PaymentUserId GetPaymentUserId(string phoneNumber)
        {
            var sproc = "UserProfile_Payments_GetPaymentUserId";

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sproc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        cmd.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;
                    }

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            PaymentUserId paymentCredential = new PaymentUserId(
                                rdr.IsDBNull(2) ? string.Empty : rdr.GetString(0),
                                rdr.GetInt32(1),
                                rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2),
                                rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3),
                                rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4));
                            return paymentCredential;
                        }
                    }
                }
            }

            return new PaymentUserId(string.Empty, -1, string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Creates a news
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="description">Description</param>
        /// <param name="sourceLink">Source Link</param>
        /// <param name="category">Category</param>
        /// <param name="imagePath">Image Path</param>
        /// <param name="creatorAdminId">Creator Admin ID</param>
        public static void CreateNews(string title, string description, string sourceLink, string category, string imagePath, int creatorAdminId)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("News_CreateNews", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = title;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = description != null ? description : string.Empty;
                    cmd.Parameters.Add("@Link", SqlDbType.VarChar).Value = sourceLink != null ? sourceLink : string.Empty;
                    cmd.Parameters.Add("@ImagePath", SqlDbType.VarChar).Value = imagePath;
                    cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = category;
                    cmd.Parameters.Add("@CreatorAdminId", SqlDbType.Int).Value = creatorAdminId;
                    cmd.Parameters.Add("@CreationTime", SqlDbType.DateTime).Value = DateTime.Now;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get all the news for the given category
        /// </summary>
        /// <param name="category">Category to filter</param>
        /// <returns>List of news items</returns>
        public static List<News> GetNews(string category)
        {
            var newsList = new List<News>();
            var sproc = "News_GetNewsForAllCategory";
            if (!string.IsNullOrEmpty(category))
            {
                sproc = "News_GetNewsForSpecificCategory";
            }

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sproc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(category))
                    {
                        cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = category;
                    }

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var news = new News(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetString(3).ToString(),
                                rdr.GetString(4).ToString(),
                                rdr.GetString(5).ToString(),
                                rdr.GetInt32(6),
                                rdr.GetDateTime(7),
                                rdr.GetString(8).ToString());
                            newsList.Add(news);
                        }
                    }
                }
            }

            return newsList;
        }

        /// <summary>
        /// Get all the Chapters
        /// </summary>
        /// <returns>List of Chapters</returns>
        public static List<Chapter> GetChapters()
        {
            var chaptersList = new List<Chapter>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Chapters_GetChapters", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var chapter = new Chapter(
                                rdr.GetInt32(0),
                                rdr.GetString(1).ToString());
                            chaptersList.Add(chapter);
                        }
                    }
                }
            }

            return chaptersList;
        }

        /// <summary>
        /// Get if admin exists or not
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns>List of Chapters</returns>
        public static int CheckIfAdminExistsOrNot(string phoneNumber)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_CheckIfAdminExists", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = phoneNumber;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            return rdr.GetInt32(0);
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Creates a Ticket
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="description">Description</param>
        /// <param name="category">Category</param>
        /// <param name="userid">User ID</param>
        /// <param name="attachmenturl">Attachment URL</param>
        /// <returns>Ticket Number</returns>
        public static string CreateTicket(string title, string description, string category, int userid, string attachmenturl)
        {
            string ticketNumber;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_Tickets_CreateTickets", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter outPutVal = new SqlParameter("@Number", SqlDbType.Int);

                    cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = title;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = description;
                    cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = category;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
                    cmd.Parameters.Add("@Number", SqlDbType.Int).Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    ticketNumber = cmd.Parameters["@Number"].Value.ToString();

                    if (!string.IsNullOrEmpty(attachmenturl))
                    {
                        AddAttachment(ticketNumber, userid, -1, attachmenturl);
                    }
                }
            }

            return ticketNumber;
        }

        /// <summary>
        /// Adds a comment to existing Ticket
        /// </summary>
        /// <param name="ticketNumber">TicketNumber</param>
        /// <param name="comments">Comment</param>
        /// <param name="userId">UserId</param>
        /// <param name="adminId">AdminId</param>
        public static void AddComment(string ticketNumber, string comments, int userId, int adminId)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_Tickets_AddComment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@TicketNumber", SqlDbType.Int).Value = ticketNumber;
                    cmd.Parameters.Add("@Comment", SqlDbType.VarChar).Value = comments;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@AdminId", SqlDbType.Int).Value = adminId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Adds an attachmetn to existing Ticket
        /// </summary>
        /// <param name="ticketNumber">TicketNumber</param>
        /// <param name="userId">UserId</param>
        /// <param name="adminId">AdminId</param>
        /// <param name="attachmenturl">AttachmentURL</param>
        public static void AddAttachment(string ticketNumber, int userId, int adminId, string attachmenturl)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Tickets_AddAttachment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@TicketNumber", SqlDbType.Int).Value = ticketNumber;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@AdminId", SqlDbType.Int).Value = adminId;
                    cmd.Parameters.Add("@AttachmentURL", SqlDbType.VarChar).Value = attachmenturl;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get Ticket Details for a particular ticket
        /// </summary>
        /// <param name="ticketnumber">TicketNumber</param>
        /// <returns>Ticket Details</returns>
        public static List<Tickets> GetTicket(string ticketnumber)
        {
            var ticketList = new List<Tickets>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Tickets_GetTicketDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(ticketnumber))
                    {
                        cmd.Parameters.Add("@TicketNumber", SqlDbType.Int).Value = ticketnumber;
                    }

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var comments = GetComment(ticketnumber);
                            var attachments = GetAttachment(ticketnumber);
                            var ticket = new Tickets(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetString(3),
                                rdr.GetInt32(4).ToString(),
                                rdr.GetString(5).ToString(),
                                rdr.GetDateTime(6),
                                rdr.GetInt32(7).ToString(),
                                comments,
                                attachments,
                                rdr.GetDateTime(8));
                            ticketList.Add(ticket);
                        }
                    }
                }
            }

            return ticketList;
        }

        /// <summary>
        /// Get Comments for a particular ticket
        /// </summary>
        /// <param name="ticketnumber">TicketNumber</param>
        /// <returns>Ticket comment Details</returns>
        public static List<Comment> GetComment(string ticketnumber)
        {
            var commentList = new List<Comment>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_Tickets_GetComments", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(ticketnumber))
                    {
                        cmd.Parameters.Add("@TicketNumber", SqlDbType.Int).Value = ticketnumber;
                    }

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var comment = new Comment(
                                rdr.GetInt32(0).ToString(),
                                SafeGetString(rdr, 1),
                                rdr.GetString(2).ToString(),
                                rdr.GetDateTime(3),
                                SafeGetString(rdr, 4));
                            commentList.Add(comment);
                        }
                    }
                }
            }

            return commentList;
        }

        /// <summary>
        /// Get Attachment for a particular ticket
        /// </summary>
        /// <param name="ticketnumber">TicketNumber</param>
        /// <returns>Ticket attachment Details</returns>
        public static List<Attachment> GetAttachment(string ticketnumber)
        {
            var attachmentList = new List<Attachment>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_Tickets_GetAttachment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(ticketnumber))
                    {
                        cmd.Parameters.Add("@TicketNumber", SqlDbType.Int).Value = ticketnumber;
                    }

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var attachment = new Attachment(
                                rdr.GetInt32(0).ToString(),
                                SafeGetString(rdr, 1),
                                SafeGetString(rdr, 2),
                                rdr.GetString(3).ToString(),
                                rdr.GetDateTime(4));
                            attachmentList.Add(attachment);
                        }
                    }
                }
            }

            return attachmentList;
        }

        /// <summary>
        /// Close an existing Ticket
        /// </summary>
        /// <param name="ticketNumber">TicketNumber</param>
        public static void CloseTicket(string ticketNumber)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Tickets_CloseTicket", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@TicketNumber", SqlDbType.Int).Value = ticketNumber;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Change chapter of an existing Ticket
        /// </summary>
        /// <param name="ticketnumber">TicketNumber</param>
        public static void ChangeChapter(string ticketnumber)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Tickets_ChangeChapter", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@TicketNumber", SqlDbType.Int).Value = ticketnumber;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get Ticket Summary for User
        /// </summary>
        /// <param name="userid">UserId</param>
        /// <returns>Ticket summary details</returns>
        public static List<Tickets> GetSummaryForUser(int userid)
        {
            var summaryList = new List<Tickets>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Tickets_GetSummaryForUsers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var ticket = new Tickets(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetString(3),
                                rdr.GetInt32(4).ToString(),
                                rdr.GetString(5).ToString(),
                                rdr.GetDateTime(6),
                                rdr.GetInt32(7).ToString(),
                                null,
                                null,
                                rdr.GetDateTime(8));
                            summaryList.Add(ticket);
                        }
                    }
                }
            }

            return summaryList;
        }

        /// <summary>
        /// Change status of an existing Ticket
        /// </summary>
        /// <param name="ticketnumber">TicketNumber</param>
        /// <param name="status">Status</param>
        public static void ChangeStatus(string ticketnumber, string status)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Tickets_ChangeStatus", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@TicketNumber", SqlDbType.Int).Value = ticketnumber;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get ticket summary chapter wise
        /// </summary>
        /// <param name="id">AdminId to return ChapterId with details</param>
        /// <returns>Ticket summary chapter wise Details</returns>
        public static List<Tickets> GetSummaryForChapters(int id)
        {
            var chapterticketList = new List<Tickets>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Tickets_GetSummaryForChapters", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var ticket = new Tickets(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetString(3),
                                rdr.GetInt32(4).ToString(),
                                rdr.GetString(5).ToString(),
                                rdr.GetDateTime(6),
                                rdr.GetInt32(7).ToString(),
                                null,
                                null,
                                rdr.GetDateTime(8));
                            chapterticketList.Add(ticket);
                        }
                    }
                }
            }

            return chapterticketList;
        }

        /// <summary>
        /// Get ticket summary chapter wise
        /// </summary>
        /// <param name="id">AdminId to return ChapterId with details</param>
        /// <returns>Ticket summary chapter wise Details</returns>
        public static List<Tickets> GetSummaryForAllChapters()
        {
            var chapterticketList = new List<Tickets>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Tickets_GetSummaryForAllChapters", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var ticket = new Tickets(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetString(3),
                                rdr.GetInt32(4).ToString(),
                                rdr.GetString(5).ToString(),
                                rdr.GetDateTime(6),
                                rdr.GetInt32(7).ToString(),
                                null,
                                null,
                                rdr.GetDateTime(8));
                            chapterticketList.Add(ticket);
                        }
                    }
                }
            }

            return chapterticketList;
        }

        /// <summary>
        /// Creates an offer
        /// </summary>
        /// <param name="categoryId">CategoryId</param>
        /// <param name="organisationName">Organisation Name</param>
        /// <param name="title">Title</param>
        /// <param name="percentageDiscount">Percentage Discount</param>
        /// <param name="fixedDiscount">fixedDiscount</param>
        /// <param name="organisationAddress">Organisation Address</param>
        /// <param name="city">city</param>
        /// <param name="email">Email</param>
        /// <param name="phoneNumber">Phone Number</param>
        /// <param name="nationalValidity">National Validity</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="expiryDate">Expiry Date</param>
        /// <param name="imagePath">Image Path</param>
        /// <param name="description">Description</param>
        public static void CreateOffer(int categoryId, string organisationName, string title, int percentageDiscount, int fixedDiscount, string organisationAddress, string city, string email, string phoneNumber, bool nationalValidity, DateTime startDate, DateTime expiryDate, string imagePath, string description)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Offers_CreateOffers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;
                    cmd.Parameters.Add("@OrganisationName", SqlDbType.VarChar).Value = organisationName != null ? organisationName : string.Empty;
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = title;
                    cmd.Parameters.Add("@PercentageDiscount", SqlDbType.Int).Value = percentageDiscount;
                    cmd.Parameters.Add("@FixedDiscount", SqlDbType.Int).Value = fixedDiscount;
                    cmd.Parameters.Add("@OrganisationAddress", SqlDbType.VarChar).Value = organisationAddress;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = city;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@NationalValidity", SqlDbType.Bit).Value = nationalValidity;
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
                    cmd.Parameters.Add("@ExpiryDate", SqlDbType.DateTime).Value = expiryDate;
                    cmd.Parameters.Add("@ImagePath", SqlDbType.VarChar).Value = imagePath;
                    cmd.Parameters.Add("@OfferDescription", SqlDbType.VarChar).Value = description != null ? description : string.Empty;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets Offers for all categories
        /// </summary>
        /// <param name="category">Gets the category of offer</param>
        /// <returns>List of Offers</returns>
        public static List<Offer> GetOffers(string category)
        {
            var offerList = new List<Offer>();
            var sproc = "Offers_GetOffersForOtherCategory";
            if (!string.IsNullOrEmpty(category))
            {
                sproc = "Offers_GetOffersForSpecificCategory";
            }

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sproc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (!string.IsNullOrEmpty(category))
                    {
                        cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = category;
                    }

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var offers = new Offer(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetString(3).ToString(),
                                rdr.GetString(4).ToString(),
                                rdr.GetString(5).ToString(),
                                rdr.GetString(6).ToString(),
                                rdr.GetString(7).ToString(),
                                rdr.GetString(8).ToString(),
                                rdr.GetString(9).ToString(),
                                rdr.GetString(10).ToString(),
                                rdr.GetString(11).ToString(),
                                rdr.GetString(12).ToString(),
                                rdr.GetString(13).ToString(),
                                rdr.GetString(14).ToString(),
                                rdr.GetString(15).ToString());
                            offerList.Add(offers);
                        }
                    }
                }
            }

            return offerList;
        }

        /// <summary>
        /// Gets Offer Categories
        /// </summary>
        /// <param name="sno">SNo</param>
        /// <returns>List of Offer Categories</returns>
        public static List<Offer> GetOfferDetail(string sno)
        {
            var offerDetailList = new List<Offer>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Offers_GetOffer", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SNo", SqlDbType.Int).Value = sno;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var offers = new Offer(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetString(3).ToString(),
                                rdr.GetString(4).ToString(),
                                rdr.GetString(5).ToString(),
                                rdr.GetString(6).ToString(),
                                rdr.GetString(7).ToString(),
                                rdr.GetString(8).ToString(),
                                rdr.GetString(9).ToString(),
                                rdr.GetString(10).ToString(),
                                rdr.GetString(11).ToString(),
                                rdr.GetString(12).ToString(),
                                rdr.GetString(13).ToString(),
                                rdr.GetString(14).ToString(),
                                rdr.GetString(15).ToString());
                            offerDetailList.Add(offers);
                        }
                    }
                }
            }

            return offerDetailList;
        }

        /// <summary>
        /// Get if admin is of Head Office
        /// </summary>
        /// <param name="isAdmin">IsAdmin</param>
        /// <param name="id">Id</param>
        /// <returns>GetChapter</returns>
        public static int GetChapter(int isAdmin, int id)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_IIA_GetChapterId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IsAdmin", SqlDbType.Int).Value = isAdmin;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                           return rdr.GetInt32(0);
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets Helpdesk Dashboard Values
        /// </summary>
        /// <param name="chapterId">chapterId</param>
        /// <returns>AdminHelpdeskDashboardObject</returns>
        public static HelpdeskDashboardA GetHelpdeskDashboardValues(int chapterId)
        {
            HelpdeskDashboardA helpdeskDashboardValues = null;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_AdminHelpdeskDashboardValues", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@chapterId", SqlDbType.Int).Value = chapterId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            helpdeskDashboardValues = new HelpdeskDashboardA(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetInt32(1).ToString(),
                                rdr.GetInt32(2).ToString(),
                                rdr.GetInt32(3).ToString(),
                                rdr.GetInt32(4).ToString(),
                                rdr.GetInt32(5).ToString(),
                                rdr.GetInt32(6).ToString(),
                                rdr.GetInt32(7).ToString(),
                                rdr.GetInt32(8).ToString(),
                                rdr.GetInt32(9).ToString(),
                                rdr.GetInt32(10).ToString(),
                                rdr.GetInt32(11).ToString(),
                                rdr.GetInt32(12).ToString(),
                                rdr.GetInt32(13).ToString());
                        }
                    }
                }
            }

            return helpdeskDashboardValues;
        }

        /// <summary>
        /// Get leader details list
        /// </summary>
        /// <param name="id">Id to return ChapterId with details</param>
        /// <returns>Leader Details chapter wise</returns>
        public static Dictionary<string, List<LeaderDetails>> GetLeaderDetails(int id)
        {
            var holeaderList = new List<LeaderDetails>();
            var chapterleaderList = new List<LeaderDetails>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("IIA_GetLeaderDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ChapterId", SqlDbType.Int).Value = id;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var leader = new LeaderDetails(
                                rdr.GetString(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetString(3).ToString(),
                                rdr.GetString(4).ToString(),
                                rdr.GetString(5).ToString());
                            if (rdr.GetInt32(6) == 82)
                            {
                                holeaderList.Add(leader);
                            }
                            else
                            {
                                chapterleaderList.Add(leader);
                            }
                        }
                    }
                }
            }

            var response = new Dictionary<string, List<LeaderDetails>>();
            response.Add("ho", holeaderList);
            response.Add("chapter", chapterleaderList);
            return response;
        }

        ///// <summary>
        ///// Gets Admin Roles
        ///// </summary>
        ///// <param name="id">Gets the Admin Id</param>
        ///// <returns>Admin Roles</returns>
        // public static Roles CheckRoles(int id)
        // {
        //    Roles roles;
        //    using (var conn = GetConnection())
        //    {
        //        using (SqlCommand cmd = new SqlCommand("Admin_CheckRoles", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        //            conn.Open();
        //            using (SqlDataReader rdr = cmd.ExecuteReader())
        //            {
        //                while (rdr.Read())
        //                {
        //                        roles = new Roles(
        //                        rdr.GetInt32(0).ToString(),
        //                        rdr.GetInt32(1).ToString(),
        //                        rdr.GetInt32(2).ToString(),
        //                        rdr.GetInt32(3).ToString(),
        //                        rdr.GetInt32(4).ToString(),
        //                        rdr.GetInt32(5).ToString(),
        //                        rdr.GetInt32(6).ToString(),
        //                        rdr.GetInt32(7).ToString(),
        //                        rdr.GetInt32(8).ToString());
        //                        return roles;
        //                }
        //            }
        //        }
        //    }

        // return null;
        // }

        /// <summary>
        /// Handles null values
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="colIndex">CollIndex</param>
        /// <returns>handling null values</returns>
        private static string SafeGetString(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetString(colIndex);
            }

            return string.Empty;
        }

        private static SqlConnection GetConnection()
        {
            return new SqlConnection(Environment.GetEnvironmentVariable("DatabaseEndpoint"));
        }

        /// <summary>
        /// Gets Membership Dashboard Values
        /// </summary>
        /// <param name="chapterId">chapterId</param>
        /// <returns>MembershipDashboardObject</returns>
        public static MembershipDashboardModel GetMembershipDashboardValues(int chapterId)
        {
            MembershipDashboardModel membershipDashboardValues = null;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_MembershipDashboardValues", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@chapterId", SqlDbType.Int).Value = chapterId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            membershipDashboardValues = new MembershipDashboardModel(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetInt32(1).ToString(),
                                rdr.GetInt32(2).ToString(),
                                rdr.GetInt32(3).ToString(),
                                rdr.GetInt32(4).ToString(),
                                rdr.GetInt32(5).ToString(),
                                rdr.GetInt32(6).ToString(),
                                rdr.GetInt32(7).ToString());
                        }
                    }
                }
            }

            return membershipDashboardValues;
        }
        /// <summary>
        /// Gets Payment record Values
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>MembershipDashboardObject</returns>
        public static List<PaymentRecordModel> GetPaymentRecordValue(int UserId)
        {
            List<PaymentRecordModel> PaymentRecordList=new List<PaymentRecordModel>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("PaymentRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var paymentReasonValues = new PaymentRecordModel(
                                rdr.GetString(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetInt32(2).ToString(),
                                rdr.GetString(3).ToString(),
                                rdr.GetDateTime(4).ToString());
                            PaymentRecordList.Add(paymentReasonValues);
                        }
                    }
                }
            }

            return PaymentRecordList;
        }
    }
    
}

