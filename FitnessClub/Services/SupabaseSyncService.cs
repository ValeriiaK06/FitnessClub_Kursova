using System.Net.Http.Headers;

namespace FitnessClub.Services
{
    public class SupabaseSyncService
    {
        // === ДАНІ З SUPABASE ===
        private const string SupabaseUrl = "https://buiotghtrxwbhepqzdnr.supabase.co";
        private const string ApiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImJ1aW90Z2h0cnh3YmhlcHF6ZG5yIiwicm9sZSI6ImFub24iLCJpYXQiOjE3ODA5MjM0MjQsImV4cCI6MjA5NjQ5OTQyNH0.PTDJ1DDgwbAPl5zyj3znmJPhXSftedETHGU9D46bg0M";
        private const string Bucket = "fitnessclub";
        private const string FileName = "fitnessclub.db";

        private readonly DatabaseService _db;
        private readonly HttpClient _http = new();

        private bool _suppressUpload;   // блокує автовивантаження під час завантаження

        public SupabaseSyncService(DatabaseService db)
        {
            _db = db;
            // Після будь-якої зміни даних — автоматичне фонове вивантаження
            _db.DataChanged += OnDataChanged;
        }

        private string ObjectUrl => $"{SupabaseUrl}/storage/v1/object/{Bucket}/{FileName}";

        private async void OnDataChanged()
        {
            if (_suppressUpload) return;
            await UploadAsync();
        }

        // Вивантажити локальну базу в хмару
        public async Task<bool> UploadAsync()
        {
            try
            {
                _db.CloseConnection();
                byte[] bytes = File.ReadAllBytes(_db.DbPath);
                _db.ReopenConnection();

                using var content = new ByteArrayContent(bytes);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                using var req = new HttpRequestMessage(HttpMethod.Post, ObjectUrl) { Content = content };
                req.Headers.Add("Authorization", $"Bearer {ApiKey}");
                req.Headers.Add("apikey", ApiKey);
                req.Headers.Add("x-upsert", "true");

                var resp = await _http.SendAsync(req);

                string body = await resp.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"UPLOAD STATUS: {(int)resp.StatusCode} {resp.StatusCode}");
                System.Diagnostics.Debug.WriteLine($"UPLOAD BODY: {body}");

                return resp.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UPLOAD EXCEPTION: {ex.Message}");
                return false;
            }
        }

        // Завантажити базу з хмари (підмінити локальну)
        public async Task<bool> DownloadAsync()
        {
            try
            {
                _suppressUpload = true;   // щоб підміна бази не запустила зворотне вивантаження

                using var req = new HttpRequestMessage(HttpMethod.Get, ObjectUrl);
                req.Headers.Add("Authorization", $"Bearer {ApiKey}");
                req.Headers.Add("apikey", ApiKey);

                var resp = await _http.SendAsync(req);
                if (!resp.IsSuccessStatusCode)
                    return false;   // у хмарі ще немає файлу — нормально для першого разу

                byte[] bytes = await resp.Content.ReadAsByteArrayAsync();

                _db.CloseConnection();
                File.WriteAllBytes(_db.DbPath, bytes);
                _db.ReopenConnection();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DOWNLOAD EXCEPTION: {ex.Message}");
                return false;
            }
            finally
            {
                _suppressUpload = false;
            }
        }
    }
}