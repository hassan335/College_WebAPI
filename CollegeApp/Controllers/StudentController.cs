﻿using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Models;
using CollegeApp.MyLogging;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CollegeApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly CollegeDbContext _db;

        private readonly IMapper _mapper;

        public StudentController(ILogger<StudentController> logger, CollegeDbContext dbContext,IMapper mapper)
        {
            _logger = logger;
            _db = dbContext;
            _mapper = mapper;

        }


        [HttpGet("All", Name = "GetStudentData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task < ActionResult<IEnumerable<StudentDTO>>> GetStudentData()
        {
            #region ok_200

            //List<StudentDTO> students = await _db.Students.Select(x => new StudentDTO()

            //{

            //    Id = x.Id,
            //    Name = x.Name,
            //    Email = x.Email,
            //    Address = x.Address,
            //    DOB = x.DOB



            //}).ToListAsync();

           var students= await _db.Students.ToListAsync();

            var studentsDTO = _mapper.Map<List<StudentDTO>>(students);



           



          _logger.LogInformation("Get All Students Method is executing");

            return Ok(studentsDTO);
            #endregion


        }
        [HttpGet("{id:int}", Name = "GetStudentDataById")]



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task <ActionResult<StudentDTO>> GetStudentDataById(int id) /*<Student>*/
        {
            _logger.LogInformation("Get Student by id  Method is executing");
            #region 400_BadRequest
            if (id < 0)

            {
                _logger.LogError("Error");

                return BadRequest();
            }
              
            #endregion

            //Student s = CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();

            //StudentDTO studentss = await _db.Students.Where(x => x.Id == id).Select(x => new StudentDTO()

            //{

            //    Id = x.Id,
            //    Name = x.Name,
            //    Email = x.Email,
            //    Address = x.Address,
            //    DOB = x.DOB



            //}).FirstOrDefaultAsync();


            var student1 = await _db.Students.Where(x => x.Id == id).SingleOrDefaultAsync();

            StudentDTO studentss =_mapper.Map<StudentDTO>(student1);






            #region Not_Found 404
            if (studentss == null)
            {
                _logger.LogError("Student not found with the given ID");
                return NotFound($"Student Not Found id:{id}");
                #endregion
            }
            #region Ok_200
            return Ok(studentss);
            #endregion



        }


        //[HttpGet("{name:regex(^[[a-zA-Z]]+$)}", Name = "GetStudentDataByName")]
        [HttpGet("{name:alpha}", Name = "GetStudentDataByName")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task <ActionResult<StudentDTO>> GetStudentDataByName(string name)
        {

            #region 400_BadRequest
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogWarning("Bad request");
                return BadRequest();
            }
            #endregion

            //Student s = CollegeRepository.Students.Where(x => x.Name == name).FirstOrDefault();

            //StudentDTO studentss = await _db.Students.Where(x => x.Name == name).Select(x => new StudentDTO()

            //{

            //    Id = x.Id,
            //    Name = x.Name,
            //    Email = x.Email,
            //    Address = x.Address



            //}).FirstOrDefaultAsync();


            var student1 = await _db.Students.Where(x => x.Name == name).SingleOrDefaultAsync();

            StudentDTO studentss = _mapper.Map<StudentDTO>(student1);








            #region Not_Found 404
            if (studentss == null)
                return NotFound($"Student Not Found name:{name}");
            #endregion

            #region Ok_200
            return Ok(studentss);
            #endregion



        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpDelete("{id}", Name = "DeleteStudentDataById")]
        public async Task<ActionResult<bool>> DeleteStudentDataById(int id)

        {

            #region 400_BadRequest
            if (id < 0)
            {
                _logger.LogWarning("bad request");
                return BadRequest();

            }
            #endregion

            Student s = await _db.Students.Where(x => x.Id == id).FirstOrDefaultAsync();

            #region Not_Found 404
            if (s == null)
                return NotFound($"Student Not Found id:{id}");
            #endregion

            #region Ok_200

            
            _db.Students.Remove(s);
            _db.SaveChanges();
            return Ok(true);
            #endregion




        }


        [ProducesResponseType(StatusCodes.Status201Created)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost("Create", Name = "CreateStudent")]

        public ActionResult<StudentDTO> CreateStudent(StudentDTO sdt)
        {
            #region BadRequest_400
            if (sdt == null)
            {
                _logger.LogWarning("bad request");
                return BadRequest();
            }

            //if (sdt.AdmissionDate < DateTime.Now.Date)
            //{
            //    ModelState.AddModelError("Admission Date Error", "Please Enter valid date");
            //    return BadRequest(ModelState);
            //}
            #endregion




            #region Created_201

            //int Id = CollegeRepository.Students.Select(x => x.Id).LastOrDefault() + 1;

            //Student st = new Student
            //{
            //    //Id = Id,
            //    Name = sdt.Name,
            //    Address = sdt.Address,
            //    Email = sdt.Email,
            //    DOB = sdt.DOB
            //};


            Student st = _mapper.Map<Student>(sdt); 

            _db .Students.AddAsync(st);
            _db.SaveChanges();

            return CreatedAtRoute("GetStudentDataById", new { id = st.Id }, st);
            #endregion









        }



        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("Update", Name = "UpdateStudent")]

       

        public async Task <ActionResult<StudentDTO>> UpdateStudent([FromBody]StudentDTO sdt)
        {
            #region BadRequest_400
            if (sdt == null || sdt.Id == 0)
            {
                _logger.LogWarning("bad request");
                return BadRequest();
            }

            #endregion
           Student sdt1 = await _db.Students.AsNoTracking().Where(x=>x.Id == sdt.Id).FirstOrDefaultAsync() ;
            #region Not_Found_404
            if (sdt1 == null)
                return NotFound();
            #endregion



            //    sdt1.Name = sdt.Name;
            //    sdt1.Address = sdt.Address;
            //sdt1.Email = sdt.Email;
            //sdt1.DOB = sdt.DOB;


            var StudentNew = _mapper.Map<Student>(sdt);

            //var StudentNew = new Student(){
            //    Id = sdt.Id,
            //       Name = sdt.Name,
            //        DOB = sdt.DOB,
            //        Email = sdt.Email,
            //        Address = sdt.Address


            //};

            _db.Students.Update(StudentNew);
            _db.SaveChanges();




            #region NoContent_204
            return NoContent();
            #endregion



        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("{id:int}/UpdatePartial", Name = "UpdateStudentPartial")]



        public async Task <ActionResult<StudentDTO>> UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchdocument )
        {
            #region BadRequest_400
            if (patchdocument == null || id == 0)
            {
                _logger.LogWarning("bad request");
                return BadRequest();

            }


            #endregion
            Student sdt1 = await _db.Students.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            #region Not_Found_404
            if (sdt1 == null)
                return NotFound();
            #endregion



            var StudentDTO = _mapper.Map<StudentDTO>(sdt1);



            //var StudentDTO = new StudentDTO
            //{
            //    Id = sdt1.Id,
            //    Name = sdt1.Name,
            //    Address = sdt1.Address,
            //    Email = sdt1.Email,
            //    DOB =sdt1.DOB


            //};


            patchdocument.ApplyTo(StudentDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            sdt1 = _mapper.Map<Student>(StudentDTO);




            //sdt1.Name = StudentDTO.Name;
            //sdt1.Address = StudentDTO.Address;
            //sdt1.Email = StudentDTO.Email;
            //sdt1.DOB = StudentDTO.DOB;

            _db.Students.Update(sdt1);
            _db.SaveChangesAsync();



            #region NoContent_204
            return NoContent();
            #endregion



        }










    }
}
