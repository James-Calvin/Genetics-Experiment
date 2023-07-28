using System;

namespace Genetic
{
  class Allele
  {
    public int Value { get; set; }
    public int Dominance { get; set; }

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

    public Allele Mutate()
    {
      if (geneDefinition == null) throw new Exception("Must set Gene Definition before mutating");

      // We want to guarantee a mutation,
      // so if we cannot mutate down, then we mutate up
      int alleleIndex = geneDefinition.GetIndex(this);
      if (!geneDefinition.CanMutateDown(alleleIndex) ||
      (geneDefinition.CanMutateUp(alleleIndex) && new Random().Next(2) == 0))
      {
        return geneDefinition.MutateUp(alleleIndex);
      }
      else
      {
        return geneDefinition.MutateDown(alleleIndex);
      }
    }
  }
}