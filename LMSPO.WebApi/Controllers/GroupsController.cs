using AutoMapper;
using LMSPO.CoreBusiness.Entities;
using LMSPO.UseCase.Exceptions.GroupEX;
using LMSPO.WebApi.Dtos;
using LMSPO.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMSPO.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {

        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(IUnitOfWork iUnitOfWork, IMapper mapper, ILogger<GroupsController> logger)

        {
            _iUnitOfWork = iUnitOfWork ?? throw new ArgumentNullException(nameof(iUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{customerId:int}")]
        [ProducesResponseType(typeof(IEnumerable<GroupDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGroups(int customerId)
        {
            try
            {
                IEnumerable<Group>? groups = await _iUnitOfWork.GetAllGroupsByCustomerIdUC.ExcecuteAsync(customerId);

                if (groups == null)
                {
                    return NotFound($"A Customer with Id: {customerId} not found");
                }
                return Ok(_mapper.Map<IEnumerable<GroupDto>>(groups));
            }
            catch (Exception ex)
            {

                // Handle exceptions and return a 500 Internal Server Error response
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

        }

        [HttpGet("{customerId:int}/{groupId:int}")]
        [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGroupByCustomerIdAndGroupId(int customerId, int groupId)
        {
            try
            {
                Group? group = await _iUnitOfWork.GetGroupByIdAndCustomerIdUC.ExecuteAsync(customerId, groupId);

                if (group == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<GroupDto>(group));
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while retrieving the group.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPost("create-group/{customerId:int}")]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)] // Specify BadRequestObjectResult as an error response
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)] // Specify NotFoundObjectResult as an error response
        public async Task<IActionResult> CreateGroup(int customerId, [FromBody] GroupDto groupDto)
        {
            if (groupDto == null)
            {
                return BadRequest("Invalid group data");
            }
            Group groupToAdd = _mapper.Map<Group>(groupDto);
            Group createdGroup = await _iUnitOfWork.CreateGroupUC.ExcecuteAsync(customerId, groupToAdd);

            if (createdGroup == null)
            {
                return BadRequest("Failed to create the group");
            }

            return CreatedAtAction(nameof(GetGroupByCustomerIdAndGroupId), new { customerId, groupId = createdGroup.GroupId }, createdGroup);
            //return Ok(_mapper.Map<GroupDto>(createdGroup));
        }

        [HttpPost("add-group-products/{groupId:int}")]
        public async Task<IActionResult> AddGroupProductsToGroup(int groupId, [FromBody] List<GroupProductDto> groupProducts)
        {
            if (groupProducts == null)
            {
                return BadRequest("Invalid group data");
            }
            try
            {

                bool result = await _iUnitOfWork.AddGroupProductsToGroupUC.ExecuteAsync(groupId, _mapper.Map<List<GroupProduct>>(groupProducts));

                if (result)
                {
                    return Ok("GroupProducts added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add GroupProducts to the group.");
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding GroupProducts to a group: {GroupId}", groupId);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("{customerId:int}/{groupId:int}")]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        //http://localhost:5047/api/Groups/1/1004
        public async Task<IActionResult> DeleteGroupByCustomerIdAndGroupId(int customerId, int groupId)
        {
            try
            {
                bool deleted = await _iUnitOfWork.DeleteGroupByIdAndCustomerIdUC.ExecuteAsync(customerId, groupId);

                if (!deleted)
                {
                    return NotFound("Group not found.");
                }

                return NoContent(); // 204 No Content status for a successful delete
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while deleting the group.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE api/groups/{groupId}/delete-selected
        [HttpDelete("{groupId:int}/delete-selected")]
        public async Task<IActionResult> DeleteSelectedGroupProducts(int groupId, [FromBody] List<int> selectedGroupProductIds)
        {
            try
            {
                bool deleted = await _iUnitOfWork.DeleteSelectedGroupProductsUC.ExecuteAsync(groupId, selectedGroupProductIds);

                if (!deleted)
                {
                    return NotFound("Group or group products not found or deletion failed.");
                }

                return NoContent(); // 204 No Content status for a successful delete
            }
            catch (InvalidGroupIdException ex)
            {
                // Log the invalid group ID exception
                _logger.LogError(ex, "Invalid group ID: {GroupId}", groupId);
                return BadRequest("Invalid group ID.");
            }
            catch (InvalidSelectedGroupProductIdsException ex)
            {
                // Log the invalid selected group product IDs exception
                _logger.LogError(ex, "Invalid selected group product IDs: {SelectedGroupProductIds}", selectedGroupProductIds);
                return BadRequest("Invalid selected group product IDs.");
            }
            catch (Exception ex)
            {
                // Log other exceptions
                _logger.LogError(ex, "An error occurred while deleting group products for Group: {GroupId}", groupId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
