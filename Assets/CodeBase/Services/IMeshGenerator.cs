namespace PolygonGame.Services
{
    using UnityEngine;

    public interface IMeshGenerator
    {
        Mesh GetMesh(int sides);

        Vector2[] GetPath(int sides);
    }
}