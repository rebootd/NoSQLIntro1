using System;
using FluentMigrator;

namespace BlogSQL.Migrations
{
    [Migration(201008311930)]
    public class TagsUpdate : Migration
    {   
        public override void Up()
        {
			Rename.Column("PostId").OnTable("Tags").To("Post_id");
        }

        public override void Down()
        {
			Rename.Column("Post_id").OnTable("Tags").To("PostId");
        }
    }
}