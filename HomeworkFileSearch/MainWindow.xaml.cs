using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HomeworkFileSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private async void StartSearch_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            string wordToSearch = WordToSearchTextBox.Text;

            if (File.Exists(filePath))
            {
                ResultTextBlock.Text = "Start Searching";
                StartSearchButton.IsEnabled = false;
                try
                {
                    int count = await CountWordInFileAsync(filePath, wordToSearch);
                    ResultTextBlock.Text = $"Word: '{wordToSearch}' Occur {count} Times";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                StartSearchButton.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("File cannot Search.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<int> CountWordInFileAsync(string filePath, string word)
        {
            int count = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    string[] words = line.Split(new char[] { ' ', ',', '.', ';', ':', '-', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string w in words)
                    {
                        if (string.Equals(w, word, StringComparison.OrdinalIgnoreCase))
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }
    }
}

