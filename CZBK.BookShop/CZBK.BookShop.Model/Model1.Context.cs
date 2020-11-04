﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CZBK.BookShop.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class book_shopEntities : DbContext
    {
        public book_shopEntities()
            : base("name=book_shopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<ActionGroup> ActionGroup { get; set; }
        public DbSet<ActionInfo> ActionInfo { get; set; }
        public DbSet<Articel_Words> Articel_Words { get; set; }
        public DbSet<BookComment> BookComment { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<CheckEmail> CheckEmail { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<keyWordsRank> keyWordsRank { get; set; }
        public DbSet<OrderBook> OrderBook { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<R_UserInfo_ActionInfo> R_UserInfo_ActionInfo { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<SearchDetails> SearchDetails { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<SysFun> SysFun { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserStates> UserStates { get; set; }
        public DbSet<VidoFile> VidoFile { get; set; }
    
        public virtual int CreateOrder(string orderId, Nullable<int> userId, string address, ObjectParameter totalMoney)
        {
            var orderIdParameter = orderId != null ?
                new ObjectParameter("orderId", orderId) :
                new ObjectParameter("orderId", typeof(string));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateOrder", orderIdParameter, userIdParameter, addressParameter, totalMoney);
        }
    }
}
