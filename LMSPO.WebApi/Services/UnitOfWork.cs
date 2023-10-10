using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;

namespace LMSPO.WebApi.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IGetGroupByIdAndCustomerIdUC getGroupByIdAndCustomerIdUC, 
            IGetAllGroupsByCustomerIdUC getAllGroupsByCustomerIdUC, 
            ICreateGroupUC createGroupUC, 
            IAddGroupProductsToGroupUC addGroupProductsToGroupUC, 
            IDeleteGroupByIdAndCustomerIdUC deleteGroupByIdAndCustomerIdUC, 
            IDeleteSelectedGroupProductsUC deleteSelectedGroupProductsUC )
        {
            GetGroupByIdAndCustomerIdUC = getGroupByIdAndCustomerIdUC ?? throw new ArgumentNullException(nameof(getGroupByIdAndCustomerIdUC));
            GetAllGroupsByCustomerIdUC = getAllGroupsByCustomerIdUC ?? throw new ArgumentNullException(nameof(getAllGroupsByCustomerIdUC));
            CreateGroupUC = createGroupUC ?? throw new ArgumentNullException(nameof(createGroupUC));
            AddGroupProductsToGroupUC = addGroupProductsToGroupUC ?? throw new ArgumentNullException(nameof(addGroupProductsToGroupUC));
            DeleteGroupByIdAndCustomerIdUC = deleteGroupByIdAndCustomerIdUC ?? throw new ArgumentNullException(nameof(deleteGroupByIdAndCustomerIdUC));
            DeleteSelectedGroupProductsUC = deleteSelectedGroupProductsUC ?? throw new ArgumentNullException(nameof(deleteSelectedGroupProductsUC));
        }

        public IGetGroupByIdAndCustomerIdUC GetGroupByIdAndCustomerIdUC { get; }

        public IGetAllGroupsByCustomerIdUC GetAllGroupsByCustomerIdUC { get; }

        public ICreateGroupUC CreateGroupUC { get; }

        public IAddGroupProductsToGroupUC AddGroupProductsToGroupUC { get; }

        public IDeleteGroupByIdAndCustomerIdUC DeleteGroupByIdAndCustomerIdUC { get; }

        public IDeleteSelectedGroupProductsUC DeleteSelectedGroupProductsUC { get; }
    }
}
