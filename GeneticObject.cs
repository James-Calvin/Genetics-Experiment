using System;
using System.Collections.Generic;

namespace Genetic;

class GeneticObject
{
  private Dictionary<string, Genotype> Genotypes { get; set; }

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

  public int GetAlleleValue(string trait, int alleleIndex)
  {
    if (!Genotypes.ContainsKey(trait)) throw new Exception("Trait " + trait + " does not exist in this Genotype");
    return Genotypes[trait].GetAllele(alleleIndex).Value;
  }

  public void Mutate(string traitName, int alleleIndex, bool mutateUp)
  {
    if (!GenotypeExists(traitName)) throw new Exception("trait name does not exist");
    Allele mutatedAllele = Genotypes[traitName].GetAllele(alleleIndex).Mutate(mutateUp);
    Genotypes[traitName].SetAllele(alleleIndex, mutatedAllele);
  }

  public static GeneticObject Combine(GeneticObject parent1, GeneticObject parent2, int configuration)
  {
    GeneticObject child = new();
    foreach (var trait in parent1.Genotypes)
    {
      if (parent2.GenotypeExists(trait.Key))
      {
        Genotype parent1Trait = parent1.GetGenotype(trait.Key);
        Genotype parent2Trait = parent2.GetGenotype(trait.Key);
        Genotype childTrait = Genotype.Combine(parent1Trait, parent2Trait, configuration);
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

