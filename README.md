# MyStudentRecordTracker


A full-stack C# application built with .NET 8 that demonstrates clean architecture, unit testing, and a cross-platform desktop UI.


## Overview

MyStudentRecordTracker allows users to manage student records with validated scoring, letter grading and persistent storage. 

## Features

- Add students with name and score
- Automatic score clamping (0-100)
- Letter Grade Calculation
- Grade distribution summary
- Cross-platform Desktop UI (Avalonia)

## Project Structure

- **MyStudentRecordTracker** 
  Core domain logic (students, scoring, services, repositories)

- **MyStudentRecordTracker.Avalonia**
  Cross-platform desktop UI built with Avalonia

- **MyStudentRecordTracker.Api**
  ASP.NET Core Web API exposing student data

## Running the Desktop Application

```
bash

dotnet run --project MyStudentRecordTracker.Avalonia
```
