using System; 
using System.Collections; 
using System.Collections.Generic; 
using System.Drawing; 
using System.ComponentModel; 
using System.Data; 
using System.Windows.Forms; 
using System.IO; 
using System.Linq; 
using System.Globalization; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.Text; 

using EAS.Data; 
using EAS.Data.ORM; 
using EAS.Data.Access; 
using EAS.Modularization; 

using EAS.Services; 
using EAS.Data.Linq; 

using WF.Demo.DAL; 

namespace WF.Demo.UI 
{ 
  [Module("74213173-b531-4b8b-9e3e-d8fb8f22a601", "请假记录", "查询审批通过或者未通过的审批记录")]  
  public partial class LeaveList : UserControl 
  { 
        IQueryable<Leave> vList = null; 
    
        public LeaveList() 
        { 
            InitializeComponent(); 
            this.dataGridView1.AutoGenerateColumns = false; 
            this.dataGridView1.DataSource = this.datasourcedataGridView1; 
        } 
        
        [ModuleStart] 
        public void StartEx() 
        {
            this.btnQuery_Click(this.btnQuery, new System.EventArgs());
        } 
        
        /// <summary>
        /// 显示记录。
        /// </summary>
        IList<Leave> DisplayList
        {
            get
            {
                return this.datasourcedataGridView1.DataSource as IList<Leave>;
            }
        }

        private void dataPager_PageChanged(object sender, EventArgs e) 
        { 
            IList<Leave> items = this.vList.Skip(this.dataPager.Skip).Take(this.dataPager.Take).ToList();
            this.datasourcedataGridView1.DataSource = items; 
        } 
        
        private void btnQuery_Click(object sender, EventArgs e)
        {
            DataEntityQuery<Leave> query = DataEntityQuery<Leave>.Create();

            var v = from c in query
                    where c.Name.StartsWith(this.tbName.Text)
                    select c;

            this.vList = v as IQueryable<Leave>;
            this.dataPager.RecordCount = this.vList.Count();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }        

        private void btnClose_Click(object sender, EventArgs e) 
        { 
             EAS.Application.Instance.CloseModule(this);  
        } 
  } 
} 
