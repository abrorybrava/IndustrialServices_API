using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using IndustrialServices_API.Models;
using IndustrialServices_API.Model;
using System;

[ApiController]
[Route("api/[controller]")]
public class CustomerMappingController : ControllerBase
{
    private readonly CustomerMapping _customerMappingRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly ResponseModel _response;

    public CustomerMappingController(IConfiguration configuration)
    {
        _customerMappingRepository = new CustomerMapping(configuration);
        _response = new ResponseModel();
    }

    [HttpGet("/GetAllCustomerMapping", Name = "GetAllCustomerMapping")]
    public IActionResult GetAllCustomerMapping()
    {
        try
        {
            _response.status = 200;
            _response.messages = "Success";
            _response.data = _customerMappingRepository.GetAllCustomerMapping();
        }
        catch (Exception ex)
        {
            _response.status = 500;
            _response.messages = "Failed, " + ex;
        }
        return Ok(_response);
    }

    [HttpGet("/GetCustomerMapping", Name = "GetCustomerMapping")]
    public IActionResult GetCustomerMapping(int id)
    {
        try
        {
            _response.status = 200;
            _response.messages = "Success";
            _response.data = _customerMappingRepository.GetCustomerMappingById(id);
        }
        catch (Exception ex)
        {
            _response.status = 500;
            _response.messages = "Failed, " + ex;
        }
        return Ok(_response);
    }

    [HttpPost("/InsertCustomerMapping", Name = "InsertCustomerMapping")]
    public IActionResult InsertCustomerMapping([FromBody] CustomerMappingModel customerMapping)
    {
        try
        {
            if (!string.IsNullOrEmpty(customerMapping.path_logo))
            {
                string fileName = customerMapping.path_logo;
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                customerMapping.path_logo = fileName;
            }

            if (!_customerMappingRepository.CheckLogo(customerMapping))
            {
                _response.status = 200;
                _response.messages = "Success";
                _customerMappingRepository.InsertCustomerMapping(customerMapping);
            }
            else
            {
                _response.status = 500;
                _response.messages = "Logo was already added!";
                return Ok(_response);
            }
        }
        catch (Exception ex)
        {
            _response.status = 500;
            _response.messages = "Failed, " + ex;
        }
        return Ok(_response);
    }

    [HttpPost("/DeleteCustomerMapping", Name = "DeleteCustomerMapping")]
    public IActionResult DeleteCustomerMapping(int id)
    {
        try
        {
            // Your logic for processing the customer mapping deletion

            _response.status = 200;
            _response.messages = "Success";
            _customerMappingRepository.DeleteCustomerMapping(id);
        }
        catch (Exception ex)
        {
            _response.status = 500;
            _response.messages = "Failed, " + ex;
        }
        return Ok(_response);
    }
}
