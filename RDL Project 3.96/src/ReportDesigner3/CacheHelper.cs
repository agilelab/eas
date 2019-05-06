using System;
using System.Collections.Generic;
using System.Text;

namespace fyiReporting.RdlDesign
{
    class CacheHelper
    {
        /// <summary>
        /// 静态私有成员，缓存帮助者的唯一实例。
        /// </summary>
        private static CacheHelper instance;

        private PrintPage[] pages;

        #region RDL Template

        private readonly string _Schema2003 =
"xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2003/10/reportdefinition\" xmlns:rd=\"http://schemas.microsoft.com/SQLServer/reporting/reportdesigner\"";
        private readonly string _Schema2005 =
"xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition\" xmlns:rd=\"http://schemas.microsoft.com/SQLServer/reporting/reportdesigner\"";

        private string _TemplateChart = " some junk";
        private string _TemplateMatrix = " some junk";
        private string _TemplateTable = @"<?xml version='1.0' encoding='UTF-8'?>
<Report |schema| > 
	<Description>|description|</Description>
	<Author>|author|</Author>
	|orientation|
	<DataSources>
		<DataSource Name='DS1'>
			|connectionproperties|
		</DataSource>
	</DataSources>
	<Width>7.5in</Width>
	<TopMargin>.25in</TopMargin>
	<LeftMargin>.25in</LeftMargin>
	<RightMargin>.25in</RightMargin>
	<BottomMargin>.25in</BottomMargin>
	|reportparameters|
	<DataSets>
		<DataSet Name='Data'>
			<Query>
				<DataSourceName>DS1</DataSourceName>
				<CommandText>|sqltext|</CommandText>
				|queryparameters|
			</Query>
			<Fields>
			|sqlfields|
			</Fields>
		</DataSet>
	</DataSets>
|ifdef reportname|
	<PageHeader>
		<Height>.5in</Height>
		<ReportItems>
			<Textbox><Top>.1in</Top><Left>.1in</Left><Width>6in</Width><Height>.25in</Height><Value>|reportnameasis|</Value><Style><FontSize>15pt</FontSize><FontWeight>Bold</FontWeight></Style></Textbox> 
		</ReportItems>
	</PageHeader>
|endif|
	<Body>
		<ReportItems>
			<Table>
				<DataSetName>Data</DataSetName>
				<NoRows>Query returned no rows!</NoRows>
				<Style><BorderStyle><Default>Solid</Default></BorderStyle></Style>
				<TableColumns>
					|tablecolumns|
				</TableColumns>
				<Header>
					<TableRows>
						<TableRow>
							<Height>12 pt</Height>
							<TableCells>|tableheaders|</TableCells>
						</TableRow>
					</TableRows>
					<RepeatOnNewPage>true</RepeatOnNewPage>
				</Header>
|ifdef grouping|
				<TableGroups>
				<TableGroup>
				<Header>
					<TableRows>
						<TableRow>
							<Height>12 pt</Height>
							<TableCells>
								<TableCell>
									<ColSpan>|columncount|</ColSpan>
									<ReportItems><Textbox><Value>=Fields.|groupbycolumn|.Value</Value><Style><PaddingLeft>2 pt</PaddingLeft><BorderStyle><Default>Solid</Default></BorderStyle><FontWeight>Bold</FontWeight></Style></Textbox></ReportItems>
								</TableCell>
							</TableCells>
						</TableRow>
					</TableRows>
					<RepeatOnNewPage>true</RepeatOnNewPage>
				</Header>
				<Footer>
					<TableRows>
						<TableRow>
							<Height>12 pt</Height>
							<TableCells>|gtablefooters|</TableCells>
						</TableRow>
					</TableRows>
				</Footer>
		<Grouping Name='|groupbycolumn|Group'><GroupExpressions><GroupExpression>=Fields!|groupbycolumn|.Value</GroupExpression></GroupExpressions></Grouping>
		</TableGroup>
		</TableGroups>
|endif|
				<Details>
					<TableRows>
						<TableRow>
							<Height>12 pt</Height>
							<TableCells>|tablevalues|</TableCells>
						</TableRow>
					</TableRows>
				</Details>
|ifdef footers|
				<Footer>
					<TableRows>
						<TableRow>
							<Height>12 pt</Height>
							<TableCells>|tablefooters|</TableCells>
						</TableRow>
					</TableRows>
				</Footer>
|endif|
			</Table>
		</ReportItems>
		<Height>|bodyheight|</Height>
	</Body>
	<PageFooter>
		<Height>14 pt</Height>
		<ReportItems>
			<Textbox><Top>1 pt</Top><Left>10 pt</Left><Height>12 pt</Height><Width>3in</Width>
				<Value>=Globals!PageNumber.Value + ' of ' + Globals!TotalPages.Value</Value>
				<Style><FontSize>10pt</FontSize><FontWeight>Normal</FontWeight></Style>
			</Textbox> 	
		</ReportItems>
	</PageFooter>
</Report>";

