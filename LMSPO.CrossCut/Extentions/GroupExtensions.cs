using LMSPO.CoreBusiness.Entities;
using LMSPO.CrossCut.Dtos;

namespace LMSPO.CrossCut.Extentions
{
    public static class GroupExtensions
    {
        public static GroupDto ToDto(this Group group)
        {
            if (group != null)
            {
                return new GroupDto
                {
                    GroupId = group.GroupId,
                    GroupName = group.GroupName,
                    EAN = group.EAN,
                    CustomerId = group.CustomerId
                };
            }
            return new GroupDto();
        }
    }
}
