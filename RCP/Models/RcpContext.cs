namespace RCP.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RcpContext : DbContext
    {
        public RcpContext()
            : base("name=RcpCon")
        {
        }

        public virtual DbSet<Wejscia> Wejscia { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wejscia>()
                .Property(e => e.Data)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Wejscia>()
                .Property(e => e.Czas)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Wejscia>()
                .Property(e => e.Nazwa)
                .IsUnicode(false);

            modelBuilder.Entity<Wejscia>()
                .Property(e => e.Unikalny)
                .IsUnicode(false);

            modelBuilder.Entity<Wejscia>()
                .Property(e => e.Biuro)
                .IsUnicode(false);

            modelBuilder.Entity<Wejscia>()
                .Property(e => e.Post)
                .IsUnicode(false);

            modelBuilder.Entity<Wejscia>()
                .Property(e => e.Karta)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<RCP.Models.Zmiany> Zmiany { get; set; }

        public System.Data.Entity.DbSet<RCP.Models.PlanPracy> PlanPracy { get; set; }

        public System.Data.Entity.DbSet<RCP.Models.Pracownicy> Pracownicy { get; set; }

        public System.Data.Entity.DbSet<RCP.Models.Przypisania> Przypisania { get; set; }
        public System.Data.Entity.DbSet<RCP.Models.CalendarJSON> Kalendarz { get; set; }
    }
}
