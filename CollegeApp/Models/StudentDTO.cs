using CollegeApp.Models.Validator;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class StudentDTO
    {

        [ValidateNever]
        public int Id { get; set; }

        [Required (ErrorMessage ="Name is required")]

        [StringLength(30)]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Adddress")]

        
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }


        public DateTime DOB { get; set; }

        //[Range(19,27,ErrorMessage ="Age is between 19 - 27")]
        //public int Age { get; set; }

        //public string password { get; set; }

        //[Compare(nameof(password))]
        //public string Confirmpassword { get; set; }

        //[DateCheck]
        //public DateTime AdmissionDate { get; set; }




    }
}
