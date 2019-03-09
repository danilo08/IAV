using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace P1
{
    public class Tablero : MonoBehaviour
    {
        // Constantes
        public static readonly float USER_DELAY = 0.0f;
        public static readonly float AI_DELAY = 0.8f;

        private static readonly float POSITION_FACTOR_R = 1.1f;
        private static readonly float POSITION_FACTOR_C = 1.1f;
        private static readonly float SCALE_FACTOR_R = 1.1f;
        private static readonly float SCALE_FACTOR_C = 1.1f;

        // Aquí es donde necesito el tipo de prefab 
        public Block blockPrefab;//Prefab
        public Block casillaMeta;
        public Block casillaBarro;
        public Block casillaAgua;
        public Block casillaPiedra;
        public Tank tank_;
<<<<<<< HEAD
        public GameObject rastro;


        //Obtener el camino a seguir, tiempo etc...
        private float targetTime = 0.2f;
        private bool timeToMove = false;
        public bool getTimeMoveForPath() { return !timeToMove; }
        private bool rockOnMyWay = false;
        private int chosenPath = 0;
        public void nextPath()
        {
            switch (chosenPath)
            {
                case 0:
                    chosenPath = 1;
                    break;
                case 1:
                    chosenPath = 0;
                    break;
                default: break;
            }
        }
        //Cosas Relacionadas con PathfidingA*
        public Vector2 gridWorldSize;
        public float nodeRadius;
        float nodeDiameter;
        int gridSizeX, gridSizeY;


=======

        //Obtener el camino a seguir, tiempo etc...
        private float targetTime = 1.0f;
        private bool timeToMove = false;
        public bool getTimeMoveForPath() { return !timeToMove; }
        private bool rockOnMyWay = false;

>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
        private Block aux;
        private GameManager manager;

        private Block meta = null;
        private Tank myTank;
        private bool IsEverythingOk = false;
        public bool isAllOk() { return IsEverythingOk; }
        public Tank getMyTank() { return myTank; }
        public Block getMyMeta() { return meta; }
        //Matriz de bloques //de nodos
<<<<<<< HEAD
        private Block[,] blocks;
=======
        private Block[,] blocks;
>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
        public List<Block> path;
        // Lista de bloques para ir moviendo (ojo, van de dos en dos... porque en C# 6 no hay tuplas todavía)
        private Queue<Block> blocksInMotion = new Queue<Block>();


        void Awake()
        {
            nodeDiameter = nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);//igual
<<<<<<< HEAD
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        }
        void Update()
        {
            if (timeToMove)
            {
                targetTime -= Time.deltaTime;
                if (targetTime < 0.0f)
=======
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        }
        void Update()
        {
            if (timeToMove)
            {
                targetTime -= Time.deltaTime;
                if (targetTime < 0.0f)
>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
                    timerEnded();
            }
        }
        public List<Block> GetNeighbours(Block node)
        {
            List<Block> neighbours = new List<Block>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    if (x != 0 && y != 0 && chosenPath == 1)
                        continue;

                    int checkX = (int)node.position.GetRow() + x;
                    int checkY = (int)node.position.GetColumn() + y;

                    if (checkX >= 0 && checkX < blocks.GetLength(0) && checkY >= 0 && checkY < blocks.GetLength(1))
                    {
<<<<<<< HEAD
                        neighbours.Add(blocks[checkX, checkY]);

=======
                        neighbours.Add(blocks[checkX, checkY]);

>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
                    }
                }
            }

            return neighbours;
        }

        public List<Block> GetBlocks(Block node)
        {
            List<Block> neighbours = new List<Block>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    //esto no es getRow y getColum?
                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        //esto?Tablero????
                        neighbours.Add(blocks[checkX, checkY]);
                    }
                }
            }
            return neighbours;
        }

        public Block NodeFromWorldPoint(Position worldPosition)
        {
            /*float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
            float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);*/
            return blocks[worldPosition.GetRow(), worldPosition.GetColumn()];
        }

