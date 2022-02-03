using GestDep.Entities;
using GestDep.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Services
{
    public class GestDepService : IGestDepService
    {
        private readonly IDAL dal; //Persistence Layer Interface
        public CityHall cityHall;  //Services only work on a City Hall
        public Gym gym;			   // Gym of the City Hall. Also available from cityHall.Gyms.First();
        
        /// </summary>
        /// Returns a service Layer connected with the persistence Layer. Retrieves the CitiHall and Gym from the database if they exist. If not, it creates new ones
        /// </summary>
        /// <param name="dal"> Persistence Layer Interface</param>
        
        public GestDepService(IDAL dal)
        {
            this.dal = dal;
            try
            {
                    
                if (dal.GetAll<CityHall>().Count() == 0) //No cityHall in the system. Data initilization. 
                {
                    bool CLEAR_DATABASE = true;
                    int ROOMS_NUMBER = 9;
                    int INSTRUCTORS_NUMBER = 5;
                    Populate populateDB = new Populate(CLEAR_DATABASE,dal);
                    cityHall = populateDB.InsertCityHall();
                    gym = populateDB.InsertGym(cityHall);     //Also in cityHall.First();                
                    populateDB.InsertRooms(ROOMS_NUMBER, gym);  //Now available from gym.rooms;
                    populateDB.InsertInstructors(INSTRUCTORS_NUMBER, cityHall); //Now available from cityHall.People;

                }
                else
                {   //Retrieve the CityHall stored
                    cityHall = dal.GetAll<CityHall>().First();

                    if (cityHall.Gyms.Count > 0)
                    { //Retrieve the Gym stored
                        gym = cityHall.Gyms.First();                       

                    }
                    else
                    { //Adding Rooms and Gym
                        bool MANTAIN_DATABASE = false;
                        int ROOMS_NUMBER = 9;
                        Populate populateDB = new Populate(MANTAIN_DATABASE, dal);
                        gym = populateDB.InsertGym(cityHall);
                        populateDB.InsertRooms(ROOMS_NUMBER, gym);
                    }
                    int INSTRUCTORS_NUMBER = 5;
                    if (dal.GetAll<Instructor>().Count() == 0)//No instructors
                    { 
                        bool MANTAIN_DATABASE = false;
                        Populate populateDB = new Populate(MANTAIN_DATABASE, dal);
                        populateDB.InsertInstructors(INSTRUCTORS_NUMBER, cityHall); //Now available from cityHall.People;
                    }

                }
            } catch(Exception e)
            {
                throw new ServiceException("Error in the service init process", e);
                
            }
        }

        public int AddNewActivity(Days activityDays, string description, TimeSpan duration, DateTime finishDate, int maximumEnrollments, int minimumEnrollments, double price, DateTime startDate, DateTime startHour, ICollection<int> roomsIds)
        {
            throw new NotImplementedException();
        }

        public void AddNewUser(string address, string iban, string id, string name, int zipCode, DateTime birthDate, bool retired)
        {
            throw new NotImplementedException();
        }

        public void AssignInstructorToActivity(int activityId, string instructorId)
        {
            throw new NotImplementedException();
        }

        public int EnrollUserInActivity(int activityId, string userId)
        {
            throw new NotImplementedException();
        }

        public void GetActivityDataFromId(int ActivityId, out Days activityDays, out string description, out TimeSpan duration, out DateTime finishDate, out int maximumEnrollments, out int minimumEnrollments, out double price, out DateTime startDate, out DateTime startHour, out ICollection<int> enrollmentIds, out string instructorId, out ICollection<int> roomIds)
        {
            throw new NotImplementedException();
        }

        public ICollection<int> GetAllActivitiesIds()
        {
            throw new NotImplementedException();
        }

        public ICollection<int> GetAllRunningOrFutureActivitiesIds()
        {
            throw new NotImplementedException();
        }

        public ICollection<string> GetAvailableInstructorsIds(Days activityDays, TimeSpan duration, DateTime finishDate, DateTime startDate, DateTime startHour)
        {
            throw new NotImplementedException();
        }

        public void GetEnrollmentDataFromIds(int activityId, int enrollmentId, out DateTime? cancellationDate, out DateTime enrollmentDate, out DateTime? returnedFirstCuotaIfCancelledActivity, out ICollection<int> paymentIds, out string userId)
        {
            throw new NotImplementedException();
        }

        public void GetGymData(out int gymId, out DateTime closingHour, out int discountLocal, out int discountRetired, out double freeUserPrice, out string name, out DateTime openingHour, out int zipCode, out ICollection<int> activityIds, out ICollection<int> roomIds)
        {
            throw new NotImplementedException();
        }

        public void GetInstructorDataFromId(string instructorId, out string address, out string IBAN, out string name, out int zipCode, out string ssn, out ICollection<int> activitiesIds)
        {
            throw new NotImplementedException();
        }

        public ICollection<int> GetListAvailableRoomsIds(Days activityDays, TimeSpan duration, DateTime finishDate, DateTime startDate, DateTime startHour)
        {
            throw new NotImplementedException();
        }

        public Dictionary<DateTime, int> GetListAvailableRoomsPerWeek(DateTime initialMonday)
        {
            throw new NotImplementedException();
        }

        public void GetPaymentDataFromId(int paymentId, out DateTime date, out string description, out double quantity)
        {
            throw new NotImplementedException();
        }

        public void GetRoomDataFromId(int roomId, out int number, out ICollection<int> activityIds)
        {
            throw new NotImplementedException();
        }

        public void GetUserDataFromId(string userId, out string address, out string iban, out string name, out int zipCode, out DateTime birthDate, out bool retired, out ICollection<int> enrollmentIds)
        {
            throw new NotImplementedException();
        }

        public double GetUserDataNotInActivityAndFirstQuota(int activityId, string userId, out string address, out string iban, out string name, out int zipCode, out DateTime birthDate, out bool retired, out ICollection<int> enrollmentIds)
        {
            throw new NotImplementedException();
        }

        #region Connection with the Persistence Layer
        public void RemoveAllData()
        {
            dal.RemoveAllData();
        }

     
        public void SaveChanges()
        {
            dal.Commit();
        }
        #endregion
    }
}
