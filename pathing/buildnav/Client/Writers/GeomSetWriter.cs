﻿using CEM.Utils;
using CEM.World;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CEM
{
    /// <summary>
    /// Recast Geometry Set wWriter
    /// </summary>
    public class GeomSetWriter : StreamWriter
    {
        public enum eAreas
        {
            Ground = 0,
            Water = 1,
            Road = 2,
            Door = 3,
            Grass = 4,
            Jump = 5,
        };
        public enum eFlags
        {
            Walk = 0x01,         // Ability to walk (ground, grass, road)
            Swim = 0x02,         // Ability to swim (water).
            Door = 0x04,         // Ability to move through doors.
            Jump = 0x08,         // Ability to jump.
            Disabled = 0x10,     // Disabled polygon
            All = 0xffff        // All abilities.
        };


        public GeomSetWriter(string file) : base(file, false)
        {
            AutoFlush = true;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Load Mesh
        /// </summary>
        /// <param name="file"></param>
        public void WriteLoadMesh(string file)
        {
            WriteLine("f {0}", file.Replace("\\", "/"));
        }

        /// <summary>
        /// Convex Volume
        /// area: 1=water
        /// </summary>
        /// <param name="file"></param>
        public void WriteConvexVolume(int verts, float hmin, float hmax, eAreas area = eAreas.Water)
        {
            hmax *= NavmeshMgr.CONVERSION_FACTOR;
            hmin *= NavmeshMgr.CONVERSION_FACTOR;
            WriteLine("v {0} {1} {2} {3}", verts, (int)area, hmin.ToString(CultureInfo.InvariantCulture), hmax.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Convex Volume Vertex
        /// </summary>
        /// <param name="vertex"></param>
        public void WriteConvexVolumeVertex(Vector3 vertex)
        {
            vertex *= NavmeshMgr.CONVERSION_FACTOR;
            WriteLine("{0} {1} {2}", vertex.X.ToString(CultureInfo.InvariantCulture),
              vertex.Z.ToString(CultureInfo.InvariantCulture), vertex.Y.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Creates an off-mesh connection
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="bidirectional"></param>
        /// <param name="area"></param>
        /// <param name="flags"></param>
        public void WriteOffMeshConnection(Vector3 start, Vector3 end, bool bidirectional = true, eAreas area = eAreas.Jump, eFlags flags = eFlags.Jump, float radius = 4.1f)
        {
            var rad = radius;
            start *= NavmeshMgr.CONVERSION_FACTOR;
            end *= NavmeshMgr.CONVERSION_FACTOR;
            WriteLine("c {0:F1} {1:F1} {2:F1} {3:F1} {4:F1} {5:F1} {6:F1} {7} {8} {9}", start.X, start.Z, start.Y,
                                                                   end.X, end.Z, end.Y,
                                                                    rad, bidirectional ? 1 : 0, (int)area, (int)flags);
        }
    }
}
