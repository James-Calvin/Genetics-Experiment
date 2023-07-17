using System;

/*
What should the genetics object do?
Constructor based on parents
Add traits and which is dominant
Add genes
create a default
create a random
create a child
mutate
allow for co-dominant traits?
*/

class DnaTemplate
{


  public void AddGeneDefinition(GeneDefinition gene)
  {
    throw new NotImplementedException();
  }

  public Genotype InstantiateGenotype()
  {
    throw new NotImplementedException();
  }
}

class Genotype
{

}


class GeneDefinition
{
  string name;
  int range;

  public GeneDefinition(string name, int range)
  {
    this.name = name;
    this.range = range;
  }

  public Gene MakeGene()
  {
    return new Gene(name, range);
  }

  public Gene MakeGene(int value1, int value2)
  {
    if (!WithinRange(value1)) throw new ArgumentException("value1 out of range");
    if (!WithinRange(value2)) throw new ArgumentException("value2 out of range");
    return new Gene(name, range, new int[] { value1, value2 });
  }

  public bool WithinRange(int value)
  {
    return (value < range - 1 && value >= 0);
  }

}

class Gene
{
  string name;
  int range;
  int[] values = new int[] { 0, 0 };

  public Gene(string name, int range)
  {
    this.name = name;
    this.range = range;
  }

  public Gene(string name, int range, int[] values)
  {
    this.name = name;
    this.range = range;
    this.values = values;
  }

  public void SetValues(int value1, int value2)
  {
    this.values[0] = value1;
    this.values[1] = value2;
  }
}


class Program
{
  public static void Main(string[] args)
  {
    // How would we like to use Genetics?

    // Instantiate a blank template
    var flowerDna = new DnaTemplate();

    // Define the possible Gene space
    var shapeGene = new GeneDefinition("shape", 3); // ⚪🤍⬜
    flowerDna.AddGeneDefinition(shapeGene);

    var colorGene = new GeneDefinition("color", 8); // 💔🧡💛💚💙💜🖤🤍
    flowerDna.AddGeneDefinition(colorGene);

    var flower1 = flowerDna.InstantiateGenotype();
  }
}