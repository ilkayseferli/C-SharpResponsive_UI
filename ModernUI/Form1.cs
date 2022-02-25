using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModernUI
{
    public partial class Form1 : Form
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        public Form1()
        {
            //ilk degerler atanıyor//
            InitializeComponent();
            random = new Random();

            //child form görünülürlüğü kapat
            btnCloseChildForm.Visible = false;
            //child form görünülürlüğü kapat

            this.Text = String.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            //ilk degerler atanıyor//
        }

        //Özelleştirilmiş Windows Bar için.//
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd,int wMsg,int wParam,int lParam);
        //Özelleştirilmiş Windows Bar için.//
       
        private Color SelectThemeColor()
        {
            //colorListin içindeki veri sayısına göre rastgele bir değer alır.
            
            //ÖRN:colorList içinde 8 adet veri varsa random bize 0'dan 8'e kadar bir sayı verir o sayıda index'e atanır.
            int index = random.Next(ThemeColor.colorList.Count);
            //ÖRN:colorList içinde 8 adet veri varsa random bize 0'dan 8'e kadar bir sayı verir o sayıda index'e atanır.
            
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.colorList.Count);
            }
            
            //random.next'te index'e atanmış veriyi tempIndex'e atar
            tempIndex = index;
            //random.next'te index'e atanmış veriyi tempIndex'e atar
            
            //colorList'teki indis numarasına göre renk alırnır (index 3 geldiyse indis 0 dan başladıgı için listedeki 4. rengi alır)
            string color = ThemeColor.colorList[index];
            //colorList'teki indis numarasına göre renk alırnır (index 3 geldiyse indis 0 dan başladıgı için listedeki 4. rengi alır)

            //Bulunan rengi Döndürür
            return ColorTranslator.FromHtml(color);
            //Bulunan rengi Döndürür
        }
        private void ActivateButton(object btnSender)
        {
            //btnSender null değilse yani buton gönderildiyse if'e gir
            if (btnSender != null)
            {
            //btnSender null değilse yani buton gönderildiyse if'e gir
                if (currentButton != (Button)btnSender)
                {
                    //Bütün butonları varsayılan ilk haline döndürür//
                    DisableButton();
                    //Bütün butonları varsayılan ilk haline döndürür//

                    //SelectThemeColor() metodu listeden rastgele bir değer alır.
                    Color color = SelectThemeColor();
                    //SelectThemeColor() metodu listeden rastgele bir değer alır.

                    //parametre olarak verilen butonu current buttona atar. ÖRN: currentButton = btnProduct
                    currentButton = (Button)btnSender;
                    //parametre olarak verilen butonu current buttona atar. ÖRN: currentButton = btnProduct

                    //parametre olarak verilen butona arkaplan rengi verir. (Bu renk yukarıda SelectThemeColor() metodundan alınmıştı)
                    currentButton.BackColor = color;
                    //parametre olarak verilen butona arkaplan rengi verir. (Bu renk yukarıda SelectThemeColor() metodundan alınmıştı)

                    //parametre olarak verilen butona yazı rengi beyaz verir
                    currentButton.ForeColor = Color.White;
                    //parametre olarak verilen butona yazı rengi beyaz verir

                    //parametre olarak verilen butona font özellikleri verilir
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    //parametre olarak verilen butona font özellikleri verilir

                    //panelTitleBar'a arkaplan rengi veriliyor
                    panelTitleBar.BackColor = color;
                    //panelTitleBar'a arkaplan rengi veriliyor

                    //color ile verdiğimiz rengi double parametresi ile başka renkleri dönüştürür
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    //color ile verdiğimiz rengi double parametresi ile başka renkleri dönüştürür

                    //primaryColor'a color'daki rengi atar//
                    ThemeColor.PrimaryColor = color;
                    //primaryColor'a color'daki rengi atar//

                    //SecondaryColor'a color renginden double parametresi ile üretilen rengi atar.
                    ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    //SecondaryColor'a color renginden double parametresi ile üretilen rengi atar.

                    //Formun içindeki formu kapatmak için kullandığımız butonu gösterir(true)
                    btnCloseChildForm.Visible = true;
                    //Formun içindeki formu kapatmak için kullandığımız butonu gösterir(true)
                }
            }
        }
        private void DisableButton()
        {
            //Genel işlevi butonları varsayılan hale getirmek (ActivateButton metodunda bu method çağırıldıktan sonra ilgili butona (ActivateButton metodunda) renk font gibi atamalar yapılır.)

            //PanelMenu içindeki controlleri gezer//
            foreach (Control previousButton in panelMenu.Controls)
            {
            //PanelMenu içindeki controlleri gezer//

                //panelMenu içindeki butonları bulur//
                if (previousButton.GetType() == typeof(Button))//tipi buton ise if'e girer
                {
                //panelMenu içindeki butonları bulur//

                    //butonlara varsayılan değerlerini verir
                    previousButton.BackColor = Color.FromArgb(105, 105, 105);//Arkaplan rengi verir
                    previousButton.ForeColor = Color.Gainsboro;//Yazı rengi verir
                    previousButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));//Font özellikleri verilir
                    //butonlara varsayılan değerlerini verir
                }
            }
        }

        private void OpenChildForm(Form childForm,object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            //parametre olarak verilen butonu işler (renk font vesaire atamalarını yapar)
            ActivateButton(btnSender);
            //parametre olarak verilen butonu işler (renk font vesaire atamalarını yapar)


            activeForm = childForm;
            childForm.TopLevel = false;
            
            //childformun borderlarını kaldırır
            childForm.FormBorderStyle = FormBorderStyle.None;
            //childformun borderlarını kaldırır

            //child formu yayar (bulunduğu kontrolü sarar)
            childForm.Dock = DockStyle.Fill;
            //child formu yayar (bulunduğu kontrolü sarar)

            //bu formun panelDesktopPane controllerine childForm'u ekle
            this.panelDesktopPane.Controls.Add(childForm);
            //bu formun panelDesktopPane controllerine childForm'u ekle


            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormProduct(),sender);
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormProduct(), sender);
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormProduct(), sender);
        }

        private void btnReporting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormProduct(), sender);
        }

        private void btnNotification_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormProduct(), sender);
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormProduct(), sender);
        }

        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            //formun içinde'ki child form açıksa kapat
            if (activeForm!= null)
            {
                activeForm.Close();
            }
            //formun içinde'ki child form açıksa kapat

            //
            Reset();
        }

        private void Reset()
        {
            //Varsayılan değerlere döner (Default)

            //Butonları varsayılan haline getirir.
            DisableButton();
            //Butonları varsayılan haline getirir.

            lblTitle.Text = "HOME";

            //panellerin renkleri atanır.
            panelTitleBar.BackColor = Color.FromArgb(0,150,136);
            panelLogo.BackColor = Color.FromArgb(39, 39, 58);
            //panellerin renkleri atanır.


            currentButton = null;

            //form içinde ki child form'un görünülürlüğü kapatılır.
            btnCloseChildForm.Visible = false;
            //form içinde ki child form'un görünülürlüğü kapatılır.

            //Varsayılan değerlere döner (Default)
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            //Pencere normal konumdayken if'e gir
            if (WindowState==FormWindowState.Normal)
            {
                //Pencereyi kapla (maximize);
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                //Maximizedeyken tekrar tıklarsak normala döner
                this.WindowState=FormWindowState.Normal;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            //Pencereyi görev çubuğuna indirir
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
