using MainForm.DATA_ACCESS;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace MainForm
{
    /// <summary>
    /// Lógica de interacción para GenreEntry.xaml
    /// </summary>
    public partial class GenreEntry : Window
    {
        //Atributs
        private IDAO dAO;

        //Constructors
        internal GenreEntry(IDAO dAO)
        {
            InitializeComponent();
            this.dAO = dAO;
        }

        //Métodes
        private void accept_btn_genreEntry_Click(object sender, RoutedEventArgs e)
        {
            int numberOfFoundLines;

            numberOfFoundLines = dAO.SelectByGenre(genre_txtbox_genreEntry.Text, "GenreTitleList.txt");
            if (numberOfFoundLines != 0)
                MessageBox.Show($"A total of {numberOfFoundLines} lines have been written into \"GenreTitleList.txt\"");
            else
                MessageBox.Show("No titles with this genre have been found");

            Close();
        }

        private void cancel_btn_genreEntry_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
