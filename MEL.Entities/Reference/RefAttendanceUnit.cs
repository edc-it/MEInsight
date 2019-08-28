using MEL.Entities.Programs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Reference
{
    public class RefAttendanceUnit
    {
        public RefAttendanceUnit()
        {
            this.Programs = new HashSet<Program>();
            this.ProgramAssessments = new HashSet<ProgramAssessment>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Attendance Unit Id")]
        [Column(Order = 0)]
        public int RefAttendanceUnitId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Attendance Unit Code")]
        [Column(Order = 1)]
        public string AttendanceUnitCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Attendance Unit")]
        [Column(Order = 2)]
        public string AttendanceUnit { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(15)]
        [Display(Name = "Unit Id")]
        [Column(Order = 3)]
        public string AttendanceUnitId { get; set; }

        public virtual ICollection<Program> Programs { get; set; }
        public virtual ICollection<ProgramAssessment> ProgramAssessments { get; set; }
    }
}
