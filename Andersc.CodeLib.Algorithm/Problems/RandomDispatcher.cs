using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Andersc.CodeLib.Common;
using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Algorithm.Problems
{
    public class DispatchTarget<TTarget>
    {
        public TTarget Target { get; set; }
        public int Weight { get; set; }
    }

    public class DispatchResult<TTarget, TData>
    {
        public DispatchTarget<TTarget> Target { get; set; }
        public List<TData> Data { get; set; }
    }

    public class RandomDispatcher<TTarget, TData>
    {
        private Random Rand { get; set; }
        private int Min { get; set; }
        private int Max { get; set; }

        private List<TData> Data { get; set; }
        private List<DispatchTarget<TTarget>> Targets { get; set; }
        private List<int> TargetIndex { get; set; }

        private int Units
        {
            get { return Data.Count/Max; }
        }

        public RandomDispatcher(List<DispatchTarget<TTarget>> targets, List<TData> data) 
        {
            if (targets.IsEmpty())
            {
                throw new ArgumentException("No targets set.");
            }

            Rand = new Random();
            Min = 0;
            Max = targets.Aggregate(0, (i, target) => i + target.Weight);
            //Console.WriteLine(Max);

            Data = data;
            Targets = targets.Where(t => t.Weight > 0).ToList();
            TargetIndex = Targets.Select(tar => tar.Weight).ToList();
            TargetIndex[0] = 0;
            for (var i = 1; i < Targets.Count; i++)
            {
                TargetIndex[i] = TargetIndex[i - 1] + Targets[i - 1].Weight;
            }

            //var units = Data.Count/Max;
            //TargetQuota = Targets.Select(tar => tar.Weight * units).ToList();

            //TargetIndex.ToArray().PrintToConsole();
        }

        private int GetNextIndex()
        {
            return Rand.Next(Min, Max);
        }

        private int GetNextTarget(int index)
        {
            for (var i = TargetIndex.Count - 1; i >= 0; i--)
            {
                if (TargetIndex[i] <= index)
                {
                    return i;
                }
            }


            return -1;
        }

        public List<DispatchResult<TTarget, TData>> Dispatch()
        {
            var result = new List<DispatchResult<TTarget, TData>>();

            var loopData = Data.Take(Max*Units).ToList();
            for(var i = 0; i < loopData.Count; i++)
            {
                if (!DispatchNext(result, loopData[i]))
                {
                    i--;
                }
            }

            var rest = Data.Skip(loopData.Count).ToList();
            for (var i = 0; i < rest.Count; i++)
            {
                DispatchNext(result, rest[i], false);
            }

            return result;
        }

        private bool DispatchNext(List<DispatchResult<TTarget, TData>> result, TData data, bool checkQuota = true)
        {
            var target = Targets[GetNextTarget(GetNextIndex())];

            var distributed = result.FirstOrDefault(r => r.Target == target);
            if (distributed.IsNotNull())
            {
                if (checkQuota && distributed.Data.Count >= (distributed.Target.Weight * Units))
                {
                    //Console.WriteLine("Full");
                    return false;
                }
                distributed.Data.Add(data);
            }
            else
            {
                var dr = new DispatchResult<TTarget, TData>() { Data = new List<TData>(), Target = target };
                dr.Data.Add(data);
                result.Add(dr);
            }

            return true;
        }
    }
}
