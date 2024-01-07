using Bugeto_Store.Application.Interfaces.Contexts;
using Bugeto_Store.Common;

namespace Bugeto_Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;
        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultGetUserDto Execute(RequestGetUserDto request)
        {
            var users=_context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                users = users.Where(p => p.FullName.Contains(request.SearchKey) && p.Email.Contains(request.SearchKey));
            }
            int rowsCount = 0;
            var userList= users.ToPaged(request.Page, 20, out rowsCount).Select(p => new GetUsersDto
            {
                Email=p.Email,
                FullName=p.FullName,
                id=p.Id,
            }).ToList();
            return new ResultGetUserDto
            {
                Rows=rowsCount,
                Users=userList,
            };
        }
    }
}
