using System;
using System.Collections.Generic;
using System.IO;
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

namespace TeamTasker.Views
{
    /// <summary>
    /// Логика взаимодействия для UserProfileView.xaml
    /// </summary>
    public partial class UserProfileView : UserControl
    {
        public UserProfileView()
        {
            InitializeComponent();
        }
        private void PropertyChanged(object sender, RoutedEventArgs e)
        {
            if (SaveButton != null)
                SaveButton.IsEnabled = true;
            if (CancelButton != null)
                CancelButton.IsEnabled = true;
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (SaveButton != null)
                SaveButton.IsEnabled = false;
            if (CancelButton != null)
                CancelButton.IsEnabled = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Image_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string imagePath = files[0];
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    BitmapImage bitmapImage = new BitmapImage();
                    using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.EndInit();
                    }
                    UserImage.Source = bitmapImage;
                    if (SaveButton != null)
                        SaveButton.IsEnabled = true;
                    if (CancelButton != null)
                        CancelButton.IsEnabled = true;
                }
            }
        }

        private void Image_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    // Проверка расширения файла или его типа MIME
                    string extension = System.IO.Path.GetExtension(file);
                    if (!string.IsNullOrEmpty(extension) && IsImageExtension(extension))
                    {
                        e.Effects = DragDropEffects.Copy;
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.None;
                        e.Handled = false;
                        return;
                    }
                }
            }

            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }
        private bool IsImageExtension(string extension)
        {
            // Возможные расширения файлов изображений
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

            // Проверка, является ли расширение допустимым расширением изображения
            return imageExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }
    }
}
