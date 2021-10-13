using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace OpenSandGame.Core
{
    public class MapData
    {
        private Texture2D _canvasSimulationTexture;
        private Color[] _canvasPixels;
        private Color[] _canvasPixelSavedData;
        private Color[] _canvasColorPalette;

        //old map data that we will run our simulation against
        public Color[] CanvasPixelSavedData { get { return _canvasPixelSavedData; } set { _canvasPixelSavedData = value; } }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Scale { get; set; }

        public MapData(int width, int height, int scale, GraphicsDeviceManager graphics)
        {
            Width = width;
            Height = height;
            Scale = scale;
            // Create the canvas pixel array.
            _canvasPixels = new Color[Width * Height];
            _canvasPixelSavedData = new Color[Width * Height];

            // Create the canvas texture.
            _canvasSimulationTexture = new Texture2D(graphics.GraphicsDevice, Width, Height);

            // Create the color palette.
            _canvasColorPalette = CreatePalette();
        }

        public MapData(GraphicsDeviceManager graphics)
        {
            Width = 128;
            Height = 128;
            Scale = 4;
            // Create the canvas pixel array.
            _canvasPixels = new Color[Width * Height];
            _canvasPixelSavedData = new Color[Width * Height];

            // Create the canvas texture.
            _canvasSimulationTexture = new Texture2D(graphics.GraphicsDevice, Width, Height);

            // Create the color palette.
            _canvasColorPalette = CreatePalette();
        }

        public void UpdateCanvas()
        {
            _canvasSimulationTexture.SetData(_canvasPixels, 0, _canvasPixels.Length);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_canvasSimulationTexture, new Rectangle(0, 0, Width * Scale, Height * Scale), Color.White);
        }

        public void SaveData()
        {
            Array.Copy(_canvasPixels,CanvasPixelSavedData, _canvasPixels.Length);
        }

        public void SetPixel(int x, int y, int colorIndex)
        {
            var pos = x + (y * Width);

            if (pos >= 0 && pos < _canvasPixels.Length)
            {
                _canvasPixels[pos] = _canvasColorPalette[colorIndex];
                //_shouldUpdateCanvas = true;
            }
        }

        public int GetPixel(int x, int y)
        {
            var pos = x + (y * Width);

            if (pos >= 0 && pos < _canvasPixels.Length)
            {
                return Array.IndexOf(_canvasColorPalette, _canvasPixels[pos]);
            }

            return -1;
        }

        public int GetSavedPixel(int x, int y)
        {
            var pos = x + (y * Width);

            if (pos >= 0 && pos < _canvasPixelSavedData.Length)
            {
                return Array.IndexOf(_canvasColorPalette, _canvasPixelSavedData[pos]);
            }

            return -1;
        }

        public void Cls()
        {
            _canvasPixels = new Color[Width * Height];
        }

        protected Color[] CreatePalette()
        {
            return new Color[]
            {
                new Color(0, 0, 0),
                new Color(29, 43, 83),
                new Color(126, 37, 83),
                new Color(0, 135, 81),
                new Color(171, 82, 54),
                new Color(95, 87, 79),
                new Color(194, 195, 199),
                new Color(255, 241, 232),
                new Color(255, 0, 77),
                new Color(255, 163, 0),
                new Color(255, 240, 36),
                new Color(0, 231, 86),
                new Color(41, 173, 255),
                new Color(131, 118, 156),
                new Color(255, 119, 168),
                new Color(255, 204, 170)
            };
        }
    }
}