using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LambdaExpression
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NorthwindDataContext ctx = new NorthwindDataContext();

            var sonuc = ctx.Products.Join(          //products sınıfına join yapacağım,
                        ctx.Categories,             // bağlayacağım tablo
                        urun => urun.CategoryID,    //o zaman ilk başta yazdığın tabloya ait ilişkili alanı ver  //ilk tabloya ait değişken üretiyor urun
                        kat => kat.CategoryID,      //bağladığın tablonun ilişkili alanını ver. iki tabloyu bağladım, şimdi ise listeleme işlemi yapacağım
                        (u, k) => new               // ilk ve ikinci tablodaki kolonlara ulaşabilmem için iki tane çıktı üretmem lazım -- u ve k -- sırayla! -- öyle ki new diyerek bir anontype oluşturuyorum -sadece bu anlık bir tip c#ta tanımlı değil
                        {
                            u.ProductName,
                            u.UnitPrice,
                            u.UnitsInStock,
                            k.CategoryName,
                            u.SupplierID            //burası artık yeni bir tablo oldu.
                        }).Join(ctx.Suppliers,      //ilk tablomuz yukarısı, ikincii bir tabloyu ver.  supplierı verme amacım ise aşağıda kolona ulaşamamış olmak
                        urun => urun.SupplierID,    //yine ilk tablodaki ilişkili alanı ver. supplierID çıkmıyor. Çünkü artık yeni tablo oluştu ve sadece yukarıda verdiğimiz kolonları görüyor. o yüzden yukarıda supplierId'yi tanımlamamız gerek.
                        tedarikci => tedarikci.SupplierID,
                        (urn, tdr) => new
                        {
                            urn.ProductName,
                            urn.CategoryName,
                            urn.UnitPrice,
                            urn.UnitsInStock,
                            tdr.CompanyName
                        });
                                //En sondaki join işlemine bağlı kolonlar geçerli olur!!!
            dataGridView1.DataSource = sonuc;

            
        }
    }
}
