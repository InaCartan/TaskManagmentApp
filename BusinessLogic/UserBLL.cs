using DTO;
using DataAccess.Repositories;

namespace BusinessLogic
{
    public class UserBLL
    {
        private UserRepository userRepo;

        public UserBLL(UserRepository userR)
        {
            userRepo = userR;
        }

        public List<UserDto> GetAllUsers()
        {
            return userRepo.GetAllUsers();
        }

        public UserDto GetUserById(int id)
        {
            return userRepo.GetUserById(id);
        }

        public UserDto GetUserByEmail(string email)
        {
            return userRepo.GetUserByEmail(email);
        }

        public void AddUser(UserDto user)
        {
            userRepo.AddUser(user);
        }

        public void UpdateUser(UserDto user)
        {
            userRepo.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            userRepo.DeleteUser(id);
        }
    }
}
