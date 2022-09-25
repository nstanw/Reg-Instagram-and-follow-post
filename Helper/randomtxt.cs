using OtpNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegFB.Helper
{
    class randomtxt
    {
        public string ramdomFiletxt(string filepath)
        {

            string[] lines = System.IO.File.ReadAllLines("txt//" + filepath); // đọc file cookie ra từng dòng "lines"

            // dem dong file text
            var seedingTxtCount = System.IO.File.ReadLines("txt//" + filepath).Count();
            Random random = new Random();

            int r = random.Next(0, seedingTxtCount);

            return lines[r];

        }

        public string rFileinFoder(string FoderPath)
        {
            var rand = new Random();
            var filess = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + FoderPath, "*.jpg");
            return filess[rand.Next(filess.Length)];
        }

        public string RandomNuber(int length = 5)
        {
            const string valid1 = "1234567890";

            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {

                res.Append(valid1[rnd.Next(valid1.Length)]);

            }
            return res.ToString();
        }

        public string GetOTP(string Key_2FA)
        {
            Key_2FA = Key_2FA.Replace(" ", "");
            var base32Bytes = OtpNet.Base32Encoding.ToBytes(Key_2FA);
            var totp = new Totp(base32Bytes);
            var twoFactorCode = totp.ComputeTotp();
            return twoFactorCode;
        }

        public void openDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                MessageBox.Show(fileName);
            }

        }

        //lưu file
        public void saveFile(string userIns, string passIns, string filepath)
        {
            string line;
            filepath = "txt//" + "AccInstagram.txt";
            StreamWriter cooki3 = new StreamWriter(filepath, true);
            {
                line = userIns + "|" + passIns;
                cooki3.WriteLine(line.ToString());
                cooki3.Close();

            }
        }


    }
}
