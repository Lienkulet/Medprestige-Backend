using AutoMapper;
using MedPrestige.BLL.Interfaces;
using MedPrestige.BLL.Logic;
using MedPrestige.DAL.Interfaces;

namespace MedPrestige.BLL
{
    public class BusinessLogic
    {
        public IUserLogic Users { get; }
        public IPatientLogic Patients { get; }
        public IDoctorLogic Doctors { get; }
        public IServiceLogic Services { get; }
        public IAppointmentLogic Appointments { get; }

        public BusinessLogic(
            IUserRepository userRepository,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository,
            IDoctorServiceRepository doctorServiceRepository,
            IDoctorDetailRepository doctorDetailRepository,
            IServiceRepository serviceRepository,
            IAppointmentRepository appointmentRepository,
            IMapper mapper)
        {
            Users = new UserLogic(userRepository, mapper);
            Patients = new PatientLogic(patientRepository, mapper);
            Doctors = new DoctorLogic(doctorRepository, doctorServiceRepository, doctorDetailRepository, userRepository, mapper);
            Services = new ServiceLogic(serviceRepository, mapper);
            Appointments = new AppointmentLogic(appointmentRepository, mapper);
        }
    }
}
