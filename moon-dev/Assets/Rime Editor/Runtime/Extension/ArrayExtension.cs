﻿using System;

namespace LevelEditor.Extension
{
    public static class ArrayExtension
    {
        public static void ForEach(this Array array, Action<Array, int[]> action)
        {
            if (array.LongLength == 0) return;

            var walker = new ArrayTraverse(array);

            do
            {
                action(array, walker.Position);
            } while (walker.Step());
        }
    }

    internal class ArrayTraverse
    {
        private readonly int[] m_maxLengths;
        public readonly  int[] Position;

        public ArrayTraverse(Array array)
        {
            m_maxLengths = new int[array.Rank];
            for (var i = 0; i < array.Rank; ++i) m_maxLengths[i] = array.GetLength(i) - 1;
            Position = new int[array.Rank];
        }

        public bool Step()
        {
            for (var i = 0; i < Position.Length; ++i)
                if (Position[i] < m_maxLengths[i])
                {
                    Position[i]++;
                    for (var j = 0; j < i; j++) Position[j] = 0;
                    return true;
                }

            return false;
        }
    }
}