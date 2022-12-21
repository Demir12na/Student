using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models.Student;

namespace StudentManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        public IUnitOfWork _unitOfWork { get; }

        public StudentController(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("/api/students")]
        public async Task<IEnumerable<StudentGetRes>> GetList([FromQuery] StudentGetReq req)
        {
            IEnumerable<StudentGetRes> res = await _unitOfWork.Students.GetListAsync(req);

            return res;
        }

        [HttpPost]
        [Route("/api/students")]
        public async Task<ActionResult> Create([FromBody] StudentCreateReq req)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(req.FirstName))
                {
                    throw new InvalidOperationException("First name can not be empty or null");
                }

                if (string.IsNullOrWhiteSpace(req.LastName))
                {
                    throw new InvalidOperationException("Last name can not be empty or null");
                }

                if (string.IsNullOrWhiteSpace(req.EmailAddress))
                {
                    throw new InvalidOperationException("E-mail address can not be empty or null");
                }

                if (req.Departments.Id == null)
                {
                    throw new InvalidOperationException("Department can not be null");
                }

                await _unitOfWork.Students.CreateAsync(req);

                return Ok(req);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("/api/students/get-update")]
        public async Task<ActionResult<StudentGetUpdateRes>> GetUpdate([FromQuery] StudentGetUpdateReq req)
        {
            try
            {
                if (req.Id == null)
                {
                    throw new InvalidOperationException("StudentId can not be null");
                }

                StudentGetUpdateRes res = await _unitOfWork.Students.GetUpdateAsync(req);

                return res;

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Route("/api/students")]
        public async Task<ActionResult> Update([FromBody] StudentUpdateReq req)
        {
            try
            {
                if (req.Id == null)
                {
                    throw new InvalidOperationException("StudentId can not be null");
                }

                await _unitOfWork.Students.UpdateAsync(req);

                return Ok(req);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("/api/students")]
        public async Task<ActionResult> DeleteAsync([FromQuery] StudentDeleteReq req)
        {
            try
            {
                if (req.Id == null)
                {
                    throw new InvalidOperationException("StudentId can not be null");
                }

                await _unitOfWork.Students.DeleteAsync(req);

                return Ok(req);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/students/departments")]
        public async Task<ActionResult<IEnumerable<DepartmentGetRes>>> GetDepartments()
        {
            try
            {
                IEnumerable<DepartmentGetRes> res = await _unitOfWork.Students.GetDepartmentsAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("/api/students/assign-group")]
        public async Task<ActionResult> AssignGroup([FromBody] StudentAssignGroupReq req)
        {
            try
            {
                if (req.Id == null)
                {
                    throw new InvalidOperationException("StudentId can not be null");
                }

                if (req.GroupId == null)
                {
                    throw new InvalidOperationException("GroupId can not be null");
                }

                await _unitOfWork.Students.AssignGroupAsync(req);

                return Ok(req);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/students/get-group")]
        public async Task<ActionResult<StudentGetGroupByStudentIdRes>> GetGroupByStudentId([FromQuery] StudentGetGroupByStudentIdReq req)
        {
            try
            {
                if (req.Id == null)
                {
                    throw new InvalidOperationException("StudentId can not be null");
                }

                StudentGetGroupByStudentIdRes res = await _unitOfWork.Students.GetGroupByStudentIdAsync(req);

                return res;

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}