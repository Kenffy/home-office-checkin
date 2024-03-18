using AutoMapper;
using HomeOfficeCheckin.Models.DTOs;
using HomeOfficeCheckin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeOfficeCheckin.Services.IServices;

namespace HomeOfficeCheckin.Controllers
{
    /// <summary>
    /// Controller responsible for managing home office time entries.
    /// </summary>
    [Route("api/checkin")]
    [ApiController]
    public class CheckinController : ControllerBase
    {
        private readonly ICheckinService _checkinService;
        private readonly IEmailService _emailService;
        private ResponseDTO _response;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckinController"/> class.
        /// </summary>
        /// <param name="checkinService">The home office service.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public CheckinController(ICheckinService checkinService, IEmailService emailService, IMapper mapper)
        {
            _checkinService = checkinService;
            _emailService = emailService;
            _mapper = mapper;
            _response = new ResponseDTO();
        }

        /// <summary>
        /// Retrieves home office times for a specific user and date.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="day">The date in format "yyyy-MM-dd".</param>
        [HttpGet("{id}/{day}")]
        public async Task<ActionResult<ResponseDTO>> GetHomeOfficeTimesByUserIdAndDay(string id, string day)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var date = DateTime.Parse(day);
                var homeOfficeTime = await _checkinService.GetHomeOfficeTimesByUserIdAndDaysAsync(id, date);
                if (homeOfficeTime == null)
                {
                    _response.Message = "Home office time found.";
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                else
                {
                    _response.Result = homeOfficeTime;
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

        /// <summary>
        /// Retrieves the current home office time for a specific user.
        /// </summary>
        /// <param name="id">The user ID.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetCurrentHomeOfficeTime(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var homeOfficeTime = await _checkinService.GetCurrentOpenHomeOfficeTimeAsync(id);
                if (homeOfficeTime == null)
                {
                    _response.Message = "Home office time found.";
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                else
                {
                    _response.Result = homeOfficeTime;
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

        /// <summary>
        /// Starts a new home office time entry.
        /// </summary>
        /// <param name="homeOfficeTimeDto">The DTO containing home office time information.</param>
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> StartHomeOfficeTime([FromBody] HomeOfficeTimeDTO homeOfficeTimeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (homeOfficeTimeDto == null)
            {
                return BadRequest(homeOfficeTimeDto);
            }


            try
            {
                var homeOfficeTimeExists = await _checkinService.GetCurrentOpenHomeOfficeTimesByUserIdAsync(homeOfficeTimeDto.UserId, homeOfficeTimeDto.CreatedAt);
                if (homeOfficeTimeExists != null)
                {
                    _response.Message = "Home office time already started.";
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var homeOfficeTime = _mapper.Map<HomeOfficeTime>(homeOfficeTimeDto);

                await _checkinService.StartHomeOfficeAsync(homeOfficeTime);
                _response.Result = _mapper.Map<HomeOfficeTimeDTO>(homeOfficeTime);
                _response.IsSuccess = true;
                _response.Message = "Home office time started";
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
        /// Stops an ongoing home office time entry.
        /// </summary>
        /// <param name="id">The ID of the home office time entry.</param>
        /// <param name="homeOfficeTimeDto">The DTO containing home office time information.</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO>> StopHomeOfficeTime(int id, [FromBody] HomeOfficeTimeDTO homeOfficeTimeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (homeOfficeTimeDto == null || homeOfficeTimeDto.Id != id)
            {
                _response.Message = "Bad request!";
                _response.IsSuccess = false;
                return BadRequest(homeOfficeTimeDto);
            }


            try
            {
                var homeOfficeTime = _mapper.Map<HomeOfficeTime>(homeOfficeTimeDto);

                await _checkinService.StopHomeOfficeAsync(homeOfficeTime);
                // Send an email to personal office
                await _emailService.SendEmailToHRAsync(homeOfficeTime);

                _response.IsSuccess = true;
                _response.Message = "Home office time stopped.";
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
