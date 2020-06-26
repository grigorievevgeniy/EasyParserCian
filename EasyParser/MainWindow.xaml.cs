using EasyParser.Core;
using EasyParser.Core.Cian;
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

namespace EasyParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ParserWorker<string[]> parser;

        public MainWindow()
        {
            InitializeComponent();

            parser = new ParserWorker<string[]>(new CianParser());

            StartButton.Click += StartButton_Click;
            StopButton.Click += StopButton_Click;

            parser.OnComplited += Parser_OnComplited;
            parser.OnNewData += Parser_OnNewData;
        }

        private void Parser_OnNewData(object arg1, string[] listAdvert)
        {
            foreach (var item in listAdvert)
            {
                ListText.Text += item + "\n   *******************  \n";
            }
        }

        private void Parser_OnComplited(object obj)
        {
            MessageBox.Show("Парсинг завершен");
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            parser.Stop();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            parser.Settings = new CianParserSettings();
            parser.Settings.StartPage = Convert.ToInt32(StartPage.Text);
            parser.Settings.EndPage = Convert.ToInt32(EndPage.Text);

            parser.Start();
        }
    }
}
