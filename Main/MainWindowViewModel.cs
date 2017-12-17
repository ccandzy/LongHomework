using Main.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Main
{
    public class MainWindowViewModel
    {
        private static object _lock = new object();
        private ObservableCollection<int> _beforeIntCollection;
        private ObservableCollection<int> _afterntCollection;

        public bool IsWhile { get; set; }
        public List<int> AllBeforeNumber = new List<int>();
        public List<int> AllAfterNumber = new List<int>();
        public Dictionary<int, List<int>> groupBeforeNumberDic = new Dictionary<int, List<int>>();
        public Dictionary<int, List<int>> groupAfterNumberDic = new Dictionary<int, List<int>>();
       

        public MainWindowViewModel()
        {
            BeforeIntCollection = new ObservableCollection<int> { 0,0, 0, 0, 0};
            AfterIntCollection = new ObservableCollection<int> { 0, 0 };
            CreateBeforeNumber();
            UpsetBeforeNumber();
            GroupingBeforeNumber();
            CreateAfterNumber();
            UpsetAfterNumber();
            GroupingAfterNumber();
        }
        
        public void BeginRefsherData()
        {
            while(IsWhile)
            {
                Thread.Sleep(500);
                SetNumber();
            }
        }

        public ObservableCollection<int> BeforeIntCollection
        {
            get => _beforeIntCollection;
            set
            {
                if (_beforeIntCollection == value) return;
                _beforeIntCollection = value;
            }
        }

        public ObservableCollection<int> AfterIntCollection
        {
            get => _afterntCollection;
            set
            {
                if (_afterntCollection == value) return;
                _afterntCollection = value;
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

        public void UpsetBeforeNumber()
        {
            for (int i = 0; i < 5; i++)
            {
                var firstNumber = new RandomHelper().GetNumber(0, AllBeforeNumber.Count);
                var twoNumber = new RandomHelper().GetNumber(0, AllBeforeNumber.Count);
                var firstValue = AllBeforeNumber[firstNumber];
                AllBeforeNumber[firstNumber] = AllBeforeNumber[twoNumber];
                AllBeforeNumber[twoNumber] = firstValue;
            }
        }

        public void UpsetAfterNumber()
        {
            for (int i = 0; i < 5; i++)
            {
                var firstNumber = new RandomHelper().GetNumber(0, AllAfterNumber.Count);
                var twoNumber = new RandomHelper().GetNumber(0, AllAfterNumber.Count);
                var firstValue = AllAfterNumber[firstNumber];
                AllAfterNumber[firstNumber] = AllAfterNumber[twoNumber];
                AllAfterNumber[twoNumber] = firstValue;
            }
        }

        public void GroupingBeforeNumber()
        {
            int divisor = AllBeforeNumber.Count / 5;
            int remainder = AllBeforeNumber.Count % 5;
            int manyGrouping = remainder == 0 ? divisor : divisor + 1;
            for (int i = 0; i < 5; i++)
            {
                groupBeforeNumberDic.Add(i, GetSectionNumberByIndex(i*divisor,divisor));
            }
        }

        public void GroupingAfterNumber()
        {
            int divisor = AllAfterNumber.Count / 2;
            int remainder = AllAfterNumber.Count % 2;
            int manyGrouping = remainder == 0 ? divisor : divisor + 1;
            for (int i = 0; i < 2; i++)
            {
                groupAfterNumberDic.Add(i, GetSectionAfterNumberByIndex(i * divisor, divisor));
            }
        }

        private List<int> GetSectionAfterNumberByIndex(int startIndex, int length)
        {
            List<int> returnList = new List<int>();
            for (int i = startIndex; i < startIndex + length; i++)
            {
                returnList.Add(AllAfterNumber[i]);
            }
            return returnList;
        }

        private List<int> GetSectionNumberByIndex(int startIndex,int length)
        {
            List<int> returnList = new List<int>();
            for (int i = startIndex; i < startIndex+length; i++)
            {
                returnList.Add(AllBeforeNumber[i]);
            }
            return returnList;
        }

        private void SetNumber()
        {
            List<Task> taskList = new List<Task>();
            foreach (var itemDic in groupBeforeNumberDic)
            {
                taskList.Add(Task.Run(() =>
                {
                    var randomInt = new RandomHelper().GetNumber(1, itemDic.Value.Count);
                    Application.Current.Dispatcher.BeginInvoke(new Action(() => BeforeIntCollection[itemDic.Key] = itemDic.Value[randomInt]));
                }));
            }

            foreach (var itemDic in groupAfterNumberDic)
            {
                taskList.Add(Task.Run(() =>
                {
                    var randomInt = new RandomHelper().GetNumber(1, itemDic.Value.Count);
                    Application.Current.Dispatcher.BeginInvoke(new Action(() => AfterIntCollection[itemDic.Key] = itemDic.Value[randomInt]));
                }));
            }

            Task.WaitAll();
            groupBeforeNumberDic.Clear();
            groupAfterNumberDic.Clear();
            UpsetBeforeNumber();
            GroupingBeforeNumber();
            UpsetAfterNumber();
            GroupingAfterNumber();
        }
    }
}
