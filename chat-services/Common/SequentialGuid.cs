using System;

namespace chat_service.Common
{
  public class SequentialGuid
  {
    Guid _CurrentGuid;
    public Guid CurrentGuid
    {
      get
      {
        return _CurrentGuid;
      }
    }

    public SequentialGuid()
    {
      _CurrentGuid = Guid.NewGuid();
    }

    public SequentialGuid(Guid previousGuid)
    {
      _CurrentGuid = previousGuid;
    }

    public static SequentialGuid operator ++(SequentialGuid sequentialGuid)
    {
      byte[] bytes = sequentialGuid._CurrentGuid.ToByteArray();
      for (int mapIndex = 0; mapIndex < 16; mapIndex++)
      {
        int bytesIndex = SqlOrderMap[mapIndex];
        bytes[bytesIndex]++;
        if (bytes[bytesIndex] != 0)
        {
          break; // No need to increment more significant bytes
        }
      }
      sequentialGuid._CurrentGuid = new Guid(bytes);

      return sequentialGuid;
    }

    private static int[] _SqlOrderMap = null;
    private static int[] SqlOrderMap
    {
      get
      {
        if (_SqlOrderMap == null)
        {
          // 3 - the least significant byte in Guid ByteArray [for SQL Server ORDER BY clause]
          // 10 - the most significant byte in Guid ByteArray [for SQL Server ORDERY BY clause]
          _SqlOrderMap = new int[16]
          {
                        3, 2, 1, 0, 5, 4, 7, 6, 9, 8, 15, 14, 13, 12, 11, 10
          };
        }

        return _SqlOrderMap;
      }
    }
  }
}