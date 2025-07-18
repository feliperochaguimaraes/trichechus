﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trichechus.Infrastructure.Context;

#nullable disable

namespace Trichechus.Infrastructure.Migrations
{
    [DbContext(typeof(TrichechusDbContext))]
    [Migration("20250708202646_fornecedor_contrato")]
    partial class fornecedor_contrato
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.15");

            modelBuilder.Entity("ContratoFornecedor", b =>
                {
                    b.Property<Guid>("ContratoId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FornecedorId")
                        .HasColumnType("TEXT");

                    b.HasKey("ContratoId", "FornecedorId");

                    b.HasIndex("FornecedorId");

                    b.ToTable("ContratoFornecedor");
                });

            modelBuilder.Entity("FuncionalidadePerfil", b =>
                {
                    b.Property<Guid>("FuncionalidadesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PerfisId")
                        .HasColumnType("TEXT");

                    b.HasKey("FuncionalidadesId", "PerfisId");

                    b.HasIndex("PerfisId");

                    b.ToTable("PerfilFuncionalidade", (string)null);
                });

            modelBuilder.Entity("PerfilUsuarioLocal", b =>
                {
                    b.Property<Guid>("PerfisId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UsuariosId")
                        .HasColumnType("TEXT");

                    b.HasKey("PerfisId", "UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("UsuarioPerfil", (string)null);
                });

            modelBuilder.Entity("Trichechus.Domain.Entities.Atividade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("AtualizadoEm")
                        .HasColumnType("DateTime2")
                        .HasColumnName("AtualizadoEm");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("DateTime2")
                        .HasColumnName("CriadoEm");

                    b.Property<DateTime?>("DeletadoEm")
                        .HasColumnType("DateTime2")
                        .HasColumnName("DeletadoEm");

                    b.Property<string>("Descricao")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeEquipeResponsavel")
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeResponsavel")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Prazo")
                        .HasColumnType("DateTime2")
                        .HasColumnName("Prazo");

                    b.Property<string>("Situacao")
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoEntrada")
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Atividades");
                });

            modelBuilder.Entity("Trichechus.Domain.Entities.Contrato", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fim")
                        .HasColumnType("DateTime2")
                        .HasColumnName("Fim");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("DateTime2")
                        .HasColumnName("Inicio");

                    b.Property<string>("NomeAlias")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("NomeAlias");

                    b.Property<string>("Objeto")
                        .IsRequired()
                        .HasColumnType("Text")
                        .HasColumnName("Objeto");

                    b.HasKey("Id");

                    b.ToTable("Contratos");
                });

            modelBuilder.Entity("Trichechus.Domain.Entities.Fornecedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Ativo");

                    b.Property<string>("CPFCNPJ")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("CPFCNPJ");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("Cep");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("Cidade");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Endereco");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Estado");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Numero");

                    b.HasKey("Id");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("Trichechus.Domain.Entities.Funcionalidade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Funcionalidades");
                });

            modelBuilder.Entity("Trichechus.Domain.Entities.Perfil", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Perfis");
                });

            modelBuilder.Entity("Trichechus.Domain.Entities.Tarefa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AtividadeId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("AtualizadoEm")
                        .HasColumnType("DateTime2")
                        .HasColumnName("AtualizadoEm");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("DateTime2")
                        .HasColumnName("CriadoEm");

                    b.Property<DateTime?>("DeletadoEm")
                        .HasColumnType("DateTime2")
                        .HasColumnName("DeletadoEm");

                    b.Property<string>("Descricao")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeResponsavel")
                        .HasColumnType("TEXT");

                    b.Property<string>("Observacao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Prazo")
                        .HasColumnType("DateTime2")
                        .HasColumnName("Prazo");

                    b.Property<string>("Situacao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AtividadeId");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("Trichechus.Domain.Entities.UsuarioLocal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("AtualizadoEm")
                        .HasColumnType("DateTime2")
                        .HasColumnName("AtualizadoEm");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("DateTime2")
                        .HasColumnName("CriadoEm");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("SenhaHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("UsuariosLocais");
                });

            modelBuilder.Entity("ContratoFornecedor", b =>
                {
                    b.HasOne("Trichechus.Domain.Entities.Contrato", null)
                        .WithMany()
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trichechus.Domain.Entities.Fornecedor", null)
                        .WithMany()
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FuncionalidadePerfil", b =>
                {
                    b.HasOne("Trichechus.Domain.Entities.Funcionalidade", null)
                        .WithMany()
                        .HasForeignKey("FuncionalidadesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trichechus.Domain.Entities.Perfil", null)
                        .WithMany()
                        .HasForeignKey("PerfisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PerfilUsuarioLocal", b =>
                {
                    b.HasOne("Trichechus.Domain.Entities.Perfil", null)
                        .WithMany()
                        .HasForeignKey("PerfisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trichechus.Domain.Entities.UsuarioLocal", null)
                        .WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trichechus.Domain.Entities.Tarefa", b =>
                {
                    b.HasOne("Trichechus.Domain.Entities.Atividade", "Atividade")
                        .WithMany("Tarefas")
                        .HasForeignKey("AtividadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atividade");
                });

            modelBuilder.Entity("Trichechus.Domain.Entities.Atividade", b =>
                {
                    b.Navigation("Tarefas");
                });
#pragma warning restore 612, 618
        }
    }
}
