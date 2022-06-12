using ETrade.Business.Abstract;
using ETrade.Business.Abstract.AuthenticationAndAuthorization;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Core.Entities.Abstract;
using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.Core.Utilities.Security;
using ETrade.Core.Utilities.Security.Helpers;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete.AuthenticationAndAuthorization
{
    public class AuthorizationManager : IAuthorizationService
    {
        protected readonly IUserService _userService;
        protected readonly ITokenHelper _tokenHelper;

        public AuthorizationManager(IUserService userService,
            ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }


        public IDataResult<ObjectDto<AccessToken>> CreateAccessToken(User user)
        {
            var operationClaimsResult = _userService.GetUsersClaimsById(user.Id);
            var operationClaims = operationClaimsResult.Data.Entities;

            var token = _tokenHelper.CreateAccessToken(user, operationClaims);

            var objectDto = new ObjectDto<AccessToken>
            {
                Entity = token,
                ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
            };

            return new SuccessfulDataResult<ObjectDto<AccessToken>>(objectDto, BusinessMessages.TokenCreatedForUser, BusinessTitles.Successful);
        }

        public IResult Login(UserForLoginDto userForLoginDto)
        {
            var user = _userService.GetUserByEmail(userForLoginDto.Email);
            var logicResut0 = BusinessLogicEngine.Run
                (CheckIfUserExists(userForLoginDto.Email));
            if (logicResut0 == null)
            {
                var logicResult1 = 
                    BusinessLogicEngine.Run
                    (CheckIfUserVerificationSuccessful(userForLoginDto.Email),
                     CheckIfUserPasswordVerified(userForLoginDto.Password, user.Data.Entity.PasswordHash, user.Data.Entity.PasswordSalt));
                if (logicResult1 != null)
                {
                    return logicResult1;
                }
            }
            else
            {
                return logicResut0;
            }
            return CheckObjectReturnValue<User>(user.Data.Entity, BusinessMessages.UserLoggedIn, BusinessMessages.UserNotLoggedIn);
        }

        public IResult Register(UserForRegisterDto userForRegisterDto)
        {

            var logicResult = 
                BusinessLogicEngine.Run
                (CheckIfUserAddedBefore(userForRegisterDto.Email));

            if (logicResult != null)
            {
                return logicResult;
            }
            
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                RoleId = 3,
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                ContactNumber = userForRegisterDto.ContactNumber,
                
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userService.Add(user);
            return CheckObjectReturnValue(user, BusinessMessages.UserRegistered, BusinessMessages.UserNotRegistered);
        }



        //Business Utilities

        private IDataResult<ObjectDto<User>> CheckObjectReturnValue(User user, string successMessage, string unSuccessMessage)
        {

            if (user != null)
            {
                ObjectDto<User> objectDto = new ObjectDto<User>
                {
                    Entity = user,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<User>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<User>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectDto<T>> CheckObjectReturnValue<T>(T entity, string successMessage, string unSuccessMessage)
        where T : class, new()
        {

            if (entity != null)
            {
                ObjectDto<T> objectDto = new ObjectDto<T>
                {
                    Entity = entity,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<T>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<T>>(unSuccessMessage, BusinessTitles.Warning);
        }


        private IDataResult<ObjectQueryableDto<User>> CheckObjectsReturnValue(IQueryable<User> users, string successMessage, string unSuccessMessage)
        {
            if (users.Count() > -1)
            {
                ObjectQueryableDto<User> objectQueryableDto = new ObjectQueryableDto<User>
                {
                    Entities = users,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<User>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<User>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<T>> CheckObjectsGenericReturnValue<T>(IQueryable<T> tEntities, string successMessage, string unSuccessMessage)
        where T : class, IEntity, new()
        {
            if (tEntities.Count() > -1)
            {
                ObjectQueryableDto<T> objectQueryableDto = new ObjectQueryableDto<T>
                {
                    Entities = tEntities,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<T>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<T>>(unSuccessMessage, BusinessTitles.Warning);
        }




        //Business Rules
        
        public IResult CheckIfUserVerificationSuccessful(string email)
        {
            var userResult = _userService.GetUserByEmail(email);

            if (!userResult.Data.Entity.IsVerificated)
            {
                return new UnSuccessfulResult(BusinessMessages.UserNotVerificated, BusinessTitles.Error);
            }
            return new SuccessfulResult();
        }

        public IResult CheckIfUserPasswordVerified(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
            {
                return new UnSuccessfulResult(BusinessMessages.UserPasswordNotVerified, BusinessTitles.Error);
            }
            return new SuccessfulResult();
        }

        private IResult CheckIfUserAddedBefore(string email)
        {
            bool status = false;
            var users = _userService.GetUsers();
            foreach (var item in users.Data.Entities)
            {
                if (item.Email.Trim().ToLower() == email.Trim().ToLower())
                {
                    status = true;
                }
            }


            return status == true
                ? new UnSuccessfulResult(BusinessMessages.UserExists, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

        private IResult CheckIfUserExists(string email)
        {
            var users = _userService.GetUsers();
            foreach (var item in users.Data.Entities)
            {
                if (item.Email.Trim().ToLower() == email.Trim().ToLower())
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.UserNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfUserNull(User user)
        {
            return user == null
                ? new UnSuccessfulResult(BusinessMessages.UserNotFound, BusinessTitles.Warning)
                : new SuccessfulResult();
        }
    }
}
