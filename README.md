# Meeting Room Booking API

A REST API for booking meeting rooms, built with ASP.NET Core 8. Includes a simple web interface for testing the API without Swagger.

## Features

- **Rooms** - create and list meeting rooms
- **Bookings** - create, view, update, and cancel bookings
- **Conflict detection** - prevents double-booking the same room for overlapping time slots
- **Filtering** - filter bookings by room and date
- **Pagination** - paginated results for the bookings list (at swagger)
- **Swagger/OpenAPI** - interactive API documentation
- **Simple web UI** - a static HTML page for creating rooms and bookings without leaving the browser
- **Docker support** - runs in a container with no local .NET install required

## Tech stack

- ASP.NET Core 8 (Web API)
- Entity Framework Core + SQLite
- Swagger/Swashbuckle
- Vanilla JavaScript for the frontend (no framework, no build step)
- Docker (multi-stage build)

## Project structure

```
MeetingRoomBooking/
├── Controllers/        # RoomsController, BookingsController
├── Models/             # Room, Booking (EF Core entities)
├── DTOs/               # Data transfer objects for requests/responses
├── Data/               # AppDbContext
├── Services/           # IBookingService, BookingService - booking business logic
├── wwwroot/             # index.html - simple web UI
├── Dockerfile            # Multi-stage build for containerized deployment
└── .dockerignore
```

## Getting started

<p align="center">
  <img src="Interface photo.png" alt="Interface look" width="700" />
</p>

### Option 1: Run locally with .NET

#### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

#### Steps

```bash
git clone https://github.com/fpkjpkjjjn/MeetingRoomBooking.git
cd MeetingRoomBooking
dotnet restore
dotnet ef database update
dotnet run
```

The app will start at `https://localhost:{port}` - the web UI loads automatically at the root URL. Swagger docs are available at `/swagger`.

### Option 2: Run with Docker

#### Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

#### Steps

```bash
git clone https://github.com/fpkjpkjjjn/MeetingRoomBooking.git
cd MeetingRoomBooking
docker build -t meetingroombooking .
docker run -p 8080:8080 meetingroombooking
```

The app will be available at `http://localhost:8080`. Database migrations are applied automatically on startup, so no manual setup is needed inside the container.

## API endpoints

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
  "userName": "Ivan",
  "title": "Team meet 1",
  "startTime": "2026-09-15T10:00:00",
  "endTime": "2026-09-15T12:00:00"
}
```

If the room is already booked for an overlapping time, the API responds with:

```json
{
  "message": "The room is already booked for an overlapping time."
}
```

## What this project demonstrates

- RESTful API design with proper HTTP status codes
- Separation of concerns: controllers handle HTTP, services handle business logic
- Dependency Injection for testability and loose coupling
- EF Core with a real conflict-detection query
- DTOs to avoid leaking database entities and circular references in JSON responses
- Containerization with a multi-stage Dockerfile and automatic database migrations on startup

## License

This project is licensed under the MIT License.
