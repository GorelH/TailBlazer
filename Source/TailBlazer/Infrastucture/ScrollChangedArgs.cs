﻿using TailBlazer.Views;

namespace TailBlazer.Infrastucture
{
    public class ScrollChangedArgs
    {
        public ScrollDirection Direction { get; }
        public int Value { get; }

        public ScrollChangedArgs(ScrollDirection scrollDirection, int value)
        {
            Direction = scrollDirection;
            Value = value;
        }

    }
}