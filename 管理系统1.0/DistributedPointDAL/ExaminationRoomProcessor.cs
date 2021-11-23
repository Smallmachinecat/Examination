using DistributedPointDAL.DistributedPointDSTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DistributedPointDAL.DistributedPointDS;

namespace DistributedPointDAL
{
    public class ExaminationRoomProcessor
    {
        ExaminationRoomTableAdapter adp = null;

        public ExaminationRoomProcessor(SqlConnection CurConnection = null)
        {
            if (CurConnection != null)
                adp = new ExaminationRoomTableAdapter(CurConnection);
            else
                adp = new ExaminationRoomTableAdapter();
        }

        public ExaminationRoomDataTable ExaminationRoomDT
        {
            get { return adp.GetData(); }
        }

        public int Save()
        {
            return adp.Update(this.ExaminationRoomDT);
        }

        public void Cancel()
        {
            this.ExaminationRoomDT.RejectChanges();
        }

        public ExaminationRoomRow AddNewExamineeRow()
        {
            ExaminationRoomRow dr = this.ExaminationRoomDT.NewExaminationRoomRow();
            this.ExaminationRoomDT.AddExaminationRoomRow(dr);
            return dr;
        }
    }
}
