using System.Data;
using Dapper;
using Importacao.Domain.Entities.Access;
using Importacao.Domain.Interfaces;

namespace Importacao.Infrastructure.Repository;

public class MidiaAcessoRepository(IDbConnection dbConnection) : IMidiaAcessoRepository {
	public async Task<MidiaAcesso> CreateMidiaAcesso(long  empresaId) {
		var sql = @"INSERT INTO MidiaAcesso 
				(DataHoraCadastro, OrigemString, EmpresaId) 
Output INSERTED.*
				VALUES (@DataHoraCadastro, @OrigemString, @EmpresaId)";

		var midia=  await dbConnection.QuerySingleAsync(sql, new {
			DataHoraCadastro = DateTime.Now,
			OrigemString = "F",
			EmpresaId = 1
		});
		return new MidiaAcesso(midia.Id, midia.OrigemString, midia.EmpresaId);
	}


	public async Task<MidiaAcesso?> GetById(long id) {
		var sql = $"SELECT Id FROM MidiaAcesso WHERE Id = @id";
		var result = await dbConnection.QueryFirstOrDefaultAsync<dynamic>(sql, new { id });
		if (result == null)
			return null;

		var MidiaAcesso = new MidiaAcesso(result.Id, result.OrigemString, result.EmpresaId);
		return MidiaAcesso;
	}
}