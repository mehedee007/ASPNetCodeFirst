namespace OctoberProjectCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationAdd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainees", "TraineeName", c => c.String(nullable: false));
            AlterColumn("dbo.Trainees", "TraineeContact", c => c.String(maxLength: 11));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainees", "TraineeContact", c => c.String());
            AlterColumn("dbo.Trainees", "TraineeName", c => c.String());
        }
    }
}
