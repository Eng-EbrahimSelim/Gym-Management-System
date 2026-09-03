using Gym_Management_System.Models;

namespace Gym_Management_System.Interfaces
{
    public interface IMemberRepo
    {
        public List<Member> GetAllMembers();
        public Member GetMemberById(int id);
        public List<Member> GetMembersByGymClassId(int gymClassId);
        public void AddMember(Member member);
        public void UpdateMember(Member member);
        public void DeleteMember(int id);
        public void EnrollMemberInClass(Enrollment enrollment);
    }
}
