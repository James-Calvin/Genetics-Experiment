using System.Collections.Generic;

namespace LocalTestingUtilities
{
  class Statistic<T>
  {
    private readonly Dictionary<T, int> record = new();

    public Statistic()
    {
    }

    public bool ContainsKey(T metric)
    {
      return record.ContainsKey(metric);
    }

    public void IncrementMetric(T metric)
    {
      IncrementMetric(metric, 1);
    }

    public void IncrementMetric(T metric, int value)
    {
      if (record.ContainsKey(metric))
      {
        record[metric] += value;
      }
      else
      {
        record.Add(metric, value);
      }
    }


    public Dictionary<T, int> GetRecord()
    {
      return record;
    }

    public int GetMetricValue(T metric)
    {
      if (record.ContainsKey(metric))
      {
        return record[metric];
      }
      else
      {
        return 0;
      }
    }

    public int GetTotal()
    {
      int sum = 0;
      foreach (var entry in record)
      {
        sum += entry.Value;
      }
      return sum;
    }
  }
}