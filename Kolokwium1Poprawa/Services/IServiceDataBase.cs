using Kolokwium1Poprawa.DTOs.Response;

namespace Kolokwium1Poprawa.Services
{
    public interface IServiceDataBase
    {
        public GetMemberResponse GetMember(int id);
        public void DeleteProject(int id);
    }
}