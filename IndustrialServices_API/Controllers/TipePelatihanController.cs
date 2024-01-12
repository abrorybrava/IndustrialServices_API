using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IndustrialServices_API.Models;

namespace IndustrialServices_API.Controllers
{
    public class TipePelatihanController : ControllerBase
    {
        private readonly TipePelatihan tipePelatihanRepository;

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

        [HttpPost("/InsertTipePelatihan", Name = "InsertTipePelatihan")]
        public IActionResult InsertTipePelatihan([FromBody] TipePelatihanModel tipePelatihan)
        {
            try
            {
                tipePelatihanRepository.InsertTipePelatihan(tipePelatihan);
                return Ok("Type of training inserted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to insert type of training: {ex.Message}");
            }
        }

        [HttpPost("/UpdateTipePelatihan", Name = "UpdateTipePelatihan")]
        public IActionResult UpdateTipePelatihan([FromBody] TipePelatihanModel tipePelatihan)
        {
            try
            {
                var existingTipePelatihan = tipePelatihanRepository.GetTipePelatihanById(tipePelatihan.id_tipe_pelatihan);
                if (existingTipePelatihan != null)
                {
                    tipePelatihanRepository.UpdateTipePelatihan(tipePelatihan);
                    return Ok("Type of training updated successfully");
                }
                else
                {
                    return NotFound($"Type of training with ID {tipePelatihan.id_tipe_pelatihan} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to update type of training: {ex.Message}");
            }
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
