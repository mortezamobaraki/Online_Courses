using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Online_Courses.Models
{
    public class StudentViewModel
    {
        [Display(Name = "آیدی")]
        public int StudentId { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "ورودی {0} حداقل دو کاراکتر و حداکثر 20 کاراکتر باید باشد")]
        [RegularExpression("^[آ-ی]+$", ErrorMessage = "لطفا فقط حروف فارسی وارد کنید")]
        public string StudentName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "ورودی {0} حداقل دو کاراکتر و حداکثر 50 کاراکتر باید باشد")]
        [RegularExpression("^[آ-ی]+$", ErrorMessage = "لطفا از حروف فارسی استفاده کنید")]
        public string StudentFamily { get; set; }


        [Display(Name = "سن")]
        [RegularExpression("[0-9]{2}", ErrorMessage = "لطفا فقط عدد بین 18 تا 120 وارد کنید")]
        public Nullable<int> StudentAge { get; set; }


        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [RegularExpression("(09)[0-9]{9}", ErrorMessage = "فرمت شماره موبایل اشتباه است!")]
        public string StudentPhoneNumber { get; set; }


        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "ورودی {0} حداقل 8 کاراکتر و حداکثر 20 کاراکتر باید باشد")]
        [DataType(DataType.Password)]
        public string StudentPassword { get; set; }


        [Display(Name = "آدرس ایمیل")]
        [EmailAddress(ErrorMessage = "فرمت آدرس ایمیل اشتباه است! ")]
        public string StudentEmail { get; set; }


        [Display(Name = "جنسیت")]
        public bool StudentGender { get; set; }


        [Display(Name = "وضعیت تاهل")]
        public Nullable<bool> StudentMarital { get; set; }


        [Display(Name = "تصویر پروفایل")]
        public string StudentImageName { get; set; }


        [Display(Name = "تاریخ ثبت نام")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        public System.DateTime StudentRegisterDate { get; set; }


        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public bool StudentIsActive { get; set; }
    }

    [MetadataType(typeof(StudentViewModel))]
    public partial class T_Students
    {

    }
}