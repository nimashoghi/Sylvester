﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sylvester
{
    public class Sn<T> : FrameC<T> where T : struct, IEquatable<T>, IComparable<T>, IConvertible, IFormattable
    {
        public Sn(T[] data, string label) : base(label)
        {
            Data = data;
        }

        public T[] Data { get; }

        public override int Length => Data.Length;

        public override T this[int index] => Data[index];

        public override ref T Ref(int index) => ref Data[index];

        public override IEnumerator GetEnumerator() => Data.GetEnumerator();

        public override bool SetVal(int index, dynamic value)
        {
            Data[index] = value;
            return true;
        }

        public override ISeries Append(params dynamic[] values)
        {
            T[] a = new T[Data.Length + values.Length];
            Data.CopyTo(a, 0);
            values.CopyTo(a, Length);
            return new Sn<T>(a, Label);
        }
    }
}
