using Microsoft.AspNetCore.Mvc;
using Store_App.Models.DBClasses;

namespace Store_App.Controllers.Interfaces
{
    public interface ICategoryController
    {
        IEnumerable<Category> SearchCategories(string query);
        Task<ActionResult<Category>> GetCategory(int categoryId);
        Task<ActionResult<Category>> CreateCategory(Category category);
        Task<ActionResult> UpdateCategory(int categoryId, Category category);
        Task<ActionResult> DeleteCategory(int categoryId);
    }
}
