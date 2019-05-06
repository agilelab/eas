using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Collections;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace fyiReporting.RDL
{
    /// <summary>
    /// 使用iText的Pdf的导出。
    /// </summary>
    class RenderiTextPdf : IPresent
    {
        static readonly char[] lineBreak = new char[] { '\n' };
        static readonly char[] wordBreak = new char[] { ' ' };
        //		static readonly int MEASUREMAX = int.MaxValue;  //  .Net 2 doesn't seem to have a limit; 1.1 limit was 32
        static readonly int MEASUREMAX = 32;  //  guess I'm wrong -- .Net 2 doesn't seem to have a limit; 1.1 limit was 32

        private  Report _Report;
        private Stream tw;

        private iTextSharp.text.Document document;
        private iTextSharp.text.Table textTable;

        iTextSharp.text.pdf.PdfWriter pdfWriter;

        private int currentPage = -1;

        private float hegihtOffset = 0;

        private Row _HeaderRow;
        private Table table;

        private int _ColIndex = -1;

        private int _ColSpan = 1;                  //行夸度

        private float rowHeight;    //行高

        public RenderiTextPdf(Report r, IStreamGen sg)
        {
            this._Report = r;
            this.tw = sg.GetStream();
        }

        #region IPresent 成员

        public bool IsPagingNeeded()
        {
            return true;
        }

        public void Start()
        {
            //iTextSharp.text.pdf.BaseFont.AddToResourceSearch(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "iTextAsian.dll"));
            //iTextSharp.text.pdf.BaseFont.AddToResourceSearch(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "iTextAsianCmaps.dll"));

            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(_Report.PageWidthPoints , _Report.PageHeightPoints);
            document = new iTextSharp.text.Document(pageSize, _Report.LeftMarginPoints, _Report.RightMarginPoints, _Report.TopMarginPoints, _Report.BottomMarginPoints);

            this.pdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(document, tw);

            document.SetMargins(_Report.LeftMarginPoints, _Report.RightMarginPoints, _Report.TopMarginPoints, _Report.BottomMarginPoints);

            document.Open();

            document.AddAuthor(_Report.Author);
            document.AddCreationDate();
            document.AddCreator(_Report.Author);
        }

        public void End()
        {
            document.Close();
        }

        /// <summary>
        /// 处理所有页面。
        /// </summary>
        /// <param name="pgs"></param>
        public void RunPages(Pages pgs)
        {
            foreach (Page p in pgs)
            {
                if (this.currentPage == -1)
                {
                    this.currentPage = 1;
                }
                else
                {
                    document.NewPage();
                    this.currentPage++;
                }

                ProcessPage(pgs, p);
            }

            return;
        }

        /// <summary>
        /// 处理单个页面。
        /// </summary>
        /// <param name="pgs"></param>
        /// <param name="items"></param>
        private void ProcessPage(Pages pgs, IEnumerable items)
        {
            foreach (PageItem pi in items)
            {
                //if (pi.SI.BackgroundImage != null)
                //{
                //    PageImage bgImg = pi.SI.BackgroundImage;
                //    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bgImg.ImageData);
                //    document.Add(image);
                //}

                if (pi is PageTextHtml)
                {
                    PageTextHtml pth = pi as PageTextHtml;
                    pth.Build(pgs.G);
                    ProcessPage(pgs, pth);
                    continue;
                }

                if (pi is PageText)
                {
                    PageText pt = pi as PageText;

                    iTextSharp.text.pdf.PdfContentByte cb = this.pdfWriter.DirectContent;

                    float y = getYvalue(pi.Y, pt.H);

                    //边线

                    if (pi.SI.BStyleLeft != BorderStyleEnum.None)
                    {
                        cb.MoveTo(pt.X, y);
                        cb.SetLineWidth(pi.SI.BWidthLeft);
                        cb.SetRGBColorStrokeF(pi.SI.BColorLeft.R, pi.SI.BColorLeft.G, pi.SI.BColorLeft.B);

                        cb.LineTo(pt.X, y + pt.H);

                        cb.Stroke();
                    }

                    if (pi.SI.BStyleRight != BorderStyleEnum.None)
                    {
                        cb.MoveTo(pt.X + pt.W, y);
                        cb.SetLineWidth(pi.SI.BWidthRight);
                        cb.SetRGBColorStrokeF(pi.SI.BColorRight.R, pi.SI.BColorRight.G, pi.SI.BColorRight.B);

                        cb.LineTo(pt.X + pt.W, y + pt.H);

                        cb.Stroke();
                    }

                    if (pi.SI.BStyleTop != BorderStyleEnum.None)
                    {
                        cb.MoveTo(pt.X, y + pt.H);
                        cb.SetLineWidth(pi.SI.BWidthTop);
                        cb.SetRGBColorStrokeF(pi.SI.BColorTop.R, pi.SI.BColorTop.G, pi.SI.BColorTop.B);

                        cb.LineTo(pt.X + pt.W, y + pt.H);

                        cb.Stroke();
                    }

                    if (pi.SI.BStyleBottom != BorderStyleEnum.None)
                    {
                        cb.MoveTo(pt.X, y);
                        cb.SetLineWidth(pi.SI.BWidthTop);
                        cb.SetRGBColorStrokeF(pi.SI.BColorTop.R, pi.SI.BColorTop.G, pi.SI.BColorTop.B);

                        cb.LineTo(pt.X + pt.W, y);

                        cb.Stroke();
                    }

                    //绝对定义文字

                    iTextSharp.text.Font font = TextUtility.GetFont(pi.SI,pt.Text);

                    float[] widih;
                    string[] sa = MeasureString(pt, pgs.G, out widih);

                    int rows = sa.Length;

                    //x标准固定

                    float x = pt.X + pi.SI.PaddingLeft;
                    int align = iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT;

                    if (pi.SI.TextAlign == TextAlignEnum.Right)
                    {
                        align = iTextSharp.text.pdf.PdfContentByte.ALIGN_RIGHT;
                        x = pt.X + pt.W - pi.SI.PaddingRight-1;
                    }
                    else if (pi.SI.TextAlign == TextAlignEnum.Center)
                    {
                        align = iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER;
                        x = pt.X + pt.W / 2;
                    }

                    cb.BeginText();
                    cb.SetFontAndSize(font.BaseFont, font.Size);

                    for (int i = 0; i < rows; i++)
                    {
                        float Yt = y + i * font.Size+1;

                        if (pi.SI.VerticalAlign == VerticalAlignEnum.Top)
                        {
                            Yt = y + pt.H - font.Size * (rows -(i+1))- 1;
                        }
                        else if (pi.SI.VerticalAlign == VerticalAlignEnum.Middle)
                        {
                            Yt = y + (pt.H - font.Size * rows) / 2 + i*font.Size+1;
                        }

                        cb.ShowTextAligned(align, sa[rows-i-1], x, Yt, 0);
                        cb.EndText();
                    }

                    continue;
                }

                if (pi is PageLine)
                {
                    PageLine pl = pi as PageLine;

                    iTextSharp.text.pdf.PdfContentByte cb = this.pdfWriter.DirectContent;

                    float y1 = getYvalue(pl.Y, 0);
                    float y2 = getYvalue(pl.Y2, 0);

                    cb.MoveTo(pl.X, y1);
                    cb.LineTo(pl.X2, y2);

                    cb.Stroke();

                    continue;
                }

                if (pi is PageImage)
                {
                    PageImage i = pi as PageImage;

                    iTextSharp.text.pdf.PdfContentByte cb = this.pdfWriter.DirectContent;

                    float y = this.getYvalue(i.Y, i.H);

                    System.Drawing.RectangleF r2 = new System.Drawing.RectangleF(i.X + i.SI.PaddingLeft,y - i.SI.PaddingTop, i.W - i.SI.PaddingLeft - i.SI.PaddingRight, i.H - i.SI.PaddingTop - i.SI.PaddingBottom);

                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(i.ImageData);

                    image.SetAbsolutePosition(i.X, y);
                    image.ScaleAbsoluteHeight(i.H);
                    image.ScaleAbsoluteWidth(i.W);

                    cb.AddImage(image);

                    cb.Stroke();

                    continue;
                }

                if (pi is PageRectangle)
                {
                    PageRectangle pr = pi as PageRectangle;

                    iTextSharp.text.Rectangle r2 = new iTextSharp.text.Rectangle(pr.X,pr.Y,pr.W,pr.H);
                    r2.Border = 1;
                    document.Add(r2);
                    continue;
                }
            }
        }

        internal float getYvalue(float y,float h)
        {
            return _Report.PageHeightPoints - y-h;
        }

        #region MeasureSting

        private string[] MeasureString(PageText pt, System.Drawing.Graphics g)
        {
            StyleInfo si = pt.SI;
            string s = pt.Text;

            System.Drawing.Font drawFont = null;
            System.Drawing.StringFormat drawFormat = null;
            System.Drawing.SizeF ms;

            string[] sa = null;

            try
            {
                // STYLE
                System.Drawing.FontStyle fs = 0;
                if (si.FontStyle == FontStyleEnum.Italic)
                    fs |= System.Drawing.FontStyle.Italic;

                if (si.IsFontBold())
                    fs |= System.Drawing.FontStyle.Bold;

                if (si.TextDecoration == TextDecorationEnum.Underline)
                    fs |= System.Drawing.FontStyle.Underline;

                drawFont = new System.Drawing.Font(StyleInfo.GetFontFamily(si.FontFamilyFull), si.FontSize, fs);

                drawFormat = new System.Drawing.StringFormat();

                if (si.TextAlign == TextAlignEnum.Center)
                    drawFormat.Alignment = System.Drawing.StringAlignment.Center;
                if (si.TextAlign == TextAlignEnum.Right)
                    drawFormat.Alignment = System.Drawing.StringAlignment.Far;
                else
                    drawFormat.Alignment = System.Drawing.StringAlignment.Near;

                ms = MeasureString(pt.Text, g, drawFont, drawFormat);

                float width = pt.W - si.PaddingLeft - si.PaddingRight;

                if (ms.Width <= (width + si.FontSize*1.5))
                {
                    sa = new string[1];
                    sa[0] = pt.Text;
                }
                else
                {
                    int x = (int)(ms.Width / width) + 1;

                    List<string> texts = new List<string>();

                    int len = pt.Text.Length;
                    int start = 0;
                    int setup = len / x;

                    while (true)
                    {
                        ms = MeasureString(pt.Text.Substring(start, setup), g, drawFont, drawFormat);

                        float subW = width - ms.Width;

                        if ((subW >= 0) & (subW <= si.FontSize))
                        {
                            texts.Add(pt.Text.Substring(start, setup));

                            if ((start + setup) == len)
                            {
                                break;
                            }
                            else
                            {
                                start += setup;

                                if ((start + setup) >= len)
                                {
                                    setup = len - start;
                                }
                            }
                        }
                        else if ((subW >= 0) & (subW > si.FontSize))
                        {
                            if ((start + setup) == len)
                            {
                                texts.Add(pt.Text.Substring(start, setup));
                                break;
                            }
                            setup++;
                        }
                        else if (subW < 0)
                        {
                            setup--;
                        }
                    }

                    sa = new string[texts.Count];
                    texts.CopyTo(sa);
                }
            }
            finally
            {
                if (drawFont != null)
                    drawFont.Dispose();
                if (drawFormat != null)
                    drawFont.Dispose();
            }

            return sa;
        }

        private string[] MeasureString(PageText pt, System.Drawing.Graphics g, out float[] width)
        {
            StyleInfo si = pt.SI;
            string s = pt.Text;

            System.Drawing.Font drawFont = null;
            System.Drawing.StringFormat drawFormat = null;
            System.Drawing.SizeF ms;
            string[] sa = null;
            width = null;
            try
            {
                // STYLE
                System.Drawing.FontStyle fs = 0;
                if (si.FontStyle == FontStyleEnum.Italic)
                    fs |= System.Drawing.FontStyle.Italic;

                // WEIGHT
                switch (si.FontWeight)
                {
                    case FontWeightEnum.Bold:
                    case FontWeightEnum.Bolder:
                    case FontWeightEnum.W500:
                    case FontWeightEnum.W600:
                    case FontWeightEnum.W700:
                    case FontWeightEnum.W800:
                    case FontWeightEnum.W900:
                        fs |= System.Drawing.FontStyle.Bold;
                        break;
                    default:
                        break;
                }

                drawFont = new System.Drawing.Font(StyleInfo.GetFontFamily(si.FontFamilyFull), si.FontSize, fs);
                drawFormat = new System.Drawing.StringFormat();
                drawFormat.Alignment = System.Drawing.StringAlignment.Near;

                // Measure string   
                //  pt.NoClip indicates that this was generated by PageTextHtml Build.  It has already word wrapped.
                if (pt.NoClip || pt.SI.WritingMode == WritingModeEnum.tb_rl)	// TODO: support multiple lines for vertical text
                {
                    ms = MeasureString(s, g, drawFont, drawFormat);
                    width = new float[1];
                    width[0] = RSize.PointsFromPixels(g, ms.Width);	// convert to points from pixels
                    sa = new string[1];
                    sa[0] = s;
                    return sa;
                }

                // handle multiple lines;
                //  1) split the string into the forced line breaks (ie "\n and \r")
                //  2) foreach of the forced line breaks; break these into words and recombine 
                s = s.Replace("\r\n", "\n");	// don't want this to result in double lines
                string[] flines = s.Split(lineBreak);
                List<string> lines = new List<string>();
                List<float> lineWidths = new List<float>();
                // remove the size reserved for left and right padding
                float ptWidth = pt.W - pt.SI.PaddingLeft - pt.SI.PaddingRight;
                if (ptWidth <= 0)
                    ptWidth = 1;
                foreach (string tfl in flines)
                {
                    string fl;
                    if (tfl.Length > 0 && tfl[tfl.Length - 1] == ' ')
                        fl = tfl.TrimEnd(' ');
                    else
                        fl = tfl;

                    // Check if entire string fits into a line
                    ms = MeasureString(fl, g, drawFont, drawFormat);
                    float tw = RSize.PointsFromPixels(g, ms.Width);
                    if (tw <= ptWidth)
                    {                       // line fits don't need to break it down further
                        lines.Add(fl);
                        lineWidths.Add(tw);
                        continue;
                    }

                    // Line too long; need to break into multiple lines
                    // 1) break line into parts; then build up again keeping track of word positions
                    string[] parts = fl.Split(wordBreak);	// this is the maximum split of lines
                    StringBuilder sb = new StringBuilder(fl.Length);
                    System.Drawing.CharacterRange[] cra = new System.Drawing.CharacterRange[parts.Length];
                    for (int i = 0; i < parts.Length; i++)
                    {
                        int sc = sb.Length;     // starting character
                        sb.Append(parts[i]);    // endding character
                        if (i != parts.Length - 1)  // last item doesn't need blank
                            sb.Append(" ");
                        int ec = sb.Length;
                        System.Drawing.CharacterRange cr = new System.Drawing.CharacterRange(sc, ec - sc);
                        cra[i] = cr;            // add to character array
                    }

                    // 2) Measure the word locations within the line
                    string wfl = sb.ToString();
                    WordStartFinish[] wordLocations = MeasureString(wfl, g, drawFont, drawFormat, cra);
                    if (wordLocations == null)
                        continue;

                    // 3) Loop thru creating new lines as needed
                    int startLoc = 0;
                    System.Drawing.CharacterRange crs = cra[startLoc];
                    System.Drawing.CharacterRange cre = cra[startLoc];
                    float cwidth = wordLocations[0].end;    // length of the first
                    float bwidth = wordLocations[0].start;  // characters need a little extra on start
                    string ts;
                    bool bLine = true;
                    for (int i = 1; i < cra.Length; i++)
                    {
                        cwidth = wordLocations[i].end - wordLocations[startLoc].start + bwidth;
                        if (cwidth > ptWidth)
                        {	// time for a new line
                            cre = cra[i - 1];
                            ts = wfl.Substring(crs.First, cre.First + cre.Length - crs.First);
                            lines.Add(ts);
                            lineWidths.Add(wordLocations[i - 1].end - wordLocations[startLoc].start + bwidth);

                            // Find the first non-blank character of the next line
                            while (i < cra.Length &&
                                    cra[i].Length == 1 &&
                                    fl[cra[i].First] == ' ')
                            {
                                i++;
                            }
                            if (i < cra.Length)   // any lines left?
                            {  // yes, continue on
                                startLoc = i;
                                crs = cre = cra[startLoc];
                                cwidth = wordLocations[i].end - wordLocations[startLoc].start + bwidth;
                            }
                            else  // no, we can stop
                                bLine = false;
                            //  bwidth = wordLocations[startLoc].start - wordLocations[startLoc - 1].end;
                        }
                        else
                            cre = cra[i];
                    }
                    if (bLine)
                    {
                        ts = fl.Substring(crs.First, cre.First + cre.Length - crs.First);
                        lines.Add(ts);
                        lineWidths.Add(cwidth);
                    }
                }
                // create the final array from the Lists
                string[] la = lines.ToArray();
                width = lineWidths.ToArray();
                return la;
            }
            finally
            {
                if (drawFont != null)
                    drawFont.Dispose();
                if (drawFormat != null)
                    drawFont.Dispose();
            }
        }

        /// <summary>
        /// Measures the location of an arbritrary # of words within a string
        /// </summary>
        private WordStartFinish[] MeasureString(string s, System.Drawing.Graphics g, System.Drawing.Font drawFont, System.Drawing.StringFormat drawFormat, System.Drawing.CharacterRange[] cra)
        {
            if (cra.Length <= MEASUREMAX)		// handle the simple case of < MEASUREMAX words
                return MeasureString32(s, g, drawFont, drawFormat, cra);

            // Need to compensate for SetMeasurableCharacterRanges limitation of 32 (MEASUREMAX)
            int mcra = (cra.Length / MEASUREMAX);	// # of full 32 arrays we need
            int ip = cra.Length % MEASUREMAX;		// # of partial entries needed for last array (if any)
            WordStartFinish[] sz = new WordStartFinish[cra.Length];	// this is the final result;
            float startPos = 0;
            System.Drawing.CharacterRange[] cra32 = new System.Drawing.CharacterRange[MEASUREMAX];	// fill out			
            int icra = 0;						// index thru the cra 
            for (int i = 0; i < mcra; i++)
            {
                // fill out the new array
                int ticra = icra;
                for (int j = 0; j < cra32.Length; j++)
                {
                    cra32[j] = cra[ticra++];
                    cra32[j].First -= cra[icra].First;	// adjust relative offsets of strings
                }

                // measure the word locations (in the new string)
                // ???? should I put a blank in front of it?? 
                string ts = s.Substring(cra[icra].First,
                    cra[icra + cra32.Length - 1].First + cra[icra + cra32.Length - 1].Length - cra[icra].First);
                WordStartFinish[] pos = MeasureString32(ts, g, drawFont, drawFormat, cra32);

                // copy the values adding in the new starting positions
                for (int j = 0; j < pos.Length; j++)
                {
                    sz[icra].start = pos[j].start + startPos;
                    sz[icra++].end = pos[j].end + startPos;
                }
                startPos = sz[icra - 1].end;	// reset the start position for the next line
            }
            // handle the remaining character
            if (ip > 0)
            {
                // resize the range array
                cra32 = new System.Drawing.CharacterRange[ip];
                // fill out the new array
                int ticra = icra;
                for (int j = 0; j < cra32.Length; j++)
                {
                    cra32[j] = cra[ticra++];
                    cra32[j].First -= cra[icra].First;	// adjust relative offsets of strings
                }
                // measure the word locations (in the new string)
                // ???? should I put a blank in front of it?? 
                string ts = s.Substring(cra[icra].First,
                    cra[icra + cra32.Length - 1].First + cra[icra + cra32.Length - 1].Length - cra[icra].First);
                WordStartFinish[] pos = MeasureString32(ts, g, drawFont, drawFormat, cra32);

                // copy the values adding in the new starting positions
                for (int j = 0; j < pos.Length; j++)
                {
                    sz[icra].start = pos[j].start + startPos;
                    sz[icra++].end = pos[j].end + startPos;
                }
            }
            return sz;
        }

        /// <summary>
        /// Measures the location of words within a string;  limited by .Net 1.1 to 32 words
        ///     MEASUREMAX is a constant that defines that limit
        /// </summary>
        /// <param name="s"></param>
        /// <param name="g"></param>
        /// <param name="drawFont"></param>
        /// <param name="drawFormat"></param>
        /// <param name="cra"></param>
        /// <returns></returns>
        private WordStartFinish[] MeasureString32(string s, System.Drawing.Graphics g, System.Drawing.Font drawFont, System.Drawing.StringFormat drawFormat, System.Drawing.CharacterRange[] cra)
        {
            if (s == null || s.Length == 0)
                return null;

            drawFormat.SetMeasurableCharacterRanges(cra);
            System.Drawing.Region[] rs = new System.Drawing.Region[cra.Length];
            rs = g.MeasureCharacterRanges(s, drawFont, new System.Drawing.RectangleF(0, 0, float.MaxValue, float.MaxValue),
                drawFormat);
            WordStartFinish[] sz = new WordStartFinish[cra.Length];
            int isz = 0;
            foreach (System.Drawing.Region r in rs)
            {
                System.Drawing.RectangleF mr = r.GetBounds(g);
                sz[isz].start = RSize.PointsFromPixels(g, mr.Left);
                sz[isz].end = RSize.PointsFromPixels(g, mr.Right);
                isz++;
            }
            return sz;
        }

        struct WordStartFinish
        {
            internal float start;
            internal float end;
        }

        private System.Drawing.SizeF MeasureString(string s, System.Drawing.Graphics g, System.Drawing.Font drawFont, System.Drawing.StringFormat drawFormat)
        {
            if (s == null || s.Length == 0)
                return System.Drawing.SizeF.Empty;

            System.Drawing.CharacterRange[] cr = { new System.Drawing.CharacterRange(0, s.Length) };
            drawFormat.SetMeasurableCharacterRanges(cr);
            System.Drawing.Region[] rs = new System.Drawing.Region[1];
            rs = g.MeasureCharacterRanges(s, drawFont, new System.Drawing.RectangleF(0, 0, float.MaxValue, float.MaxValue),
                drawFormat);
            System.Drawing.RectangleF mr = rs[0].GetBounds(g);

            return new System.Drawing.SizeF(mr.Width, mr.Height);
        }

        private float MeasureStringBlank(System.Drawing.Graphics g, System.Drawing.Font drawFont, System.Drawing.StringFormat drawFormat)
        {
            System.Drawing.SizeF ms = MeasureString(" ", g, drawFont, drawFormat);
            float width = RSize.PointsFromPixels(g, ms.Width);	// convert to points from pixels
            return width * 2;
        }

        #endregion

        public void BodyStart(Body b)
        {
            
        }

        public void BodyEnd(Body b)
        {
            
        }

        public void PageHeaderStart(PageHeader ph)
        {
            
        }

        public void PageHeaderEnd(PageHeader ph)
        {
            
        }

        public void PageFooterStart(PageFooter pf)
        {
            
        }

        public void PageFooterEnd(PageFooter pf)
        {
            
        }        

        public void DataRegionNoRows(DataRegion d, string noRowsMsg)
        {
            
        }

        public bool ListStart(List l, Row r)
        {
            return true;
        }

        public void ListEnd(List l, Row r)
        {
            
        }

        public void ListEntryBegin(List l, Row r)
        {
            
        }

        public void ListEntryEnd(List l, Row r)
        {
            
        }

        public bool TableStart(Table t, Row r)
        {
            this.textTable = new iTextSharp.text.Table(t.TableColumns.Items.Count);

            this.textTable.TableFitsPage = true;
            this.textTable.CellsFitPage = true;

            this.textTable.Border = iTextSharp.text.Rectangle.NO_BORDER;
            this.textTable.BorderWidth = 0;

            this.textTable.Padding = 0;
            this.textTable.Spacing = 0;

            List<float> widths = new List<float>(t.TableColumns.Items.Count);

            foreach (TableColumn tc in t.TableColumns)
            {
                widths.Add(tc.Width.Points);
            }

            float[] ws = new float[widths.Count];
            widths.CopyTo(ws);

            this.textTable.Widths = ws;

            return true;
        }

        public void TableEnd(Table t, Row r)
        {
            this.document.Add(this.textTable);
        }

        public void TableBodyStart(Table t, Row r)
        {
            
        }

        public void TableBodyEnd(Table t, Row r)
        {
            
        }

        public void TableFooterStart(Footer f, Row r)
        {
            
        }

        public void TableFooterEnd(Footer f, Row r)
        {
            
        }

        public void TableHeaderStart(Header h, Row r)
        {

        }

        public void TableHeaderEnd(Header h, Row r)
        {
            this.textTable.EndHeaders();
        }

        public void TableRowStart(TableRow tr, Row r)
        {
            
        }

        public void TableRowEnd(TableRow tr, Row r)
        {
            _ColIndex = -1;
             _ColSpan = 1;             //行夸度
        }

        public void TableCellStart(TableCell t, Row r)
        {
            _ColIndex += _ColSpan;
        }

        public void TableCellEnd(TableCell t, Row r)
        {
            _ColSpan = t.ColSpan;      //魏琼东
        }

        public void Textbox(Textbox tb, string t, Row r)
        {
            TableCell tableCell = tb.Parent.Parent as TableCell;

            if (tableCell != null)
            {
                string text = tb.RunText(this._Report, r);

                StyleInfo si = tb.Style.GetStyleInfo(_Report, r);

                iTextSharp.text.Font ft = TextUtility.GetFont(si, text);

                iTextSharp.text.Cell cell = new iTextSharp.text.Cell(new iTextSharp.text.Phrase(text, ft));
                cell.Colspan = tableCell.ColSpan;                

                //水不对齐
                if (si.TextAlign == TextAlignEnum.Center)
                {
                    cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                }
                else if (si.TextAlign == TextAlignEnum.Left)
                {
                    cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                }
                else if (si.TextAlign == TextAlignEnum.Right)
                {
                    cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                }

                ////垂直对齐
                if (si.VerticalAlign == VerticalAlignEnum.Middle)
                {
                    cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                }
                else if (si.VerticalAlign == VerticalAlignEnum.Top)
                {
                    cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                }
                else if (si.VerticalAlign == VerticalAlignEnum.Bottom)
                {
                    cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                }

                //cell.BackgroundColor = new Color(si.BackgroundColor);

                cell.BorderColorLeft = new Color(si.BColorLeft);
                cell.BorderColorRight = new Color(si.BColorRight);
                cell.BorderColorTop = new Color(si.BColorTop);
                cell.BorderColorBottom = new Color(si.BColorBottom);
                
                int border  = iTextSharp.text.Rectangle.NO_BORDER;

                if (si.BStyleLeft != BorderStyleEnum.None)
                {
                    cell.BorderWidthLeft = si.BWidthLeft / 4;
                    border += iTextSharp.text.Rectangle.LEFT_BORDER;
                }
                else
                {
                    cell.BorderWidthLeft = 0;
                }

                if (si.BStyleRight != BorderStyleEnum.None)
                {
                    cell.BorderWidthRight = si.BWidthRight / 4;
                    border += iTextSharp.text.Rectangle.RIGHT_BORDER;
                }
                else
                {
                    cell.BorderWidthRight = 0;
                }

                if (si.BStyleTop != BorderStyleEnum.None)
                {
                    cell.BorderWidthTop = si.BWidthTop / 4;
                    border += iTextSharp.text.Rectangle.TOP_BORDER;
                }
                else
                {
                    cell.BorderWidthTop = 0;
                }

                if (si.BStyleBottom != BorderStyleEnum.None)
                {
                    cell.BorderWidthBottom = si.BWidthBottom / 4;
                    border += iTextSharp.text.Rectangle.BOTTOM_BORDER;
                }
                else
                {
                    cell.BorderWidthBottom = 0;
                }

                cell.Border = border;
                this.textTable.AddCell(cell);
            }
        }

        public bool MatrixStart(Matrix m, MatrixCellEntry[,] matrix, Row r, int headerRows, int maxRows, int maxCols)
        {
            return true;
        }

        public void MatrixColumns(Matrix m, MatrixColumns mc)
        {
            
        }

        public void MatrixRowStart(Matrix m, int row, Row r)
        {
            
        }

        public void MatrixRowEnd(Matrix m, int row, Row r)
        {
            
        }

        public void MatrixCellStart(Matrix m, ReportItem ri, int row, int column, Row r, float h, float w, int colSpan)
        {
            
        }

        public void MatrixCellEnd(Matrix m, ReportItem ri, int row, int column, Row r)
        {
            
        }

        public void MatrixEnd(Matrix m, Row r)
        {
            
        }

        public void Chart(Chart c, Row r, ChartBase cb)
        {
            
        }

        public void Image(Image i, Row r, string mimeType, System.IO.Stream io)
        {
            
        }

        public void Line(Line l, Row r)
        {
            
        }

        public bool RectangleStart(Rectangle rect, Row r)
        {
            return true;
        }

        public void RectangleEnd(Rectangle rect, Row r)
        {
            
        }

        public void Subreport(Subreport s, Row r)
        {
            
        }

        public void GroupingStart(Grouping g)
        {
            
        }

        public void GroupingInstanceStart(Grouping g)
        {
            
        }

        public void GroupingInstanceEnd(Grouping g)
        {
            
        }

        public void GroupingEnd(Grouping g)
        {
            
        }

        public Report Report()
        {
            return this._Report;
        }

        #endregion
    }
}
