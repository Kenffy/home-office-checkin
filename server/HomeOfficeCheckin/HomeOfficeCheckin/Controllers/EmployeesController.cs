using AutoMapper;
using HomeOfficeCheckin.Models.DTOs;
using HomeOfficeCheckin.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeOfficeCheckin.Controllers
{
    /// <summary>
    /// Controller for handling employee-related operations.
    /// </summary>
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private ResponseDTO _response;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeesController"/> class.
        /// </summary>
        /// <param name="employeeService">The employee service.</param>
        /// <param name="mapper">The mapper for mapping between entities and DTOs.</param>
        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _response = new ResponseDTO();
        }

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>The response containing a list of employees.</returns>
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEployeesAsync();
                _response.Result = _mapper.Map<List<EmployeeDTO>>(employees);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Gets the employee by ID.
        /// </summary>
        /// <param name="id">The employee ID.</param>
        /// <returns>The response containing the employee.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetEmployee(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var employee = await _employeeService.GetEployeeByIdAsync(id);

                if (employee == null)
                {
                    _response.Message = "Employee not found.";
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<EmployeeDTO>(employee);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
        }

        /// <summary>
        /// Logs in the employee.
        /// </summary>
        /// <param name="loginDto">The login DTO containing username and password.</param>
        /// <returns>The response containing the logged-in employee.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDTO>> Login(LoginDTO loginDto)
        {
            try
            {
                var employee = await _employeeService.GetEployeeByUserNameAsync(loginDto.Username);

                if (employee == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "User not found";
                    return BadRequest(_response);
                }

                if (employee.Password == loginDto.Password)
                {
                    _response.Result = _mapper.Map<EmployeeDTO>(employee);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Wrong username or password!";
                    return BadRequest(_response);
                }

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
        }
    }
}
