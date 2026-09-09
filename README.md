# Meeting Room Booking API

A REST API for booking meeting rooms, built with ASP.NET Core 8. Includes a simple web interface for testing the API without Swagger.

## Features

- **Rooms** — create and list meeting rooms
- **Bookings** — create, view, update, and cancel bookings
- **Conflict detection** — prevents double-booking the same room for overlapping time slots
- **Filtering** — filter bookings by room and date
- **Pagination** — paginated results for the bookings list
- **Swagger / OpenAPI** — interactive API documentation
- **Simple web UI** — a static HTML page for creating rooms and bookings without leaving the browser

## Tech Stack

- ASP.NET Core 8 (Web API)
- Entity Framework Core + SQLite
- Swagger / Swashbuckle
- Vanilla JavaScript for the frontend (no framework, no build step)

## Project Structure

```
MeetingRoomBooking/
├── Controllers/       # RoomsController, BookingsController
├── Models/            # Room, Booking (EF Core entities)
├── DTOs/               # Data transfer objects for requests/responses
├── Data/               # AppDbContext
├── Services/           # IBookingService, BookingService — booking business logic
└── wwwroot/            # index.html — simple web UI
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Run locally

```bash
git clone https://github.com/<your-username>/MeetingRoomBooking.git
cd MeetingRoomBooking
dotnet restore
dotnet ef database update
dotnet run
```

The app will start at `https://localhost:{port}` — the web UI loads automatically at the root URL. Swagger docs are available at `/swagger`.

## API Endpoints

### Rooms

| Method | Endpoint          | Description          |
|--------|-------------------|-----------------------|
| GET    | `/api/rooms`       | List all rooms        |
| GET    | `/api/rooms/{id}`  | Get a room by id       |
| POST   | `/api/rooms`       | Create a new room      |

### Bookings

| Method | Endpoint              | Description                                      |
|--------|------------------------|---------------------------------------------------|
| GET    | `/api/bookings`        | List bookings (supports `roomId`, `date`, `page`, `pageSize` query params) |
| GET    | `/api/bookings/{id}`   | Get a booking by id                                |
| POST   | `/api/bookings`        | Create a booking (returns `409 Conflict` on overlap) |
| PUT    | `/api/bookings/{id}`   | Update a booking                                   |
| DELETE | `/api/bookings/{id}`   | Cancel a booking                                    |

### Example: creating a booking

```http
POST /api/bookings
Content-Type: application/json

{
  "roomId": 1,
  "userName": "John",
  "title": "Team standup",
  "startTime": "2026-09-10T10:00:00",
  "endTime": "2026-09-10T11:00:00"
}
```

If the room is already booked for an overlapping time, the API responds with:

```json
{
  "message": "The room is already booked for an overlapping time."
}
```

## What This Project Demonstrates

- RESTful API design with proper HTTP status codes (`200`, `201`, `204`, `404`, `409`)
- Separation of concerns: controllers handle HTTP, services handle business logic
- Dependency Injection for testability and loose coupling
- EF Core with a real conflict-detection query (interval overlap check)
- DTOs to avoid leaking database entities and circular references in JSON responses

## Possible Future Improvements

- JWT authentication and role-based authorization
- FluentValidation for richer input validation
- Unit and integration tests (xUnit, `WebApplicationFactory`)
- Docker support
- Deployment to a live environment

## License

This project is for portfolio/demonstration purposes.
