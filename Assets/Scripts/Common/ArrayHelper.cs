using ArpgDemo.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    //扩展方法：使用this关键字修饰方法的第一个参数，当调用对象为第一个参数类型时，可以直接调用该方法，无需写类名
    public static class ArrayHelper
    {
        public static T[] FindAll<T>(this T[] array, Func<T, bool> handler)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                if (handler(array[i]))
                {
                    result.Add(array[i]);
                }
            }
            return result.ToArray();
        }
        public static T Find<T>(this T[] array, Func<T, bool> handler)
        {
            List<PlayerStatus> result = new List<PlayerStatus>();
            for (int i = 0; i < array.Length; i++)
            {
                if (handler(array[i]))
                {
                    return array[i];
                }
            }
            return default(T);
        }
        public static T GetMax<T, R>(this T[] array, Func<T, R> handler) where R : IComparable
        {
            if (array == null || array.Length == 0) return default(T);
            T max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (handler(max).CompareTo(handler(array[i])) < 0)
                {
                    max = array[i];
                }
            }
            return max;
        }
        public static T GetMin<T, R>(this T[] array, Func<T, R> handler) where R : IComparable
        {
            if (array == null || array.Length == 0) return default(T);
            T min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (handler(min).CompareTo(handler(array[i])) > 0)
                {
                    min = array[i];
                }
            }
            return min;
        }
        public static R[] Select<T, R>(this T[] array, Func<T, R> handler)
        {
            R[] result = new R[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = handler(array[i]);
            }
            return result;
        }
        public static void Foreach<T>(this T[] array,Action<T> handler)
        {
            for (int i = 0; i < array.Length; i++)
            {
                handler(array[i]);
            }
        }
    }
}

