using Main.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class MainWindowViewModel
    {
        public List<int> AllBeforeNumber = new List<int>();
        public List<int> AllAfterNumber = new List<int>();
        public Dictionary<string, List<int>> groupBeforeNumberDic = new Dictionary<string, List<int>>();
        public Dictionary<string, List<int>> groupAfterNumberDic = new Dictionary<string, List<int>>();
        public MainWindowViewModel()
        {
            CreateBeforeNumber();
            CreateAfterNumber();
        }

        public void UpsetBeforeNumber()
        {
            for (int i = 0; i < 50; i++)
            {
                var firstNumber = new RandomHelper().GetNumber(0, AllBeforeNumber.Count);
                var twoNumber = new RandomHelper().GetNumber(0, AllBeforeNumber.Count);
                var firstValue = AllBeforeNumber[firstNumber];
                AllBeforeNumber[firstNumber] = AllBeforeNumber[twoNumber];
                AllBeforeNumber[twoNumber] = firstValue;
            }
        }

        private void CreateBeforeNumber()
        {
            for (int i = 1; i < 36; i++)
            {
                AllBeforeNumber.Add(i);
            }
        }

        private void CreateAfterNumber()
        {
            for (int i = 1; i < 13; i++)
            {
                AllAfterNumber.Add(i);
            }
        }
    }
}
