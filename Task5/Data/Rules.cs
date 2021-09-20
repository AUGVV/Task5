using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Rules
    {
        public int[,] ResultTable { get; set; }

        public Rules(string[] ConditionMap)
        {
            int WinPointCount = 0;
            int buffer = 0;
            ResultTable = new int[ConditionMap.Length, ConditionMap.Length];

            for (int i = 0; i < ConditionMap.Length; i++)
            {
                WinPointCount = ConditionMap.Length / 2;
                for (int j = 0; j < ConditionMap.Length; j++)
                {
                    if (ConditionMap[i] == ConditionMap[j])
                    {
                        buffer = 2;
                    }
                    else if (i < j && WinPointCount != 0)
                    {
                        buffer = 1;
                        WinPointCount--;
                    }
                    ResultTable[i, j] = buffer;
                    buffer = 0;
                }
                if (WinPointCount != 0)
                {
                    for (int z = 0; z <= WinPointCount; z++)
                    {
                            ResultTable[i, z] = 1;
                            WinPointCount--;
                    }
                }
            }
        }

        public string GiveWinner(int player, int pc, string CreatorName, string ConnectionName)
        {
            int Result = ResultTable[player, pc];
      

            if(Result == 1)
            {
                return $"{CreatorName} win!";
            }
            else if(Result == 2)
            {
                return "Draw";
            }
            else
            {
                return $"{ConnectionName} win!";
            }
        }

    }
}
