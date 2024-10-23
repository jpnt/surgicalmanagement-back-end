using Microsoft.AspNetCore.Mvc;
using surgicalmanagement_back_end.Domain.Services;
using surgicalmanagement_back_end.MergedInfraApp.DTOs.OperationRequest;

namespace surgicalmanagement_back_end.Controllers
{
    // TODO: See if the [Required] attribute in Dtos can be useful for cleaner code
    [ApiController]
    //[Authorize(Roles = "Doctor")]
    [Route("api/[controller]")]
    public class OperationRequestController : ControllerBase
    {
        private readonly IOperationRequestService _operationRequestService;
        
        public OperationRequestController(IOperationRequestService operationRequestService)
        {
            _operationRequestService = operationRequestService;
        }


        // Create OperationRequest
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOperationRequestDto dto)
        {
            if (dto is null)
                return BadRequest("Invalid operation request data.");
            
            var result = await _operationRequestService.CreateOperationRequest(dto);
            if (result.IsFailure)
                return BadRequest(result.Error); // Error messages handled via Result<T>.Error

            // Status 201 Created
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result);
        }


        // Get OperationRequest by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var operationRequestDto = await _operationRequestService.GetOperationRequest(id);
            if (operationRequestDto == null)
                return NotFound();

            return Ok(operationRequestDto.Value);
        }

        
        // Get all OperationRequests
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requestDtos = await _operationRequestService.GetAllOperationRequests();
            return Ok(requestDtos.Value);
        }
        
        
        // Update a OperationRequest
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OperationRequestDto dto)
        {
            if (dto == null || dto.Id != id)
                return BadRequest("Invalid operation request data or mismatched ID.");

            var result = await _operationRequestService.UpdateOperationRequest(dto);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            
            return Ok(result.Value);
            /*try
            {
                var updatedRequestDto = await _operationRequestService.UpdateOperationRequest(dto);
                if (updatedRequestDto == null)
                    return NotFound();

                return Ok(updatedRequestDto);
            }
            catch (NotFoundException) // TODO: result pattern instead of exceptions
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid(); // 403 Forbidden
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }*/
        }
        
        // Delete/Remove a OperationRequest by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _operationRequestService.DeleteOperationRequest(id);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }
    }
}