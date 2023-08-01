using System;

namespace GeneticsEngine;
class Allele
{
  public int Value { get; }
  public int Dominance { get; }

  private GeneDefinition geneDefinition;

  public Allele(int value, int dominance)
  {
    Value = value;
    Dominance = dominance;
  }

  public void SetGeneDefinition(GeneDefinition geneDefinition)
  {
    this.geneDefinition = geneDefinition;
  }
  public Allele Mutate(bool mutateUp)
  {
    if (geneDefinition == null) throw new Exception("Must set Gene Definition before mutating");

    // We want to guarantee a mutation,
    // so if we cannot mutate down, then we mutate up
    int alleleIndex = geneDefinition.GetIndex(this);
    if (!geneDefinition.CanMutateDown(alleleIndex) ||
    (geneDefinition.CanMutateUp(alleleIndex) && mutateUp == true))
    {
      return geneDefinition.MutateUp(alleleIndex);
    }
    else
    {
      return geneDefinition.MutateDown(alleleIndex);
    }
  }
}
