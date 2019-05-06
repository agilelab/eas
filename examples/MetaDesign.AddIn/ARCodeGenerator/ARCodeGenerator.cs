using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Design;
using System.IO;

namespace EAS.MetaDesign.CodeAddIn.ActiveRecord
{
    public class ARCodeGenerator : ICodeGenerator
    {
        #region ICodeGenerator 成员

        /// <summary>
        /// 生成枚举对象代码。
        /// </summary>
        /// <param name="enumeration">枚举对象。</param>
        /// <param name="project">工作项目信息。</param>
        /// <returns>生成代码结果。</returns>
        /// 
        public string GeneratCode(Enumeration enumeration, Project project)
        {
            StringBuilder sb = new StringBuilder();
            string Name = enumeration.Name;
            if (string.IsNullOrEmpty(Name.Trim()))
                Name = "EnumName";
            sb.Append(
                "\t\t/// <summary>"+
                "\n\t\t///" + enumeration.Caption +
                "\n\t\t///</summary>"+
                "\n\t\tpublic enum " + Name + "\n\t\t{\n");

            for (int aa = 0; aa < enumeration.Items.Count; aa++)
            {
                if (aa == enumeration.Items.Count - 1)
                {
                    if (enumeration.Items[aa].Value != 0)
                        sb.Append("\t\t\t" + enumeration.Items[aa].Name + "=" + enumeration.Items[aa].Value + "\n");
                    else
                    {
                        sb.Append("\t\t\t" + enumeration.Items[aa].Name + "\n");
                    }
                    break;
                }

                if (enumeration.Items[aa].Value == 0)
                    sb.Append("\t\t\t" + enumeration.Items[aa].Name + "," + "\n");
                else
                {
                    sb.Append("\t\t\t" + enumeration.Items[aa].Name + "=" + enumeration.Items[aa].Value + "," + "\n");
                }

            }
            sb.Append("\t\t}");
            return sb.ToString();
        }

