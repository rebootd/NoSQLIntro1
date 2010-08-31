using System;
using FluentMigrator;

namespace BlogSQL.Migrations
{
    [Migration(201008311900)]
    public class TagsMigration : Migration
    {   
        public override void Up()
        {
            Create.Table("Tags")
                .WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("PostId").AsGuid()
                .WithColumn("Name").AsString();
        }

        public override void Down()
        {
            Delete.Table("Tags");
        }
    }
}