namespace LMSPO.UseCase.GroupUCs.GroupUCInterfaces
{
    public interface IDeleteSelectedGroupProductsUC
    {
        Task<bool> ExecuteAsync(int groupId, List<int> selectedGroupProductIds);
    }
}