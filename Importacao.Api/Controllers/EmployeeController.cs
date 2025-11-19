using Importacao.Application.DTOs;
using Importacao.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Importacao.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase {
	[HttpPost]
	public async Task<IActionResult> CreateEmployee([FromBody]PersonListDto employeeDto) {
		return Ok(await employeeService.HandleCreateEmployee(employeeDto));
	}
}