namespace Coreplus.Sample.Api.Types
{
    //   public class Appointment
    //{
    //    public int Id { get; set; }
    //    public DateTime Date { get; set; }
    //    public string ClientName { get; set; }
    //    public string AppointmentType { get; set; }
    //    public int Duration { get; set; }
    //    public decimal Revenue { get; set; }
    //    public decimal Cost { get; set; }
    //    public int PractitionerId { get; set; }
    //}

    public class Appointment
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string client_name { get; set; }
        public string appointment_type { get; set; }
        public int duration { get; set; }
        public decimal revenue { get; set; }
        public decimal cost { get; set; }
        public int practitioner_id { get; set; }
    }
}
