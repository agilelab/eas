/* ====================================================================
   Copyright (C) 2004-2008  fyiReporting Software, LLC

   This file is part of the fyiReporting RDL project.
	
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.


   For additional information, email info@fyireporting.com or visit
   the website www.fyiReporting.com.
*/

using System;
using System.Xml;
using System.IO;
using System.Drawing.Imaging;

namespace fyiReporting.RDL
{
	///<summary>
	/// Defines the Chart.  A DataRegion and ReportItem
	///</summary>
	[Serializable]
	internal class Chart : DataRegion
	{
		static readonly ImageFormat IMAGEFORMAT = ImageFormat.Jpeg;
		ChartTypeEnum _Type;	// Generic Type of the chart Default: Column
		ChartSubTypeEnum _Subtype;	// Available subtypes (and default subtype) depends on Type
		SeriesGroupings _SeriesGroupings;	// Set of series groupings for the chart
		CategoryGroupings _CategoryGroupings;	// Set of category (X) groupings for the chart
		ChartData _ChartData;	// Defines the data values for the chart
		Legend _Legend;				// Defines the chart legend
		CategoryAxis _CategoryAxis;	// Defines the category axis
		ValueAxis _ValueAxis;		// Defines the value axis
		Title _Title;				// Defines a title for the chart
		int _PointWidth;			//Non-zero Percent width for bars and
									//	columns. A value of 100 represents 100%
									//	of the distance between points (i.e. a
									//	value greater than 100 will cause columns
									//	to overlap each other).
		ChartPaletteEnum _Palette;		// Determines the color palette for the chart items.
		ThreeDProperties _ThreeDProperties;	//Properties for a 3D chart layout.
		PlotArea _PlotArea;			//Properties for the plot area
		ChartElementOutputEnum _ChartElementOutput;	// Indicates whether a DataPoints element
									// containing the chart data points should
									// appear in a data rendering.  Default: Output
		Matrix _ChartMatrix;		// Pseudo matrix to calculate chart data

		internal Chart(ReportDefn r, ReportLink p, XmlNode xNode):base(r, p, xNode)
		{
			_Type=ChartTypeEnum.Column;
			_Subtype=ChartSubTypeEnum.Plain;
			_SeriesGroupings=null;
			_CategoryGroupings=null;
			_ChartData=null;
			_Legend=null;
			_CategoryAxis=null;
			_ValueAxis=null;
			_Title=null;
			_PointWidth=0;
			_Palette=ChartPaletteEnum.Default;
			_ThreeDProperties=null;
			_PlotArea=null;
			_ChartElementOutput=ChartElementOutputEnum.Output;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Type":
						_Type = ChartType.GetStyle(xNodeLoop.InnerText);
						if (_Type == ChartTypeEnum.Stock ||
							_Type == ChartTypeEnum.Unknown)
						{
							OwnerReport.rl.LogError(8, "Chart type '" + xNodeLoop.InnerText + "' is not currently supported.");
						}
						break;
					case "Subtype":
						_Subtype = ChartSubType.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "SeriesGroupings":
						_SeriesGroupings = new SeriesGroupings(r, this, xNodeLoop);
						break;
					case "CategoryGroupings":
						_CategoryGroupings = new CategoryGroupings(r, this, xNodeLoop);
						break;
					case "ChartData":
						_ChartData = new ChartData(r, this, xNodeLoop);
						break;
					case "Legend":
						_Legend = new Legend(r, this, xNodeLoop);
						break;
					case "CategoryAxis":
						_CategoryAxis = new CategoryAxis(r, this, xNodeLoop);
						break;
					case "ValueAxis":
						_ValueAxis = new ValueAxis(r, this, xNodeLoop);
						break;
					case "Title":
						_Title = new Title(r, this, xNodeLoop);
						break;
					case "PointWidth":
						_PointWidth = XmlUtil.Integer(xNodeLoop.InnerText);
						break;
					case "Palette":
						_Palette = ChartPalette.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "ThreeDProperties":
						_ThreeDProperties = new ThreeDProperties(r, this, xNodeLoop);
						break;
					case "PlotArea":
						_PlotArea = new PlotArea(r, this, xNodeLoop);
						break;
					case "ChartElementOutput":
						_ChartElementOutput = fyiReporting.RDL.ChartElementOutput.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:	
						if (DataRegionElement(xNodeLoop))	// try at DataRegion level
							break;
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Chart element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			DataRegionFinish();			// Tidy up the DataRegion

			if (_SeriesGroupings == null && _CategoryGroupings == null)
				OwnerReport.rl.LogError(8, "Chart requires either the SeriesGroupings element or CategoryGroupings element or both.");

            if (OwnerReport.rl.MaxSeverity > 4)     // if we already have severe error don't check for these additional issues
                return;

            // Do some specific checking based on the type of the Chart specified
            switch (_Type)
            {
                case ChartTypeEnum.Bubble:
                    if (_ChartData == null || 
                        _ChartData.Items[0].Datapoints.Items[0].DataValues.Items.Count != 3)
                        OwnerReport.rl.LogError(8, "Bubble charts require three DataPoints defined.");
                    break;
                case ChartTypeEnum.Scatter:
                    if (_ChartData == null ||
                        _ChartData.Items[0].Datapoints.Items[0].DataValues.Items.Count != 2)
                        OwnerReport.rl.LogError(8, "Scatter charts require two DataPoints defined.");
                    break;
                default:
                    break;
            }
            
        }

