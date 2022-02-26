using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chat_service.Common
{
  public class AggregateRootBase
  {
    protected AggregateRootBase()
    {
      Id = GenerateId();
      Created = DateTime.Now;
    }

    [Key]
    public Guid Id { get; protected set; }

    public DateTime Created { get; protected set; }

    public DateTime Updated { get; protected set; }

    public static Guid GenerateId(string guid = "")
    {
      if (string.IsNullOrEmpty(guid))
      {
        var sequentialGuid = new SequentialGuid();
        return sequentialGuid.CurrentGuid;
      }

      return Guid.Parse(guid);
    }
  }
}