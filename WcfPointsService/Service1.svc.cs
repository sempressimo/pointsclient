using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WcfPointsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class WebPointsService : IPointsService
    {
        string AppKey = "481ed410-2ae9-484c-a435-0764c2280a73";

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public int GetPointsBalance(string Key, int Customer_ID)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT SUM(Points) AS Points FROM Transactions WHERE Customer_ID = " + Customer_ID;

                SqlCommand cm = new SqlCommand(Sql, cn);
                cm.CommandType = CommandType.Text;

                object PointsTotal = cm.ExecuteScalar();

                if (PointsTotal == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    Sql = "SELECT SUM(PointsUsed) AS PointsUsed FROM Claims WHERE Customer_ID = " + Customer_ID;

                    cm = new SqlCommand(Sql, cn);
                    
                    object PointsUsed = cm.ExecuteScalar();

                    if (PointsUsed == DBNull.Value)
                    {
                        return Convert.ToInt32(PointsTotal);
                    }
                    else
                    {
                        return Convert.ToInt32(PointsTotal) - Convert.ToInt32(PointsUsed);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void EraseTable(string Key, string AdminPassword, string TableName)
        {
            if (Key != this.AppKey || AdminPassword != "iqc@prrh10")
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "DELETE FROM " + TableName;

                SqlCommand cm = new SqlCommand(Sql, cn);
                cm.CommandType = CommandType.Text;

                cm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void ClaimReward(string Key, int Customer_ID, DateTime Date, int Reward_ID, int PointsUsed, string Address, string Address2, string Town, string ZipCode, bool Claimed, decimal AmountPaid)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "INSERT INTO Claims(Customer_ID, Date, Reward_ID, PointsUsed, Address, Address2, Town, ZipCode, Claimed, AmountPaid) VALUES (@Customer_ID, @Date, @Reward_ID, @PointsUsed, @Address, @Address2, @Town, @ZipCode, @Claimed, @AmountPaid)";

                SqlCommand cm = new SqlCommand(Sql, cn);
                cm.CommandType = CommandType.Text;

                cm.Parameters.AddWithValue("@Customer_ID", Customer_ID);
                cm.Parameters.AddWithValue("@Date", Date);
                cm.Parameters.AddWithValue("@Reward_ID", Reward_ID);
                cm.Parameters.AddWithValue("@PointsUsed", PointsUsed);
                cm.Parameters.AddWithValue("@Address", Address);
                cm.Parameters.AddWithValue("@Address2", Address2);
                cm.Parameters.AddWithValue("@Town", Town);
                cm.Parameters.AddWithValue("@ZipCode", ZipCode);
                cm.Parameters.AddWithValue("@Claimed", Claimed);
                cm.Parameters.AddWithValue("@AmountPaid", AmountPaid);

                cm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void DeleteReward(string Key, int Reward_ID)
        {
            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "DELETE FROM Rewards WHERE Reward_ID = " + Reward_ID.ToString();

                SqlCommand cm = new SqlCommand(Sql, cn);
                cm.CommandType = CommandType.Text;

                cm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void DeleteCustomer(string Key, int Customer_ID)
        {
            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "DELETE FROM Customers WHERE Customer_ID = " + Customer_ID.ToString();

                SqlCommand cm = new SqlCommand(Sql, cn);
                cm.CommandType = CommandType.Text;

                cm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetBalanceReport(string Key, bool IncludeOnlyWithBalance)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string WhereStatement = "";
                if (IncludeOnlyWithBalance)
                {
                    WhereStatement = "WHERE BAL > 0 ";
                }

                string Sql = "SELECT T.Customer_ID, CU.Customer_Name, CU.Email, CU.Phone_Number, COUNT(Transaction_ID) As Transactions, SUM(T.Points) As Points, ISNULL(C.PointsUsed, 0) AS PointsUsed, (SUM(Points) - ISNULL(PointsUsed,0)) AS BAL FROM Transactions T " +
                    "LEFT JOIN (SELECT C.Customer_ID, SUM(C.PointsUsed) AS PointsUsed FROM Claims C	GROUP BY Customer_ID) C ON C.Customer_ID = T.Customer_ID INNER JOIN Customers CU ON CU.Customer_ID = T.Customer_ID " +
                    WhereStatement +
                    "GROUP BY T.Customer_ID, CU.Customer_Name, CU.Email, CU.Phone_Number, C.PointsUsed";

                DataSet ds = new DataSet("dsBalance");

                SqlDataAdapter da = new SqlDataAdapter(Sql, cn);
                da.Fill(ds, "Balance");

                if (ds != null)
                {
                    return ds.Tables["Balance"];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetCustomers(string Key, string SearchName)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string WhereStatement = "";
                if (!string.IsNullOrEmpty(SearchName))
                {
                    WhereStatement = "WHERE Customer_Name LIKE '%" +  SearchName + "%'";
                }

                string Sql = "SELECT * FROM Customers " + WhereStatement + " ORDER BY Customer_Name";

                DataSet ds = new DataSet("dsCustomers");

                SqlDataAdapter da = new SqlDataAdapter(Sql, cn);
                da.Fill(ds, "Customers");

                if (ds != null)
                {
                    return ds.Tables["Customers"];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public string GetFilesPath(string Key, int Setting_ID)
        {
            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT FilesPath FROM Settings WHERE Setting_ID = " + Setting_ID.ToString();

                SqlCommand cm = new SqlCommand(Sql, cn);
                cm.CommandType = CommandType.Text;

                object FPath = cm.ExecuteScalar();

                if (FPath != DBNull.Value)
                {
                    return FPath.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public string GetManagerPassword(string Key, int Setting_ID)
        {
            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT ManagerPassword FROM Settings WHERE Setting_ID = " + Setting_ID.ToString();

                SqlCommand cm = new SqlCommand(Sql, cn);
                cm.CommandType = CommandType.Text;

                object MPass = cm.ExecuteScalar();

                if (MPass != DBNull.Value)
                {
                    return MPass.ToString();
                }
                else
                {
                    throw new Exception("Password not set.");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public decimal GetRewardCashForPoints(string Key, int Reward_ID)
        {
            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT CashForPoints FROM Rewards WHERE Reward_ID = " + Reward_ID.ToString();

                SqlCommand cm = new SqlCommand(Sql, cn);
                cm.CommandType = CommandType.Text;

                object Cost = cm.ExecuteScalar();

                if (Cost != DBNull.Value)
                {
                    return Convert.ToDecimal(Cost);
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public int GetRewardCost(string Key, int Reward_ID)
        {
            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT Cost FROM Rewards WHERE Reward_ID = " + Reward_ID.ToString();

                SqlCommand cm = new SqlCommand(Sql, cn);
                cm.CommandType = CommandType.Text;

                object Cost = cm.ExecuteScalar();

                if (Cost != DBNull.Value)
                {
                    return Convert.ToInt32(Cost);
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public int GetCustomerID(string Key, string Phone_Number)
        {
            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT Customer_ID FROM Customers WHERE Phone_Number = '" + Phone_Number.ToString() + "'";

                SqlCommand cm = new SqlCommand(Sql, cn);
                cm.CommandType = CommandType.Text;

                object Customer_ID = cm.ExecuteScalar();

                if (Customer_ID != DBNull.Value && Customer_ID != null)
                {
                    return Convert.ToInt32(Customer_ID);
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetRewards(string Key)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT * FROM Rewards ORDER BY Reward_Name";

                DataSet ds = new DataSet("dsRewards");

                SqlDataAdapter da = new SqlDataAdapter(Sql, cn);
                da.Fill(ds, "Rewards");

                if (ds != null)
                {
                    return ds.Tables["Rewards"];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetAllRewardCategories(string Key)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT * FROM Reward_Categories ORDER BY Reward_Category_Description";

                DataSet ds = new DataSet("dsReward_Categories");

                SqlDataAdapter da = new SqlDataAdapter(Sql, cn);
                da.Fill(ds, "Reward_Categories");

                if (ds != null)
                {
                    return ds.Tables["Reward_Categories"];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetAllClaims(string Key)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT * FROM Claims";

                DataSet ds = new DataSet("dsClaims");

                SqlDataAdapter da = new SqlDataAdapter(Sql, cn);
                da.Fill(ds, "Claims");

                if (ds != null)
                {
                    return ds.Tables["Claims"];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetClaims(string Key, string Customer_ID)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT C.*, R. Reward_Name FROM Claims C INNER JOIN Rewards R ON R.Reward_ID = C.Reward_ID WHERE Customer_ID = " + Customer_ID;

                DataSet ds = new DataSet("dsClaims");

                SqlDataAdapter da = new SqlDataAdapter(Sql, cn);
                da.Fill(ds, "Claims");

                if (ds != null)
                {
                    return ds.Tables["Claims"];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetCatalog(string Key)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT * FROM Rewards ORDER BY Reward_Name";

                DataSet ds = new DataSet("dsRewards");

                SqlDataAdapter da = new SqlDataAdapter(Sql, cn);
                da.Fill(ds, "Rewards");

                if (ds != null)
                {
                    return ds.Tables["Rewards"];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetAllTransactions(string Key)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT * FROM Transactions";

                DataSet ds = new DataSet("dsTransactions");

                SqlDataAdapter da = new SqlDataAdapter(Sql, cn);
                da.Fill(ds, "Transactions");

                if (ds != null)
                {
                    return ds.Tables["Transactions"];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetTransactions(string Key, string Customer_ID)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT * FROM Transactions WHERE Customer_ID = " + Customer_ID;

                DataSet ds = new DataSet("dsTransactions");

                SqlDataAdapter da = new SqlDataAdapter(Sql, cn);
                da.Fill(ds, "Transactions");

                if (ds != null)
                {
                    return ds.Tables["Transactions"];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public bool ROExists(string Key, string RO_Number)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT RO_Number FROM Transactions WHERE RO_Number = '" + RO_Number + "'";

                SqlCommand cm = new SqlCommand(Sql, cn);
                object Num = cm.ExecuteScalar();

                if (Num != null)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public bool CustomerExists(string Key, string PhoneNumber)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT Phone_Number FROM Customers WHERE Phone_Number = '" + PhoneNumber + "'";

                SqlCommand cm = new SqlCommand(Sql, cn);
                object Phone_Number = cm.ExecuteScalar();

                if (Phone_Number != null)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetClaim(string Key, int Claim_ID)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT * FROM Claims WHERE Claim_ID = " + Claim_ID;

                DataSet ds = new DataSet("dsClaim");

                SqlDataAdapter da = new SqlDataAdapter(Sql, cn);
                da.Fill(ds, "Claim");

                if (ds != null)
                {
                    return ds.Tables["Claim"];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetReward(string Key, int Reward_ID)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT * FROM Rewards WHERE Reward_ID = " + Reward_ID;

                DataSet ds = new DataSet("dsReward");

                SqlDataAdapter da = new SqlDataAdapter(Sql, cn);
                da.Fill(ds, "Reward");

                if (ds != null)
                {
                    return ds.Tables["Reward"];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public DataTable GetCustomer(string Key, string FullName, string PhoneNumber)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "SELECT * FROM Customers WHERE Phone_Number = '" + PhoneNumber + "'";

                DataSet ds = new DataSet("dsCustomer");

                SqlDataAdapter da = new SqlDataAdapter(Sql, cn);
                da.Fill(ds, "Customer");

                if (ds != null)
                {
                    return ds.Tables["Customer"];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void SetClaimAsPaid(string Key, int Claim_ID, bool Paid)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "UPDATE Claims SET Claimed = @Claimed " +
                    "WHERE Claim_ID = " + Claim_ID;

                SqlCommand cm = new SqlCommand(Sql, cn);

                cm.Parameters.AddWithValue("@Claimed", Paid);

                cm.CommandType = CommandType.Text;

                cm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void UpdateFilesPath(string Key, int Setting_ID, string NewPath)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "UPDATE Settings SET FilesPath = @FilesPath " +
                    "WHERE Setting_ID = " + Setting_ID;

                SqlCommand cm = new SqlCommand(Sql, cn);

                cm.Parameters.AddWithValue("@FilesPath", NewPath);

                cm.CommandType = CommandType.Text;

                cm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void UpdateManagerPassword(string Key, int Setting_ID, string NewPassword)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "UPDATE Settings SET ManagerPassword = @ManagerPassword " +
                    "WHERE Setting_ID = " + Setting_ID;

                SqlCommand cm = new SqlCommand(Sql, cn);

                cm.Parameters.AddWithValue("@ManagerPassword", NewPassword);

                cm.CommandType = CommandType.Text;

                cm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void UpdateReward(string Key, int Reward_ID, string Reward_Name, int Reward_Category_ID, string Reward_Description, int Cost, Boolean IsActive, decimal CashForPoints)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "UPDATE Rewards SET Reward_Name = @Reward_Name, Reward_Category_ID = @Reward_Category_ID, Reward_Description = @Reward_Description, Cost = @Cost, IsActive = @IsActive, CashForPoints = @CashForPoints " +
                    "WHERE Reward_ID = " + Reward_ID;

                SqlCommand cm = new SqlCommand(Sql, cn);

                cm.Parameters.AddWithValue("@Reward_Name", Reward_Name);
                cm.Parameters.AddWithValue("@Reward_Category_ID", Reward_Category_ID);
                cm.Parameters.AddWithValue("@Reward_Description", Reward_Description);
                cm.Parameters.AddWithValue("@Cost", Cost);
                cm.Parameters.AddWithValue("@IsActive", IsActive);
                cm.Parameters.AddWithValue("@CashForPoints", CashForPoints);

                cm.CommandType = CommandType.Text;

                cm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void UpdateCustomer(string Key, int Customer_ID, string FullName, string PhoneNumber, string Password, string VINs, string Email)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "UPDATE Customers SET Customer_Name = @Customer_Name, Phone_Number = @Phone_Number, Password = @Password, VINs = @VINs, Email = @Email " +
                    "WHERE Customer_ID = " + Customer_ID;

                SqlCommand cm = new SqlCommand(Sql, cn);

                cm.Parameters.AddWithValue("@Customer_Name", FullName);
                cm.Parameters.AddWithValue("@Phone_Number", PhoneNumber);
                cm.Parameters.AddWithValue("@Password", Password);
                cm.Parameters.AddWithValue("@VINs", VINs);
                cm.Parameters.AddWithValue("@Email", Email);

                cm.CommandType = CommandType.Text;

                cm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void RegisterReward(string Key, string Reward_Name, int Reward_Category_ID, string Reward_Description, int Cost, Boolean IsActive, decimal CashForPoints)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "INSERT INTO Rewards(Reward_Name, Reward_Category_ID, Reward_Description, Cost, IsActive, CashForPoints) VALUES(@Reward_Name, @Reward_Category_ID, @Reward_Description, @Cost, @IsActive, @CashForPoints)";

                SqlCommand cm = new SqlCommand(Sql, cn);

                cm.Parameters.AddWithValue("@Reward_Name", Reward_Name);
                cm.Parameters.AddWithValue("@Reward_Category_ID", Reward_Category_ID);
                cm.Parameters.AddWithValue("@Reward_Description", Reward_Description);
                cm.Parameters.AddWithValue("@Cost", Cost);
                cm.Parameters.AddWithValue("@IsActive", IsActive);
                cm.Parameters.AddWithValue("@CashForPoints", CashForPoints);

                cm.CommandType = CommandType.Text;

                cm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void RegisterCustomer(string Key, string FullName, string PhoneNumber, string Password, string VINs, string Email)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "INSERT INTO Customers(Customer_Name, Phone_Number, Password, VINs, Email) VALUES(@Customer_Name, @Phone_Number, @Password, @VINs, @Email)";

                SqlCommand cm = new SqlCommand(Sql, cn);

                cm.Parameters.AddWithValue("@Customer_Name", FullName);
                cm.Parameters.AddWithValue("@Phone_Number", PhoneNumber);
                cm.Parameters.AddWithValue("@Password", Password);
                cm.Parameters.AddWithValue("@VINs", VINs);
                cm.Parameters.AddWithValue("@Email", Email);

                cm.CommandType = CommandType.Text;

                cm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public void RegisterPoints(string Key, int Customer_ID, DateTime Transaction_Date, string RO_Number, string VIN, int Points, bool ManagerGift, string GiftReason)
        {
            if (Key != this.AppKey)
            {
                throw new Exception("Invalid security key.");
            }

            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Points"].ConnectionString);
                cn.Open();

                string Sql = "INSERT INTO Transactions(Customer_ID, Transaction_Date, RO_Number, VIN, Points, ManagerGift, GiftReason) " +
                    "VALUES(@Customer_ID, @Transaction_Date, @RO_Number, @VIN, @Points, @ManagerGift, @GiftReason)";

                SqlCommand cm = new SqlCommand(Sql, cn);
                cm.CommandType = CommandType.Text;

                cm.Parameters.AddWithValue("@Customer_ID", Customer_ID);
                cm.Parameters.AddWithValue("@Transaction_Date", Transaction_Date);
                cm.Parameters.AddWithValue("@RO_Number", RO_Number);
                cm.Parameters.AddWithValue("@VIN", VIN);
                cm.Parameters.AddWithValue("@Points", Points);
                cm.Parameters.AddWithValue("@ManagerGift", ManagerGift);
                cm.Parameters.AddWithValue("@GiftReason", GiftReason);

                cm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
