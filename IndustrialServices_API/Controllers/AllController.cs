using IndustrialServices_API.Model;
using IndustrialServices_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace IndustrialServices_API.Controllers
{
    public class AllController : ControllerBase
    {
        private readonly All _allRepository;
        ResponseModel response = new ResponseModel();

        public AllController(IConfiguration configuration)
        {
            _allRepository = new All(configuration);
        }
        [HttpGet("/BigSearch", Name = "BigSearch")]
        public IActionResult BigSearch(string search)
        {
            try
            {
                var result = _allRepository.BigSearch(search);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve fasilitas: {ex.Message}");
            }
        }
    }
}
