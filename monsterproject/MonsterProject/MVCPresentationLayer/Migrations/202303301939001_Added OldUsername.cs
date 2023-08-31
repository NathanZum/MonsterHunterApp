namespace MVCPresentationLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOldUsername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "OldUsername", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "OldUsername");
        }
    }
}
