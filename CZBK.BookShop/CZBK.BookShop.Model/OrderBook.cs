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
    
    public partial class OrderBook
    {
        public int Id { get; set; }
        public string OrderID { get; set; }
        public int BookID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    
            [JsonIgnore]
        public virtual Books Books { get; set; }
            [JsonIgnore]
        public virtual Orders Orders { get; set; }
    }
}
