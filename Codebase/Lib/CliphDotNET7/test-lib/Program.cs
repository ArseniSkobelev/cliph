using Cliph;
using Cliph.Classes;

using var cliphConnection = new CliphConnection("406c39a3-653c-435b-8731-eebd793a112f");

// await cliphConnection.CreateUser(new User
// {
//     Email = "arseni.skobelev@gmail.com",
//     Password = "12345678"
// });

await cliphConnection.CreateSession(new User
{
    Email = "arseni.skobelev@gmail.com",
    Password = "12345678"
});