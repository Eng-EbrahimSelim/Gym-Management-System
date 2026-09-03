using Gym_Management_System.Data;
using Gym_Management_System.Interfaces;
using Gym_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System.Repositories
{
    public class MemberRepo : IMemberRepo
    {
        private GymDbContext _context;
        public MemberRepo(GymDbContext context)
        {
            _context = context;
        }

        public void AddMember(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public void DeleteMember(int id)
        {
            var member = _context.Members.Find(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
            }
        }

        public List<Member> GetAllMembers()
        {
            return _context.Members.ToList();
        }
        public List<Member> GetMembersByGymClassId(int gymClassId)
        {
            return _context.Members.Include(m => m.Enrollments)
                                   .Where(m => m.Enrollments.Any(e => e.GymClassId == gymClassId))
                                   .ToList();
        }

        public Member GetMemberById(int id)
        {
            return _context.Members.FirstOrDefault(m => m.Id == id);
        }

        public void UpdateMember(Member member)
        {
            _context.Members.Update(member);
            _context.SaveChanges();
        }
        public void EnrollMemberInClass(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
        }
    }
}
