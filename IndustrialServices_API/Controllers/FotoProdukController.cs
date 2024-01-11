using IndustrialServices_API.Model;
using IndustrialServices_API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace IndustrialServices_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FotoProdukController : ControllerBase
    {
        private readonly FotoProduk _fotoprodukRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FotoProdukController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _fotoprodukRepository = new FotoProduk(configuration); // Sesuaikan dengan inisialisasi yang benar untuk FotoProduk
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("/SendPhotoProduk", Name = "SendPhotoProduk")]
        public IActionResult SendPhotoProduk([FromBody] List<FotoProdukModel> fotoproduk)
        {
            try
            {
                foreach (var foto in fotoproduk)
                {
                    if (!string.IsNullOrEmpty(foto.path_foto_produk))
                    {
                        string fileName = foto.path_foto_produk;
                        fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                        foto.path_foto_produk = fileName;
                    }
                }

                var response = new ResponseModel();
                response.status = 200;
                response.messages = "Success";
                _fotoprodukRepository.insertphotoproduk(fotoproduk); // Memasukkan foto produk ke repositori

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseModel();
                response.status = 500;
                response.messages = "Failed, " + ex.Message;

                return StatusCode(500, response);
            }
        }
        [HttpGet("/GetPhotoProduk", Name = "GetPhotoProduk")]
        public IActionResult GetPhotoProduk(int id) 
        {
            try
            {
                List<FotoProdukModel> fotoProduk = _fotoprodukRepository.getPhotoProduk(id);

                if (fotoProduk == null || fotoProduk.Count == 0)
                {
                    return NotFound("No photo data found for the given ID.");
                }

                return Ok(fotoProduk);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve photo data: {ex.Message}");
            }
        }
    }
}