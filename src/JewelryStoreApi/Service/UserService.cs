using JewelryStoreApi.Model;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStoreApi.Service
{
    public class UserService : IUserService
    {
        private IRepository _userRepository;
        public UserService(IRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidateResult Validate(string basicAuthDetail)
        {
            var requestUser = DecodeBasicAuthToken(basicAuthDetail);

            var users = Task.Run(async () => await _userRepository.GetUsers()).Result;

            var foundUser = users.FirstOrDefault(user => string.Compare(user.Username, requestUser?.Username, StringComparison.OrdinalIgnoreCase) == 0 &&
                                user.Password == requestUser?.Password);

            if (foundUser != null)
            {
                return new ValidateResult { UserType = foundUser.UserType.ToString(), Valid = true };
            }

            return new ValidateResult { UserType = UserType.NotFound.ToString(), Valid = false };
        }

        private User DecodeBasicAuthToken(string encodedAuthDetail)
        {
            var basicAuth = encodedAuthDetail.Split("Basic ");

            if (basicAuth.Length <= 1)
            {
                return null;
            }

            string authToken = basicAuth[1];

            var authDetail = Encoding.UTF8.GetString(Convert.FromBase64String(authToken)).Split(":");

            return new User { Username = authDetail[0], Password = authDetail[1] };

        }

    }

}
