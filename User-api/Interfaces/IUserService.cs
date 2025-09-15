using UserApi.Api.Models;
using System.Collections.Generic;

namespace UserApi.Api.Services
{
    public interface IUserService
    {
        List<User> Get();
        User Create(User user);
    }
}
