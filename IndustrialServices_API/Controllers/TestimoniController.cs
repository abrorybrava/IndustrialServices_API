using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IndustrialServices_API.Models;
using IndustrialServices_API.Model;

namespace IndustrialServices_API.Controllers
{
    public class TestimoniController : ControllerBase
    {
        private readonly Testimoni testimoniRepository;
        ResponseModel response = new ResponseModel();

        public TestimoniController(IConfiguration configuration)
        {
            testimoniRepository = new Testimoni(configuration);
        }

        [HttpGet("/GetAllTestimonies", Name = "GetAllTestimonies")]
        public IActionResult GetAllTestimonies()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = testimoniRepository.GetAllTestimonies();
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpGet("/GetTestimony", Name = "GetTestimony")]
        public IActionResult GetTestimony(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = testimoniRepository.GetTestimonyById(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }
        [HttpGet("/GetTestimonyByPelatihan", Name = "GetTestimonyByPelatihan")]
        public IActionResult GetTestimonyByPelatihan(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = testimoniRepository.GetTestimonyByPelatihan(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPost("/InsertTestimony", Name = "InsertTestimony")]
        public IActionResult InsertTestimony([FromBody] TestimoniModel testimony)
        {
            try
            {
                if (!string.IsNullOrEmpty(testimony.foto_peserta))
                {
                    string fileName = testimony.foto_peserta;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    testimony.foto_peserta = fileName;
                }

                response.status = 200;
                response.messages = "Success";
                testimoniRepository.InsertTestimony(testimony);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPost("/UpdateTestimony", Name = "UpdateTestimony")]
        public IActionResult UpdateTestimony([FromBody] TestimoniModel testimony)
        {
            try
            {
                if (!string.IsNullOrEmpty(testimony.foto_peserta))
                {
                    string fileName = testimony.foto_peserta;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    testimony.foto_peserta = fileName;
                }
                else
                {
                    testimony.foto_peserta = null;
                }

                response.status = 200;
                response.messages = "Success";
                testimoniRepository.UpdateTestimony(testimony);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPost("/DeleteTestimony", Name = "DeleteTestimony")]
        public IActionResult DeleteTestimony(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                testimoniRepository.DeleteTestimony(id);
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
