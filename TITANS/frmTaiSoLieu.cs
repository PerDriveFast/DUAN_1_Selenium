using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Pdf;
using FileInfo = System.IO.FileInfo;
using FileFormat = Spire.Pdf.FileFormat;
using Spire.Pdf.Texts;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;


namespace TITANS
{
    public partial class frmTaiSoLieu : UserControl
    {
        public static List<GiaoDich> cacGiaoDich = new List<GiaoDich>();

        public noiDung XuLyNoiDung(string content = @"1 BPC 0 0 60 468.000 60 468.000")
        {
            var temp = content.Split(' ');
            if (temp[0] == "Tổng")
            {
                return new noiDung()
                {
                    STT = "Selenium By ",
                    MaCK = temp[0],
                    TongKLGDMua = temp[1],
                    TongGTGDMua = temp[2],
                    TongKLGDBan = temp[3],
                    TongGTGDBan = temp[4],
                    TongKLGDTuDoanh = temp[5],
                    TongGTGDTuDoanh = temp[6],
                };
            }
            else
            {

                return new noiDung()
                {
                    STT = temp[0],
                    MaCK = temp[1],
                    TongKLGDMua = temp[2],
                    TongGTGDMua = temp[3],
                    TongKLGDBan = temp[4],
                    TongGTGDBan = temp[5],
                    TongKLGDTuDoanh = temp[6],
                    TongGTGDTuDoanh = temp[7],
                };
            }
        }

        public GiaoDich XuLyPDF(string filePath = @"D:\pdf\result2.pdf")
        {
            var src = $"{filePath}";
            var pdfDocument = new iText.Kernel.Pdf.PdfDocument(new PdfReader(src));
            string text_InPDF = "";
            var lastpage = pdfDocument.GetNumberOfPages();
            for (int i = 1; i <= lastpage; ++i)
            {
                var page = pdfDocument.GetPage(i);
                var strategy = new LocationTextExtractionStrategy();

                string text = iText.Kernel.Pdf.Canvas.Parser.PdfTextExtractor.GetTextFromPage(page, strategy);
                text = text.Replace("Evaluation Warning : The document was created with Spire.PDF for .NET.", "");
                text = text.Replace("Bản quyền thuộc Sở Giao dịch Chứng khoán Hà Nội.", "\n");
                text = text.Replace("1.Tổng KLGD Tự doanh = Tổng KLGD mua Tự doanh + Tổng KLGD bán Tự doanh", "");
                text = text.Replace("2.Tổng GTGD Tự doanh = Tổng GTGD mua Tự doanh + Tổng GTGD bán Tự doanh", "");
                text = text.Replace("Đơn vị: đồng", "");
                text = text.Replace("STT Mã CK Tổng KLGD mua Tự doanh Tổng GTGD mua Tự doanh Tổng KLGD bán Tự doanh Tổng GTGD bán Tự doanh Tổng KLGD Tự doanh Tổng GTGD Tự doanh\n", "");
                text = text.Replace("*Ghi chú:", "");
                if (i == 1)
                {
                    text = text.Substring(160, text.Length - 161);
                }
                if (i == lastpage)
                {
                    text = text.Remove(text.Length - 3, 3);
                }
                text_InPDF += text;
            }
            pdfDocument.Close();
            var contentGiaoDich = text_InPDF.Split('\n').ToList();
            var name = contentGiaoDich[0];
            contentGiaoDich.RemoveRange(0, 4);

            var dsNoiDung = new List<noiDung>();
            for (int i = 0; i < contentGiaoDich.Count - 2; i++)
            {
                dsNoiDung.Add(XuLyNoiDung(contentGiaoDich[i]));
            }

            return new GiaoDich()
            {
                name = name,
                content = dsNoiDung,
            };
        }

        public frmTaiSoLieu()
        {
            InitializeComponent();
        }
        static Dictionary<string, string> listLink = new Dictionary<string, string>();

