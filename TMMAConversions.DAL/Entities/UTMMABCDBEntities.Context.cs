﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TMMAConversions.DAL.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UTMMABCDBEntities : DbContext
    {
        public UTMMABCDBEntities()
            : base("name=UTMMABCDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE> USR_TMMA_ASSIGN_MATERIAL_TO_ROUTING_FILE { get; set; }
        public virtual DbSet<USR_TMMA_BOM_FILE> USR_TMMA_BOM_FILE { get; set; }
        public virtual DbSet<USR_TMMA_FILE_STATUS> USR_TMMA_FILE_STATUS { get; set; }
        public virtual DbSet<USR_TMMA_INSPECTION_PLAN_FILE> USR_TMMA_INSPECTION_PLAN_FILE { get; set; }
        public virtual DbSet<USR_TMMA_MASTER_MESSAGE> USR_TMMA_MASTER_MESSAGE { get; set; }
        public virtual DbSet<USR_TMMA_MASTER_PERMISSION> USR_TMMA_MASTER_PERMISSION { get; set; }
        public virtual DbSet<USR_TMMA_MASTER_ROLE> USR_TMMA_MASTER_ROLE { get; set; }
        public virtual DbSet<USR_TMMA_PACKAGING_INSTRUCTION_FILE> USR_TMMA_PACKAGING_INSTRUCTION_FILE { get; set; }
        public virtual DbSet<USR_TMMA_PRODUCTION_VERSION_FILE> USR_TMMA_PRODUCTION_VERSION_FILE { get; set; }
        public virtual DbSet<USR_TMMA_PRODUCTS> USR_TMMA_PRODUCTS { get; set; }
        public virtual DbSet<USR_TMMA_PRODUCTS_TYPE> USR_TMMA_PRODUCTS_TYPE { get; set; }
        public virtual DbSet<USR_TMMA_ROLE_PERMISSION> USR_TMMA_ROLE_PERMISSION { get; set; }
        public virtual DbSet<USR_TMMA_ROUTING_FILE> USR_TMMA_ROUTING_FILE { get; set; }
        public virtual DbSet<USR_TMMA_ROUTING_WITH_MATERIAL_FILE> USR_TMMA_ROUTING_WITH_MATERIAL_FILE { get; set; }
        public virtual DbSet<USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE> USR_TMMA_ROUTING_WITHOUT_MATERIAL_FILE { get; set; }
        public virtual DbSet<USR_TMMA_USER> USR_TMMA_USER { get; set; }
        public virtual DbSet<USR_TMMA_WORK_CENTER_FILE> USR_TMMA_WORK_CENTER_FILE { get; set; }
        public virtual DbSet<USR_TMMA_WORKCENTER_ROUTING_FILE> USR_TMMA_WORKCENTER_ROUTING_FILE { get; set; }
    }
}
