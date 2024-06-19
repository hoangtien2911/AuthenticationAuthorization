using EquipmentsBusiness.Base;
using EquipmentsBusiness.Request;
using EquipmentsBusiness.Response;
using EquipmentsRepo.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EquipmentsBusiness;

public class AccountBusiness
{
    private readonly IAccountRepo _accountRepo;
    private readonly IConfiguration _config;

    public AccountBusiness(IAccountRepo accountRepo, IConfiguration config)
    {
        _accountRepo = accountRepo;
        _config = config;
    }

    public IBusinessResult AuthenticateAccount(LoginRequest request)
    {
        var account = _accountRepo.GetByUsernamePassword(request.Username, request.Password);
        if (account == null)
        {
            return new BusinessResult(false, "Email or Password not correct!");
        }

        var token = GenerateJSONWebToken(account.UserName, account.Email, account.RoleId);
        var loginResponse = new LoginResponse
        {
            Email = account.Email,
            Token = token,
            Role = GetRoleById(account.RoleId)
        };
        return new BusinessResult(true, loginResponse);
    }

    private string GenerateJSONWebToken(string username, string email, int roleId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Audience"],
          new Claim[]
          {
                  new("username", username),
                  new(ClaimTypes.Email, email),
                  new(ClaimTypes.Role, GetRoleById(roleId)),
          },
          expires: DateTime.Now.AddMinutes(30),
          signingCredentials: credentials
          );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GetRoleById(int id)
    {
        var roleName = id switch
        {
            1 => "Admin",
            2 => "Staff",
            3 => "Manager",
            _ => "User"
        };

        return roleName;
    }

}