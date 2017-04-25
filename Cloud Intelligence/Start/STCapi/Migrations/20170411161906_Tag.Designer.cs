using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using STCapi.DB;

namespace STCapi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170411161906_Tag")]
    partial class Tag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("STCapi.DB.Links", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Link");

                    b.Property<bool>("Read");

                    b.Property<string>("Tag");

                    b.HasKey("Id");

                    b.ToTable("Links");
                });
        }
    }
}
