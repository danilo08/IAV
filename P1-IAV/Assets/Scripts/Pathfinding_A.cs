using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace P1
{
    public class Pathfinding_A : MonoBehaviour
    {
        public Transform seeker, target;
        Tablero grid;

        void Awake()
        {
            grid = GetComponent<Tablero>();
           
        }

        void Update()
        {
            FindPath(seeker.position, target.position);
        }

        void FindPath(Vector3 startPos, Vector3 targetPos)
        {
            Block startNode = grid.NodeFromWorldPoint(startPos);
            Block targetNode = grid.NodeFromWorldPoint(targetPos);

            List<Block> openSet = new List<Block>();
            HashSet<Block> closedSet = new HashSet<Block>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
               Block node = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                    {
                        if (openSet[i].hCost < node.hCost)
                            node = openSet[i];
                    }
                }

                openSet.Remove(node);
                closedSet.Add(node);

                if (node == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return;
                }

                foreach (Block neighbour in grid.GetNeighbours(node))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = node;

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
                currentNode = currentNode.parent;
            }
            path.Reverse();

            grid.path = path;

        }

        int GetDistance(Block nodeA, Block nodeB)
        {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}