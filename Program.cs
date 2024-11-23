using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace s14p01
{
    class Program
    {
        static void Main(string[] args)
        {

            int inicio = 0;
            int final = 5;
            int distancia = 0;
            int n = 0;
            int cantNodos = 7;
            int actual = 0;
            int columna = 0;
            List<int> ruta = new List<int>();

            Grafo grafo = new Grafo(cantNodos);

            grafo.AdicionarArista(0, 1, 2);
            grafo.AdicionarArista(0, 3, 1);
            grafo.AdicionarArista(1, 3, 3);
            grafo.AdicionarArista(1, 4, 10);
            grafo.AdicionarArista(2, 0, 4);
            grafo.AdicionarArista(2, 5, 5);
            grafo.AdicionarArista(3, 2, 2);
            grafo.AdicionarArista(3, 4, 2);
            grafo.AdicionarArista(3, 5, 8);
            grafo.AdicionarArista(3, 6, 4);
            grafo.AdicionarArista(4, 6, 6);
            grafo.AdicionarArista(6, 5, 1);


            Console.WriteLine("=================================");
            grafo.MostrarAdyacencia();
            Console.WriteLine("=================================");


            //tabla auxiliar
            // 0 - visitado
            // 1 - Distancia
            // 2 - previo

            int[,] tabla = new int[cantNodos, 3];

            //inicializar tabla
            for (n = 0; n < cantNodos; n++) {
                tabla[n, 0] = 0;
                tabla[n, 1] = int.MaxValue;
                tabla[n, 2] = 0;
            }

            tabla[inicio, 1] = 0;

            MostrarTabla(tabla);

            Console.WriteLine("=================================");

            //inicio dijkstra

            actual = inicio;

            do
            {
                //marcar como visitado
                tabla[actual, 0] = 1;

                for (columna=0; columna<cantNodos;columna++) {
                    if (grafo.ObtenerAdyacencia(actual, columna) != 0) {
                        distancia = grafo.ObtenerAdyacencia(actual, columna) + tabla[actual, 1];
                        if (distancia < tabla[columna, 1]) {
                            tabla[columna, 1] = distancia;
                            tabla[columna, 2] = actual;

                        }
                    }
                }

                //el nuevo actual es el nodo con la menor distancia

                int indiceMenor = -1;
                int distanciaMenor = int.MaxValue;

                for (int x=0; x<cantNodos; x++) {
                    if (tabla[x,1]<distanciaMenor && tabla[x,0]==0) {
                        indiceMenor = x;
                        distanciaMenor = tabla[x, 1];
                    }
                }

                actual = indiceMenor;

            }
            while (actual!=-1);


            MostrarTabla(tabla);

            Console.WriteLine("=================================");

            // obtener la ruta

            int nodo = final;

            while (nodo!=inicio) {
                ruta.Add(nodo);
                nodo = tabla[nodo, 2];
            }
            ruta.Add(inicio);
            ruta.Reverse();

            foreach (int posicion in ruta)
                Console.Write("{0} => ", posicion);

            Console.WriteLine();
            Console.WriteLine("=================================");


            Console.ReadKey();


        }

        public static void MostrarTabla(int[,] tabla)
        {
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                Console.WriteLine("{0} --> {1}\t{2}\t{3}", i, tabla[i, 0], tabla[i, 1], tabla[i, 2]);
            }
            Console.WriteLine();
        }


    }
}
