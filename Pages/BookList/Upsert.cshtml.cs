using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Book Book { get; set; }
        public async Task OnGet(int? id)
        {
            Book = await _db.Book.FindAsync(id);
        }
        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var bookDb = await _db.Book.FindAsync(Book.Id);
                bookDb.Name = Book.Name;
                bookDb.Author = Book.Author;
                bookDb.ISBN = Book.ISBN;
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}