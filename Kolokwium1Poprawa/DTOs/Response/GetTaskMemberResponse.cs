using System;

namespace Kolokwium1Poprawa.DTOs.Response
{
    public class GetTaskMemberResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string ProjectName { get; set; }
        public string Type { get; set; }
    }
}