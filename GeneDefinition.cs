using System;
using System.Collections.Generic;

namespace Genetic
{
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
      allele.SetGeneDefinition(this);
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

      throw new Exception("The allele is not in this gene definition");
    }

    public Allele MutateDown(Allele allele)
    {
      int index = this.GetIndex(allele);
      if (CanMutateDown(index))
      {
        return alleles[index - 1];
      }
      else
      {
        return allele;
      }
    }

    public bool CanMutateDown(int index)
    {
      if (index > 0) return true;
      return false;
    }

    public bool CanMutateDown(Allele allele)
    {
      int index = GetIndex(allele);
      return CanMutateDown(index);
    }

    public Allele MutateUp(Allele allele)
    {
      int index = GetIndex(allele);
      if (CanMutateUp(index))
      {
        return alleles[index + 1];
      }
      else
      {
        return allele;
      }
    }

    public bool CanMutateUp(Allele allele)
    {
      int index = GetIndex(allele);
      return CanMutateUp(index);
    }

    public bool CanMutateUp(int index)
    {
      if (index < alleles.Count - 1) return true;
      return false;
    }
  }

}