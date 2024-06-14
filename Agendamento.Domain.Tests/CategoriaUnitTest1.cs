using FluentAssertions;
using Xunit;
using System;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Validation;
using System.Collections.Generic;

namespace Agendamento.Domain.Tests
{
    public class CategoriaTestUnit1
    {
        private Categoria CreateValidCategoria()
        {
            return new Categoria(1, "Categoria Nome");
        }

        private Produto CreateValidProduto(Categoria categoria, bool isActive = true)
        {
            return new Produto("Produto 1", 10m, "Descrição", "foto.jpg", new List<string>())
            {
                Categoria = categoria,
                IsActive = isActive
            };
        }

        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidateParameters_ResultObjectValidState()
        {
            Action action = () => new Categoria(1, "Categoria Nome");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category With Negative Id")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Categoria(-1, "Categoria Nome");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Id inválido");
        }

        [Fact(DisplayName = "Create Category With Null Name")]
        public void CreateCategory_NullNameValue_DomainExceptionNullName()
        {
            Action action = () => new Categoria(1, null);
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Nome é obrigatório");
        }

        [Fact(DisplayName = "Create Category With Empty Name")]
        public void CreateCategory_EmptyNameValue_DomainExceptionEmptyName()
        {
            Action action = () => new Categoria(1, "");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Nome é obrigatório");
        }

        [Fact(DisplayName = "Create Category With Short Name")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Categoria(1, "Ca");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("O nome deve ter no mínimo 3 caracteres");
        }

        [Fact(DisplayName = "Create Category With Long Name")]
        public void CreateCategory_LongNameValue_DomainExceptionLongName()
        {
            Action action = () => new Categoria(1, new string('a', 101));
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("O nome deve ter no máximo 100 caracteres");
        }

        [Fact(DisplayName = "Update Category With Valid Name")]
        public void UpdateCategory_WithValidName_NoDomainException()
        {
            var categoria = new Categoria(1, "Categoria Nome");
            Action action = () => categoria.Update("Nome Atualizado");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Update Category With Invalid Name")]
        public void UpdateCategory_WithInvalidName_DomainExceptionInvalidName()
        {
            var categoria = new Categoria(1, "Categoria Nome");

            Action actionShortName = () => categoria.Update("Ca");
            actionShortName.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("O nome deve ter no mínimo 3 caracteres");

            Action actionLongName = () => categoria.Update(new string('a', 101));
            actionLongName.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("O nome deve ter no máximo 100 caracteres");
        }

        [Fact(DisplayName = "Deactivate Category With Active Products")]
        public void DeactivateCategory_WithActiveProducts_DomainExceptionActiveProducts()
        {
            var categoria = CreateValidCategoria();
            categoria.Produtos = new List<Produto>
            {
                CreateValidProduto(categoria, true)
            };

            Action action = () => categoria.Deactivate();
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Não é possível desativar a categoria enquanto houver produtos ativos.");
        }

        [Fact(DisplayName = "Deactivate Category With No Active Products")]
        public void DeactivateCategory_WithNoActiveProducts_NoDomainException()
        {
            var categoria = CreateValidCategoria();
            categoria.Produtos = new List<Produto>
            {
                CreateValidProduto(categoria, false)
            };

            Action action = () => categoria.Deactivate();
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }
    }
}