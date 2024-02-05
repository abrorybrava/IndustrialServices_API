using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IndustrialServices_API.Models;
using IndustrialServices_API.Model;

namespace IndustrialServices_API.Controllers
{
    public class PengelolaWebController : ControllerBase
    {
        private readonly PengelolaWeb pengelolaWebRepository;
        private readonly ResponseModel response = new ResponseModel();

        public PengelolaWebController(IConfiguration configuration)
        {
            pengelolaWebRepository = new PengelolaWeb(configuration);
        }

        [HttpGet("/GetAllPengelolaWeb", Name = "GetAllPengelolaWeb")]
        public IActionResult GetAllPengelolaWeb()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = pengelolaWebRepository.GetAllPengelolaWeb();
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpGet("/GetAllPengelolaWebinIndex", Name = "GetAllPengelolaWebinIndex")]
        public IActionResult GetAllPengelolaWebinIndex()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = pengelolaWebRepository.GetAllPengelolaWebinIndex();
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpGet("/GetPengelolaWeb", Name = "GetPengelolaWeb")]
        public IActionResult GetPengelolaWeb(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = pengelolaWebRepository.GetPengelolaWebById(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpGet("/GetPengelolaWebById", Name = "GetPengelolaWebById")]
        public IActionResult GetPengelolaWebById(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = pengelolaWebRepository.GetAllPengelolaWebById(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpPost("/InsertPengelolaWeb", Name = "InsertPengelolaWeb")]
        public IActionResult InsertPengelolaWeb([FromBody] PengelolaWebModel pengelolaWeb)
        {
            try
            {
                if (!pengelolaWebRepository.CheckUsernamePassword(pengelolaWeb))
                {
                    response.status = 200;
                    response.messages = "Success";
                    pengelolaWebRepository.InsertPengelolaWeb(pengelolaWeb);
                }
                else if (pengelolaWebRepository.CheckUsername(pengelolaWeb))
                {
                    response.status = 500;
                    response.messages = "Username atau Password sudah digunakan";
                    return Ok(response);
                }
                else
                {
                    response.status = 500;
                    response.messages = "Username atau Password sudah digunakan";
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpPost("/UpdatePengelolaWeb", Name = "UpdatePengelolaWeb")]
        public IActionResult UpdatePengelolaWeb([FromBody] PengelolaWebModel pengelolaWeb)
        {
            try
            {
                if (!pengelolaWebRepository.CheckUsernameEdit(pengelolaWeb))
                {
                    response.status = 200;
                    response.messages = "Success";
                    pengelolaWebRepository.UpdatePengelolaWeb(pengelolaWeb);
                }
                else
                {
                    response.status = 500;
                    response.messages = "Username atau Password sudah digunakan";
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }


        [HttpPost("/UpdateUsnPwPengelolaWeb", Name = "UpdateUsnPwPengelolaWeb")]
        public IActionResult UpdateUsnPwPengelolaWeb([FromBody] PengelolaWebModel pengelolaWeb)
        {
            try
            {
                if (pengelolaWebRepository.CheckPasswordEdit(pengelolaWeb))
                {
                    response.status = 200;
                    response.messages = "Success";
                    pengelolaWebRepository.UpdateUsnPwPengelolaWeb(pengelolaWeb);
                }
                else if (pengelolaWebRepository.CheckPasswordEdit2(pengelolaWeb))
                {
                    response.status = 400;
                    response.messages = "Password sudah dipakai!";
                    return Ok(response);
                }
                else
                {
                    response.status = 500;
                    response.messages = "Password lama tidak cocok!";
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpPost("/DeletePengelolaWeb", Name = "DeletePengelolaWeb")]
        public IActionResult DeletePengelolaWeb(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                pengelolaWebRepository.DeletePengelolaWeb(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpPost("/ActivatePengelolaWeb", Name = "ActivatePengelolaWeb")]
        public IActionResult ActivatePengelolaWeb(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                pengelolaWebRepository.ActivatePengelolaWeb(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpGet("/Login", Name = "Login")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                var pengelola = pengelolaWebRepository.Login(username, password);
                if (pengelola.username != null)
                {
                    response.status = 200;
                    response.messages = "Success";
                    response.data = pengelola;
                    return Ok(response);
                }
                else
                {
                    response.status = 500;
                    response.messages = "Failed";
                }
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }
    }
}
