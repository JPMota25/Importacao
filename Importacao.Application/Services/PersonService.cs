using Importacao.Application.DTOs;
using Importacao.Application.Interfaces;
using Importacao.Domain.Entities.Profiles;
using Importacao.Domain.Interfaces;
using Importacao.Domain.ValueObject;

namespace Importacao.Application.Services;

public class PersonService(IPersonRepository personRepository) : IPersonService {
	public async Task<List<Person>> HandleVerifyPersons(PersonListDto model) {
		var persons = new List<Person>();
		foreach (var person in model.Persons) persons.Add(new Person(person.Nome, new Document(person.Documento), person.GrupoId, person.Matricula, person.DepartamentoId, person.EmpresaId));
		for (var i = persons.Count - 1; i >= 0; i--) {
			if (persons[i].Documento != null) {
				var personDb = await personRepository.GetByDocument(persons[i].Documento);
				if (persons[i].IsValid == false && persons[i].Documento != null  || (personDb != null && personDb.Documento == persons[i].Documento)) {
					persons.RemoveAt(i);
				}
			}
		}
		return persons;
	}

	public async Task<Person> HandleVerifyPerson(Person person) {
		var personDb = await personRepository.GetByDocument(person.Documento);
		if (personDb == null || person.Documento == null) return await HandleInsertPerson(person);
		await personRepository.UpdateName(person.Documento, person.Nome);
		return personDb;
	}

	public async Task HandleInsertPersons(List<Person> persons) {
		foreach (var person in persons) await personRepository.InsertPerson(person);
	}

	private async Task<Person> HandleInsertPerson(Person person) {
		return await personRepository.InsertAndGetPerson(person);
	}
}