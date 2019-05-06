using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using EAS.Data.ORM;

namespace fyiReporting.RdlDesign
{
    class DataTableConvert
    {
        #region ITable

        /// <summary>
        /// 创建表结构。
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        static System.Data.DataTable CreateTable(ITable table)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            ColumnCollection colums = table.Columns;

            foreach (Column col in colums)
            {
                System.Type type = typeof(string);
                switch (col.DataType)
                {
                    case System.Data.DbType.AnsiString:
                    case System.Data.DbType.AnsiStringFixedLength:
                    case System.Data.DbType.String:
                    case System.Data.DbType.StringFixedLength:
                    case System.Data.DbType.Xml:
                        type = typeof(string);
                        break;
                    case System.Data.DbType.Binary:
                        type = typeof(byte[]);
                        break;
                    case System.Data.DbType.Boolean:
                        type = typeof(bool);
                        break;
                    case System.Data.DbType.Byte:
                        type = typeof(byte);
                        break;
                    case System.Data.DbType.Int16:
                        type = typeof(short);
                        break;
                    case System.Data.DbType.Int32:
                        type = typeof(int);
                        break;
                    case System.Data.DbType.Int64:
                        type = typeof(long);
                        break;
                    case System.Data.DbType.UInt16:
                        type = typeof(ushort);
                        break;
                    case System.Data.DbType.UInt32:
                        type = typeof(uint);
                        break;
                    case System.Data.DbType.UInt64:
                        type = typeof(ulong);
                        break;
                    case System.Data.DbType.Single:
                        type = typeof(float);
                        break;
                    case System.Data.DbType.Currency:
                    case System.Data.DbType.Decimal:
                        type = typeof(decimal);
                        break;
                    case System.Data.DbType.Double:
                        type = typeof(double);
                        break;
                    case System.Data.DbType.Date:
                    case System.Data.DbType.DateTime:
                    case System.Data.DbType.Time:
                        type = typeof(DateTime);
                        break;
                    case System.Data.DbType.Guid:
                        type = typeof(Guid);
                        break;
                    case System.Data.DbType.Object:
                    case System.Data.DbType.VarNumeric:
                        type = typeof(System.Object);
                        break;
                    default:
                        type = typeof(System.Object);
                        break;
                }

                dataTable.Columns.Add(new System.Data.DataColumn(col.Name, type));
            }
            return dataTable;
        }

        static DataRow CreateNow(DataTable dataTable, IEntity row)
        {
            System.Data.DataRow dataRow = dataTable.NewRow();
            for (ushort k = 0; k < row.Columns.Count; k++)
            {
                Column m_column = row.Columns[k];
                if (row.GetValue(m_column) == null)
                {
                    System.Data.DbType dbType = row.Columns[k].DataType;

                    switch (dbType)
                    {
                        case System.Data.DbType.Int16:
                        case System.Data.DbType.Int32:
                        case System.Data.DbType.Int64:
                        case System.Data.DbType.UInt16:
                        case System.Data.DbType.UInt32:
                        case System.Data.DbType.UInt64:
                        case System.Data.DbType.Decimal:
                        case System.Data.DbType.Single:
                        case System.Data.DbType.Double:
                            dataRow[k] = 0;
                            break;
                        default:
                            dataRow[k] = System.DBNull.Value;
                            break;
                    }
                }
                else
                {
                    dataRow[k] = row[m_column];
                }
            }

            return dataRow;
        }

