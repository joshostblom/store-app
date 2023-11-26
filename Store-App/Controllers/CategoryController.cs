using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store_App.Models.DBClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Store_App.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly StoreAppDbContext _categoryContext;

        public CategoryController(StoreAppDbContext categoryContext)
        {
            _categoryContext = categoryContext;
        }

        [HttpGet("{query}")]
        public IEnumerable<Category> SearchCategories(string query)
        {
            if (_categoryContext.Products == null)
            {
                return new List<Category>();
            }

            IEnumerable<Category> categories = _categoryContext.Categories.ToList().Where(x => x.Name.ToLower().Contains(query.ToLower()));

            return categories;
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<Category>> GetCategory(int categoryId)
        {
            var category = await _categoryContext.Categories.FindAsync(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            _categoryContext.Categories.Add(category);
            await _categoryContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { categoryId = category.CategoryId }, category);
        }

        [HttpPut("{categoryId}")]
        public async Task<ActionResult> UpdateCategory(int categoryId, Category category)
        {
            if (categoryId != category.CategoryId)
            {
                return BadRequest();
            }

            var existingCategory = await _categoryContext.Categories.FindAsync(categoryId);

            if (existingCategory == null)
            {
                return NotFound();
            }

            // Update the properties of existingCategory
            existingCategory.Name = category.Name;

            // Set the entity state to Modified
            _categoryContext.Entry(existingCategory).State = EntityState.Modified;

            try
            {
                await _categoryContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            var category = await _categoryContext.Categories.FindAsync(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            _categoryContext.Categories.Remove(category);
            await _categoryContext.SaveChangesAsync();

            return Ok();
        }
    }
}
