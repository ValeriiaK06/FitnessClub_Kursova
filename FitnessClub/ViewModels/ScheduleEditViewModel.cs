using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;


namespace FitnessClub.ViewModels
{
    [QueryProperty(nameof(ScheduleId), "schedule_id")]
    public partial class ScheduleEditViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;
        private readonly INavigationService _nav;
        private readonly IDialogService _dialog;
        private Schedule? _editing;
        private List<Trainer> _trainers = new();

        [ObservableProperty] private string name = string.Empty;
        [ObservableProperty] private string selectedDay = string.Empty;
        [ObservableProperty] private TimeSpan startTime = new(9, 0, 0);
        [ObservableProperty] private string duration = string.Empty;
        [ObservableProperty] private int selectedTrainerIndex = -1;

        [ObservableProperty]
        private ObservableCollection<string> days = new()
        {
            "Понеділок", "Вівторок", "Середа", "Четвер",
            "П'ятниця", "Субота", "Неділя"
        };

        [ObservableProperty] private ObservableCollection<string> trainerNames = new();

        public ScheduleEditViewModel(DatabaseService db, INavigationService nav, IDialogService dialog)
        {
            _db = db;
            _nav = nav;
            _dialog = dialog;
        }

        // Отримуємо id через QueryProperty
        private int _scheduleId;
        public int ScheduleId
        {
            get => _scheduleId;
            set { _scheduleId = value; Load(); }
        }

        private void Load()
        {
            // Заповнюємо список тренерів
            _trainers = _db.GetTrainers();
            TrainerNames = new ObservableCollection<string>(
                _trainers.Select(t => $"{t.LastName} {t.FirstName}"));

            if (_scheduleId == 0)
            {
                Title = "Нове заняття";
                _editing = null;
                return;
            }

            Title = "Редагувати заняття";
            _editing = _db.GetSchedule(_scheduleId);

            if (_editing != null)
            {
                Name = _editing.Name;
                SelectedDay = _editing.DayOfWeek;
                StartTime = _editing.StartTime;
                Duration = _editing.Duration.ToString();
                SelectedTrainerIndex = _trainers.FindIndex(t => t.Id == _editing.TrainerId);
            }
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await _dialog.AlertAsync("Помилка", "Введіть назву заняття", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(SelectedDay))
            {
                await _dialog.AlertAsync("Помилка", "Оберіть день тижня", "OK");
                return;
            }
            if (SelectedTrainerIndex < 0)
            {
                await _dialog.AlertAsync("Помилка", "Оберіть тренера", "OK");
                return;
            }
            if (!int.TryParse(Duration, out int dur) || dur <= 0)
            {
                await _dialog.AlertAsync("Помилка", "Тривалість має бути додатним числом", "OK");
                return;
            }

            int trainerId = _trainers[SelectedTrainerIndex].Id;

            if (_editing == null)
            {
                _db.AddSchedule(new Schedule
                {
                    Name = Name,
                    DayOfWeek = SelectedDay,
                    StartTime = StartTime,
                    Duration = dur,
                    TrainerId = trainerId
                });
            }
            else
            {
                _editing.Name = Name;
                _editing.DayOfWeek = SelectedDay;
                _editing.StartTime = StartTime;
                _editing.Duration = dur;
                _editing.TrainerId = trainerId;
                _db.UpdateSchedule(_editing);
            }

            await _nav.GoBackAsync();
        }

        [RelayCommand]
        private async Task CancelAsync()
            => await _nav.GoBackAsync();

       
    }
}