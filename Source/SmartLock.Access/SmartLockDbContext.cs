using Microsoft.EntityFrameworkCore;
using SmartLock.Model;

namespace SmartLock.Access
{
    public class SmartLockDbContext : DbContext
    {
        public SmartLockDbContext(DbContextOptions<SmartLockDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.Property(e => e.EventTime).HasColumnType("datetime");

                entity.HasOne(d => d.Door)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.DoorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AuditLogs__DoorI__2D27B809");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AuditLogs__Offic__2C3393D0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AuditLogs__UserI__2E1BDC42");

                entity.HasIndex(d => d.UserId)
                    .HasDatabaseName("IX_AuditLog_UserId");
            });

            modelBuilder.Entity<Door>(entity =>
            {
                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Doors)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Doors__OfficeId__29572725");
            });

            modelBuilder.Entity<DoorRoleMapping>(entity =>
            {
                entity.HasOne(d => d.Door)
                    .WithMany(p => p.DoorRoleMappings)
                    .HasForeignKey(d => d.DoorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DoorRoleM__DoorI__30F848ED");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.DoorRoleMappings)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DoorRoleM__RoleI__31EC6D26");
            });

            modelBuilder.Entity<UserOfficeRoleMapping>(entity =>
            {
                entity.ToTable("UserOfficeRoleMapping");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.UserOfficeRoleMappings)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserOffic__DoorI__34C8D9D1");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserOfficeRoleMappings)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserOffic__RoleI__35BCFE0A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserOfficeRoleMappings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserOffic__UserI__36B12243");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasIndex(x => x.UserName)
                    .HasDatabaseName("IX_User_UserName")
                    .IsUnique();
            });

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Firstname = "Sheldon", LastName = "Cooper", EmailId = "sheldoncooper@gmail.com", IsAdmin = 1, EmployeeId = "1000", UserName = "sheldon", Password = "c2hlbGRvbg==" },
                new User { Id = 2, Firstname = "Adia", LastName = "Bugg", EmailId = "adia@gmail.com", IsAdmin = 0, EmployeeId = "1001", UserName = "adia", Password = "YWRpYQ==" },
                new User { Id = 3, Firstname = "Olive", LastName = "yew", EmailId = "olive@gmail.com", IsAdmin = 0, EmployeeId = "1002", UserName = "olive", Password = "b2xpdmU=" },
                new User { Id = 4, Firstname = "Peg", LastName = "Legge", EmailId = "peg@gmail.com", IsAdmin = 0, EmployeeId = "1003", UserName = "peg", Password = "cGVn" },
                new User { Id = 5, Firstname = "Allie", LastName = "Grater", EmailId = "allie@gmail.com", IsAdmin = 0, EmployeeId = "1004", UserName = "allie", Password = "YWxsaWU=" }
                );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "HeadStaff" },
                new Role { Id = 2, Name = "Travel" },
                new Role { Id = 3, Name = "Engineer" },
                new Role { Id = 4, Name = "Finance" }
                );

            modelBuilder.Entity<Office>().HasData(
                new Office { Id = 1, Name = "Clay", Address = "Amsterdam" },
                new Office { Id = 2, Name = "Booking.com", Address = "Amsterdam" }
                );

            modelBuilder.Entity<Door>().HasData(
                new Door { Id = 1, Name = "FrontDoor", Type = "Main", OfficeId = 1 },
                new Door { Id = 2, Name = "BackDoor", Type = "Back", OfficeId = 1 },
                new Door { Id = 3, Name = "StoreRoom", Type = "Store", OfficeId = 1 },
                new Door { Id = 4, Name = "FrontDoor", Type = "Main", OfficeId = 2 }
                );

            modelBuilder.Entity<UserOfficeRoleMapping>().HasData(
                new UserOfficeRoleMapping { Id = 1, UserId = 1, RoleId = 1, OfficeId = 1 },
                new UserOfficeRoleMapping { Id = 2, UserId = 2, RoleId = 2, OfficeId = 1 },
                new UserOfficeRoleMapping { Id = 3, UserId = 3, RoleId = 3, OfficeId = 1 },
                new UserOfficeRoleMapping { Id = 4, UserId = 4, RoleId = 4, OfficeId = 1 },
                new UserOfficeRoleMapping { Id = 5, UserId = 5, RoleId = 1, OfficeId = 2 }
                );

            modelBuilder.Entity<DoorRoleMapping>().HasData(
                new DoorRoleMapping { Id = 1, DoorId = 1, RoleId = 1 },
                new DoorRoleMapping { Id = 2, DoorId = 1, RoleId = 2 },
                new DoorRoleMapping { Id = 3, DoorId = 1, RoleId = 3 },
                new DoorRoleMapping { Id = 4, DoorId = 1, RoleId = 4 },
                new DoorRoleMapping { Id = 5, DoorId = 2, RoleId = 1 },
                new DoorRoleMapping { Id = 6, DoorId = 2, RoleId = 3 },
                new DoorRoleMapping { Id = 7, DoorId = 3, RoleId = 1 },
                new DoorRoleMapping { Id = 8, DoorId = 4, RoleId = 1 },
                new DoorRoleMapping { Id = 9, DoorId = 4, RoleId = 1 }
                );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<UserOfficeRoleMapping> UserOfficeRoleMappings { get; set; }
        public DbSet<DoorRoleMapping> DoorRoleMappings { get; set; }
    }
}