		override internal void FinalPass()
		{
			base.FinalPass();
			if (_SeriesGroupings != null)
				_SeriesGroupings.FinalPass();
			if (_CategoryGroupings != null)
				_CategoryGroupings.FinalPass();
			if (_ChartData != null)
				_ChartData.FinalPass();
			if (_Legend != null)
				_Legend.FinalPass();
			if (_CategoryAxis != null)
				_CategoryAxis.FinalPass();
			if (_ValueAxis != null)
				_ValueAxis.FinalPass();
			if (_Title != null)
				_Title.FinalPass();
			if (_ThreeDProperties != null)
				_ThreeDProperties.FinalPass();
			if (_PlotArea != null)
				_PlotArea.FinalPass();

			if (this.OwnerReport.rl.MaxSeverity < 8)	// Don't take this step if already have errors
			{
				_ChartMatrix = GenerateMatrix();		//   GenerateMatrix() needs no error in defn to date
				_ChartMatrix.FinalPass();
			}
			return;
		}

		override internal void Run(IPresent ip, Row row)
		{
			Report rpt = ip.Report();

			_ChartMatrix.RunReset(rpt);
			Rows _Data = GetFilteredData(ip.Report(), row);
			SetMyData(ip.Report(), _Data);

			if (!AnyRows(ip, _Data))		// if no rows, return
				return;

			// Build the Chart bitmap, along with data regions
			ChartBase cb=null;
			try
			{
				cb = RunChartBuild(rpt, row);

				ip.Chart(this, row, cb);
			}
			catch (Exception ex)
			{
				rpt.rl.LogError(8, string.Format("Exception in Chart handling.\n{0}\n{1}", ex.Message, ex.StackTrace));
			}
			finally
			{
				if (cb != null)
					cb.Dispose();
			}
			return;
		}

