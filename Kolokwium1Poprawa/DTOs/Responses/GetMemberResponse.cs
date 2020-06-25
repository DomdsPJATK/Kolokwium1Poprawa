using System.Collections.Generic;

namespace Kolokwium1Poprawa.DTOs.Response
{
    public class GetMemberResponse
    {
        public int IdTeamMember { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<GetTaskMemberResponse> Tasks { get; set; }
    }
}