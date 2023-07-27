using System;
using System.Collections.Generic;

class Gene
{
  public int Value { get; set; }
  public int Dominance { get; set; }

  public Gene(int value, int dominance)
  {
    Value = value;
    Dominance = dominance;
  }
}

class Trait
{
  public Gene Gene1 { get; set; }
  public Gene Gene2 { get; set; }

  public Trait(Gene gene1, Gene gene2)
  {
    Gene1 = gene1;
    Gene2 = gene2;
  }

  public int GetDominantValue()
  {
    return Gene1.Dominance >= Gene2.Dominance ? Gene1.Value : Gene2.Value;
  }

  public Gene GetGene1()
  {
    return Gene1;
  }

  public Gene GetGene2()
  {
    return Gene2;
  }
}


class GeneticObject
{
  public Dictionary<string, Trait> Traits { get; set; }

  public GeneticObject()
  {
    Traits = new Dictionary<string, Trait>();
  }

  public void AddTrait(string name, Trait trait)
  {
    Traits[name] = trait;
  }

  public bool TraitExists(string name)
  {
    return Traits.ContainsKey(name);
  }

  public int GetTraitValue(string name)
  {
    if (Traits.ContainsKey(name))
    {
      return Traits[name].GetDominantValue();
    }
    else
    {
      throw new Exception("Trait not found: " + name);
    }
  }

  public Trait GetTrait(string name)
  {

    if (Traits.ContainsKey(name))
    {
      return Traits[name];
    }
    else
    {
      throw new Exception("Trait not found: " + name);
    }
  }
}

class GeneticEngine
{
  public static GeneticObject CreateObject(Dictionary<string, Tuple<Trait, Trait>> traits)
  {
    var obj = new GeneticObject();
    foreach (var trait in traits)
    {
      var combinedTrait = CombineTraits(trait.Value.Item1, trait.Value.Item2);
      obj.AddTrait(trait.Key, combinedTrait);
    }
    return obj;
  }

  private static Trait CombineTraits(Trait trait1, Trait trait2)
  {

    Random rng = new();
    return CombineTraits(trait1, trait2, rng.Next(4));
  }

  private static Trait CombineTraits(Trait trait1, Trait trait2, int configuration)
  {
    return configuration switch
    {
      0 => new Trait(trait1.Gene1, trait2.Gene1),
      1 => new Trait(trait1.Gene1, trait2.Gene2),
      2 => new Trait(trait1.Gene2, trait2.Gene1),
      3 => new Trait(trait1.Gene2, trait2.Gene2),
      _ => throw new ArgumentException("Invalid configuration value"),
    };
  }

  public static GeneticObject Combine(GeneticObject parent1, GeneticObject parent2)
  {
    GeneticObject child = new();
    foreach (var trait in parent1.Traits)
    {
      if (parent2.TraitExists(trait.Key))
      {
        Trait parent1Trait = parent1.GetTrait(trait.Key);
        Trait parent2Trait = parent2.GetTrait(trait.Key);
        Trait childTrait = CombineTraits(parent1Trait, parent2Trait);
        child.AddTrait(trait.Key, childTrait);
      }
    }
    return child;
  }

  public void Mutate(GeneticObject obj)
  {
    throw new NotImplementedException();
  }
}

class TraitDefinition
{
  private readonly List<Gene> genes;

  public TraitDefinition()
  {
    genes = new List<Gene>();
  }

  public void AddGene(Gene gene)
  {
    genes.Add(gene);
    // We should probably do some checks here
    // Like co-dominance (same dominance value)
  }

  public Gene GetGene(int index)
  {
    return genes[index];
  }

  public int GetIndex(Gene gene)
  {
    int index = 0;
    bool wasFound = false;

    genes.ForEach(searchGene =>
    {
      if (searchGene.Dominance == gene.Dominance && searchGene.Value == gene.Value)
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

    throw new KeyNotFoundException("The gene is not in Trait Dictionary");
  }

  public Gene Above(Gene gene)
  {
    int index = this.GetIndex(gene);
    if (index > 0)
    {
      return genes[index - 1];
    }
    else
    {
      return null;
    }
  }

  public Gene Below(Gene gene)
  {
    int index = this.GetIndex(gene);
    if (index < genes.Count - 1)
    {
      return genes[index + 1];
    }
    else
    {
      return null;
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
    TraitDefinition colors = new();
    colors.AddGene(new Gene((int)Color.Red, 1));
    colors.AddGene(new Gene((int)Color.Orange, 2));
    colors.AddGene(new Gene((int)Color.Yellow, 3));
    colors.AddGene(new Gene((int)Color.Green, 4));
    colors.AddGene(new Gene((int)Color.Blue, 5));
    colors.AddGene(new Gene((int)Color.Purple, 6));
    colors.AddGene(new Gene((int)Color.Black, 7));
    colors.AddGene(new Gene((int)Color.White, 8));


    // Define the shape genes
    Gene shapeGeneCircle = new((int)Shape.Circle, 3);
    Gene shapeGeneSquare = new((int)Shape.Square, 2);
    Gene shapeGeneHeart = new((int)Shape.Heart, 1);

    // Parent1
    GeneticObject parent1 = new();
    Trait colorTrait1 = new(colors.GetGene(5), colors.GetGene(1));
    parent1.AddTrait("color", colorTrait1);
    Trait shapeTrait1 = new(shapeGeneHeart, shapeGeneSquare);
    parent1.AddTrait("shape", shapeTrait1);

    // Parent2
    GeneticObject parent2 = new();
    Trait colorTrait2 = new(colors.GetGene(1), colors.GetGene(2));
    parent2.AddTrait("color", colorTrait2);
    Trait shapeTrait2 = new(shapeGeneCircle, shapeGeneHeart);
    parent2.AddTrait("shape", shapeTrait2);

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

      Color myColor = (Color)child.GetTrait("color").GetDominantValue();
      colorStatistics.IncrementMetric(myColor);

      Shape myShape = (Shape)child.GetTrait("shape").GetDominantValue();
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
