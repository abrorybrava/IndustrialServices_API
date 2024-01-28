using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IndustrialServices_API.Models;
using IndustrialServices_API.Model;

namespace IndustrialServices_API.Controllers
{
    public class TipePelatihanController : ControllerBase
    {
        private readonly TipePelatihan tipePelatihanRepository;
        ResponseModel response = new ResponseModel();

        public TipePelatihanController(IConfiguration configuration)
        {
            tipePelatihanRepository = new TipePelatihan(configuration);
        }

        [HttpGet("/GetAllTipePelatihan", Name = "GetAllTipePelatihan")]
        public IActionResult GetAllTipePelatihan()
        {
            try
            {
                var tipePelatihanList = tipePelatihanRepository.GetAllTipePelatihan();
                return Ok(tipePelatihanList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve types of training: {ex.Message}");
            }   
        }

        [HttpGet("/GetTipePelatihan", Name = "GetTipePelatihan")]
        public IActionResult GetTipePelatihan(int id)
        {
            try
            {
                var tipePelatihan = tipePelatihanRepository.GetTipePelatihanById(id);
                return Ok(tipePelatihan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve type of training: {ex.Message}");
            }
        }

        [HttpGet("/GetAllTipePelatihanbyBK", Name = "GetAllTipePelatihanbyBK")]
        public IActionResult GetAllTipePelatihanbyBK(string bidang_keahlian)
        {
            try
            {
                var tipePelatihan = tipePelatihanRepository.GetAllTipePelatihanbyBK(bidang_keahlian);
                return Ok(tipePelatihan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve type of training: {ex.Message}");
            }
        }

        [HttpPost("/InsertTipePelatihan", Name = "InsertTipePelatihan")]
        public IActionResult InsertTipePelatihan([FromBody] TipePelatihanModel tipePelatihan)
        {
            try
            {
                if (!tipePelatihanRepository.CheckNama(tipePelatihan))
                {
                    response.status = 200;
                    response.messages = "Success";
                    tipePelatihanRepository.InsertTipePelatihan(tipePelatihan);
                }
                else
                {
                    response.status = 500;
                    response.messages = "Type of Facility was already added!";
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to insert type of facility: {ex.Message}");
            }
            return Ok(response);
        }

        [HttpPost("/UpdateTipePelatihan", Name = "UpdateTipePelatihan")]
        public IActionResult UpdateTipePelatihan([FromBody] TipePelatihanModel tipePelatihan)
        {
            try
            {
                var existingTipeFasilitas = tipePelatihanRepository.GetTipePelatihanById(tipePelatihan.id_tipe_pelatihan);
                if (existingTipeFasilitas != null)
                {
                    if (!tipePelatihanRepository.CheckNamaEdit(tipePelatihan))
                    {
                        response.status = 200;
                        response.messages = "Success";
                        tipePelatihanRepository.UpdateTipePelatihan(tipePelatihan);
                    }
                    else
                    {
                        response.status = 500;
                        response.messages = "Nama was already added!";
                        return Ok(response);
                    }
                }
                else
                {
                    return NotFound($"Type of facility with ID {tipePelatihan.id_tipe_pelatihan} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to update type of facility: {ex.Message}");
            }
            return Ok(response);
        }

        [HttpPost("/DeleteTipePelatihan", Name = "DeleteTipePelatihan")]
        public IActionResult DeleteTipePelatihan(int id)
        {
            try
            {
                var existingTipePelatihan = tipePelatihanRepository.GetTipePelatihanById(id);
                if (existingTipePelatihan != null)
                {
                    tipePelatihanRepository.DeleteTipePelatihan(id);
                    return Ok("Type of training deleted successfully");
                }
                else
                {
                    return NotFound($"Type of training with ID {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete type of training: {ex.Message}");
            }
        }
    }
}
