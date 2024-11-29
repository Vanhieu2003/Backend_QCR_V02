using Microsoft.AspNetCore.Mvc;
using Project.Dto;
using Project.Entities;

namespace Project.Interface
{
    public interface IUserPerTagRepository
    {
        public Task CreateUserPerGroup(AssignUserRequest request);
    }
}
