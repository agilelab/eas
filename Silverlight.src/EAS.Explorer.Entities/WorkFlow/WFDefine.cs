using System;
using System.Data;
using EAS.Data.Access;
using EAS.Data.ORM;

namespace EAS.BPM.Entities
{
   /// <summary>
   /// 实体对象 WFDefine(流程定义信息)。
   /// </summary>
   public partial class WFDefine: DataEntity<WFDefine>
   {
       public string IsPublishText
       {
           get
           {
               return this.IsPublish == 1 ? "已发布" : "未发布";
           }
       }

       /// <summary>
       /// 版本号
       /// </summary>
       public string VersionText
       {
           get
           {
               return string.Format("{0}.{1}", (this.Version & 0xff0000)>>16, this.Version & 0x00ffff);
           }
       }

       /// <summary>
       /// 图片 。
       /// </summary>
       [Column("PHOTO", "图片", DbType.Binary)]
       public byte[] Photo
       {
           get;
           set;
       }
   }
}
