using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IndustrialServices_API.Models;

namespace IndustrialServices_API.Controllers
{
    public class TipeFasilitasController : ControllerBase
    {
        private readonly TipeFasilitas tipeFasilitasRepository;

        public TipeFasilitasController(IConfiguration configuration)
        {
            tipeFasilitasRepository = new TipeFasilitas(configuration);
        }

        [HttpGet("/GetAllTipeFasilitas", Name = "GetAllTipeFasilitas")]
        public IActionResult GetAllTipeFasilitas()
        {
            try
            {
                var tipeFasilitasList = tipeFasilitasRepository.GetAllTipeFasilitas();
                return Ok(tipeFasilitasList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve types of facilities: {ex.Message}");
            }
        }

        [HttpGet("/GetTipeFasilitas", Name = "GetTipeFasilitas")]
        public IActionResult GetTipeFasilitas(int id)
        {
            try
            {
                var tipeFasilitas = tipeFasilitasRepository.GetTipeFasilitasById(id);
                return Ok(tipeFasilitas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve type of facility: {ex.Message}");
            }
        }

        [HttpPost("/InsertTipeFasilitas", Name = "InsertTipeFasilitas")]
        public IActionResult InsertTipeFasilitas([FromBody] TipeFasilitasModel tipeFasilitas)
        {
            try
            {
                tipeFasilitasRepository.InsertTipeFasilitas(tipeFasilitas);
                return Ok("Type of facility inserted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to insert type of facility: {ex.Message}");
            }
        }

        [HttpPost("/UpdateTipeFasilitas", Name = "UpdateTipeFasilitas")]
        public IActionResult UpdateTipeFasilitas([FromBody] TipeFasilitasModel tipeFasilitas)
        {
            try
            {
                var existingTipeFasilitas = tipeFasilitasRepository.GetTipeFasilitasById(tipeFasilitas.id_tipe_fasilitas);
                if (existingTipeFasilitas != null)
                {
                    tipeFasilitasRepository.UpdateTipeFasilitas(tipeFasilitas);
                    return Ok("Type of facility updated successfully");
                }
                else
                {
                    return NotFound($"Type of facility with ID {tipeFasilitas.id_tipe_fasilitas} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to update type of facility: {ex.Message}");
            }
        }

        [HttpPost("/DeleteTipeFasilitas", Name = "DeleteTipeFasilitas")]
        public IActionResult DeleteTipeFasilitas(int id)
        {
            try
            {
                var existingTipeFasilitas = tipeFasilitasRepository.GetTipeFasilitasById(id);
                if (existingTipeFasilitas != null)
                {
                    tipeFasilitasRepository.DeleteTipeFasilitas(id);
                    return Ok("Type of facility deleted successfully");
                }
                else
                {
                    return NotFound($"Type of facility with ID {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete type of facility: {ex.Message}");
            }
        }
    }
}