<<<<<<< HEAD
        public List<GameObject> Camino;
        void drawRastro()
        {
           
            foreach (Block b in path)//No entra
            {
                Debug.Log("dibujah");
                Position pos = b.position;
                uint aux = pos.GetRow();
                uint aux1 = pos.GetColumn();
                //Debug.Log(aux + " " + aux1);

                GameObject t = Instantiate(rastro,
                     new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + aux1 * POSITION_FACTOR_C,
                                  0.5f,
                                  (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - aux * POSITION_FACTOR_R),
                     Quaternion.identity);

                // t.position = pos;

                Camino.Add(t);
            }
        }

        void DestroyRastro()
        {
            foreach(GameObject game in Camino)
            {
                Destroy(game);
            }
           
        }
        void OnDrawGizmos()
        {
            // Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
=======

>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113

        void OnDrawGizmos()
        {
            // Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (blocks != null)
            {
                foreach (Block n in blocks)
                {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    if (path != null)
<<<<<<< HEAD
                        if (path.Contains(n)) {
                            Gizmos.color = Color.black;
                            
                        }
                        
                    // Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
=======
                        if (path.Contains(n))
                            Gizmos.color = Color.black;
                    // Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
                }
            }
        }

        //public Position nextBlock()



        ///////////////////////////////////////////
        public void Initialize(GameManager manager, Puzle puzzle)
<<<<<<< HEAD
        {
            //   if (manager == null) throw new ArgumentNullException(nameof(manager));
            // if (puzzle == null) throw new ArgumentNullException(nameof(puzzle));

=======
        {
            //   if (manager == null) throw new ArgumentNullException(nameof(manager));
            // if (puzzle == null) throw new ArgumentNullException(nameof(puzzle));

>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
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
                //aqui entra  +1 -1
                DestroyBlocks();

<<<<<<< HEAD
                blocks = new Block[puzzle.rows, puzzle.columns];

                transform.localScale = new Vector3(SCALE_FACTOR_C * blocks.GetLength(1), transform.localScale.y, SCALE_FACTOR_R * blocks.GetLength(0));

=======
                blocks = new Block[puzzle.rows, puzzle.columns];

                transform.localScale = new Vector3(SCALE_FACTOR_C * blocks.GetLength(1), transform.localScale.y, SCALE_FACTOR_R * blocks.GetLength(0));

>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
            }
            GenerateBlocks(puzzle);

            InstanciateTank((int)puzzle.rows, (int)puzzle.columns, puzzle);

<<<<<<< HEAD
        }

=======
        }

