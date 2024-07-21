using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel
    {
        //The readonly keyword indicates that the value of the _service variable can't be changed after it's set in the constructor.
        private readonly PizzaService _service;

        [BindProperty]
        public Pizza NewPizza { get; set; } = default!;

        // PizzaList is initialized to default! to indicate to the compiler that it will be initialized later, so null safety checks aren't required.
        public IList<Pizza> PizzaList { get; set; } = default!;

        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewPizza == null)
            {
                return Page();
            }

            _service.AddPizza(NewPizza);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeletePizza(id);

            return RedirectToAction("Get");
        }
    }
}
