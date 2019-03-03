using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace P1
{
    public class Block : MonoBehaviour
    {


        // El tablero de bloques al que notifica
        private Tablero board;

        // La posición asociada (el tablero de bloques la guarda aquí por eficiencia)
        public Position position;
        public int tipo;
        



        // Inicializa con el tablero de bloques y el texto (siempre que haya hijo con componente TextMesh), sólo si todavía no tiene puesto ningún tablero de bloques  
        // El tablero de bloques recibido no puede ser nulo, pero el texto sí (representa un bloque no visible)
        public void Initialize(Tablero board)
        {
            // if (board == null) throw new ArgumentNullException(nameof(board));
            tipo = 0;
          
            this.board = board;
          

           

        }
       
        void OnMouseDown() {//dice k board es null?



            switch (tipo)
            {
                case 0: {//libre pasa a ser agua y sin tanke
                        Debug.Log(tipo);
                        board.cambio(position, 2);
                    }
                    break;
                case 1:
                    {//casilla barro pasa a ser pidra
                        Debug.Log(tipo);
                        board.cambio(position, 3);
                    }
                    break;
                case 2://casilla agua pasa a barro
                    {
                        Debug.Log(tipo);
                        board.cambio(position, 1);
                    }
                    break;
                case 3://casilla piedra pasa a ser libre
                    {
                        Debug.Log(tipo);
                        board.cambio(position, 0);
                    }
                    break;
                    //para meta y tank Hacer script separados.
            }
            //Debug.Log("pulsacioncrack");

        }



    }
}