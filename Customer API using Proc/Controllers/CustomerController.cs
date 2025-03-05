using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using University.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.PortableExecutable;
using Company.DTO;
using Company.Models;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly DBContext _context;
    private readonly DBContextProcedures _procedures;
    private readonly IConfiguration _config;

    public CustomerController(DBContext context, DBContextProcedures procedures, IConfiguration config)
    {
        _context = context;
        _procedures = procedures;
        _config = config;
    }


    [HttpPost("AddCustomer")]
    public async Task<IActionResult> AddCustomer([FromForm] RegisterDTO request)
    {
        var returnValue = new OutputParameter<int>();
        var result = await _procedures.AddCustomerAsync(request.Name, request.Salary, request.Password, request.DeptId, returnValue);

        if (returnValue.Value == -1)
            return BadRequest("Failed to add customer.");

        return Ok(new { Message = $"Customer added successfully", Data = result });
    }


    [HttpPut("update")]
    public async Task<IActionResult> UpdateCustomer([FromForm] UpdateCustomerDTO model, CancellationToken cancellationToken)
    {
        var returnValue = new OutputParameter<int>();

        var result = await _procedures.UpdateCustomerAsync(
            model.Id,
            model.Name,
            model.Salary,
            model.Password,
            model.DeptId,
            returnValue,
            cancellationToken
        );

        if (returnValue.Value == -1)
        {
            return NotFound(new { Message = "Customer not found." });
        }

        return Ok(new
        {
            Message = "Customer updated successfully.",
        });
    }

    
    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteCustomer([FromForm] int CustomerId, CancellationToken cancellationToken)
    {
        var returnValue = new OutputParameter<int>();

        var result = await _procedures.DeleteCustomerAsync(
            CustomerId,
            returnValue,
            cancellationToken
        );

        if (returnValue.Value == -1)
        {
            return NotFound(new { Message = "Customer not found." });
        }

        return Ok(new
        {
            Message = "Customer deleted successfully.",
        });
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromForm] LoginDto request)
    {
        var returnValue = new OutputParameter<int>();

        List<GetCustomerLoginResult> result = await _procedures.GetCustomerLoginAsync(request.UserName, request.Password, returnValue);

        if (result == null || !result.Any())
        {
            return Unauthorized("Invalid UserName or Password.");
        }
        else
        {
            var customerId = result.Select(x => x.CustomerId).FirstOrDefault();
            var name = result.Select(x => x.Name).FirstOrDefault();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

            var myToken = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudiance"],
                expires: DateTime.Now.AddDays(double.Parse(_config["JWT:DurationInDay"])),
                claims: claims,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            var response = new
            {
                customerId,
                name,
                token = new JwtSecurityTokenHandler().WriteToken(myToken),
                expiration = myToken.ValidTo
            };

            return Ok(response);
        }
    }


    [Authorize]
    [HttpPost("AddDepartment")]
    public async Task<IActionResult> AddDepartment([FromForm] DepartmentName input, CancellationToken cancellationToken)
    {
        if (input == null || string.IsNullOrWhiteSpace(input.Name))
        {
            return BadRequest("Department name is required.");
        }

        var returnValue = new OutputParameter<int>();
        var result = await _procedures.AddDepartmentAsync(input.Name, returnValue, cancellationToken);

        var dept = result.FirstOrDefault();
        if (returnValue.Value == -1)
        {
            return Conflict("Department already exists.");
        }

        return Ok(new { Message = $"Department: {dept.Name} added successfully" });
    }


    [Authorize]
    [HttpPost("AddProject")]
    public async Task<IActionResult> AddProject([FromForm] ProjectDto request)
    {
        var returnValue = new OutputParameter<int>();
        var result = await _procedures.AddProjectAsync(request.Name, request.Description, returnValue);

        if (returnValue.Value != 0)
            return BadRequest("Failed to add project.");

        return Ok(new { Message = $"Project added successfully", Data = result });
    }


    [Authorize]
    [HttpPost("AddCustomerToProject")]
    public async Task<IActionResult> AddCustomerToProject([FromForm] CustomerProjectDTO request)
    {
        try
        {
            var actualReturnValue = new OutputParameter<int?>();
            var returnValue = new OutputParameter<int>();

            var result = await _procedures.AddCustomerToProjectAsync(
                request.CustomerId,
                request.ProjectId,
                actualReturnValue,
                returnValue
            );

            if (actualReturnValue.Value == -1)
            {
                return BadRequest(new { Message = "The customer is already assigned to this project." });
            }

            if (actualReturnValue.Value == 0)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the customer to the project." });
            }

            if (result == null || !result.Any())
            {
                return StatusCode(500, new { Message = "No data returned from the stored procedure." });
            }

            return Ok(new
            {
                Message = "Customer added to Project successfully",
                Data = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Message = "An error occurred while processing your request.",
                Error = ex.Message
            });
        }
    }


    [Authorize]
    [HttpGet("GetAllCustomerProjectsData")]
    public async Task<IActionResult> GetAllCustomerProjectsData()
    {
        var returnValue = new OutputParameter<int>();

        List<GetAllCustomerProjectsDataResult> result = await _procedures.GetAllCustomerProjectsDataAsync(returnValue);

        if (result == null || !result.Any())
        {
            return Unauthorized("You are UnAuthorized.");
        }
        else
        {
            var response = new
            {
                result = result
            };

            return Ok(response);
        }
    }

}