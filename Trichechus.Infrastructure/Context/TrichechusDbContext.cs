using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;

namespace Trichechus.Infrastructure.Context;

public class TrichechusDbContext : DbContext
{
	public TrichechusDbContext(DbContextOptions<TrichechusDbContext> options)
		: base(options)
	{
	}

	public DbSet<Atividade> Atividade => Set<Atividade>();
	public DbSet<BaseDados> BaseDados  => Set<BaseDados>();
	public DbSet<Catalogo> Catalogo  => Set<Catalogo>();
	public DbSet<Contrato> Contrato  => Set<Contrato>();
	public DbSet<Fornecedor> Fornecedor  => Set<Fornecedor>();
	public DbSet<Funcionalidade> Funcionalidade => Set<Funcionalidade>();
	public DbSet<Perfil> Perfil => Set<Perfil>();
	public DbSet<Repositorio> Repositorio  => Set<Repositorio>();
	public DbSet<Software> Software  => Set<Software>();
	public DbSet<Tarefa> Tarefa => Set<Tarefa>();
	public DbSet<URL> URL => Set<URL>();
	public DbSet<Usuario> Usuario => Set<Usuario>();


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Atividade>(entity =>
		{
			entity.HasKey(a => a.Id);
			entity.Property(a => a.Titulo).IsRequired().HasMaxLength(200);
			entity.Property(a => a.Descricao).HasMaxLength(1000);
		});

		modelBuilder.Entity<Tarefa>(entity =>
		{
			entity.HasKey(t => t.Id);
			entity.Property(t => t.Titulo).IsRequired().HasMaxLength(200);
			entity.HasOne(t => t.Atividade)
				  .WithMany(a => a.Tarefa)
				  .HasForeignKey(t => t.AtividadeId);
		});

		// Configuração Funcionalidade
		modelBuilder.Entity<Funcionalidade>(entity =>
		{
			entity.HasKey(f => f.Id);
			entity.Property(f => f.Nome).IsRequired().HasMaxLength(100);
			entity.HasIndex(f => f.Nome).IsUnique(); // Garante nomes únicos
			entity.Property(f => f.Descricao).HasMaxLength(500);
		});

		// Configuração Perfil
		modelBuilder.Entity<Perfil>(entity =>
		{
			entity.HasKey(p => p.Id);
			entity.Property(p => p.Nome).IsRequired().HasMaxLength(100);
			entity.HasIndex(p => p.Nome).IsUnique(); // Garante nomes únicos
			entity.Property(p => p.Descricao).HasMaxLength(500);

			// Relacionamento Muitos-para-Muitos com Funcionalidade
			entity.HasMany(p => p.Funcionalidade)
				  .WithMany(f => f.Perfil)
				  .UsingEntity(j => j.ToTable("PerfilFuncionalidade")); // Tabela de junção
		});

		// Configuração Usuario
		modelBuilder.Entity<Usuario>(entity =>
		{
			entity.HasKey(u => u.Id);
			entity.Property(u => u.Nome).IsRequired().HasMaxLength(200);
			entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
			entity.HasIndex(u => u.Email).IsUnique(); // Garante emails únicos
			entity.Property(u => u.SenhaHash).IsRequired();
			entity.Property(u => u.Ativo).IsRequired();

			// Relacionamento Muitos-para-Muitos com Perfil
			entity.HasMany(u => u.Perfil)
				  .WithMany(p => p.Usuario)
				  .UsingEntity(j => j.ToTable("UsuarioPerfil")); // Tabela de junção
		});
	}
}