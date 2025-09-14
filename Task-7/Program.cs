using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Task_7
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Write a LINQ query to find all books that are currently available.
            var av_books = from books in LibraryData.Books
                           where books.IsAvailable = true
                           select books;

            //av_books.ToConsoleTable();



            // Write a LINQ query to get a list of all book titles.
            var av_books2 = LibraryData.Books
                            .Where(book => book.IsAvailable = true)
                            .Select(b => b.Title);

            //foreach (var booktiltle in av_books2){
            //    Console.WriteLine(booktiltle);
            //}



            // Write a LINQ query to find all books in the "Programming" genre.
            var av_books3 = from book in LibraryData.Books
                            where book.Genre == "Programming"
                            select book;

            //av_books3.ToConsoleTable();



            // Write a LINQ query to sort all books alphabetically by title.
            var av_books4 = LibraryData.Books
                            .OrderBy(book => book.Title);

            //av_books4.ToConsoleTable();



            // Write a LINQ query to find all books that cost more than $30.
            var av_books5 = from book in LibraryData.Books
                            where book.Price > 30
                            select book;

            //av_books5.ToConsoleTable();



            // Write a LINQ query to get a list of all unique genres in the library.
            var av_books6 = LibraryData.Books
                            .Select(book => book.Genre).Distinct();

            //foreach (var booktiltle in av_books6)
            //{
            //    Console.WriteLine(booktiltle);
            //}



            // Write a LINQ query to count how many books are in each genre.
            var av_books7 = from book in LibraryData.Books
                            group book by book.Genre into bookgroup
                            select new
                            {
                                genre = bookgroup.Key,
                                number = bookgroup.Count()
                            };

            //av_books7.ToConsoleTable();



            // Write a LINQ query to find all books published after 2010.
            var av_books8 = LibraryData.Books
                            .Where(b => b.PublishedYear > 2010)
                            .Select(b => b);

            //av_books8.ToConsoleTable();



            // Write a LINQ query to get the first 5 books from the collection.
            var av_books9 = (from book in LibraryData.Books
                             select book).Skip(0).Take(5);

            //av_books9.ToConsoleTable();



            // Write a LINQ query to check if there are any books priced over $50.
            var av_books10 = LibraryData.Books
                            .Any(b => b.Price > 50);

            //Console.WriteLine(av_books10);



            // Write a LINQ query to join books with their authors and return book title, author name, and genre.
            var av_books11 = from book in LibraryData.Books
                             join author in LibraryData.Authors
                             on book.AuthorId equals author.Id
                             select new
                             {
                                 book.Title,
                                 author_name = author.Name,
                                 book.Genre
                             };

            //av_books11.ToConsoleTable();



            // Write a LINQ query to calculate the average price of books for each genre.
            var av_books12 = LibraryData.Books
                            .GroupBy(book => book.Genre)
                            .Select(avggenre => new
                            {
                                avggenre.Key,
                                average = Math.Round(avggenre.Average(b => b.Price),2)
                            });

            //av_books12.ToConsoleTable();



            // Write a LINQ query to find the most expensive book in the library.
            var av_books13 = (from book in LibraryData.Books
                              orderby book.Price descending
                              select new {book.Title, book.Price}).Take(1);

            //av_books13.ToConsoleTable();



            // Write a LINQ query to group books by the decade they were published (1990s, 2000s, 2010s, etc.).
            var av_books14 = LibraryData.Books
                            .GroupBy(book => (book.PublishedYear / 10) * 10)
                            .OrderBy(book => book.Key)
                            .Select(b => new
                            {
                                Decade = b.Key,
                                title = b.ToList()
                            });

            //foreach (var av in av_books14)
            //{
            //    Console.WriteLine("================================================");
            //    Console.WriteLine($"Decade: {av.Decade}");
            //    foreach(var book in av.title)
            //    {
            //        Console.WriteLine(book.Title);
            //    }
            //}



            // Write a LINQ query to find all members who have active loans (books not yet returned).
            var av_books15 = from book in LibraryData.Books
                             join loan in LibraryData.Loans
                             on book.Id equals loan.BookId
                             join member in LibraryData.Members
                             on loan.MemberId equals member.Id
                             where loan.ReturnDate == null
                             select new
                             {
                                 member.FullName,
                                 book.Title,
                             };

            //av_books15.ToConsoleTable();



            // Write a LINQ query to find books that have been borrowed more than once.
            var av_books16 = from loan in LibraryData.Loans
                             group loan by loan.BookId into bookgroup
                             where bookgroup.Count() > 1
                             join book in LibraryData.Books
                             on bookgroup.Key equals book.Id
                             select new { book.Title, book.Genre };

            //av_books16.ToConsoleTable();



            // Write a LINQ query to find all overdue books (books with due dates in the past that haven't been returned).
            var av_books17 = LibraryData.Books
                            .Join(LibraryData.Loans,
                            book => book.Id,
                            loan => loan.BookId,
                            (book, loan) => new
                            {
                                book.Title,
                                book.Genre,
                                loan.DueDate,
                                loan.ReturnDate
                            })
                            .Where(b => b.DueDate > DateTime.Now && b.ReturnDate == null)
                            .Select(b => new { b.Title, b.Genre });

            //av_books17.ToConsoleTable();



            // Write a LINQ query to find how many books each author has written, sorted by book count descending.
            var av_books18 = from book in LibraryData.Books
                             join author in LibraryData.Authors
                             on book.AuthorId equals author.Id
                             group book by author.Name into bookgroup
                             select new
                             {
                                 bookgroup.Key,
                                 booknumber = bookgroup.Count()
                             };

            //av_books18.ToConsoleTable();



            // Write a LINQ query to categorize books into price ranges (Cheap: $20, Medium: $20-$40, Expensive: $40)
            // and count books in each range.
            var av_books19 = LibraryData.Books
                            .GroupBy(book =>
                            book.Price < 20 ? "Cheap"
                            : (book.Price >= 20 && book.Price <= 40) ? "Medium"
                            : "Expensive")
                            .Select(book => new
                            {
                                category = book.Key,
                                number = book.Count()
                            });

            //av_books19.ToConsoleTable();



            // Write a LINQ query to calculate loan statistics for each member: total loans, active loans, and average days borrowed.
            var av_books20 = from member in LibraryData.Members
                             join loan in LibraryData.Loans
                             on member.Id equals loan.MemberId into membergroup
                             select new
                             {
                                 memberID = member.Id,
                                 name = member.FullName,
                                 total_loan = membergroup.Count(),
                                 active_loan = membergroup.Count(l => l.ReturnDate == null),
                                 average_days_borrowed = Math.Round(membergroup
                                                        .Where(l => l.ReturnDate != null)
                                                        .Select(l => (int)(l.ReturnDate.Value - l.LoanDate).TotalDays)
                                                        .DefaultIfEmpty(0)
                                                        .Average(),2)

                             };

            //av_books20.ToConsoleTable();
        }
    }
}
