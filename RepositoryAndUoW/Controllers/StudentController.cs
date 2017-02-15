using NagpurUniversity.DAL;
using NagpurUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagpurUniversity.Controllers
{
    public class StudentController
    {
        //private IStudentRepository studentRepository;

        private UnitOfWork unitOfWork = new UnitOfWork();

        public StudentController()
        {
            //this.studentRepository = new StudentRepository(new SchoolContext());
        }
        /// <summary>
        /// If you were using dependency injection, or DI, you wouldn't need the default constructor 
        /// because the DI software would ensure that the correct repository object would always be provided.)
        /// </summary>
        /// <param name="studentRepository"></param>
        /*
        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        */

        public Student Details(int id)
        {
            //Student student = studentRepository.GetStudentByID(id);
            Student student = unitOfWork.StudentRepository.GetByID(id);
            return student;
        }

        public IEnumerable<Student> GetStudents() {

            //var students = from s in studentRepository.GetStudents()
            //               select s;
            var students = from s in unitOfWork.StudentRepository.Get()
                           select s;
            return students;

        }
    }
}
