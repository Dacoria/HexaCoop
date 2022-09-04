using UnityEngine;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<KeyValuePair<T, float>> elements = new List<KeyValuePair<T, float>>();

    public int Count => elements.Count;

    public void Enqueue(T item, float priority)
    {
        elements.Add(new KeyValuePair<T, float>(item, priority));
    }

    // Returns the Location that has the lowest priority
    public T Dequeue()
    {
        int bestIndex = 0;

        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Value < elements[bestIndex].Value)
            {
                bestIndex = i;
            }
        }

        T bestItem = elements[bestIndex].Key;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }
}


public class AStarSearch
{
    private Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
    private Dictionary<Vector3Int, float> costSoFar = new Dictionary<Vector3Int, float>();

    private Vector3Int start;
    private Vector3Int goal;

    private float Heuristic(Vector3Int a, Vector3Int b) => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.z - b.z);

    public AStarSearch(Vector3Int start, Vector3Int goal, bool excludeObstaclesFromPath)
    {
        this.start = start;
        this.goal = goal;

        var frontier = new PriorityQueue<Vector3Int>();
        frontier.Enqueue(start, 0f);

        cameFrom.Add(start, start); 
        costSoFar.Add(start, 0f);

        while (frontier.Count > 0f)
        {
            Vector3Int current = frontier.Dequeue();

            if (current.Equals(goal)) break;

            foreach (var neighbor in HexGrid.instance.GetNeighboursFor(current, excludeObstacles: excludeObstaclesFromPath))
            {                
                float newCost = costSoFar[current] + HexGrid.instance.Cost(current, neighbor);

                if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                {

                    // If we're replacing the previous cost, remove it
                    if (costSoFar.ContainsKey(neighbor))
                    {
                        costSoFar.Remove(neighbor);
                        cameFrom.Remove(neighbor);
                    }

                    costSoFar.Add(neighbor, newCost);
                    cameFrom.Add(neighbor, current);
                    float priority = newCost + Heuristic(neighbor, goal);
                    frontier.Enqueue(neighbor, priority);
                }
            }
        }

    }

    public List<Vector3Int> FindPath()
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Vector3Int current = goal;
        // path.Add(current);

        while (!current.Equals(start))
        {
            if (!cameFrom.ContainsKey(current))
            {
                MonoBehaviour.print("cameFrom does not contain current.");
                return new List<Vector3Int>();
            }
            path.Add(current);
            current = cameFrom[current];
        }
        // path.Add(start);
        path.Reverse();
        return path;
    }
}