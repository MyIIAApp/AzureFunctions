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
        /// Get new Members Data
        /// </summary>
        /// <returns>Dictionary of new Members Data</returns>
        public static Dictionary<string, int> GetNewMember()
        {
            Dictionary<string, int> newData = new Dictionary<string, int>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("DatabaseSyncGetNewData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            try
                            {
                                newData.Add(rdr.GetString(0), rdr.GetInt32(1));
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }
            }

            return newData;
        }

        /// <summary>
        /// Get MemberDetails
        /// </summary>
        /// <returns>List of UserProfiles</returns>
        public static List<Dictionary<string, dynamic>> GetMemberDetailsForExcel()
        {
            List<Dictionary<string, dynamic>> memberData = new List<Dictionary<string, dynamic>>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("File_GetMemberDataForExcel", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Dictionary<string, dynamic> temp = new Dictionary<string, dynamic>();
                            temp.Add("MembershipId", rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0));
                            temp.Add("UnitName", rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1));
                            temp.Add("FirstName", rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2));
                            temp.Add("LastName", rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3));
                            temp.Add("MembershipJoinDate", rdr.IsDBNull(4) ? string.Empty : rdr.GetDateTime(4).ToShortDateString());
                            temp.Add("MembershipCurrentExpiryYear", rdr.IsDBNull(5) ? string.Empty : rdr.GetInt32(5).ToString());
                            temp.Add("ChapterName", rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6));
                            temp.Add("ProductCategory", rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7));
                            temp.Add("ProfileStatus", rdr.IsDBNull(8) ? 0 : rdr.GetInt32(8));
                            temp.Add("GSTIN", rdr.IsDBNull(9) ? string.Empty : rdr.GetString(9));
                            temp.Add("Exporter", rdr.IsDBNull(10) ? string.Empty : rdr.GetString(10));
                            temp.Add("PhoneNumber", rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11));
                            temp.Add("Email", rdr.IsDBNull(12) ? string.Empty : rdr.GetString(12));
                            temp.Add("DateOfBirth", rdr.IsDBNull(13) ? (DateTime?)null : rdr.GetDateTime(13));
                            temp.Add("DateOfMarriage", rdr.IsDBNull(14) ? (DateTime?)null : rdr.GetDateTime(14));
                            temp.Add("Address", rdr.IsDBNull(15) ? string.Empty : rdr.GetString(15));
                            temp.Add("Classification", rdr.IsDBNull(16) ? string.Empty : rdr.GetString(16));
                            temp.Add("EnterpriseType", rdr.IsDBNull(17) ? string.Empty : rdr.GetString(17));
                            temp.Add("MembershipFees", rdr.IsDBNull(18) ? string.Empty : rdr.GetInt32(18).ToString());
                            memberData.Add(temp);
                        }
                    }
                }
            }

            return memberData;
        }

        /// <summary>
        /// Get MemberInfo
        /// </summary>
        /// <returns>List of UserProfiles</returns>
        public static List<Dictionary<string, dynamic>> GetMemberInfo(string StartId, string EndId)
        {
            List<Dictionary<string, dynamic>> memberData = new List<Dictionary<string, dynamic>>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Get_MemberData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@StartId", SqlDbType.VarChar).Value = StartId;
                    cmd.Parameters.Add("@EndId", SqlDbType.VarChar).Value = EndId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Dictionary<string, dynamic> temp = new Dictionary<string, dynamic>();
                            temp.Add("PhoneNumber", rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0));
                            temp.Add("MembershipId", rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1));
                            temp.Add("MembershipCurrentExpiryYear", rdr.IsDBNull(2) ? string.Empty : rdr.GetInt32(2).ToString());
                            temp.Add("MembershipJoinDate", rdr.IsDBNull(3) ? string.Empty : rdr.GetDateTime(3).ToShortDateString());
                            temp.Add("ProfileStatus", rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4));
                            temp.Add("ChapterName", rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5));
                            temp.Add("UnitName", rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6));
                            temp.Add("GSTIN", rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7));
                            temp.Add("IndustryStatus", rdr.IsDBNull(8) ? string.Empty : rdr.GetString(8));
                            temp.Add("Address", rdr.IsDBNull(9) ? string.Empty : rdr.GetString(9));
                            temp.Add("District", rdr.IsDBNull(10) ? string.Empty : rdr.GetString(10));
                            temp.Add("City", rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11));
                            temp.Add("State", rdr.IsDBNull(12) ? string.Empty : rdr.GetString(12));
                            temp.Add("Country", rdr.IsDBNull(13) ? string.Empty : rdr.GetString(13));
                            temp.Add("Pincode", rdr.IsDBNull(14) ? string.Empty : rdr.GetString(14));
                            temp.Add("WebsiteUrl", rdr.IsDBNull(15) ? string.Empty : rdr.GetString(15));
                            temp.Add("ProductCategory", rdr.IsDBNull(16) ? string.Empty : rdr.GetString(16));
                            temp.Add("ProductSubCategory", rdr.IsDBNull(17) ? string.Empty : rdr.GetString(17));
                            temp.Add("MajorProducts", rdr.IsDBNull(18) ? string.Empty : rdr.GetString(18));
                            temp.Add("AnnualTurnOver", rdr.IsDBNull(19) ? string.Empty : rdr.GetString(19));
                            temp.Add("EnterpriseType", rdr.IsDBNull(20) ? string.Empty : rdr.GetString(20));
                            temp.Add("Exporter", rdr.IsDBNull(21) ? string.Empty : rdr.GetString(21));
                            temp.Add("Classification", rdr.IsDBNull(22) ? string.Empty : rdr.GetString(22));
                            temp.Add("FirstName", rdr.IsDBNull(23) ? string.Empty : rdr.GetString(23));
                            temp.Add("LastName", rdr.IsDBNull(24) ? string.Empty : rdr.GetString(24));
                            temp.Add("Email", rdr.IsDBNull(25) ? string.Empty : rdr.GetString(25));
                            temp.Add("DateOfBirth", rdr.IsDBNull(26) ? (DateTime?)null : rdr.GetDateTime(26));
                            temp.Add("DateOfMarriage", rdr.IsDBNull(27) ? (DateTime?)null : rdr.GetDateTime(27));
                            memberData.Add(temp);
                        }
                    }
                }
            }

            return memberData;
        }

        /*have to change this*/

        /// <summary>
        /// Get new Members Data
        /// </summary>
        /// <returns>Dictionary of new Members Data</returns>
        public static List<UserProfile> GetAllMembers()
        {
            List<UserProfile> newData = new List<UserProfile>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("DatabaseSyncGetNewData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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
                                    rdr.IsDBNull(33) ? (DateTime?)null : rdr.GetDateTime(33),
                                    rdr.IsDBNull(34) ? (DateTime?)null : rdr.GetDateTime(34),
                                    rdr.IsDBNull(35) ? string.Empty : rdr.GetString(35),
                                    rdr.IsDBNull(36) ? string.Empty : rdr.GetString(36),
                                    rdr.IsDBNull(37) ? string.Empty : rdr.GetString(37),
                                    rdr.IsDBNull(38) ? 0 : rdr.GetInt32(38),
                                    rdr.IsDBNull(39) ? 0 : rdr.GetInt32(39),
                                    rdr.IsDBNull(40) ? DateTime.MinValue : rdr.GetDateTime(40),
                                    rdr.IsDBNull(41) ? DateTime.MinValue : rdr.GetDateTime(41));
                        }
                    }
                }
            }

            return newData;
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
                ActiveMembershipResponse response = new ActiveMembershipResponse(userProfile.Id, userProfile.FirstName, userProfile.LastName, userProfile.Email, userProfile.MembershipId, userProfile.ChapterId, userProfile.ChapterName, userProfile.MembershipFees, DateTime.Parse(userProfile.MembershipCurrentExpiryYear.ToString() + "-04-01"), userProfile.MembershipJoinDate, 0);
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
                               rdr.IsDBNull(25) ? (DateTime?)null : rdr.GetDateTime(25),
                               rdr.IsDBNull(26) ? (DateTime?)null : rdr.GetDateTime(26),
                               rdr.IsDBNull(27) ? string.Empty : rdr.GetString(27),
                               rdr.IsDBNull(28) ? string.Empty : rdr.GetString(28),
                               rdr.IsDBNull(29) ? string.Empty : rdr.GetString(29),
                               rdr.IsDBNull(30) ? 0 : rdr.GetInt32(30),
                               rdr.IsDBNull(31) ? DateTime.Now : rdr.GetDateTime(31),
                               rdr.IsDBNull(32) ? DateTime.Now : rdr.GetDateTime(32),
                               rdr.IsDBNull(33) ? string.Empty : rdr.GetString(33));
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
        /// <param name="memberId">Membership Id of the User</param>
        /// <returns>company profile of user</returns>
        public static MembershipProfile GetMembershipProfileByNumber(string phoneNumber, string memberId)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_MembershipProfile_GetDetails_ByPhoneNumber", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = phoneNumber == null ? string.Empty : phoneNumber;
                    cmd.Parameters.Add("@MemberId", SqlDbType.VarChar).Value = memberId == null ? string.Empty : memberId;
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
                                rdr.IsDBNull(25) ? (DateTime?)null : rdr.GetDateTime(25),
                                rdr.IsDBNull(26) ? (DateTime?)null : rdr.GetDateTime(26),
                                rdr.IsDBNull(27) ? string.Empty : rdr.GetString(27),
                                rdr.IsDBNull(28) ? string.Empty : rdr.GetString(28),
                                rdr.IsDBNull(29) ? string.Empty : rdr.GetString(29),
                                rdr.IsDBNull(30) ? 0 : rdr.GetInt32(30),
                                rdr.IsDBNull(31) ? DateTime.Now : rdr.GetDateTime(31),
                                rdr.IsDBNull(32) ? DateTime.Now : rdr.GetDateTime(32),
                                rdr.IsDBNull(33) ? string.Empty : rdr.GetString(33));

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
        /// <returns>List pof UserProfiles of user</returns>
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
                                rdr.IsDBNull(33) ? (DateTime?)null : rdr.GetDateTime(33),
                                rdr.IsDBNull(34) ? (DateTime?)null : rdr.GetDateTime(34),
                                rdr.IsDBNull(35) ? string.Empty : rdr.GetString(35),
                                rdr.IsDBNull(36) ? string.Empty : rdr.GetString(36),
                                rdr.IsDBNull(37) ? string.Empty : rdr.GetString(37),
                                rdr.IsDBNull(38) ? 0 : rdr.GetInt32(38),
                                rdr.IsDBNull(39) ? 0 : rdr.GetInt32(39),
                                rdr.IsDBNull(40) ? DateTime.MinValue : rdr.GetDateTime(40),
                                rdr.IsDBNull(41) ? DateTime.MinValue : rdr.GetDateTime(41));
                            return userProfile;
                        }
                    }
                }
            }

            return new UserProfile(-1);
        }

        /// <summary>
        /// Get user.
        /// </summary>
        /// <param name="userId">Id of the User.</param>
        /// <param name="phoneNumber">PhoneNumber the User.</param>
        /// <returns>List pof UserProfiles of user</returns>
        public static List<UserProfile> GetUserProfile(dynamic userId, string phoneNumber)
        {
            List<UserProfile> userProfiles = new List<UserProfile>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_GetDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = userId != null ? userId : 0;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = phoneNumber != null ? phoneNumber : string.Empty;
                    cmd.Parameters.Add("@MembershipId", SqlDbType.VarChar).Value = string.Empty;
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
                                rdr.IsDBNull(33) ? (DateTime?)null : rdr.GetDateTime(33),
                                rdr.IsDBNull(34) ? (DateTime?)null : rdr.GetDateTime(34),
                                rdr.IsDBNull(35) ? string.Empty : rdr.GetString(35),
                                rdr.IsDBNull(36) ? string.Empty : rdr.GetString(36),
                                rdr.IsDBNull(37) ? string.Empty : rdr.GetString(37),
                                rdr.IsDBNull(38) ? 0 : rdr.GetInt32(38),
                                rdr.IsDBNull(39) ? 0 : rdr.GetInt32(39),
                                rdr.IsDBNull(40) ? DateTime.MinValue : rdr.GetDateTime(40),
                                rdr.IsDBNull(41) ? DateTime.MinValue : rdr.GetDateTime(41));
                            userProfiles.Add(userProfile);
                        }
                    }
                }
            }

            if (userProfiles.Count > 0)
            {
                return userProfiles;
            }

            userProfiles.Add(new UserProfile(-1));
            return userProfiles;
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
                        cmd.Parameters.Add("@MembershipAdmissionfee", SqlDbType.Int).Value = userProfile.MembershipAdmissionfee;
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
                        cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = userProfile.DateOfBirth != null ? userProfile.DateOfBirth : null;
                        cmd.Parameters.Add("@DateOfMarriage", SqlDbType.Date).Value = userProfile.DateOfMarriage != null ? userProfile.DateOfMarriage : null;
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
        /// Inserts/Update a user profile
        /// </summary>
        /// <param name="userProfile">UserCompanyProfile</param>
        /// <param name="adminId">adminId</param>
        public static void AdminInsertMembershipProfile(MembershipProfile userProfile, int adminId)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("UserProfile_Admin_Insert_UserProfile", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = userProfile.Id;
                        cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = userProfile.PhoneNumber != null ? userProfile.PhoneNumber : string.Empty;
                        cmd.Parameters.Add("@MembershipId", SqlDbType.VarChar).Value = string.Empty;
                        cmd.Parameters.Add("@MembershipAdmissionfee", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@MembershipFees", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@MembershipCurrentExpiryYear", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@MembershipJoinDate", SqlDbType.DateTime).Value = "1970-01-01 00:00:00.000";
                        cmd.Parameters.Add("@MembershipRenewDate", SqlDbType.DateTime).Value = "1970-01-01 00:00:00.000";
                        cmd.Parameters.Add("@MembershipExpiryYears", SqlDbType.Text).Value = string.Empty;
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
                        cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = userProfile.DateOfBirth != null ? userProfile.DateOfBirth : null;
                        cmd.Parameters.Add("@DateOfMarriage", SqlDbType.Date).Value = userProfile.DateOfMarriage != null ? userProfile.DateOfMarriage : null;
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
                    cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = membershipProfile.DateOfBirth != null ? membershipProfile.DateOfBirth : null;
                    cmd.Parameters.Add("@DateOfMarriage", SqlDbType.Date).Value = membershipProfile.DateOfMarriage != null ? membershipProfile.DateOfMarriage : null;
                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = membershipProfile.PhoneNumber != null ? membershipProfile.PhoneNumber : string.Empty;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
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
                                rdr.GetInt32(0) > 53 ? FunctionUtility.HTMLDecoder(rdr.GetString(1).ToString()) : rdr.GetString(1).ToString(),
                                rdr.GetInt32(0) > 53 ? FunctionUtility.HTMLDecoder(rdr.GetString(2).ToString()) : rdr.GetString(2).ToString(),
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
        /// Delete News
        /// </summary>
        /// <param name="id">News Id</param>
        public static void DeleteNews(int id)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_DeleteNews", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets Payment record Values
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="isAdmin">IsAdmin</param>
        /// <returns>PaymentRecordsList</returns>
        public static List<PaymentRecordModel> GetPaymentRecordValue(int userId, bool isAdmin)
        {
            List<PaymentRecordModel> paymentRecordList = new List<PaymentRecordModel>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(!isAdmin ? "PaymentRecord" : "AdminPaymentRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (!isAdmin)
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    }
                    else
                    {
                        cmd.Parameters.Add("@AdminId", SqlDbType.Int).Value = userId;
                    }

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var paymentReasonValues = new PaymentRecordModel(
                                rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0).ToString(),
                                rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1).ToString(),
                                rdr.IsDBNull(2) ? string.Empty : rdr.GetDouble(2).ToString(),
                                rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3).ToString(),
                                rdr.IsDBNull(4) ? string.Empty : rdr.GetDateTime(4).ToString(),
                                rdr.GetInt32(5),
                                rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6).ToString(),
                                rdr.IsDBNull(7) ? "Self" : rdr.GetString(7).ToString(),
                                rdr.IsDBNull(8) ? string.Empty : rdr.GetInt32(8).ToString());
                            paymentRecordList.Add(paymentReasonValues);
                        }
                    }
                }
            }

            return paymentRecordList;
        }

        /// <summary>
        /// Gets Payment record Values
        /// </summary>
        /// <param name="search">Search</param>
        /// <returns>PaymentRecordsList</returns>
        public static List<PaymentRecordAllInvoiceModel> GetPaymentRecordValueForAllInvoice(string search)
        {
            List<PaymentRecordAllInvoiceModel> paymentRecordList = new List<PaymentRecordAllInvoiceModel>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("PaymentRecordforAllInvoices", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@search", SqlDbType.VarChar).Value = search;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var paymentReasonValues = new PaymentRecordAllInvoiceModel(
                                rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0).ToString(),
                                rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1).ToString(),
                                rdr.IsDBNull(2) ? string.Empty : rdr.GetDouble(2).ToString(),
                                rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3).ToString(),
                                rdr.IsDBNull(4) ? string.Empty : rdr.GetDateTime(4).ToString(),
                                rdr.GetInt32(5),
                                rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6).ToString(),
                                rdr.IsDBNull(7) ? "Self" : rdr.GetString(7).ToString(),
                                rdr.IsDBNull(8) ? string.Empty : rdr.GetInt32(8).ToString(),
                                rdr.IsDBNull(9) ? string.Empty : rdr.GetString(9).ToString(),
                                rdr.IsDBNull(10) ? string.Empty : rdr.GetInt32(10).ToString(),
                                rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11).ToString(),
                                rdr.IsDBNull(12) ? string.Empty : rdr.GetString(12).ToString());
                            paymentRecordList.Add(paymentReasonValues);
                        }
                    }
                }
            }

            return paymentRecordList;
        }

        /// <summary>
        /// Gets Payment record Values
        /// </summary>
        /// <param name="invoiceId">invoice id</param>
        /// <returns>PaymentRecordsList</returns>
        public static List<PaymentRecordModel> GetPaymentRecordValuebyInvoice(int invoiceId)
        {
            List<PaymentRecordModel> paymentRecordList = new List<PaymentRecordModel>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("PaymentRecordbyInvoiceId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@InvoiceId", SqlDbType.Int).Value = invoiceId;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var paymentReasonValues = new PaymentRecordModel(
                                rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0).ToString(),
                                rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1).ToString(),
                                rdr.IsDBNull(2) ? string.Empty : rdr.GetDouble(2).ToString(),
                                rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3).ToString(),
                                rdr.IsDBNull(4) ? string.Empty : rdr.GetDateTime(4).ToString(),
                                rdr.GetInt32(5),
                                rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6).ToString(),
                                rdr.IsDBNull(7) ? "Self" : rdr.GetString(7).ToString(),
                                rdr.IsDBNull(8) ? string.Empty : rdr.GetInt32(8).ToString());
                            paymentRecordList.Add(paymentReasonValues);
                        }
                    }
                }
            }

            return paymentRecordList;
        }

        /// <summary>
        /// Gets Payment record Values
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <param name="isAdmin">IsAdmin</param>
        /// <param name="chapterId">ChapterId</param>
        /// <param name="reason">Membership type</param>
        /// <returns>PaymentRecordsList</returns>
        public static List<PaymentRecordModel> GetPaymentRecordValue(string startDate, string endDate, bool isAdmin, int chapterId, string reason)
        {
            List<PaymentRecordModel> paymentRecordList = new List<PaymentRecordModel>();
            using (var conn = GetConnection())
            {
                if (reason == "Membership")
                {
                    using (SqlCommand cmd = new SqlCommand("AdminPaymentRecord3", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@startDate", SqlDbType.NVarChar).Value = startDate;
                        cmd.Parameters.Add("@endDate", SqlDbType.NVarChar).Value = endDate;
                        cmd.Parameters.Add("@chapter", SqlDbType.NVarChar).Value = chapterId;

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var paymentReasonValues = new PaymentRecordModel(
                                    rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0).ToString(),
                                    rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1).ToString(),
                                    rdr.IsDBNull(2) ? string.Empty : rdr.GetDouble(2).ToString(),
                                    rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3).ToString(),
                                    rdr.IsDBNull(4) ? string.Empty : rdr.GetDateTime(4).ToString(),
                                    rdr.GetInt32(5),
                                    rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6).ToString(),
                                    rdr.IsDBNull(7) ? "Self" : rdr.GetString(7).ToString(),
                                    rdr.IsDBNull(8) ? string.Empty : rdr.GetInt32(8).ToString());
                                paymentRecordList.Add(paymentReasonValues);
                            }
                        }
                    }

                    conn.Close();
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("AdminPaymentRecord3NonMember2", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@startDate", SqlDbType.NVarChar).Value = startDate;
                        cmd.Parameters.Add("@endDate", SqlDbType.NVarChar).Value = endDate;
                        cmd.Parameters.Add("@chapter", SqlDbType.NVarChar).Value = chapterId;

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var paymentReasonValues = new PaymentRecordModel(
                                    rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0).ToString(),
                                    rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1).ToString(),
                                    rdr.IsDBNull(2) ? string.Empty : rdr.GetDouble(2).ToString(),
                                    rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3).ToString(),
                                    rdr.IsDBNull(4) ? string.Empty : rdr.GetDateTime(4).ToString(),
                                    rdr.GetInt32(5),
                                    rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6).ToString(),
                                    rdr.IsDBNull(7) ? "Self" : rdr.GetString(7).ToString(),
                                    "-");
                                paymentRecordList.Add(paymentReasonValues);
                            }
                        }
                    }
                }
            }

            return paymentRecordList;
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

                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = title;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = description;
                    cmd.Parameters.Add("@Category", SqlDbType.NVarChar).Value = category;
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
        /// Update Payment Status
        /// </summary>
        /// <param name="invoiceNumber">Invoice Number</param>
        /// <param name="status">Status of Transaction</param>
        /// <param name="onlineTxnId">Online Transaction Id</param>
        /// <returns>previous transaction status</returns>
        public static int UpdatePaymentStatus(int invoiceNumber, int status, string onlineTxnId)
        {
            int previousStatus = 0;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Payment_UpdatePaymentStatus", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@InvoiceNumber", SqlDbType.Int).Value = invoiceNumber;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                    cmd.Parameters.Add("@OnlineTxnId", SqlDbType.NVarChar).Value = onlineTxnId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            previousStatus = rdr.GetInt32(0);
                        }
                    }
                }
            }

            return previousStatus;
        }

        /// <summary>
        /// Get Payment Details for one day
        /// </summary>
        /// <returns>List of Payment Details</returns>
        public static List<List<dynamic>> GetPaymentDetail()
        {
            List<List<dynamic>> list = new List<List<dynamic>>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Payment_GetPaymentDetail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            List<dynamic> temp = new List<dynamic>();
                            temp.Add(rdr.GetInt32(0));
                            temp.Add(rdr.GetInt32(1));
                            temp.Add(rdr.GetString(2));
                            temp.Add(rdr.GetInt32(3));
                            list.Add(temp);
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a comment to existing Ticket
        /// </summary>
        /// <param name="ticketNumber">TicketNumber</param>
        /// <param name="comments">Comment</param>
        /// <param name="userId">UserId</param>
        /// <param name="adminId">AdminId</param>
        /// <returns>Returned UserId</returns>
        public static int AddComment(string ticketNumber, string comments, int userId, int adminId)
        {
            int returnedUserId = 0;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_Tickets_AddComment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@TicketNumber", SqlDbType.Int).Value = ticketNumber;
                    cmd.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = comments;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@AdminId", SqlDbType.Int).Value = adminId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            returnedUserId = rdr.GetInt32(0);
                        }
                    }
                }
            }

            return returnedUserId;
        }

        /// <summary>
        /// Gets phone number of the issue raiser
        /// </summary>
        /// <param name="ticketNumber">TicketNumber</param>
        /// <returns>Returned PhoneNumber</returns>
        public static string AddCommentGetPhoneNumber(string ticketNumber)
        {
            string phoneNumber = string.Empty;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_Tickets_AddComment_GetPhonenumber", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@TicketNumber", SqlDbType.Int).Value = ticketNumber;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            phoneNumber = rdr.GetString(0);
                        }
                    }
                }
            }

            return phoneNumber;
        }

        /// <summary>
        /// Gets phone number of the item seller
        /// </summary>
        /// <param name="itemid">TicketNumber</param>
        /// <returns>Returned PhoneNumber</returns>
        public static string EnquiryGetPhoneNumberOfSeller(string itemid)
        {
            string phoneNumber = string.Empty;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_Enquiry_GetPhoneNumberOfSellerByItemId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = itemid;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            phoneNumber = rdr.GetString(0);
                        }
                    }
                }
            }

            return phoneNumber;
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
                    cmd.Parameters.Add("@AttachmentURL", SqlDbType.NVarChar).Value = attachmenturl;
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
                                rdr.GetDateTime(8),
                                rdr.GetString(9),
                                rdr.GetString(10),
                                rdr.GetString(11),
                                rdr.IsDBNull(12) ? (int?)null : rdr.GetInt32(12));
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
                                rdr.GetDateTime(8),
                                rdr.GetString(9),
                                rdr.GetString(10),
                                rdr.GetString(11),
                                rdr.IsDBNull(12) ? (int?)null : rdr.GetInt32(12));
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
                                rdr.GetDateTime(8),
                                rdr.GetString(9),
                                rdr.GetString(10),
                                rdr.GetString(11),
                                rdr.IsDBNull(12) ? (int?)null : rdr.GetInt32(12));
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
                                rdr.GetDateTime(8),
                                rdr.GetString(9),
                                rdr.GetString(10),
                                rdr.GetString(11),
                                rdr.IsDBNull(12) ? (int?)null : rdr.GetInt32(12));
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
        public static Chapter GetChapter(int isAdmin, int id)
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
                            return new Chapter(rdr.GetInt32(0), rdr.GetString(1));
                        }
                    }
                }
            }

            return new Chapter(-1, string.Empty);
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
        /// Gets B2B Admin Dashboard Values
        /// </summary>
        /// <returns>AdminHelpdeskDashboardObject</returns>
        public static B2BAdminDashboard GetB2BAdminDashboardValues()
        {
            B2BAdminDashboard b2bAdminDashboardValues = null;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_B2BAdminDashboardValues", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            b2bAdminDashboardValues = new B2BAdminDashboard(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetInt32(1).ToString(),
                                rdr.GetInt32(2).ToString(),
                                rdr.GetInt32(3).ToString(),
                                rdr.GetInt32(4).ToString(),
                                rdr.GetInt32(5).ToString(),
                                rdr.GetInt32(6).ToString());
                        }
                    }
                }
            }

            return b2bAdminDashboardValues;
        }

        /// <summary>
        /// Gets B2B Admin Enquiries
        /// </summary>
        /// <returns>AdminHelpdeskDashboardObject</returns>
        public static List<B2BAdminEnquiry> GetB2BAdminEnquiries()
        {
            List<B2BAdminEnquiry> b2bAdminEnquiries = new List<B2BAdminEnquiry>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_B2BAdminEnquiries", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            B2BAdminEnquiry b2bAdminEnquiry = new B2BAdminEnquiry(
                                rdr.GetString(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetInt32(3).ToString());
                            b2bAdminEnquiries.Add(b2bAdminEnquiry);
                        }
                    }
                }
            }

            return b2bAdminEnquiries;
        }

        /// <summary>
        /// Gets B2B Admin Enquiries
        /// </summary>
        /// <returns>AdminHelpdeskDashboardObject</returns>
        public static List<B2BAdminListing> GetB2BAdminListing()
        {
            List<B2BAdminListing> b2bAdminListings = new List<B2BAdminListing>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_B2BAdminListings", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            B2BAdminListing b2bAdminListing = new B2BAdminListing(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2).ToString(),
                                rdr.GetDouble(3) == -1 ? "N/A" : rdr.GetDouble(3).ToString(),
                                rdr.GetString(4).ToString(),
                                rdr.GetInt32(5).ToString());
                            b2bAdminListings.Add(b2bAdminListing);
                        }
                    }
                }
            }

            return b2bAdminListings;
        }

        /// <summary>
        /// B2B Admin Block Unblock Listing
        /// </summary>
        /// <param name="update">Status to update</param>
        /// <param name="itemId">item Id to change status</param>
        public static void B2BAdminBlockUnblockListing(string update, int itemId)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_B2BAdminBlockUnblockListing", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@update", SqlDbType.VarChar).Value = update;
                    cmd.Parameters.Add("@itemId", SqlDbType.Int).Value = itemId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
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

        /// <summary>
        /// Get member Details for Insurance.
        /// </summary>
        /// <param name="membershipId">membershipId of the member.</param>
        /// <returns>Member details fro Insurance</returns>
        public static UserProfileForInsurance GetMembershipDetailsForInsurance(string membershipId)
        {
            UserProfileForInsurance memberDetails = null;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Insurance_GetMemberDetailsForInsurance", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MembershipId", SqlDbType.VarChar).Value = membershipId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            memberDetails = new UserProfileForInsurance(
                                rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0),
                                rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                                rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2),
                                rdr.IsDBNull(3) ? DateTime.MinValue : rdr.GetDateTime(3),
                                rdr.IsDBNull(4) ? DateTime.MinValue : rdr.GetDateTime(4),
                                rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5),
                                rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6),
                                rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7),
                                rdr.IsDBNull(8) ? string.Empty : rdr.GetString(8),
                                rdr.IsDBNull(9) ? string.Empty : rdr.GetString(9),
                                rdr.IsDBNull(10) ? string.Empty : rdr.GetString(10),
                                rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11),
                                rdr.IsDBNull(12) ? string.Empty : rdr.GetString(12),
                                rdr.IsDBNull(13) ? string.Empty : rdr.GetString(13),
                                rdr.IsDBNull(14) ? string.Empty : rdr.GetString(14),
                                rdr.IsDBNull(15) ? string.Empty : rdr.GetString(15),
                                rdr.IsDBNull(16) ? string.Empty : rdr.GetString(16),
                                rdr.IsDBNull(17) ? string.Empty : rdr.GetString(17));
                        }
                    }
                }
            }

            return memberDetails;
        }

        /// <summary>
        /// Get new members Details for Insurance.
        /// </summary>
        /// <param name="startingDate">starting Date of Required Time span.</param>
        /// <param name="endingDate">Ending Date if Required Time span</param>
        /// <returns>List of New Member for Insurance</returns>
        public static List<UserProfileForInsurance> GetNewMembersForInsurance(DateTime startingDate, DateTime endingDate)
        {
            List<UserProfileForInsurance> membersDetails = new List<UserProfileForInsurance>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Insurance_GetNewMembersForInsurance", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@StartingDate", SqlDbType.DateTime).Value = startingDate;
                    cmd.Parameters.Add("@EndingDate", SqlDbType.DateTime).Value = endingDate;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            UserProfileForInsurance member = new UserProfileForInsurance(
                               rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0),
                               rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                               rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2),
                               rdr.IsDBNull(3) ? DateTime.MinValue : rdr.GetDateTime(3),
                               rdr.IsDBNull(4) ? DateTime.MinValue : rdr.GetDateTime(4),
                               rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5),
                               rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6),
                               rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7),
                               rdr.IsDBNull(8) ? string.Empty : rdr.GetString(8),
                               rdr.IsDBNull(9) ? string.Empty : rdr.GetString(9),
                               rdr.IsDBNull(10) ? string.Empty : rdr.GetString(10),
                               rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11),
                               rdr.IsDBNull(12) ? string.Empty : rdr.GetString(12),
                               rdr.IsDBNull(13) ? string.Empty : rdr.GetString(13),
                               rdr.IsDBNull(14) ? string.Empty : rdr.GetString(14),
                               rdr.IsDBNull(15) ? string.Empty : rdr.GetString(15),
                               rdr.IsDBNull(16) ? string.Empty : rdr.GetString(16),
                               rdr.IsDBNull(17) ? string.Empty : rdr.GetString(17));
                            membersDetails.Add(member);
                        }
                    }
                }
            }

            return membersDetails;
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
                using (SqlCommand cmd = new SqlCommand("Admin_MembershipDashboardValues2", conn))
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
                                rdr.GetInt32(7).ToString(),
                                rdr.GetInt32(8).ToString());
                        }
                    }
                }
            }

            return membershipDashboardValues;
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
        /// <param name="status">Status of transaction</param>
        /// <param name="onlineTransactionId">Online transaction Id</param>
        /// <param name="expiryYear"> Expiry Year</param>
        /// <returns>Invoice Number</returns>
        public static string StorePayment(int userId, int adminId, double subTotal, int cgst, int sgst, int igst, double igstValue, double sgstValue, double cgstValue, string paymentReason, string paymentMode, string chequeNumber, string orderId, double total, DateTime currentTime, int status, string onlineTransactionId, int expiryYear)
        {
            total = Math.Round(total);
            cgstValue = Math.Round(cgstValue, 2);
            sgstValue = Math.Round(sgstValue, 2);
            igstValue = Math.Round(igstValue, 2);
            subTotal = Math.Round(subTotal, 2);
            string invoiceNumber;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_StorePayment2", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@AdminId", SqlDbType.Int).Value = adminId;
                    cmd.Parameters.Add("@SubTotal", SqlDbType.Float).Value = subTotal;
                    cmd.Parameters.Add("@CGST", SqlDbType.Int).Value = cgst;
                    cmd.Parameters.Add("@SGST", SqlDbType.Int).Value = sgst;
                    cmd.Parameters.Add("@IGST", SqlDbType.Int).Value = igst;
                    cmd.Parameters.Add("@CGSTValue", SqlDbType.Float).Value = cgstValue;
                    cmd.Parameters.Add("@SGSTValue", SqlDbType.Float).Value = sgstValue;
                    cmd.Parameters.Add("@IGSTValue", SqlDbType.Float).Value = igstValue;
                    cmd.Parameters.Add("@PaymentReason", SqlDbType.NVarChar).Value = paymentReason;
                    cmd.Parameters.Add("@PaymentMode", SqlDbType.NVarChar).Value = paymentMode;
                    cmd.Parameters.Add("@ChequeNumber", SqlDbType.NVarChar).Value = chequeNumber;
                    cmd.Parameters.Add("@OrderId", SqlDbType.NVarChar).Value = orderId;
                    cmd.Parameters.Add("@Total", SqlDbType.Float).Value = Math.Round(total);
                    cmd.Parameters.Add("@CurrentTime", SqlDbType.DateTime).Value = currentTime;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                    cmd.Parameters.Add("@OnlineTransactionId", SqlDbType.NVarChar).Value = onlineTransactionId;
                    cmd.Parameters.Add("@expiryYear", SqlDbType.Int).Value = expiryYear;
                    cmd.Parameters.Add("@sourceGST", SqlDbType.VarChar, 15).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Number", SqlDbType.Int).Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    invoiceNumber = cmd.Parameters["@Number"].Value.ToString();
                }
            }

            return invoiceNumber;
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
        /// Update Full Invoice Num
        /// </summary>
        /// <param name="fullInvoiceNum">Full invoice num of invoice</param>
        /// <param name="orderId">OrderId</param>
        public static void UpdateFullInvoiceNum(string fullInvoiceNum, string orderId)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_UpdateFullInvoiceNum", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FullInvoiceNum", SqlDbType.VarChar, 100).Value = fullInvoiceNum;
                    cmd.Parameters.Add("@InvoiceId", SqlDbType.NVarChar).Value = orderId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Update Full Invoice Num
        /// </summary>
        /// <param name="fullInvoiceNum">Full invoice num of invoice</param>
        /// <param name="orderId">OrderId</param>
        public static void UpdateFullInvoiceNumNonMem(string fullInvoiceNum, string orderId)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_UpdateFullInvoiceNumNonMem", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FullInvoiceNum", SqlDbType.VarChar, 100).Value = fullInvoiceNum;
                    cmd.Parameters.Add("@InvoiceId", SqlDbType.NVarChar).Value = orderId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Update Invoice Path Non Member
        /// </summary>
        /// <param name="url">URL of invoice</param>
        /// <param name="orderId">OrderId</param>
        public static void UpdateInvoicePathNonMember(string url, string orderId)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_UpdateInvoicePathNonMember", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@URL", SqlDbType.NVarChar).Value = url;
                    cmd.Parameters.Add("@OrderId", SqlDbType.NVarChar).Value = orderId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Update Invoice Path
        /// </summary>
        /// <param name="url">URL of invoice</param>
        /// <param name="invoiceId">Invoice Id</param>
        public static void StoreNonMemberInvoicePath(string url, int invoiceId)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_UpdateNonMemberInvoicePath", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrderId", SqlDbType.Int).Value = invoiceId;
                    cmd.Parameters.Add("@URL", SqlDbType.NVarChar).Value = url;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get Item listing
        /// </summary>
        /// <param name="category">Category to return ItemListing</param>
        /// <param name="subCategory">subCategory to return ItemListing</param>
        /// <param name="buyerId">buyerId to return ItemListing</param>
        /// <returns>List of Item for input category and subCategory</returns>
        public static List<dynamic> GetItemListing(string category, string subCategory, int buyerId)
        {
            var itemList = new List<dynamic>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetItemListingForSpecificCategoryAndSubCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@category", SqlDbType.NVarChar).Value = category;
                    cmd.Parameters.Add("@subCategory", SqlDbType.NVarChar).Value = subCategory;
                    cmd.Parameters.Add("@buyerId", SqlDbType.Int).Value = buyerId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var item = new Item(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetString(3).ToString(),
                                rdr.GetString(4).ToString(),
                                rdr.GetString(5).ToString(),
                                rdr.GetDouble(6) == -1 ? "N/A" : rdr.GetDouble(6).ToString(),
                                rdr.GetInt32(7).ToString(),
                                rdr.GetInt32(8).ToString(),
                                rdr.GetString(9).ToString(),
                                rdr.GetString(10).ToString(),
                                rdr.GetString(11).ToString(),
                                rdr.GetInt32(12).ToString());
                            itemList.Add(item);
                        }
                    }
                }
            }

            return itemList;
        }

        /// <summary>
        /// Get Item listing
        /// </summary>
        /// <returns>List of Item for input category and subCategory</returns>
        public static Dictionary<string, List<string>> GetValidItemList()
        {
            var itemList = new Dictionary<string, List<string>>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetValidItemListing", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (itemList.ContainsKey(rdr.GetString(0)))
                            {
                                itemList[rdr.GetString(0)].Add(rdr.GetString(1));
                            }
                            else
                            {
                                itemList.Add(rdr.GetString(0), new List<string>());
                                itemList[rdr.GetString(0)].Add(rdr.GetString(1));
                            }
                        }
                    }
                }
            }

            return itemList;
        }

        /// <summary>
        /// Get Item listing
        /// </summary>
        /// <returns>List of Item for input category and subCategory</returns>
        /// <param name="buyerId">buyer id</param>
        public static List<dynamic> GetTopItemListing(int buyerId)
        {
            var itemList = new List<dynamic>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetTopItemListing", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@buyerId", SqlDbType.Int).Value = buyerId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var item = new Item(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetString(3).ToString(),
                                rdr.GetString(4).ToString(),
                                rdr.GetString(5).ToString(),
                                rdr.GetDouble(6) == -1 ? "N/A" : rdr.GetDouble(6).ToString(),
                                rdr.GetInt32(7).ToString(),
                                rdr.GetInt32(8).ToString(),
                                rdr.GetString(9).ToString(),
                                rdr.GetString(10).ToString(),
                                rdr.GetString(11).ToString(),
                                rdr.GetInt32(12).ToString());
                            itemList.Add(item);
                        }
                    }
                }
            }

            return itemList;
        }

        /// <summary>
        /// Store Payment Details
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="adminId">adminId</param>
        /// <param name="subTotal">subTotal</param>
        /// <param name="cgstValue">IGST Amount</param>
        /// <param name="sgstValue">SGST Amount</param>
        /// <param name="igstValue">CGST Amount</param>
        /// <param name="paymentMode">Payment Mode</param>
        /// <param name="chequeNumber">Cheque Number</param>
        /// <param name="total">Total Amount</param>
        /// <param name="status">Status of transaction</param>
        /// <param name="phoneNumber">Phone Number</param>
        /// <param name="address">Address</param>
        /// <param name="gstin">GSTIN</param>
        /// <param name="chequeDate">Cheque Date</param>
        /// <param name="state">State</param>
        /// <returns>Invoice Number</returns>
        public static string StoreNonMemberPayment(string name, int adminId, double subTotal, double cgstValue, double sgstValue, double igstValue, string paymentMode, string chequeNumber, double total, int status, string phoneNumber, string address, string gstin, string chequeDate, string state)
        {
            string invoiceNumber;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_StoreNonMemberPayment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AdminId", SqlDbType.Int).Value = adminId;
                    cmd.Parameters.Add("@SubTotal", SqlDbType.Float).Value = subTotal;
                    cmd.Parameters.Add("@CGSTValue", SqlDbType.Float).Value = cgstValue;
                    cmd.Parameters.Add("@SGSTValue", SqlDbType.Float).Value = sgstValue;
                    cmd.Parameters.Add("@IGSTValue", SqlDbType.Float).Value = igstValue;
                    cmd.Parameters.Add("@PaymentMode", SqlDbType.NVarChar).Value = paymentMode;
                    cmd.Parameters.Add("@ChequeNumber", SqlDbType.NVarChar).Value = chequeNumber;
                    cmd.Parameters.Add("@Total", SqlDbType.Float).Value = Math.Round(total);
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@GSTIN", SqlDbType.NVarChar).Value = gstin;
                    cmd.Parameters.Add("@ChequeDate", SqlDbType.NVarChar).Value = chequeDate;
                    cmd.Parameters.Add("@State", SqlDbType.NVarChar).Value = state;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = address;
                    cmd.Parameters.Add("@sourceGST", SqlDbType.VarChar, 15).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Number", SqlDbType.Int).Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    invoiceNumber = cmd.Parameters["@Number"].Value.ToString();
                }
            }

            return invoiceNumber;
        }

        /// <summary>
        /// Store Payment Details
        /// </summary>
        /// <param name="itemName">ItemName</param>
        /// <param name="gSTRate">GSTRate</param>
        /// <param name="sacValue">SacValue</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="unitPrice">UnitPrice</param>
        /// <param name="invoiceId">InvoiceId</param>
        public static void StoreNonMemberPaymentItems(string itemName, string gSTRate, string sacValue, double quantity, double unitPrice, int invoiceId)
        {
            string invoiceNumber;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_StoreNonMemberPaymentItems", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ItemName", SqlDbType.NVarChar).Value = itemName;
                    cmd.Parameters.Add("@GSTRate", SqlDbType.NVarChar).Value = gSTRate;
                    cmd.Parameters.Add("@SacValue", SqlDbType.NVarChar).Value = sacValue;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                    cmd.Parameters.Add("@UnitPrice", SqlDbType.Int).Value = unitPrice;
                    cmd.Parameters.Add("@InvoiceId", SqlDbType.Int).Value = invoiceId;
                    cmd.Parameters.Add("@Number", SqlDbType.Int).Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    invoiceNumber = cmd.Parameters["@Number"].Value.ToString();
                }
            }

            /*return invoiceNumber;*/
        }

        /// <summary>
        /// Get Item Details
        /// </summary>
        /// <param name="itemId">id of item to return ItemDetails</param>
        /// /// <param name="buyerId">buyerId to check if Enquiry is made</param>
        /// <returns>List of ItemDetails for input itemId</returns>
        public static List<dynamic> GetItemDetails(int itemId, int buyerId)
        {
            var itemDetailsList = new List<dynamic>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetItemListingForSpecificId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = itemId;
                    cmd.Parameters.Add("@buyerId", SqlDbType.Int).Value = buyerId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var item = new Item(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetString(3).ToString(),
                                rdr.GetString(4).ToString(),
                                rdr.GetString(5).ToString(),
                                rdr.GetDouble(6) == -1 ? "N/A" : rdr.GetDouble(6).ToString(),
                                rdr.GetInt32(7).ToString(),
                                rdr.GetInt32(8).ToString(),
                                rdr.GetString(9).ToString(),
                                rdr.GetString(10).ToString(),
                                rdr.GetString(11).ToString(),
                                rdr.GetInt32(12).ToString());
                            itemDetailsList.Add(item);
                        }
                    }
                }
            }

            return itemDetailsList;
        }

        /// <summary>
        /// Send Enquiry
        /// </summary>
        /// <param name="message">The message in the Enquiry</param>
        /// <param name="itemId">id of item to make Enquiry</param>
        /// /// <param name="buyerId">buyerId who has made Enquiry</param>
        public static void SendEnquiry(string message, int itemId, int buyerId)
        {
            var itemDetailsList = new List<dynamic>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SendEnquiry", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@message", SqlDbType.VarChar).Value = message;
                    cmd.Parameters.Add("@itemId", SqlDbType.Int).Value = itemId;
                    cmd.Parameters.Add("@buyerId", SqlDbType.Int).Value = buyerId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get enquiry details
        /// </summary>
        /// <param name="id"> sellerid</param>
        /// <returns>ggive enquiry details</returns>
        public static List<dynamic> GetEnquiryDetailsBySeller(int id)
        {
            var detailList = new List<dynamic>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("B2B_GetEnquiryDetailsBySeller", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SellerId", SqlDbType.Int).Value = id;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var details = new Details(
                                rdr.IsDBNull(0) ? "N/A" : rdr.GetInt32(0).ToString(),
                                rdr.IsDBNull(1) ? "N/A" : rdr.GetInt32(1).ToString(),
                                rdr.IsDBNull(2) ? "N/A" : rdr.GetInt32(2).ToString(),
                                rdr.IsDBNull(3) ? "N/A" : rdr.GetString(3).ToString(),
                                rdr.IsDBNull(4) ? "N/A" : rdr.GetString(4).ToString(),
                                rdr.IsDBNull(5) ? "N/A" : rdr.GetString(5).ToString(),
                                rdr.IsDBNull(6) ? "N/A" : rdr.GetString(6).ToString(),
                                rdr.IsDBNull(7) ? "N/A" : rdr.GetString(7).ToString(),
                                rdr.IsDBNull(8) ? "N/A" : rdr.GetString(8).ToString(),
                                rdr.IsDBNull(9) ? "N/A" : rdr.GetString(9).ToString(),
                                rdr.IsDBNull(10) ? "N/A" : rdr.GetDouble(10).ToString(),
                                rdr.IsDBNull(11) ? "N/A" : rdr.GetString(11).ToString(),
                                rdr.IsDBNull(12) ? "N/A" : rdr.GetDateTime(12).ToString(),
                                rdr.IsDBNull(13) ? "N/A" : rdr.GetInt16(13).ToString(),
                                rdr.IsDBNull(14) ? "N/A" : rdr.GetString(14).ToString(),
                                rdr.IsDBNull(15) ? "N/A" : rdr.GetString(15).ToString());
                            detailList.Add(details);
                        }
                    }
                }

                return detailList;
            }
        }

        /// <summary>
        /// Get Item Details
        /// </summary>
        /// <param name="sellerId"> sellerid of item to return</param>
        /// <returns>List of ItemDetails for input itemId</returns>
        public static List<Item1> GetSellerItemDetails(int sellerId)
        {
            List<Item1> itemDetailsList = new List<Item1>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("B2B_GetSellerItemDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sellerid", SqlDbType.Int).Value = sellerId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var item = new Item1(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetString(3).ToString(),
                                rdr.GetString(4).ToString(),
                                rdr.GetString(5).ToString(),
                                rdr.GetDouble(6).ToString(),
                                rdr.GetInt32(7).ToString(),
                                rdr.GetInt32(8).ToString());
                            itemDetailsList.Add(item);
                        }
                    }
                }
            }

            return itemDetailsList;
        }

        /// <summary>
        /// Edit seller details
        /// </summary>
        /// <param name="name"> name</param>
        /// <param name="description">description</param>
        /// <param name="photoPath">photoPath</param>
        /// <param name="category">category</param>
        /// <param name="subCategory">subCategory</param>
        /// <param name="price">price</param>
        /// <param name="sellerId">sellerId</param>
        /// <param name="editOrNew">have to edit or create new list</param>
        /// <param name="id">item id</param>
        public static void EditDetailsBySeller(string name, string description, string photoPath, string category, string subCategory, float price, int sellerId, bool editOrNew, int id)
        {
            string procedure = string.Empty;
            if (editOrNew)
            {
                procedure = "B2B_EditDetailsBySeller";
                using (var conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(procedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                        cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = description;
                        cmd.Parameters.Add("@photoPath", SqlDbType.VarChar).Value = photoPath;
                        cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = category;
                        cmd.Parameters.Add("@subCategory", SqlDbType.VarChar).Value = subCategory;
                        cmd.Parameters.Add("@price", SqlDbType.Float).Value = price;
                        cmd.Parameters.Add("@sellerId", SqlDbType.Int).Value = sellerId;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                procedure = "B2B_CreateItemListBySeller";
                using (var conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(procedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                        cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = description;
                        cmd.Parameters.Add("@photoPath", SqlDbType.VarChar).Value = photoPath;
                        cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = category;
                        cmd.Parameters.Add("@subCategory", SqlDbType.VarChar).Value = subCategory;
                        cmd.Parameters.Add("@price", SqlDbType.Float).Value = price;
                        cmd.Parameters.Add("@sellerId", SqlDbType.Int).Value = sellerId;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        ///  delete Seller Details
        /// </summary>
        /// <param name="id"> listing id</param>
        /// <param name="toggle"> boolean</param>
        /// <param name="active"> activation id</param>
        public static void DeleteItemDetailsBySeller(string id, bool toggle, int active)
        {
            string procedure = string.Empty;
            if (toggle)
            {
                procedure = "B2B_ActivateDeactivateItems";
            }
            else
            {
                procedure = "B2B_DeleteItemDetailsBySeller";
            }

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(procedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (toggle)
                    {
                        cmd.Parameters.Add("@activate", SqlDbType.Int).Value = active;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(id);
                    }
                    else
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(id);
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// update enquirydetails
        /// </summary>
        /// <param name="enquiryId"> enquiry id</param>
        public static void UpdateEnquiryDetailsBySeller(int enquiryId)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("B2B_UpdateEnquiryDetailsBySeller", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@enquiryId", SqlDbType.Int).Value = enquiryId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get Clicked Number Data Table.
        /// </summary>
        /// <param name="dataType">Data Type.</param>
        /// <param name="chapterId">Chapter Id</param>
        /// <returns>Clicked Number Data</returns>
        public static List<Dictionary<string, string>> GetClickedValueData(string dataType, int chapterId)
        {
            List<Dictionary<string, string>> output = new List<Dictionary<string, string>>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_GetClickedNumberData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@dataType", SqlDbType.VarChar).Value = dataType;
                    cmd.Parameters.Add("@chapterId", SqlDbType.Int).Value = chapterId;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var temp = new Dictionary<string, string>();
                            temp.Add("Membership Id", rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0));
                            temp.Add("Phone Number", rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1));
                            temp.Add("Unit Name", rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2));
                            temp.Add("Address", rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3));
                            temp.Add("Chapter Name", rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4));
                            temp.Add("Annual Turnover", rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5));
                            temp.Add("Email Id", rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6));
                            temp.Add("Profile Status", rdr.IsDBNull(7) ? string.Empty : rdr.GetInt32(7) == 2 ? "Pending For Approval" : rdr.GetInt32(7) == 3 ? "Rejected" : rdr.GetInt32(7) == 4 ? "Approved" : rdr.GetInt32(7) == 5 ? "Active" : rdr.GetInt32(7) == 6 ? "Grace" : rdr.GetInt32(7) == 7 ? "Expired" : "N/A");
                            temp.Add("Membership Renewal Due Date", rdr.IsDBNull(8) ? string.Empty : rdr.GetInt32(8) == 0 ? "N/A" : "01-04-" + rdr.GetInt32(8).ToString());
                            temp.Add("GSTIN", rdr.IsDBNull(9) ? string.Empty : rdr.GetString(9));
                            output.Add(temp);
                        }
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// update enquirydetails
        /// </summary>
        /// <param name="userId"> user id</param>
        /// <param name="photoPath">Profile Photo Path</param>
        public static void UpdateMemberProfilePicture(int userId, string photoPath)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UserProfile_UpdateMemberProfilePicture", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@photoPath", SqlDbType.VarChar).Value = photoPath;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get Payment Details
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate"> End Date</param>
        /// <param name="chapterId">ChapterId</param>
        /// <returns>Payment Details</returns>
        public static List<PaymentDetailsForExcel> GetPaymentDetailsForExcel(string startDate, string endDate, int chapterId)
        {
            var paymentList = new List<PaymentDetailsForExcel>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Payment_GetDetailsForExcel3", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@startDate", SqlDbType.NVarChar).Value = startDate;
                    cmd.Parameters.Add("@endDate", SqlDbType.NVarChar).Value = endDate;
                    cmd.Parameters.Add("@chapter", SqlDbType.Int).Value = chapterId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var payment = new PaymentDetailsForExcel(
                                rdr.GetString(0).ToString(), // memberid
                                rdr.GetString(1).ToString(), // firstname
                                rdr.GetString(2).ToString(), // lastname
                                rdr.GetString(3).ToString(), // phonenumber
                                rdr.GetString(4).ToString(), // emil
                                rdr.IsDBNull(5) ? null : rdr.GetString(5).ToString(), // unitname
                                rdr.IsDBNull(6) ? null : rdr.GetString(6).ToString(), //// chaptername
                                rdr.GetDouble(7).ToString(), // subtotal
                                rdr.GetInt32(8).ToString(), // igstpercent
                                rdr.GetInt32(9).ToString(), // cgst
                                rdr.GetInt32(10).ToString(), // sgst
                                rdr.GetDouble(11).ToString(), // igstval
                                rdr.GetDouble(12).ToString(), // igstval
                                rdr.GetDouble(13).ToString(), // igstval
                                rdr.GetString(14).ToString(), // payreason
                                rdr.GetString(15).ToString(), // paymode
                                rdr.GetString(16).ToString(), // cheq num
                                rdr.GetString(17).ToString(), // trans id
                                rdr.GetString(18).ToString(), // orderid
                                rdr.GetDouble(19).ToString(), // total
                                rdr.IsDBNull(20) ? null : rdr.GetString(20).ToString(), // onlinefee
                                rdr.IsDBNull(21) ? null : rdr.GetString(21).ToString(), // invoice patj
                                rdr.GetDateTime(22).ToString(), // time stamp
                                rdr.GetInt32(23).ToString(),  // status
                                rdr.GetString(24).ToString(),  // GSTIN
                                rdr.GetString(25).ToString(), // InvoiceNumber
                                rdr.GetDouble(26).ToString(), // HOShare
                                rdr.GetDouble(27).ToString()); // ChapterShare
                            paymentList.Add(payment);
                        }
                    }
                }
            }

            return paymentList;
        }

        /// <summary>
        /// Get Payment Details for NonMember
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate"> End Date</param>
        /// <returns>Payment Details</returns>
        public static List<PaymentDetailsForExcel> GetPaymentDetailsForExcelNonMember(string startDate, string endDate)
        {
            var paymentList = new List<PaymentDetailsForExcel>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Payment_GetDetailsForExcel3NonMember", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@startDate", SqlDbType.NVarChar).Value = startDate;
                    cmd.Parameters.Add("@endDate", SqlDbType.NVarChar).Value = endDate;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var payment = new PaymentDetailsForExcel(
                                "-", // memberid
                                "-", // firstname
                                "-", // lastname
                                rdr.GetString(0).ToString(), // phonenumber
                                "-", // email
                                rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1).ToString(), // unitname
                                rdr.IsDBNull(14) ? string.Empty : rdr.GetString(14).ToString(), //// chaptername
                                rdr.GetDouble(2).ToString(), // subtotal
                                "-", // igstpercent
                                "-", // cgst
                                "-", // sgst
                                rdr.GetDouble(3).ToString(), // igstval
                                rdr.GetDouble(4).ToString(), // igstval
                                rdr.GetDouble(5).ToString(), // igstval
                                "-", // payreason
                                rdr.GetString(6).ToString(), // paymode
                                rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7).ToString(), // cheq num
                                "-", // trans id
                                "-", // orderid
                                rdr.GetDouble(8).ToString(), // total
                                "-", // onlinefee
                                rdr.IsDBNull(9) ? string.Empty : rdr.GetString(9).ToString(), // invoice patj
                                rdr.GetDateTime(10).ToString(), // time stamp
                                rdr.GetInt32(11).ToString(),  // status
                                rdr.GetString(12).ToString(),  // GSTIN
                                rdr.IsDBNull(13) ? string.Empty : rdr.GetString(13).ToString(), // InvoiceNumber
                                "-", // HOShare
                                "-"); // ChapterShare
                            paymentList.Add(payment);
                        }
                    }
                }
            }

            return paymentList;
        }

        /// <summary>
        /// Get Payment Details
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate"> End Date</param>
        /// <param name="chapterId">ChapterId</param>
        /// <returns>Payment Details</returns>
        public static List<PaymentDetailsForExcel> GetPaymentDetailsForExcelFailed(string startDate, string endDate, int chapterId)
        {
            var paymentList = new List<PaymentDetailsForExcel>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Payment_GetDetailsForExcelFailed", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@startDate", SqlDbType.NVarChar).Value = startDate;
                    cmd.Parameters.Add("@endDate", SqlDbType.NVarChar).Value = endDate;
                    cmd.Parameters.Add("@chapter", SqlDbType.Int).Value = chapterId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var payment = new PaymentDetailsForExcel(
                                rdr.GetString(0).ToString(), // memberid
                                rdr.GetString(1).ToString(), // firstname
                                rdr.GetString(2).ToString(), // lastname
                                rdr.GetString(3).ToString(), // phonenumber
                                rdr.GetString(4).ToString(), // emil
                                rdr.IsDBNull(5) ? null : rdr.GetString(5).ToString(), // unitname
                                rdr.IsDBNull(6) ? null : rdr.GetString(6).ToString(), //// chaptername
                                rdr.GetDouble(7).ToString(), // subtotal
                                rdr.GetInt32(8).ToString(), // igstpercent
                                rdr.GetInt32(9).ToString(), // cgst
                                rdr.GetInt32(10).ToString(), // sgst
                                rdr.GetDouble(11).ToString(), // igstval
                                rdr.GetDouble(12).ToString(), // igstval
                                rdr.GetDouble(13).ToString(), // igstval
                                rdr.GetString(14).ToString(), // payreason
                                rdr.GetString(15).ToString(), // paymode
                                rdr.GetString(16).ToString(), // cheq num
                                rdr.GetString(17).ToString(), // trans id
                                rdr.GetString(18).ToString(), // orderid
                                rdr.GetDouble(19).ToString(), // total
                                rdr.IsDBNull(20) ? null : rdr.GetString(20).ToString(), // onlinefee
                                rdr.IsDBNull(21) ? null : rdr.GetString(21).ToString(), // invoice patj
                                rdr.GetDateTime(22).ToString(), // time stamp
                                rdr.GetInt32(23).ToString(),  // status
                                rdr.GetString(24).ToString(),  // GSTIN
                                rdr.GetString(25).ToString(), // InvoiceNumber
                                rdr.GetDouble(26).ToString(), // HOShare
                                rdr.GetDouble(27).ToString()); // ChapterShare
                            paymentList.Add(payment);
                        }
                    }
                }
            }

            return paymentList;
        }

        /// <summary>
        /// Get Invoice List
        /// </summary>
        /// <param name="startId">Start Date</param>
        /// <param name="endId"> End Date</param>
        /// <returns>Invoice List</returns>
        public static List<string> GetInvoiceList(int startId, int endId)
        {
            var invoiceList = new List<string>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("getInvoiceList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@startId", SqlDbType.Int).Value = startId;
                    cmd.Parameters.Add("@endId", SqlDbType.Int).Value = endId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            invoiceList.Add(rdr.GetString(0).ToString());
                        }
                    }
                }
            }

            return invoiceList;
        }

        /// <summary>
        /// Get Payment Details
        /// </summary>
        /// <param name="invoiceId">InvoiceId</param>
        /// <param name="operation">Operation</param>
        /// <returns>Payment Details</returns>
        public static List<dynamic> GetPaymentDetailsForExcel(string invoiceId, string operation)
        {
            var paymentList = new List<dynamic>();
            using (var conn = GetConnection())
            {
                if (operation == "update")
                {
                    using (SqlCommand cmd = new SqlCommand("Payment_GetDetailsForMissingInvoices", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@fullInvoiceId", SqlDbType.VarChar, 100).Value = invoiceId;
                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                Dictionary<string, dynamic> payment = new Dictionary<string, dynamic>();
                                payment.Add("UserId", rdr.GetInt32(0).ToString());
                                payment.Add("CreateDateTimeStamp", rdr.GetDateTime(1).ToString());
                                payment.Add("InvoiceId", rdr.GetInt32(2).ToString());
                                payment.Add("SGSTPercent", rdr.GetInt32(3).ToString());
                                payment.Add("IGSTPercent", rdr.GetInt32(4).ToString());
                                payment.Add("CGSTValue", rdr.GetDouble(5).ToString());
                                payment.Add("SGSTValue", rdr.GetDouble(6).ToString());
                                payment.Add("IGSTValue", rdr.GetDouble(7).ToString());
                                payment.Add("SubTotal", rdr.GetDouble(8).ToString());
                                payment.Add("Total", rdr.GetDouble(9).ToString());
                                payment.Add("PaymentMode", rdr.GetString(10).ToString());
                                payment.Add("ChequeNumber", rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11).ToString());
                                payment.Add("AdminId", rdr.GetInt32(12).ToString());
                                payment.Add("expiryYear", Math.Abs(rdr.GetInt32(13)).ToString());
                                payment.Add("CGSTPercent", rdr.GetInt32(14).ToString());
                                payment.Add("PaymentReason", rdr.GetString(15).ToString());
                                payment.Add("sourceGST", rdr.GetString(16).ToString());

                                paymentList.Add(payment);
                            }
                        }
                    }
                }
                else if (operation == "delete")
                {
                    using (SqlCommand cmd = new SqlCommand("Payment_DeletePayment", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@fullinvoiceId", SqlDbType.VarChar, 100).Value = invoiceId;
                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                FunctionUtility.DeletePayemnt(rdr.GetInt32(0), rdr.GetInt32(1));
                            }
                        }
                    }
                }
            }

            return paymentList;
        }

        /// <summary>
        /// Get Payment Details for Non Members
        /// </summary>
        /// <param name="invoiceId">InvoiceId</param>
        /// <param name="operation">Operation</param>
        /// <returns>Payment Details</returns>
        public static List<dynamic> GetPaymentDetailsForNonMemberExcel(int invoiceId, string operation)
        {
            var paymentList = new List<dynamic>();
            using (var conn = GetConnection())
            {
                if (operation == "update")
                {
                    using (SqlCommand cmd = new SqlCommand("Payment_GetDetailsForMissingInvoicesNonMember", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@invoiceId", SqlDbType.Int).Value = invoiceId;
                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                Dictionary<string, dynamic> payment = new Dictionary<string, dynamic>();
                                payment.Add("Name", rdr.GetString(0).ToString());
                                payment.Add("PhoneNumber", rdr.GetString(1).ToString());
                                payment.Add("Address", rdr.GetString(2).ToString());
                                payment.Add("GSTIN", rdr.GetString(3).ToString());
                                payment.Add("ChequeDate", rdr.GetDateTime(4).ToString());
                                payment.Add("State", rdr.GetString(5).ToString());
                                payment.Add("AdminId", rdr.GetInt32(6).ToString());
                                payment.Add("SubTotal", rdr.GetDouble(7).ToString());
                                payment.Add("IGSTValue", rdr.GetDouble(8).ToString());
                                payment.Add("CGSTValue", rdr.GetDouble(9).ToString());
                                payment.Add("SGSTValue", rdr.GetDouble(10).ToString());
                                payment.Add("PaymentMode", rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11).ToString());
                                payment.Add("ChequeNumber", rdr.GetString(12).ToString());
                                payment.Add("Total", Math.Abs(rdr.GetDouble(13)).ToString());
                                payment.Add("InvoicePath", rdr.IsDBNull(14) ? string.Empty : rdr.GetString(14).ToString());
                                payment.Add("CreateDateTimeStamp", rdr.GetDateTime(15).ToString());
                                payment.Add("status", rdr.GetInt32(16).ToString());
                                payment.Add("InvoiceId", rdr.GetInt32(17).ToString());
                                payment.Add("sourceGST", rdr.GetString(18).ToString());
                                paymentList.Add(payment);
                            }
                        }
                    }
                }
                else if (operation == "delete")
                {
                    using (SqlCommand cmd = new SqlCommand("Payment_DeletePaymentNonMember", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@invoiceId", SqlDbType.Int).Value = invoiceId;
                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                FunctionUtility.DeletePayemnt(rdr.GetInt32(0), rdr.GetInt32(1));
                            }
                        }
                    }
                }
            }

            return paymentList;
        }

        /// <summary>
        /// Get Item Details for Non Members
        /// </summary>
        /// <param name="invoiceId">InvoiceId</param>
        /// <returns>Payment Details</returns>
        public static List<dynamic> GetItemsForNonMemberExcel(int invoiceId)
        {
            var itemList = new List<dynamic>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Payment_GetItemDetailsForNonMember", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@invoiceId", SqlDbType.Int).Value = invoiceId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Dictionary<string, dynamic> item = new Dictionary<string, dynamic>();
                            item.Add("ItemName", rdr.GetString(0).ToString());
                            item.Add("GSTRate", rdr.GetString(1).ToString());
                            item.Add("SAC", rdr.GetString(2).ToString());
                            item.Add("Quantity", rdr.GetInt32(3).ToString());
                            item.Add("UnitPrice", rdr.GetInt32(4).ToString());
                            itemList.Add(item);
                        }
                    }
                }
            }

            return itemList;
        }

        /// <summary>
        /// Get Payment Details
        /// </summary>
        /// /// <param name="startDate">Start date</param>
        /// <param name="endDate">End Date</param>
        /// <param name="chapterId">CHapter Id</param>
        /// <returns>Payment Details</returns>
        public static List<Dictionary<string, dynamic>> GetPaymentDetailsForExcelForGST(string startDate, string endDate, int chapterId)
        {
            List<Dictionary<string, dynamic>> paymentList = new List<Dictionary<string, dynamic>>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Payment_GetDetailsForExcelForGST", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@startDate", SqlDbType.NVarChar).Value = startDate;
                    cmd.Parameters.Add("@endDate", SqlDbType.NVarChar).Value = endDate;
                    cmd.Parameters.Add("@chapter", SqlDbType.NVarChar).Value = chapterId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Dictionary<string, dynamic> payment = new Dictionary<string, dynamic>();
                            payment.Add("GSTIN", rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0));
                            payment.Add("UnitName", rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1));
                            payment.Add("InvoiceNumber", rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2));
                            payment.Add("InvoiceDate", rdr.IsDBNull(3) ? string.Empty : rdr.GetDateTime(3).ToString("dd-MMM-yy"));
                            payment.Add("InvoiceValue", rdr.IsDBNull(4) ? 0 : rdr.GetDouble(4));
                            payment.Add("TaxableAmount", rdr.IsDBNull(5) ? 0 : rdr.GetDouble(5));
                            payment.Add("MemberId", rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6));
                            payment.Add("CGST", rdr.IsDBNull(7) ? 0 : rdr.GetDouble(7));
                            payment.Add("SGST", rdr.IsDBNull(8) ? 0 : rdr.GetDouble(8));
                            payment.Add("IGST", rdr.IsDBNull(9) ? 0 : rdr.GetDouble(9));
                            payment.Add("ChapterName", rdr.IsDBNull(10) ? string.Empty : rdr.GetString(10));
                            payment.Add("sourceGST", rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11));
                            payment.Add("CGSTPercent", rdr.IsDBNull(12) ? 0 : rdr.GetInt32(12));
                            payment.Add("IGSTPercent", rdr.IsDBNull(13) ? 0 : rdr.GetInt32(13));
                            payment.Add("State", rdr.IsDBNull(14) ? string.Empty : rdr.GetString(14));
                            paymentList.Add(payment);
                        }
                    }
                }
            }

            return paymentList;
        }

        /// <summary>
        /// Get Payment Details for Non Member
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>Payment Details</returns>
        public static List<Dictionary<string, dynamic>> GetPaymentDetailsForExcelForGSTNonMember2(string startDate, string endDate)
        {
            List<Dictionary<string, dynamic>> paymentList = new List<Dictionary<string, dynamic>>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Payment_GetDetailsForExcelForGSTNonMember2", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@startDate", SqlDbType.NVarChar).Value = startDate;
                    cmd.Parameters.Add("@endDate", SqlDbType.NVarChar).Value = endDate;
                    conn.Open();
                    DateTime curr = DateTime.Now;
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Dictionary<string, dynamic> payment = new Dictionary<string, dynamic>();
                            payment.Add("GSTIN", rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0));
                            payment.Add("UnitName", rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1));
                            payment.Add("InvoiceNumber", rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2));
                            payment.Add("InvoiceDate", rdr.IsDBNull(3) ? string.Empty : rdr.GetDateTime(3).ToString("dd-MMM-yy"));
                            payment.Add("InvoiceValue", rdr.IsDBNull(4) ? 0 : rdr.GetDouble(4));
                            payment.Add("TaxableAmount", rdr.IsDBNull(5) ? 0 : rdr.GetDouble(5));
                            payment.Add("MemberId", "-");
                            payment.Add("CGST", rdr.IsDBNull(6) ? 0 : rdr.GetDouble(6));
                            payment.Add("SGST", rdr.IsDBNull(7) ? 0 : rdr.GetDouble(7));
                            payment.Add("IGST", rdr.IsDBNull(8) ? 0 : rdr.GetDouble(8));
                            payment.Add("ChapterName", rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11));
                            payment.Add("sourceGST", rdr.IsDBNull(12) ? string.Empty : rdr.GetString(12));
                            payment.Add("State", rdr.IsDBNull(13) ? string.Empty : rdr.GetString(13));
                            paymentList.Add(payment);
                        }
                    }
                }
            }

            return paymentList;
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
        /// Inserts a new user profile on Database sync
        /// </summary>
        /// <param name="userProfile">UserCompanyProfile</param>
        /// <param name="adminId">adminId</param>
        public static void DatabaseSyncInsert(UserProfile userProfile, int adminId)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("UserProfile_DatabaseSync", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = userProfile.Id;
                        cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = userProfile.PhoneNumber != null ? userProfile.PhoneNumber : string.Empty;
                        cmd.Parameters.Add("@MembershipId", SqlDbType.VarChar).Value = userProfile.MembershipId != null ? userProfile.MembershipId : string.Empty;
                        cmd.Parameters.Add("@MembershipAdmissionfee", SqlDbType.Int).Value = userProfile.MembershipAdmissionfee;
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
                        cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = userProfile.DateOfBirth != null ? userProfile.DateOfBirth : null;
                        cmd.Parameters.Add("@DateOfMarriage", SqlDbType.Date).Value = userProfile.DateOfMarriage != null ? userProfile.DateOfMarriage : null;
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
        /// Get admin name
        /// </summary>
        /// <param name="adminId">AdminId</param>
        /// <returns>Returned Admin Name</returns>
        public static string GetAdminName(int adminId)
        {
            string adminName = string.Empty;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_GetAdminName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AdminId", SqlDbType.Int).Value = adminId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            adminName = rdr.GetString(0);
                        }
                    }
                }
            }

            return adminName;
        }

        /// <summary>
        /// Get admin source GST
        /// </summary>
        /// <param name="adminId">AdminId</param>
        /// <returns>Returned Admin Name</returns>
        public static string GetAdminSourceGST(int adminId)
        {
            string adminSourceGST = string.Empty;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Admin_GetAdminSourceGst", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AdminId", SqlDbType.Int).Value = adminId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            adminSourceGST = rdr.GetString(0);
                        }
                    }
                }
            }

            return adminSourceGST;
        }

        /// <summary>
        /// Updates the Ticket
        /// </summary>
        /// <param name="ticketNumber">ticket number</param>
        /// <param name="committeeId">Committee unique Id</param>
        public static void UpdateTicket(int ticketNumber, int committeeId)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateTicketWithCommitteeId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ticketNumber", SqlDbType.Int).Value = ticketNumber;
                        cmd.Parameters.Add("@committeeId", SqlDbType.Int).Value = committeeId;
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
        /// Get Phone numbers of Chairmen
        /// </summary>
        /// <returns>List of PhoneNumbers</returns>
        /// <param name="id">id</param>
        public static List<WaMemberDetailsModal> GetPhoneNumberOfAlottedChairmen(int id)
        {
            List<WaMemberDetailsModal> newData = new List<WaMemberDetailsModal>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetPhoneNumberOfAlottedChairmen", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            WaMemberDetailsModal newdetail = new WaMemberDetailsModal(
                                rdr.IsDBNull(0) ? 0 : (int)(byte)rdr.GetByte(0),
                                rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                                rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2));
                            newData.Add(newdetail);
                        }
                    }
                }
            }

            return newData;
        }

        /// <summary>
        /// Get committee names of Chairmen
        /// </summary>
        /// <returns>name of committee</returns>
        /// <param name="id">committee id</param>
        public static string GetCommitteeOfAlottedChairmen(int id)
        {
            string newData = string.Empty;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetCommitteeOfAlottedChairmen", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            newData = rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0);
                        }
                    }
                }
            }

            return newData;
        }

        /// <summary>
        /// Get sourceGST for members
        /// </summary>
        /// <returns>name of committee</returns>
        /// <param name="invoiceId">invoice id</param>
        public static string GetSourceGST(int invoiceId)
        {
            string sourceGST = string.Empty;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetSourceGST", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@invoiceId", SqlDbType.Int).Value = invoiceId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            sourceGST = rdr.IsDBNull(0) ? "09AAATI4647K1ZB" : rdr.GetString(0);
                        }
                    }
                }
            }

            return sourceGST;
        }

        /// <summary>
        /// Get sourceGST for non members
        /// </summary>
        /// <returns>name of committee</returns>
        /// <param name="invoiceId">invoice id</param>
        public static string GetSourceGSTForNonMember(int invoiceId)
        {
            string sourceGST = string.Empty;
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetSourceGSTForNonMember", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@invoiceId", SqlDbType.Int).Value = invoiceId;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            sourceGST = rdr.IsDBNull(0) ? "09AAATI4647K1ZB" : rdr.GetString(0);
                        }
                    }
                }
            }

            return sourceGST;
        }

        /// <summary>
        /// Get new Members Data
        /// </summary>
        /// <returns>List of PhoneNumbers</returns>
        /// <param name="dateType">start position</param>
        public static List<WaMemberDetailsModal> GetPhoneNumberForBirthdayAnniversaryMessage(string dateType)
        {
            List<WaMemberDetailsModal> newData = new List<WaMemberDetailsModal>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetPhoneNumberForBirthdayAnniversaryMessage", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dateType", SqlDbType.VarChar).Value = dateType;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            WaMemberDetailsModal newdetail = new WaMemberDetailsModal(
                                rdr.IsDBNull(0) ? 0 : rdr.GetInt32(0),
                                rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                                rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2));
                            newData.Add(newdetail);
                        }
                    }
                }
            }

            return newData;
        }

        /// <summary>
        /// Creates a magazine
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="magazinePath">Magazine Path</param>
        /// <param name="creatorAdminId">Creator Admin ID</param>
        /// <param name="magazineMonth">Magazine Month</param>
        /// <param name="magazineYear">Magazine Year</param>
        /// <param name="coverPhotoPath">Cover photo path</param>
        public static void CreateMagazine(string title, string magazinePath, int creatorAdminId, string magazineMonth, string magazineYear, string coverPhotoPath)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("magazine_Createmagazine", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = title;
                    cmd.Parameters.Add("@MagazinePath", SqlDbType.VarChar).Value = magazinePath;
                    cmd.Parameters.Add("@CreatorAdminId", SqlDbType.Int).Value = creatorAdminId;
                    cmd.Parameters.Add("@CreationTime", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@MagazineMonth", SqlDbType.VarChar).Value = magazineMonth;
                    cmd.Parameters.Add("@MagazineYear", SqlDbType.VarChar).Value = magazineYear;
                    cmd.Parameters.Add("@CoverPhotoPath", SqlDbType.VarChar).Value = coverPhotoPath;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Delete Magazine
        /// </summary>
        /// <param name="id">Magazine Id</param>
        public static void DeleteMagazine(int id)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Magazine_DeleteMagazine", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get all the magazine for the current month
        /// </summary>
        /// <returns>List of magazine items</returns>
        public static List<Magazine> GetCurrentMagazine()
        {
            var magazineList = new List<Magazine>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Magazine_GetCurrentMonthMagazine", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var magazine = new Magazine(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetInt32(3),
                                rdr.GetDateTime(4),
                                rdr.GetInt32(5),
                                rdr.GetString(6).ToString(),
                                rdr.GetString(7).ToString(),
                                rdr.GetString(8).ToString());
                            magazineList.Add(magazine);
                        }
                    }
                }
            }

            return magazineList;
        }

        /// <summary>
        /// Get all the magazine for the current month
        /// </summary>
        /// <returns>List of magazine items</returns>
        public static List<Magazine> GetPastMagazine()
        {
            var magazineList = new List<Magazine>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Magazine_GetPastMagazine", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var magazine = new Magazine(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetInt32(3),
                                rdr.GetDateTime(4),
                                rdr.GetInt32(5),
                                rdr.GetString(6).ToString(),
                                rdr.GetString(7).ToString(),
                                rdr.GetString(8).ToString());
                            magazineList.Add(magazine);
                        }
                    }
                }
            }

            return magazineList;
        }

        /// <summary>
        /// Get all the magazine for the current month
        /// </summary>
        /// <returns>List of magazine items</returns>
        /// <param name="magazineMonth">Magazine Month</param>
        /// <param name="magazineYear">Magazine Year</param>
        public static List<Magazine> GetMagazineByMonthAndYear(string magazineMonth, string magazineYear)
        {
            var magazineList = new List<Magazine>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Magazine_GetMagazineByMonthYear", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MagazineMonth", SqlDbType.VarChar).Value = magazineMonth;
                    cmd.Parameters.Add("@MagazineYear", SqlDbType.VarChar).Value = magazineYear;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var magazine = new Magazine(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetInt32(3),
                                rdr.GetDateTime(4),
                                rdr.GetInt32(5),
                                rdr.GetString(6).ToString(),
                                rdr.GetString(7).ToString(),
                                rdr.GetString(8).ToString());
                            magazineList.Add(magazine);
                        }
                    }
                }
            }

            return magazineList;
        }

        /// <summary>
        /// Get all the magazine
        /// </summary>
        /// <returns>List of magazine items</returns>
        public static List<Magazine> GetAllMagazine()
        {
            var magazineList = new List<Magazine>();

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Magazine_GetAllMagazine", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var magazine = new Magazine(
                                rdr.GetInt32(0).ToString(),
                                rdr.GetString(1).ToString(),
                                rdr.GetString(2).ToString(),
                                rdr.GetInt32(3),
                                rdr.GetDateTime(4),
                                rdr.GetInt32(5),
                                rdr.GetString(6).ToString(),
                                rdr.GetString(7).ToString(),
                                rdr.GetString(8).ToString());
                            magazineList.Add(magazine);
                        }
                    }
                }
            }

            return magazineList;
        }

        /// <summary>
        /// Get IIA Members List
        /// </summary>
        /// <param name="search">Search text</param>
        /// <returns>List of Active Members</returns>
        public static List<IIADirectory> GetIIADirectoryList(string search)
        {
            var directoryData = new List<IIADirectory>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetIIADirectory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@search", SqlDbType.VarChar).Value = search;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var iIADirectory = new IIADirectory(
                                rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0),
                                rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                                rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2),
                                rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3),
                                rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4),
                                rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5),
                                rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6),
                                rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7),
                                rdr.IsDBNull(8) ? string.Empty : rdr.GetString(8),
                                rdr.IsDBNull(9) ? string.Empty : rdr.GetString(9),
                                rdr.IsDBNull(10) ? string.Empty : rdr.GetString(10),
                                rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11),
                                rdr.IsDBNull(12) ? string.Empty : rdr.GetString(12),
                                rdr.IsDBNull(13) ? string.Empty : rdr.GetString(13),
                                rdr.IsDBNull(13) ? string.Empty : rdr.GetString(14));
                            directoryData.Add(iIADirectory);
                        }
                    }
                }
            }

            return directoryData;
        }

        /// <summary>
        /// Get List of GST for active profiles and not in GSTBusiness table
        /// </summary>
        /// <returns>List of Active Members</returns>
        public static List<string> GetListOfGST()
        {
            var gstList = new List<string>();
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetListOfGSTForBusiness", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var gst = new string(
                                rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0));
                            gstList.Add(gst);
                        }
                    }
                }
            }

            return gstList;
        }

        /// <summary>
        /// Insert in GST Business
        /// </summary>
        /// <param name="gst">GST</param>
        /// <param name="response">response</param>
        public static void InsertInGSTBusiness(string gst, string response)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GSTBusinessCor_InsertData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@gst", SqlDbType.VarChar).Value = gst;
                    cmd.Parameters.Add("@response", SqlDbType.VarChar).Value = response;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

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
    }
}
