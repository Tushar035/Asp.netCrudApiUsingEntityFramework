using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    public class StudentController : ApiController
    {
        studentDbContext db = new studentDbContext();

        [HttpGet]
        public HttpResponseMessage GetAllStudent()
        {
            IEnumerable<StudentRegistration> students = db.StudentRegistrations.ToList();
            if (students != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, students);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are No records registred");
            }
        }

        [HttpGet]
        public HttpResponseMessage getStudentById([FromUri] int id)
        {
            StudentRegistration student = db.StudentRegistrations.Find(id);
            if (student!=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, student);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,$"there is no student with {id} Id");
            }
        }


        [HttpGet]
        public HttpResponseMessage getStudentByName([FromBody] string studentName)
        {
            IEnumerable<StudentRegistration> student = db.StudentRegistrations.Where(e => e.Student_Name == studentName);
            if (student!=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, student);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"there are no records found with name {studentName}");
            }
        }

        [HttpPost]
        public HttpResponseMessage RegisterStudent(StudentRegistration sr)
        {
            db.StudentRegistrations.Add(sr);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created, "Your Registration is successful");
        }

        [HttpDelete]
        public HttpResponseMessage RemoveStudentById(int id)
        {
            StudentRegistration st = db.StudentRegistrations.Find(id);
            db.StudentRegistrations.Remove(st);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created, $"student with {id} ID is successfully deleted");
        }
    }
}