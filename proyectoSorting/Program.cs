using System;
using System.Diagnostics;

namespace ProyectoSorting  // No pilla la directa de ensamblado en el TEst.
{
    class Program
    {
        public class SortingArray //classe
        {

            public int[] array; 
            public int[] arrayCreciente;
            public int[] arrayDecreciente;

            public SortingArray(int elements,Random random) //Funcion ordenado
            {
                array = new int[elements];
                arrayCreciente = new int[elements];
                arrayDecreciente = new int[elements];
                for (int i = 0; i < elements; i++)
                {
                    array[i] = random.Next();
                }
                Array.Copy(array, arrayCreciente, elements);
                Array.Sort(arrayCreciente);
                Array.Copy(arrayCreciente, arrayDecreciente, elements);
                Array.Reverse(arrayDecreciente);
            }

            public void Sort(Action<int[]> func) //utilizaremos esta funcion para los cuatro tipos de ordenado y que cada vez me utilice un tipo u otro.
            {   //Action para funciones que devuelven cosas y func para funciones que no devuelven nada.

                Stopwatch time = new Stopwatch();
                int[] temp = new int[array.Length]; //hacemos una copia para que no nos modifique la original
                Array.Copy(array, temp, array.Length);
                Console.WriteLine(func.Method.Name);
                time.Start();

                func(temp); //Usamos una funcion en concreta para ordenar que necesita una array a
                             //ordenar int[] temp le copio los elementos de la array que esta mezclado y se la paso a esta funcion.

                time.Stop();

                Console.WriteLine("Initial:" + time.ElapsedMilliseconds + "ms" + time.ElapsedTicks + "ticks");

                time.Reset();
               
                time.Start();
                func(temp);
                time.Stop();
                Console.WriteLine("Increasing:" + time.ElapsedMilliseconds + "ms" + time.ElapsedTicks + "ticks");
                time.Reset();
                Array.Reverse(temp);

                time.Start();
                func(temp);
                time.Stop();
                Console.WriteLine("Decreasing:" + time.ElapsedMilliseconds + "ms" + time.ElapsedTicks + "ticks");

            }

            public void BubbleSort(int[] array)
            {
                for (int i = 0; i < array.Length -1; i++)
                {
                    for (int j = 0; j < array.Length-1; j++)
                    {
                        
                        if(array[j]> array[j+1])
                        {
                            int temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;

                        }
                    }
                }

            }
            public void BubbleSortEarlyExit(int[] array)
            {
                bool ordered = true;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    ordered = true;
                    for (int j = 0; j < array.Length - 1; j++)
                    {

                        if (array[j] > array[j + 1])
                        {
                            ordered = false;
                            int temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;

                        }
                    }
                    if (ordered)
                        return;
                }


            }
            public void QuickSort(int[] array)
            {
                QuickSort(array, 0, array.Length);
            }

            public void QuickSort(int[] array, int left, int right)
            {
                if(left< right)
                {
                    int pivot = QuickSortPivot(array,left,right);
                    QuickSort(array, left, pivot);
                    QuickSort(array, pivot+1, right);
                }
                
            }
            public int QuickSortPivot(int[] array, int left, int right)
            {
                int pivot = array[(left + right) / 2];
                while (true)
                {
                    while (array[left] < pivot)
                    {
                        left++;
                    }
                    while (array[right] > pivot)
                    {
                        right--;
                    }
                    if(left>= right)
                    {
                        return right;
                    }
                    else
                    {
                        int temp = array[left];
                        array[left] = array[right];
                        array[right]=temp;
                        right--; left++;
                    }
                }
                
            }
            public void InsertionSort(int[] array)
            {
                int n = array.Length;
                int val;
                int flag;

                for (var i = 1; i < n; i++)
                {
                    val = array[i];
                    flag = 0;
                    for (var j = i - 1; j >= 0 && flag != 1;)
                    {
                        if (val < array[j])
                        {
                            array[j + 1] = array[j];
                            j--;
                            array[j + 1] = val;
                        }
                        else flag = 1;
                    }
                }
            }

            public void MergeSort(int[] array)
            {
                this.MergeSortAux(array);
                return;
            }
            public int[] MergeSortAux(int[] array)
            {
                int[] left;
                int[] right;
                int[] result = new int[array.Length];
                //As this is a recursive algorithm, we need to have a base case to 
                //avoid an infinite recursion and therfore a stackoverflow
                if (array.Length <= 1)
                    return array;
                // The exact midpoint of our array  
                int midPoint = array.Length / 2;
                //Will represent our 'left' array
                left = new int[midPoint];

                //if array has an even number of elements, the left and right array will have the same number of 
                //elements
                if (array.Length % 2 == 0)
                    right = new int[midPoint];
                //if array has an odd number of elements, the right array will have one more element than left
                else
                    right = new int[midPoint + 1];
                //populate left array
                for (int i = 0; i < midPoint; i++)
                    left[i] = array[i];
                //populate right array   
                int x = 0;
                //We start our index from the midpoint, as we have already populated the left array from 0 to 
                // midpont
            for (int i = midPoint; i < array.Length; i++)
                {
                    right[x] = array[i];
                    x++;
                }
                //Recursively sort the left array
                left = MergeSortAux(left);
                //Recursively sort the right array
                right = MergeSortAux(right);
                //Merge our two sorted arrays
                result = merge(left, right);
                return result;
            }

            private static int[] merge(int[] left, int[] right)
            {
                int resultLength = right.Length + left.Length;
                int[] result = new int[resultLength];
                //
                int indexLeft = 0, indexRight = 0, indexResult = 0;
                //while either array still has an element
                while (indexLeft < left.Length || indexRight < right.Length)
                {
                    //if both arrays have elements  
                    if (indexLeft < left.Length && indexRight < right.Length)
                    {
                        //If item on left array is less than item on right array, add that item to the result array 
                        if (left[indexLeft] <= right[indexRight])
                        {
                            result[indexResult] = left[indexLeft];
                            indexLeft++;
                            indexResult++;
                        }
                        // else the item in the right array wll be added to the results array
                        else
                        {
                            result[indexResult] = right[indexRight];
                            indexRight++;
                            indexResult++;
                        }
                    }
                    //if only the left array still has elements, add all its items to the results array
                    else if (indexLeft < left.Length)
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    //if only the right array still has elements, add all its items to the results array
                    else if (indexRight < right.Length)
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                return result;
            }



        }
        static void Main(string[] args)
        {
            Console.WriteLine("How many elements do you want"); //Pedimos por pantalla
            int elements= int.Parse(Console.ReadLine());
            

            Console.WriteLine("what seet do you want to use"); //Cuantos seet
            int seed = int.Parse(Console.ReadLine());

            Random randon = new Random(seed);
            SortingArray array = new SortingArray(elements,randon);
            //array.Sort(array.BubbleSort);
            //array.Sort(array.BubbleSortEarlyExit);
             //array.Sort(array.QuickSort);
            //array.Sort(array.InsertionSort);
            array.Sort(array.MergeSort);
        }
            
    }
}
