using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

public class Classe
{
    public string Nome { get; set; }
    public List<string> Atributos { get; set; } = new();
}

public class Associacao
{
    public string Origem { get; set; }
    public string Destino { get; set; }
    public string MultiplicidadeOrigem { get; set; }
    public string MultiplicidadeDestino { get; set; }
    public bool Bidirecional { get; set; }
}

public class Modelo
{
    public List<Classe> Classes { get; set; } = new();
    public List<Associacao> Associacoes { get; set; } = new();
}

class Program
{
    static void Main()
    {
        string arquivo = "modelo.json";
        if (!File.Exists(arquivo))
        {
            Console.WriteLine("❌ Arquivo modelo.json não encontrado!");
            return;
        }

        string json = File.ReadAllText(arquivo);
        Modelo modelo = JsonSerializer.Deserialize<Modelo>(json);

        Console.WriteLine("📂 Modelo carregado com sucesso!");
        Console.WriteLine("\n--- ANTES ---");
        Console.WriteLine(JsonSerializer.Serialize(modelo, new JsonSerializerOptions { WriteIndented = true }));

        // Detectar cheiros
        var problemas = DetectarCheiros(modelo);
        Console.WriteLine("\n🔎 Cheiros detectados:");
        if (problemas.Any())
            problemas.ForEach(p => Console.WriteLine("⚠️ " + p));
        else
            Console.WriteLine("✅ Nenhum cheiro detectado.");

        // Aplicar refatorações
        Modelo refatorado = AplicarRefatoracoes(modelo);

        Console.WriteLine("\n--- DEPOIS ---");
        Console.WriteLine(JsonSerializer.Serialize(refatorado, new JsonSerializerOptions { WriteIndented = true }));

        // Exportar
        File.WriteAllText("modelo_refatorado.json", JsonSerializer.Serialize(refatorado, new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine("\n💾 Arquivo exportado: modelo_refatorado.json");
    }

    // -----------------------------
    // 🔎 DETECÇÃO DE CHEIROS
    // -----------------------------
    static List<string> DetectarCheiros(Modelo modelo)
    {
        List<string> problemas = new();

        // Atributos
        foreach (var classe in modelo.Classes)
        {
            foreach (var atributo in classe.Atributos)
            {
                if (atributo.Length > 20)
                    problemas.Add($"Atributo '{atributo}' na classe {classe.Nome} tem nome excessivamente longo.");
            }

            // Atributos duplicados
            var duplicados = classe.Atributos
                .GroupBy(a => a)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);

            foreach (var dup in duplicados)
                problemas.Add($"Classe {classe.Nome} possui atributo duplicado: {dup}.");
        }

        // Associações
        foreach (var assoc in modelo.Associacoes)
        {
            if (EhFrouxa(assoc.MultiplicidadeDestino))
                problemas.Add($"Multiplicidade frouxa detectada em {assoc.Origem}→{assoc.Destino} ({assoc.MultiplicidadeDestino}).");

            if (assoc.Bidirecional)
                problemas.Add($"Associação {assoc.Origem}→{assoc.Destino} é bidirecional (possivelmente desnecessária).");
        }

        return problemas;
    }

    // -----------------------------
    // 🔧 REGRAS DE REFACTORIZAÇÃO
    // -----------------------------
    static Modelo AplicarRefatoracoes(Modelo modelo)
    {
        var clone = JsonSerializer.Deserialize<Modelo>(JsonSerializer.Serialize(modelo));

        foreach (var classe in clone.Classes)
        {
            // Truncar atributos muito longos
            for (int i = 0; i < classe.Atributos.Count; i++)
            {
                if (classe.Atributos[i].Length > 20)
                    classe.Atributos[i] = classe.Atributos[i].Substring(0, 20) + "_Renomeado";
            }

            // Remover duplicados
            classe.Atributos = classe.Atributos.Distinct().ToList();
        }

        foreach (var assoc in clone.Associacoes)
        {
            if (EhFrouxa(assoc.MultiplicidadeDestino))
                assoc.MultiplicidadeDestino = "1..*"; // corrige frouxidão

            if (assoc.Bidirecional)
                assoc.Bidirecional = false; // remove bidirecionalidade
        }

        return clone;
    }

    // -----------------------------
    // 🧮 PARSER DE MULTIPLICIDADE
    // -----------------------------
    static (int min, int? max) ParseMultiplicidade(string mult)
    {
        if (string.IsNullOrWhiteSpace(mult))
            return (1, 1); // default

        if (mult.Contains(".."))
        {
            var partes = mult.Split("..");
            int min = partes[0] == "*" ? 0 : int.Parse(partes[0]);
            int? max = partes[1] == "*" ? null : int.Parse(partes[1]);
            return (min, max);
        }
        else
        {
            // Valor único (ex: "1")
            int min = mult == "*" ? 0 : int.Parse(mult);
            return (min, min);
        }
    }

    static bool EhFrouxa(string multiplicidade)
    {
        var (min, _) = ParseMultiplicidade(multiplicidade);
        return min == 0; // frouxa se permite zero
    }
}