using System.Text.Json.Serialization;
using Importacao.Application.Services;

namespace Importacao.Application.DTOs;

public class PersonListDto {
	public IList<PersonDto> Persons { get; set; }
}

public class PersonDto(string nome, string documento, string? matricula, long grupoId, long departamentoId, long empresaId) {
	public string Nome { get; set; } = nome;

	[JsonConverter(typeof(DocumentConverter))]
	public string Documento { get; set; } = documento;

	[JsonConverter(typeof(DocumentConverter))]
	public string? Matricula { get; private set; } = matricula;

	public long DepartamentoId { get; set; } = departamentoId;

	public long GrupoId { get; set; } = grupoId;
	public long EmpresaId { get; set; } = empresaId;
}