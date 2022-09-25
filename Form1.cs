using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using xNet;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Ocsp;
using RegFB.Helper;

namespace RegFB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            Thread tMain = new Thread(() =>
            {
                for (int i = 0; i < (Int32)numericUpDown3.Value; i++)
                {
                    Thread t4 = new Thread(() =>
                    {
                        Instagram(i);
                        Thread.Sleep(1000);
                    });
                    t4.IsBackground = true;
                    t4.Start();
                    onoffDcom();
                    Thread.Sleep(1000);
                }
            });
            tMain.Start();
        }

        void Instagram(int i)
        {
        login:
            string accPath = "acc.txt";

            randomtxt rtxt = new randomtxt();

            var options = new ChromeOptions();
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            string useragent = rtxt.ramdomFiletxt("ua.txt");
            options.AddArgument("--user-agent=" + useragent);
            options.AddArgument("--mute-audio");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--window-size=530,700");
            options.AddArgument("--disable-notifications");


            List<string> listAccounts = System.IO.File.ReadAllLines("txt//" + accPath).ToList();
            string[] acc = listAccounts[0].Split('|');
            string Username = acc[0];
            string Pass = acc[1];
            string twoFA = acc[2];
            listAccounts.RemoveAt(0);
            System.IO.File.WriteAllLines("txt//" + accPath, listAccounts.ToArray());

            var driver = new ChromeDriver(chromeDriverService, options);

            switch (i)
            {
                case 0:
                    driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
                    break;
                case 1:
                    driver.Manage().Window.Position = new System.Drawing.Point(530, 0);
                    break;
                case 2:
                    driver.Manage().Window.Position = new System.Drawing.Point(1060, 0);
                    break;

            }

            driver.Navigate().GoToUrl("https://www.facebook.com/");

            try
            {
                try
                {
                    driver.FindElementByXPath("/html/body/div[1]/div/div[2]/div[1]/div/div[2]/div/div[3]/form/div[4]/div[1]/div/div/input").SendKeys(Username);

                    driver.FindElementByXPath("/html/body/div[1]/div/div[2]/div[1]/div/div[2]/div/div[3]/form/div[4]/div[3]/div/div/div/div[1]/div/input").SendKeys(Pass);

                    driver.FindElementByXPath("/html/body/div[1]/div/div[2]/div[1]/div/div[2]/div/div[3]/form/div[5]/div[1]/button").Click();

                    Thread.Sleep(3000);

                    driver.FindElementByXPath("/html/body/div[1]/div/div[3]/form/div/article/section/div/section[2]/div[2]/div/input").SendKeys(rtxt.GetOTP(twoFA));

                    driver.FindElementByXPath("/html/body/div[1]/div/div[3]/form/div/article/div[1]/table/tbody/tr/td/button").Click();
                    Thread.Sleep(4000);
                }
                catch (Exception)
                { }


                #region ktcp
                if (driver.Url.Contains("checkpoint"))
                {
                    try
                    {
                        if (driver.FindElementById("checkpoint_title").Text.Contains("Xem lại") || driver.FindElementById("checkpoint_title").Text.Contains("Review recent"))
                        {
                            goto login;
                        }
                        
                    }
                    catch
                    {
                        Console.WriteLine("Loi checkpoint title Xem Lai Hoat dong dang nhap");
                        
                    }

                    try
                    {

                        if (driver.FindElementById("checkpoint_title").Text.Contains("Nhập mã"))
                        {
                            goto login;
                        }
                        
                    }
                    catch
                    {
                        Console.WriteLine("Loi checkpoint title NhapMa");
                       
                    }

                    try
                    {
                        if (driver.FindElementById("captcha") != null)
                        {
                            goto login;
                        }
                       
                    }
                    catch
                    {
                        Console.WriteLine("Loi checkpoint Capcha");
                        
                    }

                    try
                    {
                        Console.WriteLine(driver.Url);
                        if (driver.Url.Contains("confirmemail"))
                        {
                            goto login;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Loi confirm email!!!");
                       
                    }
                }
                else if (driver.Url.Contains("confirmemail"))
                {
                    
                }

                #endregion
                //try
                //{
                //    var ycCP = driver.FindElementByXPath("/html/body/div[1]/div/div[4]/div/div[1]/div/article/form/div/button");
                //    if (ycCP != null)
                //    {
                //        driver.Close();
                //        driver.Quit();
                //        goto login;
                //    }
                //}
                //catch (Exception)
                //{ }

                try
                {
                    driver.FindElementByXPath("/html/body/div[1]/div/div[3]/form/div/article/div[1]/table/tbody/tr/td/button").Click();
                    Thread.Sleep(2000);

                    driver.FindElementByXPath("/html/body/div[1]/div/div[3]/form/div/article/div[1]/table/tbody/tr/td/button").Click();
                    Thread.Sleep(2000);
                    // la toi trinh duyet
                    driver.FindElementByXPath("/html/body/div[1]/div/div[3]/form/div/article/div[2]/table/tbody/tr/td[2]/button").Click();
                    Thread.Sleep(2000);

                    driver.FindElementByXPath("/html/body/div[1]/div/div[3]/form/div/article/div[1]/table/tbody/tr/td/button").Click();
                    Thread.Sleep(7000);
                }
                catch (Exception)
                {

                }
                driver.Navigate().GoToUrl("https://www.instagram.com/");
                Thread.Sleep(2000);
                try
                {
                    //login
                    driver.FindElementByXPath("/html/body/div[1]/section/main/article/div/div/div/div[2]/button").Click(); //login
                    Thread.Sleep(1000);
                    
                    
                    //try //cp
                    //{
                    //    var getapp = driver.FindElementByXPath("/html/body/div[1]/section/main/article/div/div/div/div[2]/button/div");
                    //    if (getapp != null)
                    //    {
                    //        driver.Close();
                    //        driver.Quit();
                    //        goto login;
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //}


                }
                catch (Exception)
                {

                }
                //login2
                try
                {
                    driver.FindElementByXPath("/html/body/div[1]/section/main/article/div/div/div/div[3]/button[1]").Click();
                    Thread.Sleep(2000);
                }
                catch (Exception)
                {
                }

                try
                {
                    //ontinue
                    driver.FindElementByXPath("/html/body/div[1]/section/main/article/div/div/div/form/div[1]/div[1]/button/span[2]").Click();
                    Thread.Sleep(6000);
                }
                catch (Exception)
                {
                }

                try
                {
                    //tiep tuc
                    driver.FindElementByXPath("/html/body/div[1]/div/div[2]/div/div[1]/div/form/div[3]/button[1]").Click();
                    Thread.Sleep(5000);
                }
                catch (Exception)
                {
                }


                try
                {
                    //user name
                    string ho = rtxt.ramdomFiletxt("ho.txt");
                    string ten = rtxt.ramdomFiletxt("ten.txt");
                    string ngaysinh = rtxt.ramdomFiletxt("ngaysinh.txt");
                    string thang = rtxt.ramdomFiletxt("thang.txt");
                    string nam = new Random().Next(91, 99).ToString();

                    string userIns = ho + "." + ten + ngaysinh + thang + nam;
                    driver.FindElementByXPath("/html/body/div[1]/div/div/div[3]/form/div[2]/div/label/input").SendKeys(userIns);

                    string passIns = "cloneregphone_" + rtxt.RandomNuber();
                    driver.FindElementByXPath("/html/body/div[1]/div/div/div[3]/form/div[3]/div/label/input").SendKeys(passIns);


                    //next
                    driver.FindElementByXPath("/html/body/div[1]/div/div/div[3]/form/div[4]/div/button").Click();

                    rtxt.saveFile(userIns, passIns, "AccInstagram.txt");

                    Thread.Sleep(15000);

                }
                catch (Exception)
                {

                }

                try
                {
                    //terms
                    Console.WriteLine("terms");
                    driver.FindElementByXPath("/html/body/div[1]/section/main/div/div/div[2]/button").Click();
                    Thread.Sleep(10000);

                }
                catch (Exception)
                {
                }


                try
                {
                    //k tải app
                    driver.FindElementByXPath("/html/body/div/div/div[2]/a[2]").Click();
                    Thread.Sleep(2000);

                }
                catch (Exception)
                {
                }

                try
                {

                    // x màn hinh
                    driver.FindElementByXPath("/html/body/div[4]/div/div/div/div[3]/button[2]").SendKeys(OpenQA.Selenium.Keys.Cancel);
                    Thread.Sleep(2000);
                    driver.FindElementByXPath("/html/body/div[4]/div/div/div/div[3]/button[2]").SendKeys(OpenQA.Selenium.Keys.Enter);
                    Thread.Sleep(2000);
                    ///html/body/div[4]/div/div/div/div[3]/button[2]
                }
                catch (Exception)
                {


                }
                string avatarpath = rtxt.rFileinFoder("dataIMG");

                //up avatar
                if (ckbAVA.Checked)
                {
                    try
                    {
                        //ca nhan
                        driver.FindElementByXPath("/html/body/div[1]/section/nav[2]/div/div/div[2]/div/div/div[5]/a").Click();
                        Thread.Sleep(2000);
                        //----------------


                        driver.FindElementByXPath("/html/body/div[1]/section/main/div/header/div/div/div/div/form/input").SendKeys(rtxt.rFileinFoder("dataIMG"));
                        Thread.Sleep(9000);



                        //--------------
                        //save
                        driver.FindElementByXPath("/html/body/div[1]/section/div[1]/header/div/div[2]/button").Click();
                        Thread.Sleep(9000);

                        try
                        {
                            //post luon
                            driver.FindElementByXPath("/html/body/div[4]/div/div/div/div[2]/button[1]").Click();
                            Thread.Sleep(9000);
                        }
                        catch (Exception)
                        { }

                    }
                    catch (Exception)
                    {
                    }
                }
               
                try
                {
                    driver.FindElementByXPath("/html/body/div[4]/div/div/div/div[3]/button[2]").Click();
                    Thread.Sleep(2000);
                    // x màn hinh
                    driver.FindElementByXPath("/html/body/div[4]/div/div/div/div[3]/button[2]").SendKeys(OpenQA.Selenium.Keys.Cancel);
                    Thread.Sleep(2000);
                    driver.FindElementByXPath("/html/body/div[4]/div/div/div/div[3]/button[2]").SendKeys(OpenQA.Selenium.Keys.Enter);
                    Thread.Sleep(2000);
                    ///html/body/div[4]/div/div/div/div[3]/button[2]
                }
                catch (Exception)
                {


                }

                
               
                //up post
                for (int up = 0; up < (Int32)numericUpDown1.Value; up++)
                {
                    //chon ảnh

                    try //up load anh
                    {
                        //
                        #region
                        try
                        {
                            //click vao new pót
                            driver.FindElementByXPath("/html/body/div[1]/section/nav[2]/div/div/div[2]/div/div/div[3]").Click();

                            Thread.Sleep(1000);
                        }
                        catch (Exception)
                        { }


                        try //click send ảnh
                        {
                            driver.FindElementByXPath("/html/body/div[1]/section/nav[2]/div/div/form/input").SendKeys(avatarpath);

                        }
                        catch (Exception)
                        { }

                        try //send cand
                        {
                            
                            SendKeys.SendWait("{ESC}");
                            Thread.Sleep(1000);
                            //send enter

                        }
                        catch (Exception)
                        { }
                        #endregion

                        Thread.Sleep(3000);
                        //

                        //next
                        driver.FindElementByXPath("/html/body/div[1]/section/div[1]/header/div/div[2]/button").Click();
                        Thread.Sleep(2000);

                        //stt
                        driver.FindElementByXPath("/html/body/div[1]/section/div[2]/section[1]/div[1]/textarea").SendKeys(rtxt.ramdomFiletxt("stt.txt"));
                        Thread.Sleep(1000);

                        //share
                        driver.FindElementByXPath("/html/body/div[1]/section/div[1]/header/div/div[2]/button").Click();
                        Thread.Sleep(13000);
                    }
                    catch (Exception)
                    {

                    }
                }



                ////folow
                //for (int fo = 1; fo < (Int32)numericUpDown2.Value + 1; fo++)
                //{
                //    //tym
                //    driver.Navigate().GoToUrl("https://www.instagram.com/accounts/activity/");
                //    Thread.Sleep(3000);
                //    try
                //    {
                //        //folow
                //        driver.FindElementByXPath("/html/body/div[1]/section/main/div[3]/div/div/div["+fo+"]/div[3]/button").Click();
                //         Thread.Sleep(3000);
                //    }
                //    catch (Exception)
                //    { }

                //}

                //flow all
                if (checkBox1.Checked)
                {
                    try
                    {
                        driver.Navigate().GoToUrl("https://www.instagram.com/accounts/activity/");
                        Thread.Sleep(3000);
                        //scroll
                        driver.ExecuteScript("window.scrollTo(0,document.body.scrollHeight);");

                        driver.ExecuteScript("var x=document.getElementsByClassName('sqdOP  L3NKy   y3zKF     ');for(var i=0;i<x.length;i++){if (x[i].innerHTML=='Follow'){x[i].click();}};");
                        Thread.Sleep(5000);
                    }
                    catch (Exception)
                    { }
                }
            }
            catch
            {
                driver.Close();
                driver.Quit();
                goto login;
            }
            driver.Close();
            driver.Quit();
            goto login;

        }
        public static void resetDcom(string profileName)
        {
            Process process = new Process();
            process.StartInfo.FileName = "rasdial.exe";
            process.StartInfo.Arguments = "\"" + profileName + "\" /disconnect";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            process.WaitForExit();
            Thread.Sleep(1000);
        }

        public static void startDcom(string profileName)
        {
            Thread.Sleep(1000);
            Process process = new Process();
            process.StartInfo.FileName = "rasdial.exe";
            process.StartInfo.Arguments = "\"" + profileName + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            process.WaitForExit();
            Thread.Sleep(1500);
        }
        //C:\Program Files\D-com 3G\D-com 3G.exe"
        void onoffDcom()
        {
            resetDcom(textBox6.Text);
            Thread.Sleep(1000);
            startDcom(textBox6.Text);
            Thread.Sleep(3500);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\Ngo Sy Tuan Anh\Documents\My Code\Tool Khach Hang\Tool_Instagram\bin\Debug\net5.0-windows\txt\acc.txt");
        }





        //////////////



    }
}
