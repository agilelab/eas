using System;
using System.Collections.Generic;
using System.Text;

using iTextSharp.text.pdf;
using System.IO;

namespace fyiReporting.RDL
{
    class TextUtility
    {
        private static Dictionary<string, BaseFont> baseFonts = new Dictionary<string, BaseFont>();

        private static bool TextIsUnicode(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] > 0x00ff)
                {
                    return true;
                }                
            }

            return false;
        }

        /// <summary>
        /// 如果为西文,则直接使用默认字体。
        /// </summary>
        /// <param name="style"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static iTextSharp.text.Font GetFont(fyiReporting.RDL.StyleInfo style,string text)
        {
            string fontName = TextUtility.GetFontName(style.FontFamily);

            iTextSharp.text.Font font = null;

            if (fontName.IndexOf(".") > 0)
            {
                BaseFont baseFT = null;

                if (baseFonts.ContainsKey(fontName))
                {
                    baseFT = baseFonts[fontName];

                    if (baseFT != null)
                        font = new iTextSharp.text.Font(baseFT);
                    else
                        font = iTextSharp.text.FontFactory.GetFont("Helvetica");
                }
                else
                {
                    string windir = System.Environment.GetEnvironmentVariable("windir");
                    string path = Path.Combine(windir, "Fonts");
                    path = Path.Combine(path, fontName);

                    if (File.Exists(path))
                    {
                        try
                        {
                            baseFT = BaseFont.CreateFont(path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        }
                        catch
                        {
                            //
                        }
                    }

                    if (baseFT == null)
                        try
                        {
                            baseFT = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.NOT_EMBEDDED);
                        }
                        catch
                        {

                        }

                    if (baseFT != null)
                    {
                        baseFonts.Add(fontName, baseFT);
                        font = new iTextSharp.text.Font(baseFT);
                    }
                    else
                    {
                        font = iTextSharp.text.FontFactory.GetFont("Helvetica");
                        baseFonts.Add(fontName, null);
                    }
                }
            }
            else
            {
                font = iTextSharp.text.FontFactory.GetFont(fontName);
            }
 
            font.Color = new iTextSharp.text.Color(style.Color);
            font.Size = style.FontSize;

            if (style.IsFontBold())
                font.IsBold();

            if (style.FontStyle == FontStyleEnum.Italic)
                font.IsItalic();

            if (style.TextDecoration == TextDecorationEnum.Underline)
                font.IsUnderlined();

            return font;
        }

        ///// <summary>
        ///// 如果为西文,则直接使用默认字体。
        ///// </summary>
        ///// <param name="style"></param>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //public static iTextSharp.text.Font GetFont(fyiReporting.RDL.StyleInfo style, string text)
        //{
        //    string fontName = TextUtility.GetFontName(style.FontFamily);

        //    iTextSharp.text.Font font = null;

        //    if (!TextIsUnicode(text))
        //    {
        //        if (fontName.IndexOf(".") > 0)
        //        {
        //            font = iTextSharp.text.FontFactory.GetFont("Helvetica");
        //        }
        //    }
        //    else
        //    {
        //        if (fontName.IndexOf(".") > 0)
        //        {
        //            BaseFont baseFT = null;

        //            if (baseFonts.ContainsKey(fontName))
        //            {
        //                baseFT = baseFonts[fontName];

        //                if (baseFT != null)
        //                    font = new iTextSharp.text.Font(baseFT);
        //                else
        //                    font = iTextSharp.text.FontFactory.GetFont("Helvetica");
        //            }
        //            else
        //            {
        //                string windir = System.Environment.GetEnvironmentVariable("windir");
        //                string path = Path.Combine(windir, "Fonts");
        //                path = Path.Combine(path, fontName);

        //                if (File.Exists(path))
        //                {
        //                    try
        //                    {
        //                        baseFT = BaseFont.CreateFont(path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        //                    }
        //                    catch
        //                    {
        //                        //
        //                    }
        //                }

        //                if (baseFT == null)
        //                    try
        //                    {
        //                        baseFT = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.NOT_EMBEDDED);
        //                    }
        //                    catch
        //                    {

        //                    }

        //                if (baseFT != null)
        //                {
        //                    baseFonts.Add(fontName, baseFT);
        //                    font = new iTextSharp.text.Font(baseFT);
        //                }
        //                else
        //                {
        //                    font = iTextSharp.text.FontFactory.GetFont("Helvetica");
        //                    baseFonts.Add(fontName, null);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            font = iTextSharp.text.FontFactory.GetFont(fontName);
        //        }
        //    }

        //    font.Color = new iTextSharp.text.Color(style.Color);
        //    font.Size = style.FontSize;

        //    if (style.IsFontBold())
        //        font.IsBold();

        //    if (style.FontStyle == FontStyleEnum.Italic)
        //        font.IsItalic();

        //    if (style.TextDecoration == TextDecorationEnum.Underline)
        //        font.IsUnderlined();

        //    return font;
        //}

        internal static string GetFontName(string face)
        {
            string faceName;

            switch (face.ToLower())
            {
                case "times":
                case "times-roman":
                case "times roman":
                case "timesnewroman":
                case "times new roman":
                case "timesnewromanps":
                case "timesnewromanpsmt":
                case "serif":
                    faceName = "Times-Roman";
                    break;
                case "helvetica":
                case "arial":
                case "arialmt":
                case "sans-serif":
                case "sans serif":
                    faceName = "Helvetica";
                    break;
                case "courier":
                case "couriernew":
                case "courier new":
                case "couriernewpsmt":
                case "monospace":
                    faceName = "Courier";
                    break;
                case "symbol":
                    faceName = "Symbol";
                    break;
                case "zapfdingbats":
                case "wingdings":
                case "wingding":
                    faceName = "ZapfDingbats";
                    break;
                case "隶书":
                    faceName = "SIMLI.TTF";
                    break;
                case "黑体":
                    faceName = "SIMHEI.TTF";
                    break;
                case "幼圆":
                    faceName = "SIMYOU.TTF";
                    break;
                case "楷体":
                    faceName = "SIMKAI.TTF";
                    break;
                case "微软雅黑":
                    faceName = "MSYH.TTF";
                    break;
                case "仿宋":
                case "仿宋_gb2312":
                    faceName = "SIMFANG.TTF";
                    break;
                case "华文中宋":
                    faceName = "STZHONGS.TTF";
                    break;
                case "华文行揩":
                    faceName = "STXINGKA.TTF";
                    break;

                case "华文新魏":
                    faceName = "STXINWEI.TTF";
                    break;

                case "华文细黑":
                    faceName = "STXIHEI.TTF";
                    break;

                case "华文宋体":
                    faceName = "STSONG.TTF";
                    break;

                case "华文楷体":
                    faceName = "STLITI.TTF";
                    break;

                case "华文琥珀":
                    faceName = "STHUPO.TTF";
                    break;

                case "华文仿宋":
                    faceName = "STFANGSO.TTF";
                    break;

                case "华文彩云":
                    faceName = "STCAIYUN.TTF";
                    break;

                case "汉仪中黑简":
                    faceName = "Hy.ttf";
                    break;

                case "方正舒体":
                    faceName = "FZSTK.TTF";
                    break;

                case "方正姚体":
                    faceName = "FZYTK.TTF";
                    break;

                case "宋体":
                default:
                    {
                        //if (System.Text.Encoding.Default == System.Text.Encoding.ASCII)
                        if(face == null)
                            faceName = "Helvetica";
                        else if (face.Length ==0)
                            faceName = "Helvetica";
                        else if (face[0] < 0x00ff)
                            faceName = "Helvetica";
                        else
                            faceName = "SIMSUN.TTC";
                    }
                    break;
            }

            return faceName;
        }
    }
}
