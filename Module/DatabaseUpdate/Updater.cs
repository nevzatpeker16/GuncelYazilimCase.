using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using Solution1.Module.BusinessObjects.Database;
/* Ne hikmetse Users i bulamıyor , fakat connection string değişti */
namespace Solution1.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}
            Users customerUser = ObjectSpace.FindObject<Users>(new BinaryOperator("UserType", "M"));
            if(customerUser == null) {
                customerUser = ObjectSpace.CreateObject<Users>();
                customerUser.UserIdentity = "MusteriDeneme";
                customerUser.UserPassword = "Deneme";
                
            }
            PermissionPolicyRole createCustomerRole = CreateCustomerRole();
            customerUser.UserType = 'M';

            Users userAdmin = ObjectSpace.FindObject<Users>(new BinaryOperator("UserType", "Y"));
            if(userAdmin == null) {
                userAdmin = ObjectSpace.CreateObject<Users>();
                userAdmin.UserIdentity = "Admin";
                // Set a password if the standard authentication type is used
                userAdmin.UserPassword ="Admin";
            }
			// If a role with the Administrators name doesn't exist in the database, create this role
            PermissionPolicyRole adminRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator());
            if(adminRole == null) {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = "Yönetici";
                adminRole.AddObjectPermission<Users>(SecurityOperations.FullAccess, "[Oid] = CurrentUserId()",
                    SecurityPermissionState.Allow);
                adminRole.AddObjectPermission<Cases>(SecurityOperations.FullAccess, "[Oid] = CurrentUserId()",
                    SecurityPermissionState.Allow);
                adminRole.AddObjectPermission<Employee>(SecurityOperations.FullAccess, "[Oid] = CurrentUserId()",
                    SecurityPermissionState.Allow);

            }
            adminRole.IsAdministrative = true;
            userAdmin.UserType = 'Y';
             //This line persists created object(s).

            Users emplooyeUser = ObjectSpace.FindObject<Users>(new BinaryOperator("UserType", "C"));
            if (emplooyeUser == null)
            {
                emplooyeUser = ObjectSpace.CreateObject<Users>();
                emplooyeUser.UserIdentity = "CalisanDeneme";
                emplooyeUser.UserPassword = "CalisanDeneme";

            }

            emplooyeUser.UserType = 'C';
            PermissionPolicyRole create = CreateEmplooyeRole();
            ObjectSpace.CommitChanges();
        }


        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }


        private PermissionPolicyRole CreateCustomerRole()
        {
            PermissionPolicyRole customerRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator());
            if (customerRole == null)
            {
                customerRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                customerRole.Name = "Müşteri";
                customerRole.AddObjectPermission<Cases>(SecurityOperations.Create, "[Oid] = CurrentUserId()",
                    SecurityPermissionState.Allow);
                customerRole.AddObjectPermission<Cases> (SecurityOperations.Navigate, "[Oid] = CurrentUserId()",
                    SecurityPermissionState.Allow);
                customerRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                customerRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                customerRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                customerRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                customerRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                customerRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                customerRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);

                /*Caseleri database de ekleme çıkarma silme .*/

            }

            return customerRole;
        }

        private PermissionPolicyRole CreateEmplooyeRole()
        {
            PermissionPolicyRole emplooyeRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("UserType", "C"));
/*C Çalışana denk düşecek şekilde düşünelim.*/
            if (emplooyeRole == null)
            {
                emplooyeRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                emplooyeRole.Name = "Çalışan";
                emplooyeRole.AddObjectPermission<Cases>(SecurityOperations.ReadWriteAccess, "[Oid] = CurrentUserId()",
                    SecurityPermissionState.Allow);
                emplooyeRole.AddObjectPermission<Cases>(SecurityOperations.Navigate, "[Oid] = CurrentUserId()",
                    SecurityPermissionState.Allow);
                emplooyeRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                emplooyeRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                emplooyeRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                emplooyeRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                emplooyeRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                emplooyeRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                emplooyeRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);

            }

            return emplooyeRole;

        }
    }

}
