using MainForm.DATA_ACCESS;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MainForm
{
    /// <summary>
    /// Lógica de interacción para MergeEntry.xaml
    /// </summary>
    public partial class MergeEntry : Window
    {
        //Atributs
        IDAO dAO;

        //Constructors
        internal MergeEntry(IDAO dAO)
        {
            InitializeComponent();
            this.dAO = dAO;
        }

        private void file1Select_btn_mergeEntry_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            bool? showDialogResult;

            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;

            showDialogResult = ofd.ShowDialog();

            if (showDialogResult == true)
                file1_txtbox_mergeEntry.Text = ofd.FileName;
        }

        private void file2Select_btn_mergeEntry_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            bool? showDialogResult;

            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;

            showDialogResult = ofd.ShowDialog();

            if (showDialogResult == true)
                file2_txtbox_mergeEntry.Text = ofd.FileName;
        }
        private void outputFile_btn_mergeEntry_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            bool? saveDialogResult;

            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.FilterIndex = 1;

            saveDialogResult = sfd.ShowDialog();

            if (saveDialogResult == true)
                outputFile_txtbox_mergeEntry.Text = sfd.FileName;

            
        }

        private void accept_btn_mergeEntry_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            string file1, file2, outputFile;
            int numTitles;

            file1 = file1_txtbox_mergeEntry.Text;
            file2 = file2_txtbox_mergeEntry.Text;
            outputFile = outputFile_txtbox_mergeEntry.Text;

            numTitles = dAO.Merge(file1, file2, outputFile);

            MessageBox.Show($"Merge completed! {numTitles} titles fused. You can find them in {outputFile}");

            psi.FileName = outputFile;
            psi.UseShellExecute = true;
            Process.Start(psi);
            
            Close();
        }

        private void cancel_btn_mergeEntry_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
