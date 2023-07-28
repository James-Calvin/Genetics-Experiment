using System;

namespace Genetic
{
  class Allele
  {
    public static Random rng = new();
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
      return Mutate(rng.Next(2) == 0);
    }

    public Allele Mutate(bool mutatesUp)
    {
      if (geneDefinition == null) throw new Exception("Must set Gene Definition before mutating");

      // We want to guarantee a mutation,
      // so if we cannot mutate down, then we mutate up
      int alleleIndex = geneDefinition.GetIndex(this);
      if (!geneDefinition.CanMutateDown(alleleIndex) ||
      (geneDefinition.CanMutateUp(alleleIndex) && mutatesUp == true))
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