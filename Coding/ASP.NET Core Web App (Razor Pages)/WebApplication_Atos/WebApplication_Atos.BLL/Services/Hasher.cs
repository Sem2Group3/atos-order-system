using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using WebApplication_Atos.Core.Models;
using System.Buffers.Text;

namespace WebApplication_Atos.BLL.Services
{
    public class Hasher
    {
        private static int argon2idVersion = 19;

        private static int defaultIterations = 10;
        private static int defaultMemoryKiB = 65536;
        private static int defaultDegreeOfParallelism = 2;
        private static int defaultHashLength = 64;

        private static int saltLength = 16;

        private static byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[saltLength];
            RandomNumberGenerator.Fill(salt);
            return salt;
        }

        public static string GenerateSaltAndHashArgon2(string password)
        {
            var pwdBytes = Encoding.UTF8.GetBytes(password);

            byte[] salt = GenerateRandomSalt();

            using var argon2 = new Argon2id(pwdBytes)
            {
                Salt = salt,
                Iterations = defaultIterations,
                MemorySize = defaultMemoryKiB,
                DegreeOfParallelism = defaultDegreeOfParallelism
            };

            byte[] hash = argon2.GetBytes(defaultHashLength);

            string PHC = GenerateArgon2PHC(defaultMemoryKiB, defaultIterations, defaultDegreeOfParallelism,
                salt, hash);
            return PHC;
        }

        public static bool ComparePasswordToPHC(string password, string PHC)
        {
            Dictionary<string, string> storedParameters = Argon2PHCToParameters(PHC);

            byte[] storedHash = Convert.FromBase64String(storedParameters["hashBase64"]);

            int iterations = Convert.ToInt32(storedParameters["iterations"]);
            int memoryKiB = Convert.ToInt32(storedParameters["memoryKiB"]);
            int parallelism = Convert.ToInt32(storedParameters["parallelism"]);
            int hashLength = Convert.ToInt32(storedParameters["hashLength"]);
            byte[] salt = Convert.FromBase64String(storedParameters["saltBase64"]);

            string PHCToCompare = HashUsingArgon2(password, salt, iterations, 
                memoryKiB, parallelism, hashLength);

            Dictionary<string, string> rehashedParameters = Argon2PHCToParameters(PHCToCompare);
            byte[] hashToCompare = Convert.FromBase64String(rehashedParameters["hashBase64"]);

            if (CryptographicOperations.FixedTimeEquals(hashToCompare, storedHash))
            {
                return true;
            }
            else return false;
        }

        // uses default hashing parameters
        public static string HashUsingArgon2(string password, byte[] salt)
        {
            var pwdBytes = Encoding.UTF8.GetBytes(password);

            using var argon2 = new Argon2id(pwdBytes)
            {
                Salt = salt,
                Iterations = defaultIterations,
                MemorySize = defaultMemoryKiB,
                DegreeOfParallelism = defaultDegreeOfParallelism
            };

            byte[] hash = argon2.GetBytes(defaultHashLength);

            string PHC = GenerateArgon2PHC(defaultMemoryKiB, defaultIterations, defaultDegreeOfParallelism,
                salt, hash);
            return PHC;
        }

        // method overload for custom parameters
        public static string HashUsingArgon2(string password, byte[] salt,
            int iterations, int memoryKiB, int parallelism, int hashLength)
        {
            var pwdBytes = Encoding.UTF8.GetBytes(password);

            using var argon2 = new Argon2id(pwdBytes)
            {
                Salt = salt,
                Iterations = iterations,
                MemorySize = memoryKiB,
                DegreeOfParallelism = parallelism
            };

            byte[] hash = argon2.GetBytes(hashLength);

            string PHC = GenerateArgon2PHC(memoryKiB, iterations, parallelism, salt, hash);
            return PHC;
        }

        // the PHC format is the compact way of storing a hashed password with its salt
        // and other parameters needed to recreate the hash for validation
        // format: $argon2id$v=19$m=65536,t=3,p=4$<base64(salt)>$<base64(hash)>
        // here, v = argon2 version, m = memory cost (KiB), t = iterations (time cost), p = parallelism
        private static string GenerateArgon2PHC(int memoryKiB, int iterations, int parallelism, 
            byte[] salt, byte[] hash)
        {
            string saltBase64 = Convert.ToBase64String(salt);
            string hashBase64 = Convert.ToBase64String(hash);

            return $"$argon2id$v={argon2idVersion}$m={memoryKiB},t={iterations},p={parallelism}${saltBase64}${hashBase64}";
        }

        private static Dictionary<string, string> Argon2PHCToParameters(string PHC)
        {
            var parts = PHC.Split('$', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 5)
                throw new FormatException("Invalid PHC format: expected at least 5 segments.");

            var algorithm = parts[0];
            var versionPart = parts[1];
            var parametersPart = parts[2];
            var saltBase64 = parts[3];
            var hashBase64 = parts[4];
            var hashLength = Convert.FromBase64String(hashBase64).Length.ToString();

            var dict = new Dictionary<string, string>();

            // Parse version (e.g. "v=19")
            var versionSplit = versionPart.Split('=', 2);
            if (versionSplit.Length == 2)
                dict["version"] = versionSplit[1];

            // Parse parameters (e.g. "m=65536,t=3,p=4")
            foreach (var param in parametersPart.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                var kv = param.Split('=', 2);
                if (kv.Length == 2)
                {
                    var key = kv[0] switch
                    {
                        "m" => "memoryKiB",
                        "t" => "iterations",
                        "p" => "parallelism",
                        _ => kv[0]
                    };
                    dict[key] = kv[1];
                }
            }

            dict["saltBase64"] = saltBase64;
            dict["hashBase64"] = hashBase64;
            dict["hashLength"] = hashLength;

            return dict;
        }
    }
}
