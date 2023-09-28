namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kargotest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KargoDetays",
                c => new
                    {
                        KargoDetayid = c.Int(nullable: false, identity: true),
                        Aciklama = c.String(maxLength: 300),
                        TakipKodu = c.String(maxLength: 10),
                        Personel = c.String(maxLength: 30),
                        Alici = c.String(maxLength: 30),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KargoDetayid);
            
            CreateTable(
                "dbo.KargoTakips",
                c => new
                    {
                        KargoTakipid = c.Int(nullable: false, identity: true),
                        TakipKodu = c.String(maxLength: 10),
                        Aciklama = c.String(maxLength: 100),
                        TarihZaman = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KargoTakipid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KargoTakips");
            DropTable("dbo.KargoDetays");
        }
    }
}
