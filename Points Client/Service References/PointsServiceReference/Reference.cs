﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Points_Client.PointsServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/WcfPointsService")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PointsServiceReference.IPointsService")]
    public interface IPointsService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetData", ReplyAction="http://tempuri.org/IPointsService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IPointsService/GetDataUsingDataContractResponse")]
        Points_Client.PointsServiceReference.CompositeType GetDataUsingDataContract(Points_Client.PointsServiceReference.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetBalanceReport", ReplyAction="http://tempuri.org/IPointsService/GetBalanceReportResponse")]
        System.Data.DataTable GetBalanceReport(string Key, bool IncludeOnlyWithBalance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/RegisterPoints", ReplyAction="http://tempuri.org/IPointsService/RegisterPointsResponse")]
        void RegisterPoints(string Key, int Customer_ID, System.DateTime Transaction_Date, string RO_Number, string VIN, int Points, bool ManagerGift, string GiftReason);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/RegisterCustomer", ReplyAction="http://tempuri.org/IPointsService/RegisterCustomerResponse")]
        void RegisterCustomer(string Key, string FullName, string PhoneNumber, string Password, string VINs, string Email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetCustomer", ReplyAction="http://tempuri.org/IPointsService/GetCustomerResponse")]
        System.Data.DataTable GetCustomer(string Key, string FullName, string PhoneNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetTransactions", ReplyAction="http://tempuri.org/IPointsService/GetTransactionsResponse")]
        System.Data.DataTable GetTransactions(string Key, string Customer_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/ClaimReward", ReplyAction="http://tempuri.org/IPointsService/ClaimRewardResponse")]
        void ClaimReward(string Key, int Customer_ID, System.DateTime Date, int Reward_ID, int PointsUsed, string Address, string Address2, string Town, string ZipCode, bool Claimed, decimal AmountPaid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetClaims", ReplyAction="http://tempuri.org/IPointsService/GetClaimsResponse")]
        System.Data.DataTable GetClaims(string Key, string Customer_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetPointsBalance", ReplyAction="http://tempuri.org/IPointsService/GetPointsBalanceResponse")]
        int GetPointsBalance(string Key, int Customer_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetCustomers", ReplyAction="http://tempuri.org/IPointsService/GetCustomersResponse")]
        System.Data.DataTable GetCustomers(string Key, string SearchName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetCatalog", ReplyAction="http://tempuri.org/IPointsService/GetCatalogResponse")]
        System.Data.DataTable GetCatalog(string Key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/CustomerExists", ReplyAction="http://tempuri.org/IPointsService/CustomerExistsResponse")]
        bool CustomerExists(string Key, string PhoneNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/UpdateCustomer", ReplyAction="http://tempuri.org/IPointsService/UpdateCustomerResponse")]
        void UpdateCustomer(string Key, int Customer_ID, string FullName, string PhoneNumber, string Password, string VINs, string Email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/DeleteCustomer", ReplyAction="http://tempuri.org/IPointsService/DeleteCustomerResponse")]
        void DeleteCustomer(string Key, int Customer_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/EraseTable", ReplyAction="http://tempuri.org/IPointsService/EraseTableResponse")]
        void EraseTable(string Key, string AdminPassword, string TableName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetRewards", ReplyAction="http://tempuri.org/IPointsService/GetRewardsResponse")]
        System.Data.DataTable GetRewards(string Key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetRewardCost", ReplyAction="http://tempuri.org/IPointsService/GetRewardCostResponse")]
        int GetRewardCost(string Key, int Reward_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/SetClaimAsPaid", ReplyAction="http://tempuri.org/IPointsService/SetClaimAsPaidResponse")]
        void SetClaimAsPaid(string Key, int Claim_ID, bool Paid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetClaim", ReplyAction="http://tempuri.org/IPointsService/GetClaimResponse")]
        System.Data.DataTable GetClaim(string Key, int Claim_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetAllTransactions", ReplyAction="http://tempuri.org/IPointsService/GetAllTransactionsResponse")]
        System.Data.DataTable GetAllTransactions(string Key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetAllClaims", ReplyAction="http://tempuri.org/IPointsService/GetAllClaimsResponse")]
        System.Data.DataTable GetAllClaims(string Key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetRewardCashForPoints", ReplyAction="http://tempuri.org/IPointsService/GetRewardCashForPointsResponse")]
        decimal GetRewardCashForPoints(string Key, int Reward_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/RegisterReward", ReplyAction="http://tempuri.org/IPointsService/RegisterRewardResponse")]
        void RegisterReward(string Key, string Reward_Name, int Reward_Category_ID, string Reward_Description, int Cost, bool IsActive, decimal CashForPoints);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetReward", ReplyAction="http://tempuri.org/IPointsService/GetRewardResponse")]
        System.Data.DataTable GetReward(string Key, int Reward_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/DeleteReward", ReplyAction="http://tempuri.org/IPointsService/DeleteRewardResponse")]
        void DeleteReward(string Key, int Reward_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetAllRewardCategories", ReplyAction="http://tempuri.org/IPointsService/GetAllRewardCategoriesResponse")]
        System.Data.DataTable GetAllRewardCategories(string Key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/UpdateReward", ReplyAction="http://tempuri.org/IPointsService/UpdateRewardResponse")]
        void UpdateReward(string Key, int Reward_ID, string Reward_Name, int Reward_Category_ID, string Reward_Description, int Cost, bool IsActive, decimal CashForPoints);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetManagerPassword", ReplyAction="http://tempuri.org/IPointsService/GetManagerPasswordResponse")]
        string GetManagerPassword(string Key, int Setting_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/UpdateManagerPassword", ReplyAction="http://tempuri.org/IPointsService/UpdateManagerPasswordResponse")]
        void UpdateManagerPassword(string Key, int Setting_ID, string NewPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetFilesPath", ReplyAction="http://tempuri.org/IPointsService/GetFilesPathResponse")]
        string GetFilesPath(string Key, int Setting_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/UpdateFilesPath", ReplyAction="http://tempuri.org/IPointsService/UpdateFilesPathResponse")]
        void UpdateFilesPath(string Key, int Setting_ID, string NewPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/ROExists", ReplyAction="http://tempuri.org/IPointsService/ROExistsResponse")]
        bool ROExists(string Key, string RO_Number);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPointsService/GetCustomerID", ReplyAction="http://tempuri.org/IPointsService/GetCustomerIDResponse")]
        int GetCustomerID(string Key, string Phone_Number);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPointsServiceChannel : Points_Client.PointsServiceReference.IPointsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PointsServiceClient : System.ServiceModel.ClientBase<Points_Client.PointsServiceReference.IPointsService>, Points_Client.PointsServiceReference.IPointsService {
        
        public PointsServiceClient() {
        }
        
        public PointsServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PointsServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PointsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PointsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public Points_Client.PointsServiceReference.CompositeType GetDataUsingDataContract(Points_Client.PointsServiceReference.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Data.DataTable GetBalanceReport(string Key, bool IncludeOnlyWithBalance) {
            return base.Channel.GetBalanceReport(Key, IncludeOnlyWithBalance);
        }
        
        public void RegisterPoints(string Key, int Customer_ID, System.DateTime Transaction_Date, string RO_Number, string VIN, int Points, bool ManagerGift, string GiftReason) {
            base.Channel.RegisterPoints(Key, Customer_ID, Transaction_Date, RO_Number, VIN, Points, ManagerGift, GiftReason);
        }
        
        public void RegisterCustomer(string Key, string FullName, string PhoneNumber, string Password, string VINs, string Email) {
            base.Channel.RegisterCustomer(Key, FullName, PhoneNumber, Password, VINs, Email);
        }
        
        public System.Data.DataTable GetCustomer(string Key, string FullName, string PhoneNumber) {
            return base.Channel.GetCustomer(Key, FullName, PhoneNumber);
        }
        
        public System.Data.DataTable GetTransactions(string Key, string Customer_ID) {
            return base.Channel.GetTransactions(Key, Customer_ID);
        }
        
        public void ClaimReward(string Key, int Customer_ID, System.DateTime Date, int Reward_ID, int PointsUsed, string Address, string Address2, string Town, string ZipCode, bool Claimed, decimal AmountPaid) {
            base.Channel.ClaimReward(Key, Customer_ID, Date, Reward_ID, PointsUsed, Address, Address2, Town, ZipCode, Claimed, AmountPaid);
        }
        
        public System.Data.DataTable GetClaims(string Key, string Customer_ID) {
            return base.Channel.GetClaims(Key, Customer_ID);
        }
        
        public int GetPointsBalance(string Key, int Customer_ID) {
            return base.Channel.GetPointsBalance(Key, Customer_ID);
        }
        
        public System.Data.DataTable GetCustomers(string Key, string SearchName) {
            return base.Channel.GetCustomers(Key, SearchName);
        }
        
        public System.Data.DataTable GetCatalog(string Key) {
            return base.Channel.GetCatalog(Key);
        }
        
        public bool CustomerExists(string Key, string PhoneNumber) {
            return base.Channel.CustomerExists(Key, PhoneNumber);
        }
        
        public void UpdateCustomer(string Key, int Customer_ID, string FullName, string PhoneNumber, string Password, string VINs, string Email) {
            base.Channel.UpdateCustomer(Key, Customer_ID, FullName, PhoneNumber, Password, VINs, Email);
        }
        
        public void DeleteCustomer(string Key, int Customer_ID) {
            base.Channel.DeleteCustomer(Key, Customer_ID);
        }
        
        public void EraseTable(string Key, string AdminPassword, string TableName) {
            base.Channel.EraseTable(Key, AdminPassword, TableName);
        }
        
        public System.Data.DataTable GetRewards(string Key) {
            return base.Channel.GetRewards(Key);
        }
        
        public int GetRewardCost(string Key, int Reward_ID) {
            return base.Channel.GetRewardCost(Key, Reward_ID);
        }
        
        public void SetClaimAsPaid(string Key, int Claim_ID, bool Paid) {
            base.Channel.SetClaimAsPaid(Key, Claim_ID, Paid);
        }
        
        public System.Data.DataTable GetClaim(string Key, int Claim_ID) {
            return base.Channel.GetClaim(Key, Claim_ID);
        }
        
        public System.Data.DataTable GetAllTransactions(string Key) {
            return base.Channel.GetAllTransactions(Key);
        }
        
        public System.Data.DataTable GetAllClaims(string Key) {
            return base.Channel.GetAllClaims(Key);
        }
        
        public decimal GetRewardCashForPoints(string Key, int Reward_ID) {
            return base.Channel.GetRewardCashForPoints(Key, Reward_ID);
        }
        
        public void RegisterReward(string Key, string Reward_Name, int Reward_Category_ID, string Reward_Description, int Cost, bool IsActive, decimal CashForPoints) {
            base.Channel.RegisterReward(Key, Reward_Name, Reward_Category_ID, Reward_Description, Cost, IsActive, CashForPoints);
        }
        
        public System.Data.DataTable GetReward(string Key, int Reward_ID) {
            return base.Channel.GetReward(Key, Reward_ID);
        }
        
        public void DeleteReward(string Key, int Reward_ID) {
            base.Channel.DeleteReward(Key, Reward_ID);
        }
        
        public System.Data.DataTable GetAllRewardCategories(string Key) {
            return base.Channel.GetAllRewardCategories(Key);
        }
        
        public void UpdateReward(string Key, int Reward_ID, string Reward_Name, int Reward_Category_ID, string Reward_Description, int Cost, bool IsActive, decimal CashForPoints) {
            base.Channel.UpdateReward(Key, Reward_ID, Reward_Name, Reward_Category_ID, Reward_Description, Cost, IsActive, CashForPoints);
        }
        
        public string GetManagerPassword(string Key, int Setting_ID) {
            return base.Channel.GetManagerPassword(Key, Setting_ID);
        }
        
        public void UpdateManagerPassword(string Key, int Setting_ID, string NewPassword) {
            base.Channel.UpdateManagerPassword(Key, Setting_ID, NewPassword);
        }
        
        public string GetFilesPath(string Key, int Setting_ID) {
            return base.Channel.GetFilesPath(Key, Setting_ID);
        }
        
        public void UpdateFilesPath(string Key, int Setting_ID, string NewPath) {
            base.Channel.UpdateFilesPath(Key, Setting_ID, NewPath);
        }
        
        public bool ROExists(string Key, string RO_Number) {
            return base.Channel.ROExists(Key, RO_Number);
        }
        
        public int GetCustomerID(string Key, string Phone_Number) {
            return base.Channel.GetCustomerID(Key, Phone_Number);
        }
    }
}
