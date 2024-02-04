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

namespace Library
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //FillDb();
            using (AppContext db = new AppContext())
            {
                var customers = db.Customers.ToList();
                foreach (var customer in customers)
                {
                    CustomersListBox.Items.Add(customer.Name);
                    CustomersComboBox.Items.Add(customer.Name);
                }

                var books = db.Books.ToList();
                foreach (var book in books)
                {
                    BooksComboBox.Items.Add(book.Name);
                }
            }
            CustomersListBox.SelectionChanged += CustomerSelection;

        }

        private void FillDb()
        {
            using (AppContext db = new AppContext())
            {
                Customer customer1 = new Customer() { Name = "Afruz", Surname = "Quliyeva", Age = 21 };
                Customer customer2 = new Customer() { Name = "Farid", Surname = "Salayev", Age = 19 };
                Customer customer3 = new Customer() { Name = "Alina", Surname = "Mirzoyeva", Age = 17 };
                Customer customer4 = new Customer() { Name = "Rustam", Surname = "Veliyev", Age = 24 };


                Book book1 = new Book() { Name = "Portraint of Dorian Gray", AuthorName = "Oscar Wilde" };
                Book book2 = new Book() { Name = "Theatre", AuthorName = "Somerset Maugham" };
                Book book3 = new Book() { Name = "Death on the Nile", AuthorName = "Agata Chistie" };
                Book book4 = new Book() { Name = "The night in Lisbon", AuthorName = "Erich Maria Remarque" };
                Book book5 = new Book() { Name = "A walk to remember", AuthorName = "Nicholas Sparks" };


                db.Books.AddRange(book1, book2, book3, book4, book5);
                db.Customers.AddRange(customer1, customer2, customer3);


                db.SaveChanges();

            }
        }

        private void TakeBookButton_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                if (BooksComboBox.SelectedIndex == -1 || CustomersComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Choose both the customer and the book!");
                }
                else
                {
                    db.Books?.Find(BooksComboBox.SelectedIndex+1).Customers.Add(db.Customers?.Find(CustomersComboBox.SelectedIndex + 1));
                    db.Customers?.Find(CustomersComboBox.SelectedIndex+1).Books.Add(db.Books?.Find(BooksComboBox.SelectedIndex + 1));
                    db.SaveChanges();


                    MessageBox.Show("Book was added to customer's list");
                }
            }
        }


        private void CustomerSelection(object sender, SelectionChangedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                BooksListBox.Items.Clear();
                Customer ind = db.Customers.Find(CustomersListBox.SelectedIndex+1);
                var books = db.Books.Where(b => b.Customers.Contains(ind)).ToList();
                foreach (var book in books)
                {
                    BooksListBox.Items.Add(book.Name);
                }
            }

        }
        private void UpdateBooksListBox()
        {
            using (AppContext db = new AppContext())
            {
                BooksListBox.Items.Clear();
                Customer ind = db.Customers.Find(CustomersListBox.SelectedIndex + 1);
                var books = db.Books.Where(b => b.Customers.Contains(ind)).ToList();
                foreach (var book in books)
                {
                    BooksListBox.Items.Add(book.Name);
                }
            }

        }
        private void ReturnBookButton_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                int selectedBookIndex = BooksListBox.SelectedIndex;
                int selectedCustomerIndex = CustomersListBox.SelectedIndex;

                if (selectedBookIndex >= 0 && selectedCustomerIndex >= 0)
                {
                    var bookToRemove = db.Books?.Find(selectedBookIndex + 1);
                    var customerToRemoveBookFrom = db.Customers?.Find(selectedCustomerIndex + 1);

                    if (bookToRemove != null && customerToRemoveBookFrom != null)
                    {
                        customerToRemoveBookFrom.Books.Remove(bookToRemove);
                        bookToRemove.Customers.Remove(customerToRemoveBookFrom);

                        db.SaveChanges();
                      
                        BooksListBox.Items.Clear();
                        foreach (var book in customerToRemoveBookFrom.Books.ToList())
                        {
                            BooksListBox.Items.Add(book);

                        }
                      
                        MessageBox.Show("Book was returned");
                    }
                    else
                    {
                        MessageBox.Show("Invalid book or customer index");
                    }
                }
                else
                {
                    MessageBox.Show("No book or customer selected");
                }


            }
        }
    }
}