        public void DowsLoad_File_Pdf()
        {
            try
            {
                var options = new EdgeOptions();
                options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

                var driver = new EdgeDriver(options);


                string folder = @"\test";


                var CurrentDirectory = Directory.GetCurrentDirectory();
                var downloadDirectory = $"{CurrentDirectory}" + @"\test";
                string Pathfile = $"{downloadDirectory}" + @"\test.txt";

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                if (!System.IO.File.Exists(Pathfile))
                {
                    System.IO.File.Create(Pathfile);
                }

                // read file txt
                var logFile = System.IO.File.ReadAllLines(Pathfile);
                var xlsFileExist = new List<string>(logFile);

                try
                {
                    driver.Navigate().GoToUrl("https://www.hnx.vn/vi-vn/thong-tin-cong-bo-ny-hnx.html");
                    driver.Manage().Window.Maximize();

                    var el6 = driver.FindElement(By.XPath("//*[@id=\"txtTieuDeTin\"]"));
                    el6.SendKeys("kết quả giao dịch tự doanh");
                    var el7 = driver.FindElement(By.XPath("//*[@id=\"btn_searchL\"]"));
                    el7.Click();
                    Thread.Sleep(1000);

                    var select_show_more = driver.FindElement(By.XPath("//*[@id=\"divNumberRecordOnPage\"]"));
                    select_show_more.Click();

                    Thread.Sleep(1000);
                    var select = driver.FindElement(By.XPath("//*[@id=\"divNumberRecordOnPage\"]/option[5]"));
                    select.Click();


                    var page2 = driver.FindElement(By.XPath("//*[@id=\"d_number_of_page\"]/li[2]/span"));
                    page2.Click();
                    Thread.Sleep(1000);


                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                    //var a = Convert.ToInt32(js.ExecuteScript("var lenght = document.getElementsByClassName('hrefViewDetail').length; return lenght;"));
                    for (int i = 1; i <= 50; i++)
                    {
                        var _temp = (string)js.ExecuteScript($"return( document.querySelector(\"#_tableDatas > tbody > tr:nth-child({i}) > td.tdLeftAlign > a\").text);");
                        _temp = _temp.Remove(0, 4);
                        _temp = _temp.Replace('/', '-');

                        //  _temp = _temp.Replace(" ","");
                        var _name = _temp;
                        js.ExecuteScript($@"document.querySelector('#_tableDatas > tbody > tr:nth-child({i})').cells[3].children[0].click()");
                        Thread.Sleep(1900);

                        var _link = (string)js.ExecuteScript(@"return (document.querySelector('#divViewDetailArticles > div.divContentArticlesDetail > div.divLstFileAttach > p > a').href);");
                        //done
                        listLink.Add(_name, _link);

                    }

                    /*          Thread.Sleep(3000);
                              var page2 = driver.FindElement(By.XPath("//*[@id=\"d_number_of_page\"]/li[2]/span"));
                              page2.Click();*/



                }


                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    // exit chrome

                    driver.Quit();
                }


                // save file download url
                // save file name .txt
                FileInfo fileInfo = new FileInfo($@"test\test.txt");


                using (StreamWriter sw = fileInfo.AppendText())
                {
                    int a = 0;
                    Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
                    foreach (var link in listLink)
                    {

                        //Create a WebClient object

                        WebClient webClient = new WebClient();

                        //Download data from URL and save as memory stream

                        using (MemoryStream ms = new MemoryStream(webClient.DownloadData(link.Value)))

                        {

                            doc.LoadFromStream(ms);
                            //save file name
                            sw.WriteLineAsync($@"{a}.{link.Key}");
                        }
                        //Save to PDF file

                        doc.SaveToFile($@"file_pdf\result_{a}.pdf", FileFormat.PDF);


                        a++;
                    }

                    Thread.Sleep(1000);

                }
            }catch(Exception ex) { }
           
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DowsLoad_File_Pdf();
        }

        private void frmTaiSoLieu_Load(object sender, EventArgs e)
        {

        }

        private void btpdf_Click(object sender, EventArgs e)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;//Để nhập tiếng việt
            Console.OutputEncoding = System.Text.Encoding.Unicode;//Để xuất tiếng việt
            for (int i = 0; i < 49; i++)
            {
                cacGiaoDich.Add(XuLyPDF($@"C:\New folder (3)\pdf\result{i}.pdf"));
            }
                MessageBox.Show("Thanh Cong");
        }
        public void save_database()
        {

        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