		override internal void RunPage(Pages pgs, Row row)
		{
			Report rpt = pgs.Report;

			if (IsHidden(pgs.Report, row))
				return;

			_ChartMatrix.RunReset(rpt);
			Rows _Data = GetFilteredData(rpt, row);
			SetMyData(rpt, _Data);

			SetPagePositionBegin(pgs);

			if (!AnyRowsPage(pgs, _Data))		// if no rows return
				return;						//   nothing left to do

			// Build the Chart bitmap, along with data regions
			Page p = pgs.CurrentPage;
			ChartBase cb=null;
			try
			{
				cb = RunChartBuild(rpt, row);					// Build the chart
				System.Drawing.Image im = cb.Image(rpt);	// Grab the image
				int height = im.Height;							// save height and width
				int width = im.Width;

				MemoryStream ostrm = new MemoryStream();
                /* The following is a new image saving logic which will allow for higher 
                 * quality images using JPEG with 100% quality
                 * 06122007AJM */
                System.Drawing.Imaging.ImageCodecInfo[] info;
                info = ImageCodecInfo.GetImageEncoders();
                EncoderParameters encoderParameters;
                encoderParameters = new EncoderParameters(1);
                // 20022008 AJM GJL - Centralised class with global encoder settings
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, ImageQualityManager.ChartImageQuality);
                System.Drawing.Imaging.ImageCodecInfo codec = null;
                for (int i = 0; i < info.Length; i++)
                {
                    if (info[i].FormatDescription == "JPEG")
                    {
                        codec = info[i];
                        break;
                    }
                }

                im.Save(ostrm, codec, encoderParameters);
                // 06122007AJM The follow has been replaced with the code above
				//im.Save(ostrm, info);	// generate a jpeg   TODO: get png to work with pdf

				byte[] ba = ostrm.ToArray();
				ostrm.Close();
				PageImage pi = new PageImage(IMAGEFORMAT, ba, width, height);	// Create an image
				
				RunPageRegionBegin(pgs);

				SetPagePositionAndStyle(rpt, pi, row);
				pi.SI.BackgroundImage = null;	// chart already has the background image

				if (pgs.CurrentPage.YOffset + pi.Y + pi.H >= pgs.BottomOfPage && !pgs.CurrentPage.IsEmpty())
				{	// force page break if it doesn't fit on the page
					pgs.NextOrNew();
					pgs.CurrentPage.YOffset = OwnerReport.TopOfPage;
					if (this.YParents != null)
						pi.Y = 0;
				}

				p = pgs.CurrentPage;
				
				p.AddObject(pi);	// Put image onto the current page

				RunPageRegionEnd(pgs);

				if (!this.PageBreakAtEnd && !IsTableOrMatrixCell(rpt))
				{
					float newY = pi.Y + pi.H;
					p.YOffset += newY;	// bump the y location
				}
                SetPagePositionEnd(pgs, pi.Y + pi.H);
            }
			catch (Exception ex)
			{
				rpt.rl.LogError(8, string.Format("Exception in Chart handling.\n{0}\n{1}", ex.Message, ex.StackTrace));
			}
			finally
			{
				if (cb != null)
					cb.Dispose();
			}

            return;
		}

		ChartBase RunChartBuild(Report rpt, Row row)
		{
			// Get the matrix that defines the data; 
			_ChartMatrix.SetMyData(rpt, GetMyData(rpt));	// set the data in the matrix
			int maxColumns;
			int maxRows;
			MatrixCellEntry[,] matrix = _ChartMatrix.RunBuild(rpt, out maxRows, out maxColumns);

			// Build the Chart bitmap, along with data regions
			ChartBase cb=null;
			switch (_Type)
			{
				case ChartTypeEnum.Column:
					cb = new ChartColumn(rpt, row, this, matrix);
					break;
				case ChartTypeEnum.Line:
				case ChartTypeEnum.Area:			// handled by line
					cb = new ChartLine(rpt, row, this, matrix);
					break;
				case ChartTypeEnum.Bar:
					cb = new ChartBar(rpt, row, this, matrix);
					break;
				case ChartTypeEnum.Pie:
				case ChartTypeEnum.Doughnut:		// handled by pie
					cb = new ChartPie(rpt, row, this, matrix);
					break;
                case ChartTypeEnum.Bubble:
                case ChartTypeEnum.Scatter:
                    cb = new ChartBubble(rpt, row, this, matrix);
                    break;
                case ChartTypeEnum.Stock:
				default:
					cb = new ChartColumn(rpt, row, this, matrix);
					break;
			}

			return cb;
		}

