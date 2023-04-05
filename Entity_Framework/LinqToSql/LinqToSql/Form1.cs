using LinqToSql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqToSql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NorthwindDataContext ctx = new NorthwindDataContext();  //oluşturduğumuz dbml nesnesi bize yazmış olduğumuz ismin sonuna datacontext ekleyerek bir context nesnesi tasarladı. işlemlerimizi bununla yapacağız. Facade işlemlerini yaptığımız özellikler

            //GÖSTERMEK İSTEDİĞİM KOLONLAR
            var sonuc = from urun in ctx.Products
                        join kat in ctx.Categories
                        on urun.CategoryID equals kat.CategoryID
                        //join tedarikci in ctx.Suppliers
                        //on urun.SupplierID equals tedarikci.SupplierID
                        //orderby urun.UnitPrice
                        select new
                        {
                            urun.ProductID, //kolon adı kullanmak istiyorsak eşittir metoduyla yazmalıyız - u.UnitPtice || sd.UnitPrice
                            urun.ProductName,
                            urun.UnitPrice,
                            urun.UnitsInStock,
                            kat.CategoryName,
                            //tedarikci.CompanyName
                        };


            dataGridView1.DataSource = ctx.Products;
            //Product p = new Product(); // like an entity

            cmbKategori.DisplayMember = "CategoryName";
            cmbKategori.ValueMember = "CategoryID";
            cmbKategori.DataSource = ctx.Categories;


            cmbTedarikci.DisplayMember = "CompanyName";
            cmbTedarikci.ValueMember = "SupplierID";
            cmbTedarikci.DataSource = ctx.Suppliers;
            
            
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Products prd = new Products();
            prd.ProductName = txtUrunAd.Text;
            prd.UnitPrice = numFiyat.Value;
            prd.UnitsInStock = Convert.ToInt16(numStok.Value);
            prd.CategoryID = (int)cmbKategori.SelectedValue;
            prd.SupplierID = (int)cmbTedarikci.SelectedValue;

            NorthwindDataContext ctx = new NorthwindDataContext();

            ctx.Products.InsertOnSubmit(prd);   //içinde product tipinde parametre istiyor
            ctx.SubmitChanges();  //eklemenin silmenin güncellemenin sql'de çalıştırılmasını sağlayan metod. contextte olan değişiklikleri veritabanına aktarma

            dataGridView1.DataSource = ctx.Products;

            //SCOPE IDENTITY eklemiş old kaydın id'sini geri döndürme

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow == null)  
                return; 

            int urunId = (int)dataGridView1.CurrentRow.Cells["ProductId"].Value;

            NorthwindDataContext ctx = new NorthwindDataContext();

            //ctx.Products.DeleteOnSubmit() //product tipinde parametre istiyor

            Product p = ctx.Products.SingleOrDefault(urun=>urun.ProductID == urunId); //öyle ki (lambda expression)dedikten sonra urun product nesnesine ait bir değişken oldu. sadece burada tanımlı. suppliers.sod urun desem yine supliers adında bir örnek olmuş olurdu.
            //SingleOrDefault metoduyla id'si benim yakalamış olduğum id'ye eşit olan veriyi tutuyor. bunu fs p isminde product tipinde bir değişkene atadım.

            ctx.Products.DeleteOnSubmit(p);
            ctx.SubmitChanges();

            dataGridView1.DataSource = ctx.Products;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;  //seçili satırı row değişkenine atıyorum. Aşağıda seçili satırları ilgili alana aktarıyorum
            txtUrunAd.Text = row.Cells["ProductName"].Value.ToString();
            numFiyat.Value = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
            numStok.Value = Convert.ToDecimal(row.Cells["UnitsInStock"].Value);
            cmbKategori.SelectedValue = row.Cells["CategoryID"].Value;
            cmbTedarikci.SelectedValue = row.Cells["SupplierID"].Value;
            txtUrunAd.Tag = row.Cells["ProductID"].Value; //güncelleme işleminde bize seçili ürünün id'si de lazım.


        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            NorthwindDataContext ctx = new NorthwindDataContext();

            int id = (int)txtUrunAd.Tag;

            Products p = ctx.Products.SingleOrDefault(x => x.ProductID == id);  //contextin product s sınıfından seç sod metodu ile, x öyle ki x'in productID'si id olanı getir(benim seçtiğim row) dedim ve bunu product tipinde bir p değişkenine atadım

            p.ProductName = txtUrunAd.Text; //inden gelir
            p.UnitPrice = numFiyat.Value;
            p.UnitsInStock = Convert.ToInt16(numStok.Value);
            p.CategoryID = (int)cmbKategori.SelectedValue;
            p.SupplierID = (int)cmbTedarikci.SelectedValue;

            ctx.SubmitChanges();

            dataGridView1.DataSource = ctx.Products;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            NorthwindDataContext ctx = new NorthwindDataContext();

            dataGridView1.DataSource = ctx.Products.Where(x => x.ProductName.Contains(txtAra.Text)); //x'in prodcutname'i txtAra nın textini içeriyorsa getir. where komutu
        }

    
    }
}
//private void button1_Click(object sender, EventArgs e)
//{
//    NorthwindDataContext ctx = new NorthwindDataContext();

//    var sonuc = from u in ctx.Products
//                join sd in ctx.Order_Details
//                on u.ProductID equals sd.ProductID
//                join satis in ctx.Orders
//                on sd.OrderID equals satis.OrderID
//                join musteri in ctx.Customers
//                on satis.CustomerID equals musteri.CustomerID
//                group sd by u.ProductName into grup    //satış detay tablosuna göre gruplama yapmak istiyorum ama productname kolonuna göre yapsın
//                                                       //toplam satış adetlerini ve toplam satış fiyatlarını satış detay tablosunda getirtebilirim ancak.
//                                                       //o yüzden satış detay tablosuna göre gruplama yapmam lazım. ama istiyorum ki ben ürün adına göre gruplama yapayım --KEY
//                                                       //çayı 30 kere yazacağına çayı bir kere yazsın 30 desin sonra toplam satış..
//                                                       //grouplama işlemi yaptığım için ortaya yeni bir sonuç çıkıyor, ona bir isim veriyorum. into grup --- u'ya falan ulaşamam artık

//                select new
//                {
//                    grup.Key, //productname
//                    SatisAdet = grup.Count(),
//                    ToplamSatis = grup.Sum(total => total.UnitPrice * total.Quantity) //herhangi bir fonksiyon içinde lambda expresion kullanarak oluşturulan değişken gruplama yaptığınız tabloya ait bir değişken olur

//                };
//}