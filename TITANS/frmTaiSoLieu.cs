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
namespace TITANS
{
    public partial class frmTaiSoLieu : UserControl
    {
        public frmTaiSoLieu()
        {
            InitializeComponent();
        }
        static Dictionary<string, string> listLink = new Dictionary<string, string>();
        public void DowsLoad_File_Pdf()
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

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                Thread.Sleep(1000);
                //var a = Convert.ToInt32(js.ExecuteScript("var lenght = document.getElementsByClassName('hrefViewDetail').length; return lenght;"));
                for (int i = 1; i <= 2; i++)
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

                //Thread.Sleep(3000); //3s
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
                PdfDocument doc = new PdfDocument();
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
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DowsLoad_File_Pdf();
        }
    }
}
