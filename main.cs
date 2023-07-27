using System;
using Genetic;
using LocalTestingUtilities;

// ❤️🧡💛💚💙💜🖤🤍
enum Color { Red, Orange, Yellow, Green, Blue, Purple, Black, White }

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
    GeneDefinition shapes = new();
    shapes.AddAllele(new Allele((int)Shape.Circle, 3));
    shapes.AddAllele(new Allele((int)Shape.Square, 2));
    shapes.AddAllele(new Allele((int)Shape.Heart, 1));

    // Parent1
    GeneticObject parent1 = new();
    Genotype colorTrait1 = new(colors.GetGene((int)Color.Blue), colors.GetGene((int)Color.Red));
    parent1.AddGenotype("color", colorTrait1);
    Genotype shapeTrait1 = new(shapes.GetGene((int)Shape.Heart), shapes.GetGene((int)Shape.Square));
    parent1.AddGenotype("shape", shapeTrait1);

    // Parent2
    GeneticObject parent2 = new();
    Genotype colorTrait2 = new(colors.GetGene((int)Color.Red), colors.GetGene((int)Color.Orange));
    parent2.AddGenotype("color", colorTrait2);
    Genotype shapeTrait2 = new(shapes.GetGene((int)Shape.Circle), shapes.GetGene((int)Shape.Heart));
    parent2.AddGenotype("shape", shapeTrait2);

    Console.Write("Parents: ");
    Console.Write(RenderColoredShape(parent1));
    Console.WriteLine(RenderColoredShape(parent2));

    // Statistics Setup
    Statistic<Color> colorStatistics = new();
    Statistic<Shape> shapeStatistics = new();

    // Making babies 😏
    int childCount = 5000;
    for (int i = 0; i < childCount; i++)
    {
      var child = GeneticObject.Combine(parent1, parent2, 1);

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
