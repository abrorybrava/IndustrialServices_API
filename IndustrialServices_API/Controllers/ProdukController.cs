using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IndustrialServices_API.Models;

namespace IndustrialServices_API.Controllers
{
    public class ProdukController : ControllerBase
    {
        private readonly Produk produkRepository;

        public ProdukController(IConfiguration configuration)
        {
            produkRepository = new Produk(configuration);
        }

        [HttpGet("/GetAllProduk", Name = "GetAllProduk")]
        public IActionResult GetAllProduk()
        {
            try
            {
                var produkList = produkRepository.GetAllProduk();
                return Ok(produkList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve products: {ex.Message}");
            }
        }

        [HttpGet("/GetProduk", Name = "GetProduk")]
        public IActionResult GetProduk(int id)
        {
            try
            {
                    var produk = produkRepository.GetProdukById(id);
                    return Ok(produk);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve product: {ex.Message}");
            }
        }
        [HttpPost("/InsertProduk", Name = "InsertProduk")]
        public IActionResult InsertProduk([FromBody] ProdukModel produk)
        {
            try
            {
                produkRepository.InsertProduk(produk);
                return Ok("Product inserted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to insert product: {ex.Message}");
            }
        }
        [HttpPost("/UpdateProduk", Name = "UpdateProduk")]
        public IActionResult UpdateProduk([FromBody] ProdukModel produk)
        {
            try
            {
                var existingProduk = produkRepository.GetProdukById(produk.id_produk);
                if (existingProduk != null)
                {
                    produkRepository.UpdateProduk(produk);
                    return Ok("Product updated successfully");
                }
                else
                {
                    return NotFound($"Product with ID {produk.id_produk} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to update product: {ex.Message}");
            }
        }

        [HttpPost("/DeleteProduk", Name = "DeleteProduk")]
        public IActionResult DeleteProduk(int id)
        {
            try
            {
                var existingProduk = produkRepository.GetProdukById(id);
                if (existingProduk != null)
                {
                    produkRepository.DeleteProduk(id);
                    return Ok("Product deleted successfully");
                }
                else
                {
                    return NotFound($"Product with ID {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete product: {ex.Message}");
            }
        }
        [HttpGet("/GetAllProdukinWeb", Name = "GetAllProdukinWeb")]
        public IActionResult GetAllProdukinWeb()
        {
            try
            {
                var produkList = produkRepository.GetAllProdukinWeb();
                return Ok(produkList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve fasilitas: {ex.Message}");
            }
        }
    }
}
