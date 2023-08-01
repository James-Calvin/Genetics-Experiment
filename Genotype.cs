namespace GeneticsEngine;
class Genotype
{
  private Allele Allele1;
  private Allele Allele2;

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

  public static Genotype Combine(Genotype genotype1, Genotype genotype2, int configuration)
  {
    Allele allele1 = (configuration & 0b1) == 0 ? genotype1.Allele1 : genotype1.Allele2;
    Allele allele2 = (configuration & 0b10) == 0 ? genotype2.Allele1 : genotype2.Allele2;
    return new Genotype(allele1, allele2);
  }

  public int GetDominantValue()
  {
    return Allele1.Dominance >= Allele2.Dominance ? Allele1.Value : Allele2.Value;
  }

  public Allele GetAllele(int index)
  {
    return index switch
    {
      0 => Allele1,
      1 => Allele2,
      _ => throw new System.Exception("Allele index out of bounds."),
    };
  }

  public void SetAllele(int index, Allele allele)
  {
    if (index == 0)
    {
      Allele1 = allele;
    }
    else if (index == 1)
    {
      Allele2 = allele;
    }
    else throw new System.Exception("Allele index out of bounds.");
  }
}
