using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TesteContasPagar.Controllers;
using TesteContasPagar.Dal;
using TesteContasPagar.Models;
using Xunit;


namespace ComprasPagarTest
{
    public class ContasPagarTest 
    {
        ContasPagarController _controller;
        ContasPagarContext _context;
        public ContasPagarTest()
        {
            _context = new ContasPagarContext();
            _controller = new ContasPagarController(_context);
        }
        
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.GetContaPagar();
            // Assert
            var items = Assert.IsType<List<ContaPagar>>(okResult);
            Assert.NotEmpty(items);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var obj = new ContaPagar();
            obj.Nome = "Teste Unitario Conta";
            obj.DataVencimento = Convert.ToDateTime("2020-10-10");
            obj.DataPagamento = Convert.ToDateTime("2020-10-11");
            obj.ValorOriginal = 200M;
            // Act
            var okResult = _controller.PostContaPagar(obj);
            // Assert
            Assert.IsType<ActionResult<ContaPagar>>(okResult.Result);
        }
    }
}
