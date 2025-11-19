using Importacao.Application.DTOs;
using Importacao.Application.Interfaces;
using Importacao.Domain.Entities.Profiles;
using Importacao.Domain.Interfaces;
using Importacao.Domain.ValueObject;

namespace Importacao.Application.Services;

public class EmployeeService(IMidiaAcessoService midiaAcessoService, IPersonService personService, IEmployeeRepository employeeRepository) : IEmployeeService {
	public async Task<List<Employee>> HandleCreateEmployee(PersonListDto model) {
		var persons = new List<Person>();
		var employees = new List<Employee>();

		foreach (var person in model.Persons) persons.Add(new Person(person.Nome, new Document(person.Documento), person.GrupoId, person.Matricula, person.DepartamentoId, person.EmpresaId));
		for (var i = persons.Count - 1; i >= 0; i--) {
			var midia = await midiaAcessoService.HandleCreateMidiaAcesso(persons[i].GrupoId);
			var person = await personService.HandleVerifyPerson(persons[i]);
			var employee = await HandleVerifyEmployee(persons[i].Documento.Cpf);

			if (employee != null)
				await employeeRepository.UpdateEmployee(employee.Id,persons[i].GrupoId);
			employee ??= await HandleInsertEmployee(person.Id, persons[i].GrupoId, persons[i].Matricula, midia.Id, persons[i].DepartamentoId, persons[i].EmpresaId);
			employees.Add(employee);
		}
		return employees;
	}

	private async Task<Employee?> HandleVerifyEmployee(string? personDocument) {
		var employeeDb = await employeeRepository.GetEmployeeByDocument(personDocument);
		return employeeDb;
	}

	private async Task<Employee> HandleInsertEmployee(long personId, long grupoId, string? matricula, long midiaId, long departamentoId, long empresaId) {
		return await employeeRepository.CreateEmployee(personId, grupoId, matricula, midiaId, departamentoId, empresaId);
	}
 }