        private string _TemplateList = @"<?xml version='1.0' encoding='UTF-8'?>
<Report |schema| > 
	<Description>|description|</Description>
	<Author>|author|</Author>
	|orientation|
	<DataSources>
		<DataSource Name='DS1'>
			|connectionproperties|
		</DataSource>
	</DataSources>
	<Width>7.5in</Width>
	<TopMargin>.25in</TopMargin>
	<LeftMargin>.25in</LeftMargin>
	<RightMargin>.25in</RightMargin>
	<BottomMargin>.25in</BottomMargin>
	|reportparameters|
	<DataSets>
		<DataSet Name='Data'>
			<Query>
				<DataSourceName>DS1</DataSourceName>
				<CommandText>|sqltext|</CommandText>
				|queryparameters|
			</Query>
			<Fields>
				|sqlfields|
			</Fields>
		</DataSet>
	</DataSets>
	<PageHeader>
		<Height>.5in</Height>
		<ReportItems>
|ifdef reportname|
			<Textbox><Top>.02in</Top><Left>.1in</Left><Width>6in</Width><Height>.25in</Height><Value>|reportname|</Value><Style><FontSize>15pt</FontSize><FontWeight>Bold</FontWeight></Style></Textbox> 
|endif|
			|listheaders|
		</ReportItems>
	</PageHeader>
	<Body><Height>25 pt</Height>
		<ReportItems>
			<List>
				<DataSetName>Data</DataSetName>
				<Height>24 pt</Height>
				<NoRows>Query returned no rows!</NoRows>
				<ReportItems>
					|listvalues|
				</ReportItems>
				<Width>|listwidth|</Width>
			</List>
		</ReportItems>
		</Body>
	<PageFooter>
		<Height>14 pt</Height>
		<ReportItems>
			<Textbox><Top>1 pt</Top><Left>10 pt</Left><Height>12 pt</Height><Width>3in</Width>
				<Value>=Globals!PageNumber.Value + ' of ' + Globals!TotalPages.Value</Value>
				<Style><FontSize>10pt</FontSize><FontWeight>Normal</FontWeight></Style>
			</Textbox> 	
		</ReportItems>
	</PageFooter>
		</Report>";

        #endregion

        internal CacheHelper()
        {
            this.pages = new PrintPage[26];

            //A制(公制)
            this.pages[0] = new PrintPage("A0", 841M, 1189M);
            this.pages[1] = new PrintPage("A1", 594M, 841M);
            this.pages[2] = new PrintPage("A2", 420M, 594M);
            this.pages[3] = new PrintPage("A3", 297M, 420M);
            this.pages[4] = new PrintPage("A4", 210M, 297M);
            this.pages[5] = new PrintPage("A5", 148M, 210M);
            this.pages[6] = new PrintPage("A6", 105M, 148M);
            this.pages[7] = new PrintPage("A7", 74M, 105M);
            this.pages[8] = new PrintPage("A8", 52M, 74M);

            //B制(日制)
            this.pages[9] = new PrintPage("B0", 1030M, 1456M);
            this.pages[10] = new PrintPage("B1", 728M, 1030M);
            this.pages[11] = new PrintPage("B2", 515M, 728M);
            this.pages[12] = new PrintPage("B3", 364M, 515M);
            this.pages[13] = new PrintPage("B4", 257M, 364M);
            this.pages[14] = new PrintPage("B5", 182M, 257M);
            this.pages[15] = new PrintPage("B6", 128M, 182M);
            this.pages[16] = new PrintPage("B7", 91M, 128M);
            this.pages[17] = new PrintPage("B8", 64M, 91M);

            //L度(美制)
            this.pages[18] = new PrintPage("LGA", 216M, 356M);
            this.pages[19] = new PrintPage("L/S", 216M, 278M);

            //信封尺寸
            this.pages[20] = new PrintPage("信封DL", 110M, 220M);
            this.pages[21] = new PrintPage("C信封", 229M, 324M);
            this.pages[22] = new PrintPage("信封C5", 162M, 229M);
            this.pages[23] = new PrintPage("信封C6", 114M, 162M);
            this.pages[24] = new PrintPage("信封C7/6", 81M, 162M);

            //算定义
            this.pages[25] = new PrintPage("自定义", 0M, 0M);

        }

        public static CacheHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new CacheHelper();

                return instance;
            }
        }

        public PrintPage[] Pages
        {
            get
            {
                return this.pages;
            }
        }

        public string Schema2003
        {
            get
            {
                return this._Schema2003;
            }
        }

        public string Schema2005
        {
            get
            {
                return this._Schema2005;
            }
        }

        public string TemplateChart
        {
            get
            {
                return this._TemplateChart;
            }
        }

        public string TemplateMatrix
        {
            get
            {
                return this._TemplateMatrix;
            }
        }

        public string TemplateTable
        {
            get
            {
                return this._TemplateTable;
            }
        }

        public string TemplateList
        {
            get
            {
                return this._TemplateList;
            }
        }        
    }
}
