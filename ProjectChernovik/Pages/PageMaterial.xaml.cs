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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectChernovik.Utilities;
using ProjectChernovik.Entities;

namespace ProjectChernovik.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMaterial.xaml
    /// </summary>
    public partial class PageMaterial : Page
    {
        public PageMaterial()
        {
            InitializeComponent();

            ListMaterial.ItemsSource = DBcontext.Context.Material.ToList();

            var MaterialTypes = DBcontext.Context.MaterialType.ToList();
            MaterialTypes.Insert(0, new MaterialType { Title = "Все типы" });
            cmbType.ItemsSource = MaterialTypes;
            cmbType.SelectedIndex = 0;

            cmbSort.Items.Insert(0, "без сортировки");
            cmbSort.Items.Insert(1, "Наименование");
            cmbSort.Items.Insert(2, "Остаток на складе");
            cmbSort.Items.Insert(3, "Стоимость");
            cmbSort.SelectedIndex = 0;
        }

        private void SortingChane()
        {
            var sortItem = DBcontext.Context.Material.ToList();
            if(string.IsNullOrWhiteSpace(TxtSearch.Text) == false)
            {
                sortItem = sortItem.Where(sort => sort.Title.ToLower().Contains(TxtSearch.Text.ToLower()) || sort.Description.ToLower().Contains(TxtSearch.Text.ToLower())).ToList();
            }
            if(cmbType.SelectedIndex > 0)
            {
                sortItem = sortItem.Where(sort => sort.MaterialTypeID == cmbType.SelectedIndex).ToList();
            }
            switch(cmbSort.SelectedIndex)
            {
                case 1:
                    {
                        if (chbfiltr.IsChecked == true)
                        {
                            sortItem = sortItem.OrderByDescending(sort => sort.Title).ToList();
                        }
                        else
                        {
                            sortItem = sortItem.OrderBy(sort => sort.Title).ToList();
                        }
                        break;
                    }
                case 2:
                    {
                        if (chbfiltr.IsChecked == true)
                        {
                            sortItem = sortItem.OrderByDescending(sort => sort.CountInStock).ToList();
                        }
                        else
                        {
                            sortItem = sortItem.OrderBy(sort => sort.CountInStock).ToList();
                        }
                        break;
                    }
                case 3:
                    {
                        if (chbfiltr.IsChecked == true)
                        {
                            sortItem = sortItem.OrderByDescending(sort => sort.Cost).ToList();
                        }
                        else
                        {
                            sortItem = sortItem.OrderBy(sort => sort.Cost).ToList();
                        }
                        break;
                    }
            }
            ListMaterial.ItemsSource = sortItem;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SortingChane();
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortingChane();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortingChane();
        }

        private void chbfiltr_Checked(object sender, RoutedEventArgs e)
        {
            SortingChane();
        }

        private void chbfiltr_Unchecked(object sender, RoutedEventArgs e)
        {
            SortingChane();
        }

        private void ListMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Delet_btn.Visibility = Visibility.Visible;
            Edit_btn.Visibility = Visibility.Visible;
        }

        private void Add_vtn_Click(object sender, RoutedEventArgs e)
        {
            FrameManeger.frmMain.Navigate(new AddMaterialPage(null));
            ListMaterial.ItemsSource = DBcontext.Context.Material.ToList();
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            var item = ListMaterial.SelectedItem;
            FrameManeger.frmMain.Navigate(new AddMaterialPage(item as Material));
            ListMaterial.ItemsSource = DBcontext.Context.Material.ToList();
        }

        private void Delet_btn_Click(object sender, RoutedEventArgs e)
        {
            var delit = ListMaterial.SelectedItem as Material;
            if (MessageBox.Show($"Вы хотите удалить продукт №{delit.ID} ?", "Удаление данных", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DBcontext.Context.Material.Remove(delit);
                    DBcontext.Context.SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ListMaterial.ItemsSource = DBcontext.Context.Material.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
