﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Uchet_vedom
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class uchvedEntities : DbContext
    {
        public uchvedEntities()
            : base("name=uchvedEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<klass> klass { get; set; }
        public virtual DbSet<posech> posech { get; set; }
        public virtual DbSet<predmet> predmet { get; set; }
        public virtual DbSet<roditeli> roditeli { get; set; }
        public virtual DbSet<roli> roli { get; set; }
        public virtual DbSet<uchenikk> uchenikk { get; set; }
        public virtual DbSet<vedom> vedom { get; set; }
    }
}
