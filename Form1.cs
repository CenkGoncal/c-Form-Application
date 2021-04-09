using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Configuration;
using System.IO;



namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        int size2, size = 0;
        int kasatop = 0;
        int total = 0;
        int bakiye = 500;
        public static bool station = true;
        public Form1()
        {
            InitializeComponent();
            button2.Hide();
            button3.Hide();
            label7.Hide();
            button4.Hide();
            button5.Hide();
            label9.Text = "bakiyeniz: " + bakiye.ToString();
          

        }
       

        public List<KeyValuePair<string, string>> kartDestesi()
        {
            List<KeyValuePair<string, string>> a = new List<KeyValuePair<string, string>>();

            string tur = "maca,kupa,karo,sinek";

            foreach (var index in tur.Split(',').ToArray())
            {
                for (var i = 1; i <= 13; i++)
                {
                    a.Add(new KeyValuePair<string, string>(index.ToString(), i.ToString()));
                }
            }


            return a;

        }
        //list karıstıtma fonkisyonu
        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }
        public List<KeyValuePair<string, string>> ShuffleDeste()
        {
            List<KeyValuePair<string, string>> b = kartDestesi();
            List<KeyValuePair<string, string>> c = ShuffleList(b);
            return c;
        }
        public static void bahisform()
        {

            bahis ba = new bahis();
            ba.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (station)
            {
                bahisform();
            }
            else
            {
                List<KeyValuePair<string, string>> d = ShuffleDeste();
                label8.Text = bahis.chip.ToString();
                kasatop = ConvertTen(Convert.ToInt32(d[2].Value));
                bakiye = bakiye - (bahis.chip / 2);
                label9.Text = "Kalan bakiyeniz: " + bakiye.ToString();
                for (int i = 0; i < 3; i++)
                {
                    if (i != 2)
                    {

                        Image img = Image.FromFile(@"D:\test\project\WindowsFormsApplication3\WindowsFormsApplication3\iskambil\" + d[i].Key + d[i].Value + ".jpg");
                        PictureBox pi = new PictureBox()
                        {
                            Image = img,
                            Size = new Size(img.Width, img.Height),
                            Location = new Point(size + 10, 0)


                        };
                        panel2.Controls.Add(pi);
                        size = pi.Right;
                        if (((Convert.ToInt32(d[i].Value) == 1) && (Convert.ToInt32(d[i + 1].Value) >= 10)) || ((Convert.ToInt32(d[i].Value) >= 10) && (Convert.ToInt32(d[i + 1].Value) == 1)))
                        {
                            label3.ForeColor = Color.Green;
                            label3.Text = "BlackJack Yaptınız";
                            kasa();
                            button2.Hide();
                            button3.Hide();
                            button5.Hide();
                            button4.Show();
                            MessageBox.Show("1");
                            bakiye = bakiye + ((Convert.ToInt32(label8.Text)*3)/2);

                        }
                        else
                        {
                            hesapla(Convert.ToInt32(d[i].Value));

                        }

                    }
                    else
                    {
                        kasatop = ConvertTen(Convert.ToInt32(d[i].Value));
                        Image img = Image.FromFile(@"D:\test\project\WindowsFormsApplication3\WindowsFormsApplication3\iskambil\" + d[i].Key + d[i].Value + ".jpg");
                        PictureBox pi = new PictureBox()
                        {
                            Image = img,
                            Size = new Size(img.Width, img.Height),
                            Location = new Point(size2, 0)


                        };
                        panel1.Controls.Add(pi);
                        size2 = pi.Right;
                        if (d[2].Value == "1")
                        {

                            DialogResult sigorta = MessageBox.Show("Sigorta yapmak istermisin", "Sigorta", MessageBoxButtons.YesNo);
                            //orginal bahisin yarısı kadar olabilir.sigorta parası sadece kurpiyerin blackjack yapması durumunda geçerlidir.
                            //kurpiyerin elinde blackjack yoksa sigorta için verilen bahis kaybedilmektedir.
                            //eğer kurpiyerin elinde sigorta bahisi 1'e 2 verir
                            if (sigorta==DialogResult.Yes)
                            {

                            }
                            else if (sigorta == DialogResult.No)
                            { 
                            
                            }
                        }
                        


                    }




                }
                //label3.Text = mesaj(Convert.ToInt32(d[i].Value));
                label2.Text = total.ToString();

                button2.Show();
                button3.Show();
                button5.Show();
                button4.Hide();
                button1.Enabled = false;


            }
        }
        private void NewGame()
        {
            panel1.Controls.Clear();
            panel2.Controls.Clear();
            label3.Text = "";
            label5.Text = "";
            label8.Text = "";
            label7.Text = "";
            button1.Enabled = true;
        }

        private int hesapla(int deger)
        {
            deger=ConvertTen(deger);
            if ((total > 10) && deger == 1)
            {
                total = total + 1;
                return total;
            }
            else if ((total < 10) && deger == 1)
            {
                total = total + 11;
                return total;
            }
            else
            {
                total = total + deger;
                sonuc(total);
                return total;
            }
        }
  
        private void sonuc(int total)
        {
            if (total > 21)
            {
                label3.ForeColor = Color.Red;
                label3.Text = "Elininiz 21 Geçti";
                button2.Hide();
                button3.Hide();
                button5.Hide();
                button4.Show();

            }


        }
        public string karsılastır(int kasa, int gamer)
        {
            if (kasa > 21)
            {
                bakiye = bakiye + bahis.chip;
                label9.Text = "Kalan bakiyeniz: " + bakiye.ToString();
                button2.Hide();
                button3.Hide();
                button5.Hide();
                button4.Show();
                return "oyuncu kazandı";

            }
            else if (kasa < gamer)
            {
                bakiye = bakiye + bahis.chip;
                label9.Text = "Kalan bakiyeniz: " + bakiye.ToString();
                button2.Hide();
                button3.Hide();
                button5.Hide();
                button4.Show();
                return "oyuncu kazandı";
            }
            else if (kasa == gamer)
            {
                button2.Hide();
                button3.Hide();
                button5.Hide();
                button4.Show();
                label9.Text = "Kalan bakiyeniz: " + (bakiye + (bahis.chip / 2)).ToString();
                return "kazanan yok";
            }

            else
            {
                button2.Hide();
                button3.Hide();
                button5.Hide();
                button4.Show();
                return "kasa kazandı";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            kasa();
        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (total <= 21)
            {
                button5.Hide();
                List<KeyValuePair<string, string>> f = ShuffleDeste();
                Image img = Image.FromFile(@"D:\test\project\WindowsFormsApplication3\WindowsFormsApplication3\iskambil\" + f[0].Key + f[0].Value + ".jpg");
                hesapla(Convert.ToInt32(f[0].Value));
                PictureBox pi = new PictureBox()
                {
                    Image = img,
                    Size = new Size(img.Width, img.Height),
                    Location = new Point(size + 10, 0)
                };
                panel2.Controls.Add(pi);

                label2.Text = total.ToString();
                size = pi.Right;
            }
            else
            {
                label3.ForeColor = Color.Red;
                label3.Text = "Eliniz 21 aştı";
                //button2.Enabled = false;
                //button3.Enabled = false;
                button2.Hide();
                button3.Hide();
                button5.Hide();
                button4.Show();

            }

        }
 
        private void button4_Click(object sender, EventArgs e)
        {
            label2.Text = "";//total için
            label3.Text = "";//el dagıtılan 21 asma mesajı
            label5.Text = "";//kasatop için
            label7.Text = "";//durumu sıfırlamak için
            label8.Text = "Sonuc";//sonuc değerini için
            button1.Enabled = true;
            button3.Enabled = true;
            button4.Hide();
            panel1.Controls.Clear();
            panel2.Controls.Clear();
            size = 0;
            size2 = 0;
            total = 0;
            kasatop = 0;
            station = true;
        }


        public void kasa()
        {
            List<KeyValuePair<string, string>> f = ShuffleDeste();
            
            int count = 0;
            while (kasatop < 16)
            {
                if ((kasatop == 1 && Convert.ToInt32(f[count].Value) == 10) && (kasatop == 10 && Convert.ToInt32(f[count].Value) == 1))
                {
                    MessageBox.Show("Hello");

                }
                else
                {
                    Image img = Image.FromFile(@"D:\test\project\WindowsFormsApplication3\WindowsFormsApplication3\iskambil\" + f[count].Key + f[count].Value + ".jpg");
                    PictureBox pi = new PictureBox()
                    {
                        Size = new Size(img.Width, img.Height),
                        Location = new Point(size2 + 10, 0),
                        Image = img

                    };
                    panel1.Controls.Add(pi);
                    kasatop = kasatop + ConvertTen(Convert.ToInt32(f[count].Value));
                   /* if (Convert.ToInt32(f[count].Value) > 10)
                    {
                        kasatop = kasatop + 10;
                    }
                    else
                    {
                        kasatop = kasatop + Convert.ToInt32(f[count].Value);
                    }*/
                    size2 = pi.Right;
                    count++;
                }
                button3.Enabled = false;
                label5.Text = kasatop.ToString();

                label7.Show();
                label7.Text = karsılastır(kasatop, total);
            }




        }
        public int ConvertTen(int value)
        {
            if (value > 10)
            {
                return value = 10;
            }
            else
            {
                return value;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            bakiye = bakiye - (bahis.chip/2);
            label9.Text = "Double'dan sonra\n Kalan Bakiyeniz: " + bakiye.ToString();
            label8.Text = (Convert.ToInt32(label8.Text)+(bahis.chip)).ToString();
        }

    }
}