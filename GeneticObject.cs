using System;
using System.Collections.Generic;

namespace Genetic
{
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

    public static GeneticObject Combine(GeneticObject obj1, GeneticObject obj2)
    {
      return Combine(obj1, obj2, 0.0);
    }

    public static GeneticObject Combine(GeneticObject parent1, GeneticObject parent2, double mutationProbability)
    {
      GeneticObject child = new();
      foreach (var trait in parent1.Genotypes)
      {
        if (parent2.GenotypeExists(trait.Key))
        {
          Genotype parent1Trait = parent1.GetGenotype(trait.Key);
          Genotype parent2Trait = parent2.GetGenotype(trait.Key);
          Genotype childTrait = Genotype.Combine(parent1Trait, parent2Trait, mutationProbability);
          child.AddGenotype(trait.Key, childTrait);
        }
      }
      return child;
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

}