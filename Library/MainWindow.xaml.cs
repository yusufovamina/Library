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
            FillDb();
            using (AppContext db = new AppContext())
            {
                var customers = db.Customers.ToList();
               foreach (var customer in customers) { 
                CustomersComboBox.Items.Add(customer.Name) ;
                }

                var books = db.Books.ToList();
                foreach (var book in books)
                {
                    BooksComboBox.Items.Add(book.Name);
                }
            }
        }

       private void FillDb()
        {
            using (AppContext db = new AppContext())
            {
            //    Customer customer1 = new Customer() { Name = "Afruz", Surname = "Quliyeva", Age = 21 };
            //    Customer customer2 = new Customer() { Name = "Farid", Surname = "Salayev", Age = 19 };
            //    Customer customer3 = new Customer() { Name = "Alina", Surname = "Mirzoyeva", Age = 17 };
            //    Customer customer4 = new Customer() { Name = "Rustam", Surname = "Veliyev", Age = 24 };


            //    Book book1 = new Book() { Name = "Portraint of Dorian Gray", AuthorName = "Oscar Wilde", CustomerObj = customer1 };
            //    Book book2 = new Book() { Name = "Theatre", AuthorName = "Somerset Maugham", CustomerObj = customer3 };
            //    Book book3 = new Book() { Name = "Death on the Nile", AuthorName = "Agata Chistie", CustomerObj = customer2 };
            //    Book book4 = new Book() { Name = "The night in Lisbon", AuthorName = "Erich Maria Remarque", CustomerObj = customer4 };
            //    Book book5 = new Book() { Name = "A walk to remember", AuthorName = "Nicholas Sparks", CustomerObj = customer4 };


            //    db.Books.AddRange(book1, book2, book3, book4, book5);
            //    db.Customers.AddRange(customer1, customer2, customer3);

               
                //db.SaveChanges();
                
            }
        }

        private void TakeBookButton_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                db.Customers.Find(CustomersComboBox.SelectedIndex).Books.Add(db.Books.Find(BooksComboBox.SelectedIndex));

            }
        }
    }
}
