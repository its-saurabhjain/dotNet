using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NagpurUniversity.Models;

namespace NagpurUniversity.DAL
{
    public class StudentRepository : IStudentRepository
    {
        SchoolContext schoolContext;
        public StudentRepository(SchoolContext schoolContext) {

            this.schoolContext = schoolContext;

        }
        public void DeleteStudent(int studentID)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Student GetStudentByID(int studentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudents()
        {
            throw new NotImplementedException();
        }

        public void InsertStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