        /// <summary>
        /// 生成数据表对象及表列信息生成代码。
        /// </summary>
        /// <param name="rootEntity">表信息。</param>
        /// <param name="project">工作项目信息。</param>
        /// <returns>生成代码结果。</returns>
        public string GeneratCode(RootEntity rootEntity, Project project)
        {
            StringBuilder mySbr = new StringBuilder();
            //添加using
            mySbr.Append("#region Using\n" +
                "using System; \n" +
                "using System.Collections.Generic;\n" +
                "using System.Linq;\n" +
                "using System.Text;\n\n" +
                "using Castle.ActiveRecord.Framework;\n" +
                "using Castle.ActiveRecord.Queries;\n" +
                "using Castle.ActiveRecord;\n\n" +
                "using Common.Logging;\n\n"+
                "using IBeam.MDAA;\n" +
                "using IBeam.MDAA.Utility;\n" +
                "using IBeam.MDAA.ORMObjects;\n" 
                +"\n#endregion\n");

            string namespaces = "namespace";
            string className = "className";
            string primaryKey = "";

            //判断命名空间与类名时候为空
            if (!string.IsNullOrEmpty(project.Namespace.Trim()))
            {
                namespaces = project.Namespace;
            }
            if (!string.IsNullOrEmpty(rootEntity.ClassName.Trim()))
            {
                className = rootEntity.ClassName;
            }
            //添加类声明与注释
            mySbr.Append("\nnamespace " + namespaces +".ORMObjects"+
                "\n{\n" +
                 "\t/// <summary> \n\t/// " + rootEntity.Caption + " \n\t/// </summary> \n" +
                "\t[Serializable]\n" +
                "\t[ActiveRecord(\"" + rootEntity.MapTable + "\", Cache = CacheEnum.ReadWrite)] \n" +
                "\tpublic class " + "DAL" + className + " : ActiveRecordBase<" + "DAL" + className + ">, ILocalCache \n    {\n");
            
            //添加构造函数
            mySbr.Append("\t\t#region 构造函数 \n" +
            "\t\tpublic DAL" + rootEntity.ClassName + "():\n\t\t base()\n\t\t{  }\n");
            mySbr.Append("\t\t#endregion\n\n");

            mySbr.Append("\t\t#region 公有属性 \n\n");
            
            //判断拼音
            bool hasName = false, hasPinyin = false;
            String NameStr="",PinyinStr="";
            for (int i = 0; i < rootEntity.Properties.Count; i++)
            {
                if (rootEntity.Properties[i].Name.Equals("Name"))
                {
                    hasName = true;
                    NameStr=rootEntity.Properties[i].Caption;
                }
                if (rootEntity.Properties[i].Name.Equals("NamePinyin"))
                { 
                    hasPinyin = true;
                    PinyinStr = rootEntity.Properties[i].Caption;
                }
            }
            String PingyingStr = "";
            //拼音名字都存在哟
            if (hasName && hasPinyin)
            {
                PingyingStr="\n\t\t#region "+NameStr+
                "\n\t\t/// <summary>"+
                "\n\t\t/// "+NameStr+
                "\n\t\t/// </summary>"+
                "\n\t\tprivate string _Name;"+
                "\n\t\t[Property(Length = ConstSetting.ShortVarchar)]"+
                "\n\t\tpublic string Name"+
                "\n\t\t{"+
                "\n\t\t\tget { return _Name; }"+
                "\n\t\t\tset"+
                "\n\t\t\t{" +
                "\n\t\t\t\t_Name = value;" +
                "\n\t\t\t\tif (string.IsNullOrEmpty(_Name))" +
                "\n\t\t\t\t{" +
                "\n\t\t\t\t\t_Pinyin = string.Empty;" +
                "\n\t\t\t\t}" +
                "\n\t\t\t\telse" +
                "\n\t\t\t\t{" +
                "\n\t\t\t\t\t_Pinyin = Chinese2Spell.GetHeadOfChs(_Name);" +
                "\n\t\t\t\t}" +
                "\n\t\t\t}" +
                "\n\t\t}"+
                "\n\t\t#endregion\n"+
                "\n\t\t#region "+PinyinStr+
                "\n\t\tprivate string _Pinyin;"+
                "\n\t\t/// <summary>"+
                "\n\t\t/// "+PinyinStr+
                "\n\t\t/// </summary>"+
                "\n\t\t[Property(Length = ConstSetting.ShortVarchar)]"+
                "\n\t\tpublic string NamePinyin"+
                "\n\t\t{"+
                "\n\t\t\tget { return _Pinyin; }"+
                "\n\t\tset"+
                "\n\t\t{"+
                "\n\t\t_Pinyin = value;"+
                "\n\t\t}"+
                "\n\t\t\t}"+
                "\n\t\t #endregion\n";
            }

            for (int i = 0; i < rootEntity.Properties.Count; i++)
            {
                //不好提前删,在此判断拼音是否存在
                if (hasName && hasPinyin)
                {
                    if (rootEntity.Properties[i].ColumnName.Trim().Equals("Name") || rootEntity.Properties[i].ColumnName.Trim().Equals("NamePinyin"))
                    {
                        continue;
                    }
                }
                
                //当是主键的时候添加主键特有
                if (rootEntity.Properties[i].PrimaryKey)
                {
                    primaryKey = rootEntity.Properties[i].Name;
                    mySbr.Append("\t\t#region " + rootEntity.Properties[i].Caption + "\n");
                    string PrimaryKeyStr = "[PrimaryKey(PrimaryKeyType.Identity, \"" + rootEntity.Properties[i].Name + "\")]";
                    mySbr.Append("\t\t/// <summary> \n\t\t/// " + rootEntity.Properties[i].Caption + " \n\t\t/// </summary> \n\t\t" + PrimaryKeyStr + " \n ");
                    mySbr.Append("\t\tpublic " + rootEntity.Properties[i].Type + " " + rootEntity.Properties[i].Name + "\n\t\t{ get;set; } \n");
                    mySbr.Append("\t\t#endregion \n");

                    //在主键后面添加拼音,如果存在的话
                    mySbr.Append(PingyingStr);
                }
                else
                {
                    //非主键
                    mySbr.Append("\t\t#region " + rootEntity.Properties[i].Caption + "\n");

                    mySbr.Append("\t\t/// <summary> \n\t\t/// " + rootEntity.Properties[i].Caption + " \n\t\t/// </summary> \n ");
                    string length = "";
                    //当类型为string 判断长度,转换字符串,
                    if (rootEntity.Properties[i].Type.Equals("string"))
                    {
                        int size = rootEntity.Properties[i].Size;
                        if (size > 0 && size <= 10)
                            length += "Length = ConstSetting.VeryShort";
                        else if (size > 10 && size <= 40)
                            length += "Length = ConstSetting.ShortVarchar";
                        else if (size > 40 && size <= 255)
                            length += "Length = ConstSetting.LongVarchar";
                        else if (size > 255 && size <= 2000)
                            length += "Length = ConstSetting.VeryLong";
                        else if (size > 2000 && size <= 10000)
                            length += "Length = ConstSetting.VeryVeryLong";
                        else if (size > 10000 && size <= 100000)
                            length += "Length = ConstSetting.MostLong";
                    }
                    //如果为不同的表名列名
                    if (rootEntity.Properties[i].ColumnName.Trim().Equals(rootEntity.Properties[i].Name))
                    {
                        mySbr.Append("\t\t[Property(" + length + ")] \n");
                    }
                    else
                    {
                        mySbr.Append("\t\t[Property(\"" + rootEntity.Properties[i].ColumnName + "\""+(length==""?"":","+length)+")] \n");
                    }

                    mySbr.Append("\t\tpublic " + rootEntity.Properties[i].Type + " " + rootEntity.Properties[i].Name + "\n\t\t{ get;set; } \n");
                    mySbr.Append("\t\t#endregion \n\n");

                }

            }
            mySbr.Append("\t\t#endregion \n");
            ///------------------------------------------------------------------------
            ///


            ////把命名空间和类名做处理
            //string [] namespaceArr=namespaces.Split(',');
            //namespaces=namespaces.Replace("ORMObjects", "Objects");
            //if (className.StartsWith("DAL"))
            //    className = className.Replace("DAL", "");


            //判断主键
            string PrimaryKeyTemp = primaryKey;
            if (!string.IsNullOrEmpty(primaryKey))
            {
                primaryKey = "\t\t\tchain.Add(LocalCache.GetCacheKey(\"" + namespaces + "." + className + "\", \"" + primaryKey + "\", this." + primaryKey + "));\n";
            }

            //添加缓存,如果非主键,缓存只有一句话
            mySbr.Append(
            "\n\t\t#region 本地缓存\n" +
            "\t\t/// <summary>\n" +
           "\t\t/// 取得当前对象的缓存变更检查Key\n" +
           "\t\t/// </summary>\n" +
           "\t\t/// <returns>缓存变更检查Key</returns>\n" +
           "\t\tpublic virtual IEnumerable<string> GetCurrentCacheKey()\n" +
           "\t\t{\n" +
           "\t\t\tList<string> chain = new List<string>();\n" +
           "\t\t\tchain.Add(LocalCache.GetCacheKey(\"" + namespaces + "." + className + "\"));\n" +
           primaryKey +
           "\t\t\treturn chain;\n" +
           "\t\t}\n" +
           "\t\t/// <summary>\n" +
           "\t\t/// 取得当前对象在类继承结构中的缓存变更检查Key\n" +
           "\t\t/// </summary>\n" +
           "\t\t/// <returns>缓存变更检查Key</returns>\n" +
           "\t\tpublic virtual IEnumerable<string> GetCacheKeyOfChain()\n" +
           "\t\t{\n" +
           "\t\t\treturn GetCurrentCacheKey();\n" +
           "\t\t}\n" +
           "\t\t/// <summary>\n" +
           "\t\t/// 取得当前对象的相关对象缓存变更检查Key\n" +
           "\t\t/// </summary>\n" +
           "\t\t/// <returns>相关对象缓存变更检查Key</returns>\n" +
           "\t\tpublic virtual IEnumerable<string> GetCacheKeyOfRelativeChain()\n" +
           "\t\t{\n" +
           "\t\t\tList<string> chain = new List<string>();\n" +
           "\t\t\treturn chain;\n" +
           "\t\t}\n" +
           "\t\t#endregion\n" +
           "\n\t\t#region 公有静态方法\n"+
           "\n\t\t#endregion\n"+
           "\t\t}" +
           "\n}\n"
            );

            return mySbr.ToString();
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 输出/生成解决方法。
        /// </summary>
        /// <param name="project">项目元数据。</param>
        /// <param name="outputFolder">输出路径。</param>
        /// <param name="haveSolution">是否生成解决方案。</param>
        public void GeneratSolution(Project project, string outputFolder, bool haveSolution = false)
        {
            try
            {
                //目录
                string ImplementDirectory = Path.Combine(outputFolder, "Objects");
                //string UIDirectory = Path.Combine(rootPath, "UI");
                string partialImplementDirectory = Path.Combine(ImplementDirectory);
                string enumDirectory = Path.Combine(ImplementDirectory, "Enums");

                if (!System.IO.Directory.Exists(ImplementDirectory))
                    System.IO.Directory.CreateDirectory(ImplementDirectory);

                //if (!Directory.Exists(UIDirectory))
                //    Directory.CreateDirectory(UIDirectory);

                if (!System.IO.Directory.Exists(partialImplementDirectory))
                    System.IO.Directory.CreateDirectory(partialImplementDirectory);

                string code = string.Empty;

                #region //1.生成实体代码。

                foreach (RootEntity table in project.RootEntities)
                {
                    string proPath = Path.Combine(partialImplementDirectory, table.Directory);
                    if (!System.IO.Directory.Exists(proPath))
                    {
                        System.IO.Directory.CreateDirectory(proPath);
                    }
                    code = this.GeneratCode(table, project);
                    WriteCode(code, proPath, table.ClassName + ".cs");

                }

                #endregion

                #region //2.生成枚举代码。
                //生成枚举目录。
                var enums = project.Enumerations.Where(p => !string.IsNullOrEmpty(p.Name)).ToList();
                foreach (var vEnum in enums)
                {
                    string proPath = Path.Combine(enumDirectory, vEnum.Directory);
                    if (!System.IO.Directory.Exists(proPath))
                    {
                        System.IO.Directory.CreateDirectory(proPath);
                    }
                    code = this.GeneratCode(vEnum, project);
                    WriteCode(code, proPath, vEnum.Name + ".cs");
                }

                #endregion

                //生成数据上下文
                // code = CodeGenerator.CodeGeneratHelper.DataContextCodeGenerator.GeneratCode();
                // CodeWritHelper.WriteCode(code, ImplementDirectory, "DbEntities.cs", true);

                // if (haveSolution)
                //    CodeGenerator.CodeGeneratHelper.SolutionCodeGenerator.GeneratCode(new string[] { rootPath });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 代码方案名称。
        /// </summary>
        public string Name
        {
            get
            {
                return "ARCode";
            }
        }

        #endregion

        private void WriteCode(string Content, string path, string fileName)
        {
            var vFile = Path.Combine(path , fileName);
            try
            {
                File.WriteAllText(vFile, Content);

            }
            catch 
            {

            }
        }
    }
}