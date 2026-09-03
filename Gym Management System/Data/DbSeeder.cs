using Gym_Management_System.Models;

namespace Gym_Management_System.Data
{
    public static class DbSeeder
    {
        public static void Seed(GymDbContext context)
        {
            // Make sure database is created
            context.Database.EnsureCreated();

            // =========================
            // Trainers
            // =========================
            if (!context.Trainers.Any())
            {
                context.Trainers.AddRange(
                      new Trainer
                      {
                          Name = "Admin",
                          Email = "admin@gym.com",
                          Password = "Admin123",
                          Specialization = "Fitness Training",
                          IsAdmin = true
                      },
                    new Trainer
                    {
                        Name = "Ebrahim Selim",
                        Email = "ebrahim@gmail.com",
                       
                        Password = "123456",
                        Specialization = "Fitness Training",
                        IsAdmin = true
                    },

                    new Trainer
                    {
                        Name = "Mohamed Ali",
                        Email = "mohamed@gym.com",
                        Password = "123456",
                        Specialization = "Bodybuilding",
                        IsAdmin = false
                    },

                    new Trainer
                    {
                        Name = "Omar Khaled",
                        Email = "omar@gym.com",
                        Password = "123456",
                        Specialization = "CrossFit",
                        IsAdmin = false
                    }
                );

                context.SaveChanges();
            }

            // =========================
            // Members
            // =========================
            if (!context.Members.Any())
            {
                context.Members.AddRange(
                    new Member
                    {
                        Name = "Ahmed Hassan",
                        Email = "ahmed@gym.com",
                        Phone = "01012345678"
                    },

                    new Member
                    {
                        Name = "Mahmoud Ahmed",
                        Email = "mahmoud@gmail.com",
                        Phone = "01112345678"
                    },

                    new Member
                    {
                        Name = "Youssef Mohamed",
                        Email = "youssef@gmail.com",
                        Phone = "01212345678"
                    },

                    new Member
                    {
                        Name = "Ali Hassan",
                        Email = "ali@gmail.com",
                        Phone = "01512345678"
                    }
                );

                context.SaveChanges();
            }

            // =========================
            // Gym Classes
            // =========================
            if (!context.GymClasses.Any())
            {
                var trainers = context.Trainers.ToList();

                context.GymClasses.AddRange(
                    new GymClass
                    {
                        Name = "Morning Fitness",
                        Description = "Morning fitness and cardio training.",
                        Schedule = "Saturday - Monday - Wednesday, 9:00 AM",
                        TrainerId = trainers[0].Id
                    },

                    new GymClass
                    {
                        Name = "Bodybuilding",
                        Description = "Strength and muscle building exercises.",
                        Schedule = "Sunday - Tuesday - Thursday, 6:00 PM",
                        TrainerId = trainers[1].Id
                    },

                    new GymClass
                    {
                        Name = "CrossFit",
                        Description = "High intensity CrossFit training.",
                        Schedule = "Saturday - Monday - Wednesday, 7:00 PM",
                        TrainerId = trainers[2].Id
                    }
                );

                context.SaveChanges();
            }

            // =========================
            // Enrollments
            // =========================
            if (!context.Enrollments.Any())
            {
                var members = context.Members.ToList();
                var classes = context.GymClasses.ToList();

                context.Enrollments.AddRange(
                    new Enrollment
                    {
                        MemberId = members[0].Id,
                        GymClassId = classes[0].Id,
                        EnrollmentDate = DateTime.Now
                    },

                    new Enrollment
                    {
                        MemberId = members[0].Id,
                        GymClassId = classes[1].Id,
                        EnrollmentDate = DateTime.Now
                    },

                    new Enrollment
                    {
                        MemberId = members[1].Id,
                        GymClassId = classes[0].Id,
                        EnrollmentDate = DateTime.Now
                    },

                    new Enrollment
                    {
                        MemberId = members[2].Id,
                        GymClassId = classes[2].Id,
                        EnrollmentDate = DateTime.Now
                    },

                    new Enrollment
                    {
                        MemberId = members[3].Id,
                        GymClassId = classes[1].Id,
                        EnrollmentDate = DateTime.Now
                    }
                );

                context.SaveChanges();
            }
        }
    }
}