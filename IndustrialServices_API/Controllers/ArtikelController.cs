using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IndustrialServices_API.Models;
using IndustrialServices_API.Model;
using System;

namespace IndustrialServices_API.Controllers
{
    public class ArtikelController : ControllerBase
    {
        private readonly Artikel artikelRepository;
        ResponseModel response = new ResponseModel();

        public ArtikelController(IConfiguration configuration)
        {
            artikelRepository = new Artikel(configuration);
        }

        [HttpGet("/GetAllArtikel", Name = "GetAllArtikel")]
        public IActionResult GetAllArtikel()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = artikelRepository.GetAllArtikels();
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }
        [HttpGet("/GetAllArtikelsinHome", Name = "GetAllArtikelsinHome")]
        public IActionResult GetAllArtikelsinHome()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = artikelRepository.GetAllArtikelsinHome();
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpGet("/GetAllArtikelsDone", Name = "GetAllArtikelsDone")]
        public IActionResult GetAllArtikelsDone()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = artikelRepository.GetAllArtikelsDone();
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpGet("/GetArtikel", Name = "GetArtikel")]
        public IActionResult GetArtikel(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = artikelRepository.GetArtikelById(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpGet("/GetAnotherArtikel", Name = "GetAnotherArtikel")]
        public IActionResult GetAnotherArtikel(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = artikelRepository.GetAnotherArtikel(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpGet("/GetDetailArtikel", Name = "GetDetailArtikel")]
        public IActionResult GetDetailArtikel(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = artikelRepository.GetDetailArtikel(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPost("/InsertArtikel", Name = "InsertArtikel")]
        public IActionResult InsertArtikel([FromBody] ArtikelModel article)
        {
            try
            {
                if (!string.IsNullOrEmpty(article.sampul_artikel))
                {
                    string fileName = article.sampul_artikel;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    article.sampul_artikel = fileName;
                }

                response.status = 200;
                response.messages = "Success";  

                // Call the method to insert the article into the repository
                artikelRepository.InsertArtikel(article);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message;
            }
            return Ok(response);
        }

        [HttpPost("/UpdateArtikel", Name = "UpdateArtikel")]
        public IActionResult UpdateArtikel([FromBody] ArtikelModel artikel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(artikel.sampul_artikel))
                    {
                        string fileName = artikel.sampul_artikel;
                        fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                        artikel.sampul_artikel = fileName;
                    }

                    response.status = 200;
                    response.messages = "Success";
                    artikelRepository.UpdateArtikel(artikel);
                }
                else
                {
                    response.status = 400; // Bad Request
                    response.messages = "Invalid data provided.";
                }
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPost("/DeleteArtikel", Name = "DeleteArtikel")]
        public IActionResult DeleteArtikel(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                artikelRepository.DeleteArtikel(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPost("/DoneArtikel", Name = "DoneArtikel")]
        public IActionResult DoneArtikel(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                artikelRepository.DoneArtikel(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }
    }
}
