using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IndustrialServices_API.Models;
using IndustrialServices_API.Model;

namespace IndustrialServices_API.Controllers
{
    public class TipeFasilitasController : ControllerBase
    {
        private readonly TipeFasilitas tipeFasilitasRepository;
        ResponseModel response = new ResponseModel();

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
                if (!tipeFasilitasRepository.CheckNama(tipeFasilitas))
                {
                    response.status = 200;
                    response.messages = "Success";
                    tipeFasilitasRepository.InsertTipeFasilitas(tipeFasilitas);
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

        [HttpPost("/UpdateTipeFasilitas", Name = "UpdateTipeFasilitas")]
        public IActionResult UpdateTipeFasilitas([FromBody] TipeFasilitasModel tipeFasilitas)
        {
            try
            {
                var existingTipeFasilitas = tipeFasilitasRepository.GetTipeFasilitasById(tipeFasilitas.id_tipe_fasilitas);
                if (existingTipeFasilitas != null)
                {
                    if (!tipeFasilitasRepository.CheckNamaEdit(tipeFasilitas))
                    {
                        response.status = 200;
                        response.messages = "Success";
                        tipeFasilitasRepository.UpdateTipeFasilitas(tipeFasilitas);
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
                    return NotFound($"Type of facility with ID {tipeFasilitas.id_tipe_fasilitas} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to update type of facility: {ex.Message}");
            }
            return Ok(response);
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