		// We generate a matrix so at runtime the matrix data engine
		//  will create the necessary summary data for the chart
		private Matrix GenerateMatrix()
		{
			XmlDocument mDoc = new XmlDocument();
			XmlElement m = mDoc.CreateElement("Matrix");
			mDoc.AppendChild(m);
			// Add in DataSetName if provided in Chart
			if (this.DataSetName != null)
			{
				XmlElement dsn = mDoc.CreateElement("DataSetName");
				dsn.InnerText = this.DataSetName;
				m.AppendChild(dsn);
			}

			/* MatrixColumns -- required but not really used for this case
			XmlElement mcols = mDoc.CreateElement("MatrixColumns");
			m.AppendChild(mcols);
			XmlElement mcol = mDoc.CreateElement("MatrixColumn");
			mcols.AppendChild(mcol);
			XmlElement w = mDoc.CreateElement("Width");
			w.InnerText = "1in";
			mcol.AppendChild(w); Moved this to below GJL */ 

			// ColumnGroupings -- cooresponds to SeriesGroupings
			GenerateMatrixSeries(mDoc, m);

			// RowGroupings -- corresponds to CategoryGroupings
			GenerateMatrixCategories(mDoc, m);

			// MatrixRows -- corresponds to ChartData
			XmlElement mrs = mDoc.CreateElement("MatrixRows"); // DataPoints
			m.AppendChild(mrs);
            XmlElement mr = mDoc.CreateElement("MatrixRow"); //DataPoint
			mrs.AppendChild(mr);
			XmlElement h = mDoc.CreateElement("Height");
			h.InnerText = "1in";				
            mr.AppendChild(h);
            XmlElement mcs = mDoc.CreateElement("MatrixCells"); //DataValues
			mr.AppendChild(mcs);//Moved this out of the loops GJL
			foreach (ChartSeries cs in this.ChartData.Items)
			{
				
                foreach (DataPoint dp in cs.Datapoints.Items)
				{
					
					XmlElement mc = mDoc.CreateElement("MatrixCell");  //DataValue
					mcs.AppendChild(mc);
					XmlElement mcris = mDoc.CreateElement("ReportItems");
					mc.AppendChild(mcris);
					XmlElement mcri = mDoc.CreateElement("ChartExpression");
					mcris.AppendChild(mcri);
                    
                    /* Added this so that the plot type can be seen when
                     * the chart it being drawn
                     * 05122007AJM */
                    XmlElement aPlotType = mDoc.CreateElement("PlotType");
                    aPlotType.InnerText = cs.PlotType.ToString();
                    mcri.AppendChild(aPlotType);

                    /*Custom Element for YAxis GJL 14022008*/
                    XmlElement aYaxis = mDoc.CreateElement("YAxis");
                    aYaxis.InnerText = cs.YAxis.ToString();
                    mcri.AppendChild(aYaxis);
                     

                    XmlElement dvl = mDoc.CreateElement("DataPoint");
                    dvl.InnerText = this.OwnerReport.CreateDynamicName(dp);
                    mcri.AppendChild(dvl);

                    XmlElement dvs = mDoc.CreateElement("DataValues");
                    mcri.AppendChild(dvs);
                    foreach (DataValue dv in dp.DataValues.Items)
                    {
                        XmlElement dvv = mDoc.CreateElement("DataValue");
                        dvs.AppendChild(dvv);
                        XmlElement dve = mDoc.CreateElement("Value");
                        dvv.AppendChild(dve);
                        dve.InnerText = dv.Value.Source;
					}
				}
			}

			Matrix mt = new Matrix(this.OwnerReport, this, m);
			return mt;	
		}

        // Handle simple case with single expression
        void CreateChartExpression(XmlDocument mDoc, XmlElement ce, string expr, Expression label)
        {
            XmlElement dvs = mDoc.CreateElement("DataValues");
            ce.AppendChild(dvs);

            XmlElement dvv = mDoc.CreateElement("DataValue");
            dvs.AppendChild(dvv);
            XmlElement dve = mDoc.CreateElement("Value");
            dvv.AppendChild(dve);
            dve.InnerText = expr;
            if (label == null)
                return;

            XmlElement lbl = mDoc.CreateElement("ChartLabel");
            ce.AppendChild(lbl);
            lbl.InnerText = label.Source;
        }

