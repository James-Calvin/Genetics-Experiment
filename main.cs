using System;
using System.Collections.Generic;
using System.Linq;

class Gene<T>
{
  public T Value { get; set; }
  public int Dominance { get; set; }

  public Gene(T value, int dominance)
  {
    Value = value;
    Dominance = dominance;
  }
}

interface ITrait
{
  object GetDominantValue();

}

class Trait<T> : ITrait
{
  public Gene<T> Gene1 { get; set; }
  public Gene<T> Gene2 { get; set; }

  public Trait(Gene<T> gene1, Gene<T> gene2)
  {
    Gene1 = gene1;
    Gene2 = gene2;
  }

  public T GetDominantValue()
  {
    return Gene1.Dominance >= Gene2.Dominance ? Gene1.Value : Gene2.Value;
  }

  object ITrait.GetDominantValue()
  {
    return GetDominantValue();
  }
}


class GeneticObject
{
  public Dictionary<string, ITrait> Traits { get; set; }

  public GeneticObject()
  {
    Traits = new Dictionary<string, ITrait>();
  }

  public void AddTrait(string name, ITrait trait)
  {
    Traits[name] = trait;
  }

  public object GetTraitValue(string name)
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
}

class GeneticEngine
{
  public static GeneticObject CreateObject(Dictionary<string, Tuple<ITrait, ITrait>> traits)
  {
    var obj = new GeneticObject();
    foreach (var trait in traits)
    {
      var combinedTrait = CombineTraits(trait.Value.Item1, trait.Value.Item2);
      obj.AddTrait(trait.Key, combinedTrait);
    }
    return obj;
  }

  private static ITrait CombineTraits(ITrait trait1, ITrait trait2)
  {
    throw new NotImplementedException();
    // implement the logic for combining traits. You could randomly choose a gene from each trait.
  }

  public static GeneticObject Combine(GeneticObject parent1, GeneticObject parent2)
  {
    throw new NotImplementedException();
  }

  public void Mutate(GeneticObject obj)
  {
    throw new NotImplementedException();
  }
}

// 💔🧡💛💚💙💜🖤🤍
enum Color { Red, Orange, Yellow, Green, Blue, Purple, Black, White };

// ⚪⬜🤍
enum Shape { Circle, Square, Heart }


class Program
{
  public static void Main(string[] _)
  {
    // Define the color genes
    Gene<Color> colorGeneRed = new(Color.Red, 1);
    Gene<Color> colorGeneOrange = new(Color.Orange, 2);
    Gene<Color> colorGeneYellow = new(Color.Yellow, 3);
    Gene<Color> colorGeneGreen = new(Color.Green, 4);
    Gene<Color> colorGeneBlue = new(Color.Blue, 5);
    Gene<Color> colorGenePurple = new(Color.Purple, 6);
    Gene<Color> colorGeneBlack = new(Color.Black, 7);
    Gene<Color> colorGeneWhite = new(Color.White, 8);

    // Define the shape genes
    Gene<Shape> shapeGeneCircle = new(Shape.Circle, 1);
    Gene<Shape> shapeGeneSquare = new(Shape.Square, 2);
    Gene<Shape> shapeGeneHeart = new(Shape.Heart, 3);

    GeneticObject parent1 = new();
    Trait<Color> colorTrait1 = new(colorGeneBlue, colorGeneRed);
  }
}
