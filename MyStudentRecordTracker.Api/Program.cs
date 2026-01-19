using Microsoft.AspNetCore.Mvc;
using HelloWorld;

var builder = WebApplication.CreateBuilder(args);

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Stable shared data location (so API always uses the same file)
var dataFolder = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
    "MyStudentRecordTracker"
);
Directory.CreateDirectory(dataFolder);

var dataPath = Path.Combine(dataFolder, "students.json");

// Dependency Injection
builder.Services.AddSingleton<IStudentRepository>(_ => new StudentRepository(dataPath));
builder.Services.AddSingleton<StudentService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// GET all students (sorted)
app.MapGet("/api/students", (StudentService service) =>
{
    var students = service.GetStudentsSorted()
        .Select(s => new StudentDto(
            s.Name,
            s.Score,
            ScoreUtils.ClampScore(s.Score),
            ScoreUtils.LetterGrade(s.Score)
        ));

    return Results.Ok(students);
});

// POST add student
app.MapPost("/api/students", ([FromBody] AddStudentRequest req, StudentService service) =>
{
    service.AddStudent(req.Name, req.Score);
    return Results.Ok(new { message = "Student added" });
});

// GET grade distribution
app.MapGet("/api/distribution", (StudentService service) =>
{
    return Results.Ok(service.GetGradeDistribution());
});

// GET by name
app.MapGet("/api/students/{name}", (string name, StudentService service) =>
{
    if (service.TryFindStudent(name, out var student))
    {
        var dto = new StudentDto(
            student.Name,
            student.Score,
            ScoreUtils.ClampScore(student.Score),
            ScoreUtils.LetterGrade(student.Score)
        );

        return Results.Ok(dto);
    }

    return Results.NotFound(new { message = "Student not found" });
});

app.Run();

public record AddStudentRequest(string Name, int Score);
public record StudentDto(string Name, int ScoreRaw, int ScoreClamped, char Grade);



