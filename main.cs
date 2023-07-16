using System;

/*
What should the genetics object do?
Constructor based on parents
Add traits and which is dominate
Add genes
create a default
create a random
create a child
mutate
allow for co-dominate traits?
*/

class Trait
{
  private String name;
  private Int64 minValue;
  private Int64 maxValue;

  public Trait(String name, Int64 minValue, Int64 maxValue)
  {
    this.minValue = minValue;
    this.maxValue = maxValue;
  }
}

class GeneticObject
{

  public List<Trait> traits = new List<Trait>();

  public GeneticObject() { }



}

class Program
{
  public static void Main(string[] args)
  {
    Console.WriteLine("Hello World");
  }
}