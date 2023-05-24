using Huddle.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Huddle.Domain.Context
{
    public class HuddleContext : IdentityDbContext<User,Role,Guid>
    {
        public HuddleContext(DbContextOptions options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });
            modelBuilder.Entity<Role>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });
            modelBuilder.Entity<Consumer>().ToTable("Consumers");
            modelBuilder.Entity<BusinessOwner>().ToTable("businessOwners");
            modelBuilder.Entity<EventPlanner>().ToTable("EventPlanners");
            //if saveChanges() thrown an error then check this...
             modelBuilder.Entity<Event>()
                .HasMany(e => e.Reviews)
                .WithOne(r => r.Event)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BusinessOwner>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.businessOwner)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BusinessOwner>()
                 .HasMany(b => b.FollowedBusinessOwners)
                 .WithOne(f => f.BusinessOwner)
                 .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<EventPlanner>()
                .HasMany(e => e.FollowedEventPlanners)
                .WithOne(f => f.EventPlanner)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Event>()
                .HasMany(e => e.FollowedEvents)
                .WithOne(f => f.Event)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ConsumerActivity>()
                .HasNoKey();
            modelBuilder.Entity<ActivePlaceInGroup>()
                .HasNoKey();
            modelBuilder.Entity<Activity>()
                .HasMany(c => c.Consumers)
                .WithMany(a => a.Activities)
                .UsingEntity<ConsumerActivity>();
            
            modelBuilder.Entity<Group>()
                .HasMany(m => m.Consumers)
                .WithMany(c => c.Groups)
                .UsingEntity<GroupConsumer>();
         

        }

        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<BusinessOwner> BusinessOwners { get; set; }
        public DbSet<EventPlanner> EventPlanners { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<FollowedBusinessOwner> FollowedBusinessOwners { get; set; }
        public DbSet<FollowedEventPlanner> FollowedEventPlanners { get; set; }
        public DbSet<FollowedEvent> FollowedEvents { get; set; }
        public DbSet<ConsumerActivity> ConsumerActivities { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivePlaceInGroup> ActivePlacesInGroups { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupConsumer> GroupConsumers { get; set; }

    }
}
