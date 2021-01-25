using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace one_to_many
{
    public class Student
    {
        public int studentid { get; set; }
        public string student_name { get; set; }
        public int student_age { get; set; }
        public uint student_phone { get; set; }

        public int gradeid { get; set; }
        public Grade grade { get; set; }
    }

    public class Grade
    {
        public int gradeid { get; set; }
        public string grade_name { get; set; }
    }

    public class ClassRoom : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=.;Database=Db2;Trusted_Connection=True;");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using(var context=new ClassRoom())
            {
                var std = new Student()
               {
                    student_name = "Afaq",
                    student_age=23,
                    student_phone=03204450987,
                    gradeid=1
                    
                };
                var std1 = new Student()
                {
                    student_name = "Yasim",
                    student_age = 24,
                    student_phone = 0062786328,
                    gradeid = 4
                };
                var std2 = new Student()
                {
                    student_name = "Wamik",
                    student_age = 22,
                    student_phone = 0333350987,
                    gradeid = 5
                };
                var std3 = new Student()
                {
                    student_name = "Umair",
                    student_age = 99,
                    student_phone = 03204450987,
                    gradeid = 2
                };
               
                var grad = new Grade()
                {
                    
                    grade_name="A"
                };
                var grad1 = new Grade()
                {
                    grade_name = "B"
                };
                var grad2 = new Grade()
                {
                    grade_name = "C"
                };
                var grad3 = new Grade()
                {
                    grade_name = "D"
                };
                var grad4= new Grade()
                {
                    grade_name = "E"
                };
                var grad5 = new Grade()
                {
                    grade_name = "F"
                };

                /*context.Grades.Add(grad1);
                context.Grades.Add(grad2);
                context.Grades.Add(grad3);
                context.Grades.Add(grad4);
                context.Grades.Add(grad5);*/


                /*  context.Students.Add(std);
                  context.Students.Add(std1);
                  context.Students.Add(std2);
                  context.Students.Add(std3);*/



                context.SaveChanges();


                var result = from s in context.Students
                             join g in context.Grades on s.gradeid equals g.gradeid
                             select new
                             {
                                 std_name = s.student_name,
                                 std_grade = g.grade_name
                             };
                foreach (var res in result)
                    Console.WriteLine("Name:{0}\t\tGrade:{1}", res.std_name, res.std_grade);

                Console.WriteLine("=====================RawSql==============================");

                var rawsql = context.Students.FromSqlRaw("Select * from Students where gradeid<3").ToList();
                foreach (var r in rawsql)
                    Console.WriteLine("Name:" + r.student_name + "\tAge:" + r.student_age + "\tGrade:" + r.gradeid);
                
            }

        }
    }
}
