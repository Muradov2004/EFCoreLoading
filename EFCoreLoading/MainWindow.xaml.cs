using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace EFCoreLoading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Book> books { get; } = new();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void ComboBox1_SelectionChanged(object sender, RoutedEventArgs e)
        {

            ComboBoxItem? selectedItem = ComboBox1.SelectedItem as ComboBoxItem;

            books.Clear();
            using (LibraryContext database = new())
            {

                if (selectedItem!.Content.ToString() == "Authors")
                {
                    ComboBox2.Items.Clear();

                    var authors = database.Authors.ToList();

                    authors.ForEach(a => ComboBox2.Items.Add($"{a.FirstName} {a.LastName}"));
                }
                else if (selectedItem!.Content.ToString() == "Themes")
                {
                    ComboBox2.Items.Clear();

                    var themes = database.Themes.ToList();

                    themes.ForEach(t => ComboBox2.Items.Add($"{t.Name}"));
                }
                else if (selectedItem!.Content.ToString() == "Categories")
                {
                    ComboBox2.Items.Clear();

                    var categories = database.Categories.ToList();

                    categories.ForEach(c => ComboBox2.Items.Add($"{c.Name}"));
                }

            }
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem? selectedItem = ComboBox1.SelectedItem as ComboBoxItem;

            books.Clear();

            if (selectedItem!.Content.ToString() == "Authors")
            {

                var selectedAuthor = ComboBox2.SelectedItem as string;

                if (selectedAuthor != null)
                {

                    using (LibraryContext database = new())
                    {
                        var authors = database.Authors.ToList();

                        var author = authors.FirstOrDefault(a => $"{a.FirstName} {a.LastName}" == selectedAuthor);

                        var authorBooks = author!.Books.ToList();

                        authorBooks.ForEach(b => books.Add(b));
                    }
                }
            }
            else if (selectedItem.Content.ToString() == "Themes")
            {
                var selectedTheme = ComboBox2.SelectedItem as string;

                if (selectedTheme != null)
                {

                    using (LibraryContext database = new())
                    {
                        var themes = database.Themes.ToList();

                        var theme = themes.FirstOrDefault(t => t.Name == selectedTheme);

                        var themeBooks = theme!.Books.ToList();

                        themeBooks.ForEach(b => books.Add(b));
                    }
                }
            }
            else if (selectedItem.Content.ToString() == "Categories")
            {
                var selectedCategory = ComboBox2.SelectedItem as string;

                if (selectedCategory != null)
                {

                    using (LibraryContext database = new())
                    {
                        var categories = database.Categories.ToList();

                        var category = categories.FirstOrDefault(t => t.Name == selectedCategory);

                        var categoryBooks = category!.Books.ToList();

                        categoryBooks.ForEach(b => books.Add(b));
                    }

                }

            }

        }
    }
}

