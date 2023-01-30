using Dapper;
using AutoMapper;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using System.Data;

namespace WebAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly ADONetDbContext _context;
        public UserRepository(ADONetDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            var query = "SELECT * FROM AspNetUsers; ";

            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<AppUser>(query);
                return users.ToList();
            }
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            var query = "SELECT * FROM AspNetUsers WHERE UserName = @UserName; ";

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<AppUser>(query, new { username });
                return user;
            }
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            var query = "SELECT * FROM AspNetUsers WHERE Id = @id; ";

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<AppUser>(query, new { id });
                return user;
            }
        }

        public async Task UpdateUserAsync(AppUser user)
        {
            var query = @"
UPDATE AspNetUsers 
SET 
    DisplayName = @DisplayName, 
    FirstName = @FirstName, 
    LastName = @LastName, 
    Gender = @Gender,
    LastActive = @LastActive
WHERE Id = @id;";
            var parameters = new DynamicParameters();
            parameters.Add("Id", user.Id, DbType.Int32);
            parameters.Add("DisplayName", user.DisplayName, DbType.String);
            parameters.Add("FirstName", user.FirstName, DbType.String);
            parameters.Add("LastName", user.LastName, DbType.String);
            parameters.Add("Gender", user.Gender, DbType.String);
            parameters.Add("LastActive", DateTime.UtcNow, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var query = "DELETE FROM AspNetUsers WHERE Id = @id; " + 
            "DELETE FROM AspNetUserRoles WHERE UserId = @id; " +
            "DELETE FROM AspNetUserTokens WHERE UserId = @id; " +
            "DELETE FROM AspNetUserClaims WHERE UserId = @id; " +
            "DELETE FROM AspNetUserLogins WHERE UserId = @id; ";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            var query = "SELECT * FROM AspNetUsers WHERE UserName = @UserName; ";

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<MemberDto>(query, new { username });
                return user;
            }
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = "SELECT COUNT(*) AS TotalCount FROM AspNetUsers; " +
                "SELECT * FROM AspNetUsers ";

            query += userParams.OrderBy switch
            {
                "created" => " ORDER BY Created DESC ",
                "userName" => " ORDER BY UserName ASC ",
                _ => " ORDER BY LastActive DESC ",
            };

            query += " OFFSET " + ((userParams.PageNumber - 1) * userParams.PageSize) + " ROWS " +
                " FETCH NEXT " + userParams.PageSize + " ROWS ONLY; ";

            using (var connection = _context.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(query))
            {
                var count = await multi.ReadSingleOrDefaultAsync<int>();
                var members = await multi.ReadAsync<MemberDto>();

                return PagedList<MemberDto>.CreateAsync(
                members,
                count,
                userParams.PageNumber,
                userParams.PageSize);
            }
        }
    }
}
