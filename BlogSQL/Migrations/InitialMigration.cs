using System;
using FluentMigrator;

namespace BlogSQL.Migrations
{
    [Migration(201007181900)]
    public class InitialMigration : Migration
    {   
        public override void Up()
        {
            Create.Table("Posts")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Hash").AsString()
                .WithColumn("Title").AsString()
                .WithColumn("Content").AsString()
                .WithColumn("Published").AsDateTime()
                .WithColumn("Created").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table("Posts");
        }
    }
}