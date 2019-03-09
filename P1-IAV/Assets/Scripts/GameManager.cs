using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P1
{
    public class GameManager : MonoBehaviour
    {

        private static readonly uint MAX_RANDOM_MOVES = 50;

        // El tablero de bloques (prefab or not)
        public Tablero board;
        public InputField rowsInput;
        public InputField columnsInput;
       
        public uint rows = 3;
        public uint columns = 3;

        // El modelo del puzle deslizante 
        private Puzle puzzle;
        // El resolutor del puzle (que puede admitir varias estrategias)
        private GameManager solver;
        private double time = 0.0d; // in seconds
        private uint steps = 0;
        public void IncreaseDimensions()
        {
            if (board.getTimeMoveForPath())//Si no se ha terminado el recorrido
            {
                rows++;
                columns++;
                puzzle = new Puzle(rows, columns);
                Destroy(board.getMyTank().gameObject);
                board.Initialize(this, puzzle);
            }
        }
        public void DecreaseDimensions()
        {
            if (board.getTimeMoveForPath())
            {
                rows--;
                columns--;
                puzzle = new Puzle(rows, columns);
                Destroy(board.getMyTank().gameObject);
                board.Initialize(this, puzzle);
            }
        }
        public void RandomizeBoard()
        {
            /* if (board.getTimeMoveForPath())
             {
                 board.DestroyBlocks();
             Destroy(board.getMyTank().gameObject);

             puzzle = new Puzle(rows, columns);

             board.Initialize(this, puzzle);
                 }
                 */
            IncreaseDimensions();
            DecreaseDimensions();
        }
        public void defineDimensions()
        {
            uint newRows = Convert.ToUInt32(rowsInput.text);
            uint newColumns = Convert.ToUInt32(columnsInput.text);
            if (newRows != null && newColumns != null)
            {
                puzzle = new Puzle(newRows, newColumns);
                Destroy(board.getMyTank().gameObject);
                board.Initialize(this, puzzle);
            }

        }
        // Generador de números aleatorios del sistema (podría ser el de Unity, también)
        private System.Random random;

        void Start()
        {
            //Podría lanzar excepciones si no ha sido inicializado con gameobjects en todos sus campos clave (salvo que toque cargar la info de fichero o algo así...)

            random = new System.Random();

            // Mientras que no requiera de las dimensiones del puzle ni nada especial, se puede crear en el Start
            solver = new GameManager();

            Initialize(rows, columns);
        }

        private void Initialize(uint rows, uint columns)
        {
            if (board == null) throw new InvalidOperationException("The board reference is null");
            
            this.rows = rows;
            this.columns = columns;
           

            // Se crea el puzle internamente 
            puzzle = new Puzle(rows, columns);
            // Se crea el resolutor (que puede admitir varias estrategias)

            // Inicializar todo el tablero de bloques
            board.Initialize(this, puzzle);

        
        }
    }
}