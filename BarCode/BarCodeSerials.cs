using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using static System.Math;
using System.Drawing;
using System.Security.AccessControl;

namespace BarCode
{
    public partial class BarCodeSerials : Form
    {
        public BarCodeSerials()
        {
            InitializeComponent();
        }
      
  
        private void button1_Click(object sender, EventArgs e)
        {
           
            DialogResult result1 = MessageBox.Show("Do you Want To Create New Serial",
                    "request serial",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (result1 != DialogResult.Yes)
                return;
            else
            {
                String FileName = "Serials";
                String Path = @"D:\Genrate Serial\Data\";
                if (Directory.Exists(Path))
                {
                    try
                    {
                        string folderPath = Path;
                        string adminUserName = Environment.UserName;// getting your adminUserName
                        DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                        FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                        ds.AddAccessRule(fsa);
                        Directory.SetAccessControl(folderPath, ds);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                    if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                    StreamWriter stream = new StreamWriter(Path+FileName);
                    File.SetAttributes(Path + FileName, File.GetAttributes(Path + FileName) | FileAttributes.Hidden);

                    try
                    {
                        string folderPath = Path;
                        string adminUserName = Environment.UserName;// getting your adminUserName
                        DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                        FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                        ds.AddAccessRule(fsa);
                        Directory.SetAccessControl(folderPath, ds);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    stream.Close();

     //               unlock folder
     //               try
     //               {
     //                   string folderPath = textBox1.Text;
     //                   string adminUserName = Environment.UserName;// getting your adminUserName
     //                   DirectorySecurity ds = Directory.GetAccessControl(folderPath);
     //                   FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny)
     //ds.RemoveAccessRule(fsa);
     //                   Directory.SetAccessControl(folderPath, ds);
     //                   MessageBox.Show("UnLocked");
     //               }
     //               catch (Exception ex)
     //               {
     //                   MessageBox.Show(ex.Message);
     //               }
                }
             
                StreamReader reader = new StreamReader(Path + FileName);
              
                String Content = reader.ReadToEnd();
                reader.Close();
                Boolean temp = false;
                Random r = new Random();
                while (!temp)
                {
                    int randomnum = r.Next();
                    if (!Content.Contains(randomnum.ToString() + "\n"))
                    {
                        StreamWriter writer = new StreamWriter(Path + FileName, true);
                        writer.WriteLine(randomnum.ToString() + "\n");
                        writer.Close();
                        label1.Text = randomnum.ToString();
                        richTextBox1.Text = Content + randomnum.ToString();
                        richTextBox1.SelectionStart = richTextBox1.Text.Length;
                        // scroll it automatically
                        richTextBox1.ScrollToCaret();
                        textBox1.Text = randomnum.ToString();
                        textBox1.Focus();
                        temp = true;
                    }
                }
            }
        }

        private void BarCodeSerials_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
