using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace P1
{
    public class Pathfinding_A : MonoBehaviour
    {
        Position seeker, target;
        public Tablero grid;
        bool finish = false;
        public void setNoFinish() { finish = false; }
        void Awake()
        {
            //grid = GetComponent<Tablero>();
           
        }

        void Update()
        {
            finish = grid.getTimeMoveForPath();
            
            if (grid.isAllOk())
            {
                seeker = grid.getMyTank().position;
                target = grid.getMyMeta().position;
                FindPath();
            }
        }

        void FindPath()
        {
           
            Block startNode = grid.NodeFromWorldPoint(seeker);
            Block targetNode = grid.NodeFromWorldPoint(target);

            Heap<Block> openSet = new Heap<Block>(grid.MaxSize);
            HashSet<Block> closedSet = new HashSet<Block>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Block currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    if (!finish)//si se cambia la meta se tiene ke setear de nuevo a false
                    {
                        RetracePath(startNode, targetNode);
                        finish = true;
                    }
                    return;
                }

                foreach (Block neighbour in grid.GetNeighbours(currentNode))
                {
                    
                    
                    if (neighbour.tipo==3|| closedSet.Contains(neighbour) || neighbour == null)
                    {
                        continue;
                    }

                    int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour)+ neighbour.movementPenalty;
                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }

        void RetracePath(Block startNode, Block endNode)
        {
            List<Block> path = new List<Block>();
            Block currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                // Debug.Log(currentNode.position.GetRow()+" ,"+currentNode.position.GetColumn());
                currentNode = currentNode.parent;
            }
            path.Reverse();
            foreach (Block b in path)
            {
                //Debug.Log(b.position.GetRow() + "," + b.position.GetColumn());
            }
            grid.path = path;
            
        }

        int GetDistance(Block nodeA, Block nodeB)
        {
            int dstX =(int)Mathf.Abs(nodeA.position.GetRow() - nodeB.position.GetRow());
            int dstY = (int)Mathf.Abs(nodeA.position.GetColumn() - nodeB.position.GetColumn());

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}