		void GenerateMatrixCategories(XmlDocument mDoc, XmlNode m)
		{
			XmlElement rgs = mDoc.CreateElement("RowGroupings");
			m.AppendChild(rgs);
			if (this.CategoryGroupings == null)
			{
				XmlElement rg = mDoc.CreateElement("RowGrouping");
				rgs.AppendChild(rg);
				XmlElement width = mDoc.CreateElement("Width");
				width.InnerText = "1in";
				rg.AppendChild(width);
				XmlElement dr = mDoc.CreateElement("DynamicRows");
				rg.AppendChild(dr);
				
				XmlElement drgrp = mDoc.CreateElement("Grouping");
				dr.AppendChild(drgrp);
				XmlElement drges = mDoc.CreateElement("GroupExpressions");
				drgrp.AppendChild(drges);
				XmlElement drris = mDoc.CreateElement("ReportItems");
				dr.AppendChild(drris);
				XmlElement drge = mDoc.CreateElement("GroupExpression");
				drges.AppendChild(drge);
				drge.InnerText = "";
				XmlElement drri = mDoc.CreateElement("ChartExpression");
				drris.AppendChild(drri);
                CreateChartExpression(mDoc, drri, "", null);
				return;
			}

			foreach (CategoryGrouping catGrp in this.CategoryGroupings.Items)
			{
				XmlElement rg = mDoc.CreateElement("RowGrouping");
				rgs.AppendChild(rg);
				XmlElement width = mDoc.CreateElement("Width");
				width.InnerText = "1in";
				rg.AppendChild(width);
				//Additional for static rows...not yet implemented
                if (catGrp.StaticCategories != null)
                {
                    XmlElement dr = mDoc.CreateElement("StaticRows");
                    rg.AppendChild(dr);

                }
                else
                {
                    XmlElement dr = mDoc.CreateElement("DynamicRows");
                    rg.AppendChild(dr);

                    XmlElement drgrp = mDoc.CreateElement("Grouping");
                    dr.AppendChild(drgrp);
                    XmlElement drges = mDoc.CreateElement("GroupExpressions");
                    drgrp.AppendChild(drges);
                    XmlElement drris = mDoc.CreateElement("ReportItems");
                    dr.AppendChild(drris);
                    foreach (GroupExpression ge in catGrp.DynamicCategories.Grouping.GroupExpressions.Items)
                    {
                        XmlElement drge = mDoc.CreateElement("GroupExpression");
                        drges.AppendChild(drge);
                        drge.InnerText = ge.Expression.Source;
                        XmlElement drri = mDoc.CreateElement("ChartExpression");
                        drris.AppendChild(drri);
                        CreateChartExpression(mDoc, drri, ge.Expression.Source, catGrp.DynamicCategories.Label);

                    }

                    if (catGrp.DynamicCategories.Sorting != null)
                    {
                        XmlElement drsrt = mDoc.CreateElement("Sorting");
                        dr.AppendChild(drsrt);
                        foreach (SortBy sb in catGrp.DynamicCategories.Sorting.Items)
                        {
                            XmlElement drsrtby = mDoc.CreateElement("SortBy");
                            drsrt.AppendChild(drsrtby);
                            XmlElement srtexp = mDoc.CreateElement("SortExpression");
                            srtexp.InnerText = sb.SortExpression.Source;
                            drsrtby.AppendChild(srtexp);
                            if (sb.Direction == SortDirectionEnum.Descending)
                            {
                                XmlElement srtdir = mDoc.CreateElement("Direction");
                                srtdir.InnerText = "Descending";
                                drsrtby.AppendChild(srtdir);
                            }
                        }
                    }
                }				
			}
		}
		
