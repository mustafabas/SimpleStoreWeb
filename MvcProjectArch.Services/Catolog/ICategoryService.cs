using MvcProjectArch.Entities.Tables.Catolog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArch.Services.Catolog
{
    public interface ICategoryService
    {
        IList<Category> GetAllCategory();
        Category GetCategoryByCategoryId(int categoryId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        IList<Category> GetCategoriesByCategoryParentId(int categoryParentId);
        IList<Category> GetSubCategoriesByCategoryId(int categoryId);
        
        
    }
}
