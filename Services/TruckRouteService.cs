using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TruckRouteAPI.Controllers;
using TruckRouteAPI.Models;
using TruckRouteAPI.Repository;
using TruckRouteAPI.Services.Interface;
using Route = TruckRouteAPI.Models.Route;

namespace TruckRouteAPI.Services
{
    public class TruckRouteService : ITruckRouteService
    {
        private readonly _DBContext _dBContext;
        public TruckRouteService(_DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public User GetUserById(long id)
        {
            try
            {
                User user = new User();
                user = _dBContext.users.FirstOrDefault(x => x.Id == id);

                if (user == null) 
                    throw new Exception("Usuário inexistente");

                return user;  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateUser(User user)
        {
            try
            {
                _dBContext.users.Add(user);
                _dBContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddRoute(Models.Route route)
        {
            try
            {
                _dBContext.routers.Add(route);
                _dBContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveRoutetoFile(string routeName, string password)
        {
            Route route = new Route();
            route = _dBContext.routers.FirstOrDefault(x => x.RouteName == routeName);

            if (route != null)
            {
                SaveFile(route, password);  
            }
            else
            {
                throw new Exception("Rota não encontrada");
            }
        }

        public void SaveFile(Route route, string password)
        {
            try
            {
                string fileName = route.RouteName;
                string textoParaCriptografar = File.ReadAllText(fileName);

                using (Aes aesAlg = Aes.Create())
                {
                    byte[] senhaBytes = Encoding.UTF8.GetBytes(password);
                    aesAlg.Key = SHA256.Create().ComputeHash(senhaBytes);
                    aesAlg.IV = new byte[16]; 

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (FileStream arquivoCriptografado = new FileStream(fileName + ".encrypted", FileMode.Create))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(arquivoCriptografado, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(cryptoStream))
                            {
                                writer.Write(RouteDetails(route));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao criptografar o arquivo: {ex.Message}");
            }
        }

        public string RouteDetails(Route route)
        {
            return $"Origem: {route.Origin}, Destino: {route.Destiny}, Distância: {route.Distance} km";
        }
    }
}
