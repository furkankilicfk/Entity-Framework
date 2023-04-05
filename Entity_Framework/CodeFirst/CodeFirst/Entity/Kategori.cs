using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Entity
{
    public class Kategori
    {
        [Key]
        public int KategoriID { get; set; }
        public string KategoriAdi { get; set; }          //add-migration KategoriAdToKategoriAdiInKategorisTable(veri kaybetmeme) --- add-migration RenameKategoriAdToKategoriAdiInKategorisTable

       /* public string KategoriDetay { get; set; }  */     //add-migration DeleteKategoriDetayColumnInKategorisTable

        public ICollection<Urunler> Urunlers { get; set; }  //Tablo ilişkilendirmesi yaptık. Bunun anlamı: Kategorilerin içerisinde birden fazla ürün olabilir.
                                                            //BİR KATEGORİ BİRDEN FAZLA ÜRÜNDE YER ALABİLİR.
    }
}
