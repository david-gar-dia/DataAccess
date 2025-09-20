using MainForm.DATA_ACCESS;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Atributs
        private IDAO dAO;

        //Constructors
        public MainWindow()
        {
            InitializeComponent();
            dAO = DAOFactory.CreateDAOImpl(EnumDAOType.CSV);
        }

        //Métodes
        private void genre_btn_mainWindow_Click(object sender, RoutedEventArgs e)
        {
            GenreEntry genreEntry = new GenreEntry(dAO);

            genreEntry.Owner = this;
            genreEntry.ShowDialog();
        }

        private void index_btn_mainWindow_Click(object sender, RoutedEventArgs e)
        {
            NumberEntry numberEntry = new NumberEntry(dAO);

            numberEntry.Owner = this;
            numberEntry.ShowDialog();
        }

        private void id_btn_mainWindow_Click(object sender, RoutedEventArgs e)
        {
            NumberEntry numberEntry = new NumberEntry(dAO, EnumTypeSearch.Id);

            numberEntry.Owner = this;
            numberEntry.ShowDialog();
        }

        private void bulkIndex_btn_mainWindow_Click(object sender, RoutedEventArgs e)
        {
            BulkTitleEntry bulkTitleEntry = new BulkTitleEntry(dAO);

            bulkTitleEntry.Owner = this;
            bulkTitleEntry.ShowDialog();
        }
    }
}