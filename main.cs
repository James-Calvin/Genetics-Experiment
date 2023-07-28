using System;
using Genetic;

// ❤️🧡💛💚💙💜🖤🤍
enum Color { Red, Orange, Yellow, Green, Blue, Purple, Black, White }

// ⚪⬜🤍
enum Shape { Circle, Square, Heart, Food }

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
      { "🍒", "🍊", "🍌", "🍏", "🍭", "🍇", "🍩", "🍦"},
      };

    return stateSpace[(int)shape, (int)color];
  }
  public static void Main(string[] _)
  {
    // Define the color genes
    GeneDefinition colors = new();
    colors.CreateAllele((int)Color.Red, 1);
    colors.CreateAllele((int)Color.Orange, 2);
    colors.CreateAllele((int)Color.Yellow, 3);
    colors.CreateAllele((int)Color.Green, 4);
    colors.CreateAllele((int)Color.Blue, 5);
    colors.CreateAllele((int)Color.Purple, 6);
    colors.CreateAllele((int)Color.Black, 7);
    colors.CreateAllele((int)Color.White, 8);


    // Define the shape genes
    GeneDefinition shapes = new();
    shapes.CreateAllele((int)Shape.Circle, 3);
    shapes.CreateAllele((int)Shape.Square, 2);
    shapes.CreateAllele((int)Shape.Heart, 1);
    shapes.CreateAllele((int)Shape.Food, 0);

    // Parent1
    GeneticObject parent1 = new();
    Genotype colorTrait1 = new(colors.GetAllele((int)Color.Blue), colors.GetAllele((int)Color.Red));
    parent1.AddGenotype("color", colorTrait1);
    Genotype shapeTrait1 = new(shapes.GetAllele((int)Shape.Square), shapes.GetAllele((int)Shape.Heart));
    parent1.AddGenotype("shape", shapeTrait1);

    // Parent2
    GeneticObject parent2 = new();
    Genotype colorTrait2 = new(colors.GetAllele((int)Color.Red), colors.GetAllele((int)Color.Orange));
    parent2.AddGenotype("color", colorTrait2);
    Genotype shapeTrait2 = new(shapes.GetAllele((int)Shape.Circle), shapes.GetAllele((int)Shape.Heart));
    parent2.AddGenotype("shape", shapeTrait2);

    Console.WriteLine("Welcome to my Genetics Engine demonstration");
    Console.WriteLine("Press [space] to make another child from the parents");
    Console.WriteLine("Press '1' to replace parent 1 with the current child");
    Console.WriteLine("Press '2' to replace parent 2 with the current child");
    Console.WriteLine("Press 'Q' to quit");

    Console.Write("Parents: ");
    Console.Write(RenderColoredShape(parent1));
    Console.WriteLine(RenderColoredShape(parent2));


    // Making babies 😏
    var child = GeneticObject.Combine(parent1, parent2, 0.15);

    bool willQuit = false;
    while (!willQuit)
    {
      Console.Write(RenderColoredShape(child));
      Console.Write(" " + (Color)child.Genotypes["color"].Allele1.Value + ":" + (Color)child.Genotypes["color"].Allele2.Value);
      Console.Write(" " + (Shape)child.Genotypes["shape"].Allele1.Value + ":" + (Shape)child.Genotypes["shape"].Allele2.Value);
      Console.WriteLine();

      var response = Console.ReadKey();
      switch (response.Key)
      {
        case ConsoleKey.Spacebar:
          child = GeneticObject.Combine(parent1, parent2, 0.25);
          break;

        case ConsoleKey.D1:
          parent1 = child;

          Console.Write("Parents: ");
          Console.Write(RenderColoredShape(parent1));
          Console.WriteLine(RenderColoredShape(parent2));
          break;

        case ConsoleKey.D2:
          parent2 = child;
          Console.Write("Parents: ");
          Console.Write(RenderColoredShape(parent1));
          Console.WriteLine(RenderColoredShape(parent2));
          break;

        case ConsoleKey.Q:
          willQuit = true;
          break;

        default:
          Console.WriteLine("Invalid key pressed.");
          break;

      }
    }
  }
}
