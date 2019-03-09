using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace P1
{

    public class Block : MonoBehaviour, IHeapItem<Block>//pork no tira?
    {

        public bool walkable;
        public Vector3 worldPosition;
        public int gCost;
        public int hCost;
        public Block parent;
        public int gridX;
        public int gridY;
        public int movementPenalty;
        // El tablero de bloques al que notifica
        private Tablero board;

        // La posición asociada (el tablero de bloques la guarda aquí por eficiencia)
        public Position position;
        public int tipo;
        int heapIndex;
        bool activated = true;


        public Block(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY,int _penalty)
        {
            walkable = _walkable;
            worldPosition = _worldPos;
            gridX = _gridX;
            gridY = _gridY;
            movementPenalty = _penalty;
        }
        public Block() { }


        // Inicializa con el tablero de bloques y el texto (siempre que haya hijo con componente TextMesh), sólo si todavía no tiene puesto ningún tablero de bloques  
        // El tablero de bloques recibido no puede ser nulo, pero el texto sí (representa un bloque no visible)
        public void Initialize(Tablero board)
        {
            // if (board == null) throw new ArgumentNullException(nameof(board));
            //tipo = 0;     
            this.board = board;
        }
        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }
        void update()
        {
            if (activated)
            {
                this.GetComponent<MeshRenderer>();
            }

        }

        void OnMouseDown()
        {
           // Debug.Log(board.tank_.getMode());
            if (board.getMyTank().getMode())//no se porque no va si deberia ir
            {
                switch (tipo)//cambiar
                {
                    case 0:
                        {//libre pasa a ser agua y sin tanke
                            board.GoToMeta(position);
                            //cambia casilla a meta
                        }
                        break;
                    case 1:
                        {//casilla barro pasa a ser pidra
                            board.GoToMeta(position);                          
                        }
                        break;
                    case 2://casilla agua pasa a barro
                        {                        
                            board.GoToMeta(position);
                        }
                        break;
                   // case default: break;
                }
                board.getMyTank().unSelect();
               //finish=false;???
               //Para que el algoritmo busque la nueva ruta ya que acabamos de crear una nueva meta

            } 
            else 
            {
                switch (tipo)
                {
                    case 0:
                        {//libre pasa a ser agua y sin tanke
                            //Debug.Log(tipo);
                            board.cambio(position, 2);
                        }
                        break;
                    case 1:
                        {//casilla barro pasa a ser pidra
                          //  Debug.Log(tipo);
                            board.cambio(position, 3);
                        }
                        break;
                    case 2://casilla agua pasa a barro
                        {
                           // Debug.Log(tipo);
                            board.cambio(position, 1);
                        }
                        break;
                    case 3://casilla piedra pasa a ser libre
                        {
                           // Debug.Log(tipo);
                            board.cambio(position, 0);
                        }
                        break;
                        //para meta y tank Hacer script separados.
                }
                //Debug.Log("pulsacioncrack");

            }
        }

        public int HeapIndex
        {
            get
            {
                return heapIndex;
            }
            set
            {
                heapIndex = value;
            }
        }
        public int CompareTo(Block nodeToCompare)
        {
            int compare = fCost.CompareTo(nodeToCompare.fCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(nodeToCompare.hCost);
            }
            return -compare;
        }
    }
}