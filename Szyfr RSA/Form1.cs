using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;

namespace Szyfr_RSA
{

    public partial class Form1 : Form
    {
        public BigInteger n = 0, ee = 0, d = 0, fi = 0;
        int blcklen;
        public static BigInteger inverse_modulo(BigInteger a, BigInteger n)
        {
            BigInteger u, w, x, z, q;

            u = 1; w = a;
            x = 0; z = n;
            while (w != 0)
            {
                if (w < z)
                {
                    q = u; u = x; x = q;
                    q = w; w = z; z = q;
                }
                q = w / z;
                u -= q * x;
                w -= q * z;
            }
            if (z == 1)
            {
                if (x < 0) x += n;
                return x;
            }
            return -1;
        }

        public static BigInteger nwd(BigInteger a, BigInteger b)
        {
            BigInteger c;                    
            while (b != 0)             
            {
                c = a % b;           
                a = b;                
                b = c;               
            }
            return a;                 
        }

        BigInteger ASCIICode(string str, int countInBlock)
        {
            List<byte[]> lista = new List<byte[]>();
            BigInteger x = 0;
            char[] buffer = new char[1];
            char[] txt = str.ToCharArray();
            for (int i = 0; i < str.Length; i++)
            {
                buffer[0] = txt[i];
                lista.Add(System.Text.Encoding.ASCII.GetBytes(buffer)); 
            }
            for (int i = lista.Count -1; i >= 0; i--)
            {
                    x += new BigInteger(lista[i]) * BigInteger.Pow(new BigInteger(256), lista.Count -1 - i);
            }
            return x;
        }
        BigInteger ASCIICode(char[] txt, int countInBlock)
        {
            List<byte[]> lista = new List<byte[]>();
            BigInteger x = 0;
            char[] buffer = new char[1];
            for (int i = 0; i < txt.Length; i++)
            {
                buffer[0] = txt[i];
                lista.Add(System.Text.Encoding.ASCII.GetBytes(buffer));
            }
            for (int i = lista.Count - 1; i >= 0; i--)
            {
                    x += new BigInteger(lista[i]) * BigInteger.Pow(new BigInteger(256), lista.Count - 1 -i);
            }
            return x;
        }

        string ASCIIDecode(BigInteger x, int length)
        {
            List<byte[]> lista = new List<byte[]>();
            //int length = Convert.ToInt32(Math.Floor(BigInteger.Log(x, 256.0f)));
            byte[] buffer = new byte[1];
            string txt = "";
            BigInteger div, power;
            for (int i = length; i >= 0; i--)
            {
                power = BigInteger.Pow(new BigInteger(256), i);
                div = x / power;
                buffer = div.ToByteArray();
                x = x % power;
                if(div != 0)
                txt = txt + System.Text.Encoding.ASCII.GetString(buffer);
                
            }
            return txt;
        }


        // private string separator = ";&^&^**#&$&*-(@&+(*>*?>#:+>>?>:/:+;:}{][#$)@_$@:$#(`+@#&#(@$+*!)!#&-*($@$(*#/)(@*%*-&#(@IąĄFe%@(@#*%";
        private bool szyfruj = true;
        public Form1()
        {
            InitializeComponent();
        }

        private string szyfrowanie(string tekst, int block_length)
        {
            int key_length = n.ToByteArray().Length;
            int k = 0;
            while (block_length >= key_length) block_length -= 1;
            numericUpDown1.Value = block_length;
            while (tekst.Length % block_length != 0)
            {
                tekst = tekst + "_";
            }
            char[] txt = tekst.ToCharArray();
            

            string szyfr;
            szyfr = "";
            for(int i = 0; i < txt.LongLength ; i+=block_length)
            {
                char[] buffer = new char[block_length];
                for (int j = 0; j < block_length; j++)
                {
                    buffer[j] = txt[k];
                    k++;
                }
                BigInteger tmp = ASCIICode(buffer, block_length);
                tmp = BigInteger.ModPow(tmp, ee, n);
                szyfr = szyfr + tmp.ToString("X") + "+";
            }
            return szyfr;
        }

