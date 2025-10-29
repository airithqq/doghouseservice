using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using TestTask.Application.DTO;
using TestTask.Domain.Interfaces;
using TestTask.Domain.Models;

namespace TestTask.Controllers
{
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogsService _dogService;
        private readonly IMapper _mapper;
        public DogsController(IDogsService dogService, IMapper mapper)
        {
            _dogService = dogService;
            _mapper = mapper;
            
        }

        [HttpGet("dogs")] 
        public async Task<IActionResult> GetAll(
            [FromQuery] string? attribute,
            [FromQuery] string? order,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var dogs = await _dogService.GetAllDogsAsync(attribute, order, pageNumber, pageSize);
            var responseList = _mapper.Map<List<ResponseDogDTO>>(dogs);
            return Ok(responseList);
        }

        [HttpPost("dog")]
        public async Task<IActionResult> Create([FromBody] CreateDogDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dogEntity = _mapper.Map<DogEntity>(dto);
            var created = await _dogService.CreateDogAsync(dogEntity);
            var response = _mapper.Map<ResponseDogDTO>(created);
            if (created == null) return BadRequest("Input correct values");
            return Ok(created);
        }

        [HttpGet("ping")]
        public string Ping()
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();
            var version = Assembly.GetExecutingAssembly().GetName().Version!;
            var info = $"{assembly.Name}.Version{version.ToString(3)}";
            return info;
        }
    }
}
