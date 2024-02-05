using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IndustrialServices_API.Models;
using System;

namespace IndustrialServices_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FasilitasController : ControllerBase
    {
        private readonly Fasilitas _fasilitasRepository;

        public FasilitasController(IConfiguration configuration)
        {
            _fasilitasRepository = new Fasilitas(configuration);
        }

        [HttpGet("/GetAllFasilitas", Name = "GetAllFasilitas")]
        public IActionResult GetAllFasilitas()
        {
            try
            {
                var fasilitasList = _fasilitasRepository.GetAllFasilitas();
                return Ok(fasilitasList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve fasilitas: {ex.Message}");
            }
        }
        [HttpGet("/GetAllFasilitasinWeb", Name = "GetAllFasilitasinWeb")]
        public IActionResult GetAllFasilitasinWeb()
        {
            try
            {
                var fasilitasList = _fasilitasRepository.GetAllFasilitasinWeb();
                return Ok(fasilitasList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve fasilitas: {ex.Message}");
            }
        }

        [HttpGet("/GetFasilitasDetails", Name = "GetFasilitasDetails")]
        public IActionResult GetFasilitasDetails(int id)
        {
            try
            {
                var fasilitas = _fasilitasRepository.GetFasilitasDetails(id);
                return Ok(fasilitas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve fasilitas: {ex.Message}");
            }
        }

        [HttpGet("/GetFasilitas", Name = "GetFasilitas")]
        public IActionResult GetFasilitas(int id)
        {
            try
            {
                var fasilitas = _fasilitasRepository.GetFasilitasById(id);
                return Ok(fasilitas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve fasilitas: {ex.Message}");
            }
        }

        [HttpPost("/InsertFasilitas", Name = "InsertFasilitas")]
        public IActionResult InsertFasilitas([FromBody] FasilitasModel fasilitas)
        {
            try
            {

                _fasilitasRepository.InsertFasilitas(fasilitas);
                return Ok("Fasilitas inserted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to insert fasilitas: {ex.Message}");
            }
        }

        [HttpPost("/UpdateFasilitas", Name = "UpdateFasilitas")]
        public IActionResult UpdateFasilitas([FromBody] FasilitasModel fasilitas)
        {
            try
            {

                    _fasilitasRepository.UpdateFasilitas(fasilitas);
                    return Ok("Fasilitas updated successfully");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to update fasilitas: {ex.Message}");
            }
        }

        [HttpPost("/DeleteFasilitas", Name = "DeleteFasilitas")]
        public IActionResult DeleteFasilitas(int id)
        {
            try
            {
                var existingFasilitas = _fasilitasRepository.GetFasilitasById(id);
                if (existingFasilitas != null)
                {
                    _fasilitasRepository.DeleteFasilitas(id);
                    return Ok("Fasilitas deleted successfully");
                }
                else
                {
                    return NotFound($"Fasilitas with ID {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete fasilitas: {ex.Message}");
            }
        }
    }
}
