using clinic.models;
using Oracle.ManagedDataAccess.Client;
using System.Numerics;

namespace clinic.packpages
{
    public interface IPKG_BOOKING {
        void Add_booking(Booking booking);

    }

    public class PKG_BOOKING : PKG_BASE, IPKG_BOOKING
    {
        public PKG_BOOKING(IConfiguration configuration) : base(configuration) { }

        public void Add_booking(Booking booking)
        {

            OracleConnection conn = new OracleConnection
            {
                ConnectionString = ConnStr
            };
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = "olerning.PKG_NO_CLINIC_BOOKING.add_booking";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("user_id", OracleDbType.Int32).Value = booking.UserId;
                cmd.Parameters.Add("doctor_id", OracleDbType.Int32).Value = booking.DoctorId;
                cmd.Parameters.Add("v_booking_date", OracleDbType.Date).Value = booking.Booking_date;
                cmd.Parameters.Add("v_description", OracleDbType.Varchar2).Value = booking.Description;

                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
        }
    }
}
