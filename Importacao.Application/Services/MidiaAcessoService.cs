using Importacao.Application.Interfaces;
using Importacao.Domain.Entities.Access;
using Importacao.Domain.Interfaces;

namespace Importacao.Application.Services;

public class MidiaAcessoService(IMidiaAcessoRepository midiaAcessoRepository) : IMidiaAcessoService {
	public async Task<MidiaAcesso> HandleCreateMidiaAcesso(long empresa) {
		return await midiaAcessoRepository.CreateMidiaAcesso(empresa);
	}
	
}