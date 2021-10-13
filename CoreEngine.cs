using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace OpenSandGame.Core
{
    public class CoreEngine
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Scale { get; set; }

        int counter = 1;
        float countDuration = 0.0001f; //every  2s.
        float currentTime = 0f;

        MapData mapData;
        Texture2D pixelData;

        public CoreEngine(int width, int height, int scale, GraphicsDeviceManager graphics)
        {
            Width = width;
            Height = height;
            Scale = scale;
            mapData = new MapData(width, height, scale, graphics);
        }

        public CoreEngine(GraphicsDeviceManager graphics)
        {
            Width = 128;
            Height = 128;
            Scale = 4;
            mapData = new MapData(graphics);
        }

        public void UpdateSimulation(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                float PI = 3.14f;
                float i, angle, x1, y1;

                for (i = 0; i < 360; i++)
                {
                    angle = i;
                    x1 = 4f * MathF.Cos(angle * PI / 180f);
                    y1 = 4f * MathF.Sin(angle * PI / 180f);
                    mapData.SetPixel(mouseState.X + (int)x1, mouseState.Y + (int)y1, 6);
                }
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                float PI = 3.14f;
                float i, angle, x1, y1;

                for (i = 0; i < 360; i++)
                {
                    angle = i;
                    x1 = 4f * MathF.Cos(angle * PI / 180f);
                    y1 = 4f * MathF.Sin(angle * PI / 180f);
                    mapData.SetPixel(mouseState.X + (int)x1, mouseState.Y + (int)y1, 1);
                }
            }

            if (mouseState.MiddleButton == ButtonState.Pressed)
            {
                mapData.SetPixel(mouseState.X - (mouseState.X / 4), mouseState.Y - (mouseState.Y / 4), 4);
            }

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //Time passed since last Update() 

            if (currentTime >= countDuration)
            {
                counter++;
                currentTime -= countDuration; // "use up" the time

                mapData.SaveData();
                for (int i = Height - 2; i > 0; i--)
                {
                    for (int j = Width - 2; j > 0; j--)
                    {
                        /*switch(mapData.GetSavedPixel(j,i))
                        {
                            case 1: SimulateSand(); break;
                            case 2: SimulateWater(); break;
                        }*/
                        int pixel = mapData.GetSavedPixel(j, i);
                        //-1 = null
                        //0 = air

                        //Sand
                        if (pixel == 6)
                        {
                            //if below == water tile
                            if (mapData.GetPixel(j, i + 1) == 1)
                            {
                                mapData.SetPixel(j, i, 1);
                                mapData.SetPixel(j, i + 1, 6);
                            }
                            //if below empty
                            else if (mapData.GetSavedPixel(j, i + 1) < 1)
                            {
                                mapData.SetPixel(j, i, 0);
                                mapData.SetPixel(j, i + 1, 6);
                            }
                            //bottom right
                            else if (mapData.GetSavedPixel(j + 1, i + 1) < 1)
                            {
                                mapData.SetPixel(j, i, 0);
                                mapData.SetPixel(j + 1, i + 1, 6);
                            }
                            //bottom left
                            else if (mapData.GetSavedPixel(j - 1, i + 1) < 1)
                            {
                                mapData.SetPixel(j, i, 0);
                                mapData.SetPixel(j - 1, i + 1, 6);
                            }
                        }

                        //Water
                        if (pixel == 1)
                        {
                            //if below empty
                            if (mapData.GetPixel(j, i + 1) < 1)
                            {
                                mapData.SetPixel(j, i, 0);
                                mapData.SetPixel(j, i + 1, 1);
                            }
                            else if (mapData.GetPixel(j, i - 1) == 1) //if water above,, squeeze out
                            {
                                if (mapData.GetPixel(j + 1, i) < 1)
                                {
                                    mapData.SetPixel(j, i, 0);
                                    mapData.SetPixel(j + 1, i, 1);
                                }
                                else if (mapData.GetPixel(j - 1, i) < 1)
                                {
                                    mapData.SetPixel(j, i, 0);
                                    mapData.SetPixel(j - 1, i, 1);
                                }
                            }
                            //right
                            else if (mapData.GetPixel(j + 1, i + 1) < 1)
                            {
                                mapData.SetPixel(j, i, 0);
                                mapData.SetPixel(j + 1, i + 1, 1);
                            }
                            //left
                            else if (mapData.GetPixel(j - 1, i + 1) < 1)
                            {
                                mapData.SetPixel(j, i, 0);
                                mapData.SetPixel(j - 1, i + 1, 1);
                            }
                            //right
                            else if (mapData.GetPixel(j + 1, i) < 1)
                            {
                                mapData.SetPixel(j, i, 0);
                                mapData.SetPixel(j + 1, i, 1);
                            }
                            //left
                            else if (mapData.GetPixel(j - 1, i) < 1)
                            {
                                mapData.SetPixel(j, i, 0);
                                mapData.SetPixel(j - 1, i, 1);
                            }
                        }
                    }
                }
                mapData.UpdateCanvas();
            }
        }

        public void LoadContent()
        {
            mapData.SetPixel(20, 23, 1); //sand
            mapData.SetPixel(20, 22, 1); //sand
            mapData.SetPixel(20, 20, 1); //sand
            mapData.SetPixel(20, 17, 1); //sand
            mapData.SetPixel(20, 15, 1); //sand
            mapData.SetPixel(15, 12, 2); //water

            mapData.UpdateCanvas(); //draw our pixels after we changed data
        }

        public void Render(SpriteBatch spriteBatch)
        {
            mapData.Render(spriteBatch);
        }
    }
}