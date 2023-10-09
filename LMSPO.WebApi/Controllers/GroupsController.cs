using AutoMapper;
using LMSPO.CoreBusiness.Entities;
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
        private readonly ILogger<GroupsController> _logger;
        private IMapper _mapper;

        public GroupsController(IGetGroupByIdAndCustomerIdUC getGroupByIdAndCustomerIdUC,
            IGetAllGroupsByCustomerIdUC getAllGroupsByCustomerIdUC,
            ICreateGroupUC createGroupUC,
            ILogger<GroupsController> logger,
            IMapper mapper
            )
        {
            _getGroupByIdAndCustomerIdUC = getGroupByIdAndCustomerIdUC ?? throw new ArgumentNullException(nameof(getGroupByIdAndCustomerIdUC));
            _getAllGroupsByCustomerIdUC = getAllGroupsByCustomerIdUC ?? throw new ArgumentNullException(nameof(getAllGroupsByCustomerIdUC));
            _createGroupUC = createGroupUC ?? throw new ArgumentNullException(nameof(createGroupUC));
            _logger =logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }
        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetGroups(int customerId)
        {
            IEnumerable<Group>? groups = await _getAllGroupsByCustomerIdUC.ExcecuteAsync(customerId);


            if (groups == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<GroupDto>>(groups));
        }

        [HttpGet("{customerId:int}/{groupId:int}")]
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

    }
}
