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

namespace WPF_Exam
{
 
    public partial class CharactersPage : Page
    {
        public CharactersPage()
        {
            InitializeComponent();

             
            foreach (Characters c in MainWindow.characters)
            {
                chars_listBox.Items.Add(c.Name);
            }

        }

        private void Search_textBox_KeyUp(object sender, KeyEventArgs e)
        {
             
            chars_listBox.Items.Clear();

            
            var result = MainWindow.characters.Where(x => x.Name.ToLower().Contains(search_textBox.Text.ToLower())).Select(y => y.Name).ToList();

            
            foreach (string name in result)
            {
                chars_listBox.Items.Add(name);
            }
        }
        private void Chars_listBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
             
            if (chars_listBox.SelectedItem == null)
                return;

            string selection = chars_listBox.SelectedItem.ToString();

  
            var image = MainWindow.characters.Where(x => x.Name.Equals(selection)).Select(y => y).ToList();


            string imageURL = "";
            string character = "";
             
            foreach (Characters s in image)
            {
              
                imageURL = s.Image;
                character = s.Name;

            }

            showImage(imageURL, character);
        }

        
        private void showImage(string imageURL, string name)
        {
            var image = new Image();
            var fullFilePath = imageURL;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();

            image.Source = bitmap;
            grid_image.Children.Add(image);
            lbl_char_page.Content = name;
            grid_image.Visibility = Visibility.Visible;
            grid_chars.Visibility = Visibility.Hidden;
        }



    }
}
