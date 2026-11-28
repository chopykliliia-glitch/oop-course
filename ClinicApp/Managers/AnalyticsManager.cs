using ClinicApp.Models;

namespace ClinicApp.Managers;

public class AnalyticsManager
{
    private AppointmentManager _appointments;
    private DoctorManager _doctors;
    private PatientManager _patients;

    public AnalyticsManager(
        AppointmentManager appointments,
        DoctorManager doctors,
        PatientManager patients)
    {
        _appointments = appointments;
        _doctors = doctors;
        _patients = patients;
    }

    public IEnumerable<DoctorStats> ComputeDoctorStats()
    {
        Appointment[] appointments = _appointments.GetAll();

        return _doctors.GetAll().Select(d =>
        {
            Appointment[] own = appointments
                .Where(a => a.DoctorId == d.Id)
                .ToArray();
            
            DateTime last = own.Any()
                ? own.Max(a => a.ScheduledAt)
                : DateTime.MinValue;

            return new DoctorStats(
                d.Id,
                d.FullName,
                own.Count(),
                own.Sum(a => a.GetCost()),
                last);
        });
    }

    public IEnumerable<PatientStats> ComputePatientStats()
    {
        Appointment[] appointments = _appointments.GetAll();

        return _patients.GetAll().Select(p =>
        {
            Appointment[] own = appointments
                .Where(a => a.PatientId == p.Id)
                .ToArray();

            DateTime last = own.Any()
                ? own.Max(a => a.ScheduledAt)
                : DateTime.MinValue;

            return new PatientStats(
                p.Id,
                p.FullName,
                own.Count(),
                own.Sum(a => a.GetCost()),
                last);
        });
    }
}