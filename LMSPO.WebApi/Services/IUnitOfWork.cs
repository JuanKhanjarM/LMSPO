using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;

namespace LMSPO.WebApi.Services
{
    public interface IUnitOfWork
    {
        IGetGroupByIdAndCustomerIdUC GetGroupByIdAndCustomerIdUC { get; }
        IGetAllGroupsByCustomerIdUC GetAllGroupsByCustomerIdUC { get; }
        ICreateGroupUC CreateGroupUC { get; }
        IAddGroupProductsToGroupUC AddGroupProductsToGroupUC { get; }
        IDeleteGroupByIdAndCustomerIdUC DeleteGroupByIdAndCustomerIdUC { get; }
        IDeleteSelectedGroupProductsUC DeleteSelectedGroupProductsUC { get; }
    }
}
