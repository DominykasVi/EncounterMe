namespace WebServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingNewUsersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                 c => new
                 {
                    Id = c.Int(nullable: false, identity: true),
                    name = c.String(nullable: false),
                    email = c.String(nullable: false),
                    hashpassword = c.Binary(nullable: false)
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
