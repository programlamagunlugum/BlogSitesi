namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fazla : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blogs", "BlogTitle", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Blogs", "BlogTitle", c => c.String(maxLength: 50));
        }
    }
}
