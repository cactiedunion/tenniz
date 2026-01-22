using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tenniz.Graphics;

public static class TextureAtlas
{
    public static Texture2D Texture;
    public static Dictionary<string, Rectangle> Regions;

    public static void LoadFromFolder(GraphicsDevice device, string path)
    {
        var textures = new List<(string name, Texture2D tex)>();

        foreach (string file in Directory.GetFiles(path, "*.png", SearchOption.AllDirectories))
        {
            using var fs = File.OpenRead(file);
            var tex = Texture2D.FromStream(device, fs);

            string key = Path.GetFileNameWithoutExtension(file);
            textures.Add((key, tex));
        }

        textures = textures.OrderByDescending(t => t.tex.Height).ToList();

        int atlasWidth = 2048;
        int atlasHeight = 2048;

        Color[] atlasPixels = new Color[atlasWidth * atlasHeight];

        var regions = new Dictionary<string, Rectangle>();

        int x = 0;
        int y = 0;
        int rowHeight = 0;

        foreach (var (name, tex) in textures)
        {
            if (x + tex.Width > atlasWidth)
            {
                x = 0;
                y += rowHeight;
                rowHeight = 0;
            }

            rowHeight = System.Math.Max(rowHeight, tex.Height);

            Color[] pixels = new Color[tex.Width * tex.Height];
            tex.GetData(pixels);

            for (int py = 0; py < tex.Height; py++)
            {
                for (int px = 0; px < tex.Width; px++)
                {
                    int atlasIndex = (x + px) + (y + py) * atlasWidth;
                    atlasPixels[atlasIndex] = pixels[px + py * tex.Width];
                }
            }

            regions[name] = new Rectangle(x, y, tex.Width, tex.Height);

            x += tex.Width;
        }

        Texture2D atlas = new Texture2D(device, atlasWidth, atlasHeight);
        atlas.SetData(atlasPixels);

        Texture = atlas;
        Regions = regions;
    }
}