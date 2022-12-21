using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    internal class GroupController : ControllerBase
    {
        public IUnitOfWork _unitOfWork { get; }

        public GroupController(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("/api/groups")]
        public async Task<IEnumerable<GroupGetRes>> GetList()
        {
            IEnumerable<GroupGetRes> res = await _unitOfWork.Groups.GetListAsync();

            return res;
        }

        [HttpPost]
        [Route("/api/groups")]
        public async Task<ActionResult> Create([FromBody] GroupCreateReq req)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(req.Name))
                {
                    throw new InvalidOperationException("Name can not be empty or null");
                }

                await _unitOfWork.Groups.CreateAsync(req);

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
        [Route("/api/groups/get-update")]
        public async Task<ActionResult<GroupGetUpdateRes>> GetUpdate([FromQuery] GroupGetUpdateReq req)
        {
            try
            {
                GroupGetUpdateRes res = await _unitOfWork.Groups.GetUpdateAsync(req);

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
        [Route("/api/groups")]
        public async Task<ActionResult> Update([FromBody] GroupUpdateReq req)
        {
            try
            {
                await _unitOfWork.Groups.UpdateAsync(req);

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
        [Route("/api/groups")]
        public async Task<ActionResult> DeleteAsync([FromQuery] GroupDeleteReq req)
        {
            try
            {
                await _unitOfWork.Groups.DeleteAsync(req);

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

    }
}