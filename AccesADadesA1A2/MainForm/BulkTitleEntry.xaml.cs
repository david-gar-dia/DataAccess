using MainForm.DATA_ACCESS;
using MainForm.MODEL;
using System;
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
    /// Lógica de interacción para BulkTitleEntry.xaml
    /// </summary>
    public partial class BulkTitleEntry : Window
    {
        //Atributs
        private IDAO dAO;
        internal BulkTitleEntry(IDAO dAO)
        {
            InitializeComponent();

            this.dAO = dAO;
        }

        private void accept_btn_bulkTitleEntry_Click(object sender, RoutedEventArgs e)
        {
            RawTitle[] result;
            int startingPos, size;

            startingPos = Convert.ToInt32(startingPos_txtbox_bulkTitleEntry.Text);
            size = Convert.ToInt32(size_txtbox_bulkTitleEntry.Text);

            result = dAO.ReadTitles(startingPos, size);

            MessageBox.Show($"There were {result.Length} titles found.\nThe first one was {result[0]}.\nThe last one was {result[result.Length - 1]}");

            Close();
        }

        private void cancel_btn_bulkTitleEntry_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
