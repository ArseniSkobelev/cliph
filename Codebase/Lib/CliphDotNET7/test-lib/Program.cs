using Cliph;
using Cliph.Classes;

using var cliphConnection = new CliphConnection("fb405b6b-1cda-44ed-8d89-5f086c4f3c99");

await cliphConnection.CreateUser(new User
{
    Email = "arseni.skobelev@gmail.com",
    Password = "12345678"
});