using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace FitnessClub.ViewModels;

[QueryProperty(nameof(BookingId), "booking_id")]
public partial class BookingEditViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private ClientBooking? _editing;
    private List<Client> _clients = new();
    private List<Schedule> _schedules = new();
    private List<DateTime> _availableDates = new();

    [ObservableProperty] private ObservableCollection<string> clientNames = new();
    [ObservableProperty] private ObservableCollection<string> scheduleNames = new();
    [ObservableProperty] private ObservableCollection<string> dateOptions = new();

    [ObservableProperty] private int selectedClientIndex = -1;
    [ObservableProperty] private int selectedScheduleIndex = -1;
    [ObservableProperty] private int selectedDateIndex = -1;

    public BookingEditViewModel(DatabaseService db)
    {
        _db = db;
    }

    private int _bookingId;
    public int BookingId
    {
        get => _bookingId;
        set { _bookingId = value; Load(); }
    }

    private void Load()
    {
        _clients = _db.GetClients();
        _schedules = _db.GetSchedules();

        ClientNames = new ObservableCollection<string>(
            _clients.Select(c => $"{c.LastName} {c.FirstName}"));
        ScheduleNames = new ObservableCollection<string>(
            _schedules.Select(s => $"{s.Name} ({s.DayOfWeek})"));

        if (_bookingId == 0)
        {
            Title = "Новий запис";
            _editing = null;
            return;
        }

        Title = "Редагувати запис";
        _editing = _db.GetBooking(_bookingId);

        if (_editing != null)
        {
            SelectedClientIndex = _clients.FindIndex(c => c.Id == _editing.ClientId);
            SelectedScheduleIndex = _schedules.FindIndex(s => s.Id == _editing.ScheduleId);
            // Дати згенеруються через OnSelectedScheduleIndexChanged;
            // обрану дату виставимо після генерації
            SelectDateAfterLoad(_editing.BookingDate);
        }
    }

    private DateTime? _pendingDate;
    private void SelectDateAfterLoad(DateTime date)
    {
        _pendingDate = date;
        RebuildDates();
    }

    // Автоматично викликається при зміні обраного заняття
    partial void OnSelectedScheduleIndexChanged(int value)
    {
        RebuildDates();
    }

    private void RebuildDates()
    {
        _availableDates = new List<DateTime>();
        DateOptions = new ObservableCollection<string>();
        SelectedDateIndex = -1;

        if (SelectedScheduleIndex < 0 || SelectedScheduleIndex >= _schedules.Count)
            return;

        string dayName = _schedules[SelectedScheduleIndex].DayOfWeek;
        DayOfWeek? target = MapDay(dayName);
        if (target == null) return;

        // Генеруємо найближчі 8 дат цього дня тижня
        var date = DateTime.Today;
        while (date.DayOfWeek != target.Value)
            date = date.AddDays(1);

        for (int i = 0; i < 8; i++)
        {
            _availableDates.Add(date);
            date = date.AddDays(7);
        }

        // Якщо редагуємо і стара дата не потрапила в список — додаємо її першою
        if (_pendingDate.HasValue && !_availableDates.Any(d => d.Date == _pendingDate.Value.Date))
            _availableDates.Insert(0, _pendingDate.Value);

        foreach (var d in _availableDates)
            DateOptions.Add(d.ToString("dd.MM.yyyy (dddd)",
                new System.Globalization.CultureInfo("uk-UA")));

        // Виставляємо обрану дату
        if (_pendingDate.HasValue)
        {
            int idx = _availableDates.FindIndex(d => d.Date == _pendingDate.Value.Date);
            if (idx >= 0) SelectedDateIndex = idx;
            _pendingDate = null;
        }
    }

    private static DayOfWeek? MapDay(string ua) => ua switch
    {
        "Понеділок" => DayOfWeek.Monday,
        "Вівторок" => DayOfWeek.Tuesday,
        "Середа" => DayOfWeek.Wednesday,
        "Четвер" => DayOfWeek.Thursday,
        "П'ятниця" => DayOfWeek.Friday,
        "Субота" => DayOfWeek.Saturday,
        "Неділя" => DayOfWeek.Sunday,
        _ => null
    };

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (SelectedClientIndex < 0)
        {
            await Shell.Current.DisplayAlert("Помилка", "Оберіть клієнта", "OK");
            return;
        }
        if (SelectedScheduleIndex < 0)
        {
            await Shell.Current.DisplayAlert("Помилка", "Оберіть заняття", "OK");
            return;
        }
        if (SelectedDateIndex < 0 || SelectedDateIndex >= _availableDates.Count)
        {
            await Shell.Current.DisplayAlert("Помилка", "Оберіть дату", "OK");
            return;
        }

        int clientId = _clients[SelectedClientIndex].Id;
        int scheduleId = _schedules[SelectedScheduleIndex].Id;
        DateTime bookingDate = _availableDates[SelectedDateIndex];

        if (_editing == null)
        {
            _db.AddBooking(new ClientBooking
            {
                ClientId = clientId,
                ScheduleId = scheduleId,
                BookingDate = bookingDate
            });
        }
        else
        {
            _editing.ClientId = clientId;
            _editing.ScheduleId = scheduleId;
            _editing.BookingDate = bookingDate;
            _db.UpdateBooking(_editing);
        }

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}