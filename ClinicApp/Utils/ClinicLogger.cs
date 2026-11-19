using System.Text;

namespace ClinicApp.Utils;

public class ClinicLogger
{
    private readonly string _logPath;

    public ClinicLogger(string logPath = "clinic.log")
    {
        _logPath = logPath;
    }

    public void LogInfo(string message)
    {
        Write("INFO ", message);
    }

    public void LogWarning(string message)
    {
        Write("WARN ", message);
    }

    public void LogError(string message)
    {
        Write("ERROR", message);
    }

    public bool Exists()
    {
        return File.Exists(_logPath);
    }

    public string[] GetLastLines(int n)
    {
        if (n <= 0 || !Exists())
        {
            return new string[0];
        }

        string[] allLines = File.ReadAllLines(_logPath, Encoding.UTF8);

        int start = allLines.Length - n;
        if (start < 0)
        {
            start = 0;
        }

        int count = allLines.Length - start;
        string[] result = new string[count];
        Array.Copy(allLines, start, result, 0, count);

        return result;
    }

    public void Clear()
    {

        File.Delete(_logPath);
    }
    
    private void Write(string level, string message)
    {
        string line =
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}" +
            Environment.NewLine;

        try
        {
            File.AppendAllText(_logPath, line, Encoding.UTF8);
        }
        catch (IOException)
        {
            // збій логування не повинен валити програму
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}