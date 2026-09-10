FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MeetingRoomBooking.csproj", "./"]
RUN dotnet restore "MeetingRoomBooking.csproj"
COPY . .
RUN dotnet build "MeetingRoomBooking.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetingRoomBooking.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "MeetingRoomBooking.dll"]