using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UESAN.StoreDB.DOMAIN.Core.Entities;
using UESAN.StoreDB.DOMAIN.Infrastructure.Data;

namespace UESAN.StoreDB.DOMAIN.Infrastructure.Repositories
{
    public class CategoryRepository
    {
        private readonly StoreDbContext _dbContext;

        public CategoryRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public IEnumerable<Category> GetCategories()
        //{
        //    var categories = _dbContext.Category.ToList();
        //    return categories;

        //}

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _dbContext.Category.ToListAsync();
            return categories;

        }

        //get category by id
        public async Task<Category> GetCategoryByID(int id)
        {
            var category = await _dbContext.Category.Where(c => c.Id == id && c.IsActive == true).FirstOrDefaultAsync();
            return category;
        }

        //crear una categoria

        public async Task<int> Insert(Category category)
        {
            await _dbContext.Category.AddAsync(category);
            return category.Id;
        }

        //actualizar una categoria

        public async Task<bool> Update(Category category)
        {
            _dbContext.Category.Update(category);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        } 

        //eliminar category

        public async Task<bool> Delete(int id)
        {
            var category = await _dbContext.Category.FirstOrDefaultAsync(c => c.Id == id);
            //_dbContext.Category.Remove(category); //este funciona en caso de no tener una tabla de actividad

            //haremos un update de estado

            if (category == null) return false;

            category.IsActive = false;
            int rows = await _dbContext.SaveChangesAsync();
            return (rows>0);



        }




    }
}
