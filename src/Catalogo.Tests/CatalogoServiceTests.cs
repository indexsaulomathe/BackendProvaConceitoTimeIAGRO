using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CatalogoServiceTests
{
    private CatalogoService catalogoService;
    private List<Livro> catalogo;

    [TestInitialize]
    public void Setup()
    {
        catalogoService = new CatalogoService("../CatalogoRepository.json");
        catalogo = LerCatalogoDoArquivo();
    }

    [TestMethod]
    public void ObterTodos_DeveRetornarTodosOsLivros()
    {
        Assert.IsTrue(CompararListas(catalogo, catalogoService.ObterTodos(), new LivroComparer()));
    }

    [TestMethod]
    public void BuscarLivros_PorTermo_DeveRetornarLivrosCorrespondentes()
    {
        var termoBusca = "Jules";
        var esperado = catalogo.FindAll(l => l.Name.Contains(termoBusca) || l.Specifications.Author.Contains(termoBusca));

        Assert.IsTrue(CompararListas(esperado, catalogoService.BuscarLivros(termoBusca), new LivroComparer()));
    }

    [TestMethod]
    public void OrdenarPorPreco_Ascendente_DeveRetornarLivrosOrdenadosAscendente()
    {
        var esperado = catalogo.OrderBy(l => l.Price);

        Assert.IsTrue(CompararListas(esperado.ToList(), catalogoService.OrdenarPorPreco(true).ToList(), new LivroComparer()));
    }

    [TestMethod]
    public void OrdenarPorPreco_Descendente_DeveRetornarLivrosOrdenadosDescendente()
    {
        var esperado = catalogo.OrderByDescending(l => l.Price);

        Assert.IsTrue(CompararListas(esperado.ToList(), catalogoService.OrdenarPorPreco(false).ToList(), new LivroComparer()));
    }

    [TestMethod]
    public void CalcularFrete_DeveRetornarFreteCorreto()
    {
        decimal precoLivro = 10.00m;
        decimal esperado = precoLivro * 0.2m;

        Assert.AreEqual(esperado, catalogoService.CalcularFrete(precoLivro));
    }

    private List<Livro> LerCatalogoDoArquivo()
    {
        try
        {
            string caminhoArquivoJson = "../CatalogoRepository.json";
            string json = File.ReadAllText(caminhoArquivoJson);
            return JsonSerializer.Deserialize<List<Livro>>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo JSON: {ex.Message}");
            return new List<Livro>();
        }
    }

    private bool CompararListas<T>(List<T> esperado, List<T> atual, IComparer<T> comparador)
    {
        if (esperado.Count != atual.Count)
            return false;

        for (int i = 0; i < esperado.Count; i++)
        {
            if (comparador.Compare(esperado[i], atual[i]) != 0)
                return false;
        }

        return true;
    }

    private class LivroComparer : IComparer<Livro>
    {
        public int Compare(Livro x, Livro y)
        {
            return x.Id.CompareTo(y.Id);
        }
    }
}