        private string szyfrowanie(byte[] bytes, int block_length)
        {
            int key_length = n.ToByteArray().Length;
            int k = 0;
            List<byte> bytestmp = new List<byte>();
            for (long i = 0; i < bytes.LongLength; i++)
            {
                bytestmp.Add(bytes[i]);
            }
            while (block_length >= key_length) block_length -= 1;
            numericUpDown1.Value = block_length;
            while (bytestmp.Count % block_length != 0)
            {
                bytestmp.Add(Convert.ToByte(0));
            }
            string szyfr;
            szyfr = "";
            for (int i = 0; i < bytestmp.Count; i += block_length)
            {
                byte[] buffer = new byte[block_length];
                for (int j = 0; j < block_length; j++)
                {
                    buffer[j] = bytestmp[k];
                    k++;
                }
                BigInteger tmp = new BigInteger(buffer);
                tmp = BigInteger.ModPow(tmp, ee, n);
                szyfr = szyfr + tmp.ToString("X") + "+";
            }
            return szyfr;
        }

        private string deszyfrowanie(string szyfr, int block_length)
        {
            BigInteger tmp, decod;
            string[] bloki = szyfr.Split('+');
            string tekst = "";
            for (int i = 0; i < bloki.Length; i++)
            {
                if (bloki[i] != "" && bloki[i] != "\n" && bloki[i] != " ")
                {
                    tmp = BigInteger.Parse(bloki[i], System.Globalization.NumberStyles.HexNumber);
                    decod = BigInteger.ModPow(tmp, d, n);
                    
                   // Buffer.BlockCopy(bytes, i, buffer, 0, block_length); //DOKONCZYC
                    tekst = tekst + ASCIIDecode(decod, block_length);
                }
            }
            
            return tekst;
        }

        /*private byte[] deszyfrowaniePLIK(string szyfr, int block_length)
        {
            BigInteger tmp, decod;
            string[] bloki = szyfr.Split('+');
            List<byte[block_length]> bytestmp = new List<byte[block_length];
            for (int i = 0; i < bloki.Length; i++)
            {
                if (bloki[i] != "" && bloki[i] != "\n" && bloki[i] != " ")
                {
                    tmp = BigInteger.Parse(bloki[i], System.Globalization.NumberStyles.HexNumber);
                    decod = BigInteger.ModPow(tmp, d, n);

                    // Buffer.BlockCopy(bytes, i, buffer, 0, block_length); //DOKONCZYC
                    bytestmp = 
                }
            }

            return tekst;
        }*/


        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                saveFileDialog4.ShowDialog();
            }
            else
            {
                if (!checkBox2.Checked || !szyfruj)
                {
                    blcklen = Convert.ToInt32(numericUpDown1.Value);
                    if (textBoxEnter.Text != "")
                    {
                        if (szyfruj && n != 0 && ee != 0)
                        {
                            richTextBox2.Text = szyfrowanie(textBoxEnter.Text, blcklen);
                        }
                        else if (!szyfruj && n != 0 && d != 0)
                        {
                            richTextBox2.Text = deszyfrowanie(textBoxEnter.Text, blcklen);
                        }
                    }
                }
                else if (checkBox2.Checked && szyfruj && n != 0 && ee != 0)
                    saveFileDialog3.ShowDialog();
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            textBoxN2.ReadOnly = true;
            textBoxD.ReadOnly = true;
            textBoxE.ReadOnly = false;
            textBoxN1.ReadOnly = false;
            buttonGen.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
            label1.Text = "Wiadomość";
            label8.Text = "Szyfrogram";
            szyfruj = true;
            checkBox4.Enabled = true;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            textBoxN2.ReadOnly = false;
            textBoxD.ReadOnly = false;
            textBoxE.ReadOnly = true;
            textBoxN1.ReadOnly = true;
            buttonGen.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;
            label8.Text = "Wiadomość";
            label1.Text = "Szyfrogram";
            szyfruj = false;
            checkBox4.Checked = false;
            checkBox4.Enabled = false;
        }

        private void textBoxN1_TextChanged(object sender, EventArgs e)
        {
            textBoxN2.Text = textBoxN1.Text;
            n = BigInteger.Parse(textBoxN1.Text);
        }