		void GenerateMatrixSeries(XmlDocument mDoc, XmlNode m)
		{
			XmlElement cgs = mDoc.CreateElement("ColumnGroupings");
			m.AppendChild(cgs);
			if (this.SeriesGroupings == null)
			{
				XmlElement cg = mDoc.CreateElement("ColumnGrouping");
				cgs.AppendChild(cg);
				XmlElement h = mDoc.CreateElement("Height");
				h.InnerText = "1in";
				cg.AppendChild(h);
				XmlElement dc = mDoc.CreateElement("DynamicColumns");
				cg.AppendChild(dc);
				
				XmlElement dcgrp = mDoc.CreateElement("Grouping");
				dc.AppendChild(dcgrp);
				XmlElement dcges = mDoc.CreateElement("GroupExpressions");
				dcgrp.AppendChild(dcges);
				XmlElement dcris = mDoc.CreateElement("ReportItems");
				dc.AppendChild(dcris);
				XmlElement dcge = mDoc.CreateElement("GroupExpression");
				dcges.AppendChild(dcge);
				dcge.InnerText = "";
				XmlElement dcri = mDoc.CreateElement("ChartExpression");
				dcris.AppendChild(dcri);
                CreateChartExpression(mDoc, dcri, "", null);

                XmlElement mcols = mDoc.CreateElement("MatrixColumns");
                m.AppendChild(mcols);
                XmlElement mcol = mDoc.CreateElement("MatrixColumn");
                mcols.AppendChild(mcol);
                XmlElement w = mDoc.CreateElement("Width");
                w.InnerText = "1in";
                mcol.AppendChild(w); //GJL

				return;
			}

			foreach (SeriesGrouping serGrp in this.SeriesGroupings.Items)
			{
                XmlElement cg = mDoc.CreateElement("ColumnGrouping");
                cgs.AppendChild(cg);
                XmlElement h = mDoc.CreateElement("Height");
                h.InnerText = "1in";
                cg.AppendChild(h);
				//GJL 041207
              
                XmlElement mcols = mDoc.CreateElement("MatrixColumns");
                m.AppendChild(mcols);
                if (serGrp.StaticSeries != null)
                {  
                    XmlElement sgs = mDoc.CreateElement("StaticColumns");
                    cg.AppendChild(sgs);
                    foreach (StaticMember sm in serGrp.StaticSeries.Items)
                    {
                        XmlElement sg = mDoc.CreateElement("StaticColumn");
                        sgs.AppendChild(sg);

                        XmlElement sgRI = mDoc.CreateElement("ReportItems");                        
                        sg.AppendChild(sgRI);
                        XmlElement sgRIe = mDoc.CreateElement("ChartExpression");
                        sgRI.AppendChild(sgRIe);
                        CreateChartExpression(mDoc, sgRIe, sm.Label.Source, sm.Label);

                   
                        XmlElement mcol = mDoc.CreateElement("MatrixColumn");
                        mcols.AppendChild(mcol);
                        XmlElement w = mDoc.CreateElement("Width");
                        w.InnerText = "1in";
                        mcol.AppendChild(w);

                    }
                }

                //end GJL
                else
                {                
                    XmlElement mcol = mDoc.CreateElement("MatrixColumn");
                    mcols.AppendChild(mcol);
                    XmlElement w = mDoc.CreateElement("Width");
                    w.InnerText = "1in";
                    mcol.AppendChild(w); //GJL
                    
                    XmlElement dc = mDoc.CreateElement("DynamicColumns");
                    cg.AppendChild(dc);

                    XmlElement dcgrp = mDoc.CreateElement("Grouping");
                    dc.AppendChild(dcgrp);
                    XmlElement dcges = mDoc.CreateElement("GroupExpressions");
                    dcgrp.AppendChild(dcges);
                    XmlElement dcris = mDoc.CreateElement("ReportItems");
                    dc.AppendChild(dcris);
                    if (serGrp.DynamicSeries != null)
                    {
                        foreach (GroupExpression ge in serGrp.DynamicSeries.Grouping.GroupExpressions.Items)
                        {
                            XmlElement dcge = mDoc.CreateElement("GroupExpression");
                            dcges.AppendChild(dcge);
                            dcge.InnerText = ge.Expression.Source;
                            XmlElement dcri = mDoc.CreateElement("ChartExpression");
                            dcris.AppendChild(dcri);
                            CreateChartExpression(mDoc, dcri, ge.Expression.Source, serGrp.DynamicSeries.Label);

                            if (serGrp.DynamicSeries.Sorting != null)
                            {
                                XmlElement dcsrt = mDoc.CreateElement("Sorting");
                                dc.AppendChild(dcsrt);
                                foreach (SortBy sb in serGrp.DynamicSeries.Sorting.Items)
                                {
                                    XmlElement dcsrtby = mDoc.CreateElement("SortBy");
                                    dcsrt.AppendChild(dcsrtby);
                                    XmlElement srtexp = mDoc.CreateElement("SortExpression");
                                    srtexp.InnerText = sb.SortExpression.Source;
                                    dcsrtby.AppendChild(srtexp);
                                    if (sb.Direction == SortDirectionEnum.Descending)
                                    {
                                        XmlElement srtdir = mDoc.CreateElement("Direction");
                                        srtdir.InnerText = "Descending";
                                        dcsrtby.AppendChild(srtdir);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        XmlElement dcge = mDoc.CreateElement("GroupExpression");
                        dcges.AppendChild(dcge);
                        dcge.InnerText = "=''";          // just put in constant expression
                        XmlElement dcri = mDoc.CreateElement("ChartExpression");
                        dcris.AppendChild(dcri);
                        CreateChartExpression(mDoc, dcri, "=''", null);
                    }
                }
             }
		}

		internal ChartTypeEnum Type
		{
			get { return  _Type; }
			set {  _Type = value; }
		}

		internal ChartSubTypeEnum Subtype
		{
			get { return  _Subtype; }
			set {  _Subtype = value; }
		}

		internal SeriesGroupings SeriesGroupings
		{
			get { return  _SeriesGroupings; }
			set {  _SeriesGroupings = value; }
		}

		internal CategoryGroupings CategoryGroupings
		{
			get { return  _CategoryGroupings; }
			set {  _CategoryGroupings = value; }
		}

		internal ChartData ChartData
		{
			get { return  _ChartData; }
			set {  _ChartData = value; }
		}

		internal Legend Legend
		{
			get { return  _Legend; }
			set {  _Legend = value; }
		}

		internal CategoryAxis CategoryAxis
		{
			get { return  _CategoryAxis; }
			set {  _CategoryAxis = value; }
		}

		internal ValueAxis ValueAxis
		{
			get { return  _ValueAxis; }
			set {  _ValueAxis = value; }
		}

		internal Title Title
		{
			get { return  _Title; }
			set {  _Title = value; }
		}

		internal int PointWidth
		{
			get { return  _PointWidth; }
			set {  _PointWidth = value; }
		}

		internal ChartPaletteEnum Palette
		{
			get { return  _Palette; }
			set {  _Palette = value; }
		}

		internal ThreeDProperties ThreeDProperties
		{
			get { return  _ThreeDProperties; }
			set {  _ThreeDProperties = value; }
		}

		internal PlotArea PlotArea
		{
			get { return  _PlotArea; }
			set {  _PlotArea = value; }
		}

		internal ChartElementOutputEnum ChartElementOutput
		{
			get { return  _ChartElementOutput; }
			set {  _ChartElementOutput = value; }
		}

		internal Matrix ChartMatrix
		{
			get { return _ChartMatrix; }
		}

		// Runtime data; either original query if no groups
		// or sorting or a copied version that is grouped/sorted
		private Rows GetMyData(Report rpt)
		{
			return rpt.Cache.Get(this, "data") as Rows;
		}

		private void SetMyData(Report rpt, Rows data)
		{
			if (data == null)
				rpt.Cache.Remove(this, "data");
			else
				rpt.Cache.AddReplace(this, "data", data);
		}
	}
}
