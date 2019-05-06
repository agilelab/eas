using System;
using System.Data;
using EAS.Data.Access;
using EAS.Data.ORM;
using EAS.Workflow;

namespace EAS.BPM.Entities
{
   /// <summary>
   /// 实体对象 WFInstance(流程实例信息)。
   /// </summary>
   public partial class WFInstance: DataEntity<WFInstance>
   {
       public string IsComplateText
       {
           get
           {
               return this.IsComplate == 0 ? "运行" : (this.IsComplate == 1 ? "完成" : "终止");
           }
       }

       /// <summary>
       /// 获取工作流实例相关联的业务数据对象。
       /// </summary>
       /// <returns></returns>
       public IWorkflowDataEntity GetDataEntity()
       {
           try
           {

               System.Type T = EAS.Objects.ClassProvider.GetType(this.DataType);
               return (IWorkflowDataEntity)EAS.Serialization.SerializeHelper.DeserializeXml(T, this.DataXML);
           }
           catch
           {
               return null;
           }
       }
   }
}
