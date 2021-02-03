﻿using OpenTK;
using System.IO;

namespace MNL
{
    public class NiVectorExtraData : NiExtraData
    {
        public Vector3 Data;
        public float UnkownFloat;

        public NiVectorExtraData(NiFile file, BinaryReader reader)
            : base(file, reader)
        {
            Data = reader.ReadVector3();
            UnkownFloat = reader.ReadSingle();
        }
    }
}