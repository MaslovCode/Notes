using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notes
{
    public partial class MainWindow : Form
    {
        BindingList<string> Notes = new BindingList<string>();
        public MainWindow()
        {
            InitializeComponent();
            if (!File.Exists("Notes.txt")) 
            { 
                using(StreamWriter w = new StreamWriter("Notes.txt"))
                {
                    w.Write("FirstNote.rtf\n");
                    w.Close(); 
                }
                Editor form = new Editor();
                form.SaveFileRtf("FirstNote.rtf");
            }
            UpdateNotes();
            this.Load += new EventHandler(Form1_Load);

        }
        private void Form1_Load(System.Object sender, System.EventArgs e)
        {
        }
        public void UpdateList() 
        {

            listBox1.DataSource = null;
            listBox1.DataSource = Notes;
        }
        public void UpdateNotes() 
        {
            Notes.Clear();
            using (StreamReader reader = new StreamReader("Notes.txt"))
            {
                while (reader.Peek() != -1) { Notes.Add(System.IO.Path.GetFileNameWithoutExtension(reader.ReadLine())); }
            }
            UpdateList();
        }
        public BindingList<string> Note() 
        {
            return Notes;
        }

        private void addNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor f2 = new Editor();
            f2.Owner = this;
            f2.ShowDialog();

        }

        private void refreshNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateNotes();
        }

        private void deleteNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                using (StreamReader reader = new StreamReader("Notes.txt"))
                {
                    List<string> tmp = new List<string>();
                    while (reader.Peek() != -1)
                    {
                        tmp.Add(reader.ReadLine());
                    }
                    reader.Close();
                    StreamWriter wrt = new StreamWriter("Notes.txt");
                    int n = 0;
                    foreach (string s in tmp)
                    {
                        if (n != listBox1.SelectedIndex) wrt.WriteLine(s);
                        else try { File.Delete(s); } catch (Exception ex) { };
                        n++;
                    }
                    wrt.Close();
                }
                UpdateNotes();
            }
            else { MessageBox.Show("Not a single note is selected!"); }
        }
        
        public void deleteNote(string Name) 
        {
            using (StreamReader reader = new StreamReader("Notes.txt"))
            {
                List<string> tmp = new List<string>();
                while (reader.Peek() != -1)
                {
                    tmp.Add(reader.ReadLine());
                }
                reader.Close();
                StreamWriter wrt = new StreamWriter("Notes.txt");
                foreach (string s in tmp)
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(s) != Name) wrt.WriteLine(s);
                    else try { File.Delete(s); } catch (Exception ex) { MessageBox.Show("Error"); };
                }
                wrt.Close();
            }
            UpdateNotes();
        }
        private void changeNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >=0) 
            {
                using (StreamReader reader = new StreamReader("Notes.txt"))
                {
                    List<string> tmp = new List<string>();
                    while (reader.Peek() != -1)
                    {
                        tmp.Add(reader.ReadLine());
                    }
                    reader.Close();
                    foreach (string s in tmp)
                    {
                        if (System.IO.Path.GetFileNameWithoutExtension(s) == Notes[listBox1.SelectedIndex]) 
                        {
                            Editor form2 = new Editor(s);
                            form2.Owner = this;
                            form2.ShowDialog();
                        }
                    }
                }
            }
        }
    }
}
