using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IndustrialServices_API.Models;
using System;

namespace IndustrialServices_API.Controllers
{
    public class PelatihanController : ControllerBase
    {
        private readonly Pelatihan pelatihanRepository;

        public PelatihanController(IConfiguration configuration)
        {
            pelatihanRepository = new Pelatihan(configuration);
        }

        [HttpGet("/GetAllPelatihan", Name = "GetAllPelatihan")]
        public IActionResult GetAllPelatihan()
        {
            try
            {
                var pelatihanList = pelatihanRepository.GetAllPelatihan();
                return Ok(pelatihanList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve pelatihan: {ex.Message}");
            }
        }

        [HttpGet("/GetPelatihan", Name = "GetPelatihan")]
        public IActionResult GetPelatihan(int id)
        {
            try
            {
                var pelatihan = pelatihanRepository.GetPelatihanById(id);
                return Ok(pelatihan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve pelatihan: {ex.Message}");
            }
        }

        [HttpGet("/GetFasilitasPelatihan", Name = "GetFasilitasPelatihan")]
        public IActionResult GetFasilitasPelatihan(int id)
        {
            try
            {
                var pelatihan = pelatihanRepository.GetFasilitasPelatihan(id);
                return Ok(pelatihan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve fasilitas: {ex.Message}");
            }
        }

        [HttpPost("/InsertPelatihan", Name = "InsertPelatihan")]
        public IActionResult InsertPelatihan([FromBody] PelatihanModel pelatihan)
        {
            try
            {
                pelatihanRepository.InsertPelatihan(pelatihan);
                return Ok("Pelatihan inserted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to insert pelatihan: {ex.Message}");
            }
        }

        [HttpPost("/InsertPengajarPelatihan", Name = "InsertPengajarPelatihan")]
        public IActionResult InsertPengajarPelatihan([FromBody] List<PelatihanModel> pelatihan)
        {
            try
            {
                pelatihanRepository.InsertPengajarPelatihan(pelatihan);
                return Ok("Pelatihan inserted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to insert pelatihan: {ex.Message}");
            }
        }


        [HttpGet("/GetPengajarPelatihan", Name = "GetPengajarPelatihan")]
        public IActionResult GetPengajarPelatihan(int id)
        {
            try
            {
                var pelatihan = pelatihanRepository.GetPengajarPelatihan(id);
                return Ok(pelatihan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve fasilitas: {ex.Message}");
            }
        }


        [HttpPost("/UpdatePelatihan", Name = "UpdatePelatihan")]
        public IActionResult UpdatePelatihan([FromBody] PelatihanModel pelatihan)
        {
            try
            {
                var existingPelatihan = pelatihanRepository.GetPelatihanById(pelatihan.id_pelatihan);
                if (existingPelatihan != null)
                {
                    pelatihanRepository.UpdatePelatihan(pelatihan);
                    return Ok("Pelatihan updated successfully");
                }
                else
                {
                    return NotFound($"Pelatihan with ID {pelatihan.id_pelatihan} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to update pelatihan: {ex.Message}");
            }
        }

        [HttpPost("/DeletePelatihan", Name = "DeletePelatihan")]
        public IActionResult DeletePelatihan(int id)
        {
            try
            {
                var existingPelatihan = pelatihanRepository.GetPelatihanById(id);
                if (existingPelatihan != null)
                {
                    pelatihanRepository.DeletePelatihan(id);
                    return Ok("Pelatihan deleted successfully");
                }
                else
                {
                    return NotFound($"Pelatihan with ID {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete pelatihan: {ex.Message}");
            }
        }
    }
}
