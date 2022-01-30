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
using ProjectChernovik.Entities;
using ProjectChernovik.Utilities;

namespace ProjectChernovik.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddMaterialPage.xaml
    /// </summary>
    public partial class AddMaterialPage : Page
    {
        private Material currentMaterial;
        public AddMaterialPage(Material material)
        {
            InitializeComponent();
            currentMaterial = material ?? new Material();
            cmbType.ItemsSource = DBcontext.Context.MaterialType.ToList();
            DataContext = currentMaterial;
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentMaterial.Title))
                builder.AppendLine("Укажите наименование");
            if (string.IsNullOrWhiteSpace(currentMaterial.CountInPack.ToString()))
                builder.AppendLine("Укажите количество в упаковке");
            if (string.IsNullOrWhiteSpace(currentMaterial.Unit))
                builder.AppendLine("Укажите Единицу измерения");
            if (string.IsNullOrWhiteSpace(currentMaterial.MinCount.ToString()))
                builder.AppendLine("Укажите минимальное количество");
            if (string.IsNullOrWhiteSpace(currentMaterial.Cost.ToString()))
                builder.AppendLine("Укажите цену");
            if (string.IsNullOrWhiteSpace(currentMaterial.Description))
                currentMaterial.Description = "";
            if (string.IsNullOrWhiteSpace(currentMaterial.Image))
                currentMaterial.Image = "";

            if (builder.Length>0)
            {
                MessageBox.Show(builder.ToString());
                return;
            }

            if (currentMaterial.ID == 0)
            {
                DBcontext.Context.Material.Add(currentMaterial);
            }

            try
            {
                DBcontext.Context.SaveChanges();
                MessageBox.Show("Данне сохранены");
                FrameManeger.frmMain.Navigate(new PageMaterial());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            FrameManeger.frmMain.GoBack();
        }
    }
}
