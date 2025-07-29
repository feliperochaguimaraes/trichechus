
namespace Trichechus.Domain.Entities
{
	public class Perfil
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Nome { get; set; } = default!; // Ex: "Administrador Local", "Usuário Padrão"
		public string? Descricao { get; set; }

		// Navegação para Funcionalidades (Muitos-para-Muitos)
		public ICollection<Funcionalidade> Funcionalidade { get; set; } = new List<Funcionalidade>();

		// Navegação para Usuários (Muitos-para-Muitos)
		public ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
	}
}
