using FitnessClub.Models;
using SQLite;

namespace FitnessClub.Services
{
    public class DatabaseService
    {
        private SQLiteConnection _db;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "fitnessclub.db");
            _db = new SQLiteConnection(dbPath);

            CreateTables();
            SeedData();
        }

        private void CreateTables()
        {
            _db.CreateTable<Trainer>();
            _db.CreateTable<Client>();
            _db.CreateTable<Subscription>();
            _db.CreateTable<Service>();
            _db.CreateTable<SubscriptionService>();
            _db.CreateTable<ClientSubscription>();
            _db.CreateTable<Schedule>();
            _db.CreateTable<ClientBooking>();
        }

        private void SeedData()
        {
            // Заповнюємо тільки якщо таблиці порожні
            if (_db.Table<Trainer>().Count() > 0)
                return;

            // Тренери — по кілька на спеціалізацію для вибору
            // Силові тренування — 2 тренери
            _db.Insert(new Trainer { LastName = "Коваль", FirstName = "Олексій", MiddleName = "Іванович", Specialization = "Силові тренування", Experience = 8, Photo = "trainer1.jpg", Phone = "+380661112233", Email = "koval@fitness.ua" });
            _db.Insert(new Trainer { LastName = "Мороз", FirstName = "Віктор", MiddleName = "Павлович", Specialization = "Силові тренування", Experience = 12, Photo = "trainer5.jpg", Phone = "+380665556677", Email = "moroz@fitness.ua" });

            // Йога та пілатес — 2 тренери
            _db.Insert(new Trainer { LastName = "Петренко", FirstName = "Марина", MiddleName = "Сергіївна", Specialization = "Йога та пілатес", Experience = 6, Photo = "trainer2.jpg", Phone = "+380662223344", Email = "petrenko@fitness.ua" });
            _db.Insert(new Trainer { LastName = "Ткаченко", FirstName = "Софія", MiddleName = "Андріївна", Specialization = "Йога та пілатес", Experience = 4, Photo = "trainer6.jpg", Phone = "+380666667788", Email = "tkachenko@fitness.ua" });

            // Бокс та кікбоксинг — 2 тренери
            _db.Insert(new Trainer { LastName = "Савченко", FirstName = "Дмитро", MiddleName = "Олегович", Specialization = "Бокс та кікбоксинг", Experience = 10, Photo = "trainer3.jpg", Phone = "+380663334455", Email = "savchenko@fitness.ua" });
            _db.Insert(new Trainer { LastName = "Гриценко", FirstName = "Роман", MiddleName = "Юрійович", Specialization = "Бокс та кікбоксинг", Experience = 7, Photo = "trainer7.jpg", Phone = "+380667778899", Email = "grytsenko@fitness.ua" });

            // Зумба та аеробіка — 1 тренер
            _db.Insert(new Trainer { LastName = "Лисенко", FirstName = "Анна", MiddleName = "Вікторівна", Specialization = "Зумба та аеробіка", Experience = 5, Photo = "trainer4.jpg", Phone = "+380664445566", Email = "lysenko@fitness.ua" });

            // Кардіо та схуднення — 1 тренер
            _db.Insert(new Trainer { LastName = "Кравченко", FirstName = "Ірина", MiddleName = "Михайлівна", Specialization = "Кардіо та схуднення", Experience = 9, Photo = "trainer8.jpg", Phone = "+380668889900", Email = "kravchenko@fitness.ua" });

            // Клієнти
            _db.Insert(new Client { LastName = "Іваненко", FirstName = "Марія", MiddleName = "Петрівна", BirthDate = new DateTime(1995, 3, 15), Phone = "+380671234567", Email = "ivanenko@gmail.com", RegistrationDate = new DateTime(2024, 1, 10) });
            _db.Insert(new Client { LastName = "Шевченко", FirstName = "Андрій", MiddleName = "Олександрович", BirthDate = new DateTime(1990, 7, 22), Phone = "+380672345678", Email = "shevchenko@gmail.com", RegistrationDate = new DateTime(2024, 2, 5) });
            _db.Insert(new Client { LastName = "Бондаренко", FirstName = "Олена", MiddleName = "Ігорівна", BirthDate = new DateTime(1998, 11, 3), Phone = "+380673456789", Email = "bondarenko@gmail.com", RegistrationDate = new DateTime(2024, 3, 18) });
            _db.Insert(new Client { LastName = "Мельник", FirstName = "Сергій", MiddleName = "Васильович", BirthDate = new DateTime(1988, 5, 12), Phone = "+380674567890", Email = "melnyk@gmail.com", RegistrationDate = new DateTime(2024, 1, 25) });
            _db.Insert(new Client { LastName = "Коваленко", FirstName = "Наталія", MiddleName = "Дмитрівна", BirthDate = new DateTime(2000, 9, 8), Phone = "+380675678901", Email = "kovalenko@gmail.com", RegistrationDate = new DateTime(2024, 2, 14) });
            _db.Insert(new Client { LastName = "Пономаренко", FirstName = "Олександр", MiddleName = "Романович", BirthDate = new DateTime(1993, 12, 1), Phone = "+380676789012", Email = "ponomarenko@gmail.com", RegistrationDate = new DateTime(2024, 3, 2) });
            _db.Insert(new Client { LastName = "Гончар", FirstName = "Юлія", MiddleName = "Сергіївна", BirthDate = new DateTime(1997, 4, 19), Phone = "+380677890123", Email = "gonchar@gmail.com", RegistrationDate = new DateTime(2024, 3, 20) });
            _db.Insert(new Client { LastName = "Ткачук", FirstName = "Максим", MiddleName = "Ігорович", BirthDate = new DateTime(1991, 8, 27), Phone = "+380678901234", Email = "tkachuk@gmail.com", RegistrationDate = new DateTime(2024, 4, 1) });

            // Абонементи
            _db.Insert(new Subscription { Name = "Базовий", Description = "Доступ до тренажерного залу", PricePerMonth = 799 });
            _db.Insert(new Subscription { Name = "Преміум", Description = "Зал + групові заняття + сауна", PricePerMonth = 1499 });
            _db.Insert(new Subscription { Name = "VIP", Description = "Все включено + персональний тренер", PricePerMonth = 2999 });

            // Послуги
            _db.Insert(new Service { Name = "Тренажерний зал" });
            _db.Insert(new Service { Name = "Групові заняття" });
            _db.Insert(new Service { Name = "Сауна" });
            _db.Insert(new Service { Name = "Басейн" });
            _db.Insert(new Service { Name = "Персональний тренер" });
            _db.Insert(new Service { Name = "Масаж" });

            // Послуги абонементів
            _db.Insert(new SubscriptionService { SubscriptionId = 1, ServiceId = 1 });
            _db.Insert(new SubscriptionService { SubscriptionId = 2, ServiceId = 1 });
            _db.Insert(new SubscriptionService { SubscriptionId = 2, ServiceId = 2 });
            _db.Insert(new SubscriptionService { SubscriptionId = 2, ServiceId = 3 });
            _db.Insert(new SubscriptionService { SubscriptionId = 3, ServiceId = 1 });
            _db.Insert(new SubscriptionService { SubscriptionId = 3, ServiceId = 2 });
            _db.Insert(new SubscriptionService { SubscriptionId = 3, ServiceId = 3 });
            _db.Insert(new SubscriptionService { SubscriptionId = 3, ServiceId = 5 });
            _db.Insert(new SubscriptionService { SubscriptionId = 3, ServiceId = 6 });

            // Абонементи клієнтів
            _db.Insert(new ClientSubscription { ClientId = 1, SubscriptionId = 2, PurchaseDate = new DateTime(2024, 1, 10), ExpiryDate = new DateTime(2024, 4, 10), IsActive = true });
            _db.Insert(new ClientSubscription { ClientId = 2, SubscriptionId = 1, PurchaseDate = new DateTime(2024, 2, 5), ExpiryDate = new DateTime(2024, 5, 5), IsActive = true });
            _db.Insert(new ClientSubscription { ClientId = 3, SubscriptionId = 3, PurchaseDate = new DateTime(2024, 3, 18), ExpiryDate = new DateTime(2024, 6, 18), IsActive = false });
            _db.Insert(new ClientSubscription { ClientId = 4, SubscriptionId = 1, PurchaseDate = new DateTime(2024, 1, 25), ExpiryDate = new DateTime(2024, 4, 25), IsActive = true });
            _db.Insert(new ClientSubscription { ClientId = 5, SubscriptionId = 2, PurchaseDate = new DateTime(2024, 2, 14), ExpiryDate = new DateTime(2024, 5, 14), IsActive = true });
            _db.Insert(new ClientSubscription { ClientId = 6, SubscriptionId = 3, PurchaseDate = new DateTime(2024, 3, 2), ExpiryDate = new DateTime(2024, 6, 2), IsActive = true });
            _db.Insert(new ClientSubscription { ClientId = 7, SubscriptionId = 1, PurchaseDate = new DateTime(2024, 3, 20), ExpiryDate = new DateTime(2024, 6, 20), IsActive = true });
            _db.Insert(new ClientSubscription { ClientId = 8, SubscriptionId = 2, PurchaseDate = new DateTime(2024, 4, 1), ExpiryDate = new DateTime(2024, 7, 1), IsActive = false });

            // Розклад занять — тренери відповідають своїм спеціалізаціям
            _db.Insert(new Schedule { Name = "Силове тренування", DayOfWeek = "Понеділок", StartTime = new TimeSpan(9, 0, 0), Duration = 60, TrainerId = 1 });
            _db.Insert(new Schedule { Name = "Силове тренування", DayOfWeek = "Середа", StartTime = new TimeSpan(10, 0, 0), Duration = 60, TrainerId = 2 });
            _db.Insert(new Schedule { Name = "Йога", DayOfWeek = "Понеділок", StartTime = new TimeSpan(18, 30, 0), Duration = 90, TrainerId = 3 });
            _db.Insert(new Schedule { Name = "Пілатес", DayOfWeek = "Четвер", StartTime = new TimeSpan(17, 0, 0), Duration = 60, TrainerId = 4 });
            _db.Insert(new Schedule { Name = "Бокс", DayOfWeek = "Вівторок", StartTime = new TimeSpan(19, 0, 0), Duration = 60, TrainerId = 5 });
            _db.Insert(new Schedule { Name = "Кікбоксинг", DayOfWeek = "П'ятниця", StartTime = new TimeSpan(19, 30, 0), Duration = 60, TrainerId = 6 });
            _db.Insert(new Schedule { Name = "Зумба", DayOfWeek = "Середа", StartTime = new TimeSpan(18, 0, 0), Duration = 60, TrainerId = 7 });
            _db.Insert(new Schedule { Name = "Кардіо", DayOfWeek = "Четвер", StartTime = new TimeSpan(8, 0, 0), Duration = 45, TrainerId = 8 });

            // Записи клієнтів
            _db.Insert(new ClientBooking { ClientId = 1, ScheduleId = 3, BookingDate = new DateTime(2024, 3, 25) });
            _db.Insert(new ClientBooking { ClientId = 2, ScheduleId = 5, BookingDate = new DateTime(2024, 3, 26) });
            _db.Insert(new ClientBooking { ClientId = 3, ScheduleId = 7, BookingDate = new DateTime(2024, 3, 27) });
            _db.Insert(new ClientBooking { ClientId = 4, ScheduleId = 1, BookingDate = new DateTime(2024, 3, 28) });
            _db.Insert(new ClientBooking { ClientId = 5, ScheduleId = 4, BookingDate = new DateTime(2024, 3, 29) });
            _db.Insert(new ClientBooking { ClientId = 6, ScheduleId = 6, BookingDate = new DateTime(2024, 3, 30) });
        }

        // ===== ТРЕНЕРИ =====
        public List<Trainer> GetTrainers() => _db.Table<Trainer>().ToList();
        public Trainer GetTrainer(int id) => _db.Find<Trainer>(id);
        public void AddTrainer(Trainer trainer) => _db.Insert(trainer);
        public void UpdateTrainer(Trainer trainer) => _db.Update(trainer);
        public void DeleteTrainer(Trainer trainer) => _db.Delete(trainer);

        // ===== КЛІЄНТИ =====
        public List<Client> GetClients() => _db.Table<Client>().ToList();
        public Client GetClient(int id) => _db.Find<Client>(id);
        public void AddClient(Client client) => _db.Insert(client);
        public void UpdateClient(Client client) => _db.Update(client);
        public void DeleteClient(Client client) => _db.Delete(client);

        // ===== ЗАПИСИ КЛІЄНТІВ =====
        public List<ClientBooking> GetBookings() => _db.Table<ClientBooking>().ToList();
        public ClientBooking GetBooking(int id) => _db.Find<ClientBooking>(id);
        public void AddBooking(ClientBooking booking) => _db.Insert(booking);
        public void UpdateBooking(ClientBooking booking) => _db.Update(booking);
        public void DeleteBooking(ClientBooking booking) => _db.Delete(booking);



        // ===== РОЗКЛАД =====
        public List<Schedule> GetSchedules() => _db.Table<Schedule>().ToList();
        public Schedule GetSchedule(int id) => _db.Find<Schedule>(id);
        public void AddSchedule(Schedule schedule) => _db.Insert(schedule);
        public void UpdateSchedule(Schedule schedule) => _db.Update(schedule);
        public void DeleteSchedule(Schedule schedule) => _db.Delete(schedule);
        // ===== ДОПОМІЖНІ (тільки читання) =====
        public List<Subscription> GetSubscriptions() => _db.Table<Subscription>().ToList();
        public List<Service> GetServices() => _db.Table<Service>().ToList();
      
        public List<ClientSubscription> GetClientSubscriptions() => _db.Table<ClientSubscription>().ToList();
    }
}