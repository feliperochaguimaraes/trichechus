
using AutoMapper;
using Trichechus.Application.DTOs;
using Trichechus.Domain.Entities;

namespace Trichechus.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// CreateMap<CreateAtividadeCommand, Atividade>();
			// Outros mapeamentos podem ser adicionados aqui
			CreateMap<Atividade, AtividadeDto>().ReverseMap();
			CreateMap<BaseDados, BaseDadosDto>().ReverseMap();
			CreateMap<Catalogo, CatalogoDto>().ReverseMap();
			CreateMap<Contrato, ContratoDto>().ReverseMap();
			CreateMap<Fornecedor, FornecedorDto>().ReverseMap();
			CreateMap<Funcionalidade, FuncionalidadeDto>().ReverseMap();
			CreateMap<Perfil, PerfilDto>().ReverseMap();
			CreateMap<Repositorio, RepositorioDto>().ReverseMap();
			CreateMap<Software, SoftwareDto>().ReverseMap();
			CreateMap<Tarefa, TarefaDto>().ReverseMap();
		}
	}
}