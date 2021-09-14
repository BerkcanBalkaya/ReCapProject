using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string BrandAdded = "Brand added";
        public static string BrandUpdated = "Brand updated";
        public static string BrandDeleted = "Brand deleted";
        public static string BrandNameInvalid = "Brand's name is invalid";
        public static string BrandsListed = "Brands listed";
        public static string BrandListedById = "Brand listed by id";

        public static string ColorAdded = "Color added";
        public static string ColorUpdated = "Color updated";
        public static string ColorDeleted = "Color deleted";
        public static string ColorNameInvalid = "Color's name is invalid";
        public static string ColorsListed = "Colors listed";
        public static string ColorListedById = "Color listed by id";

        public static string CarAdded = "Car added";
        public static string CarUpdated = "Car updated";
        public static string CarDeleted = "Car deleted";
        public static string CarNameOrPriceInvalid = "Car's name or price is invalid";
        public static string CarsListed = "Cars listed";
        public static string CarListedById = "Car listed by id";
        
        public static string CustomerAdded = "Customer added";
        public static string CustomerUpdated = "Customer updated";
        public static string CustomerDeleted = "Customer deleted";
        public static string CustomerCompanyNameInvalid = "Customer's company name is invalid";
        public static string CustomersListed = "Customers listed";
        public static string CustomerListedById = "Customer listed by id";

        public static string UserAdded = "User added";
        public static string UserUpdated = "User updated";
        public static string UserDeleted = "User deleted";
        public static string UserInvalid = "User is invalid";
        public static string UsersListed = "Users listed";
        public static string UserListedById = "User listed by id";

        public static string RentalAdded = "Rental added";
        public static string RentalUpdated = "Rental updated";
        public static string RentalDeleted = "Rental deleted";
        public static string RentalInvalid = "Rental is invalid";
        public static string RentalsListed = "Rentals listed";
        public static string RentalListedById = "Rental listed by id";
        public static string RentalDateOfCarInvalid = "Rental date of choosen car is invalid";

        public static string CarDetailListed = "Car detail listed";

        public static string CarImageAdded="CarImage added";
        public static string CarImageUpdated="CarImage updated";
        public static string CarImageDeleted="CarImage deleted";
        public static string CarImagesListed="CarImages listed";
        public static string CarImageListedById="CarImage listed by id";
        public static string CarImageEmpty="CarImage was empty and replaced with default image";
        public static string CarImageNotEmpty="CarImage found";
        public static string CarImageLimit="CarImage limit has been reached";

        public static string ClaimsListedByUser = "Claims listed by user";
        public static string UserListedByEmail = "User listed by email";
        public static string UserNotFound = "User not found";
        public static string PasswordError = "Password error";
        public static string SuccessfulLogin = "Login Successfull";
        public static string UserAlreadyExists = "User already Exists";
        public static string UserRegistered = "User successfuly Registered";
        public static string AccessTokenCreated = "Access token Created";
        public static string AuthorizationDenied = "Autherization Denied";
    }
}
