using Project3.Dtos;
using Project3.Models;

namespace Project3
{
    public static class Extensions
    {
        public static ContactDto AsContactDto(this ContactModel contact)
        {
            return new ContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                Surname = contact.Surname,
                Email = contact.Email,
                Category = contact.Category,
                Other = contact.Other,
                SubCategory = contact.SubCategory,
                PhoneNumber = contact.PhoneNumber,
                Date = contact.Date
            };
        }
        public static CategoryDto AsCategoryDto(this CategoryModel category)
        {
            return new CategoryDto
            {
                Category = category.Category
            };
        }
        public static SubCategoryDto AsSubCategoryDto(this SubCategoryModel subCategory)
        {
            return new SubCategoryDto
            {
                SubCategory = subCategory.SubCategory
            };
        }

    }
}
