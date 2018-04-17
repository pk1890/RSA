using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;

namespace Szyfr_RSA
{
    public partial class generator_klucza : Form
    {
        Form1 form1;
        public generator_klucza(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBoxP.Text != "" && textBoxQ.Text != "")
            {
                BigInteger p, q;
                p = BigInteger.Parse(textBoxP.Text);
                q = BigInteger.Parse(textBoxQ.Text);
                form1.n = p * q;
                form1.fi = (p - 1) * (q - 1);
                RandomNumberGenerator rnd = RandomNumberGenerator.Create();
                byte[] rand = new byte[form1.fi.ToByteArray().LongLength];
                do
                {
                    rnd.GetBytes(rand);
                    form1.ee = new BigInteger(rand);
                } while (Form1.nwd(form1.fi, form1.ee) != 1 || form1.ee <= 1 || form1.ee >= form1.fi);
                form1.d = Form1.inverse_modulo(form1.ee, form1.fi);
                this.Close();
            }
        }

        private void LoadP_Click(object sender, EventArgs e)
        {
            openFileDialogP.ShowDialog();
        }

        private void openFileDialogP_FileOk(object sender, CancelEventArgs e)
        {
            string[] primes = File.ReadAllLines(openFileDialogP.FileName);
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();
            byte[] bytes  = new byte[8];
            rnd.GetBytes(bytes);
            long linia = Math.Abs( BitConverter.ToInt64(bytes, 0))%primes.LongLength;
            textBoxP.Text = primes[linia];

        }

        private void LoadQ_Click(object sender, EventArgs e)
        {
            openFileDialogQ.ShowDialog();
        }

        private void openFileDialogQ_FileOk(object sender, CancelEventArgs e)
        {
            string[] primes = File.ReadAllLines(openFileDialogQ.FileName);
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();
            byte[] bytes = new byte[8];
            rnd.GetBytes(bytes);
            long linia = Math.Abs(BitConverter.ToInt64(bytes, 0)) % primes.LongLength;
            textBoxQ.Text = primes[linia];
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBoxP.PasswordChar = '•';
                textBoxQ.PasswordChar = '•';
            }
            else
            {
                textBoxP.PasswordChar = '\0';
                textBoxQ.PasswordChar = '\0';
            }
        }
    }
}
