using Microsoft.AspNetCore.Mvc;
using surgicalmanagement_back_end.Domain.Services;
using surgicalmanagement_back_end.MergedInfraApp.DTOs.OperationRequest;
using surgicalmanagement_back_end.MergedInfraApp.Exceptions;

namespace surgicalmanagement_back_end.Controllers
{
    //[Authorize()] //TODO: Implement roles
    [ApiController]
    [Route("api/[controller]")]
    public class OperationRequestController : ControllerBase
    {
        // Controller does not have data mapper in order to keep Single Responsibility Principle.
        private readonly IOperationRequestService _operationRequestService;
        
        public OperationRequestController(IOperationRequestService operationRequestService)
        {
            _operationRequestService = operationRequestService;
        }


        // Create OperationRequest
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOperationRequestDto dto)
        {
            // Validation
            if (dto == null)
                return BadRequest("Invalid operation request data.");
            if (dto.PatientId == Guid.Empty)
                return BadRequest("PatientId is required.");
            if (dto.DoctorId == Guid.Empty)
                return BadRequest("DoctorId is required.");
            if (dto.OperationTypeId == Guid.Empty)
                return BadRequest("OperationTypeId is required.");
            if (dto.RequestedDate < DateTime.UtcNow)
                return BadRequest("RequestedDate cannot be in the past.");

            try
            {
                var operationRequestDto = await _operationRequestService.CreateOperationRequest(dto);
                return CreatedAtAction(nameof(Get), new { id = operationRequestDto.Id }, operationRequestDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception (implementation depends on your logging setup)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get OperationRequest by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var operationRequestDto = await _operationRequestService.GetOperationRequest(id);

            if (operationRequestDto == null)
                return NotFound();

            return Ok(operationRequestDto);
        }

        // Get all OperationRequests
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requestDtos = await _operationRequestService.GetAllOperationRequests();
            return Ok(requestDtos);
        }
        
        // Update a OperationRequest
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OperationRequestDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid operation request data.");
            if (dto.Id != id) //TODO: is this necessary?
                return BadRequest("Mismatched ID in the URL and the body.");

            try
            {
                var updatedRequestDto = await _operationRequestService.UpdateOperationRequest(dto);
                if (updatedRequestDto == null)
                    return NotFound();

                return Ok(updatedRequestDto);
            }
            catch (NotFoundException)
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
            }
        }
        
        // Delete/Remove a OperationRequest by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // TODO: Notify the planning module and update any schedules that were using this request
            try
            {
                await _operationRequestService.DeleteOperationRequest(id);
                return NoContent(); // Return 204 No Content on successful deletion
            }
            catch (NotFoundException)
            {
                return NotFound(); // Return 404 if the operation request is not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}