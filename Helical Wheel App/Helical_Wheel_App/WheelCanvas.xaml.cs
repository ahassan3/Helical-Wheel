using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Helical_Wheel_App
{
    public partial class WheelCanvas : ContentView
    {
        private static int RADIUS = 90;
        private static int AMINORADIUS = 9;
        private string AminoSequence = "";
        public List<KeyValuePair<string, Point>> HelicalStructure {get;set;}
        SKPaint Wheel = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,            
        };
        SKPaint Line = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,
        };
        SKPaint PolarAminoAcid = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Blue,
            BlendMode = SKBlendMode.SrcATop
        };
        SKPaint NonPolarAminoAcid = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Black,
            BlendMode = SKBlendMode.SrcATop

        };
        SKPaint PolarLetters = new SKPaint
        {
            Style = SKPaintStyle.StrokeAndFill,
            Color = SKColors.WhiteSmoke,
            TextScaleX = .5f
        };
        SKPaint NonPolarLetters = new SKPaint
        {
            Style = SKPaintStyle.StrokeAndFill,
            Color = SKColors.White,
            TextScaleX = .5f
        };
        SKPaint InvalidAminoAcid = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Red,
            BlendMode = SKBlendMode.SrcATop
        };
        public WheelCanvas(string aminoSeq)
        {
            AminoSequence = aminoSeq;
            HelicalStructure = new List<KeyValuePair<string, Point>>();
            InitializeComponent();

        }
        public void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            var surface = args.Surface;
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.FloralWhite);

            int width = args.Info.Width;
            int height = args.Info.Height;
            
            canvas.Translate(width / 2, height / 2);
            canvas.Scale(width / 200f);
            canvas.DrawCircle(0, 0,RADIUS, Wheel);
            string aminos = AminoSequence;
            if (aminos.Contains(","))
                HelicalWheelBuilderThreeLetter(aminos, canvas);
            else
                HelicalWheelBuilderLetter(aminos, canvas);
            
        }
        public void HelicalWheelBuilderLetter(string aminoAcids, SKCanvas canvas)
        {
            var aminoClass = new AminoAcids();
            var listAminos = aminoAcids.ToCharArray().ToList();
            float x = 0; float y = 0;
            int angle = 100;
            char lastChar = '0';
            bool polarity = false;
            int incr = 1;
            var AminosOnCanvas = new Tuple<int, int>[listAminos.Count];
            foreach (var item in listAminos)
            {
                if (char.IsLetter(item))
                {
                    if (angle > 100)
                    {
                        canvas.DrawLine(x, y, (float)(RADIUS * Math.Cos(angle * Math.PI / 180F)), (float)(RADIUS * Math.Sin(angle * Math.PI / 180F)), Line);
                        if (polarity)
                            canvas.DrawText(lastChar.ToString().ToUpper() + incr, x - 6, y + 4, PolarLetters);
                        else
                            canvas.DrawText(lastChar.ToString().ToUpper() + incr, x - 6, y + 4, NonPolarLetters);
                        incr += 1;
                    }
                    x = (float)(RADIUS * Math.Cos(angle * Math.PI / 180F));
                    y = (float)(RADIUS * Math.Sin(angle * Math.PI / 180F));
                    HelicalStructure.Add(new KeyValuePair<string, Point>(item.ToString().ToUpper() + incr, new Point(x,y)) );
                    if (aminoClass.IsAminoAcid(null, item))
                    {
                        if (polarity = aminoClass.IsPolar(null, item))
                            canvas.DrawCircle(x, y, AMINORADIUS, PolarAminoAcid);
                        else
                            canvas.DrawCircle(x, y, AMINORADIUS, NonPolarAminoAcid);
                    }
                    else
                    {
                        canvas.DrawCircle(x, y, AMINORADIUS, InvalidAminoAcid);
                    }
                    angle += 100;
                    lastChar = item;
                }
            }
            if (char.IsLetter(lastChar))
            {
                if (polarity)
                    canvas.DrawText(lastChar.ToString().ToUpper() + incr, x - 6, y + 4, PolarLetters);
                else
                    canvas.DrawText(lastChar.ToString().ToUpper() + incr, x - 6, y + 4, NonPolarLetters);
            }
        }
        public void HelicalWheelBuilderThreeLetter(string aminoAcids, SKCanvas canvas)
        {
            var aminoClass = new AminoAcids();
            var listAminos = aminoAcids.Split(',').ToList();
            float xval = 0; float yval = 0;
            int angle = 100;
            bool polarity = false;
            string lastAmino ="";
            int incr = 1;
            foreach (var item in listAminos)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    if (angle > 100)
                    {
                        canvas.DrawLine(xval, yval, (float)(RADIUS * Math.Cos(angle * Math.PI / 180F)), (float)(RADIUS * Math.Sin(angle * Math.PI / 180F)), Line);
                        if (polarity)
                            canvas.DrawText(lastAmino.Substring(0, 1).ToString().ToUpper() + lastAmino.Substring(1,2) + incr, xval - 7, yval + 4, PolarLetters);
                        else
                            canvas.DrawText(lastAmino.Substring(0, 1).ToString().ToUpper() + lastAmino.Substring(1,2) + incr, xval - 7, yval + 4, NonPolarLetters);
                        incr += 1;
                    }
                    xval = (float)(RADIUS * Math.Cos(angle * Math.PI / 180F));
                    yval = (float)(RADIUS * Math.Sin(angle * Math.PI / 180F));
                    var itemFound = HelicalStructure.Where(x => (x.Value.X == xval));
                    if(itemFound.Any())
                    {
                        xval *= 1.1f;
                        yval *= 1.1f;
                    }
                    HelicalStructure.Add(new KeyValuePair<string, Point>(item.Substring(0, 1).ToString().ToUpper() + item.Substring(1, 2) + incr, new Point(xval, yval)));
                    if (aminoClass.IsAminoAcid(item))
                    {
                        if (polarity = aminoClass.IsPolar(item))
                            canvas.DrawCircle(xval, yval, AMINORADIUS, PolarAminoAcid);
                        else
                            canvas.DrawCircle(xval, yval, AMINORADIUS, NonPolarAminoAcid);
                    }
                    else
                    {
                        canvas.DrawCircle(xval, yval, AMINORADIUS, InvalidAminoAcid);
                    }
                    angle += 100;
                    lastAmino = item;
                }
            }
            if (!string.IsNullOrWhiteSpace(lastAmino))
            {
                if (polarity)
                    canvas.DrawText(lastAmino.Substring(0, 1).ToString().ToUpper() + lastAmino.Substring(1, 2) + incr, xval - 7, yval + 4, PolarLetters);
                else
                    canvas.DrawText(lastAmino.Substring(0, 1).ToString().ToUpper() + lastAmino.Substring(1, 2) + incr, xval - 7, yval + 4, NonPolarLetters);
            }
        }


    }
}
