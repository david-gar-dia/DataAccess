using MainForm.DATA_ACCESS;
using System;
using MainForm.MODEL;
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
    /// Lógica de interacción para NumberEntry.xaml
    /// </summary>
    public partial class NumberEntry : Window
    {
        //Atributs
        private IDAO dAO;
        private EnumTypeSearch typeSearch;

        //Constructors
        internal NumberEntry(IDAO dAO) : this(dAO, EnumTypeSearch.Index) {}
        internal NumberEntry(IDAO dAO, EnumTypeSearch typeSearch)
        {
            InitializeComponent();
            this.dAO = dAO;
            this.typeSearch = typeSearch;
        }

        //Métodes
        private void accept_btn_numberEntry_Click(object sender, RoutedEventArgs e)
        {
            RawTitle result;
            int refinedGivenNumber, _out;

            if (int.TryParse(number_txtbox_numberEntry.Text, out _out))
                refinedGivenNumber = _out;
            else
                refinedGivenNumber = Convert.ToInt32(number_txtbox_numberEntry.Text.Substring(2));

            switch (typeSearch)
            {
                case EnumTypeSearch.Index:
                    result = dAO.SelectByIndex(refinedGivenNumber);
                    break;
                case EnumTypeSearch.Id:
                    result = dAO.SelectById(refinedGivenNumber);
                    break;
                default:
                    throw new ArgumentException("The given search type was not among the list of valid ones");
            }

            if (result != null)
                MessageBox.Show($"The title found was: {result}");
            else
                MessageBox.Show("No title has been found under this criteria");

            Close();
        }

        private void cancel_btn_numberEntry_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    enum EnumTypeSearch
    {
        Index,
        Id
    }
}
