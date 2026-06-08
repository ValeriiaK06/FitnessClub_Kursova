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
            _db.CreateTable<SubscriptionHistory>();
            _db.CreateTable<Specialization>();
        }

        private void SeedData()
        {
            // Заповнюємо тільки якщо таблиці порожні
            if (_db.Table<Trainer>().Count() > 0)
                return;


            // Спеціалізації (id 1..5)
            _db.Insert(new Specialization { Name = "Силові тренування" });   // 1
            _db.Insert(new Specialization { Name = "Йога та пілатес" });     // 2
            _db.Insert(new Specialization { Name = "Бокс та кікбоксинг" });  // 3
            _db.Insert(new Specialization { Name = "Зумба та аеробіка" });   // 4
            _db.Insert(new Specialization { Name = "Кардіо та схуднення" }); // 5

            // Силові (id 1)
            _db.Insert(new Trainer { LastName = "Коваль", FirstName = "Олексій", MiddleName = "Іванович", SpecializationId = 1, Experience = 8, Photo = "trainer1.jpg", Phone = "+380661112233", Email = "koval@fitness.ua" });
            _db.Insert(new Trainer { LastName = "Мороз", FirstName = "Віктор", MiddleName = "Павлович", SpecializationId = 1, Experience = 12, Photo = "trainer5.jpg", Phone = "+380665556677", Email = "moroz@fitness.ua" });

            // Йога та пілатес (id 2)
            _db.Insert(new Trainer { LastName = "Петренко", FirstName = "Марина", MiddleName = "Сергіївна", SpecializationId = 2, Experience = 6, Photo = "trainer2.jpg", Phone = "+380662223344", Email = "petrenko@fitness.ua" });
            _db.Insert(new Trainer { LastName = "Ткаченко", FirstName = "Софія", MiddleName = "Андріївна", SpecializationId = 2, Experience = 4, Photo = "trainer6.jpg", Phone = "+380666667788", Email = "tkachenko@fitness.ua" });

            // Бокс та кікбоксинг (id 3)
            _db.Insert(new Trainer { LastName = "Савченко", FirstName = "Дмитро", MiddleName = "Олегович", SpecializationId = 3, Experience = 10, Photo = "trainer3.jpg", Phone = "+380663334455", Email = "savchenko@fitness.ua" });
            _db.Insert(new Trainer { LastName = "Гриценко", FirstName = "Роман", MiddleName = "Юрійович", SpecializationId = 3, Experience = 7, Photo = "trainer7.jpg", Phone = "+380667778899", Email = "grytsenko@fitness.ua" });

            // Зумба та аеробіка (id 4)
            _db.Insert(new Trainer { LastName = "Лисенко", FirstName = "Анна", MiddleName = "Вікторівна", SpecializationId = 4, Experience = 5, Photo = "trainer4.jpg", Phone = "+380664445566", Email = "lysenko@fitness.ua" });

            // Кардіо та схуднення (id 5)
            _db.Insert(new Trainer { LastName = "Кравченко", FirstName = "Ірина", MiddleName = "Михайлівна", SpecializationId = 5, Experience = 9, Photo = "trainer8.jpg", Phone = "+380668889900", Email = "kravchenko@fitness.ua" });



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
            // Записи клієнтів — дати обчислюються за днем заняття (майбутні, активні)
            // Нагадування про дні занять із seed:
            // Schedule 1 — Силове, Понеділок | 3 — Йога, Понеділок | 4 — Пілатес, Четвер
            // 5 — Бокс, Вівторок | 6 — Кікбоксинг, П'ятниця | 7 — Зумба, Середа
            _db.Insert(new ClientBooking { ClientId = 1, ScheduleId = 3, BookingDate = NextWeekday(DayOfWeek.Monday, 0) });
            _db.Insert(new ClientBooking { ClientId = 2, ScheduleId = 5, BookingDate = NextWeekday(DayOfWeek.Tuesday, 0) });
            _db.Insert(new ClientBooking { ClientId = 3, ScheduleId = 7, BookingDate = NextWeekday(DayOfWeek.Wednesday, 0) });
            _db.Insert(new ClientBooking { ClientId = 4, ScheduleId = 1, BookingDate = NextWeekday(DayOfWeek.Monday, 1) });
            _db.Insert(new ClientBooking { ClientId = 5, ScheduleId = 4, BookingDate = NextWeekday(DayOfWeek.Thursday, 0) });
            _db.Insert(new ClientBooking { ClientId = 6, ScheduleId = 6, BookingDate = NextWeekday(DayOfWeek.Friday, 0) });
        }

        // ===== ТРЕНЕРИ =====
        public List<Trainer> GetTrainers()
        {
            var trainers = _db.Table<Trainer>().ToList();

            foreach (var t in trainers)
            {
                var spec = _db.Table<Specialization>().FirstOrDefault(s => s.Id == t.SpecializationId);
                t.SpecializationName = spec?.Name ?? "—";
            }

            return trainers;
        }
        public Trainer GetTrainer(int id) => _db.Find<Trainer>(id);
        public void AddTrainer(Trainer trainer) => _db.Insert(trainer);
        public void UpdateTrainer(Trainer trainer) => _db.Update(trainer);
        public void DeleteTrainer(Trainer trainer) => _db.Delete(trainer);

        // ===== КЛІЄНТИ =====
        public List<Client> GetClients()
        {
            var clients = _db.Table<Client>().ToList();

            foreach (var client in clients)
            {
                // Шукаємо активний абонемент цього клієнта
                var clientSub = _db.Table<ClientSubscription>()
                    .FirstOrDefault(cs => cs.ClientId == client.Id && cs.IsActive);

                if (clientSub != null)
                {
                    var sub = _db.Table<Subscription>()
                        .FirstOrDefault(s => s.Id == clientSub.SubscriptionId);

                    client.SubscriptionName = sub?.Name ?? "—";
                    client.HasActiveSubscription = true;
                }
                else
                {
                    client.SubscriptionName = "Немає активного";
                    client.HasActiveSubscription = false;
                }
            }

            return clients;
        }
        public Client GetClient(int id) => _db.Find<Client>(id);
        public void AddClient(Client client) => _db.Insert(client);
        public void UpdateClient(Client client) => _db.Update(client);
        public void DeleteClient(Client client) => _db.Delete(client);

        // ===== ЗАПИСИ КЛІЄНТІВ =====
        public List<ClientBooking> GetBookings()
        {
            var list = _db.Table<ClientBooking>().ToList();

            foreach (var b in list)
            {
                var client = _db.Table<Client>().FirstOrDefault(c => c.Id == b.ClientId);
                b.ClientName = client != null ? $"{client.LastName} {client.FirstName}" : "—";

                var schedule = _db.Table<Schedule>().FirstOrDefault(s => s.Id == b.ScheduleId);
                b.ScheduleName = schedule?.Name ?? "—";
                b.DayOfWeek = schedule?.DayOfWeek ?? "";

                // Тренер цього заняття
                if (schedule != null)
                {
                    var trainer = _db.Table<Trainer>().FirstOrDefault(t => t.Id == schedule.TrainerId);
                    b.TrainerName = trainer != null ? $"{trainer.LastName} {trainer.FirstName}" : "—";
                }
                else
                {
                    b.TrainerName = "—";
                }
            }

            return list;
        }

        // Кількість активних (майбутніх) записів — для дашборда
        public int GetActiveBookingsCount() =>
            _db.Table<ClientBooking>().ToList().Count(b => b.BookingDate.Date >= DateTime.Today);
        public ClientBooking GetBooking(int id) => _db.Find<ClientBooking>(id);
        public void AddBooking(ClientBooking booking) => _db.Insert(booking);
        public void UpdateBooking(ClientBooking booking) => _db.Update(booking);
        public void DeleteBooking(ClientBooking booking) => _db.Delete(booking);



        // ===== РОЗКЛАД =====
        public List<Schedule> GetSchedules()
        {
            // 1. Берем голое расписание
            var schedules = _db.Table<Schedule>().ToList();

            // 2. Для каждой записи расписания вручную находим тренера по ID
            foreach (var schedule in schedules)
            {
                // Ищем в таблице тренеров того, чей Id совпадает с TrainerId в расписании
                schedule.Trainer = _db.Table<Trainer>().FirstOrDefault(t => t.Id == schedule.TrainerId);
            }

            return schedules;
        }
        public Schedule GetSchedule(int id) => _db.Find<Schedule>(id);
        public void AddSchedule(Schedule schedule) => _db.Insert(schedule);
        public void UpdateSchedule(Schedule schedule) => _db.Update(schedule);
        public void DeleteSchedule(Schedule schedule) => _db.Delete(schedule);




        // ===== АБОНЕМЕНТИ КЛІЄНТІВ =====
        public List<ClientSubscription> GetClientSubscriptions()
        {
            var list = _db.Table<ClientSubscription>().ToList();

            foreach (var cs in list)
            {
                var client = _db.Table<Client>().FirstOrDefault(c => c.Id == cs.ClientId);
                cs.ClientName = client != null ? $"{client.LastName} {client.FirstName}" : "—";

                var sub = _db.Table<Subscription>().FirstOrDefault(s => s.Id == cs.SubscriptionId);
                cs.PlanName = sub?.Name ?? "—";
            }

            return list;
        }
        public ClientSubscription GetClientSubscription(int id) => _db.Find<ClientSubscription>(id);
        public void AddClientSubscription(ClientSubscription cs) => _db.Insert(cs);
        public void UpdateClientSubscription(ClientSubscription cs) => _db.Update(cs);
        public void DeleteClientSubscription(ClientSubscription cs) => _db.Delete(cs);




        // ===== ІСТОРІЯ АБОНЕМЕНТІВ =====
        public List<SubscriptionHistory> GetSubscriptionHistory() =>
            _db.Table<SubscriptionHistory>().OrderByDescending(h => h.ActionDate).ToList();
        public void AddHistory(SubscriptionHistory h) => _db.Insert(h);


        // ===== АБОНЕМЕНТИ =====
        public List<Subscription> GetSubscriptions()
        {
            var subs = _db.Table<Subscription>().ToList();

            foreach (var sub in subs)
            {
                // Знаходимо послуги цього абонемента
                var links = _db.Table<SubscriptionService>()
                    .Where(ss => ss.SubscriptionId == sub.Id).ToList();

                var names = new List<string>();
                foreach (var link in links)
                {
                    var service = _db.Table<Service>().FirstOrDefault(s => s.Id == link.ServiceId);
                    if (service != null) names.Add(service.Name);
                }

                sub.ServicesList = names.Count > 0
            ? string.Join("\n", names.Select(n => "✔ " + n))
            : "Без послуг";
            }

            return subs;
        }
        public Subscription GetSubscription(int id) => _db.Find<Subscription>(id);
        public void AddSubscription(Subscription sub) => _db.Insert(sub);
        public void UpdateSubscription(Subscription sub) => _db.Update(sub);
        public void DeleteSubscription(Subscription sub)
        {
            // Видаляємо абонемент і всі його зв'язки з послугами
            var links = _db.Table<SubscriptionService>()
                .Where(ss => ss.SubscriptionId == sub.Id).ToList();
            foreach (var link in links)
                _db.Delete(link);
            _db.Delete(sub);
        }

        // Зв'язки абонемент↔послуги
        public List<int> GetServiceIdsForSubscription(int subId) =>
            _db.Table<SubscriptionService>()
                .Where(ss => ss.SubscriptionId == subId)
                .Select(ss => ss.ServiceId).ToList();

        public void SetSubscriptionServices(int subId, List<int> serviceIds)
        {
            // Видаляємо старі зв'язки
            var old = _db.Table<SubscriptionService>()
                .Where(ss => ss.SubscriptionId == subId).ToList();
            foreach (var link in old)
                _db.Delete(link);

            // Додаємо нові
            foreach (var serviceId in serviceIds)
                _db.Insert(new SubscriptionService { SubscriptionId = subId, ServiceId = serviceId });
        }


        public Service GetService(int id) => _db.Find<Service>(id);
        public void AddService(Service service) => _db.Insert(service);
        public void UpdateService(Service service) => _db.Update(service);
        public void DeleteService(Service service)
        {
            // Прибираємо зв'язки цієї послуги з абонементами
            var links = _db.Table<SubscriptionService>()
                .Where(ss => ss.ServiceId == service.Id).ToList();
            foreach (var link in links)
                _db.Delete(link);
            _db.Delete(service);
        }


        // ===== ДОПОМІЖНІ (тільки читання) =====

        public List<Service> GetServices() => _db.Table<Service>().ToList();

        // Найближча майбутня дата заданого дня тижня (+ зсув тижнів)
        private DateTime NextWeekday(DayOfWeek day, int weeksAhead = 0)
        {
            var date = DateTime.Today;
            while (date.DayOfWeek != day)
                date = date.AddDays(1);
            return date.AddDays(weeksAhead * 7);
        }


        // ===== СПЕЦІАЛІЗАЦІЇ =====
        public List<Specialization> GetSpecializations() => _db.Table<Specialization>().ToList();
        public Specialization GetSpecialization(int id) => _db.Find<Specialization>(id);
        public void AddSpecialization(Specialization s) => _db.Insert(s);
        public void UpdateSpecialization(Specialization s) => _db.Update(s);
        public void DeleteSpecialization(Specialization s) => _db.Delete(s);


    }
}