﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FPISummaryPortal.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FPI_Dev_DatabaseEntities1 : DbContext
    {
        public FPI_Dev_DatabaseEntities1()
            : base("name=FPI_Dev_DatabaseEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<HG_PDM> HG_PDM { get; set; }
        public virtual DbSet<iICE_Staging> iICE_Staging { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<xItemClaim> xItemClaims { get; set; }
        public virtual DbSet<xItemImagesDetail> xItemImagesDetails { get; set; }
        public virtual DbSet<xItemImagesHeader> xItemImagesHeaders { get; set; }
    
        public virtual int uspGetDatafromHG()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspGetDatafromHG");
        }
    
        public virtual int uspGetDatafromiICEStaging()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspGetDatafromiICEStaging");
        }
    }
}