using MvcProjectArch.UI.Models.Catolog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models
{
    public class ProductHeap
    {
        List<ProductItemModel> arr;
        int arrSize;//size for the array container
        int heapSize;//keeps track of the number of elements
        public ProductHeap()
        {
            arrSize = 0;
            heapSize = 0;
            arr = new List<ProductItemModel>();
        }
      
     
        public void insert(ProductItemModel value)
        {

            arr.Insert(heapSize, value);
                heapSize++;
                siftUp(heapSize - 1);
            
        }
        public void remove(ProductItemModel value)
        {
            for (int i = 0; i < heapSize - 1; i++)
            {
                if (arr[i] == value)
                {
                    arr[i] = arr[heapSize - 1];
                    heapSize--;
                    siftDown(i);
                    break;
                }
            }
        }
        public void removeMin()
        {
            if (heapSize == 0)
            {
                throw new Exception("Heap is empty!");
            }
            else
            {
                arr[0] = arr[heapSize - 1];
                heapSize--;
                if (heapSize > 0)
                {
                    siftDown(0);
                }
            }
        }
        private void siftUp(int index)
        {
            int parentIndex;
            ProductItemModel temp;
            if (index != 0)
            {
                parentIndex = getParentIndex(index);
                if (arr[parentIndex].ProductPrice > arr[index].ProductPrice)
                {
                    temp = arr[parentIndex];
                    arr[parentIndex] = arr[index];
                    arr[index] = temp;
                    siftUp(parentIndex);
                }
            }
        }
        private int getParentIndex(int index)
        {
            return (index - 1) / 2;
        }
        private void siftDown(int nodeIndex)
        {
            int leftChildIndex, rightChildIndex, minIndex;
            ProductItemModel tmp;

            leftChildIndex = getLeftChildIndex(nodeIndex);

            rightChildIndex = getRightChildIndex(nodeIndex);

            if (rightChildIndex >= heapSize)
            {
                if (leftChildIndex >= heapSize)
                {
                    return;
                }
                else
                {
                    minIndex = leftChildIndex;
                }
            }
            else
            {
                if (arr[leftChildIndex].ProductPrice <= arr[rightChildIndex].ProductPrice)
                {
                    minIndex = leftChildIndex;
                }
                else
                {
                    minIndex = rightChildIndex;
                }
            }
            if (arr[nodeIndex].ProductPrice > arr[minIndex].ProductPrice)
            {
                tmp = arr[minIndex];

                arr[minIndex] = arr[nodeIndex];

                arr[nodeIndex] = tmp;

                siftDown(minIndex);
            }
        }
        private int getRightChildIndex(int nodeIndex)
        {
            return (2 * nodeIndex) + 2;
        }
        private int getLeftChildIndex(int nodeIndex)
        {
            return (2 * nodeIndex) + 1;
        }
        public List<ProductItemModel> GetProductByCount(int count,int ProductSize)
        {
            List<ProductItemModel> model = new List<ProductItemModel>();
            for (int i = 0; i < count; i++)
            {
                if (count < ProductSize)
                {
                    model.Add(arr[i]);
                }
                else
                {
                    break;
                }
            }
            return model;
        }
        public ProductItemModel getMin()
        {
            return arr[0];
        }
        //public void BuildMinHeap(ProductItemModel[] input)
        //{
        //    if (heapSize > 0)
        //    {
        //        //clear the current heap
        //        Array.Resize(ref arr, input.Length);
        //        heapSize = 0;
        //        for (int i = 0; i < arr.Count; i++)
        //        {
        //            arr[i] = input[i];
        //            heapSize++;
        //        }
        //    }
        //    for (int i = heapSize - 1 / 2; i >= 0; i--)
        //    {
        //        MinHeapify(i);
        //    }
        //}
        //private void MinHeapify(int index)
        //{
        //    int left = 2 * index;
        //    int right = (2 * index) + 1;
        //    int smallest = index;
        //    if (left < heapSize && arr[left].ProductPrice < arr[index].ProductPrice)
        //    {
        //        smallest = left;
        //    }
        //    else
        //    {
        //        smallest = index;
        //    }
        //    if (right < heapSize && arr[right].ProductPrice < arr[smallest].ProductPrice)
        //    {
        //        smallest = right;
        //    }
        //    if (smallest != index)
        //    {
        //        swap(ref arr, index, smallest);
        //        MinHeapify(smallest);
        //    }
        //}
        private void swap(ref ProductItemModel[] input, int a, int b)
        {
            ProductItemModel temp = input[a];
            input[a] = input[b];
            input[b] = temp;
        }
        //public void deleteHeap()
        //{
        //    Array.Resize(ref arr, 0);
        //    heapSize = 0;
        //}
    }
}