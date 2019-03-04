using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    public class GameManager : MonoBehaviour
    {

        private static readonly uint MAX_RANDOM_MOVES = 50;

        // El tablero de bloques (prefab or not)
        public Tablero board;

        //public GameObject infoPanel;
      //  public Text timeNumber;
       // public Text stepsNumber;
       // public InputField rowsInput;
       // public InputField columnsInput;

        // Dimensiones iniciales del puzle (3x3 en caso de que el diseñador no especifique nada en el Inspector)
        public uint rows = 3;
        public uint columns = 3;

        // El modelo del puzle deslizante 
        private Puzle puzzle;
        // El resolutor del puzle (que puede admitir varias estrategias)
        private GameManager solver;
        private double time = 0.0d; // in seconds
        private uint steps = 0;

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