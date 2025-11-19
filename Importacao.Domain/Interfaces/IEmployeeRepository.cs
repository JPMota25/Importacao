using Importacao.Domain.Entities.Profiles;

namespace Importacao.Domain.Interfaces;

public interface IEmployeeRepository {
	Task<Employee> CreateEmployee(long pessoaId, long grupoId, string? matricula, long midiaAcessoId, long  departamentoId, long empresaId);
	Task<Employee?> GetEmployeeByDocument(string document);
	Task UpdateEmployee(long funcionario,long grupoId);
}