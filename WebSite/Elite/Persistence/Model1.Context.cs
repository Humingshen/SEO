﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hms.Web.Persistence
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebSiteEntities : DbContext
    {
        public WebSiteEntities()
            : base("name=WebSiteEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<T_Admin> T_Admin { get; set; }
        public virtual DbSet<T_Article> T_Article { get; set; }
        public virtual DbSet<T_Comment> T_Comment { get; set; }
        public virtual DbSet<T_Elements> T_Elements { get; set; }
        public virtual DbSet<T_Pages> T_Pages { get; set; }
    }
}