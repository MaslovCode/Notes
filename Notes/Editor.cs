using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Notes
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
        }
        public Editor(string path)
        {
            InitializeComponent();
            if(File.Exists(path)) this.richTextBox1.LoadFile(path,RichTextBoxStreamType.RichText);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
                
            saveFileDialog.Filter = "Текстовые файлы (*.rtf)|*.rtf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bool find = false;
                MainWindow form = new MainWindow();
                BindingList<string> bList = new BindingList<string>();
                bList = form.Note();
                for (int i = 0; i < bList.Count; i++) if (bList[i] == System.IO.Path.GetFileNameWithoutExtension(saveFileDialog.FileName)) find = true;
                if (!find) 
                {
                    this.richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                    File.AppendAllText("Notes.txt", saveFileDialog.FileName + "\n");
                }
                else 
                {
                    form.deleteNote(System.IO.Path.GetFileNameWithoutExtension(saveFileDialog.FileName));
                    this.richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                    File.AppendAllText("Notes.txt", saveFileDialog.FileName + "\n");
                }

            }

        }

        private void partOfTheTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;

            fontDialog1.Font = this.richTextBox1.SelectionFont;
            fontDialog1.Color = this.richTextBox1.SelectionColor;

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                this.richTextBox1.SelectionFont = fontDialog1.Font;
                this.richTextBox1.SelectionColor = fontDialog1.Color;
            }
        }

        private void allTheTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;

            fontDialog1.Font = this.richTextBox1.Font;
            fontDialog1.Color = this.richTextBox1.ForeColor;

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                this.richTextBox1.Font = fontDialog1.Font;
                this.richTextBox1.ForeColor = fontDialog1.Color;
            }
        }

        private void insertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "Images |*.png;*.jpg";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Image image = Image.FromFile(openFileDialog1.FileName);
                    Clipboard.SetImage(image);
                    richTextBox1.Paste();
                }
            }
        }
        public void SaveFileRtf(string path) 
        {
            this.richTextBox1.SaveFile(path, RichTextBoxStreamType.RichText);
        }
    }
}
