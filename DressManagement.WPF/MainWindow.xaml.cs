using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DressManagement.WPF.Utils;
using DevExpress.Xpf.WindowsUI;
using DevExpress.Xpf.Docking;

namespace DressManagement.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        MainWindowVM Viewmodel;
        public MainWindow()
        {
            InitializeComponent();
            Viewmodel = new MainWindowVM();
            DataContext = Viewmodel;
            GridControlLocalizer.Active = new TurkishFiltersLocalizer();
            //notifier.ShowInformation("Star Note Veri Takip Uygulaması Versiyon 2.0");
        }

       
 
        private void HamburgerMenuNavigationButton_Click_1(object sender, RoutedEventArgs e)
        {
            string msg = " Uygulamayı kapatmak istiyor musunuz?";
            MessageBoxResult result = MessageBox.Show(msg, "Star Note ", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void HamburgerSubMenuNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            HamburgerSubMenuNavigationButton menu = sender as HamburgerSubMenuNavigationButton;
            DocumentPanel panel = null;
            if (menu != null)
            {
                Viewmodel.GetActivePage(Convert.ToInt16(menu.Tag));
            }
        }

        private void ThemedWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string msg = " Uygulamayı kapatmak istiyor musunuz?";
            MessageBoxResult result = MessageBox.Show(msg, "Dress Management App", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
    }

    public class TurkishFiltersLocalizer : GridControlLocalizer
    {
        protected override void PopulateStringTable()
        {
            base.PopulateStringTable();

            AddString(GridControlStringId.GridGroupPanelText, "Grouplamak istediğiniz alanları buraya sürükleyiniz.");
            AddString(GridControlStringId.MenuColumnClearSorting, "Sıralamayı Kaldır");
            AddString(GridControlStringId.MenuColumnHideGroupPanel, "Grouplama Alanını Gizle");
            AddString(GridControlStringId.MenuColumnShowGroupPanel, "Grouplama Alanını Göster");
            //AddString(GridControlStringId.menucolumnhidecol, " Sütünü Kaldır");
            AddString(GridControlStringId.MenuColumnFilterEditor, " Filtreleme & Düzenleme");
            //AddString(GridControlStringId.menucolumnshow , "Aramayı Göster");
            AddString(GridControlStringId.MenuColumnShowColumnChooser, "Kolon Seçimini Aç");
            AddString(GridControlStringId.MenuColumnSortAscending, " Artan");
            AddString(GridControlStringId.MenuColumnSortDescending, "Azalan");
            AddString(GridControlStringId.MenuColumnGroup, "Bu alan için Grupla");
            AddString(GridControlStringId.MenuColumnUnGroup, "Çöz");
            //AddString(GridControlStringId.MenuColumnColumnCustomization, "Özel Sütün");
            AddString(GridControlStringId.MenuColumnBestFit, "En uygun Genişlik");
            //AddString(GridControlStringId.MenuColumnFilter, "Filtrele");
            AddString(GridControlStringId.MenuColumnClearFilter, "Filtremeyi Kaldır");
            AddString(GridControlStringId.MenuColumnBestFitColumns, "Tüm Sütünler Optimal");
            AddString(GridControlStringId.ConditionalFormatting_Manager_BeginningWith, "ile başlıyor");
            //AddString(GridControlStringId.ConditionalFormatting_Manager_BeginningWith, "ile başlıyor");
            AddString(GridControlStringId.MenuGroupPanelClearGrouping, "Gruplandırmayı temizle");
            //AddString(GridControlStringId.FindControlClearButton,"Temizle");
            //AddString(GridControlStringId.FindControlFindButton,"Ara");
            AddString(GridControlStringId.FilterEditorTitle, "Kolon Filtreleme");

            AddString(GridControlStringId.MenuGroupPanelFullExpand, "Hepsini genişlet");
            AddString(GridControlStringId.MenuGroupPanelFullCollapse, "Hepsini gizle");
            AddString(GridControlStringId.ColumnChooserCaption, "Kolon Seçimi");
            AddString(GridControlStringId.ExtendedColumnChooserSearchColumns, "Kolon arama");
        }
    }
}