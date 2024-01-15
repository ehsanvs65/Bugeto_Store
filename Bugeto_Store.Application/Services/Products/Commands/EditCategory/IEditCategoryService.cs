using Bugeto_Store.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bugeto_Store.Common.Dto;

namespace Bugeto_Store.Application.Services.Products.Commands.EditCategory
{
    public interface IEditCategoryService
    {
        ResultDto Execute(RequestEditCategoryDto request);
    }
    public class EditCategoryService : IEditCategoryService
    {
        private readonly IDataBaseContext _context;
        public EditCategoryService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestEditCategoryDto request)
        {
            var Category = _context.Categories.Find(request.Id);
            if (Category == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد"
                };
            }
            Category.Name = request.Name;
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "ویرایش کاربر انجام شد"
            };

        }
    }
    public class RequestEditCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
