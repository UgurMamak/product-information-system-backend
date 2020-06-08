using Application.Bussiness.Abstract;
using Application.Core.Utilities.Results;
using Application.Core.Utilities.Security.Hashing;
using Application.Core.Utilities.Security.Jwt;
using Application.Entities.Dtos.Auth;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Concrete
{
    public class AuthService : IAuthService
    {
        private IUserService _userService;
        ITokenHelper _tokenHelper;
        public AuthService(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            /* Kullanıcı kayıt olduktan veya login olduktan token yaratılıcak. ve kullanıcı işlemlerini bu token vasıtasıyka gerçekleştirecek.
              * Aşağıdaki kodlar başarılı olma durumunda olabilecek işlemler başarısız işlem olma durumunda da burada eklemeler yapmalıyız
            */
            var claims =await _userService.GetClaims(user);//user rollerini döndürecek (Kullanıcının sahip olduğu rolleri dönecek.)           
            var accessToken = _tokenHelper.CreateToken(user, claims);//user bilgisi ve roll bilgisini token oluşturacak operasyona parametre olarak veriyoruz.
            accessToken.Role = claims;
            return  new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public async Task<IDataResult<User>> Login(LoginDto LoginDto)
        {
            var isThereUser =await _userService.GetByMail(LoginDto.Email);
            if(isThereUser==null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            //Gelen şifre haslenerek db'deki değer ile karşılaştırılır.
            if (!HashingHelper.VerifyPasswordHash(LoginDto.Password, isThereUser.PasswordHash, isThereUser.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            //return new SuccessDataResult<User>(userToCheck);/Bu şekilde de olabilir ama mesaj yazdırmak istiyorsak bu şekilde yapabiliriz.
            return new SuccessDataResult<User>(isThereUser, Messages.SuccessfulLogin);
        }

        public async Task<IDataResult<User>> Register(RegisterDto RegisterDto, string imgName)
        {
            byte[] passwordHash, passwordSalt; //işlem bitince bunlar oluşacak

            //bu çalışınca hash ve salt değerleri dönecek. bu operasyon dönünce yukarıda tanımlanan değerlerde değişecek.
            HashingHelper.CreatePasswordHash(RegisterDto.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = RegisterDto.Email,
                FirstName = RegisterDto.FirstName,
                LastName = RegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                ImageName = imgName,
                RoleId=Convert.ToInt32(RegisterDto.Role)
            };
             await _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);

            /*
            var registerSave = new SuccessDataResult<User>(user, Messages.UserRegistered);
            _userService.AddUserRole(userForRegisterDto, registerSave.Data.Id);//kayıt işleminden sonra role tablosuna kaydetme işlemine geçer.
            //return new SuccessResult(registerSave.Message);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
            */

        }

        public async Task<IResult> UserExists(string email)
        {
            if (await _userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);//eğer kullanıcı varsa ErrorDataResult döndüreceğiz.
            }
            return new SuccessResult();
        }
    }
}
