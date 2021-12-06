namespace WebServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestingUsersTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        email = c.String(),
                        hashpassword = c.Binary(),
                        accessLevel = c.Int(nullable: false),
                        Filename = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
