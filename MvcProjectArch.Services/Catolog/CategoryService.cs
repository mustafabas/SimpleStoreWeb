using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcProjectArch.Entities.Tables.Catolog;
using MvcProjectArch.Core.Data;

namespace MvcProjectArch.Services.Catolog
{
    public  class CategoryService:ICategoryService
    {
        private IRepository<Category> _categoyRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoyRepository = categoryRepository;
        }

        public IList<Category> GetAllCategory()
        {
            var query = _categoyRepository.Table;
            return query.ToList();
        }

        public Category GetCategoryByCategoryId(int categoryId)
        {
            if (categoryId == 0)
                throw new ArgumentNullException();
            var query = _categoyRepository.Table;
            return query.FirstOrDefault(x=>x.ID==categoryId);
        }

        public void AddCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException();
            _categoyRepository.Insert(category);
        }

        public void UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException();
            _categoyRepository.Update(category);
        }

        public void DeleteCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException();
            _categoyRepository.Delete(category);
        }


        public IList<Category> GetCategoriesByCategoryParentId(int categoryParentId)
        {
            if (categoryParentId == 0)
                throw new ArgumentNullException();
            var query = _categoyRepository.Table;
            return query.Where(x => x.CategoryParentId == categoryParentId).ToList();
        }


        public IList<Category> GetSubCategoriesByCategoryId(int categoryId)
        {
            if (categoryId == 0)
                throw new ArgumentNullException();
            var query = _categoyRepository.Table;
            return query.Where(x => x.CategoryParentId == categoryId).ToList();
        }
    }
}
