using LMSPO.CoreBusiness.Entities;
using LMSPO.CrossCut.Dtos;

namespace LMSPO.UseCase.GroupUCs.GroupUCInterfaces
{
    public interface ICreateGroupUC
    {
        Task<GroupDto?> ExcecuteAsync(int customerId, GroupDto group);
    }
}