using MEInsight.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace MEInsight.Web.ViewComponents
{
    public class DetailsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public DetailsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id, string controller, string view)
        {
            DetailsViewModel detailsViewModel = new()
            {
                OrganizationId = id,
            };
            //var items = await GetItems(id, controller, view);
            return View(detailsViewModel);
        }

        //private async Task<DetailsViewModel?> GetItems(Guid id, string controller, string view)
        //{

        //    DetailsViewModel detailsViewModel = new()
        //    {
        //        OrganizationId = id,
        //    };

        //    return detailsViewModel;
        //}
    }
}
