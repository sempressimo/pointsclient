using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace WcfPointsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPointsService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here

        [OperationContract]
        DataTable GetBalanceReport(string Key, bool IncludeOnlyWithBalance);

        [OperationContract]
        void RegisterPoints(string Key, int Customer_ID, DateTime Transaction_Date, string RO_Number, string VIN, int Points, bool ManagerGift, string GiftReason);

        [OperationContract]
        void RegisterCustomer(string Key, string FullName, string PhoneNumber, string Password, string VINs, string Email);

        [OperationContract]
        DataTable GetCustomer(string Key, string FullName, string PhoneNumber);

        [OperationContract]
        DataTable GetTransactions(string Key, string Customer_ID);

        [OperationContract]
        void ClaimReward(string Key, int Customer_ID, DateTime Date, int Reward_ID, int PointsUsed, string Address, string Address2, string Town, string ZipCode, bool Claimed, decimal AmountPaid);

        [OperationContract]
        DataTable GetClaims(string Key, string Customer_ID);

        [OperationContract]
        int GetPointsBalance(string Key, int Customer_ID);

        [OperationContract]
        DataTable GetCustomers(string Key, string SearchName);

        [OperationContract]
        DataTable GetCatalog(string Key);

        [OperationContract]
        bool CustomerExists(string Key, string PhoneNumber);

        [OperationContract]
        void UpdateCustomer(string Key, int Customer_ID, string FullName, string PhoneNumber, string Password, string VINs, string Email);

        [OperationContract]
        void DeleteCustomer(string Key, int Customer_ID);

        [OperationContract]
        void EraseTable(string Key, string AdminPassword, string TableName);

        [OperationContract]
        DataTable GetRewards(string Key);

        [OperationContract]
        int GetRewardCost(string Key, int Reward_ID);

        [OperationContract]
        void SetClaimAsPaid(string Key, int Claim_ID, bool Paid);

        [OperationContract]
        DataTable GetClaim(string Key, int Claim_ID);

        [OperationContract]
        DataTable GetAllTransactions(string Key);

        [OperationContract]
        DataTable GetAllClaims(string Key);

        [OperationContract]
        decimal GetRewardCashForPoints(string Key, int Reward_ID);

        [OperationContract]
        void RegisterReward(string Key, string Reward_Name, int Reward_Category_ID, string Reward_Description, int Cost, Boolean IsActive, decimal CashForPoints);

        [OperationContract]
        DataTable GetReward(string Key, int Reward_ID);

        [OperationContract]
        void DeleteReward(string Key, int Reward_ID);

        [OperationContract]
        DataTable GetAllRewardCategories(string Key);

        [OperationContract]
        void UpdateReward(string Key, int Reward_ID, string Reward_Name, int Reward_Category_ID, string Reward_Description, int Cost, Boolean IsActive, decimal CashForPoints);

        [OperationContract]
        string GetManagerPassword(string Key, int Setting_ID);

        [OperationContract]
        void UpdateManagerPassword(string Key, int Setting_ID, string NewPassword);

        [OperationContract]
        string GetFilesPath(string Key, int Setting_ID);

        [OperationContract]
        void UpdateFilesPath(string Key, int Setting_ID, string NewPath);

        [OperationContract]
        bool ROExists(string Key, string RO_Number);

        [OperationContract]
        int GetCustomerID(string Key, string Phone_Number);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
