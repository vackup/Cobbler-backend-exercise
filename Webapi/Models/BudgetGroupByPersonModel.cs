namespace Webapi.Models
{
    public class BudgetGroupByPersonModel
    {
        public int Id { get; set; }

        public AuthorResponseModel Author { get; set; }

        public string Title { get; set; }

        public int SalesCount { get; set; }
    }
}