using AutoMapper;
using CMS.Core.Entities;
using Hotel.Application.DTOs.GuestDtos;
using Hotel.Application.DTOs.PaymentDtos;
using Hotel.Application.DTOs.ReservationDtos;
using Hotel.Application.DTOs.RoomDtos;
using Hotel.Application.DTOs.User;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<ApplicationUser, RegisterUserDto>().ReverseMap();
            CreateMap<ApplicationUser, LoginUserDto>().ReverseMap();

            

            // Payment mappings
            CreateMap<Payment, PaymentDetailsDto>().ReverseMap();
            CreateMap<CreatePaymentDto, Payment>()
                .ForMember(dest => dest.Reservation, opt => opt.Ignore()); // Ignore Reservation for creation  

            // Room mappings  
            CreateMap<Room, RoomDetailsDto>().ReverseMap();
            CreateMap<CreateRoomDto, Room>().ReverseMap();
            CreateMap<UpdateRoomDto, Room>().ReverseMap();

            // Guest mappings
            CreateMap<CreateGuestDto, Guest>();
            CreateMap<UpDateGuestDto, Guest>().ReverseMap();
            CreateMap<Guest, GuestDetailsDto>()
            .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.Reservations.FirstOrDefault() != null ? src.Reservations.FirstOrDefault()!.Id : (int?)null))
            .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Reservations.FirstOrDefault() != null ? src.Reservations.FirstOrDefault()!.RoomId : (int?)null))
            .ForMember(dest => dest.CheckInDate, opt => opt.MapFrom(src => src.Reservations.FirstOrDefault() != null ? src.Reservations.FirstOrDefault()!.CheckInDate : default(DateTime)))
            .ForMember(dest => dest.CheckOutDate, opt => opt.MapFrom(src => src.Reservations.FirstOrDefault() != null ? src.Reservations.FirstOrDefault()!.CheckOutDate : default(DateTime)));
            

            // Reservation mappings  
            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();
            CreateMap<Reservation, ReservationDetailsDto>()
                .ForMember(r => r.RoomNumber, opt => opt.MapFrom(r => r.Room.Number))
                .ForMember(r => r.RoomType, opt => opt.MapFrom(r => r.Room.Type))
                .ForMember(r => r.PricePerNighr, opt => opt.MapFrom(r => r.Room.PricePerNight))
                .ForMember(g => g.GuestName, opt => opt.MapFrom(sg => sg.Guest.FullName))
                .ForMember(g => g.Phone, opt => opt.MapFrom(g => g.Guest.Phone))
                .ForMember(g => g.Email, opt => opt.MapFrom(g => g.Guest.Email))
                .ForMember(g => g.NationalId, opt => opt.MapFrom(g => g.Guest.NationalId));







        }
    }
}

