using FluentValidation;
using FluentValidation.AspNetCore;
using Hotel.Application.Auth;
using Hotel.Application.Interfaces;
using Hotel.Application.Services;
using Hotel.Application.Validators;
using Hotel.Domain.Enims;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces;
using Hotel.Infrastructure.Persistence;
using Hotel.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            // Configure Database Context
            services.AddDbContext<HotelDbContext>(options =>
                options.UseSqlServer(connectionString));


            // Configure Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<HotelDbContext>()
                    .AddDefaultTokenProviders();

            // Register Application Services
            services.AddScoped<AuthService>();


            // Register Repositories and Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();

            // Register Domain Services
            services.AddScoped(typeof(IRoomService), typeof(RoomService));
            services.AddScoped(typeof(IGuestService), typeof(GuestService));
            services.AddScoped(typeof(IReservationService), typeof(ReservationService));
            services.AddScoped(typeof(IStripePaymentService), typeof(StripePaymentService));

            // Configure Authorization Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("ReceptionistOnly", policy => policy.RequireRole("Receptionist"));
                options.AddPolicy("GuestOnly", policy => policy.RequireRole("Guest"));
            });

            //Add FluentValidation
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<LoginUserDtoValidator>()
                .AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();




            return services;
        }


        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtKey = configuration["Jwt:Key"]!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = key
                };
            });

            return services;
        }

        // Seed data
        public static void SeedData(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
                    context.Database.EnsureCreated(); // Ensures the database is created

                    // Only seed if no rooms exist to prevent duplicate data on every run
                    if (!context.Rooms.Any())
                    {
                        // Seed Rooms
                        var rooms = new List<Room>
                    {
                            new Room 
                            {
                                Number = "101",
                                Description = "Comfortable single room with sea view, ideal for solo travelers. Equipped with air conditioning, WiFi, and a relaxing seaside atmosphere.",
                                Type = RoomType.Single,
                                PricePerNight = 500,
                                IsAvailable = true,
                                ImageUrl = "room101.jpg",
                                Features = "AirConditioner,WiFi,SeaView"
                            },
                            new Room 
                            {
                                Number = "102", 
                                Description = "Spacious double room perfect for couples. Includes a large bed, WiFi, and TV for entertainment.", 
                                Type = RoomType.Double, 
                                PricePerNight = 800, 
                                IsAvailable = true, 
                                ImageUrl = "room102.jpg",
                                Features = "WiFi,TV"
                            },
                            new Room
                            { 
                                Number = "201", 
                                Description = "Luxury suite with private balcony. Features air conditioning, mini bar, and safe box for extra comfort and security.", 
                                Type = RoomType.Suite, 
                                PricePerNight = 1500, 
                                IsAvailable = false, 
                                ImageUrl = "room201.jpg",
                                Features = "AirConditioner,MiniBar,SafeBox"
                            },
                            new Room
                            {
                                Number = "202", 
                                Description = "Triple room suitable for families or groups. Includes WiFi, mini bar, and safe box for added convenience.", 
                                Type = RoomType.Triple, 
                                PricePerNight = 1200, 
                                IsAvailable = false, 
                                ImageUrl = "room202.jpg",
                                Features = "WiFi,MiniBar,SafeBox"
                            },
                            new Room
                            {
                                Number = "301", 
                                Description = "Spacious family room with kitchenette, perfect for longer stays. Equipped with air conditioning and WiFi for a homely experience.", 
                                Type = RoomType.Family, 
                                PricePerNight = 2000, 
                                IsAvailable = true, 
                                ImageUrl = "room301.jpg",
                                Features = "AirConditioner,WiFi,Kitchenette"
                            }
                        };
                        context.Rooms.AddRange(rooms);
                        context.SaveChanges();

                        // Seed Guests
                        var guests = new List<Guest>
                        {
                            new Guest 
                            {
                                FullName = "Ahmed Ali",
                                Phone = "0100000001",
                                Email = "ahmed@example.com",
                                NationalId = "29801012345678",
                                Notes = "VIP Guest"
                            },
                            new Guest 
                            { 
                                FullName = "Sara Mohamed",
                                Phone = "0100000002",
                                Email = "sara@example.com",
                                NationalId = "29902098765432",
                                Notes = "Prefers high floors" },
                            new Guest 
                            {
                                FullName = "Omar Hassan",
                                Phone = "0100000003",
                                Email = "omar@example.com",
                                NationalId = "30003019283746",
                                Notes = "Late check-in and vegetarian meals"
                            },
                            new Guest 
                            {
                                FullName = "Mona Adel",
                                Phone = "0100000004",
                                Email = "mona@example.com",
                                NationalId = "29704011223344" , 
                                Notes = "Requires extra pillows"
                            },
                            new Guest
                            {
                                FullName = "Khaled Youssef",
                                Phone = "0100000005",
                                Email = "khaled@example.com",
                                NationalId = "29505055667788",
                                Notes = "Business traveler - needs meeting room"
                            }
                        };
                        context.Guests.AddRange(guests);
                        context.SaveChanges();

                        // Seed Reservations
                        var reservations = new List<Reservation>
                        {
                            new Reservation
                            {
                                RoomId = 3,
                                GuestId = 1,
                                CheckInDate = new DateTime(2025, 8, 10),
                                CheckOutDate = new DateTime(2025, 8, 15),
                                TotalPrice = 7500,
                                Status = ReservationStatus.Confirmed
                            },
                            new Reservation
                            {
                                RoomId = 1,
                                GuestId = 2,
                                CheckInDate = new DateTime(2025, 8, 20),
                                CheckOutDate = new DateTime(2025, 8, 22),
                                TotalPrice = 1000,
                                Status = ReservationStatus.Pending
                            },
                            new Reservation 
                            {
                                RoomId = 4, 
                                GuestId = 3, 
                                CheckInDate = new DateTime(2025, 9, 1), 
                                CheckOutDate = new DateTime(2025, 9, 5), 
                                TotalPrice = 4800, 
                                Status = ReservationStatus.Confirmed 
                            },
                            new Reservation 
                            {
                                RoomId = 5,
                                GuestId = 4,
                                CheckInDate = new DateTime(2025, 8, 25),
                                CheckOutDate = new DateTime(2025, 8, 30),
                                TotalPrice = 10000,
                                Status = ReservationStatus.CheckedIn
                            },
                            new Reservation
                            {
                                RoomId = 2,
                                GuestId = 5,
                                CheckInDate = new DateTime(2025, 9, 10),
                                CheckOutDate = new DateTime(2025, 9, 15),
                                TotalPrice = 4000,
                                Status = ReservationStatus.Pending
                            }
                        };

                        context.Reservations.AddRange(reservations);
                        context.SaveChanges();

                        // Seed Payments
                        
                    }
                }
            }
        }
    }
}
