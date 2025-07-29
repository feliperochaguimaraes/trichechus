namespace Trichechus.Domain.Entities
{
	public class Funcionalidade
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Nome { get; set; } = default!; // Ex: "CriarAtividade", "VerRelatorioX", "T_CAD_ABE_CTA"
		public string? Descricao { get; set; }

		// Navegação para Perfis
		public ICollection<Perfil> Perfil { get; set; } = new List<Perfil>();
	}
}
