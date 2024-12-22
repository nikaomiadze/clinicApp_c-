using clinic.models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Numerics;

namespace clinic.packpages
{
    public interface IPKG_BOOKING {
        void Add_booking(Booking booking);
        List<Booking> Get_doctor_booking(int id);

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
                cmd.CommandText = "SYS.PKG_NO_CLINIC_BOOKING.add_booking";
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
        public List<Booking> Get_doctor_booking(int id)
            {
                List<Booking> list = new List<Booking>();

                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConnStr;
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SYS.PKG_NO_CLINIC_BOOKING.get_booking_by_doctor_id";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("v_doctor_id", OracleDbType.Int32).Value = id;
                cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Booking booking = new Booking();
                    booking.Id = int.Parse(reader["id"].ToString());
                    booking.UserId = int.Parse(reader["user_id"].ToString());
                    booking.DoctorId = int.Parse(reader["doctor_id"].ToString());
                    booking.Booking_date=DateTime.Parse(reader["booking_date"].ToString());
                    booking.Description = reader["description"].ToString();
                   

                    list.Add(booking);


                }
                return list;
            }
        

    }
}
