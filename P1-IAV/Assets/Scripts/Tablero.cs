using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace P1
{
    public class Tablero : MonoBehaviour
    {
        // Constantes
        public static readonly float USER_DELAY = 0.0f;
        public static readonly float AI_DELAY = 0.2f;

        private static readonly float POSITION_FACTOR_R = 1.1f;
        private static readonly float POSITION_FACTOR_C = 1.1f;
        private static readonly float SCALE_FACTOR_R = 1.1f;
        private static readonly float SCALE_FACTOR_C = 1.1f;

        // Aquí es donde necesito el tipo de prefab 
        public Block blockPrefab;//CasillaNormal
        public Block casillaMeta;
        public Block casillaBarro;
        public Block casillaAgua;
        public Block casillaPiedra;
        public Tank tank_;
        private Block aux;

        private GameManager manager;


        //Matriz de bloques
        private Block[,] blocks;

        // Lista de bloques para ir moviendo (ojo, van de dos en dos... porque en C# 6 no hay tuplas todavía)
        private Queue<Block> blocksInMotion = new Queue<Block>();

      
        
        public void Initialize(GameManager manager, Puzle puzzle)
        {
         //   if (manager == null) throw new ArgumentNullException(nameof(manager));
           // if (puzzle == null) throw new ArgumentNullException(nameof(puzzle));

            this.manager = manager;

            if (blocks == null)
            {
                blocks = new Block[puzzle.rows, puzzle.columns];

                // Ajustar las dimensiones del tablero de bloques
                // La cámara podría también ajustarse si fuese hija y dependiera de alguna manera del tablero de bloques  
                transform.localScale = new Vector3(SCALE_FACTOR_C * blocks.GetLength(1), transform.localScale.y, SCALE_FACTOR_R * blocks.GetLength(0));
                /* Valores INICIALES de X e Y 
                  float x = (columns / 2.0f) * 1.1f - 0.55f; //Tanto el 1.1f como el 0.5 debería ser preguntarle al Box por su tamaño horizontal (y o bien sumarle ese margen de 0.1 o coger la mitad) 
                  float y = (rows / 2.0f) * 1.1f - 0.55f;  //0.5 debería ser preguntarle al Box por su tamaño vertical (y coger la mitad) 
                */
                // blocks[0, 0].type_="2";

            }
            else if (blocks.GetLength(0) != puzzle.rows || blocks.GetLength(1) != puzzle.columns)
            {
                DestroyBlocks();

                blocks = new Block[puzzle.rows, puzzle.columns];
               
                transform.localScale = new Vector3(SCALE_FACTOR_C * blocks.GetLength(1), transform.localScale.y, SCALE_FACTOR_R * blocks.GetLength(0));
           
            }
            GenerateBlocks(puzzle);

            InstanciateTank((int)puzzle.rows,(int)puzzle.columns,puzzle);

        }
               
        private void InstanciateTank(int rows,int colum, Puzle puzzle)
        {
            uint aux = 0;
            aux = (uint)UnityEngine.Random.Range(0,rows );
            uint aux1 = 0;
            aux1 =(uint) UnityEngine.Random.Range(0, colum);
            Position pos = new Position(aux, aux1);//mirar si no va al reves

            uint val = puzzle.GetValue(pos);
            if ((int)val != 3 && (int)val != 4)
            {

               Tank t = Instantiate(tank_,
                        new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + aux1 * POSITION_FACTOR_C,
                                     0,
                                     (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - aux * POSITION_FACTOR_R),
                        Quaternion.identity);
                //InstanciateTank(Tank, tranform.position, transform.rotation);

               //tank_.position=pos;
            }
            else InstanciateTank(rows, colum, puzzle);
        }
       


        private void GenerateBlocks(Puzle puzzle)
        {
           // if (puzzle == null) throw new ArgumentNullException(nameof(puzzle));

            var rows = blocks.GetLength(0);
            var columns = blocks.GetLength(1);

            for (var r = 0u; r < rows; r++)
            {
                for (var c = 0u; c < columns; c++)
                {

                    Block block = blocks[r, c];
                    Position position = new Position(r, c);
                    uint value = puzzle.GetValue(position);

                    if (block == null)
                    {
                        switch (value)
                        {
                            case 0: aux = blockPrefab; blockPrefab.tipo = 0;break;
                            case 1: aux = casillaAgua; casillaAgua.tipo = 1; break;
                            case 2:aux = casillaBarro; casillaBarro.tipo = 2; break; 
                            case 3: aux = casillaPiedra; casillaPiedra.tipo = 3; break; 
                            case 4:aux = casillaMeta; casillaMeta.tipo = 4; break; 
                            default: aux = blockPrefab; break;//como casilla por defecto ponemosla normal
                        }
                        block = Instantiate(aux,
                         new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + c * POSITION_FACTOR_C,
                                      0,
                                      (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - r * POSITION_FACTOR_R),
                         Quaternion.identity);

                    }



                    blocks[r, c] = block;

                    // Estuviera o no ya creado el bloque, se inicializa y reposiciona
                   
                    block.position = position;
                    block.Initialize(this);
                    // El texto que se pone en el bloque es el valor +1, salvo si es el último valor, que no se mandará texto para que sea un bloque no visible
                    /*if (value == 0)
                        block.Initialize(this, null);
                    else
                        block.Initialize(this, value.ToString());
                    Debug.Log(ToString() + "generated " + block.ToString() + ".");
                    */
                }
            }

        }


        // Destruye todos los bloques de la matriz
        // No se utiliza GetLowerBound ni GetUpperBound porque sabemos que son matrices que siempre empiezan en cero y acaban en positivo
        private void DestroyBlocks()
        {
          //  if (blocks == null) throw new InvalidOperationException("This object has not been initialized");

            var rows = blocks.GetLength(0);
            var columns = blocks.GetLength(1);

            for (var r = 0u; r < rows; r++)
            {
                for (var c = 0u; c < columns; c++)
                {
                    if (blocks[r, c] != null)
                        Destroy(blocks[r, c].gameObject);
                }
            }
        }


        void update()
        {



        }

        public void cambio(Position pos,int tipo)
        {
            Destroy(blocks[pos.GetRow(), pos.GetColumn()].gameObject);
            //Block b =aux;
            
            switch (tipo)
            {
                case 0: aux = blockPrefab; blockPrefab.tipo = 0; break;
                case 1: aux = casillaAgua; casillaAgua.tipo = 1; break;
                case 2: aux = casillaBarro; casillaBarro.tipo = 2; break;
                case 3: aux = casillaPiedra; casillaPiedra.tipo = 3; break;
             //   default: aux = blockPrefab; break;//como casilla por defecto ponemosla normal
            }
            Block b = Instantiate(aux,
                       new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + pos.GetColumn() * POSITION_FACTOR_C,
                                    0,
                                    (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - pos.GetRow() * POSITION_FACTOR_R),
                       Quaternion.identity);

            blocks[pos.GetRow(), pos.GetColumn()] = b;
            b.position = pos;
            b.Initialize(this);
            // b.position = position;
            //cambiar pos..
        }
    }
}