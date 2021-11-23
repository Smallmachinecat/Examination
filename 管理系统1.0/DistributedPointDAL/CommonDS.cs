using System.Data.SqlClient;



namespace CommonDAL
{


    partial class CommonDS
    {
    }
}

namespace CommonDAL.CommonDSTableAdapters
{
    partial class ExaminationOptionDetailedTableAdapter
    {
        public ExaminationOptionDetailedTableAdapter(SqlConnection DBConnection)
        {
            this.Connection = DBConnection;
            this.ClearBeforeFill = true;
        }
    }

    partial class ExaminationPaperDetailedTableAdapter
    {
        public ExaminationPaperDetailedTableAdapter(SqlConnection DBConnection)
        {
            this.Connection = DBConnection;
            this.ClearBeforeFill = true;
        }
    }

    partial class AnswerSheetDetailedTableAdapter
    {
        public AnswerSheetDetailedTableAdapter(SqlConnection DBConnection)
        {
            this.Connection = DBConnection;
            this.ClearBeforeFill = true;
        }
    }

    partial class ExaminationPaperTableAdapter
    {
        public ExaminationPaperTableAdapter(SqlConnection DBConnection)
        {
            this.Connection = DBConnection;
            this.ClearBeforeFill = true;
        }
    }

    partial class ExaminationVenueOrderTableAdapter
    {
        public ExaminationVenueOrderTableAdapter(SqlConnection DBConnection)
        {
            this.Connection = DBConnection;
            this.ClearBeforeFill = true;
        }
    }

    partial class ExaminationVenueTableAdapter
    {
        public ExaminationVenueTableAdapter(SqlConnection DBConnection)
        {
            this.Connection = DBConnection;
            this.ClearBeforeFill = true;
        }
    }

    partial class HeartbeatTableAdapter
    {
        public HeartbeatTableAdapter(SqlConnection DBConnection)
        {
            this.Connection = DBConnection;
            this.ClearBeforeFill = true;
        }
    }

    partial class ExamineeTableAdapter
    {
        public ExamineeTableAdapter(SqlConnection DBConnection)
        {
            this.Connection = DBConnection;
            this.ClearBeforeFill = true;
        }
    }

    public partial class ExaminationRoomTableAdapter
    {
        public ExaminationRoomTableAdapter(SqlConnection DBConnection)
        {
            this.Connection = DBConnection;
            this.ClearBeforeFill = true;
        }
    }
}

