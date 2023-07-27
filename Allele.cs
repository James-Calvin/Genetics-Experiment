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

    public void Mutate()
    {
      if (geneDefinition == null) throw new System.Exception("Must set Gene Definition before mutating");

      // We want to guarantee a mutation,
      // so if we cannot mutate down, then we mutate up
      if (!geneDefinition.CanMutateDown(this) ||
      (new Random().Next(2) == 0 && geneDefinition.CanMutateUp(this)))
      {
        geneDefinition.MutateUp(this);
      }
      else
      {
        geneDefinition.MutateDown(this);
      }
    }
  }
}