        private void textBoxN2_TextChanged(object sender, EventArgs e)
        {
            textBoxN1.Text = textBoxN2.Text;
            n = BigInteger.Parse(textBoxN1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonGen_Click(object sender, EventArgs e)
        {
            
            generator_klucza form2 = new generator_klucza(this);
            form2.Owner = this;
            form2.ShowDialog();
            textBoxD.Text = d.ToString();
            textBoxE.Text = ee.ToString();
            textBoxN1.Text = n.ToString();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            StreamWriter write = new StreamWriter(saveFileDialog1.FileName);
            string x = n.ToString("X");
            string y = ee.ToString("X");
           // byte[] bytex = Convert.FromBase64String(x);
            //File.WriteAllBytes(saveFileDialog1.FileName, bytex);
            write.Write(x);
            write.Write("+");
            write.Write(y);
            //write.Write(System.Text.Encoding.Unicode.GetString(y));*/
            write.Close();
            saveFileDialog2.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
           StreamReader read = new StreamReader(openFileDialog1.FileName);
           // byte[] key = File.ReadAllBytes(openFileDialog1.FileName);
           // string []keys = StringExtension.Split(key, separator);
           // string string_key = Convert.ToBase64String(key);
            //byte[] keys = Convert.FromBase64String(string_key);
           string key = read.ReadToEnd();
           string[] keys = key.Split('+');
           n = BigInteger.Parse(keys[0], System.Globalization.NumberStyles.HexNumber);
           ee = BigInteger.Parse(keys[1], System.Globalization.NumberStyles.HexNumber);
           // ee = new BigInteger(System.Text.Encoding.Unicode.GetBytes(keys[1]));
            textBoxN1.Text = n.ToString();
            textBoxE.Text = ee.ToString();
            read.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            StreamReader read = new StreamReader(openFileDialog2.FileName);
            // byte[] key = File.ReadAllBytes(openFileDialog1.FileName);
            // string []keys = StringExtension.Split(key, separator);
            // string string_key = Convert.ToBase64String(key);
            //byte[] keys = Convert.FromBase64String(string_key);
            string key = read.ReadToEnd();
            string[] keys = key.Split('+');
            n = BigInteger.Parse(keys[0], System.Globalization.NumberStyles.HexNumber);
            d = BigInteger.Parse(keys[1], System.Globalization.NumberStyles.HexNumber);
            // ee = new BigInteger(System.Text.Encoding.Unicode.GetBytes(keys[1]));
            textBoxN2.Text = n.ToString();
            textBoxD.Text = d.ToString();
            read.Close();
        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            StreamWriter write = new StreamWriter(saveFileDialog2.FileName);
            string x = n.ToString("X");
            string y = d.ToString("X");
            // byte[] bytex = Convert.FromBase64String(x);
            //File.WriteAllBytes(saveFileDialog1.FileName, bytex);
            write.Write(x);
            write.Write("+");
            write.Write(y);
            //write.Write(System.Text.Encoding.Unicode.GetString(y));*/
            write.Close();
        }

        private void textBoxD_TextChanged(object sender, EventArgs e)
        {
            d = BigInteger.Parse(textBoxD.Text);
        }

        private void textBoxE_TextChanged(object sender, EventArgs e)
        {
            ee = BigInteger.Parse(textBoxE.Text);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            textBoxEnter.Text = File.ReadAllText(openFileDialog3.FileName);
        }

        private void saveFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            blcklen = Convert.ToInt32(numericUpDown1.Value);
            string szyfrogram;
            if (textBoxEnter.Text != "")
            {
               // this.UseWaitCursor = true;
                   szyfrogram = szyfrowanie(textBoxEnter.Text, blcklen);
                   File.WriteAllText(saveFileDialog3.FileName, szyfrogram);
                   richTextBox2.Text = szyfrogram;
                 //  this.UseWaitCursor = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog4.ShowDialog();
        }

        private void openFileDialog4_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog4.FileName;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBoxEnter.Enabled = false;
                button4.Enabled = false;
                button6.Enabled = true;
                label9.Text = "Ilość bajtów w bloku";
                checkBox2.Enabled = false;
            }
            else
            {
                textBoxEnter.Enabled = true;
                button4.Enabled = true;
                button6.Enabled = false;
                checkBox2.Enabled = true;
                label9.Text = "Ilość znaków w bloku";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBoxEnter.PasswordChar = '•';
            else
                textBoxEnter.PasswordChar = '\0';
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBoxN1.PasswordChar = '•';
                textBoxN2.PasswordChar = '•';
                textBoxE.PasswordChar = '•';
                textBoxD.PasswordChar = '•';
            }
            else
            {
                textBoxN1.PasswordChar = '\0';
                textBoxN2.PasswordChar = '\0';
                textBoxE.PasswordChar = '\0';
                textBoxD.PasswordChar = '\0';
            }
        }

        private void saveFileDialog4_FileOk(object sender, CancelEventArgs e)
        {
            byte[] bytes = File.ReadAllBytes(openFileDialog4.FileName);
            string txt = szyfrowanie(bytes, Convert.ToInt32(numericUpDown1.Value));
            File.WriteAllText(saveFileDialog4.FileName, txt);

        }
    }
    public static class StringExtension
    {
        public static string[] Split(this string str, string splitter)
        {
            return str.Split(new[] { splitter }, StringSplitOptions.None);
        }
    }
}
