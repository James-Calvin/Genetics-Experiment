using System;
using System.Collections.Generic;

namespace GeneticsEngine;
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

  public void CreateAllele(int value, int dominance)
  {
    Allele allele = new(value, dominance);
    AddAllele(allele);
  }

  public Allele GetAllele(int index)
  {
    return alleles[index];
  }

  public int GetIndex(Allele allele)
  {

    for (int i = 0; i < alleles.Count; i++)
    {
      if (alleles[i].Value == allele.Value &&
      alleles[i].Dominance == allele.Dominance)
        return i;
    }

    throw new Exception("The allele is not in this gene definition");
  }

  public Allele MutateDown(int index)
  {
    if (CanMutateDown(index))
    {
      return alleles[index - 1];
    }
    else
    {
      return alleles[index];
    }
  }

  public bool CanMutateDown(int index)
  {
    if (index > 0) return true;
    return false;
  }

  public Allele MutateUp(int index)
  {
    if (CanMutateUp(index))
    {
      return alleles[index + 1];
    }
    else
    {
      return alleles[index];
    }
  }

  public bool CanMutateUp(int index)
  {
    if (index < alleles.Count - 1) return true;
    return false;
  }
}

