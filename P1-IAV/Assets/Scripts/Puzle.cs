using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace P1
{

    public class Puzle : MonoBehaviour
    {
        enum tipo { Normal, Agua, Barro, Piedra, Meta };
                    //0    //1      //2     //3     //4     //5
        //aux para Ramdon
        private int nAgua = 1;
        private int nBarro = 1;
        private int nPiedras = 1;
        int aux_ = 0;
        //control
        private bool nMetas = false;


        public uint rows;
        public uint columns;
        private uint[,] matrix;

        // Mantiene una referencia actualizada a la posición del hueco, siempre (por eficiencia) 
        // Lo podría llamar sólo Gap (o SpecialValue)
        public Position GapPosition { get; private set; }

        private static readonly uint GAP_VALUE = 0u;
        private static readonly uint DEFAULT_ROWS = 3u;
        private static readonly uint DEFAULT_COLUMNS = 3u;

        //constructor
        public Puzle(uint rows, uint columns)
        {
            if (rows == 0) throw new ArgumentException(string.Format("{0} is not a valid rows value", rows), "rows");
            if (columns == 0) throw new ArgumentException(string.Format("{0} is not a valid columns value", columns), "columns");

            aux_ = ((int)rows + (int)columns);
            nAgua = aux_;
            nBarro = aux_ * 4;
            nPiedras = aux_*2;

            this.Initialize(rows, columns);
        }


        public void Initialize(uint rows, uint columns)
        {
            if (rows == 0) throw new ArgumentException(string.Format("{0} is not a valid rows value", rows), "rows");
            if (columns == 0) throw new ArgumentException(string.Format("{0} is not a valid columns value", columns), "columns");

            this.rows = rows;
            this.columns = columns;

            // Podría aprovecharse la matriz que ya estuviese creada, pero por ahora no lo hacemos
            matrix = new uint[rows, columns];

            for (var r = 0u; r < rows; r++)
                for (var c = 0u; c < columns; c++)
                {
                    matrix[r, c] = GetRamdomValue(r, c);
                    if (matrix[r, c] == GAP_VALUE)//Preguntgar????
                        GapPosition = new Position(r, c);
                }
        }
        // Devuelve el valor contenido (uint) en una determinada posición
        // Si no hay ningún valor, se devolverá nulo
        public uint GetValue(Position position)
        {
            // if (position == null) throw new ArgumentNullException(nameof(position));
            if (position.GetRow() >= rows) throw new ArgumentException(string.Format("{0} is not a valid row for this matrix", position.GetRow()), "row");
            if (position.GetColumn() >= columns) throw new ArgumentException(string.Format("{0} is not a valid column for this matrix", position.GetColumn()), "column");

            // Se podría ver si la posición es la de GapPosition y devolver el valor de hueco directamente
            return matrix[position.GetRow(), position.GetColumn()];
        }
        // Devuelve el que se considera valor inicial por defecto de una posición
        private uint GetDefaultValue(uint row, uint column)
        {
            // Control de excepciones
            uint aux = (row * columns) + column + 1u; // Se suma 1 porque los seres humanos preferimos empezar contando desde el 1 para los valores
            if (aux != rows * columns)
                return aux;
            else
                // Quedará a GAP_VALUE la posición de la matriz a la que le tocaría recibir el rows * columns - 1
                // En realidad ya vale 0 al estar inicializada (si siempre fuese a ser el 0 podría hacerse con la operación módulo)
                return GAP_VALUE;
        }

        private uint GetRamdomValue(uint row, uint column)
        {
            int aux = 0;

            aux = UnityEngine.Random.Range(0, 5);
            //casilla de meta
            if (aux == 4 && nMetas) aux = (int)GetRamdomValue(row, column);
            if (aux == 4 && !nMetas) { nMetas = true; }         
            //casillaAgua
            if (aux==1 &&nAgua<=0) aux = (int)GetRamdomValue(row, column);
            if (aux == 1 && nAgua > 0) nAgua--;
            //CasillaBarro
            if (aux == 2 && nBarro <= 0) aux = (int)GetRamdomValue(row, column);
            if (aux == 2 && nBarro > 0) nBarro--;
            //CasillaPiedras
            if (aux == 3 && nPiedras <= 0) aux = (int)GetRamdomValue(row, column);
            if (aux == 3 && nPiedras > 0) nPiedras--;

          

            return (uint)aux;

        }
    }
}
          

