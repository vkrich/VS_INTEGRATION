using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Massiv
    { // закрытые поля
        int size; // количество элементов массива
        int[] mas;
        // свойство
        public int Size
        {
            // свойство
            get
            {                    // способ получения свойства
                return size;
            }
            set
            {                      // способ установки свойства
                if (value >= 1)
                {
                    size = value;
                }
                else
                {
                    size = 0;
                }
            }
        }
        public string CreateMas()
        {
            string s = "";
            if (size == 0) return s;
            else
            {
                Random r = new Random();
                mas = new int[size];
                // заполнение массива

                for (int i = 0; i < size; i++)
                {
                    mas[i] = r.Next(-100, 100);
                    s = s + mas[i].ToString() + " ";
                }
                return s;
            }
        }
    }
    //функция вычисления суммы элементов массива
   
    
}
