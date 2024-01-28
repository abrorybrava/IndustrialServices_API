using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IndustrialServices_API.Models;
using IndustrialServices_API.Model;

namespace IndustrialServices_API.Controllers
{
    public class FlyerController : ControllerBase
    {
        private readonly Flyer flyerRepository;
        private readonly IConfiguration configuration;
        private ResponseModel response = new ResponseModel();

        public FlyerController(IConfiguration configuration)
        {
            this.configuration = configuration;
            flyerRepository = new Flyer(configuration);
        }

        [HttpGet("GetAllFlyers", Name = "GetAllFlyers")]
        public IActionResult GetAllFlyers()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = flyerRepository.GetFlyers();
            }
            catch (System.Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpGet("GetFlyers", Name = "GetFlyers")]
        public IActionResult GetFlyers(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = flyerRepository.GetFlyersbyId(id);
            }
            catch (System.Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpPost("InsertFlyer", Name = "InsertFlyer")]
        public IActionResult InsertFlyer([FromBody] FlyerModel flyer)
        {
            try
            {
                if (!string.IsNullOrEmpty(flyer.path_flyer))
                {
                    string fileName = flyer.path_flyer;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    flyer.path_flyer = fileName;
                }
                response.status = 200;
                response.messages = "Success";
                flyerRepository.InsertFlyer(flyer);
            }
            catch (System.Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpPost("UpdateFlyer", Name = "UpdateFlyer")]
        public IActionResult UpdateFlyer([FromBody] FlyerModel flyer)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                flyerRepository.UpdateFlyer(flyer);
            }
            catch (System.Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpPost("DeleteFlyer", Name = "DeleteFlyer")]
        public IActionResult DeleteFlyer(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                flyerRepository.DeleteFlyer(id);
            }
            catch (System.Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }
    }
}
