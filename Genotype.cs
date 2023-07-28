using System;

namespace Genetic
{
  class Genotype
  {
    private readonly static Random rng = new();
    public Allele Allele1 { get; set; }
    public Allele Allele2 { get; set; }

    public Genotype(Allele allele1, Allele allele2)
    {
      Allele1 = allele1;
      Allele2 = allele2;
    }

    public Genotype(Allele allele)
    {
      Allele1 = allele;
      Allele2 = allele;
    }

    public static Genotype Combine(Genotype genotype1, Genotype genotype2)
    {
      return Combine(genotype1, genotype2, 0.0);
    }

    public static Genotype Combine(Genotype genotype1, Genotype genotype2, double mutationProbability)
    {

      Allele allele1 = rng.Next(2) == 0 ? genotype1.Allele1 : genotype1.Allele2;
      Allele allele2 = rng.Next(2) == 0 ? genotype2.Allele1 : genotype2.Allele2;
      if (rng.NextDouble() < mutationProbability)
      {
        allele1 = allele1.Mutate();
      }
      if (rng.NextDouble() < mutationProbability)
      {
        allele2 = allele2.Mutate();
      }

      return new Genotype(allele1, allele2);
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
}