namespace LMSPO.UseCase.GroupUCs.GroupUCInterfaces
{
    public interface IDeleteGroupByIdAndCustomerIdUC
    {
        Task<bool> ExecuteAsync(int customerId, int groupId);
    }
}