>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
        private void InstanciateTank(int rows, int colum, Puzle puzzle)
        {
            uint aux = 0;
            aux = (uint)UnityEngine.Random.Range(0, rows);
            uint aux1 = 0;
            aux1 = (uint)UnityEngine.Random.Range(0, colum);
            Position pos = new Position(aux, aux1);//mirar si no va al reves

            uint val = puzzle.GetValue(pos);
            if ((int)val != 3 && (int)val != 4)
<<<<<<< HEAD
            {
                Tank t = Instantiate(tank_,
                         new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + aux1 * POSITION_FACTOR_C,
                                      0,
                                      (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - aux * POSITION_FACTOR_R),
=======
            {
                Tank t = Instantiate(tank_,
                         new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + aux1 * POSITION_FACTOR_C,
                                      0,
                                      (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - aux * POSITION_FACTOR_R),
>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
                         Quaternion.identity);
                //InstanciateTank(Tank, tranform.position, transform.rotation);
                myTank = t;
                myTank.position = pos;
                myTank.Initialize(this);
                // tank_.seleccionado = true;
<<<<<<< HEAD
                IsEverythingOk = true; //Comienza el A*Update
                                       //tank_.position=pos;
            }
            else InstanciateTank(rows, colum, puzzle);
        }

        private void GenerateBlocks(Puzle puzzle)
        {
            // if (puzzle == null) throw new ArgumentNullException(nameof(puzzle));

=======
                IsEverythingOk = true; //Comienza el A*Update
                                       //tank_.position=pos;
            }
            else InstanciateTank(rows, colum, puzzle);
        }

        private void GenerateBlocks(Puzle puzzle)
        {
            // if (puzzle == null) throw new ArgumentNullException(nameof(puzzle));

>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
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
<<<<<<< HEAD
                            case 0: aux = blockPrefab; blockPrefab.tipo = 0; blockPrefab.movementPenalty = 1; break;
                            case 1: aux = casillaAgua; casillaAgua.tipo = 1; casillaAgua.movementPenalty = 20; break;
                            case 2: aux = casillaBarro; casillaBarro.tipo = 2; casillaBarro.movementPenalty = 40; break;
                            case 3: aux = casillaPiedra; casillaPiedra.tipo = 3; break;
                            case 4: aux = casillaMeta; casillaMeta.tipo = 4; break;
                                // default: aux = blockPrefab; break;//como casilla por defecto ponemosla normal
=======
                            case 0: aux = blockPrefab; blockPrefab.tipo = 0; break;
                            case 1: aux = casillaAgua; casillaAgua.tipo = 1; break;
                            case 2: aux = casillaBarro; casillaBarro.tipo = 2; break;
                            case 3: aux = casillaPiedra; casillaPiedra.tipo = 3; break;
                            case 4: aux = casillaMeta; casillaMeta.tipo = 4; break;
                                // default: aux = blockPrefab; break;//como casilla por defecto ponemosla normal
>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
                        }
                        block = Instantiate(aux,
                         new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + c * POSITION_FACTOR_C,
                                      0,
                                      (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - r * POSITION_FACTOR_R),
                         Quaternion.identity);

                        if (value == 4)
<<<<<<< HEAD
                        {
                            meta = block; meta.position = position; meta.Initialize(this);
=======
                        {
                            meta = block; meta.position = position; meta.Initialize(this);
>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
                        }
                        else if (value == 3) block.walkable = true;
                    }



<<<<<<< HEAD
                    blocks[r, c] = block;

                    // Estuviera o no ya creado el bloque, se inicializa y reposiciona

=======
                    blocks[r, c] = block;

                    // Estuviera o no ya creado el bloque, se inicializa y reposiciona

>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
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
<<<<<<< HEAD
        public void DestroyBlocks()
        {
            //  if (blocks == null) throw new InvalidOperationException("This object has not been initialized");

=======
        private void DestroyBlocks()
        {
            //  if (blocks == null) throw new InvalidOperationException("This object has not been initialized");

>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
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
<<<<<<< HEAD
            //añado destruir camino y destroy tank

            DestroyRastro();
            Destroy(myTank.gameObject);
        }



        public void cambio(Position pos, int tipo)
        {
            Destroy(blocks[pos.GetRow(), pos.GetColumn()].gameObject);
            //Block b =aux;

=======
        }



        public void cambio(Position pos, int tipo)
        {
            Destroy(blocks[pos.GetRow(), pos.GetColumn()].gameObject);
            //Block b =aux;

>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
            switch (tipo)
            {
                case 0: aux = blockPrefab; blockPrefab.tipo = 0; break;
                case 1: aux = casillaAgua; casillaAgua.tipo = 1; break;
                case 2: aux = casillaBarro; casillaBarro.tipo = 2; break;
<<<<<<< HEAD
                case 3: aux = casillaPiedra; casillaPiedra.tipo = 3; break;
                    //   default: aux = blockPrefab; break;//como casilla por defecto ponemosla normal
=======
                case 3: aux = casillaPiedra; casillaPiedra.tipo = 3; break;
                    //   default: aux = blockPrefab; break;//como casilla por defecto ponemosla normal
>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
            }
            Block b = Instantiate(aux,
                       new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + pos.GetColumn() * POSITION_FACTOR_C,
                                    0,
                                    (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - pos.GetRow() * POSITION_FACTOR_R),
                       Quaternion.identity);

            b.position = pos;
            blocks[pos.GetRow(), pos.GetColumn()] = b;
            b.Initialize(this);
            // b.position = position;
            //cambiar pos..
        }

        public void GoToMeta(Position pos)
<<<<<<< HEAD
        {
            //destruimos la casilla se leccionada para poner la meta

            //sustituimos la anterior meta por una casilla Normal
            //Guardamos positcion e la casila meta
            Debug.Log(!timeToMove);
            Debug.Log(rockOnMyWay + "rock");
            if (!timeToMove || rockOnMyWay)
            {
                Position position = new Position(meta.position.GetRow(), meta.position.GetColumn());//Me guardo la pos
                                                                                                    //destruimos la anterior meta
                Destroy(blocks[meta.position.GetRow(), meta.position.GetColumn()].gameObject);//


                Block b1 = Instantiate(blockPrefab,
                         new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + position.GetColumn() * POSITION_FACTOR_C,
                                      0,
                                      (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - position.GetRow() * POSITION_FACTOR_R),
                         Quaternion.identity);


                b1.position = position;
                blocks[position.GetRow(), position.GetColumn()] = b1;
                b1.Initialize(this);
                //////////////////////////////////////////////////////


                Destroy(blocks[pos.GetRow(), pos.GetColumn()].gameObject);
                //creamos la casillaMeta en la posicion seleccionada
                casillaMeta.tipo = 4;//no se si es necesario
                Block b = Instantiate(casillaMeta,
                          new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + pos.GetColumn() * POSITION_FACTOR_C,
                                       0,
                                       (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - pos.GetRow() * POSITION_FACTOR_R),
                          Quaternion.identity);


                meta = b; meta.position = pos; meta.Initialize(this);

                b.position = pos;
                blocks[pos.GetRow(), pos.GetColumn()] = b;
                b.Initialize(this);
                timeToMove = true;
                
=======
        {
            //destruimos la casilla se leccionada para poner la meta

            //sustituimos la anterior meta por una casilla Normal
            //Guardamos positcion e la casila meta
            Debug.Log(!timeToMove);
            Debug.Log(rockOnMyWay + "rock");
            if (!timeToMove || rockOnMyWay)
            {
                Position position = new Position(meta.position.GetRow(), meta.position.GetColumn());//Me guardo la pos
                                                                                                    //destruimos la anterior meta
                Destroy(blocks[meta.position.GetRow(), meta.position.GetColumn()].gameObject);//


                Block b1 = Instantiate(blockPrefab,
                         new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + position.GetColumn() * POSITION_FACTOR_C,
                                      0,
                                      (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - position.GetRow() * POSITION_FACTOR_R),
                         Quaternion.identity);


                b1.position = position;
                blocks[position.GetRow(), position.GetColumn()] = b1;
                b1.Initialize(this);
                //////////////////////////////////////////////////////


                Destroy(blocks[pos.GetRow(), pos.GetColumn()].gameObject);
                //creamos la casillaMeta en la posicion seleccionada
                casillaMeta.tipo = 4;//no se si es necesario
                Block b = Instantiate(casillaMeta,
                          new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + pos.GetColumn() * POSITION_FACTOR_C,
                                       0,
                                       (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - pos.GetRow() * POSITION_FACTOR_R),
                          Quaternion.identity);


                meta = b; meta.position = pos; meta.Initialize(this);

                b.position = pos;
                blocks[pos.GetRow(), pos.GetColumn()] = b;
                b.Initialize(this);
                timeToMove = true;
>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
            }

        }
       

        public int MaxSize
        {
            get
            {
                //return gridSizeX * gridSizeY;
                return blocks.GetLength(0) * blocks.GetLength(1);
            }
        }
<<<<<<< HEAD

        private void MoveTankToGoal()
        {
            if (path.Count != 0)
            {
                if (!path[0].walkable)
                {
                    rockOnMyWay = false;
                    uint aux = path[0].position.GetRow();
                    uint aux1 = path[0].position.GetColumn();
                    DestroyRastro();
                    drawRastro();
                    path.RemoveAt(0);
                    Position pos = new Position(aux, aux1);
                    myTank.transform.position = new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + aux1 * POSITION_FACTOR_C,
                                             0,
                                             (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - aux * POSITION_FACTOR_R);
                   /* Destroy(myTank.gameObject);
                    Tank t = Instantiate(tank_,
                                new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + aux1 * POSITION_FACTOR_C,
                                             0,
                                             (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - aux * POSITION_FACTOR_R),
                                Quaternion.identity);
                    //InstanciateTank(Tank, tranform.position, transform.rotation);
                    myTank = t;*/
                    myTank.position = pos;
                    //myTank.Initialize(this);

                    //dibujar ruta
                    //Dibujar ruta();
                }
                else {rockOnMyWay = true; }

               
            }
            else { rockOnMyWay = true; }

            
        }

        private void timerEnded()
        {
            //do my stuff
            MoveTankToGoal();
            targetTime = 0.8f;
            if (stopPath())
            {
                timeToMove = false;
                Debug.Log("EO");
            }
        }

        private bool stopPath()
        {
            if (myTank.position.GetColumn() == meta.position.GetColumn() && myTank.position.GetRow() == meta.position.GetRow())
            {
                return true;

            }
            return false;
        }

        public void EraseEvertything()
        {
          foreach (Block b in blocks){
                Destroy(b.gameObject);
            }
            Destroy(meta.gameObject);
            Destroy(myTank.gameObject);
=======
        private void MoveTankToGoal()
        {
            if (path.Count != 0)
            {
                if (!path[0].walkable)
                {
                    rockOnMyWay = false;
                    Debug.Log(path[0].tipo);
                    uint aux = path[0].position.GetRow();
                    uint aux1 = path[0].position.GetColumn();
                    path.RemoveAt(0);
                    Position pos = new Position(aux, aux1);
                    myTank.transform.position = new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + aux1 * POSITION_FACTOR_C,
                                             0,
                                             (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - aux * POSITION_FACTOR_R);
                   /* Destroy(myTank.gameObject);
                    Tank t = Instantiate(tank_,
                                new Vector3(-((blocks.GetLength(1) / 2.0f) * POSITION_FACTOR_C - (POSITION_FACTOR_C / 2.0f)) + aux1 * POSITION_FACTOR_C,
                                             0,
                                             (blocks.GetLength(0) / 2.0f) * POSITION_FACTOR_R - (POSITION_FACTOR_R / 2.0f) - aux * POSITION_FACTOR_R),
                                Quaternion.identity);
                    //InstanciateTank(Tank, tranform.position, transform.rotation);
                    myTank = t;*/
                    myTank.position = pos;
                    //myTank.Initialize(this);
                }
                else {rockOnMyWay = true; }
            }
            else { rockOnMyWay = true; }

        }
        private void timerEnded()
        {
            //do my stuff
            MoveTankToGoal();
            targetTime = 2.0f;
            if (stopPath())
            {
                timeToMove = false;
                Debug.Log("EO");
            }
        }

        private bool stopPath()
        {
            if (myTank.position.GetColumn() == meta.position.GetColumn() && myTank.position.GetRow() == meta.position.GetRow())
            {
                return true;

            }
            return false;
>>>>>>> e24ac3d0fabe93e7c38f428f4ccfb57a1e12d113
        }
    }
}