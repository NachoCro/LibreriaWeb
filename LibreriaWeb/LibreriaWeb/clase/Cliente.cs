namespace LibreriaWeb.clase
{

    public class Cliente
    {
        public string Nombre { get; set; }
        public string Contrasenia { get; set; }

        public Cliente(string nombre, string contrasenia)
        {
            Nombre = nombre;
            Contrasenia = contrasenia;
        }

        public void RegistrarUsario()
        {
            bool validarIngreso = false;
            while (!validarIngreso)
            {
                Console.WriteLine("Ingresa un usuario: (Las mayusculas y los caracteres especiales cuentan)");
                string NombreUsuario = Console.ReadLine().Trim();
                Console.WriteLine("Ingrese una contraseña: ");
                string ContraseniaUsuario = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(NombreUsuario) && !string.IsNullOrEmpty(ContraseniaUsuario))
                {
                    Nombre = NombreUsuario;
                    Contrasenia = ContraseniaUsuario;
                    validarIngreso = true;
                }
                else
                {
                    Console.WriteLine("No ingresaste nada en usuario o contraseña");
                }
            }
        }

        public bool ValidarCliente()
        {
            bool validarIngreso = false;
            while (!validarIngreso)
            {
                Console.WriteLine("Ingresa un usuario: (Las mayusculas y los caracteres especiales cuentan)");
                string NombreUsuario = Console.ReadLine().Trim();
                Console.WriteLine("Ingrese una contraseña: ");
                string ContraseniaUsuario = Console.ReadLine().Trim();
                if (NombreUsuario == Nombre && ContraseniaUsuario == Contrasenia)
                {
                    validarIngreso = true;
                }
                else
                {
                    Console.WriteLine("No ingresaste nada en usuario o contraseña");
                }
            }

            if (validarIngreso)
            {
                return true;
            }

            return false;
        }
    }
}