using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EFCoreLoading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Book> books { get; } = new();

        List<Author> authors;
        List<Theme> themes;
        List<Category> categories;


        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            using (LibraryContext database = new())
            {
                authors = database.Authors.ToList();
                themes = database.Themes.ToList();
                categories = database.Categories.ToList();
            }
        }

        private void ComboBox1_SelectionChanged(object sender, RoutedEventArgs e)
        {

            ComboBoxItem? selectedItem = ComboBox1.SelectedItem as ComboBoxItem;

            books.Clear();

            if (selectedItem!.Content.ToString() == "Authors")
            {
                ComboBox2.Items.Clear();

                authors.ForEach(a => ComboBox2.Items.Add($"{a.FirstName} {a.LastName}"));
            }
            else if (selectedItem!.Content.ToString() == "Themes")
            {
                ComboBox2.Items.Clear();

                themes.ForEach(t => ComboBox2.Items.Add($"{t.Name}"));
            }
            else if (selectedItem!.Content.ToString() == "Categories")
            {
                ComboBox2.Items.Clear();

                categories.ForEach(c => ComboBox2.Items.Add($"{c.Name}"));
            }

        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem? selectedItem = ComboBox1.SelectedItem as ComboBoxItem;


            if (selectedItem!.Content.ToString() == "Authors")
            {
                books.Clear();

                var selectedAuthor = ComboBox2.SelectedItem as string;

                if (selectedAuthor != null)
                {


                    var author = authors.FirstOrDefault(a => $"{a.FirstName} {a.LastName}" == selectedAuthor);
                    var authorBooks = author!.Books.ToList();
                        //.Join(
                        //    database.Authors,
                        //    b => b.IdAuthor,
                        //    a => a.Id,
                        //    (b, a) => new { Book = b, Author = a }
                        //)
                        //.ToList()
                        //.Where(x => $"{x.Author.FirstName} {x.Author.LastName}" == selectedAuthor)
                        //.Select(x => x.Book)
                        //.ToList();

                    authorBooks.ForEach(b => books.Add(b));
                }
            }
            //else if (selectedItem.Content.ToString() == "Themes")
            //{
            //    var selectedTheme = ComboBox2.SelectedItem as string;

            //    if (selectedTheme != null)
            //    {
            //        books.Clear();

            //        var themeBooks = database.Books
            //            .Join(
            //                database.Themes,
            //                b => b.IdThemes,
            //                t => t.Id,
            //                (b, t) => new { Book = b, Theme = t }
            //            )
            //            .ToList()
            //            .Where(x => x.Theme.Name == selectedTheme)
            //            .Select(x => x.Book)
            //            .ToList();

            //        themeBooks.ForEach(b => books.Add(b));
            //    }
            //}
            //else if (selectedItem.Content.ToString() == "Categories")
            //{
            //    var selectedCategory = ComboBox2.SelectedItem as string;

            //    if (selectedCategory != null)
            //    {
            //        books.Clear();

            //        var categoryBooks = database.Books
            //            .Join(
            //                database.Categories,
            //                b => b.IdCategory,
            //                c => c.Id,
            //                (b, c) => new { Book = b, Category = c }
            //            )
            //            .ToList()
            //            .Where(x => x.Category.Name == selectedCategory)
            //            .Select(x => x.Book)
            //            .ToList();

            //        categoryBooks.ForEach(b => books.Add(b));
            //    }

            //}

        }
    }
}

