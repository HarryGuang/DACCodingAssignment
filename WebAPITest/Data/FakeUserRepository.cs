using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPITest.Data
{
    public class FakeUserRepository : IUserRepository
    {
        private static List<AppUser> _data = new List<AppUser>();

        public FakeUserRepository(List<AppUser> data) 
        {
            _data = data;
        }

        public async Task DeleteUserAsync(int id)
        {
            await Task.Run(async () => { _data = _data.Where(x => x.Id != id).ToList(); });            
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            MemberDto user =
            await Task.FromResult(
                _data
                   .Where(x => x.UserName == username)
                   .Select(x => new MemberDto
                   {
                       Id = x.Id,
                       UserName = x.UserName,
                       DisplayName = x.DisplayName,
                       FirstName = x.FirstName,
                       LastName = x.LastName,
                       Gender = x.Gender,
                       Created = x.Created,
                       LastActive = x.LastActive
                   })
                   .SingleOrDefault());

            return user;
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            List<MemberDto> members = new List<MemberDto>();
            int count = 0;
            IEnumerable<MemberDto> query = null;

            await Task.Run(async () => {
                count = _data.Count;
                query = _data.Select(x => new MemberDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    DisplayName = x.DisplayName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gender = x.Gender,
                    Created = x.Created,
                    LastActive = x.LastActive
                }).ToList();
                query = userParams.OrderBy switch
                {
                    "created" => query.OrderByDescending(u => u.Created),
                    "userName" => query.OrderBy(u => u.UserName),
                    _ => query.OrderByDescending(u => u.LastActive)
                };
                query = query.Skip((userParams.PageNumber - 1) * userParams.PageSize)
                .Take(userParams.PageSize);
            });

            return PagedList<MemberDto>.CreateAsync(
                query,
                count,
                userParams.PageNumber,
                userParams.PageSize);
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            AppUser user = null;
            await Task.Run(async () => {
                user = _data.Where(x => x.Id != id).FirstOrDefault();                
            });
            return user;
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            AppUser user = null;
            await Task.Run(async () =>
            {
                user = _data.Where(x => x.UserName != username).FirstOrDefault();
            });
            return user;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            IEnumerable<AppUser> rtn = null;
            await Task.Run(async () =>
            {
                rtn = _data.ToList();
            });
            return rtn;
        }

        public async Task UpdateUserAsync(AppUser user)
        {
            await Task.Run(async () =>
            {
                var rtn = _data.Where(x => x.Id != user.Id).FirstOrDefault();
                if (rtn != null) { 
                    rtn.UserName = user.UserName;
                    rtn.Gender = user.Gender;
                    rtn.DisplayName = user.DisplayName;
                    rtn.FirstName = user.FirstName;
                    rtn.LastName = user.LastName;
                }
            });
        }
    }
}
