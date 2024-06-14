using FluentAssertions;
using Xunit;
using System;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Validation;
using System.Collections.Generic;

namespace Agendamento.Domain.Tests
{
    public class ProdutoTestUnit1
    {
        private Categoria CreateValidCategoria()
        {
            return new Categoria(1, "Categoria Teste");
        }

        [Fact(DisplayName = "Create Product With Valid Parameters")]
        public void CreateProduct_WithValidParameters()
        {
            var categoria = CreateValidCategoria();
            var produto = new Produto("Produto Teste", 100m, "Descrição do Produto", "foto.jpg", null)
            {
                Categoria = categoria
            };

            produto.Should().NotBeNull();
            produto.Nome.Should().Be("Produto Teste");
            produto.Preco.Should().Be(100m);
            produto.Descricao.Should().Be("Descrição do Produto");
            produto.FotoPrincipal.Should().Be("foto.jpg");
            produto.IsActive.Should().BeTrue();
        }

        [Fact(DisplayName = "Create Product With Null Photo and No Domain Exception")]
        public void CreateProduct_WithNullPhoto_NoDomainException()
        {
            var categoria = CreateValidCategoria();
            Action action = () => new Produto("Produto Teste", 100m, "Descrição do Produto", null, null)
            {
                Categoria = categoria
            };
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product With Null Photo and No Null Reference Exception")]
        public void CreateProduct_WithNullPhoto_NoNullReferenceException()
        {
            var categoria = CreateValidCategoria();
            Action action = () => new Produto("Produto Teste", 100m, "Descrição do Produto", null, null)
            {
                Categoria = categoria
            };
            action.Should().NotThrow<NullReferenceException>();
        }

        [Fact(DisplayName = "Create Product With Invalid Id")]
        public void CreateProduct_InvalidId_DomainExceptionInvalidId()
        {
            var categoria = CreateValidCategoria();
            Action action = () => new Produto(-1, "Produto Teste", 100m, "Descrição do Produto", "foto.jpg", null)
            {
                Categoria = categoria
            };

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Valor de Id é inválido");
        }

        [Fact(DisplayName = "Create Product With Invalid Name")]
        public void CreateProduct_InvalidName_DomainExceptionInvalidName()
        {
            var categoria = CreateValidCategoria();
            Action action = () => new Produto("Pr", 100m, "Descrição do Produto", "foto.jpg", null)
            {
                Categoria = categoria
            };

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Nome deve ter no mínimo 3 caracteres");
        }

        [Fact(DisplayName = "Create Product With Invalid Description")]
        public void CreateProduct_InvalidDescription_DomainExceptionInvalidDescription()
        {
            var categoria = CreateValidCategoria();
            Action action = () => new Produto("Produto Teste", 100m, "De", "foto.jpg", null)
            {
                Categoria = categoria
            };

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Descrição deve ter no mínimo 3 caracteres");
        }

        [Fact(DisplayName = "Create Product With Invalid Price")]
        public void CreateProduct_InvalidPrice_DomainExceptionInvalidPrice()
        {
            var categoria = CreateValidCategoria();
            Action action = () => new Produto("Produto Teste", -100m, "Descrição do Produto", "foto.jpg", null)
            {
                Categoria = categoria
            };

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Valor de preço Inválido");
        }

        [Fact(DisplayName = "Create Product With Long Name")]
        public void CreateProduct_LongName_DomainExceptionLongName()
        {
            var categoria = CreateValidCategoria();
            Action action = () => new Produto(new string('a', 101), 100m, "Descrição do Produto", "foto.jpg", null)
            {
                Categoria = categoria
            };

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Nome deve ter no máximo 100 caracteres");
        }

        [Fact(DisplayName = "Create Product With Long Description")]
        public void CreateProduct_LongDescription_DomainExceptionLongDescription()
        {
            var categoria = CreateValidCategoria();
            Action action = () => new Produto("Produto Teste", 100m, new string('a', 101), "foto.jpg", null)
            {
                Categoria = categoria
            };

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Descrição deve ter no máximo 100 caracteres");
        }

        [Fact(DisplayName = "Create Product With Long Main Photo")]
        public void CreateProduct_LongMainPhoto_DomainExceptionLongMainPhoto()
        {
            var categoria = CreateValidCategoria();
            Action action = () => new Produto("Produto Teste", 100m, "Descrição do Produto", new string('a', 251), null)
            {
                Categoria = categoria
            };

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Foto principal excede o número de caracteres permitidos");
        }

        [Fact(DisplayName = "Create Product With Long Additional Photo")]
        public void CreateProduct_LongAdditionalPhoto_DomainExceptionLongAdditionalPhoto()
        {
            var categoria = CreateValidCategoria();
            Action action = () => new Produto("Produto Teste", 100m, "Descrição do Produto", "foto.jpg", new List<string> { new string('a', 251) })
            {
                Categoria = categoria
            };

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Uma ou mais fotos excedem o número de caracteres permitidos");
        }

        [Fact(DisplayName = "Update Product With Valid Parameters")]
        public void UpdateProduct_WithValidParameters()
        {
            var categoria = CreateValidCategoria();
            var produto = new Produto("Produto Teste", 100m, "Descrição do Produto", "foto.jpg", null)
            {
                Categoria = categoria
            };

            produto.Update("Produto Atualizado", 150m, "Descrição Atualizada", 1, "nova_foto.jpg", null);

            produto.Nome.Should().Be("Produto Atualizado");
            produto.Preco.Should().Be(150m);
            produto.Descricao.Should().Be("Descrição Atualizada");
            produto.FotoPrincipal.Should().Be("nova_foto.jpg");
        }

        [Fact(DisplayName = "Deactivate Product Successfully")]
        public void DeactivateProduct_Successfully()
        {
            var categoria = CreateValidCategoria();
            var produto = new Produto("Produto Teste", 100m, "Descrição do Produto", "foto.jpg", null)
            {
                Categoria = categoria
            };

            produto.Deactivate();

            produto.IsActive.Should().BeFalse();
        }
    }
}