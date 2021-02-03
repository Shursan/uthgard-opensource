﻿using OpenTK;
using OpenTK.Graphics;
using System.IO;

namespace MNL
{
    public class NiPalette : NiObject
    {
        public byte UnkownByte;
        public Color4[] Palette;

        public NiPalette(NiFile file, BinaryReader reader) : base(file, reader)
        {
            UnkownByte = reader.ReadByte();
            Palette = new Color4[reader.ReadUInt32()];
            for (var i = 0; i < Palette.Length; i++)
            {
                Palette[i] = reader.ReadColor4Byte();
            }
        }
    }
}
