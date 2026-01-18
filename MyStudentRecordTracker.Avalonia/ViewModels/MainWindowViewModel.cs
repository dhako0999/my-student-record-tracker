using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using HelloWorld;

namespace HelloWorld.Avalonia.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly StudentService _service;

    public ObservableCollection<string> Students { get; } = new();

    private string _nameInput = "";
    public string NameInput
    {
        get => _nameInput;
        set
        {
            if (_nameInput != value)
            {
                _nameInput = value;
                OnPropertyChanged(nameof(NameInput));
                OnPropertyChanged(nameof(CanAddStudent));
            }
        }
    }

    private string _scoreInput = "";
    public string ScoreInput
    {
        get => _scoreInput;
        set
        {
            if (_scoreInput != value)
            {
                _scoreInput = value;
                OnPropertyChanged(nameof(ScoreInput));
                OnPropertyChanged(nameof(CanAddStudent));
            }
        }
    }

    private string _distributionText = "";
    public string DistributionText
    {
        get => _distributionText;
        set
        {
            if (_distributionText != value)
            {
                _distributionText = value;
                OnPropertyChanged(nameof(DistributionText));
            }
        }
    }

    private string _statusMessage = "";

    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            if (_statusMessage != value)
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
    }

    public MainWindowViewModel()
    {
        string dataPath = Path.Combine(AppContext.BaseDirectory, "students.json");
        var repository = new StudentRepository(dataPath);
        _service = new StudentService(repository);

        Refresh();
    }

    public void AddStudent()
    {
        if (string.IsNullOrWhiteSpace(NameInput))
        {
            StatusMessage = "Name cannot be empty.";
            return;
        }


        if (!int.TryParse(ScoreInput, out int score))
        {
            StatusMessage = "Score must be a number.";
            return;
        }
            

        try
        {
            _service.AddStudent(NameInput, score);
            StatusMessage = "Student added successfully.";
            NameInput = "";
            ScoreInput = "";
            Refresh();
        }
        catch (Exception ex)
        {
            StatusMessage = ex.Message;
            // later we can show UI error messages
        }
    }

    public void Refresh()
    {
        StatusMessage = "";
        NameInput = "";
        ScoreInput = "";
        
        Students.Clear();

        foreach (var s in _service.GetStudentsSorted())
        {
            int clamped = ScoreUtils.ClampScore(s.Score);
            char grade = ScoreUtils.LetterGrade(s.Score);
            Students.Add($"{s.Name} — {clamped} ({grade})");
        }

        var d = _service.GetGradeDistribution();
        DistributionText = $"A:{d['A']}  B:{d['B']}  C:{d['C']}  D:{d['D']}  F:{d['F']}";
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public bool CanAddStudent
    {
        get =>
            !string.IsNullOrWhiteSpace(NameInput) && int.TryParse(ScoreInput, out _);
    }
}
