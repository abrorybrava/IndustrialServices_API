using IndustrialServices_API.Model;
using IndustrialServices_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace IndustrialServices_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FotoPelatihanController : ControllerBase
    {
        private readonly FotoPelatihan _fotopelatihanRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FotoPelatihanController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _fotopelatihanRepository = new FotoPelatihan(configuration); // Sesuaikan dengan inisialisasi yang benar untuk FotoProduk
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("/SendPhotoPelatihan", Name = "SendPhotoPelatihan")]
        public IActionResult SendPhotoPelatihan([FromBody] List<FotoPelatihanModel> fotopelatihan)
        {
            try
            {
                foreach (var foto in fotopelatihan)
                {
                    if (!string.IsNullOrEmpty(foto.path_foto_pelatihan))
                    {
                        string fileName = foto.path_foto_pelatihan;
                        fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                        foto.path_foto_pelatihan = fileName;
                    }
                }

                var response = new ResponseModel();
                response.status = 200;
                response.messages = "Success";
                _fotopelatihanRepository.insertphotopelatihan(fotopelatihan); // Memasukkan foto produk ke repositori

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
        [HttpGet("/GetPhotoPelatihan", Name = "GetPhotoPelatihan")]
        public IActionResult GetPhotoPelatihan(int id)
        {
            try
            {
                List<FotoPelatihanModel> fotoPelatihan = _fotopelatihanRepository.getPhotoPelatihan(id);

                if (fotoPelatihan == null || fotoPelatihan.Count == 0)
                {
                    return NotFound("No photo data found for the given ID.");
                }

                return Ok(fotoPelatihan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve photo data: {ex.Message}");
            }
        }
    }
}
