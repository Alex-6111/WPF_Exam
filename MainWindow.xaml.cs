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
using System.IO;
using Newtonsoft.Json;
using Newtonsoft;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace WPF_Exam
{

    public partial class MainWindow : Window
    {
         
        public static List<Characters> characters = new List<Characters>();

        public MainWindow()
        {
            InitializeComponent();

            getChars();

            Main.Content = new CharactersPage();
        }

        private void getChars()
        {
            string url = "https://rickandmortyapi.com/api/character";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using(StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            JObject job = JObject.Parse(json: response);

            List<JToken> results = job["results"].Children().ToList();

            foreach (JToken result in results)
            {
                Characters character = result.ToObject<Characters>();

                characters.Add(character);
            }
        }

        private void Species_button_Click(object sender, RoutedEventArgs e)
        {
            changeContent("Species");
        }

        private void CharsPage_button_Click(object sender, RoutedEventArgs e)
        {
            changeContent("Characters");
        }

        public void changeContent(string content)
        {
            switch (content)
            {
                case "Species":
                    Main.Content = new Species();
                    break;
                case "Characters":
                    Main.Content = new CharactersPage();
                    break;
            }
        }
    }
}
