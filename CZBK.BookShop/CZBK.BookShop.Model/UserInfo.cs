//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CZBK.BookShop.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class UserInfo
    {
        public UserInfo()
        {
            this.R_UserInfo_ActionInfo = new HashSet<R_UserInfo_ActionInfo>();
            this.Role = new HashSet<Role>();
        }
    
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public System.DateTime RegTime { get; set; }
        public string Email { get; set; }
    
            [JsonIgnore]
        public virtual ICollection<R_UserInfo_ActionInfo> R_UserInfo_ActionInfo { get; set; }
            [JsonIgnore]
        public virtual ICollection<Role> Role { get; set; }
    }
}
