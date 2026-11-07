using Microsoft.EntityFrameworkCore;
using DigitalInvestigationAPI.Models;

namespace DigitalInvestigationAPI.Data
{
    public class DigitalInvestigationContext : DbContext
    {
        public DigitalInvestigationContext(
            DbContextOptions<DigitalInvestigationContext> options)
            : base(options)
        {
        }

        public DbSet<CaseClass> Cases { get; set; }

        public DbSet<SuspectClass> Suspects { get; set; }

        public DbSet<EvidenceClass> Evidence { get; set; }

        public DbSet<InvestigationClass> Investigations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Investigation relationships
            modelBuilder.Entity<InvestigationClass>()
                .HasOne(i => i.Case)
                .WithMany()
                .HasForeignKey(i => i.CaseID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InvestigationClass>()
                .HasOne(i => i.Suspect)
                .WithMany()
                .HasForeignKey(i => i.SuspectID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // INITIAL CASE
            // ==========================================

            modelBuilder.Entity<CaseClass>().HasData(
                new CaseClass
                {
                    CaseID = 1,
                    CaseName = "The Missing Prototype",
                    Description = "A technology prototype has disappeared from the research laboratory.",
                    Status = "OPEN"
                }
            );


            // ==========================================
            // INITIAL SUSPECTS
            // ==========================================

            modelBuilder.Entity<SuspectClass>().HasData(

                new SuspectClass
                {
                    SuspectID = 1,
                    SuspectName = "Alex Morgan",
                    Occupation = "Software Developer",
                    Description = "Alex developed the software used by the prototype and had access to the laboratory."
                },

                new SuspectClass
                {
                    SuspectID = 2,
                    SuspectName = "Jamie Smith",
                    Occupation = "Security Officer",
                    Description = "Jamie was responsible for security at the building on the night of the incident."
                },

                new SuspectClass
                {
                    SuspectID = 3,
                    SuspectName = "Taylor Williams",
                    Occupation = "Research Assistant",
                    Description = "Taylor worked with the research team and had access to the laboratory during working hours."
                }
            );


            // ==========================================
            // INITIAL EVIDENCE
            // ==========================================

            modelBuilder.Entity<EvidenceClass>().HasData(

                new EvidenceClass
                {
                    EvidenceID = 1,
                    Title = "Security Access Log",
                    Location = "Security Office",
                    Description = "Jamie Smith's access card was used to enter the research laboratory at 23:41."
                },

                new EvidenceClass
                {
                    EvidenceID = 2,
                    Title = "CCTV Report",
                    Location = "Research Laboratory",
                    Description = "CCTV footage shows a person entering the laboratory at approximately 23:43. The person's face cannot be clearly identified."
                },

                new EvidenceClass
                {
                    EvidenceID = 3,
                    Title = "Fingerprint Report",
                    Location = "Research Laboratory",
                    Description = "A partial fingerprint was found on the prototype storage cabinet. The fingerprint belongs to a person who regularly works in the laboratory."
                },

                new EvidenceClass
                {
                    EvidenceID = 4,
                    Title = "Email Message",
                    Location = "Archive Room",
                    Description = "An email sent shortly before the incident states: \"The prototype must be moved before tomorrow's demonstration.\""
                },

                new EvidenceClass
                {
                    EvidenceID = 5,
                    Title = "Photograph",
                    Location = "Research Laboratory",
                    Description = "A photograph taken after the incident shows that the prototype cabinet was open and the laboratory lights were switched off."
                }
            );
        }
    }
}