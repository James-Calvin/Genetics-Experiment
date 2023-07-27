using System;
using System.Collections.Generic;

class Allele
{
  public int Value { get; set; }
  public int Dominance { get; set; }

  public Allele(int value, int dominance)
  {
    Value = value;
    Dominance = dominance;
  }
}

class Genotype
{
  public Allele Allele1 { get; set; }
  public Allele Allele2 { get; set; }

  public Genotype(Allele allele1, Allele allele2)
  {
    Allele1 = allele1;
    Allele2 = allele2;
  }

  public int GetDominantValue()
  {
    return Allele1.Dominance >= Allele2.Dominance ? Allele1.Value : Allele2.Value;
  }

  public Allele GetGene1()
  {
    return Allele1;
  }

  public Allele GetGene2()
  {
    return Allele2;
  }
}


class GeneticObject
{
  public Dictionary<string, Genotype> Genotypes { get; set; }

  public GeneticObject()
  {
    Genotypes = new Dictionary<string, Genotype>();
  }

  public void AddGenotype(string name, Genotype trait)
  {
    Genotypes[name] = trait;
  }

  public bool GenotypeExists(string name)
  {
    return Genotypes.ContainsKey(name);
  }

  public int GetTraitValue(string name)
  {
    if (Genotypes.ContainsKey(name))
    {
      return Genotypes[name].GetDominantValue();
    }
    else
    {
      throw new Exception("Genotype not found: " + name);
    }
  }

  public Genotype GetGenotype(string name)
  {

    if (Genotypes.ContainsKey(name))
    {
      return Genotypes[name];
    }
    else
    {
      throw new Exception("Genotype not found: " + name);
    }
  }
}

class GeneticEngine
{


  private static Genotype CombineTraits(Genotype trait1, Genotype trait2)
  {

    Random rng = new();
    return CombineTraits(trait1, trait2, rng.Next(4));
  }

  private static Genotype CombineTraits(Genotype trait1, Genotype trait2, int configuration)
  {
    return configuration switch
    {
      0 => new Genotype(trait1.Allele1, trait2.Allele1),
      1 => new Genotype(trait1.Allele1, trait2.Allele2),
      2 => new Genotype(trait1.Allele2, trait2.Allele1),
      3 => new Genotype(trait1.Allele2, trait2.Allele2),
      _ => throw new ArgumentException("Invalid configuration value"),
    };
  }

  public static GeneticObject Combine(GeneticObject parent1, GeneticObject parent2)
  {
    GeneticObject child = new();
    foreach (var trait in parent1.Genotypes)
    {
      if (parent2.GenotypeExists(trait.Key))
      {
        Genotype parent1Trait = parent1.GetGenotype(trait.Key);
        Genotype parent2Trait = parent2.GetGenotype(trait.Key);
        Genotype childTrait = CombineTraits(parent1Trait, parent2Trait);
        child.AddGenotype(trait.Key, childTrait);
      }
    }
    return child;
  }

  public void Mutate(GeneticObject obj)
  {
    throw new NotImplementedException();
  }
}

class GeneDefinition
{
  private readonly List<Allele> alleles;

  public GeneDefinition()
  {
    alleles = new List<Allele>();
  }

  public void AddAllele(Allele allele)
  {
    alleles.Add(allele);
    // We should probably do some checks here
    // Like co-dominance (same dominance value)
    // And sort by dominance, asc
  }

  public Allele GetGene(int index)
  {
    return alleles[index];
  }

  public int GetIndex(Allele allele)
  {
    int index = 0;
    bool wasFound = false;

    alleles.ForEach(searchGene =>
    {
      if (searchGene.Dominance == allele.Dominance && searchGene.Value == allele.Value)
      {
        wasFound = true;
        return;
      }
      index++;
    });

    if (wasFound)
    {
      return index;
    }

    throw new KeyNotFoundException("The allele is not in this gene definition");
  }

  public Allele MutateDown(Allele gene)
  {
    int index = this.GetIndex(gene);
    if (index > 0)
    {
      return alleles[index - 1];
    }
    else
    {
      return gene;
    }
  }

  public Allele MutateUp(Allele gene)
  {
    int index = this.GetIndex(gene);
    if (index < alleles.Count - 1)
    {
      return alleles[index + 1];
    }
    else
    {
      return gene;
    }
  }
}

class Statistic<T>
{
  private readonly Dictionary<T, int> record = new();

  public Statistic()
  {
  }

  public bool ContainsKey(T metric)
  {
    return record.ContainsKey(metric);
  }