        /// <summary>
        /// 根据表对象生成数据表。
        /// </summary>
        /// <param name="table">数据表对象。</param>
        /// <returns>数据表。</returns>
        internal static System.Data.DataTable ToDataTable(ITable table)
        {
            System.Data.DataTable dataTable = CreateTable(table);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                System.Data.DataRow row = CreateNow(dataTable, table.Rows[i]);
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        #endregion

        #region 随机200条

        /// <summary>
        /// 动态生成记录。
        /// </summary>
        /// <param name="table"></param>
        static void InsertData(ITable table)
        {
            IEntity dataObject = null;
            int cols = table.Rows.Count;
            int rows = 200;

            ColumnCollection columns = table.Columns;
            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                dataObject = System.Activator.CreateInstance(table.EntityType) as IEntity;

                foreach (Column m_Column in columns)
                {
                    switch (m_Column.DataType)
                    {
                        case DbType.Byte:
                            dataObject[m_Column] = (Byte)random.Next(256);
                            break;
                        case DbType.Int16:
                        case DbType.UInt16:
                            dataObject[m_Column] = (UInt16)random.Next(1000);
                            break;
                        case DbType.Int32:
                        case DbType.UInt32:
                            dataObject[m_Column] = random.Next(10000);
                            break;
                        case DbType.Int64:                        
                        case DbType.UInt64:
                            dataObject[m_Column] = random.Next(100000);
                            break;
                        case DbType.Decimal:
                        case DbType.Currency:
                            dataObject[m_Column] = (decimal)(random.NextDouble()* 1000);
                            break;
                        case DbType.Single:
                        case DbType.Double:
                            dataObject[m_Column] = random.NextDouble() * 1000;
                            break;
                        case DbType.Date:
                        case DbType.DateTime:
                        case DbType.Time:
                            dataObject[m_Column] = DateTime.Now;
                            break;
                        case DbType.Guid:
                            dataObject[m_Column] = Guid.Empty;
                            break;
                        case DbType.AnsiString:
                        case DbType.String:
                            dataObject[m_Column] = GenerateCheckCode(10);
                            break;                            
                        default:
                            dataObject[m_Column] = System.DBNull.Value;
                            break;
                    }
                }

                table.Rows.Add(dataObject);
            }
        }

