using AutoMapper;
using LMSPO.CoreBusiness.Entities;
using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;
using LMSPO.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSPO.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly ICreateGroupUC _createGroupUC;
        private IMapper _mapper;

        public GroupsController(ICreateGroupUC createGroupUC, IMapper mapper)
        {
            _createGroupUC = createGroupUC ?? throw new ArgumentNullException(nameof(createGroupUC));
            _mapper = mapper;
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

            //return CreatedAtAction(nameof(GetGroup), new { customerId, groupId = createdGroup.GroupId }, createdGroup);
            return Ok(_mapper.Map<GroupDto>(createdGroup));
        }

    }
}
