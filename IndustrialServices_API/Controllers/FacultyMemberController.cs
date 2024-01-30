using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IndustrialServices_API.Models;
using IndustrialServices_API.Model;

namespace IndustrialServices_API.Controllers
{
    public class FacultyMemberController : ControllerBase
    {
        private readonly FacultyMember facultyMemberRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        ResponseModel response = new ResponseModel();

        public FacultyMemberController(IConfiguration configuration)
        {
            facultyMemberRepository = new FacultyMember(configuration);
        }


        [HttpGet("/GetAllFacultyMembers", Name = "GetAllFacultyMembers")]
        public IActionResult GetAllFacultyMembers()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = facultyMemberRepository.GetAllFacultyMembers();
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpGet("/GetAllFacultyMembersinWeb", Name = "GetAllFacultyMembersinWeb")]
        public IActionResult GetAllFacultyMembersinWeb(string filter)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = facultyMemberRepository.GetAllFacultyMembersinWeb(filter);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpGet("/GetAllFacultyMembersbyBK", Name = "GetAllFacultyMembersbyBK")]
        public IActionResult GetAllFacultyMembersbyBK(string bidang_keahlian)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = facultyMemberRepository.GetAllFacultyMembersbyBK(bidang_keahlian);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }
        [HttpGet("/GetAllFacultyMembersTechnical", Name = "GetAllFacultyMembersTechnical")]
        public IActionResult GetAllFacultyMembersTechnical()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = facultyMemberRepository.GetAllFacultyMembersTechnical();
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }
        [HttpGet("/GetAllFacultyMembersNonTechnical", Name = "GetAllFacultyMembersNonTechnical")]
        public IActionResult GetAllFacultyMembersNonTechnical()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = facultyMemberRepository.GetAllFacultyMembersNonTechnical();
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpGet("/GetFacultyMember", Name = "GetFacultyMember")]
        public IActionResult GetFacultyMember(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = facultyMemberRepository.GetFacultyMemberById(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpGet("/GetFacultyMemberDetails", Name = "GetFacultyMemberDetails")]
        public IActionResult GetFacultyMemberDetails(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = facultyMemberRepository.GetFacultyMemberDetails(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPost("/InsertFacultyMember", Name = "InsertFacultyMember")]
        public IActionResult InsertFacultyMember([FromBody] FacultyMemberModel facultyMember)
        {
            try
            {
                if (!string.IsNullOrEmpty(facultyMember.foto_pengajar))
                {
                    string fileName = facultyMember.foto_pengajar;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1); 
                    facultyMember.foto_pengajar = fileName;
                }

                if (!facultyMemberRepository.CheckNPK(facultyMember))
                {
                    response.status = 200;
                    response.messages = "Success";
                    facultyMemberRepository.InsertFacultyMember(facultyMember);
                }
                else
                {
                    response.status = 500;
                    response.messages = "NPK was already added!";
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPost("/UpdateFacultyMember", Name = "UpdateFacultyMember")]
        public IActionResult UpdateFacultyMember([FromBody] FacultyMemberModel facultyMember)
        {
            try
            {
                if (!string.IsNullOrEmpty(facultyMember.foto_pengajar))
                {
                    string fileName = facultyMember.foto_pengajar;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    facultyMember.foto_pengajar = fileName;
                }
                else
                {
                    facultyMember.foto_pengajar = null;
                }

                if (!facultyMemberRepository.CheckNPKEdit(facultyMember))
                {
                    response.status = 200;
                    response.messages = "Success";
                    facultyMemberRepository.UpdateFacultyMember(facultyMember);
                }
                else
                {
                    response.status = 500;
                    response.messages = "NPK was already added!";
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPost("/DeleteFacultyMember", Name = "DeleteFacultyMember")]
        public IActionResult DeleteFacultyMember(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                facultyMemberRepository.DeleteFacultyMember(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }
    }
}