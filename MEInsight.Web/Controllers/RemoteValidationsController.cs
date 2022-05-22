using MEInsight.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace MEInsight.Web.Controllers
{
    public class RemoteValidationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RemoteValidationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Uses [Remote] to validate user input and verify if the Organization Code already exists in the database.
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="OrganizationCodeInitialValue"></param>
        /// <returns>false (not valid) if code already exists</returns>
        [AcceptVerbs("Post")]
        public IActionResult VerifyOrganizationCode(string OrganizationCode, string OrganizationCodeInitialValue)
        {
            if (OrganizationCode == OrganizationCodeInitialValue)
            {
                return Json(true);
            }

            if (_context.Organizations.Any(e => e.OrganizationCode == OrganizationCode))
            {
                return Json(false);
            }

            return Json(true);
        }

        /// <summary>
        /// Uses [Remote] to validate user input and verify if the Participant Code already exists in the database.
        /// </summary>
        /// <param name="ParticipantCode"></param>
        /// <param name="ParticipantCodeInitialValue"></param>
        /// <returns>false (not valid) if code already exists</returns>
        [AcceptVerbs("Post")]
        public IActionResult VerifyParticipantCode(string ParticipantCode, string ParticipantCodeInitialValue)
        {
            if (ParticipantCode == ParticipantCodeInitialValue)
            {
                return Json(true);
            }

            if (_context.Participants.Any(e => e.ParticipantCode == ParticipantCode))
            {
                return Json(false);
            }

            return Json(true);
        }

        /// <summary>
        /// Uses [Remote] to validate user input and verify if the Partner Code already exists in the database.
        /// </summary>
        /// <param name="PartnerCode"></param>
        /// <param name="PartnerCodeInitialValue"></param>
        /// <returns>false (not valid) if code already exists</returns>
        [AcceptVerbs("Post")]
        public IActionResult VerifyPartnerCode(string PartnerCode, string PartnerCodeInitialValue)
        {
            if (PartnerCode == PartnerCodeInitialValue)
            {
                return Json(true);
            }

            if (_context.Partners.Any(e => e.PartnerCode == PartnerCode))
            {
                return Json(false);
            }

            return Json(true);
        }

        /// <summary>
        /// Uses [Remote] to validate user input and verify if the School Code already exists in the database.
        /// </summary>
        /// <param name="SchoolCode"></param>
        /// <param name="SchoolCodeInitialValue"></param>
        /// <returns>false (not valid) if code already exists</returns>
        [AcceptVerbs("Post")]
        public IActionResult VerifySchoolCode(string SchoolCode, string SchoolCodeInitialValue)
        {
            if (SchoolCode == SchoolCodeInitialValue)
            {
                return Json(true);
            }

            if (_context.Schools.Any(e => e.SchoolCode == SchoolCode))
            {
                return Json(false);
            }

            return Json(true);
        }

        /// <summary>
        /// Uses [Remote] to validate user input and verify if the Student Code already exists in the database.
        /// </summary>
        /// <param name="StudentCode"></param>
        /// <param name="StudentCodeInitialValue"></param>
        /// <returns>false (not valid) if code already exists</returns>
        [AcceptVerbs("Post")]
        public IActionResult VerifyStudentCode(string StudentCode, string StudentCodeInitialValue)
        {
            if (StudentCode == StudentCodeInitialValue)
            {
                return Json(true);
            }

            if (_context.Students.Any(e => e.StudentCode == StudentCode))
            {
                return Json(false);
            }

            return Json(true);
        }

        /// <summary>
        /// Uses [Remote] to validate user input and verify if the Group Code already exists in the database.
        /// </summary>
        /// <param name="GroupCode"></param>
        /// <param name="GroupCodeInitialValue"></param>
        /// <returns>false (not valid) if code already exists</returns>
        [AcceptVerbs("Post")]
        public IActionResult VerifyGroupCode(string GroupCode, string GroupCodeInitialValue)
        {
            if (GroupCode == GroupCodeInitialValue)
            {
                return Json(true);
            }

            if (_context.Groups.Any(e => e.GroupCode == GroupCode))
            {
                return Json(false);
            }

            return Json(true);
        }

        /// <summary>
        /// Uses [Remote] to validate user input and verify if the Location Code (RefLocationId) already exists in the database.
        /// </summary>
        /// <param name="LocationCode"></param>
        /// <param name="LocationCodeInitialValue"></param>
        /// <returns>false (not valid) if code already exists</returns>
        [AcceptVerbs("Post")]
        public IActionResult VerifyLocationCode(string RefLocationId, string LocationCodeInitialValue)
        {
            if (RefLocationId == LocationCodeInitialValue)
            {
                return Json(true);
            }

            if (_context.Locations.Any(e => e.RefLocationId == RefLocationId))
            {
                return Json(false);
            }

            return Json(true);
        }

        /// <summary>
        /// Uses [Remote] to validate user input and verify if the Tracking Code already exists in the database.
        /// </summary>
        /// <param name="TrackingCode"></param>
        /// <param name="TrackingCodeInitialValue"></param>
        /// <returns>false (not valid) if code already exists</returns>
        [AcceptVerbs("Post")]
        public IActionResult VerifyTrackingCode(string TrackingCode, string TrackingCodeInitialValue)
        {
            if (TrackingCode == TrackingCodeInitialValue)
            {
                return Json(true);
            }

            if (_context.TLMDistributions.Any(e => e.TrackingCode == TrackingCode))
            {
                return Json(false);
            }

            return Json(true);
        }

        /// <summary>
        /// Uses [Remote] to validate user input and verify if the TLM Material Code already exists in the database.
        /// </summary>
        /// <param name="TLMMaterialCode"></param>
        /// <param name="TLMMaterialCodeInitialValue"></param>
        /// <returns>false (not valid) if code already exists</returns>
        [AcceptVerbs("Post")]
        public IActionResult VerifyTLMMaterialCode(string TLMMaterialCode, string TLMMaterialCodeInitialValue)
        {
            if (TLMMaterialCode == TLMMaterialCodeInitialValue)
            {
                return Json(true);
            }

            if (_context.TLMMaterials.Any(e => e.TLMMaterialCode == TLMMaterialCode))
            {
                return Json(false);
            }

            return Json(true);
        }
    }
}
