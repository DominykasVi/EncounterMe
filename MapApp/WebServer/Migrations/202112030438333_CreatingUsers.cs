namespace WebServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Email = c.String(),
                        Hashpassword = c.Binary(nullable: false),
                        AccessLevel = c.Int(nullable: false),
                        LevelPoints = c.Int(nullable: false),
                        AchievementNum = c.Int(nullable: false),
                        FoundLocationNum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