  public void IncrementMetric(T metric)
  {
    IncrementMetric(metric, 1);
  }

  public void IncrementMetric(T metric, int value)
  {
    if (record.ContainsKey(metric))
    {
      record[metric] += value;
    }
    else
    {
      record.Add(metric, value);
    }
  }


  public Dictionary<T, int> GetRecord()
  {
    return record;
  }

  public int GetMetricValue(T metric)
  {
    if (record.ContainsKey(metric))
    {
      return record[metric];
    }
    else
    {
      return 0;
    }
  }

  public int GetTotal()
  {
    int sum = 0;
    foreach (var entry in record)
    {
      sum += entry.Value;
    }
    return sum;
  }
}

// ❤️🧡💛💚💙💜🖤🤍
enum Color { Red, Orange, Yellow, Green, Blue, Purple, Black, White };

// ⚪⬜🤍
enum Shape { Circle, Square, Heart }

class Program
{
  public static string RenderColoredShape(GeneticObject obj)
  {
    Shape shape = (Shape)obj.GetTraitValue("shape");
    Color color = (Color)obj.GetTraitValue("color");
    return RenderColoredShape(shape, color);
  }
  public static string RenderColoredShape(Shape shape, Color color)
  {
    string[,] stateSpace = new string[,] {
      { "🔴", "🟠", "🟡", "🟢", "🔵", "🟣", "⚫", "⚪" },
      { "🟥", "🟧", "🟨", "🟩", "🟦", "🟪", "⬛", "⬜"},
      { "❤️", "🧡", "💛", "💚", "💙", "💜", "🖤", "🤍"},
      };

    return stateSpace[(int)shape, (int)color];
  }
  public static void Main(string[] _)
  {
    // Define the color genes
    GeneDefinition colors = new();
    colors.AddAllele(new Allele((int)Color.Red, 1));
    colors.AddAllele(new Allele((int)Color.Orange, 2));
    colors.AddAllele(new Allele((int)Color.Yellow, 3));
    colors.AddAllele(new Allele((int)Color.Green, 4));
    colors.AddAllele(new Allele((int)Color.Blue, 5));
    colors.AddAllele(new Allele((int)Color.Purple, 6));
    colors.AddAllele(new Allele((int)Color.Black, 7));
    colors.AddAllele(new Allele((int)Color.White, 8));


    // Define the shape genes
    Allele shapeGeneCircle = new((int)Shape.Circle, 3);
    Allele shapeGeneSquare = new((int)Shape.Square, 2);
    Allele shapeGeneHeart = new((int)Shape.Heart, 1);

    // Parent1
    GeneticObject parent1 = new();
    Genotype colorTrait1 = new(colors.GetGene(5), colors.GetGene(1));
    parent1.AddGenotype("color", colorTrait1);
    Genotype shapeTrait1 = new(shapeGeneHeart, shapeGeneSquare);
    parent1.AddGenotype("shape", shapeTrait1);

    // Parent2
    GeneticObject parent2 = new();
    Genotype colorTrait2 = new(colors.GetGene(1), colors.GetGene(2));
    parent2.AddGenotype("color", colorTrait2);
    Genotype shapeTrait2 = new(shapeGeneCircle, shapeGeneHeart);
    parent2.AddGenotype("shape", shapeTrait2);

    Console.Write("Parents: ");
    Console.Write(RenderColoredShape(parent1));
    Console.WriteLine(RenderColoredShape(parent2));

    // Statistics Setup
    Statistic<Color> colorStatistics = new();
    Statistic<Shape> shapeStatistics = new();

    // Making babies 😏
    int childCount = 50;
    for (int i = 0; i < childCount; i++)
    {
      var child = GeneticEngine.Combine(parent1, parent2);

      Color myColor = (Color)child.GetGenotype("color").GetDominantValue();
      colorStatistics.IncrementMetric(myColor);

      Shape myShape = (Shape)child.GetGenotype("shape").GetDominantValue();
      shapeStatistics.IncrementMetric(myShape);
    }

    Console.WriteLine();
    int totalColors = colorStatistics.GetTotal();
    foreach (var color in colorStatistics.GetRecord())
    {
      Console.WriteLine(color.Key + ": " + (100 * color.Value / totalColors) + "%");
    }

    Console.WriteLine();
    int totalShapes = shapeStatistics.GetTotal();
    foreach (var shape in shapeStatistics.GetRecord())
    {
      Console.WriteLine(shape.Key + ": " + (100 * shape.Value / totalShapes) + "%");
    }

  }
}
