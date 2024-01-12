using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IndustrialServices_API.Models;

namespace IndustrialServices_API.Controllers
{
    public class TipeProdukController : ControllerBase
    {
        private readonly TipeProduk tipeProdukRepository;

        public TipeProdukController(IConfiguration configuration)
        {
            tipeProdukRepository = new TipeProduk(configuration);
        }

        [HttpGet("/GetAllTipeProduk", Name = "GetAllTipeProduk")]
        public IActionResult GetAllTipeProduk()
        {
            try
            {
                var tipeProdukList = tipeProdukRepository.GetAllTipeProduk();
                return Ok(tipeProdukList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve types of products: {ex.Message}");
            }
        }

        [HttpGet("/GetTipeProduk", Name = "GetTipeProduk")]
        public IActionResult GetTipeProduk(int id)
        {
            try
            {
                var tipeProduk = tipeProdukRepository.GetTipeProdukById(id);
                return Ok(tipeProduk);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve type of product: {ex.Message}");
            }
        }

        [HttpPost("/InsertTipeProduk", Name = "InsertTipeProduk")]
        public IActionResult InsertTipeProduk([FromBody] TipeProdukModel tipeProduk)
        {
            try
            {
                tipeProdukRepository.InsertTipeProduk(tipeProduk);
                return Ok("Type of product inserted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to insert type of product: {ex.Message}");
            }
        }

        [HttpPost("/UpdateTipeProduk", Name = "UpdateTipeProduk")]
        public IActionResult UpdateTipeProduk([FromBody] TipeProdukModel tipeProduk)
        {
            try
            {
                var existingTipeProduk = tipeProdukRepository.GetTipeProdukById(tipeProduk.id_tipe_produk);
                if (existingTipeProduk != null)
                {
                    tipeProdukRepository.UpdateTipeProduk(tipeProduk);
                    return Ok("Type of product updated successfully");
                }
                else
                {
                    return NotFound($"Type of product with ID {tipeProduk.id_tipe_produk} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to update type of product: {ex.Message}");
            }
        }

        [HttpPost("/DeleteTipeProduk", Name = "DeleteTipeProduk")]
        public IActionResult DeleteTipeProduk(int id)
        {
            try
            {
                var existingTipeProduk = tipeProdukRepository.GetTipeProdukById(id);
                if (existingTipeProduk != null)
                {
                    tipeProdukRepository.DeleteTipeProduk(id);
                    return Ok("Type of product deleted successfully");
                }
                else
                {
                    return NotFound($"Type of product with ID {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete type of product: {ex.Message}");
            }
        }
    }
}