        /// <summary>
        /// 动态生成记录
        /// </summary>
        /// <param name="DataEntity"></param>
        /// <returns></returns>
        static List<BaseDataEntity> InsertData(BaseDataEntity DataEntity)
        {
            int rows = 200;
            List<BaseDataEntity> dataList = new List<BaseDataEntity>(rows);
            ColumnCollection columns = DataEntity.GetColumns();

            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                BaseDataEntity dataObject = System.Activator.CreateInstance(DataEntity.GetType()) as BaseDataEntity;

                foreach (Column m_Column in columns)
                {
                    switch (m_Column.DataType)
                    {
                        case DbType.Byte:
                            dataObject[m_Column] = (Byte)random.Next(256);
                            break;
                        case DbType.Int16:
                        case DbType.UInt16:
                            dataObject[m_Column] = (UInt16)random.Next(1000);
                            break;
                        case DbType.Int32:
                        case DbType.UInt32:
                            dataObject[m_Column] = random.Next(10000);
                            break;
                        case DbType.Int64:
                        case DbType.UInt64:
                            dataObject[m_Column] = random.Next(100000);
                            break;
                        case DbType.Decimal:
                        case DbType.Currency:
                            dataObject[m_Column] = (decimal)(random.NextDouble() * 1000);
                            break;
                        case DbType.Single:
                        case DbType.Double:
                            dataObject[m_Column] = random.NextDouble() * 1000;
                            break;
                        case DbType.Date:
                        case DbType.DateTime:
                        case DbType.Time:
                            dataObject[m_Column] = DateTime.Now;
                            break;
                        case DbType.Guid:
                            dataObject[m_Column] = Guid.Empty;
                            break;
                        case DbType.AnsiString:
                        case DbType.String:
                            dataObject[m_Column] = GenerateCheckCode(10);
                            break;
                        default:
                            dataObject[m_Column] = System.DBNull.Value;
                            break;
                    }
                }
                dataList.Add(dataObject);
            }
            return dataList;
        }

        static string GenerateCheckCode(int codeCount)
        {
            int rep=0;

            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }

        #endregion

        #region IDataEntity

        /// <summary>
        /// 生成结构。
        /// </summary>
        /// <param name="DataEntity"></param>
        /// <returns></returns>
        static System.Data.DataTable CreateTable(BaseDataEntity DataEntity)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();

            ColumnCollection columns = DataEntity.GetColumns();
            foreach (Column col in columns)
            {
                System.Type type = typeof(string);

                if (col.Property != null)
                {
                    type = col.Property.PropertyType;
                }
                else
                {
                    switch (col.DataType)
                    {
                        case System.Data.DbType.AnsiString:
                        case System.Data.DbType.AnsiStringFixedLength:
                        case System.Data.DbType.String:
                        case System.Data.DbType.StringFixedLength:
                        case System.Data.DbType.Xml:
                            type = typeof(string);
                            break;
                        case System.Data.DbType.Binary:
                            type = typeof(byte[]);
                            break;
                        case System.Data.DbType.Boolean:
                            type = typeof(bool);
                            break;
                        case System.Data.DbType.Byte:
                            type = typeof(byte);
                            break;
                        case System.Data.DbType.Int16:
                            type = typeof(short);
                            break;
                        case System.Data.DbType.Int32:
                            type = typeof(int);
                            break;
                        case System.Data.DbType.Int64:
                            type = typeof(long);
                            break;
                        case System.Data.DbType.UInt16:
                            type = typeof(ushort);
                            break;
                        case System.Data.DbType.UInt32:
                            type = typeof(uint);
                            break;
                        case System.Data.DbType.UInt64:
                            type = typeof(ulong);
                            break;
                        case System.Data.DbType.Single:
                            type = typeof(float);
                            break;
                        case System.Data.DbType.Currency:
                        case System.Data.DbType.Decimal:
                            type = typeof(decimal);
                            break;
                        case System.Data.DbType.Double:
                            type = typeof(double);
                            break;
                        case System.Data.DbType.Date:
                        case System.Data.DbType.DateTime:
                        case System.Data.DbType.Time:
                            type = typeof(DateTime);
                            break;
                        case System.Data.DbType.Guid:
                            type = typeof(Guid);
                            break;
                        case System.Data.DbType.Object:
                        case System.Data.DbType.VarNumeric:
                            type = typeof(System.Object);
                            break;
                        default:
                            type = typeof(System.Object);
                            break;
                    }
                }
                dataTable.Columns.Add(new System.Data.DataColumn(col.Name, type));
            }
            return dataTable;
        }

        static DataRow CreateNow(DataTable dataTable, BaseDataEntity DataEntity)
        {
            System.Data.DataRow dataRow = dataTable.NewRow();
            ColumnCollection columns = DataEntity.GetColumns();
            for (ushort k = 0; k < columns.Count; k++)
            {
                Column m_Column = DataEntity.GetColumns()[k];
                if (m_Column.Property != null)  //新格式
                {
                    dataRow[k] = m_Column.Property.GetValue(DataEntity, new object[] { });
                }
                else
                {
                    #region 旧格式

                    if (DataEntity[m_Column] == null)
                    {
                        System.Data.DbType dbType = m_Column.DataType;

                        switch (dbType)
                        {
                            case System.Data.DbType.Int16:
                            case System.Data.DbType.Int32:
                            case System.Data.DbType.Int64:
                            case System.Data.DbType.UInt16:
                            case System.Data.DbType.UInt32:
                            case System.Data.DbType.UInt64:
                            case System.Data.DbType.Decimal:
                            case System.Data.DbType.Single:
                            case System.Data.DbType.Double:
                                dataRow[k] = 0;
                                break;
                            default:
                                dataRow[k] = System.DBNull.Value;
                                break;
                        }
                    }
                    else
                    {
                        dataRow[k] = DataEntity[m_Column];
                    }

                    #endregion
                }
            }

            return dataRow;
        }

        /// <summary>
        /// 根据实体记录生成数据表。
        /// </summary>
        /// <param name="row">实体行。</param>
        /// <returns>数据表。</returns>
        internal static DataTable ToDataTable(BaseDataEntity DataEntity)
        {
            System.Data.DataTable dataTable = CreateTable(DataEntity);
            dataTable.Rows.Add(CreateNow(dataTable, DataEntity));
            return dataTable;
        }

        /// <summary>
        /// 有用/有数据。
        /// </summary>
        /// <param name="DataEntity"></param>
        /// <returns></returns>
        internal static System.Data.DataTable ToDataTable2(BaseDataEntity DataEntity)
        {
            List<BaseDataEntity> dataList = InsertData(DataEntity);
            System.Data.DataTable dataTable = CreateTable(DataEntity);

            foreach (BaseDataEntity dataObject in dataList)
            {
                dataTable.Rows.Add(CreateNow(dataTable, dataObject));
            }

            return dataTable;
        }

        #endregion        
    }    
}
