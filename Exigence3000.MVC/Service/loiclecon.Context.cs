﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exigence3000.MVC.Service
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class tities : DbContext
    {
        public tities()
            : base("name=tities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Exigence> Exigence { get; set; }
        public virtual DbSet<Jalon> Jalon { get; set; }
        public virtual DbSet<Projet> Projet { get; set; }
        public virtual DbSet<Responsable> Responsable { get; set; }
        public virtual DbSet<Tache> Tache { get; set; }
    }
}
