using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using EAS.Data.ORM;

namespace fyiReporting.RdlDesign
{
    internal class DesignerUtility2:DesignerUtility
    {
        /// <summary>
        /// 生成表列名称列表。
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        static internal ArrayList GetSqlColumns(EAS.Data.ORM.ITable table)
        { 
            ArrayList cols = new ArrayList();

            if (table != null)
            {
                System.Data.DataTable dataTable = DataTableConvert.ToDataTable(table);

                foreach (System.Data.DataColumn dc in dataTable.Columns)
                {
                    SqlColumn sc = new SqlColumn();
                    sc.Name = dc.ColumnName;
                    sc.DataType = dc.DataType;
                    cols.Add(sc);
                }
            }

            return cols;
        }

        /// <summary>
        /// 生成表列名称列表。
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        static internal ArrayList GetSqlColumns(BaseDataEntity DataEntity)
        {
            ArrayList cols = new ArrayList();

            if (DataEntity != null)
            {
                System.Data.DataTable dataTable = DataTableConvert.ToDataTable(DataEntity);

                foreach (System.Data.DataColumn dc in dataTable.Columns)
                {
                    SqlColumn sc = new SqlColumn();
                    sc.Name = dc.ColumnName;
                    sc.DataType = dc.DataType;
                    cols.Add(sc);
                }
            }

            return cols;
        }
    }
}
