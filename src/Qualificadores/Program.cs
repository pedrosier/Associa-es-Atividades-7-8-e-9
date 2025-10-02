using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string arquivo = "hospital_leitos.csv"; // CSV de exemplo
        string[] delimitador = { "," };

        // Lê todas as linhas do CSV
        var linhas = File.ReadAllLines(arquivo).ToList();

        // Cabeçalho (primeira linha)
        var cabecalho = linhas[0].Split(delimitador, StringSplitOptions.None);

        Console.WriteLine("📂 Arquivo carregado: " + arquivo);
        Console.WriteLine("Colunas disponíveis: ");
        for (int i = 0; i < cabecalho.Length; i++)
            Console.WriteLine($"{i} - {cabecalho[i]}");

        Console.WriteLine("\nDigite os índices das colunas que formarão a chave lógica (separados por vírgula):");
        string? entrada = Console.ReadLine();
        var indices = entrada.Split(',').Select(int.Parse).ToArray();

        // Cria lista de registros
        var registros = linhas.Skip(1).Select(l => l.Split(delimitador, StringSplitOptions.None)).ToList();

        // Monta chave lógica para cada linha
        var chavePorLinha = registros.Select((cols, idx) =>
            new
            {
                Linha = idx + 2, // +2 por causa do cabeçalho (linha 1)
                Chave = string.Join("-", indices.Select(i => cols[i])),
                Valores = string.Join(",", cols)
            }).ToList();

        // Detecta duplicados
        var duplicados = chavePorLinha
            .GroupBy(x => x.Chave)
            .Where(g => g.Count() > 1)
            .SelectMany(g => g)
            .ToList();

        int total = registros.Count;
        int totalDuplicados = duplicados.Count;
        double perc = totalDuplicados * 100.0 / total;

        // Estatísticas
        Console.WriteLine("\n📊 Estatísticas de Colisões:");
        Console.WriteLine($"Total de registros: {total}");
        Console.WriteLine($"Registros em colisão: {totalDuplicados}");
        Console.WriteLine($"Percentual de colisões: {perc:F2}%\n");

        if (duplicados.Any())
        {
            Console.WriteLine("⚠️ Linhas com colisões detectadas:\n");
            foreach (var d in duplicados)
            {
                Console.WriteLine($"Linha {d.Linha}: {d.Valores} (Chave = {d.Chave})");
            }

            // Exportar duplicados
            string arquivoDuplicados = "colisoes.csv";
            File.WriteAllLines(arquivoDuplicados,
                new[] { string.Join(",", cabecalho) }
                .Concat(duplicados.Select(d => d.Valores)));

            Console.WriteLine($"\n💾 Colisões exportadas em: {arquivoDuplicados}");
        }
        else
        {
            Console.WriteLine("✅ Nenhuma colisão encontrada!");
        }
    }
}
