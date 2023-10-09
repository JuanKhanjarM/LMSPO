using AutoMapper;
using LMSPO.CoreBusiness.Entities;
using LMSPO.UseCase.Exceptions.GroupEX;
using LMSPO.UseCase.GroupUCs;
using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;
using LMSPO.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LMSPO.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        
        private readonly IGetGroupByIdAndCustomerIdUC _getGroupByIdAndCustomerIdUC;
        private readonly IGetAllGroupsByCustomerIdUC _getAllGroupsByCustomerIdUC;
        private readonly ICreateGroupUC _createGroupUC;
        private readonly IDeleteGroupByIdAndCustomerIdUC _deleteGroupByIdAndCustomerIdUC;
        private readonly IDeleteSelectedGroupProductsUC  _deleteSelectedGroupProductsUC;
        private readonly ILogger<GroupsController> _logger;
        private IMapper _mapper;

        public GroupsController(IGetGroupByIdAndCustomerIdUC getGroupByIdAndCustomerIdUC,
            IGetAllGroupsByCustomerIdUC getAllGroupsByCustomerIdUC,
            ICreateGroupUC createGroupUC,
            IDeleteGroupByIdAndCustomerIdUC deleteGroupByIdAndCustomerIdUC,
            IDeleteSelectedGroupProductsUC deleteSelectedGroupProductsUC,
            ILogger<GroupsController> logger,
            IMapper mapper
            )
        {
            _getGroupByIdAndCustomerIdUC = getGroupByIdAndCustomerIdUC ?? throw new ArgumentNullException(nameof(getGroupByIdAndCustomerIdUC));
            _getAllGroupsByCustomerIdUC = getAllGroupsByCustomerIdUC ?? throw new ArgumentNullException(nameof(getAllGroupsByCustomerIdUC));
            _createGroupUC = createGroupUC ?? throw new ArgumentNullException(nameof(createGroupUC));
            _deleteGroupByIdAndCustomerIdUC = deleteGroupByIdAndCustomerIdUC ?? throw new ArgumentNullException(nameof(deleteGroupByIdAndCustomerIdUC));
            _deleteSelectedGroupProductsUC = deleteSelectedGroupProductsUC ?? throw new ArgumentNullException(nameof(deleteSelectedGroupProductsUC));
            _logger =logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        [HttpGet("{customerId:int}")]
        [ProducesResponseType(typeof(IEnumerable<GroupDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGroups(int customerId)
        {
            try
            {
                IEnumerable<Group>? groups = await _getAllGroupsByCustomerIdUC.ExcecuteAsync(customerId);

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
        public async Task<IActionResult> GetGroupByCustomerIdAndGroupId(int customerId,int groupId)
        {
            try
            {
                Group? group = await _getGroupByIdAndCustomerIdUC.ExecuteAsync(customerId,groupId);

                if (group == null)
                {
                    return NotFound();
                }

                GroupDto groupDto = _mapper.Map<GroupDto>(group);

                return Ok(groupDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while retrieving the group.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPost("{customerId:int}")]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)] // Specify BadRequestObjectResult as an error response
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)] // Specify NotFoundObjectResult as an error response
        public async Task<IActionResult> CreateGroup(int customerId, [FromBody] GroupDto groupDto)
        {
            if (groupDto == null)
            {
                return BadRequest("Invalid group data");
            }
            Group groupToAdd = _mapper.Map<Group>(groupDto);
            Group createdGroup = await _createGroupUC.ExcecuteAsync(customerId, groupToAdd);

            if (createdGroup == null)
            {
                return BadRequest("Failed to create the group");
            }

            return CreatedAtAction(nameof(GetGroupByCustomerIdAndGroupId), new { customerId, groupId = createdGroup.GroupId }, createdGroup);
            //return Ok(_mapper.Map<GroupDto>(createdGroup));
        }

        [HttpDelete("{customerId:int}/{groupId:int}")]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        //http://localhost:5047/api/Groups/1/1004
        public async Task<IActionResult> DeleteGroupByCustomerIdAndGroupId(int customerId, int groupId)
        {
            try
            {
                bool deleted = await _deleteGroupByIdAndCustomerIdUC.ExecuteAsync(customerId,groupId);

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
                bool deleted = await _deleteSelectedGroupProductsUC.ExecuteAsync(groupId, selectedGroupProductIds);

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
