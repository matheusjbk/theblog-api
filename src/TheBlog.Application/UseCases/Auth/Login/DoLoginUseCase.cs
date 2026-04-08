using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Errors;
using TheBlog.Domain.Primitives;
using TheBlog.Domain.Repositories;
using TheBlog.Domain.Security.Cryptography;
using TheBlog.Domain.Security.Tokens;

namespace TheBlog.Application.UseCases.Auth.Login;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserRepository _userRepository;
    IPasswordEncrypter _passwordEncrypter;
    IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(IUserRepository userRepository, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accessTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordEncrypter = passwordEncrypter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResultValue<LoginResponse>> Execute(LoginRequest request)
    {
        var user = await _userRepository.GetByEmail(request.Email);

        if (user is null || !_passwordEncrypter.IsValid(request.Password, user.Password)) return ResultValue<LoginResponse>.Failure(new UnauthorizedError(""));

        var loginResponse = new LoginResponse
        {
            AccessToken = _accessTokenGenerator.GenerateToken(user)
        };

        return ResultValue<LoginResponse>.Success(loginResponse);
    }
}
