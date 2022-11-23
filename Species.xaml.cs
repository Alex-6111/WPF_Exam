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
using Newtonsoft.Json.Linq;

namespace WPF_Exam
{
    public partial class Species : Page
    {
        public Species()
        {
            InitializeComponent();
 
            var uniqueSpecies = MainWindow.characters.Select(x => x.Species).GroupBy(m => m).Select(g => g.FirstOrDefault());

            foreach (var s in uniqueSpecies)
            {
                species_listBox.Items.Add(s);
            }
        }

        private void Species_listBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            speciesChars_listBox.Items.Clear();

            if (species_listBox.SelectedItem == null)
                return;

            string selected = species_listBox.SelectedItem.ToString();

            var chars = MainWindow.characters.Where(x => x.Species.Contains(selected)).Select(y => y.Name).ToList();

            foreach (string name in chars)
            {
                speciesChars_listBox.Items.Add(name);
            }
        }
        
        private void SpeciesChars_listBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (speciesChars_listBox.SelectedItem == null)
                return;

            string selection = speciesChars_listBox.SelectedItem.ToString();

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
            grid_species_image.Children.Add(image);
            lbl_species.Content = name;
            grid_species_image.Visibility = Visibility.Visible;
            grid_species_inner.Visibility = Visibility.Hidden;
        }
    }
}
