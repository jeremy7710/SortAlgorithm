using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortAlgorithm
{
    public partial class Form1 : Form
    {
        int[] arr;//, arr2;

        public Form1()
        {
            InitializeComponent();

            resetRandomArray();

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

        }


        private void ResetBtn_Click(object sender, EventArgs e)
        {
            resetRandomArray();
        }

        private void SortBtn_Click(object sender, EventArgs e)
        {

            // Create List data structure and convert to integer array
            string text = textBox1.Text;

            int index = 0;
            string ans = ""; //text.Substring(index + 2, text.IndexOf(",", index));
            List<int> list = new List<int>();

            while (index + 2 < text.Length)
            {
                if (index == 0)
                    ans = text.Substring(0, text.IndexOf(","));
                else
                {
                    int start = index + 2, end = text.IndexOf(",", index + 1);
                    ans = text.Substring(start, end - start);
                }

                index = text.IndexOf(",", index + 1);

                list.Add(int.Parse(ans));
                //Console.WriteLine("index: " + index);
                //Console.WriteLine(ans);
            }

            // Convert to integer array
            arr = new int[list.Count];


            int i = 0;
            foreach (int number in list)
            {
                arr[i] = number;
                i++;
            }


            // Sort the numbers
            //if(comboBox1.SelectedIndex == 1)
            //Console.WriteLine("SelectedIndex: " + comboBox1.SelectedIndex);
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    bubbleSort();
                    break;
                case 1:
                    selectSort();
                    break;
                case 2:
                    insertSort();
                    break;
                case 3:
                    mergeSort(0, arr.Length - 1);
                    break;
                case 4:
                    quickSort(0, arr.Length - 1);
                    break;
                case 5:
                    heapSort();
                    break;
                case 6:
                    shellSort();
                    break;
                case 7:
                    radixSort();
                    break;
                default:
                    MessageBox.Show("Please select a sort algorithm.");
                    return;
            }

            





            // Print on label1
            label1.Text = comboBox1.Text + ":\n\n";

            for (i = 0; i < list.Count; i++)
            {
                label1.Text += arr[i] + ", ";
                //Console.Write(arr[i] + ", ");
            }

        }

        private void radixSort()
        {
            int i, k, n, m;
            for (n = 1; n <= 100; n *= 10)
            {
                int[,] tmp = new int[10, arr.Length];
                for (i = 0; i < arr.Length; i++)
                {
                    m = (arr[i] / n) % 10;
                    if (m < 0)
                    {
                        MessageBox.Show("Radix Sort can't sort nagitive number.");
                        return;
                    }
                    tmp[m, i] = arr[i];
                }
                k = 0;
                for (m = 0; m < 10; m++)
                {
                    for (i = 0; i < arr.Length; i++)
                    {
                        if (tmp[m, i] != 0)
                        {
                            arr[k] = tmp[m, i];
                            k++;
                        }
                    }
                }

            }
        }

        private void shellSort()
        {
            int i, j, tmp, index;
            index = arr.Length / 2;

            while (index != 0)
            {
                for (i = index; i < arr.Length; i++)
                {
                    tmp = arr[i];
                    j = i - index;
                    while (tmp < arr[j])
                    {
                        arr[j + index] = arr[j];
                        j -= index;
                        if (j < 0)
                            break;
                    }
                    arr[index + j] = tmp;
                }
                index /= 2;
            }
        }

        private void heapSort()
        {
            int i;

            for (i = arr.Length / 2 - 1; i >= 0; i--)
                cheap(i, arr.Length);

            for (i = arr.Length - 1; i > 0; i--)
            {
                swap(0, i);
                cheap(0, i);
            }
        }

        private void cheap(int b, int bound)
        {
            while (b < bound / 2)
            {
                int cnode = b * 2 + 1;
                if (cnode + 1 < bound && arr[cnode] < arr[cnode + 1])
                    cnode++;

                if (arr[b] < arr[cnode])
                {
                    swap(b, cnode);
                }
                b = cnode;
            }
        }

        private void quickSort(int left, int right)
        {
            int i = left + 1, point = right;

            if (left >= right)
                return;

            while (true)
            {
                while (i <= right && arr[i] < arr[left])
                    i++;
                while (arr[point] > arr[left])
                    point--;
                if (i >= point)
                    break;
                swap(i, point);
            }
            swap(left, point);

            quickSort(left, point - 1);
            quickSort(point + 1, right);
        }

        private void mergeSort(int left, int right)
        {
            int i, j, k, mid;
            int[] arr2 = new int[this.arr.Length];

            if (left >= right)
                return;

            mid = (left + right) / 2;
            mergeSort(left, mid);
            mergeSort(mid + 1, right);

            for (i = left; i <= mid; i++)
                arr2[i] = arr[i];
            for (j = mid + 1; j <= right; j++)
                arr2[right + (mid - j + 1)] = arr[j];

            i = left; j = right;
            for (k = left; k <= right; k++)
            {
                if (arr2[i] <= arr2[j])
                    arr[k] = arr2[i++];
                else
                    arr[k] = arr2[j--];
            }
        }

        private void insertSort()
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int j = i, tmp = arr[i + 1];
                while (j >= 0 && tmp < arr[j])
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = tmp;
            }
        }

        private void selectSort()
        {
            int minIndex;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                minIndex = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                        minIndex = j;
                }

                swap(minIndex, i);
            }
        }

        private void bubbleSort()
        {
            for (int i = arr.Length - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        swap(j, j + 1);
                    }
                }
            }
        }

        private void swap(int index1, int index2)
        {
            if (index1 == index2)
                return;

            int temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;
        }

        private void resetRandomArray()
        {
            int[] a = new int[10];
            Random rand = new Random();
            string text = "";

            for (int i = 0; i < 10; i++)
            {
                a[i] = rand.Next(-100, 100);

                text = text + a[i] + ", ";
                //Console.WriteLine(a[i]);
            }

            textBox1.Text = text;
        }


    }
}
