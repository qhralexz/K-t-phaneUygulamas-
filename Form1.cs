using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb; //Access bağlantı dosyaları

namespace Kütüphane_Performans__554
{
    public partial class Form1 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=kutuphane.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();

        public Form1()
        {
            InitializeComponent();
        }
        //DataGrid üzerinde kayıtları listeleme bölümü
        void listele()
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * from kutuphane", baglanti);
            adtr.Fill(ds, "kutuphane");
            dataGridView1.DataSource = ds.Tables["kutuphane"];
            adtr.Dispose();
            baglanti.Close();
        }
        //Kayıt ekleme bölümü
        private void button1_Click(object sender, EventArgs e)
        {
            resim.Text = pictureBox1.ImageLocation;
            if (s_no.Text != "" && tc.Text != "" && ad.Text != "" && soyad.Text != "" && telefon.Text != "" && u_tarihi.Text != "" && okul_no.Text != "" && sinif.Text != "" && adres.Text != "" && resim.Text != "")
            {
                komut.Connection = baglanti;
                komut.CommandText = "Insert Into kutuphane(s_no,tc,ad,soyad,telefon,u_tarihi,okul_no,sinif,adres,resim) Values ('" + s_no.Text + "','" + tc.Text + "','" + ad.Text + "','" + soyad.Text + "','" + telefon.Text + "','" + u_tarihi.Text + "','" + okul_no.Text + "','" + sinif.Text + "','" + adres.Text + "','" + resim.Text + "')";
                baglanti.Open();
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglanti.Close();
                MessageBox.Show("Kayıt Tamamlandı!");
                ds.Clear();
                listele();
            }
            else
            {
                MessageBox.Show("Boş alan geçmeyiniz!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }
        //Kayıt silme bölümü
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Delete from kutuphane where s_no=" + s_no.Text + "";
            komut.ExecuteNonQuery();
            komut.Dispose();
            baglanti.Close();
            ds.Clear();
            listele();
        }
        //Kayıt Arama Bölümü
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=kutuphane.accdb");
            adtr = new OleDbDataAdapter("Select * from kutuphane where ad like '" + ad.Text + "%'", baglanti);
            ds = new DataSet();
            baglanti.Open();
            adtr.Fill(ds, "kutuphane");
            dataGridView1.DataSource = ds.Tables["kutuphane"];
            baglanti.Close();

        }
        //Kayıt Güncelleme Bölümü
        private void button4_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText =" update kutuphane set tc='" + tc.Text + "', ad='" + ad.Text + "', soyad='" + soyad.Text + "', telefon='" + telefon.Text + "',u_tarihi='"+u_tarihi.Text+"',okul_no='"+okul_no.Text+"',sinif='"+sinif.Text+"',adres='"+adres.Text+"',resim='"+resim.Text+"' where s_no=" + s_no.Text + "";
            komut.ExecuteNonQuery();
            baglanti.Close();
            ds.Clear();
            listele();
        }
        //DataGrid'e Tıklayınca kayıtları Textbox'lara yazdırma bölümü
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            s_no.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tc.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            ad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            soyad.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            telefon.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            u_tarihi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            okul_no.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            sinif.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            adres.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[9].Value.ToString();

        }
        //Resim Ekleme
        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
