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
    
    public partial class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
    
            [JsonIgnore]
        public virtual Books Books { get; set; }
            [JsonIgnore]
        public virtual Users Users { get; set; }
    }
}
