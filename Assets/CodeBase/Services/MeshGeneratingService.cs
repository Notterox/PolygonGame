namespace PolygonGame.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using PolygonGame.Utils;
    using UnityEngine;

    public class MeshGeneratingService : IMeshGenerator
    {
        private const float FullCircle = 360f;
        private const float DefaultRotation = 90f;
        private const float StickThickness = 0.2f;
        private readonly Dictionary<int, CacheItem> meshCache;

        public MeshGeneratingService()
        {
            meshCache = new Dictionary<int, CacheItem>();
        }

        public Mesh GetMesh(int sides)
        {
            if (meshCache.TryGetValue(sides, out var cache))
            {
                return cache.Mesh;
            }

            CacheItem result = GenerateMesh(sides);
            meshCache.Add(sides, result);

            return result.Mesh;
        }

        public Vector2[] GetPath(int sides)
        {
            if (meshCache.TryGetValue(sides, out CacheItem cache))
            {
                return cache.Vertices.Select(v => (Vector2)v).ToArray();
            }

            var result = GenerateMesh(sides);
            meshCache.Add(sides, result);

            return result.Vertices.Select(v => (Vector2)v).ToArray();
        }

        private CacheItem GenerateMesh(int sides)
        {
            if (sides == 2)
            {
                return GenerateStick();
            }

            Mesh mesh = new Mesh();

            float sectorAngle = FullCircle / sides;
            float offset = (sectorAngle / 2f) + DefaultRotation;
            Vector3[] vertices = new Vector3[sides]
                .Select((_, index) => (Vector3)PolarCoordinates.ToCartesian(1, (index * sectorAngle) + offset))
                .ToArray();
            mesh.vertices = vertices;

            int[] tris = new int[(sides - 2) * 3];

            for (int i = 0; i < sides - 2; i++)
            {
                tris[(i * 3) + 0] = i + 2;
                tris[(i * 3) + 1] = i + 1;
                tris[(i * 3) + 2] = 0;
            }

            mesh.triangles = tris;

            return new CacheItem { Mesh = mesh, Vertices = vertices };
        }

        private CacheItem GenerateStick()
        {
            CacheItem result = GenerateMesh(4);
            Vector3[] vertices = result.Vertices;
            Mesh mesh = result.Mesh;

            for (int i = 0; i < result.Vertices.Length; i++)
            {
                var v = vertices[i];
                vertices[i] = new Vector3(v.x, v.y * StickThickness, v.z);
            }

            mesh.vertices = vertices;
            mesh.RecalculateBounds();

            return new CacheItem { Mesh = mesh, Vertices = vertices };
        }

        private struct CacheItem
        {
            public Mesh Mesh;
            public Vector3[] Vertices;
        }
    }
}