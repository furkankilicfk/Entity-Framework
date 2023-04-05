using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CodeFirst.Entity
{
    public class Context:DbContext       //Context sınıfı DbContext sınıfından kalıtım alarak veritabanı işlemlerini yapan sınıftır.
    {                                    //DbContext sınıfı içindeki değerleri context sınıfı miras alsın ki bunu form tarafında kullanalım
                                         //Kalıtım aldırmazsak proje sql'de oluşmuyor. Çünkü databasecreate komutunu context nesnesine ait değil db contexte ait
        public DbSet<Urunler> Urunlers { get; set; }  //Oluşturduğumuz Urunler tablosunu veri tabanında tanımlamak için Entity Framework'ün DbSet generic class'ını kullanarak entity frameworkümüzüe set ediyoruz ve
                                                      //onu artık Urunlers diye çağıracağımızı söylüyoruz

        public DbSet<Kategori> Kategoris { get; set; }

        /*public DbSet<Musteri> Musteris { get; set; } */       //add-migration CreateMusterisTable sadece migrationa yansıtmış oluruz. update-database veri tabanında görmek için
                                                                //ben veritabanından önce bütün işlemlerimi burada yapacağım. en son veritabanına aktaracağım

                                                                //add-migration DeleteMusterisTable   //tablo-entity sınıf sildik
    }
}
