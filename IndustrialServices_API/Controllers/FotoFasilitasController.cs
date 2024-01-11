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
    public class FotoFasilitasController : ControllerBase
    {
        private readonly FotoFasilitas _fotofasilitasRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FotoFasilitasController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _fotofasilitasRepository = new FotoFasilitas(configuration); // Sesuaikan dengan inisialisasi yang benar untuk FotoProduk
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("/SendPhotoFasilitas", Name = "SendPhotoFasilitas")]
        public IActionResult SendPhotoFasilitas([FromBody] List<FotoFasilitasModel> fotofasilitas)
        {
            try
            {
                foreach (var foto in fotofasilitas)
                {
                    if (!string.IsNullOrEmpty(foto.path_foto_fasilitas))
                    {
                        string fileName = foto.path_foto_fasilitas;
                        fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                        foto.path_foto_fasilitas = fileName;
                    }
                }

                var response = new ResponseModel();
                response.status = 200;
                response.messages = "Success";
                _fotofasilitasRepository.insertphotofasilitas(fotofasilitas); // Memasukkan foto produk ke repositori

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
        [HttpGet("/GetPhotoFasilitas", Name = "GetPhotoFasilitas")]
        public IActionResult GetPhotoFasilitas(int id)
        {
            try
            {
                List<FotoFasilitasModel> fotoFasilitas = _fotofasilitasRepository.getPhotoFasilitas(id);

                if (fotoFasilitas == null || fotoFasilitas.Count == 0)
                {
                    return NotFound("No photo data found for the given ID.");
                }

                return Ok(fotoFasilitas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve photo data: {ex.Message}");
            }
        }
    }
}