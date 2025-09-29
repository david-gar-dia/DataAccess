using MainForm.DATA_ACCESS;
using System;
using System.Collections.Generic;
using MainForm.MODEL;
using System.Diagnostics;
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
using Microsoft.Win32;

namespace MainForm
{
    /// <summary>
    /// Lógica de interacción para PreMergeEntry.xaml
    /// </summary>
    public partial class PreMergeEntry : Window
    {
        //Atributs
        private IDAO dAO;

        //Constructors
        internal PreMergeEntry(IDAO dAO)
        {
            InitializeComponent();
            this.dAO = dAO;
        }

        //Métodes
        private void outputFile_btn_preMergeEntry_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            bool? saveDialogResult;

            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.FilterIndex = 1;

            saveDialogResult = sfd.ShowDialog();

            if (saveDialogResult == true)
                outputFile_txtbox_preMergeEntry.Text = sfd.FileName;
        }

        private void accept_btn_preMergeEntry_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            RawTitle[] titlesToPremerge;
            string outputFile;
            int length, startingPos, endingPos;

            startingPos = Convert.ToInt32(startPos_txtbox_preMergeEntry.Text);
            endingPos = Convert.ToInt32(finalPos_txtbox_preMergeEntry.Text);

            length = endingPos - startingPos + 1;
            
            titlesToPremerge = dAO.ReadTitles(startingPos, length);

            outputFile = outputFile_txtbox_preMergeEntry.Text;

            if (outputFile == "")
                outputFile = $"preMerge_{titlesToPremerge.Count()}.txt";

            dAO.PreMerge(titlesToPremerge, outputFile);

            MessageBox.Show($"PreMerge has been successfully done, check {outputFile} to find the result");

            psi.FileName = outputFile;
            psi.UseShellExecute = true;
            Process.Start(psi);

            Close();
        }

        private void cancel_btn_preMergeEntry_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
