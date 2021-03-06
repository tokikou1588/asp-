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
    
    public partial class Orders
    {
        public Orders()
        {
            this.OrderBook = new HashSet<OrderBook>();
        }
    
        public string OrderId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string PostAddress { get; set; }
        public int state { get; set; }
    
            [JsonIgnore]
        public virtual ICollection<OrderBook> OrderBook { get; set; }
            [JsonIgnore]
        public virtual Users Users { get; set; }
    }
}
