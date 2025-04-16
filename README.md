# ContactsApp

Prosta aplikacja webowa zbudowana w architekturze klient-serwer, umożliwiająca zarządzanie listą kontaktów. Backend stworzony w technologii ASP.NET Core udostępnia REST API, natomiast frontend oparty jest na React jako SPA (Single Page Application). Projekt powstał z wykorzystaniem szablonu "React and ASP.NET Core" dostępnego w Visual Studio.

## Funkcjonalności
1. **Logowanie i rejestracja** użytkownika:
   - Walidacja danych
   - Haszowanie hasła (BCrypt)

3. **Szczegóły kontaktu** (dostępne po zalogowaniu):
   - Dodawanie, edytowanie i usuwanie kontaktów
   - Kategorie i podkategorie kontaktów przechowywane sa w bazie danych

## Technologie

**Backend**: (ASP.NET Core)
- Język: C#
- Framework: ASP.NET Core 8
- Architektura: REST API
- Baza danych: Entity Framework Core SQLite
- Bezpieczeństwo: JWT, haszowanie haseł BCrypt
- Walidacja: FluentValidation
- AutoMapper: mapowanie DTO do encji

**Frontend**: (React)
- Biblioteka: React (z szablonu Visual Studio)
- Axios: Wykonywanie requestów
- UI: Domyślny szkielet
- Obsługa JWT i localStorage

## Struktura projektu
- `ClientApp/` - frontend React
- `Controllers/` - kontrolery REST API
- `Dtos/` - klasy DTO do przesyłania danych
- `Entities/` - encje bazodanowe
- `Services/` - logika biznesowa (np. `AuthService`)
- `Validators/` - walidatory FluentValidation
- `Repositories/` - abstrakcje dostępu do danych (UserRepository, ContactRepository)
- `Program.cs / Startup.cs` - konfiguracja aplikacji, DI, middleware, CORS, JWT
  
## Uruchomienie projektu

1. **Wymagania**:
   - Visual Studio 2022+ z zainstalowanym .NET SDK (np. .NET 8)
   - Node.js (automatycznie instalowany przy uruchamianiu React przez Visual Studio)

2. **Kroki uruchomienia**:
   - Otwórz plik `.sln` w Visual Studio
   - Wybierz profil uruchamiania `IIS Express` lub `Projekt ASP.NET Core` i kliknij "Start"
   - Visual Studio automatycznie uruchomi backend oraz frontend (React) w jednym procesie


