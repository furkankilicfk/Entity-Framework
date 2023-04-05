namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KategoriAdToKategoriAdiInKategorisTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kategoris", "KategoriAdi", c => c.String());     //Rename ile migrationa kolon adı değiştirme komutu verdiğimizde bunu update-database yaptığımızda kolonun içindeki veriler de silinir çünkü önceki kolon silinir yerine yeni bir kolon gelir
            Sql("Update Kategoris set KategoriAdi=KategoriAd");         //eski isim kolonundaki verileri yeni isimdeki kolonda kaybetmemek için sql'de isim güncellemesi yaptık.
            DropColumn("dbo.Kategoris", "KategoriAd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kategoris", "KategoriAd", c => c.String());
            Sql("Update Kategoris set KategoriAd=KategoriAdi");
            DropColumn("dbo.Kategoris", "KategoriAdi");
        }
    }
}
