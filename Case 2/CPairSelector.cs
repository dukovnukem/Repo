using System;
using System.Collections.Generic;
using System.Linq;

namespace ChoosePairs
{
    //Задание 2. Есть коллекция чисел и отдельное число Х.
    //Надо вывести все пары чисел, которые в сумме равны заданному Х.
    class CPairSelector
    {
        public static IEnumerable<Tuple<Int32, Int32>> Select(ICollection<Int32> col, Int32 x)
        {
            Int32[] numbers = col.ToArray();

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[i] + numbers[j] == x)
                        yield return Tuple.Create(numbers[i], numbers[j]);
                }
            }
        }
    }
}