//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Solution1.Module.BusinessObjects.Database
{

    public partial class Users : XPLiteObject
    {
        int fUserID;
        [Key(true)]
        public int UserID
        {
            get { return fUserID; }
            set { SetPropertyValue<int>(nameof(UserID), ref fUserID, value); }
        }
        char fUserType;
        public char UserType
        {
            get { return fUserType; }
            set { SetPropertyValue<char>(nameof(UserType), ref fUserType, value); }
        }
        string fUserIdentity;
        [Size(50)]
        public string UserIdentity
        {
            get { return fUserIdentity; }
            set { SetPropertyValue<string>(nameof(UserIdentity), ref fUserIdentity, value); }
        }
        string fUserPassword;
        [Size(50)]
        public string UserPassword
        {
            get { return fUserPassword; }
            set { SetPropertyValue<string>(nameof(UserPassword), ref fUserPassword, value); }
        }
        [Association(@"CasesReferencesUsers")]
        public XPCollection<Cases> CasesCollection { get { return GetCollection<Cases>(nameof(CasesCollection)); } }
        [Association(@"CasesReferencesUsers1")]
        public XPCollection<Cases> CasesCollection1 { get { return GetCollection<Cases>(nameof(CasesCollection1)); } }
    }

}
