using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trichechus.Application.DTOs;

// DTO para exibir perfis (pode incluir funcionalidades)
public class PerfilDto
{
	public Guid Id { get; set; }
	public string Nome { get; set; } = default!;
	public string? Descricao { get; set; }
	public List<FuncionalidadeDto>? Funcionalidades { get; set; }
}

// DTO para criar perfis
public class CreatePerfilDto
{
	[Required(ErrorMessage = "O nome do perfil é obrigatório")]
	[StringLength(100)]
	public string Nome { get; set; } = default!;

	[StringLength(500)]
	public string? Descricao { get; set; }

	// Opcional: Lista de IDs de funcionalidades para associar ao perfil
	public List<Guid>? FuncionalidadeIds { get; set; }
}

// DTO para atualizar perfis
public class UpdatePerfilDto
{
	public Guid Id { get; set; }

	[Required(ErrorMessage = "O nome do perfil é obrigatório")]
	[StringLength(100)]
	public string Nome { get; set; } = default!;

	[StringLength(500)]
	public string? Descricao { get; set; }

	// Opcional: Lista de IDs de funcionalidades para associar ao perfil
	public List<Guid>? FuncionalidadeIds { get; set; }
}

// DTO para associar/desassociar funcionalidade de um perfil
public class AssociarFuncionalidadePerfilDto
{
	[Required]
	public Guid FuncionalidadeId { get; set; }
}
